Option Explicit On
Option Strict On

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Threading

Public Module DatabaseBackup
    Private ReadOnly autoBackupSync As New Object()
    Private autoBackupTimer As System.Threading.Timer
    Private autoBackupRunning As Boolean
    Private autoBackupOwner As Form
    Private autoBackupNextRetry As DateTime = DateTime.MinValue
    Private autoBackupFailureNotifiedDate As DateTime = DateTime.MinValue

    Public Sub StartAutomaticBackup(Optional owner As Form = Nothing)
        SyncLock autoBackupSync
            If autoBackupTimer IsNot Nothing OrElse Not IsAutomaticBackupEnabled() Then Return
            autoBackupOwner = owner
            autoBackupNextRetry = DateTime.MinValue
            autoBackupFailureNotifiedDate = DateTime.MinValue
            autoBackupTimer = New System.Threading.Timer(AddressOf AutomaticBackupTick,
                                                          Nothing,
                                                          TimeSpan.FromMinutes(1),
                                                          TimeSpan.FromMinutes(1))
        End SyncLock
    End Sub

    Public Sub StopAutomaticBackup()
        SyncLock autoBackupSync
            If autoBackupTimer Is Nothing Then Return
            autoBackupTimer.Dispose()
            autoBackupTimer = Nothing
            autoBackupOwner = Nothing
        End SyncLock
    End Sub

    Public Sub BackupCurrentDatabase(owner As Form)
        Dim oldCursor As Cursor = If(owner Is Nothing, Cursors.Default, owner.Cursor)

        Try
            Dim backupPath As String = PerformBackup(owner, True)
            If String.IsNullOrWhiteSpace(backupPath) Then Return

            MessageBox.Show("สำรองฐานข้อมูลเรียบร้อยแล้ว" & Environment.NewLine &
                            "ไฟล์ถูกบันทึกไว้ที่" & Environment.NewLine & backupPath,
                            "Backup Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("สำรองฐานข้อมูลไม่สำเร็จ" & Environment.NewLine & ex.Message,
                            "Backup Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If owner IsNot Nothing Then owner.Cursor = oldCursor
        End Try
    End Sub

    Private Sub AutomaticBackupTick(state As Object)
        If autoBackupRunning OrElse Not IsAutomaticBackupEnabled() Then Return

        Dim now As DateTime = DateTime.Now
        If now < autoBackupNextRetry Then Return
        Dim backupTime As TimeSpan = GetAutomaticBackupTime()
        If now.TimeOfDay < backupTime Then Return

        SyncLock autoBackupSync
            If autoBackupRunning Then Return
            autoBackupRunning = True
        End SyncLock

        Try
            Dim backupPath As String = PerformBackup(Nothing, False)
            If Not String.IsNullOrWhiteSpace(backupPath) Then
                Dim backupFolder As String = Path.GetDirectoryName(backupPath)
                Dim databaseName As String = Path.GetFileName(backupPath).Split(" "c)(0)
                WriteAutomaticBackupMarker(backupFolder, databaseName, now.Date)
                DeleteOldBackups(backupFolder, databaseName, GetAutomaticBackupKeepDays(), now)
                WriteAutomaticBackupLog(backupFolder, "สำรองฐานข้อมูลอัตโนมัติสำเร็จ: " & backupPath)
                autoBackupNextRetry = DateTime.MinValue
                autoBackupFailureNotifiedDate = DateTime.MinValue
            End If
        Catch ex As Exception
            autoBackupNextRetry = now.AddMinutes(30)
            Try
                Dim folder As String = GetBackupFolder()
                Directory.CreateDirectory(folder)
                WriteAutomaticBackupLog(folder, "สำรองฐานข้อมูลอัตโนมัติไม่สำเร็จ: " & ex.Message)
            Catch
            End Try
            If autoBackupFailureNotifiedDate.Date <> now.Date Then
                autoBackupFailureNotifiedDate = now.Date
                ShowAutomaticBackupFailure(ex.Message)
            End If
        Finally
            SyncLock autoBackupSync
                autoBackupRunning = False
            End SyncLock
        End Try
    End Sub

    Private Sub ShowAutomaticBackupFailure(errorMessage As String)
        Dim owner As Form = autoBackupOwner
        If owner Is Nothing OrElse owner.IsDisposed Then Return

        Dim showAction As Action = Sub()
                                       MessageBox.Show(owner,
                                                       "สำรองฐานข้อมูลอัตโนมัติไม่สำเร็จ" & Environment.NewLine &
                                                       errorMessage & Environment.NewLine & Environment.NewLine &
                                                       "ระบบจะลองใหม่อีกครั้งภายใน 30 นาที" & Environment.NewLine &
                                                       "กรุณาตรวจสอบสิทธิ์เขียนโฟลเดอร์ Backup",
                                                       "Auto Backup แจ้งเตือน",
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Warning)
                                   End Sub
        Try
            If owner.InvokeRequired Then
                owner.BeginInvoke(showAction)
            Else
                showAction()
            End If
        Catch
        End Try
    End Sub

    Private Function PerformBackup(owner As Form, showConfirmation As Boolean) As String
        Dim backupPath As String = ""
        Dim oldCursor As Cursor = If(owner Is Nothing, Cursors.Default, owner.Cursor)

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim builder As New SqlConnectionStringBuilder(conn.ConnectionString)
                Dim databaseName As String = builder.InitialCatalog.Trim()
                If String.IsNullOrWhiteSpace(databaseName) Then
                    Throw New ApplicationException("ไม่พบชื่อฐานข้อมูลใน Connection String")
                End If

                Dim warning As String = ""
                If builder.DataSource.IndexOf("(local)", StringComparison.OrdinalIgnoreCase) < 0 AndAlso
                   Not String.Equals(builder.DataSource, ".", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not String.Equals(builder.DataSource, "localhost", StringComparison.OrdinalIgnoreCase) Then
                    warning = "คำเตือน: ไฟล์จะถูกสร้างบนเครื่องที่ SQL Server ทำงานอยู่ ไม่ใช่เครื่อง Client" &
                              Environment.NewLine & Environment.NewLine
                End If

                If showConfirmation AndAlso MessageBox.Show(warning & "ต้องการสำรองฐานข้อมูล " & databaseName & " ใช่หรือไม่?",
                                                            "ยืนยันการสำรองฐานข้อมูล", MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                    Return ""
                End If

                Dim backupFolder As String = GetBackupFolder()
                Directory.CreateDirectory(backupFolder)
                If owner IsNot Nothing Then owner.Cursor = Cursors.WaitCursor

                Dim serverDate As DateTime
                Using dateCmd As New SqlCommand("SELECT GETDATE()", conn)
                    dateCmd.CommandTimeout = 30
                    serverDate = Convert.ToDateTime(dateCmd.ExecuteScalar())
                End Using

                If Not showConfirmation AndAlso HasAutomaticBackupToday(backupFolder, databaseName, serverDate.Date) Then
                    Return ""
                End If

                Dim fileName As String = databaseName & " " & serverDate.ToString("yyyy-MM-dd HH-mm-ss") & ".bak"
                backupPath = Path.Combine(backupFolder, fileName)
                Dim backupName As String = databaseName & " backup " & serverDate.ToString("yyyy-MM-dd HH:mm:ss")
                Dim sql As String =
                    "BACKUP DATABASE " & QuoteIdentifier(databaseName) &
                    " TO DISK = @BackupPath WITH INIT, NAME = @BackupName"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 1800
                    cmd.Parameters.Add("@BackupPath", SqlDbType.NVarChar, 4000).Value = backupPath
                    cmd.Parameters.Add("@BackupName", SqlDbType.NVarChar, 128).Value = backupName
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return backupPath
        Finally
            If owner IsNot Nothing Then owner.Cursor = oldCursor
        End Try
    End Function

    Private Function GetBackupFolder() As String
        Dim configuredFolder As String = ConfigurationManager.AppSettings("AutoBackup.Folder")
        If String.IsNullOrWhiteSpace(configuredFolder) Then
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
                                "Coffee_SMH2", "Database Backup")
        End If
        Return Environment.ExpandEnvironmentVariables(configuredFolder.Trim())
    End Function

    Private Function IsAutomaticBackupEnabled() As Boolean
        Dim value As String = ConfigurationManager.AppSettings("AutoBackup.Enabled")
        If String.IsNullOrWhiteSpace(value) Then Return True
        Dim result As Boolean
        If Boolean.TryParse(value, result) Then Return result
        Return True
    End Function

    Private Function GetAutomaticBackupTime() As TimeSpan
        Dim value As String = ConfigurationManager.AppSettings("AutoBackup.Time")
        Dim result As DateTime
        If DateTime.TryParseExact(value, "HH:mm", CultureInfo.InvariantCulture,
                                  DateTimeStyles.None, result) Then
            Return result.TimeOfDay
        End If
        Return New TimeSpan(23, 0, 0)
    End Function

    Private Function GetAutomaticBackupKeepDays() As Integer
        Dim value As Integer
        If Integer.TryParse(ConfigurationManager.AppSettings("AutoBackup.KeepDays"), value) Then
            Return Math.Max(1, value)
        End If
        Return 30
    End Function

    Private Function GetMarkerPath(folder As String, databaseName As String) As String
        Return Path.Combine(folder, databaseName & ".autobackup.last")
    End Function

    Private Function HasAutomaticBackupToday(folder As String, databaseName As String, backupDate As DateTime) As Boolean
        Dim markerPath As String = GetMarkerPath(folder, databaseName)
        If Not File.Exists(markerPath) Then Return False
        Dim text As String = File.ReadAllText(markerPath).Trim()
        Dim markerDate As DateTime
        Return DateTime.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, markerDate) AndAlso markerDate.Date = backupDate.Date
    End Function

    Private Sub WriteAutomaticBackupMarker(folder As String, databaseName As String, backupDate As DateTime)
        File.WriteAllText(GetMarkerPath(folder, databaseName), backupDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
    End Sub

    Private Sub DeleteOldBackups(folder As String, databaseName As String, keepDays As Integer, now As DateTime)
        Dim cutoff As DateTime = now.AddDays(-Math.Max(1, keepDays))
        For Each filePath As String In Directory.GetFiles(folder, databaseName & " *.bak")
            Try
                If File.GetLastWriteTime(filePath) < cutoff Then File.Delete(filePath)
            Catch
            End Try
        Next
    End Sub

    Private Sub WriteAutomaticBackupLog(folder As String, message As String)
        File.AppendAllText(Path.Combine(folder, "AutoBackup.log"),
                           DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) &
                           " | " & message & Environment.NewLine)
    End Sub

    Private Function QuoteIdentifier(identifier As String) As String
        Return "[" & identifier.Replace("]", "]]" ) & "]"
    End Function
End Module
