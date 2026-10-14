Imports System.Data.SqlClient
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmVatSell
    Inherits Form

    Private ReadOnly reportTable As DataTable = CreateReportTable()
    Private ReadOnly monthlyTable As DataTable = CreateMonthlyReportTable()

    Private Const AllBranchesText As String = "ไม่ระบุสาขา"
    Private Const AllCompaniesText As String = "- ทั้งหมด -"
    Private Const NoCompanyText As String = "- เฉพาะใบขายที่ไม่ได้ระบุบริษัท -"

    Private Class MonthlyKey
        Public Property BranchNumber As Integer
        Public Property ClientNumber As Integer
        Public Property RDBranchName As String
        Public Property POSID As String
    End Class

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Shared Function CreateReportTable() As DataTable
        Dim table As New DataTable("ReportSell")
        table.Columns.Add("DocType", GetType(Integer))
        table.Columns.Add("DocTypeName", GetType(String))
        table.Columns.Add("DateTime", GetType(DateTime))
        table.Columns.Add("BillNumber", GetType(String))
        table.Columns.Add("CustomerName", GetType(String))
        table.Columns.Add("RDBranchName", GetType(String))
        table.Columns.Add("TaxCode", GetType(String))
        table.Columns.Add("PriceBV", GetType(Decimal))
        table.Columns.Add("VAT", GetType(Decimal))
        table.Columns.Add("BranchName", GetType(String))
        table.Columns.Add("CompanyName", GetType(String))
        table.Columns.Add("MachineNumber", GetType(String))
        table.Columns.Add("InvoiceNumber", GetType(String))
        table.Columns.Add("Exempt", GetType(Decimal))
        table.Columns.Add("TotalSales", GetType(Decimal))
        Return table
    End Function

    Private Shared Function CreateMonthlyReportTable() As DataTable
        Dim table As New DataTable("ReportSell2")
        table.Columns.Add("BranchDetail", GetType(String))
        table.Columns.Add("ClientNumberAndPOSID", GetType(String))
        table.Columns.Add("ClientNumber", GetType(Integer))
        table.Columns.Add("RDBranchName", GetType(String))
        table.Columns.Add("POSID", GetType(String))
        table.Columns.Add("CustomerName", GetType(String))
        table.Columns.Add("SellDay", GetType(Integer))
        table.Columns.Add("DocumentDate", GetType(DateTime))
        table.Columns.Add("BillSort", GetType(String))
        table.Columns.Add("DocumentType", GetType(Integer))
        table.Columns.Add("DocumentTypeName", GetType(String))
        table.Columns.Add("SellInvoice", GetType(String))
        table.Columns.Add("CNInvoice", GetType(String))
        table.Columns.Add("PriceBV", GetType(Decimal))
        table.Columns.Add("VAT", GetType(Decimal))
        table.Columns.Add("PriceNV", GetType(Decimal))
        table.Columns.Add("TotalPrice", GetType(Decimal))
        Return table
    End Function

    Private Sub frmVatSell_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureReportTabs()
        LoadBranches()
        LoadCompanies()
        SetLatestAvailableMonth()
        BuildInitialGrid()
    End Sub

    Private Sub ConfigureReportTabs()
        TabPage1.Text = "รายงานปกติ"
        TabPage2.Text = "รายงาน (รายเดือน)"
        TabPage1.Padding = New Padding(8)
        TabPage2.Padding = New Padding(8)
        MoveReportGridToSelectedTab()
    End Sub

    Private Sub frmVatSell_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'The grid is created/rebound during Load. Apply the selected tab layout
        'once more after WinForms has completed the initial layout pass.
        ApplySelectedTabLayout()
    End Sub

    Private Sub ReportTabChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        MoveReportGridToSelectedTab()
        ApplySelectedTabLayout()
    End Sub

    Private Sub MoveReportGridToSelectedTab()
        Dim targetTab As TabPage = If(TabControl1.SelectedTab Is TabPage2, TabPage2, TabPage1)
        If grid.Parent IsNot targetTab Then
            If grid.Parent IsNot Nothing Then grid.Parent.Controls.Remove(grid)
            targetTab.Controls.Add(grid)
        End If
        grid.Dock = DockStyle.Fill
        grid.BringToFront()
        If grid.DataSource Is Nothing OrElse Not grid.DataSource.Equals(If(TabControl1.SelectedTab Is TabPage2, monthlyTable, reportTable)) Then
            grid.DataSource = If(TabControl1.SelectedTab Is TabPage2, monthlyTable, reportTable)
        End If
    End Sub

    Private Sub ApplySelectedTabLayout()
        If TabControl1.SelectedTab Is TabPage2 Then
            grid.DataSource = monthlyTable
            view.PopulateColumns()
            ConfigureMonthlyColumns()
        Else
            grid.DataSource = reportTable
            view.PopulateColumns()
            ConfigureNormalColumns()
        End If
    End Sub

    Private Sub frmVatSell_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub LoadBranches()
        cbBranch.Items.Clear()
        cbBranch.Items.Add(AllBranchesText)

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT BranchNumber, BranchName FROM Branch ORDER BY BranchNumber ASC", conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cbBranch.Items.Add(String.Format("{0} - {1}", reader.GetInt32(0), reader.GetString(1).Trim()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลสาขาไม่สำเร็จ: " & ex.Message, "Vat Sell", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        cbBranch.SelectedIndex = If(cbBranch.Items.Count > 1, 1, 0)
    End Sub

    Private Sub LoadCompanies()
        Dim table As New DataTable()
        table.Columns.Add("ID", GetType(Integer))
        table.Columns.Add("Name", GetType(String))
        table.Rows.Add(-1, AllCompaniesText)
        table.Rows.Add(0, NoCompanyText)

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT ID, RTRIM(Name) AS Name FROM Company WHERE Status IS NULL ORDER BY ID ASC", conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            table.Rows.Add(reader.GetInt32(0), ReadString(reader, "Name"))
                        End While
                    End Using
                End Using
            End Using
        Catch
        End Try

        cbCompany.ValueMember = "ID"
        cbCompany.DisplayMember = "Name"
        cbCompany.DataSource = table
        cbCompany.SelectedIndex = 0
    End Sub

    Private Sub SetLatestAvailableMonth()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT MAX(MaxDate) FROM (" &
                    "SELECT MAX(Sell.DateTime) AS MaxDate FROM Sell WHERE Sell.Status IS NULL " &
                    "UNION ALL " &
                    "SELECT MAX(CNCustomer.DateTime) AS MaxDate FROM CNCustomer WHERE CNCustomer.Status IS NULL" &
                    ") X"

                Using cmd As New SqlCommand(sql, conn)
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then dpMonth.Value = CDate(value)
                End Using
            End Using
        Catch
            dpMonth.Value = DateTime.Now
        End Try
    End Sub

    Private Sub BuildInitialGrid()
        reportTable.Clear()
        monthlyTable.Clear()
        grid.DataSource = If(TabControl1.SelectedTab Is TabPage2, monthlyTable, reportTable)
        UpdateSummary()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        SearchVatSell()
    End Sub

    Private Sub SearchVatSell()
        Dim firstDay As New DateTime(dpMonth.Value.Year, dpMonth.Value.Month, 1)
        Dim lastDay As DateTime = firstDay.AddMonths(1).AddDays(-1)
        Dim selectedBranch As Integer? = GetSelectedBranchNumber()
        Dim selectedCompany As Integer = CInt(cbCompany.SelectedValue)
        Dim totalBeforeVat As Decimal = 0D
        Dim totalVat As Decimal = 0D

        Me.Cursor = Cursors.WaitCursor
        reportTable.Clear()

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                LoadSellRows(conn, firstDay, lastDay, selectedBranch, selectedCompany, totalBeforeVat, totalVat)
                LoadCreditNoteRows(conn, firstDay, lastDay, selectedBranch, totalBeforeVat, totalVat)
                LoadMonthlyRows(conn, firstDay, lastDay, selectedBranch, selectedCompany)
            End Using

            grid.DataSource = If(TabControl1.SelectedTab Is TabPage2, monthlyTable, reportTable)
            ApplySelectedTabLayout()
            UpdateSummary(totalBeforeVat, totalVat)

            If reportTable.Rows.Count = 0 Then
                MessageBox.Show("ไม่พบข้อมูลภาษีขายในเดือน/สาขา/ประเภท VAT/บริษัทที่เลือก", "Vat Sell", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("ค้นหารายงานภาษีขายไม่สำเร็จ: " & ex.Message, "Vat Sell", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadSellRows(conn As SqlConnection,
                             firstDay As DateTime,
                             lastDay As DateTime,
                             selectedBranch As Integer?,
                             selectedCompany As Integer,
                             ByRef totalBeforeVat As Decimal,
                             ByRef totalVat As Decimal)
        Dim sql As String =
            "SELECT Sell.DateTime, RTRIM(Sell.BillNumber) AS BillNumber, " &
            "RTRIM(Customer.Name) AS CustomerName, RTRIM(Customer.RDBranchName) AS RDBranchName, " &
            "RTRIM(Customer.TaxCode) AS TaxCode, Sell.PriceBV, (Sell.TotalPrice - Sell.PriceBV) AS VAT, " &
            "RTRIM(Branch.BranchName) AS BranchName, RTRIM(Company.Name) AS CompanyName " &
            "FROM Sell " &
            "LEFT OUTER JOIN Customer ON Sell.CustomerNumber = Customer.CustomerNumber " &
            "LEFT OUTER JOIN Branch ON Sell.BranchNumber = Branch.BranchNumber " &
            "LEFT OUTER JOIN Company ON Sell.CompanyID = Company.ID " &
            "WHERE Sell.Status IS NULL AND Sell.DateTime >= @DateFrom AND Sell.DateTime <= @DateTo "

        sql &= BuildOptionalFilters("Sell", selectedBranch)

        If selectedCompany = 0 Then
            sql &= "AND Sell.CompanyID IS NULL "
        ElseIf selectedCompany > 0 Then
            sql &= "AND Sell.CompanyID = @CompanyID "
        End If

        sql &= "ORDER BY Sell.DateTime ASC"

        Using cmd As New SqlCommand(sql, conn)
            AddCommonParameters(cmd, firstDay, lastDay, selectedBranch)
            If selectedCompany > 0 Then cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = selectedCompany

            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim priceBeforeVat As Decimal = ReadDecimal(reader, "PriceBV")
                    Dim vat As Decimal = ReadDecimal(reader, "VAT")

                    Dim row As DataRow = reportTable.NewRow()
                    row("DocType") = 0
                    row("DocTypeName") = "ขาย"
                    row("DateTime") = reader("DateTime")
                    row("BillNumber") = ReadString(reader, "BillNumber")
                    row("CustomerName") = ReadString(reader, "CustomerName")
                    row("RDBranchName") = ReadString(reader, "RDBranchName")
                    row("TaxCode") = ReadString(reader, "TaxCode")
                    row("PriceBV") = priceBeforeVat
                    row("VAT") = vat
                    row("BranchName") = ReadString(reader, "BranchName")
                    row("CompanyName") = ReadString(reader, "CompanyName")
                    row("MachineNumber") = ""
                    row("InvoiceNumber") = ""
                    row("Exempt") = 0D
                    row("TotalSales") = priceBeforeVat + vat
                    reportTable.Rows.Add(row)

                    totalBeforeVat += priceBeforeVat
                    totalVat += vat
                End While
            End Using
        End Using
    End Sub

    Private Sub LoadCreditNoteRows(conn As SqlConnection,
                                   firstDay As DateTime,
                                   lastDay As DateTime,
                                   selectedBranch As Integer?,
                                   ByRef totalBeforeVat As Decimal,
                                   ByRef totalVat As Decimal)
        Dim sql As String =
            "SELECT CNCustomer.DateTime, RTRIM(CNCustomer.DocNumber) AS DocNumber, " &
            "RTRIM(CNCustomer.BillNumber) AS BillNumber, RTRIM(Customer.Name) AS CustomerName, " &
            "RTRIM(Customer.RDBranchName) AS RDBranchName, RTRIM(Customer.TaxCode) AS TaxCode, " &
            "CNCustomer.PriceBV, (CNCustomer.TotalPrice - CNCustomer.PriceBV) AS VAT, " &
            "RTRIM(Branch.BranchName) AS BranchName " &
            "FROM CNCustomer " &
            "LEFT OUTER JOIN Customer ON CNCustomer.CustomerNumber = Customer.CustomerNumber " &
            "LEFT OUTER JOIN Branch ON CNCustomer.BranchNumber = Branch.BranchNumber " &
            "WHERE CNCustomer.Status IS NULL AND CNCustomer.DateTime >= @DateFrom AND CNCustomer.DateTime <= @DateTo "

        sql &= BuildOptionalFilters("CNCustomer", selectedBranch)
        sql &= "ORDER BY CNCustomer.DateTime ASC"

        Using cmd As New SqlCommand(sql, conn)
            AddCommonParameters(cmd, firstDay, lastDay, selectedBranch)

            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim priceBeforeVat As Decimal = -1D * ReadDecimal(reader, "PriceBV")
                    Dim vat As Decimal = -1D * ReadDecimal(reader, "VAT")
                    Dim billNumber As String = ReadString(reader, "BillNumber")
                    If billNumber = "" Then billNumber = "DocNo " & ReadString(reader, "DocNumber")

                    Dim row As DataRow = reportTable.NewRow()
                    row("DocType") = 1
                    row("DocTypeName") = "ลดหนี้"
                    row("DateTime") = reader("DateTime")
                    row("BillNumber") = billNumber
                    row("CustomerName") = ReadString(reader, "CustomerName")
                    row("RDBranchName") = ReadString(reader, "RDBranchName")
                    row("TaxCode") = ReadString(reader, "TaxCode")
                    row("PriceBV") = priceBeforeVat
                    row("VAT") = vat
                    row("BranchName") = ReadString(reader, "BranchName")
                    row("CompanyName") = ""
                    row("MachineNumber") = ""
                    row("InvoiceNumber") = ""
                    row("Exempt") = 0D
                    row("TotalSales") = priceBeforeVat + vat
                    reportTable.Rows.Add(row)

                    totalBeforeVat += priceBeforeVat
                    totalVat += vat
                End While
            End Using
        End Using
    End Sub

    Private Sub LoadMonthlyRows(conn As SqlConnection,
                                firstDay As DateTime,
                                lastDay As DateTime,
                                selectedBranch As Integer?,
                                selectedCompany As Integer)
        monthlyTable.Clear()
        monthlyTable.DefaultView.Sort = "DocumentDate ASC, BillSort ASC, DocumentType ASC"
        Dim sellWhere As String = "S.Status IS NULL AND S.DateTime >= @DateFrom AND S.DateTime <= @DateTo"
        Dim cnWhere As String = "C.Status IS NULL AND C.DateTime >= @DateFrom AND C.DateTime <= @DateTo"
        If selectedBranch.HasValue Then
            sellWhere &= " AND S.BranchNumber=@BranchNumber"
            cnWhere &= " AND C.BranchNumber=@BranchNumber"
        End If
        If cbViewType.SelectedIndex = 0 Then
            sellWhere &= " AND S.PercentVat > 0"
            cnWhere &= " AND C.PercentVat > 0"
        ElseIf cbViewType.SelectedIndex = 1 Then
            sellWhere &= " AND S.PercentVat = 0"
            cnWhere &= " AND C.PercentVat = 0"
        End If
        If selectedCompany = 0 Then
            sellWhere &= " AND S.CompanyID IS NULL"
        ElseIf selectedCompany > 0 Then
            sellWhere &= " AND S.CompanyID=@CompanyID"
        End If

        'Keep one row per document. The previous implementation created every day of the
        'month and then grouped all bills from the same day into an invoice range.
        Dim monthlySql As String =
            "SELECT D.DocumentType,D.DocumentDate,D.BillSort,D.BranchNumber,D.ClientNumber," &
            "D.RDBranchName,D.POSID,D.CustomerName,D.SellInvoice,D.CNInvoice,D.PriceBV,D.VAT,D.PriceNV,D.TotalPrice " &
            "FROM (" &
            "SELECT 0 AS DocumentType,S.DateTime AS DocumentDate," &
            "ISNULL(NULLIF(RTRIM(S.BillNumber),N''),N'') AS BillSort," &
            "S.BranchNumber,S.ClientNumber,RTRIM(ISNULL(B.RDBranchName,N'')) AS RDBranchName," &
            "RTRIM(ISNULL(BC.POSID,N'')) AS POSID,RTRIM(ISNULL(CU.Name,N'')) AS CustomerName," &
            "ISNULL(NULLIF(RTRIM(S.BillNumber),N''),N'') AS SellInvoice,N'' AS CNInvoice," &
            "S.PriceBV,(S.TotalPrice-S.PriceBV) AS VAT,S.PriceNV,S.TotalPrice " &
            "FROM Sell S " &
            "LEFT JOIN Branch B ON B.BranchNumber=S.BranchNumber " &
            "LEFT JOIN BranchClient BC ON BC.BranchNumber=S.BranchNumber AND BC.ClientNumber=S.ClientNumber " &
            "LEFT JOIN Customer CU ON CU.CustomerNumber=S.CustomerNumber " &
            "WHERE " & sellWhere & " " &
            "UNION ALL " &
            "SELECT 1 AS DocumentType,C.DateTime AS DocumentDate," &
            "CASE WHEN C.BillNumber IS NULL OR RTRIM(C.BillNumber)=N'' THEN CONVERT(nvarchar(100),C.DocNumber) ELSE RTRIM(C.BillNumber) END AS BillSort," &
            "C.BranchNumber,1 AS ClientNumber,RTRIM(ISNULL(B.RDBranchName,N'')) AS RDBranchName," &
            "RTRIM(ISNULL(BC.POSID,N'')) AS POSID,RTRIM(ISNULL(CU.Name,N'')) AS CustomerName,N'' AS SellInvoice," &
            "CASE WHEN C.BillNumber IS NULL OR RTRIM(C.BillNumber)=N'' THEN CONVERT(nvarchar(100),C.DocNumber) ELSE RTRIM(C.BillNumber) END AS CNInvoice," &
            "-1*C.PriceBV AS PriceBV,-1*(C.TotalPrice-C.PriceBV) AS VAT,-1*C.PriceNV AS PriceNV,-1*C.TotalPrice AS TotalPrice " &
            "FROM CNCustomer C " &
            "LEFT JOIN Branch B ON B.BranchNumber=C.BranchNumber " &
            "LEFT JOIN BranchClient BC ON BC.BranchNumber=C.BranchNumber AND BC.ClientNumber=1 " &
            "LEFT JOIN Customer CU ON CU.CustomerNumber=C.CustomerNumber " &
            "WHERE " & cnWhere & " " &
            ") D " &
            "ORDER BY D.DocumentDate ASC,D.BillSort ASC,D.DocumentType ASC"

        Using cmd As New SqlCommand(monthlySql, conn)
            AddCommonParameters(cmd, firstDay, lastDay, selectedBranch)
            If selectedCompany > 0 Then cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = selectedCompany
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    Dim documentDate As DateTime = Convert.ToDateTime(rd("DocumentDate"))
                    Dim documentType As Integer = Convert.ToInt32(rd("DocumentType"))
                    Dim branchNumber As Integer = Convert.ToInt32(rd("BranchNumber"))
                    Dim clientNumber As Integer = Convert.ToInt32(rd("ClientNumber"))
                    Dim branchName As String = ReadString(rd, "RDBranchName")
                    Dim billNumber As String = ReadString(rd, "BillSort")

                    Dim row As DataRow = monthlyTable.NewRow()
                    row("BranchDetail") = String.Format("[{0:000}] {1}", branchNumber, If(branchName = "", "สาขาที่ " & branchNumber.ToString(), branchName))
                    row("ClientNumberAndPOSID") = String.Format("[{0:000}]", clientNumber)
                    row("ClientNumber") = clientNumber
                    row("RDBranchName") = branchName
                    row("POSID") = ReadString(rd, "POSID")
                    row("CustomerName") = ReadString(rd, "CustomerName")
                    row("SellDay") = documentDate.Day
                    row("DocumentDate") = documentDate
                    row("BillSort") = billNumber
                    row("DocumentType") = documentType
                    row("DocumentTypeName") = If(documentType = 0, "ขาย", "ลดหนี้")
                    row("SellInvoice") = If(documentType = 0, billNumber, "")
                    row("CNInvoice") = If(documentType = 1, billNumber, "")
                    row("PriceBV") = ReadDecimal(rd, "PriceBV")
                    row("VAT") = ReadDecimal(rd, "VAT")
                    row("PriceNV") = ReadDecimal(rd, "PriceNV")
                    row("TotalPrice") = ReadDecimal(rd, "TotalPrice")
                    monthlyTable.Rows.Add(row)
                End While
            End Using
        End Using
    End Sub

    Private Shared Function FormatInvoiceRange(firstValue As Object, lastValue As Object) As String
        If firstValue Is Nothing OrElse firstValue Is DBNull.Value Then Return ""
        Dim firstText As String = Convert.ToString(firstValue).Trim()
        If firstText = "" Then Return ""
        If lastValue Is Nothing OrElse lastValue Is DBNull.Value Then Return firstText
        Dim lastText As String = Convert.ToString(lastValue).Trim()
        If lastText = "" OrElse String.Equals(firstText, lastText, StringComparison.OrdinalIgnoreCase) Then Return firstText
        Return firstText & " - " & lastText
    End Function

    Private Function BuildOptionalFilters(tableName As String, selectedBranch As Integer?) As String
        Dim sql As String = ""

        If selectedBranch.HasValue Then
            sql &= "AND " & tableName & ".BranchNumber = @BranchNumber "
        End If

        Select Case cbViewType.SelectedIndex
            Case 0
                sql &= "AND " & tableName & ".PercentVat > 0 "
            Case 1
                sql &= "AND " & tableName & ".PercentVat = 0 "
        End Select

        Return sql
    End Function

    Private Sub AddCommonParameters(cmd As SqlCommand,
                                    firstDay As DateTime,
                                    lastDay As DateTime,
                                    selectedBranch As Integer?)
        cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = firstDay.Date
        cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = lastDay.Date.AddDays(1).AddMilliseconds(-1)
        If selectedBranch.HasValue Then cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = selectedBranch.Value
    End Sub

    Private Function GetSelectedBranchNumber() As Integer?
        If cbBranch.SelectedIndex <= 0 Then Return Nothing

        Dim text As String = cbBranch.Text
        Dim dashIndex As Integer = text.IndexOf(" -", StringComparison.Ordinal)
        If dashIndex <= 0 Then Return Nothing

        Dim branchNumber As Integer
        If Integer.TryParse(text.Substring(0, dashIndex).Trim(), branchNumber) Then Return branchNumber
        Return Nothing
    End Function

    Private Sub ConfigureNormalColumns()
        If view.Columns.Count = 0 Then Return
        For Each column In view.Columns
            column.Visible = False
        Next

        SetCaption("DocTypeName", "ประเภท", 90, HorzAlignment.Center)
        SetCaption("DateTime", "วันที่", 115, HorzAlignment.Center, "dd/MM/yyyy")
        SetCaption("BillNumber", "เลขที่", 130, HorzAlignment.Near)
        SetCaption("CustomerName", "ชื่อลูกค้า", 260, HorzAlignment.Near)
        SetCaption("RDBranchName", "ชื่อสาขาตามกรมสรรพากร", 190, HorzAlignment.Near)
        SetCaption("TaxCode", "เลขประจำตัวผู้เสียภาษี", 170, HorzAlignment.Near)
        SetCaption("PriceBV", "จำนวนเงินก่อน VAT", 150, HorzAlignment.Far, "N2", True)
        SetCaption("VAT", "VAT", 130, HorzAlignment.Far, "N2", True)
        SetCaption("BranchName", "สาขา", 160, HorzAlignment.Near)
        SetCaption("CompanyName", "บริษัท", 190, HorzAlignment.Near)

        Dim fields As String() = {"DocTypeName", "DateTime", "BillNumber", "CustomerName", "RDBranchName", "TaxCode", "PriceBV", "VAT", "BranchName", "CompanyName"}
        For index As Integer = 0 To fields.Length - 1
            If view.Columns(fields(index)) IsNot Nothing Then
                view.Columns(fields(index)).Visible = True
                view.Columns(fields(index)).VisibleIndex = index
            End If
        Next
        view.RefreshData()
    End Sub

    Private Sub ConfigureMonthlyColumns()
        If view.Columns.Count = 0 Then Return
        For Each column In view.Columns
            column.Visible = False
        Next

        SetCaption("DocumentTypeName", "ประเภท", 85, HorzAlignment.Center)
        SetCaption("BranchDetail", "สาขา", 220, HorzAlignment.Near)
        SetCaption("ClientNumberAndPOSID", "หมายเลขเครื่อง", 130, HorzAlignment.Center)
        SetCaption("CustomerName", "ชื่อลูกค้า", 180, HorzAlignment.Near)
        SetCaption("SellDay", "วันที่", 75, HorzAlignment.Center, "N0")
        SetCaption("SellInvoice", "เลขที่ใบขาย", 180, HorzAlignment.Near)
        SetCaption("CNInvoice", "เลขที่ใบลดหนี้", 180, HorzAlignment.Near)
        SetCaption("PriceBV", "จำนวนเงิน", 120, HorzAlignment.Far, "N2", True)
        SetCaption("VAT", "ภาษีมูลค่าเพิ่ม", 140, HorzAlignment.Far, "N2", True)
        SetCaption("PriceNV", "ยกเว้นภาษี", 120, HorzAlignment.Far, "N2", True)
        SetCaption("TotalPrice", "รวมยอดขาย", 140, HorzAlignment.Far, "N2", True)

        Dim fields As String() = {"DocumentTypeName", "BranchDetail", "ClientNumberAndPOSID", "CustomerName", "SellDay", "SellInvoice", "CNInvoice", "PriceBV", "VAT", "PriceNV", "TotalPrice"}
        For index As Integer = 0 To fields.Length - 1
            Dim fieldName As String = fields(index)
            If view.Columns(fieldName) IsNot Nothing Then view.Columns(fieldName).Visible = True
            If view.Columns(fieldName) IsNot Nothing Then view.Columns(fieldName).VisibleIndex = index
        Next

        view.ColumnPanelRowHeight = 36
        view.RowHeight = 30
        view.OptionsView.ShowFooter = True
        view.RefreshData()
    End Sub

    Private Sub SetCaption(fieldName As String,
                           caption As String,
                           width As Integer,
                           alignment As HorzAlignment,
                           Optional formatString As String = "",
                           Optional showSummary As Boolean = False)
        Dim col = view.Columns(fieldName)
        If col Is Nothing Then Return

        col.Caption = caption
        col.Width = width
        col.AppearanceCell.TextOptions.HAlignment = alignment
        col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center

        If formatString <> "" Then
            col.DisplayFormat.FormatType = If(fieldName = "DateTime", FormatType.DateTime, FormatType.Numeric)
            col.DisplayFormat.FormatString = formatString
        End If

        If showSummary Then
            col.Summary.Clear()
            col.Summary.Add(SummaryItemType.Sum, fieldName, "{0:N2}")
        End If
    End Sub

    Private Sub UpdateSummary(Optional totalBeforeVat As Decimal = 0D, Optional totalVat As Decimal = 0D)
        lblSummary.Text = String.Format("Sum Money Before VAT = {0:N2}   |   Sum VAT = {1:N2}", totalBeforeVat, totalVat)
        Dim rowCount As Integer = If(TabControl1.SelectedTab Is TabPage2, monthlyTable.Rows.Count, reportTable.Rows.Count)
        lblCount.Text = rowCount.ToString("N0") & " รายการ"
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If TabControl1.SelectedTab Is TabPage2 Then
            If monthlyTable.Rows.Count = 0 Then
                MessageBox.Show("ไม่สามารถพิมพ์ได้ เนื่องจากยังไม่มีรายการใน List", "Vat Sell", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim monthlyCompanyInfo As String() = GetSelectedCompanyInfo()
            Using viewer As New frmReportViewer()
                viewer.LoadVatSellMonthlyReport(monthlyTable, dpMonth.Value, cbBranch.Text, monthlyCompanyInfo(0), monthlyCompanyInfo(1), monthlyCompanyInfo(2), monthlyCompanyInfo(3))
                viewer.ShowDialog(Me)
            End Using
            Return
        End If

        If reportTable.Rows.Count = 0 Then
            MessageBox.Show("ไม่สามารถพิมพ์ได้ เนื่องจากยังไม่มีรายการใน List", "Vat Sell", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim totalBeforeVat As Decimal = 0D
        Dim totalVat As Decimal = 0D
        For Each row As DataRow In reportTable.Rows
            totalBeforeVat += ReadDecimal(row, "PriceBV")
            totalVat += ReadDecimal(row, "VAT")
        Next

        Dim companyInfo As String() = GetSelectedCompanyInfo()
        Using viewer As New frmReportViewer()
            viewer.LoadVatSellReport(reportTable, dpMonth.Value, cbBranch.Text, companyInfo(0), totalBeforeVat, totalVat, companyInfo(1), companyInfo(2), companyInfo(3))
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function GetSelectedCompanyInfo() As String()
        Dim result As String() = {cbCompany.Text, "", "", ""}
        Dim companyId As Integer
        If cbCompany.SelectedValue Is Nothing OrElse Not Integer.TryParse(Convert.ToString(cbCompany.SelectedValue), companyId) OrElse companyId <= 0 Then Return result

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT TOP 1 RTRIM(Name) AS Name, RTRIM(Address) AS Address, RTRIM(Tel) AS Tel, RTRIM(TaxCode) AS TaxCode FROM Company WHERE ID = @ID", conn)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = companyId
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            result(0) = ReadString(reader, "Name")
                            result(1) = ReadString(reader, "Address")
                            result(2) = ReadString(reader, "Tel")
                            result(3) = ReadString(reader, "TaxCode")
                        End If
                    End Using
                End Using
            End Using
        Catch
        End Try
        Return result
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Shared Function ReadString(reader As SqlDataReader, fieldName As String) As String
        Dim ordinal As Integer = reader.GetOrdinal(fieldName)
        If reader.IsDBNull(ordinal) Then Return ""
        Return Convert.ToString(reader.GetValue(ordinal)).Trim()
    End Function

    Private Shared Function ReadDecimal(reader As SqlDataReader, fieldName As String) As Decimal
        Dim ordinal As Integer = reader.GetOrdinal(fieldName)
        If reader.IsDBNull(ordinal) Then Return 0D
        Return Convert.ToDecimal(reader.GetValue(ordinal))
    End Function

    Private Shared Function ReadDecimal(row As DataRow, fieldName As String) As Decimal
        If row Is Nothing OrElse Not row.Table.Columns.Contains(fieldName) OrElse row.IsNull(fieldName) Then Return 0D
        Return Convert.ToDecimal(row(fieldName))
    End Function

End Class
