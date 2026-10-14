Imports System.Data.SqlClient
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmVatBuy
    Inherits Form

    Private ReadOnly pnlTop As New Panel()
    Private ReadOnly lblTitle As New Label()
    Private ReadOnly lblMonth As New Label()
    Private ReadOnly lblBranch As New Label()
    Private ReadOnly lblViewType As New Label()
    Private ReadOnly dpMonth As New DateTimePicker()
    Private ReadOnly cbBranch As New ComboBox()
    Private ReadOnly cbViewType As New ComboBox()
    Private ReadOnly btnSearch As New Button()
    Private ReadOnly btnPrint As New Button()
    Private ReadOnly btnClose As New Button()
    Private ReadOnly lblSummary As New Label()
    Private ReadOnly lblCount As New Label()
    Private ReadOnly grid As New GridControl()
    Private ReadOnly view As New GridView()
    Private ReadOnly reportTable As DataTable = CreateReportTable()

    Private Const AllBranchesText As String = "ไม่ระบุสาขา"

    Public Sub New()
        InitializeForm()
    End Sub

    Private Shared Function CreateReportTable() As DataTable
        Dim table As New DataTable("ReportBuy")
        table.Columns.Add("DocType", GetType(Integer))
        table.Columns.Add("DocTypeName", GetType(String))
        table.Columns.Add("DateTime", GetType(DateTime))
        table.Columns.Add("BillNumber", GetType(String))
        table.Columns.Add("SupplierName", GetType(String))
        table.Columns.Add("RDBranchName", GetType(String))
        table.Columns.Add("TaxCode", GetType(String))
        table.Columns.Add("PriceBV", GetType(Decimal))
        table.Columns.Add("VAT", GetType(Decimal))
        table.Columns.Add("BranchName", GetType(String))
        Return table
    End Function

    Private Sub InitializeForm()
        Me.Text = "Report Vat Buy (รายงานภาษีซื้อ)"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.WindowState = FormWindowState.Maximized
        Me.Font = New Font("Segoe UI", 10.0!, FontStyle.Regular)
        Me.BackColor = Color.White
        Me.KeyPreview = True

        pnlTop.Dock = DockStyle.Top
        pnlTop.Height = 118
        pnlTop.BackColor = Color.FromArgb(245, 247, 250)
        pnlTop.Padding = New Padding(12)

        lblTitle.AutoSize = False
        lblTitle.Text = "Vat Buy (รายการภาษีซื้อ)"
        lblTitle.Font = New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold)
        lblTitle.Location = New Point(12, 10)
        lblTitle.Size = New Size(290, 34)

        lblMonth.Text = "Month / Year"
        lblMonth.Location = New Point(12, 50)
        lblMonth.Size = New Size(120, 22)

        dpMonth.Format = DateTimePickerFormat.Custom
        dpMonth.CustomFormat = "MMMM / yyyy"
        dpMonth.Value = DateTime.Now
        dpMonth.Location = New Point(12, 74)
        dpMonth.Size = New Size(170, 27)

        lblBranch.Text = "Branch"
        lblBranch.Location = New Point(202, 50)
        lblBranch.Size = New Size(120, 22)

        cbBranch.DropDownStyle = ComboBoxStyle.DropDownList
        cbBranch.Location = New Point(202, 74)
        cbBranch.Size = New Size(260, 29)

        lblViewType.Text = "VAT Type"
        lblViewType.Location = New Point(482, 50)
        lblViewType.Size = New Size(120, 22)

        cbViewType.DropDownStyle = ComboBoxStyle.DropDownList
        cbViewType.Items.AddRange(New Object() {"เฉพาะมี VAT", "เฉพาะไม่มี VAT", "ทั้งหมด"})
        cbViewType.SelectedIndex = 0
        cbViewType.Location = New Point(482, 74)
        cbViewType.Size = New Size(170, 29)

        btnSearch.Text = "ค้นหา"
        btnSearch.Location = New Point(672, 72)
        btnSearch.Size = New Size(110, 32)
        btnSearch.BackColor = Color.FromArgb(37, 99, 235)
        btnSearch.ForeColor = Color.White
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.FlatAppearance.BorderSize = 0
        AddHandler btnSearch.Click, AddressOf btnSearch_Click

        btnPrint.Text = "พิมพ์"
        btnPrint.Location = New Point(792, 72)
        btnPrint.Size = New Size(110, 32)
        btnPrint.BackColor = Color.FromArgb(20, 184, 166)
        btnPrint.ForeColor = Color.White
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.FlatAppearance.BorderSize = 0
        AddHandler btnPrint.Click, AddressOf btnPrint_Click

        btnClose.Text = "ปิด"
        btnClose.Location = New Point(912, 72)
        btnClose.Size = New Size(100, 32)
        btnClose.BackColor = Color.FromArgb(239, 68, 68)
        btnClose.ForeColor = Color.White
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderSize = 0
        AddHandler btnClose.Click, Sub() Me.Close()

        lblSummary.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblSummary.ForeColor = Color.FromArgb(37, 99, 235)
        lblSummary.TextAlign = ContentAlignment.MiddleLeft
        lblSummary.Location = New Point(1032, 50)
        lblSummary.Size = New Size(430, 26)

        lblCount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblCount.Text = "0 รายการ"
        lblCount.TextAlign = ContentAlignment.MiddleRight
        lblCount.Location = New Point(1280, 78)
        lblCount.Size = New Size(180, 26)

        pnlTop.Controls.AddRange(New Control() {
            lblTitle, lblMonth, dpMonth, lblBranch, cbBranch, lblViewType, cbViewType,
            btnSearch, btnPrint, btnClose, lblSummary, lblCount
        })

        grid.Dock = DockStyle.Fill
        grid.MainView = view
        grid.ViewCollection.Add(view)
        grid.DataSource = reportTable

        view.OptionsBehavior.Editable = False
        view.OptionsView.ShowGroupPanel = False
        view.OptionsView.ShowFooter = True
        view.OptionsView.ColumnAutoWidth = False
        view.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always

        Me.Controls.Add(grid)
        Me.Controls.Add(pnlTop)

        AddHandler Me.Load, AddressOf frmVatBuy_Load
        AddHandler Me.KeyDown, AddressOf frmVatBuy_KeyDown
    End Sub

    Private Sub frmVatBuy_Load(sender As Object, e As EventArgs)
        LoadBranches()
        SetLatestAvailableMonth()
        BuildInitialGrid()
    End Sub

    Private Sub frmVatBuy_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub LoadBranches()
        cbBranch.Items.Clear()
        cbBranch.Items.Add(AllBranchesText)

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT BranchNumber, BranchName FROM Branch ORDER BY BranchNumber ASC", conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cbBranch.Items.Add(String.Format("{0} - {1}",
                                                             reader.GetInt32(0),
                                                             reader.GetString(1).Trim()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลสาขาไม่สำเร็จ: " & ex.Message, "Vat Buy", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        cbBranch.SelectedIndex = If(cbBranch.Items.Count > 1, 1, 0)
    End Sub

    Private Sub SetLatestAvailableMonth()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT MAX(MaxDate) " &
                    "FROM (" &
                    "SELECT MAX(Buy.DateTimeTax) AS MaxDate FROM Buy WHERE Buy.Status IS NULL " &
                    "UNION ALL " &
                    "SELECT MAX(CNSupplier.DateTime) AS MaxDate FROM CNSupplier WHERE CNSupplier.Status IS NULL" &
                    ") X"

                Using cmd As New SqlCommand(sql, conn)
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        dpMonth.Value = CDate(value)
                    End If
                End Using
            End Using
        Catch
            dpMonth.Value = DateTime.Now
        End Try
    End Sub

    Private Sub BuildInitialGrid()
        reportTable.Clear()
        ConfigureColumns()
        UpdateSummary()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        SearchVatBuy()
    End Sub

    Private Sub SearchVatBuy()
        Dim firstDay As New DateTime(dpMonth.Value.Year, dpMonth.Value.Month, 1)
        Dim lastDay As DateTime = firstDay.AddMonths(1).AddDays(-1)
        Dim selectedBranch As Integer? = GetSelectedBranchNumber()

        Dim totalBeforeVat As Decimal = 0D
        Dim totalVat As Decimal = 0D

        Me.Cursor = Cursors.WaitCursor
        reportTable.Clear()

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                LoadBuyRows(conn, firstDay, lastDay, selectedBranch, totalBeforeVat, totalVat)
                LoadCreditNoteRows(conn, firstDay, lastDay, selectedBranch, totalBeforeVat, totalVat)
            End Using

            grid.DataSource = reportTable
            ConfigureColumns()
            UpdateSummary(totalBeforeVat, totalVat)

            If reportTable.Rows.Count = 0 Then
                MessageBox.Show("ไม่พบข้อมูลภาษีซื้อในเดือน/สาขา/ประเภท VAT ที่เลือก", "Vat Buy", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("ค้นหารายงานภาษีซื้อไม่สำเร็จ: " & ex.Message, "Vat Buy", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadBuyRows(conn As SqlConnection,
                            firstDay As DateTime,
                            lastDay As DateTime,
                            selectedBranch As Integer?,
                            ByRef totalBeforeVat As Decimal,
                            ByRef totalVat As Decimal)

        Dim sql As String =
            "SELECT Buy.DateTime, RTRIM(Buy.BillNumber) AS BillNumber, " &
            "RTRIM(Supplier.Name) AS SupplierName, RTRIM(Supplier.RDBranchName) AS RDBranchName, " &
            "RTRIM(Supplier.TaxCode) AS TaxCode, Buy.PriceBV, (Buy.TotalPrice - Buy.PriceBV) AS VAT, " &
            "RTRIM(Branch.BranchName) AS BranchName " &
            "FROM Buy " &
            "LEFT OUTER JOIN Supplier ON Buy.SupplierNumber = Supplier.SupplierNumber " &
            "LEFT OUTER JOIN Branch ON Buy.BranchNumber = Branch.BranchNumber " &
            "WHERE Buy.Status IS NULL " &
            "AND Buy.DateTimeTax >= @DateFrom AND Buy.DateTimeTax <= @DateTo "

        sql &= BuildOptionalFilters("Buy", selectedBranch)
        sql &= " ORDER BY Buy.DateTime ASC"

        Using cmd As New SqlCommand(sql, conn)
            AddCommonParameters(cmd, firstDay, lastDay, selectedBranch)
            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim priceBeforeVat As Decimal = ReadDecimal(reader, "PriceBV")
                    Dim vat As Decimal = ReadDecimal(reader, "VAT")

                    Dim row As DataRow = reportTable.NewRow()
                    row("DocType") = 0
                    row("DocTypeName") = "ซื้อ"
                    row("DateTime") = reader("DateTime")
                    row("BillNumber") = ReadString(reader, "BillNumber")
                    row("SupplierName") = ReadString(reader, "SupplierName")
                    row("RDBranchName") = ReadString(reader, "RDBranchName")
                    row("TaxCode") = ReadString(reader, "TaxCode")
                    row("PriceBV") = priceBeforeVat
                    row("VAT") = vat
                    row("BranchName") = ReadString(reader, "BranchName")
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
            "SELECT CNSupplier.DateTime, RTRIM(CNSupplier.DocNumber) AS DocNumber, " &
            "RTRIM(CNSupplier.BillNumber) AS BillNumber, RTRIM(Supplier.Name) AS SupplierName, " &
            "RTRIM(Supplier.RDBranchName) AS RDBranchName, RTRIM(Supplier.TaxCode) AS TaxCode, " &
            "CNSupplier.PriceBV, (CNSupplier.TotalPrice - CNSupplier.PriceBV) AS VAT, " &
            "RTRIM(Branch.BranchName) AS BranchName " &
            "FROM CNSupplier " &
            "LEFT OUTER JOIN Supplier ON CNSupplier.SupplierNumber = Supplier.SupplierNumber " &
            "LEFT OUTER JOIN Branch ON CNSupplier.BranchNumber = Branch.BranchNumber " &
            "WHERE CNSupplier.Status IS NULL " &
            "AND CNSupplier.DateTime >= @DateFrom AND CNSupplier.DateTime <= @DateTo "

        sql &= BuildOptionalFilters("CNSupplier", selectedBranch)
        sql &= " ORDER BY CNSupplier.DateTime ASC"

        Using cmd As New SqlCommand(sql, conn)
            AddCommonParameters(cmd, firstDay, lastDay, selectedBranch)
            Using reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim priceBeforeVat As Decimal = -1D * ReadDecimal(reader, "PriceBV")
                    Dim vat As Decimal = -1D * ReadDecimal(reader, "VAT")
                    Dim billNumber As String = ReadString(reader, "BillNumber")
                    If billNumber = "" Then
                        billNumber = "DocNo " & ReadString(reader, "DocNumber")
                    End If

                    Dim row As DataRow = reportTable.NewRow()
                    row("DocType") = 1
                    row("DocTypeName") = "ลดหนี้"
                    row("DateTime") = reader("DateTime")
                    row("BillNumber") = billNumber
                    row("SupplierName") = ReadString(reader, "SupplierName")
                    row("RDBranchName") = ReadString(reader, "RDBranchName")
                    row("TaxCode") = ReadString(reader, "TaxCode")
                    row("PriceBV") = priceBeforeVat
                    row("VAT") = vat
                    row("BranchName") = ReadString(reader, "BranchName")
                    reportTable.Rows.Add(row)

                    totalBeforeVat += priceBeforeVat
                    totalVat += vat
                End While
            End Using
        End Using
    End Sub

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
        If selectedBranch.HasValue Then
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = selectedBranch.Value
        End If
    End Sub

    Private Function GetSelectedBranchNumber() As Integer?
        If cbBranch.SelectedIndex <= 0 Then Return Nothing

        Dim text As String = cbBranch.Text
        Dim dashIndex As Integer = text.IndexOf(" -", StringComparison.Ordinal)
        If dashIndex <= 0 Then Return Nothing

        Dim branchNumber As Integer
        If Integer.TryParse(text.Substring(0, dashIndex).Trim(), branchNumber) Then
            Return branchNumber
        End If

        Return Nothing
    End Function

    Private Sub ConfigureColumns()
        view.PopulateColumns()

        If view.Columns("DocType") IsNot Nothing Then view.Columns("DocType").Visible = False

        SetCaption("DocTypeName", "ประเภท", 90, HorzAlignment.Center)
        SetCaption("DateTime", "วันที่", 115, HorzAlignment.Center, "dd/MM/yyyy")
        SetCaption("BillNumber", "เลขที่", 130, HorzAlignment.Near)
        SetCaption("SupplierName", "ชื่อผู้ขาย (Supplier Name)", 260, HorzAlignment.Near)
        SetCaption("RDBranchName", "ชื่อสาขาตามกรมสรรพากร", 190, HorzAlignment.Near)
        SetCaption("TaxCode", "เลขประจำตัวผู้เสียภาษี", 170, HorzAlignment.Near)
        SetCaption("PriceBV", "จำนวนเงินก่อน VAT", 150, HorzAlignment.Far, "N2", True)
        SetCaption("VAT", "VAT", 130, HorzAlignment.Far, "N2", True)
        SetCaption("BranchName", "สาขา", 160, HorzAlignment.Near)

        view.BestFitColumns()
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
            col.DisplayFormat.FormatType = FormatType.Numeric
            If fieldName = "DateTime" Then col.DisplayFormat.FormatType = FormatType.DateTime
            col.DisplayFormat.FormatString = formatString
        End If

        If showSummary Then
            col.Summary.Clear()
            col.Summary.Add(SummaryItemType.Sum, fieldName, "{0:N2}")
        End If
    End Sub

    Private Sub UpdateSummary(Optional totalBeforeVat As Decimal = 0D, Optional totalVat As Decimal = 0D)
        lblSummary.Text = String.Format("Sum Money Before VAT = {0:N2}   |   Sum VAT = {1:N2}", totalBeforeVat, totalVat)
        lblCount.Text = reportTable.Rows.Count.ToString("N0") & " รายการ"
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        If reportTable.Rows.Count = 0 Then
            MessageBox.Show("ไม่สามารถพิมพ์ได้ เนื่องจากยังไม่มีรายการใน List", "Vat Buy", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        grid.ShowPrintPreview()
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
End Class
