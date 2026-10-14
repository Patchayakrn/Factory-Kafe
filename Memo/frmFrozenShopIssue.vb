Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Partial Public Class frmFrozenShopIssue
    Inherits Form

    Private currentIssueId As Integer
    Private isSaved As Boolean
    Private selectedProductPN As Long

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        AddHandler btnClose.Click, AddressOf CloseForm
        AddHandler btnPrint.Click, AddressOf PrintDocumentClick
        AddHandler btnSave.Click, AddressOf SaveDocumentClick
        AddHandler btnNew.Click, AddressOf NewDocumentClick
        AddHandler txtProductCode.KeyDown, AddressOf ProductCodeKeyDown
        AddHandler txtProductCode.Leave, AddressOf ProductCodeLeave
        AddHandler btnSelectProduct.Click, AddressOf SelectProductClick
        AddHandler btnAdd.Click, AddressOf AddDetailClick
        AddHandler btnRemove.Click, AddressOf RemoveDetailClick
        AddHandler btnCancel.Click, AddressOf CancelDocumentClick
        AddHandler btnHistory.Click, AddressOf HistoryClick
        AddHandler MyBase.Load, AddressOf FormLoaded
        AddHandler MyBase.Shown, AddressOf FormShown
        btnPrint.Enabled = False
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        EnsureIssueTables()
        dtpOrderDate.Value = Date.Today
        dtpReceiveDate.Value = Date.Today.AddHours(11)
        GenerateDocumentNo()
        UpdatePrintButtonState()
        UpdateTotals()
    End Sub

    Private Sub UpdatePrintButtonState()
        btnPrint.Enabled = isSaved
    End Sub

    Private Sub FormShown(sender As Object, e As EventArgs)
        If MdiParent IsNot Nothing Then
            AddHandler MdiParent.Resize, AddressOf MdiParentResize
        End If

        'The form is created before it is attached to the MDI parent.  Applying
        'the size again here guarantees that it fills the actual MDI client area.
        BeginInvoke(New MethodInvoker(AddressOf FitToMdiWorkspace))
    End Sub

    Private Sub MdiParentResize(sender As Object, e As EventArgs)
        FitToMdiWorkspace()
    End Sub

    Private Sub FitToMdiWorkspace()
        If IsDisposed OrElse MdiParent Is Nothing Then Return

        Dim mdiClient As MdiClient = Nothing
        For Each child As Control In MdiParent.Controls
            If TypeOf child Is MdiClient Then
                mdiClient = DirectCast(child, MdiClient)
                Exit For
            End If
        Next

        If mdiClient Is Nothing Then
            WindowState = FormWindowState.Maximized
            Return
        End If

        SuspendLayout()
        Try
            WindowState = FormWindowState.Normal
            FormBorderStyle = FormBorderStyle.None
            StartPosition = FormStartPosition.Manual
            Location = Point.Empty
            Size = mdiClient.ClientSize
        Finally
            ResumeLayout(True)
        End Try
    End Sub

    Private Sub EnsureIssueTables()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID(N'dbo.FrozenShopIssue', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.FrozenShopIssue (" &
                    "IssueID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_FrozenShopIssue PRIMARY KEY, " &
                    "DocumentNo NVARCHAR(30) NOT NULL CONSTRAINT UQ_FrozenShopIssue_DocumentNo UNIQUE, " &
                    "OrderDate DATE NOT NULL, ReceiveDate DATETIME NULL, Destination NVARCHAR(200) NULL, " &
                    "Remark NVARCHAR(500) NULL, OfficerNumber INT NULL, CreatedAt DATETIME NOT NULL CONSTRAINT DF_FrozenShopIssue_CreatedAt DEFAULT GETDATE()) END; " &
                    "IF OBJECT_ID(N'dbo.FrozenShopIssueDetail', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.FrozenShopIssueDetail (" &
                    "IssueDetailID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_FrozenShopIssueDetail PRIMARY KEY, " &
                    "IssueID INT NOT NULL, [LineNo] INT NOT NULL, PN BIGINT NULL, ProductCode NVARCHAR(50) NOT NULL, " &
                    "ProductName NVARCHAR(200) NOT NULL, Crates DECIMAL(18,2) NOT NULL CONSTRAINT DF_FrozenShopIssueDetail_Crates DEFAULT 0, " &
                    "Kilograms DECIMAL(18,2) NOT NULL CONSTRAINT DF_FrozenShopIssueDetail_Kilograms DEFAULT 0, " &
                    "Remark NVARCHAR(500) NULL) END; " &
                    "IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_FrozenShopIssueDetail_IssueID' AND object_id=OBJECT_ID(N'dbo.FrozenShopIssueDetail')) " &
                    "CREATE INDEX IX_FrozenShopIssueDetail_IssueID ON dbo.FrozenShopIssueDetail(IssueID, [LineNo]);"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 30
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ตรวจสอบตารางใบเบิกสินค้าไม่สำเร็จ: " & ex.Message, "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub GenerateDocumentNo()
        Dim prefix As String = (Date.Today.Year + 543).ToString(CultureInfo.InvariantCulture).Substring(2, 2) & Date.Today.ToString("MM")
        Dim nextNumber As Integer = 1
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String = "SELECT MAX(DocumentNo) FROM dbo.FrozenShopIssue WHERE DocumentNo LIKE @Prefix + N'%'"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 10).Value = prefix
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Dim existing As String = Convert.ToString(value).Trim()
                        If existing.Length > prefix.Length Then Integer.TryParse(existing.Substring(prefix.Length), nextNumber)
                        nextNumber += 1
                    End If
                End Using
            End Using
        Catch
        End Try
        txtDocumentNo.Text = prefix & nextNumber.ToString("000", CultureInfo.InvariantCulture)
    End Sub

    Private Sub ProductCodeKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadProductByCode()
        End If
    End Sub

    Private Sub ProductCodeLeave(sender As Object, e As EventArgs)
        If Not String.IsNullOrWhiteSpace(txtProductCode.Text) Then LoadProductByCode()
    End Sub

    Private Sub SelectProductClick(sender As Object, e As EventArgs)
        Using picker As New fProduct()
            picker.IsSelectMode = True
            picker.AllowOutOfStockSelection = True
            picker.SearchText = txtProductCode.Text.Trim()

            If picker.ShowDialog(Me) = DialogResult.OK Then
                selectedProductPN = picker.SelectedPN
                txtProductCode.Text = If(picker.SelectedProductCode, "").Trim()
                txtProductName.Text = If(picker.SelectedProductName, "").Trim()
                txtProductCode.Focus()
                txtProductCode.SelectionStart = txtProductCode.TextLength
            End If
        End Using
    End Sub

    Private Sub LoadProductByCode()
        selectedProductPN = 0
        txtProductName.Clear()
        Dim code As String = txtProductCode.Text.Trim()
        If code = "" Then Return
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String = "SELECT TOP 1 PN, RTRIM(ISNULL(ProductCode,N'')) AS ProductCode, RTRIM(ISNULL(Name,N'')) AS ProductName FROM dbo.Product WHERE RTRIM(ISNULL(ProductCode,N''))=@Code"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = code
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            selectedProductPN = Convert.ToInt64(reader("PN"))
                            txtProductCode.Text = Convert.ToString(reader("ProductCode")).Trim()
                            txtProductName.Text = Convert.ToString(reader("ProductName")).Trim()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ค้นหาสินค้าไม่สำเร็จ: " & ex.Message, "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub AddDetailClick(sender As Object, e As EventArgs)
        LoadProductByCode()
        If selectedProductPN <= 0 OrElse String.IsNullOrWhiteSpace(txtProductCode.Text) Then
            MessageBox.Show("กรุณาระบุรหัสสินค้าที่มีอยู่ในระบบก่อนเพิ่มรายการ", "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProductCode.Focus()
            Return
        End If
        If nudCrates.Value <= 0D AndAlso nudKilograms.Value <= 0D Then
            MessageBox.Show("กรุณาระบุจำนวนลังหรือกิโลกรัม", "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim rowIndex As Integer = dgvDetails.Rows.Add()
        Dim row As DataGridViewRow = dgvDetails.Rows(rowIndex)
        row.Cells("LineNo").Value = rowIndex + 1
        row.Cells("ProductCode").Value = txtProductCode.Text.Trim()
        row.Cells("ProductName").Value = txtProductName.Text.Trim()
        row.Cells("Crates").Value = nudCrates.Value
        row.Cells("Kilograms").Value = nudKilograms.Value
        row.Cells("Remark").Value = txtLineRemark.Text.Trim()
        row.Tag = selectedProductPN
        ClearDetailInputs()
        UpdateTotals()
        isSaved = False
    End Sub

    Private Sub RemoveDetailClick(sender As Object, e As EventArgs)
        For i As Integer = dgvDetails.SelectedRows.Count - 1 To 0 Step -1
            dgvDetails.Rows.Remove(dgvDetails.SelectedRows(i))
        Next
        RenumberRows()
        UpdateTotals()
        isSaved = False
    End Sub

    Private Sub ClearDetailInputs()
        txtProductCode.Clear()
        txtProductName.Clear()
        txtLineRemark.Clear()
        nudCrates.Value = 0D
        nudKilograms.Value = 0D
        selectedProductPN = 0
        txtProductCode.Focus()
    End Sub

    Private Sub RenumberRows()
        For i As Integer = 0 To dgvDetails.Rows.Count - 1
            dgvDetails.Rows(i).Cells("LineNo").Value = i + 1
        Next
    End Sub

    Private Sub UpdateTotals()
        Dim crates As Decimal = 0D
        Dim kilograms As Decimal = 0D
        For Each row As DataGridViewRow In dgvDetails.Rows
            crates += CellDecimal(row, "Crates")
            kilograms += CellDecimal(row, "Kilograms")
        Next
        If lblTotal IsNot Nothing Then lblTotal.Text = "รวมลัง " & crates.ToString("N2") & "   |   รวมกิโลกรัม " & kilograms.ToString("N2")
    End Sub

    Private Function CellDecimal(row As DataGridViewRow, columnName As String) As Decimal
        If row Is Nothing OrElse row.Cells(columnName).Value Is Nothing Then Return 0D
        Dim result As Decimal
        If Decimal.TryParse(Convert.ToString(row.Cells(columnName).Value), NumberStyles.Any, CultureInfo.CurrentCulture, result) Then Return result
        Return 0D
    End Function

    Private Function SaveDocument(Optional showSuccessMessage As Boolean = True) As Boolean
        If dgvDetails.Rows.Count = 0 Then
            MessageBox.Show("กรุณาเพิ่มรายการสินค้าอย่างน้อย 1 รายการ", "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtDocumentNo.Text) Then Return False

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    If currentIssueId <= 0 Then
                        Const insertHeader As String = "INSERT INTO dbo.FrozenShopIssue (DocumentNo,OrderDate,ReceiveDate,Destination,Remark,OfficerNumber) VALUES (@DocumentNo,@OrderDate,@ReceiveDate,@Destination,@Remark,@OfficerNumber); SELECT CAST(SCOPE_IDENTITY() AS INT);"
                        Using cmd As New SqlCommand(insertHeader, conn, tr)
                            AddHeaderParameters(cmd)
                            currentIssueId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using
                    Else
                        Const updateHeader As String = "UPDATE dbo.FrozenShopIssue SET OrderDate=@OrderDate,ReceiveDate=@ReceiveDate,Destination=@Destination,Remark=@Remark WHERE IssueID=@IssueID"
                        Using cmd As New SqlCommand(updateHeader, conn, tr)
                            AddHeaderParameters(cmd)
                            cmd.Parameters.Add("@IssueID", SqlDbType.Int).Value = currentIssueId
                            cmd.ExecuteNonQuery()
                        End Using
                        Using cmd As New SqlCommand("DELETE FROM dbo.FrozenShopIssueDetail WHERE IssueID=@IssueID", conn, tr)
                            cmd.Parameters.Add("@IssueID", SqlDbType.Int).Value = currentIssueId
                            cmd.ExecuteNonQuery()
                        End Using
                    End If

                    Const insertDetail As String = "INSERT INTO dbo.FrozenShopIssueDetail (IssueID,[LineNo],PN,ProductCode,ProductName,Crates,Kilograms,Remark) VALUES (@IssueID,@LineNo,@PN,@ProductCode,@ProductName,@Crates,@Kilograms,@Remark)"
                    For Each row As DataGridViewRow In dgvDetails.Rows
                        Using cmd As New SqlCommand(insertDetail, conn, tr)
                            cmd.Parameters.Add("@IssueID", SqlDbType.Int).Value = currentIssueId
                            cmd.Parameters.Add("@LineNo", SqlDbType.Int).Value = Convert.ToInt32(row.Cells("LineNo").Value)
                            Dim pn As Long = If(row.Tag Is Nothing, 0L, Convert.ToInt64(row.Tag))
                            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = If(pn > 0, CType(pn, Object), DBNull.Value)
                            cmd.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 50).Value = Convert.ToString(row.Cells("ProductCode").Value).Trim()
                            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 200).Value = Convert.ToString(row.Cells("ProductName").Value).Trim()
                            AddDecimalParameter(cmd, "@Crates", CellDecimal(row, "Crates"))
                            AddDecimalParameter(cmd, "@Kilograms", CellDecimal(row, "Kilograms"))
                            cmd.Parameters.Add("@Remark", SqlDbType.NVarChar, 500).Value = Convert.ToString(row.Cells("Remark").Value).Trim()
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using
            isSaved = True
            UpdatePrintButtonState()
            btnSave.Enabled = False
            If showSuccessMessage Then
                MessageBox.Show("บันทึกใบเบิกสินค้าเรียบร้อยแล้ว", "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("บันทึกใบเบิกสินค้าไม่สำเร็จ: " & ex.Message, "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub AddHeaderParameters(cmd As SqlCommand)
        cmd.Parameters.Add("@DocumentNo", SqlDbType.NVarChar, 30).Value = txtDocumentNo.Text.Trim()
        cmd.Parameters.Add("@OrderDate", SqlDbType.Date).Value = dtpOrderDate.Value.Date
        cmd.Parameters.Add("@ReceiveDate", SqlDbType.DateTime).Value = dtpReceiveDate.Value
        cmd.Parameters.Add("@Destination", SqlDbType.NVarChar, 200).Value = txtDestination.Text.Trim()
        cmd.Parameters.Add("@Remark", SqlDbType.NVarChar, 500).Value = txtHeaderRemark.Text.Trim()
        cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = If(ModSession.LoginOfficerID > 0, CType(ModSession.LoginOfficerID, Object), DBNull.Value)
    End Sub

    Private Sub AddDecimalParameter(cmd As SqlCommand, name As String, value As Decimal)
        Dim parameter As SqlParameter = cmd.Parameters.Add(name, SqlDbType.Decimal)
        parameter.Precision = 18
        parameter.Scale = 2
        parameter.Value = value
    End Sub

    Private Sub SaveDocumentClick(sender As Object, e As EventArgs)
        If Not SaveDocument() Then Return

        'สร้างข้อมูลรายงานไว้ก่อน แล้วจึงเคลียร์รายการบนหน้าจอ
        Dim reportTable As DataTable = BuildFrozenShopReportTable()
        dgvDetails.Rows.Clear()
        UpdateTotals()
        isSaved = False
        UpdatePrintButtonState()

        Using viewer As New frmReportViewer()
            viewer.PrintLoadedReportAfterShown()
            viewer.LoadFrozenShopIssueReport(reportTable)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Sub NewDocumentClick(sender As Object, e As EventArgs)
        If Not isSaved AndAlso dgvDetails.Rows.Count > 0 Then
            Dim answer = MessageBox.Show("รายการปัจจุบันยังไม่ได้บันทึก ต้องการเริ่มรายการใหม่หรือไม่", "Frozen Shop", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If answer <> DialogResult.Yes Then Return
        End If
        currentIssueId = 0
        isSaved = False
        UpdatePrintButtonState()
        btnSave.Enabled = True
        dgvDetails.Rows.Clear()
        txtHeaderRemark.Clear()
        dtpOrderDate.Value = Date.Today
        dtpReceiveDate.Value = Date.Today.AddHours(11)
        GenerateDocumentNo()
        ClearDetailInputs()
        UpdateTotals()
    End Sub

    Private Sub CancelDocumentClick(sender As Object, e As EventArgs)
        NewDocumentClick(sender, e)
    End Sub

    Private Sub HistoryClick(sender As Object, e As EventArgs)
        Using history As New frmFrozenShopIssueHistory()
            If history.ShowDialog(Me) = DialogResult.OK Then
                LoadExistingIssue(history.SelectedIssueId)
            End If
        End Using
    End Sub

    Private Sub LoadExistingIssue(issueId As Integer)
        If issueId <= 0 Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT TOP 1 DocumentNo,OrderDate,ReceiveDate,Destination,Remark FROM dbo.FrozenShopIssue WHERE IssueID=@IssueID", conn)
                    cmd.Parameters.Add("@IssueID", SqlDbType.Int).Value = issueId
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If Not reader.Read() Then
                            MessageBox.Show("ไม่พบใบเบิกที่เลือก", "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return
                        End If

                        txtDocumentNo.Text = Convert.ToString(reader("DocumentNo")).Trim()
                        dtpOrderDate.Value = Convert.ToDateTime(reader("OrderDate"))
                        If reader("ReceiveDate") Is DBNull.Value Then
                            dtpReceiveDate.Value = dtpOrderDate.Value.Date.AddHours(11)
                        Else
                            dtpReceiveDate.Value = Convert.ToDateTime(reader("ReceiveDate"))
                        End If
                        txtDestination.Text = Convert.ToString(reader("Destination")).Trim()
                        txtHeaderRemark.Text = Convert.ToString(reader("Remark")).Trim()
                    End Using
                End Using

                Using cmd As New SqlCommand("SELECT [LineNo],PN,ProductCode,ProductName,Crates,Kilograms,Remark FROM dbo.FrozenShopIssueDetail WHERE IssueID=@IssueID ORDER BY [LineNo]", conn)
                    cmd.Parameters.Add("@IssueID", SqlDbType.Int).Value = issueId
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        dgvDetails.Rows.Clear()
                        While reader.Read()
                            Dim rowIndex As Integer = dgvDetails.Rows.Add()
                            Dim row As DataGridViewRow = dgvDetails.Rows(rowIndex)
                            row.Cells("LineNo").Value = Convert.ToInt32(reader("LineNo"))
                            row.Cells("ProductCode").Value = Convert.ToString(reader("ProductCode")).Trim()
                            row.Cells("ProductName").Value = Convert.ToString(reader("ProductName")).Trim()
                            row.Cells("Crates").Value = Convert.ToDecimal(reader("Crates"))
                            row.Cells("Kilograms").Value = Convert.ToDecimal(reader("Kilograms"))
                            row.Cells("Remark").Value = Convert.ToString(reader("Remark")).Trim()
                            row.Tag = If(reader("PN") Is DBNull.Value, 0L, Convert.ToInt64(reader("PN")))
                        End While
                    End Using
                End Using
            End Using

            currentIssueId = issueId
            isSaved = True
            UpdatePrintButtonState()
            btnSave.Enabled = True
            UpdateTotals()
        Catch ex As Exception
            MessageBox.Show("เรียกใบย้อนหลังไม่สำเร็จ: " & ex.Message, "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CloseForm(sender As Object, e As EventArgs)
        'ใช้ Hide อย่างเดียว เพื่อปิดเฉพาะหน้าจอนี้และไม่กระทบฟอร์มหลัก
        Hide()
        If MdiParent IsNot Nothing Then MdiParent.Activate()
    End Sub

    Private Sub PrintDocumentClick(sender As Object, e As EventArgs)
        If Not isSaved AndAlso Not SaveDocument() Then Return
        Using viewer As New frmReportViewer()
            viewer.LoadFrozenShopIssueReport(BuildFrozenShopReportTable())
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function BuildFrozenShopReportTable() As DataTable
        Dim table As New DataTable("FrozenShopIssue")
        table.Columns.Add("DocumentNo", GetType(String))
        table.Columns.Add("OrderDate", GetType(DateTime))
        table.Columns.Add("ReceiveDate", GetType(DateTime))
        table.Columns.Add("Destination", GetType(String))
        table.Columns.Add("HeaderRemark", GetType(String))
        table.Columns.Add("LineNo", GetType(Integer))
        table.Columns.Add("ProductCode", GetType(String))
        table.Columns.Add("ProductName", GetType(String))
        table.Columns.Add("Crates", GetType(Decimal))
        table.Columns.Add("Kilograms", GetType(Decimal))
        table.Columns.Add("Remark", GetType(String))

        For Each row As DataGridViewRow In dgvDetails.Rows
            Dim output As DataRow = table.NewRow()
            output("DocumentNo") = txtDocumentNo.Text.Trim()
            output("OrderDate") = dtpOrderDate.Value
            output("ReceiveDate") = dtpReceiveDate.Value
            output("Destination") = txtDestination.Text.Trim()
            output("HeaderRemark") = txtHeaderRemark.Text.Trim()
            output("LineNo") = Convert.ToInt32(row.Cells("LineNo").Value)
            output("ProductCode") = Convert.ToString(row.Cells("ProductCode").Value).Trim()
            output("ProductName") = Convert.ToString(row.Cells("ProductName").Value).Trim()
            output("Crates") = CellDecimal(row, "Crates")
            output("Kilograms") = CellDecimal(row, "Kilograms")
            output("Remark") = Convert.ToString(row.Cells("Remark").Value).Trim()
            table.Rows.Add(output)
        Next

        Return table
    End Function

End Class
