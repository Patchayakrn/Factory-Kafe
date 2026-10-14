Imports System.Drawing
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Forms

Public Class frmMemoEmployeeHoliday
    Inherits Form

    Private documentRunningNo As Integer = 1
    Private currentMemoId As Integer
    Private isSaved As Boolean

    Public Sub New()
        InitializeComponent()

        AddHandler btnNew.Click, AddressOf NewDocumentClick
        AddHandler btnAddRow.Click, AddressOf AddRowClick
        AddHandler btnRemoveRow.Click, AddressOf RemoveRowClick
        AddHandler btnHistory.Click, AddressOf HistoryClick
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnClose.Click, AddressOf CloseClick
        AddHandler btnPrint.Click, AddressOf PrintClick
        AddHandler dtpDocumentDate.ValueChanged, AddressOf DocumentDateChanged
        AddHandler gridEmployees.EditingControlShowing, AddressOf EmployeesEditingControlShowing
        AddHandler gridEmployees.CellEndEdit, AddressOf EmployeesCellEndEdit
        btnPrint.Enabled = False
    End Sub

    Private Sub NewDocumentClick(sender As Object, e As EventArgs)
        documentRunningNo += 1
        currentMemoId = 0
        isSaved = False
        UpdatePrintButtonState()
        txtSalesManager.Clear()
        txtBody.Clear()
        dtpDocumentDate.Value = Date.Today
        UpdateDocumentNo()
        gridEmployees.Rows.Clear()
        AddEmployeeRow()
    End Sub

    Private Sub DocumentDateChanged(sender As Object, e As EventArgs)
        UpdateDocumentNo()
    End Sub

    Private Sub UpdateDocumentNo()
        GenerateDocumentNo()
    End Sub

    Private Sub UpdatePrintButtonState()
        btnPrint.Enabled = isSaved
    End Sub

    Private Sub GenerateDocumentNo()
        Dim buddhistYearTwoDigits As Integer = (dtpDocumentDate.Value.Year + 543) Mod 100
        Dim prefix As String = String.Format(CultureInfo.InvariantCulture,
                                             "FTR{0:00}{1:00}",
                                             buddhistYearTwoDigits,
                                             dtpDocumentDate.Value.Month)
        Dim nextNumber As Integer = documentRunningNo

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String = "SELECT MAX(DocumentNo) FROM dbo.EmployeeHolidayMemo WHERE DocumentNo LIKE @Prefix + N'%'"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = prefix
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Dim existing As String = Convert.ToString(value).Trim()
                        If existing.Length > prefix.Length Then
                            Dim existingNumber As Integer
                            If Integer.TryParse(existing.Substring(prefix.Length), existingNumber) Then
                                nextNumber = existingNumber + 1
                            End If
                        End If
                    End If
                End Using
            End Using
        Catch
            'ถ้าฐานข้อมูลยังเชื่อมต่อไม่ได้ ให้ใช้เลขเริ่มต้นของเอกสารแทน
        End Try

        documentRunningNo = Math.Max(nextNumber, 1)
        txtDocumentNo.Text = prefix & documentRunningNo.ToString("000", CultureInfo.InvariantCulture)
    End Sub

    Private Sub AddRowClick(sender As Object, e As EventArgs)
        AddEmployeeRow()
    End Sub

    Private Sub AddEmployeeRow()
        Dim rowNumber As Integer = gridEmployees.Rows.Count + 1
        gridEmployees.Rows.Add(rowNumber, "", "", "", "")
        gridEmployees.CurrentCell = gridEmployees.Rows(gridEmployees.Rows.Count - 1).Cells(1)
        gridEmployees.BeginEdit(True)
    End Sub

    Private employeeCodeEditor As TextBox

    Private Sub EmployeesEditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs)
        If employeeCodeEditor IsNot Nothing Then
            RemoveHandler employeeCodeEditor.KeyDown, AddressOf EmployeeCodeEditorKeyDown
            RemoveHandler employeeCodeEditor.TextChanged, AddressOf EmployeeCodeEditorTextChanged
            employeeCodeEditor = Nothing
        End If

        If gridEmployees.CurrentCell Is Nothing OrElse
           gridEmployees.CurrentCell.ColumnIndex <> colEmployeeCode.Index Then
            Return
        End If

        Dim editor As TextBox = TryCast(e.Control, TextBox)
        If editor Is Nothing Then Return

        employeeCodeEditor = editor
        AddHandler employeeCodeEditor.KeyDown, AddressOf EmployeeCodeEditorKeyDown
        AddHandler employeeCodeEditor.TextChanged, AddressOf EmployeeCodeEditorTextChanged
        EmployeeCodeEditorTextChanged(employeeCodeEditor, EventArgs.Empty)
    End Sub

    Private Sub EmployeeCodeEditorTextChanged(sender As Object, e As EventArgs)
        Dim editor As TextBox = TryCast(sender, TextBox)
        If editor Is Nothing Then Return

        Dim caretPosition As Integer = editor.SelectionStart
        Dim upperText As String = editor.Text.ToUpperInvariant()
        If editor.Text <> upperText Then
            editor.Text = upperText
            editor.SelectionStart = Math.Min(caretPosition, editor.TextLength)
        End If
    End Sub

    Private Sub EmployeeCodeEditorKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter OrElse gridEmployees.CurrentCell Is Nothing Then Return

        e.SuppressKeyPress = True
        e.Handled = True

        Dim rowIndex As Integer = gridEmployees.CurrentCell.RowIndex
        gridEmployees.EndEdit()
        LookupEmployeeName(rowIndex)
    End Sub

    Private Sub EmployeesCellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 OrElse e.ColumnIndex <> colEmployeeCode.Index Then Return
        LookupEmployeeName(e.RowIndex)
    End Sub

    Private Sub LookupEmployeeName(rowIndex As Integer)
        If rowIndex < 0 OrElse rowIndex >= gridEmployees.Rows.Count Then Return

        Dim row As DataGridViewRow = gridEmployees.Rows(rowIndex)
        Dim officerCode As String = Convert.ToString(row.Cells("colEmployeeCode").Value).Trim()
        row.Cells("colEmployeeName").Value = ""

        If officerCode.Length = 0 Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 RTRIM(ISNULL(Name, N'')) " &
                    "FROM Officer " &
                    "WHERE UPPER(RTRIM(ISNULL(OfficerCode, N''))) = @OfficerCode"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@OfficerCode", SqlDbType.NVarChar, 50).Value = officerCode
                    Dim result As Object = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value OrElse String.IsNullOrWhiteSpace(Convert.ToString(result)) Then
                        MessageBox.Show("ไม่พบรหัสพนักงาน " & officerCode, "ค้นหาพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        gridEmployees.CurrentCell = row.Cells("colEmployeeCode")
                        gridEmployees.BeginEdit(True)
                        Return
                    End If

                    row.Cells("colEmployeeName").Value = Convert.ToString(result).Trim()
                    gridEmployees.CurrentCell = row.Cells("colOldHoliday")
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ค้นหาพนักงานไม่สำเร็จ: " & ex.Message, "ค้นหาพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RemoveRowClick(sender As Object, e As EventArgs)
        If gridEmployees.CurrentRow Is Nothing OrElse gridEmployees.CurrentRow.IsNewRow Then Return
        gridEmployees.Rows.Remove(gridEmployees.CurrentRow)
        For i As Integer = 0 To gridEmployees.Rows.Count - 1
            gridEmployees.Rows(i).Cells("colLine").Value = i + 1
        Next
    End Sub

    Private Sub CloseClick(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub HistoryClick(sender As Object, e As EventArgs)
        Using history As New frmMemoEmployeeHolidayHistory()
            If history.ShowDialog(Me) = DialogResult.OK Then
                LoadExistingMemo(history.SelectedMemoId)
            End If
        End Using
    End Sub

    Private Sub LoadExistingMemo(memoId As Integer)
        If memoId <= 0 Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using headerCommand As New SqlCommand(
                    "SELECT TOP 1 DocumentNo,DocumentDate,SubjectText,BodyText " &
                    "FROM dbo.EmployeeHolidayMemo WHERE MemoID=@MemoID", conn)
                    headerCommand.Parameters.Add("@MemoID", SqlDbType.Int).Value = memoId
                    Using reader As SqlDataReader = headerCommand.ExecuteReader()
                        If Not reader.Read() Then
                            MessageBox.Show("ไม่พบเอกสารที่เลือก", "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If

                        txtDocumentNo.Text = Convert.ToString(reader("DocumentNo")).Trim()
                        If reader("DocumentDate") IsNot DBNull.Value Then dtpDocumentDate.Value = Convert.ToDateTime(reader("DocumentDate"))
                        txtSalesManager.Text = Convert.ToString(reader("SubjectText")).Trim()
                        txtBody.Text = Convert.ToString(reader("BodyText")).Trim()
                    End Using
                End Using

                gridEmployees.Rows.Clear()
                Using detailCommand As New SqlCommand(
                    "SELECT [LineNo],OfficerCode,OfficerName,OldHoliday,NewHoliday " &
                    "FROM dbo.EmployeeHolidayMemoDetail WHERE MemoID=@MemoID ORDER BY [LineNo]", conn)
                    detailCommand.Parameters.Add("@MemoID", SqlDbType.Int).Value = memoId
                    Using reader As SqlDataReader = detailCommand.ExecuteReader()
                        While reader.Read()
                            gridEmployees.Rows.Add(
                                Convert.ToInt32(reader("LineNo")),
                                Convert.ToString(reader("OfficerCode")).Trim(),
                                Convert.ToString(reader("OfficerName")).Trim(),
                                Convert.ToString(reader("OldHoliday")).Trim(),
                                Convert.ToString(reader("NewHoliday")).Trim())
                        End While
                    End Using
                End Using
            End Using

            currentMemoId = memoId
            isSaved = True
            UpdatePrintButtonState()
            If gridEmployees.Rows.Count = 0 Then AddEmployeeRow()
        Catch ex As Exception
            MessageBox.Show("เรียกดูเอกสารย้อนหลังไม่สำเร็จ: " & ex.Message, "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function EnsureHolidayMemoTables() As Boolean
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID(N'dbo.EmployeeHolidayMemo', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.EmployeeHolidayMemo (" &
                    "MemoID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EmployeeHolidayMemo PRIMARY KEY, " &
                    "DocumentNo NVARCHAR(30) NOT NULL CONSTRAINT UQ_EmployeeHolidayMemo_DocumentNo UNIQUE, " &
                    "DocumentDate DATE NOT NULL, SubjectText NVARCHAR(500) NULL, BodyText NVARCHAR(MAX) NULL, " &
                    "CreatedBy INT NULL, CreatedAt DATETIME NOT NULL CONSTRAINT DF_EmployeeHolidayMemo_CreatedAt DEFAULT GETDATE()) END; " &
                    "IF OBJECT_ID(N'dbo.EmployeeHolidayMemoDetail', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.EmployeeHolidayMemoDetail (" &
                    "MemoDetailID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EmployeeHolidayMemoDetail PRIMARY KEY, " &
                    "MemoID INT NOT NULL, [LineNo] INT NOT NULL, OfficerCode NVARCHAR(50) NOT NULL, " &
                    "OfficerName NVARCHAR(200) NULL, OldHoliday NVARCHAR(100) NULL, NewHoliday NVARCHAR(100) NULL) END; " &
                    "IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name=N'FK_EmployeeHolidayMemoDetail_Memo') " &
                    "ALTER TABLE dbo.EmployeeHolidayMemoDetail ADD CONSTRAINT FK_EmployeeHolidayMemoDetail_Memo " &
                    "FOREIGN KEY (MemoID) REFERENCES dbo.EmployeeHolidayMemo(MemoID); " &
                    "IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_EmployeeHolidayMemoDetail_MemoID' " &
                    "AND object_id=OBJECT_ID(N'dbo.EmployeeHolidayMemoDetail')) " &
                    "CREATE INDEX IX_EmployeeHolidayMemoDetail_MemoID ON dbo.EmployeeHolidayMemoDetail(MemoID, [LineNo]);"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 30
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("สร้างตารางบันทึกวันหยุดพนักงานไม่สำเร็จ: " & ex.Message,
                            "บันทึกวันหยุดพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Sub SaveClick(sender As Object, e As EventArgs)
        If Not SaveDocument(True) Then Return

        'เก็บข้อมูลไว้สร้างรายงานก่อน แล้วเริ่มเอกสารใหม่เพื่อล้างข้อมูลบนหน้าจอ
        Dim reportTable As DataTable = BuildEmployeeHolidayReportTable()
        NewDocumentClick(Nothing, EventArgs.Empty)

        Using viewer As New frmReportViewer()
            viewer.PrintLoadedReportAfterShown()
            viewer.LoadEmployeeHolidayMemoReport(reportTable)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function SaveDocument(showSuccessMessage As Boolean) As Boolean
        gridEmployees.EndEdit()

        If Not EnsureHolidayMemoTables() Then Return False
        If String.IsNullOrWhiteSpace(txtDocumentNo.Text) Then
            MessageBox.Show("ไม่พบเลขที่เอกสาร", "บันทึกวันหยุดพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim detailRows As New List(Of DataGridViewRow)()
        For Each row As DataGridViewRow In gridEmployees.Rows
            Dim code As String = Convert.ToString(row.Cells("colEmployeeCode").Value).Trim().ToUpperInvariant()
            If code.Length = 0 Then Continue For

            row.Cells("colEmployeeCode").Value = code
            Dim name As String = Convert.ToString(row.Cells("colEmployeeName").Value).Trim()
            If name.Length = 0 Then
                MessageBox.Show("กรุณาใส่รหัสพนักงานแล้วกด Enter ให้แสดงชื่อก่อนบันทึก", "บันทึกวันหยุดพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                gridEmployees.CurrentCell = row.Cells("colEmployeeCode")
                gridEmployees.BeginEdit(True)
                Return False
            End If
            detailRows.Add(row)
        Next

        If detailRows.Count = 0 Then
            MessageBox.Show("กรุณาเพิ่มรายชื่อพนักงานอย่างน้อย 1 รายการ", "บันทึกวันหยุดพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    If currentMemoId <= 0 Then
                        Const insertHeader As String =
                            "INSERT INTO dbo.EmployeeHolidayMemo " &
                            "(DocumentNo,DocumentDate,SubjectText,BodyText,CreatedBy) " &
                            "VALUES (@DocumentNo,@DocumentDate,@SubjectText,@BodyText,@CreatedBy); " &
                            "SELECT CAST(SCOPE_IDENTITY() AS INT);"
                        Using cmd As New SqlCommand(insertHeader, conn, tr)
                            AddHeaderParameters(cmd)
                            currentMemoId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using
                    Else
                        Const updateHeader As String =
                            "UPDATE dbo.EmployeeHolidayMemo SET DocumentDate=@DocumentDate,SubjectText=@SubjectText,BodyText=@BodyText, " &
                            "CreatedBy=@CreatedBy WHERE MemoID=@MemoID"
                        Using cmd As New SqlCommand(updateHeader, conn, tr)
                            AddHeaderParameters(cmd)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = currentMemoId
                            cmd.ExecuteNonQuery()
                        End Using
                        Using cmd As New SqlCommand("DELETE FROM dbo.EmployeeHolidayMemoDetail WHERE MemoID=@MemoID", conn, tr)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = currentMemoId
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    Const insertDetail As String =
                        "INSERT INTO dbo.EmployeeHolidayMemoDetail " &
                        "(MemoID,[LineNo],OfficerCode,OfficerName,OldHoliday,NewHoliday) " &
                        "VALUES (@MemoID,@LineNo,@OfficerCode,@OfficerName,@OldHoliday,@NewHoliday)"
                    For Each row As DataGridViewRow In detailRows
                        Using cmd As New SqlCommand(insertDetail, conn, tr)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = currentMemoId
                            cmd.Parameters.Add("@LineNo", SqlDbType.Int).Value = Convert.ToInt32(row.Cells("colLine").Value)
                            cmd.Parameters.Add("@OfficerCode", SqlDbType.NVarChar, 50).Value = Convert.ToString(row.Cells("colEmployeeCode").Value).Trim()
                            cmd.Parameters.Add("@OfficerName", SqlDbType.NVarChar, 200).Value = Convert.ToString(row.Cells("colEmployeeName").Value).Trim()
                            cmd.Parameters.Add("@OldHoliday", SqlDbType.NVarChar, 100).Value = Convert.ToString(row.Cells("colOldHoliday").Value).Trim()
                            cmd.Parameters.Add("@NewHoliday", SqlDbType.NVarChar, 100).Value = Convert.ToString(row.Cells("colNewHoliday").Value).Trim()
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using

            isSaved = True
            UpdatePrintButtonState()
            If showSuccessMessage Then
                MessageBox.Show("บันทึกเอกสารแจ้งวันหยุดพนักงานเรียบร้อยแล้ว", "บันทึกวันหยุดพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("บันทึกเอกสารไม่สำเร็จ: " & ex.Message, "บันทึกวันหยุดพนักงาน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub AddHeaderParameters(cmd As SqlCommand)
        cmd.Parameters.Add("@DocumentNo", SqlDbType.NVarChar, 30).Value = txtDocumentNo.Text.Trim()
        cmd.Parameters.Add("@DocumentDate", SqlDbType.Date).Value = dtpDocumentDate.Value.Date
        cmd.Parameters.Add("@SubjectText", SqlDbType.NVarChar, 500).Value = txtSalesManager.Text.Trim()
        cmd.Parameters.Add("@BodyText", SqlDbType.NVarChar, -1).Value = txtBody.Text.Trim()
        cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = If(ModSession.LoginOfficerID > 0, CType(ModSession.LoginOfficerID, Object), DBNull.Value)
    End Sub

    Private Sub PrintClick(sender As Object, e As EventArgs)
        If Not SaveDocument(False) Then Return

        Using viewer As New frmReportViewer()
            viewer.LoadEmployeeHolidayMemoReport(BuildEmployeeHolidayReportTable())
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function BuildEmployeeHolidayReportTable() As DataTable
        Dim table As New DataTable("EmployeeHolidayMemo")
        table.Columns.Add("DocumentDate", GetType(DateTime))
        table.Columns.Add("DocumentNo", GetType(String))
        table.Columns.Add("SubjectText", GetType(String))
        table.Columns.Add("BodyText", GetType(String))
        table.Columns.Add("Recipient1", GetType(String))
        table.Columns.Add("Recipient2", GetType(String))
        table.Columns.Add("CopyRecipient", GetType(String))
        table.Columns.Add("LineNo", GetType(Integer))
        table.Columns.Add("OfficerCode", GetType(String))
        table.Columns.Add("OfficerName", GetType(String))
        table.Columns.Add("OldHoliday", GetType(String))
        table.Columns.Add("NewHoliday", GetType(String))

        Dim recipient1 As String = TextBox1.Text.Trim() & "    " & TextBox2.Text.Trim()
        Dim recipient2 As String = TextBox4.Text.Trim() & "    " & TextBox3.Text.Trim()
        Dim copyRecipient As String = TextBox6.Text.Trim() & "    " & TextBox5.Text.Trim()

        For Each row As DataGridViewRow In gridEmployees.Rows
            Dim code As String = Convert.ToString(row.Cells("colEmployeeCode").Value).Trim()
            If code.Length = 0 Then Continue For

            Dim lineNo As Integer = table.Rows.Count + 1
            If row.Cells("colLine").Value IsNot Nothing Then Integer.TryParse(Convert.ToString(row.Cells("colLine").Value), lineNo)

            Dim output As DataRow = table.NewRow()
            output("DocumentDate") = dtpDocumentDate.Value.Date
            output("DocumentNo") = txtDocumentNo.Text.Trim()
            output("SubjectText") = txtSalesManager.Text.Trim()
            output("BodyText") = txtBody.Text.Trim()
            output("Recipient1") = recipient1.Trim()
            output("Recipient2") = recipient2.Trim()
            output("CopyRecipient") = copyRecipient.Trim()
            output("LineNo") = lineNo
            output("OfficerCode") = code.ToUpperInvariant()
            output("OfficerName") = Convert.ToString(row.Cells("colEmployeeName").Value).Trim()
            output("OldHoliday") = Convert.ToString(row.Cells("colOldHoliday").Value).Trim()
            output("NewHoliday") = Convert.ToString(row.Cells("colNewHoliday").Value).Trim()
            table.Rows.Add(output)
        Next

        Return table
    End Function

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        EnsureHolidayMemoTables()
        dtpDocumentDate.Value = Date.Today
        UpdateDocumentNo()
        If gridEmployees.Rows.Count = 0 Then AddEmployeeRow()
    End Sub
End Class
