Imports System.Drawing
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Forms

Public Class frmMemoPromotion
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
                Const sql As String = "SELECT MAX(DocumentNo) FROM dbo.PromotionMemo WHERE DocumentNo LIKE @Prefix + N'%'"
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
        Using history As New frmMemoPromotionHistory()
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
                    "FROM dbo.PromotionMemo WHERE MemoID=@MemoID", conn)
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
                    "SELECT [LineNo],PromotionText,RewardText,ActivityDateText " &
                    "FROM dbo.PromotionMemoDetail WHERE MemoID=@MemoID ORDER BY [LineNo]", conn)
                    detailCommand.Parameters.Add("@MemoID", SqlDbType.Int).Value = memoId
                    Using reader As SqlDataReader = detailCommand.ExecuteReader()
                        While reader.Read()
                            gridEmployees.Rows.Add(
                                Convert.ToInt32(reader("LineNo")),
                                Convert.ToString(reader("PromotionText")).Trim(),
                                Convert.ToString(reader("RewardText")).Trim(),
                                Convert.ToString(reader("ActivityDateText")).Trim(),
                                "")
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

    Private Function EnsurePromotionMemoTables() As Boolean
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID(N'dbo.PromotionMemo', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.PromotionMemo (" &
                    "MemoID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PromotionMemo PRIMARY KEY, " &
                    "DocumentNo NVARCHAR(30) NOT NULL CONSTRAINT UQ_PromotionMemo_DocumentNo UNIQUE, " &
                    "DocumentDate DATE NOT NULL, SubjectText NVARCHAR(500) NULL, BodyText NVARCHAR(MAX) NULL, " &
                    "CreatedBy INT NULL, CreatedAt DATETIME NOT NULL CONSTRAINT DF_PromotionMemo_CreatedAt DEFAULT GETDATE()) END; " &
                    "IF OBJECT_ID(N'dbo.PromotionMemoDetail', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.PromotionMemoDetail (" &
                    "MemoDetailID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PromotionMemoDetail PRIMARY KEY, " &
                    "MemoID INT NOT NULL, [LineNo] INT NOT NULL, PromotionText NVARCHAR(1000) NULL, " &
                    "RewardText NVARCHAR(1000) NULL, ActivityDateText NVARCHAR(200) NULL, " &
                    "CONSTRAINT FK_PromotionMemoDetail_Memo FOREIGN KEY (MemoID) REFERENCES dbo.PromotionMemo(MemoID), " &
                    "CONSTRAINT UQ_PromotionMemoDetail_Memo_Line UNIQUE (MemoID, [LineNo])) END; " &
                    "IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_PromotionMemoDetail_MemoID' " &
                    "AND object_id=OBJECT_ID(N'dbo.PromotionMemoDetail')) " &
                    "CREATE INDEX IX_PromotionMemoDetail_MemoID ON dbo.PromotionMemoDetail(MemoID, [LineNo]);"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 30
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("สร้างตารางบันทึกกิจกรรม/โปรโมชั่นไม่สำเร็จ: " & ex.Message,
                            "บันทึกกิจกรรม/โปรโมชั่น", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Sub SaveClick(sender As Object, e As EventArgs)
        If Not SaveDocument(True) Then Return

        'เก็บข้อมูลไว้สร้างรายงานก่อน แล้วเริ่มเอกสารใหม่เพื่อล้างข้อมูลบนหน้าจอ
        Dim reportTable As DataTable = BuildPromotionReportTable()
        NewDocumentClick(Nothing, EventArgs.Empty)

        Using viewer As New frmReportViewer()
            viewer.PrintLoadedReportAfterShown()
            viewer.LoadMemoPromotionReport(reportTable, reportTable.Rows.Count >= 2)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function SaveDocument(showSuccessMessage As Boolean) As Boolean
        gridEmployees.EndEdit()

        If Not EnsurePromotionMemoTables() Then Return False
        If String.IsNullOrWhiteSpace(txtDocumentNo.Text) Then
            MessageBox.Show("ไม่พบเลขที่เอกสาร", "บันทึกกิจกรรม/โปรโมชั่น", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim detailRows As New List(Of DataGridViewRow)()
        For Each row As DataGridViewRow In gridEmployees.Rows
            Dim promotionText As String = Convert.ToString(row.Cells("colEmployeeCode").Value).Trim()
            Dim rewardText As String = Convert.ToString(row.Cells("colEmployeeName").Value).Trim()
            Dim activityDateText As String = Convert.ToString(row.Cells("colOldHoliday").Value).Trim()
            If promotionText.Length = 0 AndAlso rewardText.Length = 0 AndAlso activityDateText.Length = 0 Then Continue For

            If promotionText.Length = 0 Then
                MessageBox.Show("กรุณาระบุกิจกรรมหรือโปรโมชั่น", "บันทึกกิจกรรม/โปรโมชั่น", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                gridEmployees.CurrentCell = row.Cells("colEmployeeCode")
                gridEmployees.BeginEdit(True)
                Return False
            End If
            detailRows.Add(row)
        Next

        If detailRows.Count = 0 Then
            MessageBox.Show("กรุณาเพิ่มกิจกรรมหรือโปรโมชั่นอย่างน้อย 1 รายการ", "บันทึกกิจกรรม/โปรโมชั่น", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    If currentMemoId <= 0 Then
                        Const insertHeader As String =
                            "INSERT INTO dbo.PromotionMemo " &
                            "(DocumentNo,DocumentDate,SubjectText,BodyText,CreatedBy) " &
                            "VALUES (@DocumentNo,@DocumentDate,@SubjectText,@BodyText,@CreatedBy); " &
                            "SELECT CAST(SCOPE_IDENTITY() AS INT);"
                        Using cmd As New SqlCommand(insertHeader, conn, tr)
                            AddHeaderParameters(cmd)
                            currentMemoId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using
                    Else
                        Const updateHeader As String =
                        "UPDATE dbo.PromotionMemo SET DocumentDate=@DocumentDate,SubjectText=@SubjectText,BodyText=@BodyText, " &
                            "CreatedBy=@CreatedBy WHERE MemoID=@MemoID"
                        Using cmd As New SqlCommand(updateHeader, conn, tr)
                            AddHeaderParameters(cmd)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = currentMemoId
                            cmd.ExecuteNonQuery()
                        End Using
                        Using cmd As New SqlCommand("DELETE FROM dbo.PromotionMemoDetail WHERE MemoID=@MemoID", conn, tr)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = currentMemoId
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    Const insertDetail As String =
                        "INSERT INTO dbo.PromotionMemoDetail " &
                        "(MemoID,[LineNo],PromotionText,RewardText,ActivityDateText) " &
                        "VALUES (@MemoID,@LineNo,@PromotionText,@RewardText,@ActivityDateText)"
                    For Each row As DataGridViewRow In detailRows
                        Using cmd As New SqlCommand(insertDetail, conn, tr)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = currentMemoId
                            cmd.Parameters.Add("@LineNo", SqlDbType.Int).Value = Convert.ToInt32(row.Cells("colLine").Value)
                            cmd.Parameters.Add("@PromotionText", SqlDbType.NVarChar, 1000).Value = Convert.ToString(row.Cells("colEmployeeCode").Value).Trim()
                            cmd.Parameters.Add("@RewardText", SqlDbType.NVarChar, 1000).Value = Convert.ToString(row.Cells("colEmployeeName").Value).Trim()
                            cmd.Parameters.Add("@ActivityDateText", SqlDbType.NVarChar, 200).Value = Convert.ToString(row.Cells("colOldHoliday").Value).Trim()
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using

            isSaved = True
            UpdatePrintButtonState()
            If showSuccessMessage Then
                MessageBox.Show("บันทึกเอกสารกิจกรรม/โปรโมชั่นเรียบร้อยแล้ว", "บันทึกกิจกรรม/โปรโมชั่น", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("บันทึกเอกสารไม่สำเร็จ: " & ex.Message, "บันทึกกิจกรรม/โปรโมชั่น", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        Dim reportTable As DataTable = BuildPromotionReportTable()
        Using viewer As New frmReportViewer()
            viewer.LoadMemoPromotionReport(reportTable, reportTable.Rows.Count >= 2)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function BuildPromotionReportTable() As DataTable
        Dim table As New DataTable("MemoPromotion")
        table.Columns.Add("DocumentDate", GetType(DateTime))
        table.Columns.Add("DocumentNo", GetType(String))
        table.Columns.Add("SubjectText", GetType(String))
        table.Columns.Add("BodyText", GetType(String))
        table.Columns.Add("Recipient1", GetType(String))
        table.Columns.Add("Recipient2", GetType(String))
        table.Columns.Add("CopyRecipient", GetType(String))
        table.Columns.Add("LineNo", GetType(Integer))
        table.Columns.Add("PromotionText", GetType(String))
        table.Columns.Add("RewardText", GetType(String))
        table.Columns.Add("ActivityDateText", GetType(String))

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
            output("PromotionText") = code
            output("RewardText") = Convert.ToString(row.Cells("colEmployeeName").Value).Trim()
            output("ActivityDateText") = Convert.ToString(row.Cells("colOldHoliday").Value).Trim()
            table.Rows.Add(output)
        Next

        Return table
    End Function

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        EnsurePromotionMemoTables()
        dtpDocumentDate.Value = Date.Today
        UpdateDocumentNo()
        If gridEmployees.Rows.Count = 0 Then AddEmployeeRow()
    End Sub
End Class
