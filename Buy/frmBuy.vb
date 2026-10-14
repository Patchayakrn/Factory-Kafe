Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class frmBuy
    Inherits Form

    Private Const BranchNumber As Integer = 1
    Private Const BuyBillType As Integer = 0

    Private ReadOnly dtItems As New DataTable("BuyD")
    Private ReadOnly decCulture As CultureInfo = CultureInfo.InvariantCulture
    Private printHeader As DataTable
    Private printDetails As DataTable
    Private supplierNumber As Integer
    Private currentBuyNumber As Integer
    Private isSaved As Boolean
    Private isEditMode As Boolean
    Private increaseOnlyAfterSale As Boolean
    Private ReadOnly originalBuyQuantities As New Dictionary(Of Integer, Decimal)()
    Private convertedAdjustmentCount As Integer
    Private selectedProductPN As Long
    Private selectedProductCode As String = ""
    Private selectedProductBarcode As String = ""
    Private selectedProductName As String = ""
    Private selectedProductUnit As String = ""
    Private selectedProductPrice As Decimal
    Private selectedProductAllowDecimal As Boolean
    Private _busyAutoInput As Boolean
    Private ReadOnly pnlItemQuantityTotal As New Panel()
    Private ReadOnly lblItemQuantityTotalCaption As New Label()
    Private ReadOnly lblItemQuantityTotalValue As New Label()
    Dim OfficerID As Integer
    Dim OfficerName As String

    Public Sub New()
        Me.New(False)
    End Sub

    Private Sub New(reportOnly As Boolean)
        If reportOnly Then Return
        InitializeComponent()
        ConfigureItemTable()
    End Sub

    Private Sub frmBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LayoutBuyForm()
        lblBranch.Text = "สาขา 1 (สำนักงานใหญ่) "
        'เปิดให้ดูรายงานย้อนหลังได้ทันที แม้ยังไม่ได้สร้างใบซื้อใหม่
        btnPrint.Enabled = True
        dtpTaxDate.Format = DateTimePickerFormat.Custom
        dtpTaxDate.CustomFormat = "MM/yyyy"
        dtpTaxDate.ShowUpDown = True
        If Not isEditMode Then
            lbDocNumber.Text = "กำลังสร้าง..."
            dtpDate.Value = Date.Today
            txtVat.Text = "0.00"
            cbHaveTax.SelectedIndex = 1
            cbVatType.SelectedIndex = 1
            cboCostType.SelectedIndex = 0
        End If
        btnPrint.Enabled = True

        If Not String.IsNullOrEmpty(ModSession.LoginUserName) Then
            Me.Text = "Buy-ซื้อสินค้า " & " Welcome  " & ModSession.LoginName
            OfficerID = ModSession.LoginOfficerID
            OfficerName = ModSession.LoginName
        End If
    End Sub

    Private Sub frmBuy_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        LayoutBuyForm()
    End Sub

    Private Sub LayoutBuyForm()
        If IsDisposed OrElse ClientSize.Width <= 0 OrElse ClientSize.Height <= 0 Then Return

        SuspendLayout()
        Try
            Dim margin As Integer = 12
            Dim titleHeight As Integer = If(Panel1 Is Nothing, 0, Panel1.Height)
            Dim headerTop As Integer = titleHeight + 6
            'grpProduct is a child of grpItems, so the item panel starts directly below the header.
            Dim bodyTop As Integer = headerTop + grpHeader.Height
            Dim footerHeight As Integer = 48
            Dim summaryWidth As Integer = Math.Max(330, CInt(ClientSize.Width * 0.28R))
            Dim bodyHeight As Integer = Math.Max(170, ClientSize.Height - bodyTop - footerHeight - margin)
            Dim summaryLeft As Integer = Math.Max(margin + 460, ClientSize.Width - summaryWidth - margin)
            Dim itemWidth As Integer = Math.Max(460, summaryLeft - margin - 8)

            grpHeader.SetBounds(margin, headerTop, Math.Max(700, ClientSize.Width - margin * 2), grpHeader.Height)
            'Keep the comment box inside the header when the form is narrower than the designer size.
            txtComment.Width = Math.Max(300, grpHeader.ClientSize.Width - txtComment.Left - 12)
            grpItems.SetBounds(margin, bodyTop, itemWidth, bodyHeight)
            grpSummary.SetBounds(summaryLeft, bodyTop, Math.Max(300, ClientSize.Width - summaryLeft - margin), bodyHeight)

            'Layout the add-product panel inside grpItems and put the grid directly underneath it.
            grpProduct.SetBounds(0, 0, Math.Max(700, grpItems.ClientSize.Width), grpProduct.Height)
            dgvItems.Dock = DockStyle.None
            Dim itemTotalHeight As Integer = 42
            pnlItemQuantityTotal.SetBounds(3,
                                           Math.Max(grpProduct.Bottom + 2, grpItems.ClientSize.Height - itemTotalHeight - 4),
                                           Math.Max(300, grpItems.ClientSize.Width - 6),
                                           itemTotalHeight)
            dgvItems.SetBounds(3, grpProduct.Bottom + 2,
                               Math.Max(300, grpItems.ClientSize.Width - 6),
                               Math.Max(80, pnlItemQuantityTotal.Top - grpProduct.Bottom - 4))

            LayoutBuyHeader()

            'The buttons belong to Panel2, so their coordinates must be relative to Panel2.
            'Using the form coordinates here makes them fall outside the footer when the form is resized.
            Dim footerTop As Integer = Math.Max(0, (Panel2.ClientSize.Height - btnCancel.Height) \ 2)
            'Keep the footer buttons near the visible left side.  frmBuy is wider than
            'some user screens, so placing them at the far right makes them disappear
            'outside the visible part of the form.
            btnPrint.SetBounds(margin, footerTop, btnPrint.Width, btnPrint.Height)
            btnSave.SetBounds(btnPrint.Right + 8, footerTop, btnSave.Width, btnSave.Height)
            btnCancel.SetBounds(btnSave.Right + 8, footerTop, btnCancel.Width, btnCancel.Height)
        Finally
            ResumeLayout(True)
        End Try
    End Sub

    Private Sub LayoutBuyHeader()
        If Panel1 Is Nothing OrElse Panel1.ClientSize.Width <= 0 Then Return

        Dim width As Integer = Panel1.ClientSize.Width
        Label8.Left = 12
        Dim docValueLeft As Integer = Math.Max(150, width - 150)
        Dim docLabelLeft As Integer = Math.Max(260, docValueLeft - 98)

        lblBranch.Left = Math.Max(300, docLabelLeft - 320)
        Label1.Left = docLabelLeft
        lblBranch.Width = Math.Max(240, Label1.Left - lblBranch.Left - 10)
        lbDocNumber.Left = docValueLeft
        lbDocNumber.Width = Math.Max(120, width - lbDocNumber.Left - 12)
    End Sub

    Private Sub ConfigureItemTable()
        dtItems.Columns.Add("Type", GetType(Integer))
        dtItems.Columns.Add("Item", GetType(Integer))
        dtItems.Columns.Add("SItem", GetType(Integer))
        dtItems.Columns.Add("PN", GetType(Integer))
        dtItems.Columns.Add("ProductCode", GetType(String))
        dtItems.Columns.Add("Barcode", GetType(String))
        dtItems.Columns.Add("ProductName", GetType(String))
        dtItems.Columns.Add("Number", GetType(Decimal))
        dtItems.Columns.Add("AllowDecimal", GetType(Boolean))
        dtItems.Columns.Add("LUnit", GetType(String))
        dtItems.Columns.Add("PriceBD", GetType(Decimal))
        dtItems.Columns.Add("DiscountCash", GetType(Decimal))
        dtItems.Columns.Add("DiscountP", GetType(Decimal))
        dtItems.Columns.Add("DiscountPString", GetType(String))
        dtItems.Columns.Add("Price", GetType(Decimal))
        dtItems.Columns.Add("SumPrice", GetType(Decimal))

        dgvItems.Columns.Clear()
        AddItemColumn("colItem", "ลำดับ", "Item", 58, DataGridViewContentAlignment.MiddleCenter)
        AddItemColumn("colPN", "ID", "PN", 58, DataGridViewContentAlignment.MiddleCenter)
        AddItemColumn("colProductCode", "รหัสสินค้า", "ProductCode", 125, DataGridViewContentAlignment.MiddleLeft)
        AddItemColumn("colBarcode", "Barcode", "Barcode", 135, DataGridViewContentAlignment.MiddleLeft)
        'Keep this column at a fixed width.  A Fill column was being squeezed between
        'Barcode and จำนวนซื้อ, which made its two borders look like a double line.
        AddItemColumn("colProductName", "ชื่อสินค้า", "ProductName", 260, DataGridViewContentAlignment.MiddleLeft)
        AddItemColumn("colNumber", "จำนวนซื้อ", "Number", 95, DataGridViewContentAlignment.MiddleRight, False, "N2")
        AddItemColumn("colLUnit", "หน่วย", "LUnit", 75, DataGridViewContentAlignment.MiddleCenter)
        AddItemColumn("colPriceBD", "ราคาก่อนลด", "PriceBD", 115, DataGridViewContentAlignment.MiddleRight, False, "N2")
        AddItemColumn("colDiscountP", "ส่วนลด %", "DiscountPString", 90, DataGridViewContentAlignment.MiddleRight)
        AddItemColumn("colPrice", "ราคาหลังลด", "Price", 115, DataGridViewContentAlignment.MiddleRight, False, "N2")
        AddItemColumn("colSumPrice", "จำนวนเงิน", "SumPrice", 120, DataGridViewContentAlignment.MiddleRight, False, "N2")
        AddItemCheckColumn("colAllowDecimal", "จำนวนเงินเป็นทศนิยมได้", "AllowDecimal", 150)

        ConfigureItemQuantityTotal()
        AddHandler dtItems.RowChanged, AddressOf dtItems_Changed
        AddHandler dtItems.RowDeleted, AddressOf dtItems_Changed
        AddHandler dgvItems.CellValidating, AddressOf dgvItems_CellValidating
        AddHandler dgvItems.CellEndEdit, AddressOf dgvItems_CellEndEdit

        dgvItems.EnableHeadersVisualStyles = False
        dgvItems.ColumnHeadersHeight = 38
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(218, 235, 249)
        dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dgvItems.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)
        dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvItems.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvItems.ColumnHeadersDefaultCellStyle.BackColor
        dgvItems.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvItems.ColumnHeadersDefaultCellStyle.ForeColor
        dgvItems.CellBorderStyle = DataGridViewCellBorderStyle.Single
        dgvItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        dgvItems.GridColor = Color.FromArgb(170, 195, 215)
        dgvItems.DefaultCellStyle.Font = New Font("Cordia New", 13.8!, FontStyle.Regular)
        dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 242, 252)
        dgvItems.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvItems.RowTemplate.Height = 34
        dgvItems.AutoGenerateColumns = False
        dgvItems.DataSource = dtItems
        UpdateItemQuantityTotal()
    End Sub

    Private Sub ConfigureItemQuantityTotal()
        pnlItemQuantityTotal.Name = "pnlItemQuantityTotal"
        pnlItemQuantityTotal.BackColor = Color.FromArgb(238, 246, 253)
        pnlItemQuantityTotal.BorderStyle = BorderStyle.FixedSingle

        lblItemQuantityTotalCaption.AutoSize = False
        lblItemQuantityTotalCaption.Text = "รวมจำนวนซื้อ"
        lblItemQuantityTotalCaption.TextAlign = ContentAlignment.MiddleRight
        lblItemQuantityTotalCaption.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)
        lblItemQuantityTotalCaption.ForeColor = Color.Black

        lblItemQuantityTotalValue.AutoSize = False
        lblItemQuantityTotalValue.Text = "0.00"
        lblItemQuantityTotalValue.TextAlign = ContentAlignment.MiddleRight
        lblItemQuantityTotalValue.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)
        lblItemQuantityTotalValue.ForeColor = Color.DarkBlue

        pnlItemQuantityTotal.Controls.Add(lblItemQuantityTotalCaption)
        pnlItemQuantityTotal.Controls.Add(lblItemQuantityTotalValue)
        grpItems.Controls.Add(pnlItemQuantityTotal)
        LayoutItemQuantityTotal()
    End Sub

    Private Sub LayoutItemQuantityTotal()
        If pnlItemQuantityTotal Is Nothing OrElse pnlItemQuantityTotal.ClientSize.Width <= 0 Then Return

        'จัดตำแหน่งให้ค่ารวมอยู่ใต้คอลัมน์ "จำนวนซื้อ" ของ Grid
        Dim quantityColumnLeft As Integer = 58 + 58 + 125 + 135 + 260
        lblItemQuantityTotalCaption.SetBounds(Math.Max(0, quantityColumnLeft - 205), 0, 200, pnlItemQuantityTotal.ClientSize.Height)
        lblItemQuantityTotalValue.SetBounds(quantityColumnLeft, 0, 95, pnlItemQuantityTotal.ClientSize.Height)
    End Sub

    Private Sub dtItems_Changed(sender As Object, e As DataRowChangeEventArgs)
        UpdateItemQuantityTotal()
    End Sub

    Private Sub dgvItems_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
        If Not increaseOnlyAfterSale OrElse e.RowIndex < 0 OrElse dgvItems.Columns(e.ColumnIndex).Name <> "colNumber" Then Return

        Dim view As DataRowView = TryCast(dgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If view Is Nothing Then Return

        Dim quantity As Decimal
        If Not Decimal.TryParse(Convert.ToString(e.FormattedValue), NumberStyles.Any, decCulture, quantity) AndAlso
           Not Decimal.TryParse(Convert.ToString(e.FormattedValue), NumberStyles.Any, CultureInfo.CurrentCulture, quantity) Then
            e.Cancel = True
            MessageBox.Show("กรุณาระบุจำนวนซื้อให้ถูกต้อง", "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim pn As Integer = Convert.ToInt32(view.Row("PN"), decCulture)
        Dim originalQuantity As Decimal = If(originalBuyQuantities.ContainsKey(pn), originalBuyQuantities(pn), 0D)
        If quantity < originalQuantity Then
            e.Cancel = True
            MessageBox.Show("โหมดนี้อนุญาตให้เพิ่มจำนวนซื้อเท่านั้น ห้ามลดจำนวนจากที่บันทึกเดิม", "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub dgvItems_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If Not increaseOnlyAfterSale OrElse e.RowIndex < 0 OrElse dgvItems.Columns(e.ColumnIndex).Name <> "colNumber" Then Return
        Dim view As DataRowView = TryCast(dgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If view Is Nothing Then Return

        Dim quantity As Decimal = DbDecimal(view.Row("Number"))
        Dim unitPrice As Decimal = DbDecimal(view.Row("Price"))
        view.Row("SumPrice") = Math.Round(quantity * unitPrice, 2, MidpointRounding.AwayFromZero)
        UpdateItemQuantityTotal()
    End Sub

    Private Sub UpdateItemQuantityTotal()
        If lblItemQuantityTotalValue Is Nothing Then Return

        Dim totalQuantity As Decimal = 0D
        For Each row As DataRow In dtItems.Rows
            If row.RowState <> DataRowState.Deleted Then
                totalQuantity += DbDecimal(row("Number"))
            End If
        Next

        lblItemQuantityTotalValue.Text = totalQuantity.ToString("N2", decCulture)
        LayoutItemQuantityTotal()
    End Sub

    Public Function LoadExistingBuy(buyNumber As Integer, branchNumber As Integer) As Boolean
        If Not CanCurrentUserEditBuy() Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์แก้ไขรายการใบซื้อ", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If buyNumber <= 0 OrElse branchNumber <> branchNumber Then Return False

        Try
            increaseOnlyAfterSale = False
            convertedAdjustmentCount = 0
            originalBuyQuantities.Clear()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const headerSql As String =
                    "SELECT TOP 1 Buy.SupplierNumber, ISNULL(RTRIM(Supplier.Name),'') AS SupplierName, " &
                    "Buy.BillNumber, Buy.Credit, Buy.VatType, Buy.DateTime, Buy.DateTimeTax, Buy.Comment, " &
                    "Buy.Money, Buy.DCPercent, Buy.DCCash, Buy.PercentVat, Buy.PriceBV, Buy.TotalPrice, Buy.HiddenCostType " &
                    "FROM Buy LEFT JOIN Supplier ON Buy.SupplierNumber = Supplier.SupplierNumber " &
                    "WHERE Buy.BranchNumber=@BranchNumber AND Buy.BuyNumber=@BuyNumber AND Buy.Status IS NULL"
                Using cmd As New SqlCommand(headerSql, conn)
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If Not reader.Read() Then Return False

                        isEditMode = True
                        currentBuyNumber = buyNumber
                        supplierNumber = If(IsDBNull(reader("SupplierNumber")), 0, Convert.ToInt32(reader("SupplierNumber"), decCulture))
                        txtSupplierName.Text = Convert.ToString(reader("SupplierName")).Trim()
                        txtVendorBillNumber.Text = Convert.ToString(reader("BillNumber")).Trim()
                        txtCredit.Text = DbDecimal(reader("Credit")).ToString("N2", decCulture)
                        dtpDate.Value = SafeDateValue(reader("DateTime"), Date.Today)
                        dtpTaxDate.Value = SafeDateValue(reader("DateTimeTax"), dtpDate.Value)
                        txtComment.Text = Convert.ToString(reader("Comment")).Trim()
                        cbVatType.SelectedIndex = Math.Max(0, Math.Min(cbVatType.Items.Count - 1, DbInt(reader("VatType"))))
                        cbHaveTax.SelectedIndex = If(DbDecimal(reader("PercentVat")) > 0D, 1, 0)
                        cboCostType.SelectedIndex = Math.Max(0, Math.Min(cboCostType.Items.Count - 1, DbInt(reader("HiddenCostType"))))
                        KryptonTextBox1.Text = DbDecimal(reader("DCPercent")).ToString("N2", decCulture)
                        KryptonTextBox6.Text = DbDecimal(reader("DCCash")).ToString("N2", decCulture)
                        KryptonTextBox2.Text = DbDecimal(reader("PercentVat")).ToString("N2", decCulture)
                        KryptonTextBox7.Text = DbDecimal(reader("PriceBV")).ToString("N2", decCulture)
                        KryptonTextBox8.Text = DbDecimal(reader("TotalPrice")).ToString("N2", decCulture)
                        txtTotalMoney.Text = DbDecimal(reader("TotalPrice")).ToString("N2", decCulture)
                    End Using
                End Using

                increaseOnlyAfterSale = Not CanEditExistingBuy(conn, branchNumber, buyNumber)

                dtItems.Clear()
                Const detailSql As String =
                    "SELECT BuyD.Type, BuyD.Item, BuyD.SItem, BuyD.PN, " &
                    "ISNULL(RTRIM(Product.ProductCode),'') AS ProductCode, ISNULL(RTRIM(Product.Barcode),'') AS Barcode, " &
                    "ISNULL(RTRIM(Product.Name),'') AS ProductName, BuyD.Number, ISNULL(Product.AllowDecimal,0) AS AllowDecimal, " &
                    "ISNULL(RTRIM(BuyD.LUnit),'') AS LUnit, BuyD.PriceBD, BuyD.DiscountCash, BuyD.DiscountP, " &
                    "ISNULL(RTRIM(BuyD.DiscountPString),'0') AS DiscountPString, BuyD.Price, BuyD.SumPrice " &
                    "FROM BuyD LEFT JOIN Product ON BuyD.PN=Product.PN " &
                    "WHERE BuyD.BranchNumber=@BranchNumber AND BuyD.BuyNumber=@BuyNumber ORDER BY BuyD.Item"
                Using cmd As New SqlCommand(detailSql, conn)
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim row As DataRow = dtItems.NewRow()
                            row("Type") = DbInt(reader("Type"))
                            row("Item") = DbInt(reader("Item"))
                            row("SItem") = DbInt(reader("SItem"))
                            row("PN") = DbInt(reader("PN"))
                            row("ProductCode") = Convert.ToString(reader("ProductCode")).Trim()
                            row("Barcode") = Convert.ToString(reader("Barcode")).Trim()
                            row("ProductName") = Convert.ToString(reader("ProductName")).Trim()
                            row("Number") = DbDecimal(reader("Number"))
                            row("AllowDecimal") = DbInt(reader("AllowDecimal")) <> 0
                            row("LUnit") = Convert.ToString(reader("LUnit")).Trim()
                            row("PriceBD") = DbDecimal(reader("PriceBD"))
                            row("DiscountCash") = DbDecimal(reader("DiscountCash"))
                            row("DiscountP") = DbDecimal(reader("DiscountP"))
                            row("DiscountPString") = Convert.ToString(reader("DiscountPString")).Trim()
                            row("Price") = DbDecimal(reader("Price"))
                            row("SumPrice") = DbDecimal(reader("SumPrice"))
                            dtItems.Rows.Add(row)
                            originalBuyQuantities(DbInt(reader("PN"))) = DbDecimal(reader("Number"))
                        End While
                    End Using
                End Using
            End Using

            If increaseOnlyAfterSale Then
                ApplyIncreaseOnlyMode()
                MessageBox.Show("ใบซื้อนี้มีรายการถูกขายหรือตัดสต๊อกแล้ว" & Environment.NewLine &
                                "ระบบเปิดให้เพิ่มจำนวนรายการเดิมหรือเพิ่มสินค้าใหม่ได้" & Environment.NewLine &
                                "ห้ามลดจำนวน ลบสินค้า เปลี่ยนราคา หรือวันที่ซื้อ",
                                "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            lbDocNumber.Text = currentBuyNumber.ToString(decCulture)
            isSaved = True
            btnSave.Enabled = True
            btnPrint.Enabled = True
            Return True
        Catch ex As Exception
            isEditMode = False
            currentBuyNumber = 0
            ShowError("โหลดใบซื้อเพื่อแก้ไขไม่สำเร็จ", ex)
            Return False
        End Try
    End Function

    Private Sub ApplyIncreaseOnlyMode()
        grpHeader.Enabled = False
        'อนุญาตให้เพิ่มสินค้าใหม่ในใบเดิมได้ เช่น ย้ายรายการจากใบซื้อที่กรอกซ้ำ
        'แต่ยังล็อกข้อมูลหัวใบและไม่อนุญาตให้ลบรายการเดิมที่อาจถูกขายไปแล้ว
        grpProduct.Enabled = True
        btnAddItem.Enabled = True
        btnRemoveItem.Enabled = False
        dgvItems.Enabled = True
        For Each column As DataGridViewColumn In dgvItems.Columns
            column.ReadOnly = True
        Next
        If dgvItems.Columns.Contains("colNumber") Then
            dgvItems.Columns("colNumber").ReadOnly = False
            dgvItems.Columns("colNumber").DefaultCellStyle.BackColor = Color.LightYellow
        End If
        btnSave.Enabled = True
        btnSave.BringToFront()
    End Sub

    Private Function CanEditExistingBuy(conn As SqlConnection, branchNumber As Integer, buyNumber As Integer) As Boolean
        Const sql As String =
            "SELECT COUNT(1) FROM BuyStock bs " &
            "LEFT JOIN Stock s ON s.BranchNumber=bs.BranchNumber AND s.BillType=@BillType " &
            "AND s.BillNumber=bs.BuyNumber AND s.PN=bs.PN " &
            "WHERE bs.BranchNumber=@BranchNumber AND bs.BuyNumber=@BuyNumber " &
            "AND (s.PN IS NULL OR ISNULL(s.Number,0) < ISNULL(bs.Number,0))"
        Using cmd As New SqlCommand(sql, conn)
            cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
            Return Convert.ToInt32(cmd.ExecuteScalar(), decCulture) = 0
        End Using
    End Function

    Private Function CanCurrentUserEditBuy() As Boolean
        Return PermissionService.HasPermission("Buy.Edit")
    End Function

    Private Function DbInt(value As Object) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0
        Return Convert.ToInt32(value, decCulture)
    End Function

    Private Function DbDecimal(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
        Return Convert.ToDecimal(value, decCulture)
    End Function

    Private Function SafeDateValue(value As Object, fallback As DateTime) As DateTime
        If value Is Nothing OrElse value Is DBNull.Value Then Return fallback
        Return Convert.ToDateTime(value, decCulture)
    End Function

    Private Sub AddItemColumn(name As String, headerText As String, propertyName As String, width As Integer,
                              alignment As DataGridViewContentAlignment, Optional fill As Boolean = False,
                              Optional format As String = "")
        Dim column As New DataGridViewTextBoxColumn With {
            .Name = name,
            .HeaderText = headerText,
            .DataPropertyName = propertyName,
            .Width = width,
            .ReadOnly = True,
            .SortMode = DataGridViewColumnSortMode.NotSortable
        }
        column.DefaultCellStyle.Alignment = alignment
        If Not String.IsNullOrWhiteSpace(format) Then column.DefaultCellStyle.Format = format
        If fill Then
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            column.MinimumWidth = width
        End If
        dgvItems.Columns.Add(column)
    End Sub

    Private Sub AddItemCheckColumn(name As String, headerText As String, propertyName As String, width As Integer)
        Dim column As New DataGridViewCheckBoxColumn With {
            .Name = name,
            .HeaderText = headerText,
            .DataPropertyName = propertyName,
            .Width = width,
            .ReadOnly = True,
            .SortMode = DataGridViewColumnSortMode.NotSortable,
            .ThreeState = False
        }
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvItems.Columns.Add(column)
    End Sub

    Private Function ToDecimalValue(value As String, defaultValue As Decimal) As Decimal
        Dim result As Decimal
        If Decimal.TryParse(value, NumberStyles.Number, decCulture, result) Then Return result
        If Decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, result) Then Return result
        Return defaultValue
    End Function

    Private Function SqlValue(value As Object) As Object
        If value Is Nothing OrElse value Is DBNull.Value Then Return DBNull.Value
        If TypeOf value Is String AndAlso String.IsNullOrWhiteSpace(CStr(value)) Then Return DBNull.Value
        Return value
    End Function

    'Private Sub RecalculateTotals()
    '    Dim subtotal As Decimal = 0D
    '    For Each row As DataRow In dtItems.Rows
    '        subtotal += Convert.ToDecimal(row("SumPrice"), decCulture)
    '    Next

    '    Dim percent As Decimal = Math.Max(0D, ToDecimalValue(txtDiscountPercent.Text, 0D))
    '    Dim cashDiscount As Decimal = Math.Max(0D, ToDecimalValue(txtDiscountCash.Text, 0D))
    '    Dim vatPercent As Decimal = Math.Max(0D, ToDecimalValue(txtVat.Text, 0D))
    '    Dim discountPercentMoney As Decimal = Math.Round(subtotal * percent / 100D, 2, MidpointRounding.AwayFromZero)
    '    Dim afterDiscount As Decimal = Math.Max(0D, subtotal - discountPercentMoney - cashDiscount)
    '    Dim net As Decimal
    '    Dim vat As Decimal

    '    If cboVatType.SelectedIndex = 0 Then
    '        vat = Math.Round(afterDiscount * vatPercent / 100D, 2, MidpointRounding.AwayFromZero)
    '        net = afterDiscount + vat
    '    Else
    '        net = afterDiscount
    '        vat = Math.Round(net - (net / (1D + vatPercent / 100D)), 2, MidpointRounding.AwayFromZero)
    '        afterDiscount = net - vat
    '    End If

    '    lblSubtotalValue.Text = subtotal.ToString("N2")
    '    lblDiscountValue.Text = discountPercentMoney.ToString("N2")
    '    lblBeforeVatValue.Text = afterDiscount.ToString("N2")
    '    lblVatValue.Text = vat.ToString("N2")
    '    lblTotalValue.Text = net.ToString("N2")
    'End Sub

    Private Sub txtTotals_TextChanged(sender As Object, e As EventArgs)
        ' RecalculateTotals()
    End Sub

    Private Sub cboVatType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' RecalculateTotals()
    End Sub

    Private Sub btnLoadSupplier_Click(sender As Object, e As EventArgs) Handles btnLoadSupplier.Click
        Using supplierForm As New fsupplier()
            If supplierForm.ShowDialog(Me) = DialogResult.OK Then
                supplierNumber = supplierForm.SelectedSupplierNumber
                txtSupplierName.Text = supplierForm.SelectedSupplierName
                txtCredit.Text = supplierForm.SelectedCredit.ToString()
            End If
        End Using
    End Sub


    Private Sub btnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
        If dgvItems.CurrentRow Is Nothing Then Return
        Dim view As DataRowView = TryCast(dgvItems.CurrentRow.DataBoundItem, DataRowView)
        If view IsNot Nothing Then
            view.Row.Delete()
            dtItems.AcceptChanges()
        End If
        RenumberItems()
        btnPrint.Enabled = True
    End Sub

    Private Sub btnFindProduct_Click(sender As Object, e As EventArgs) Handles btnFindProduct.Click
        OpenProductSelector()
    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        If selectedProductPN <= 0 Then
            If Not TryLoadProductFromText() Then OpenProductSelector()
            If selectedProductPN <= 0 Then Return
        End If

        Dim quantity As Decimal
        If Not Decimal.TryParse(txtQuantity.Text.Trim(), NumberStyles.Number, decCulture, quantity) AndAlso
           Not Decimal.TryParse(txtQuantity.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, quantity) Then
            MessageBox.Show("กรุณาระบุจำนวนสินค้าให้ถูกต้อง", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQuantity.Focus()
            txtQuantity.SelectAll()
            Return
        End If

        If quantity <= 0D Then
            MessageBox.Show("จำนวนสินค้าต้องมากกว่า 0", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQuantity.Focus()
            txtQuantity.SelectAll()
            Return
        End If

        If Not selectedProductAllowDecimal AndAlso quantity <> Decimal.Truncate(quantity) Then
            MessageBox.Show("สินค้านี้ไม่อนุญาตให้ใส่จำนวนเป็นทศนิยม", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQuantity.Focus()
            txtQuantity.SelectAll()
            Return
        End If

        Dim existing As DataRow = Nothing
        For Each row As DataRow In dtItems.Rows
            'BuyStock กำหนดคีย์เป็น BranchNumber + BuyNumber + PN
            'จึงต้องรวมสินค้า PN เดียวกันให้เหลือหนึ่งรายการ แม้หน่วยจะต่างกันเล็กน้อย
            If Convert.ToInt64(row("PN"), decCulture) = selectedProductPN Then
                existing = row
                Exit For
            End If
        Next

        If existing Is Nothing Then
            existing = dtItems.NewRow()
            existing("Type") = 0
            existing("Item") = GetNextItemNumber()
            existing("SItem") = 0
            existing("PN") = CInt(selectedProductPN)
            existing("ProductCode") = selectedProductCode
            existing("Barcode") = selectedProductBarcode
            existing("ProductName") = selectedProductName
            existing("Number") = quantity
            existing("AllowDecimal") = selectedProductAllowDecimal
            existing("LUnit") = selectedProductUnit
            existing("PriceBD") = selectedProductPrice
            existing("DiscountCash") = 0D
            existing("DiscountPString") = "0"
            existing("DiscountP") = 0D
            existing("Price") = selectedProductPrice
            existing("SumPrice") = Math.Round(quantity * selectedProductPrice, 2, MidpointRounding.AwayFromZero)
            dtItems.Rows.Add(existing)
            btnPrint.Enabled = True
        Else
            Dim totalQuantity As Decimal = Convert.ToDecimal(existing("Number"), decCulture) + quantity
            existing("Number") = totalQuantity
            existing("SumPrice") = Math.Round(totalQuantity * Convert.ToDecimal(existing("Price"), decCulture), 2, MidpointRounding.AwayFromZero)
        End If

        dgvItems.Refresh()
        ClearSelectedProduct()
    End Sub

    Private Sub txtProductKey_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProductKey.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If _busyAutoInput Then Exit Sub
        _busyAutoInput = True

        Try
            Dim kw As String = txtProductKey.Text.Trim()
            If kw = "" Then Exit Sub

            OpenProductByName(kw)

            txtProductKey.Clear()
            txtProductKey.Focus()
        Finally
            _busyAutoInput = False
        End Try
    End Sub

    Private Sub OpenProductByName(keyword As String)
        If String.IsNullOrWhiteSpace(keyword) Then Return

        txtProductKey.Text = keyword.Trim()
        OpenProductSelector()

        If selectedProductPN > 0 Then
            btnAddItem.PerformClick()
        End If
    End Sub

    Private Sub OpenProductSelector()
        Dim keyword As String = txtProductKey.Text.Trim()

        Using productForm As New fProduct()
            productForm.IsSelectMode = True
            productForm.AllowOutOfStockSelection = True
            'แสดงสินค้าทั้งหมด ทั้งรายการติดลบและรายการที่มี Stock ปกติ
            productForm.OnlyNegativeStockSelection = False
            productForm.SearchText = keyword

            'อย่าผูก Dialog กับ MDI Child/MDI Parent เพราะทำให้บางเครื่องวาดหน้าต่างไม่ครบ
            If productForm.ShowDialog() = DialogResult.OK Then
                If productForm.SelectedProducts.Count > 0 Then
                    For Each product In productForm.SelectedProducts
                        AddProductToBuyItems(product.PN, product.ProductCode, product.Barcode,
                                             product.ProductName, product.Unit, product.SellPrice,
                                             product.AllowDecimal <> 0, PurchaseQuantityFromStock(product.Stock))
                    Next
                    RenumberItems()
                    dtItems.DefaultView.Sort = "ProductCode ASC, Item ASC"
                    dgvItems.Refresh()
                    ClearSelectedProduct()
                    Return
                End If

                selectedProductPN = productForm.SelectedPN
                selectedProductCode = productForm.SelectedProductCode
                selectedProductBarcode = productForm.SelectedBarcode
                selectedProductName = productForm.SelectedProductName
                selectedProductUnit = productForm.SelectedUnit
                selectedProductPrice = productForm.SelectedSellPrice
                selectedProductAllowDecimal = productForm.SelectedAllowDecimal <> 0
                txtProductKey.Text = productForm.SelectedProductName
                txtProductCode.Text = productForm.SelectedProductCode
                txtQuantity.Text = "1"
                txtProductKey.Tag = productForm.SelectedPN
                txtQuantity.Focus()
                txtQuantity.SelectAll()
            End If
        End Using
    End Sub

    Private Function PurchaseQuantityFromStock(stock As Decimal) As Decimal
        Dim quantity As Decimal = Math.Abs(stock)
        If quantity <= 0D Then Return 1D
        Return quantity
    End Function

    Private Sub AddProductToBuyItems(pn As Long, productCode As String, barcode As String,
                                     productName As String, unit As String, price As Decimal,
                                     allowDecimal As Boolean, quantity As Decimal)
        Dim existing As DataRow = Nothing
        For Each row As DataRow In dtItems.Rows
            'BuyStock เก็บได้หนึ่งรายการต่อ PN ต่อใบซื้อ จึงต้องรวม PN ซ้ำเข้าด้วยกัน
            If Convert.ToInt64(row("PN"), decCulture) = pn Then
                existing = row
                Exit For
            End If
        Next

        If existing Is Nothing Then
            existing = dtItems.NewRow()
            existing("Type") = 0
            existing("Item") = GetNextItemNumber()
            existing("SItem") = 0
            existing("PN") = CInt(pn)
            existing("ProductCode") = productCode
            existing("Barcode") = barcode
            existing("ProductName") = productName
            existing("Number") = quantity
            existing("AllowDecimal") = allowDecimal
            existing("LUnit") = unit
            existing("PriceBD") = 0D
            existing("DiscountCash") = 0D
            existing("DiscountPString") = "0"
            existing("DiscountP") = 0D
            existing("Price") = 0D
            existing("SumPrice") = 0D
            dtItems.Rows.Add(existing)
        Else
            Dim totalQuantity As Decimal = Convert.ToDecimal(existing("Number"), decCulture) + quantity
            existing("Number") = totalQuantity
            existing("PriceBD") = 0D
            existing("Price") = 0D
            existing("SumPrice") = 0D
        End If

        btnPrint.Enabled = True
    End Sub

    Private Function TryLoadProductFromText() As Boolean
        Dim keyword As String = txtProductKey.Text.Trim()
        If String.IsNullOrWhiteSpace(keyword) Then Return False

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String = "SELECT TOP 1 PN, RTRIM(ProductCode), RTRIM(Barcode), RTRIM(Name), " &
                                      "ISNULL(Price1, 0), ISNULL(SUnit, ''), ISNULL(AllowDecimal, 0) " &
                                      "FROM Product WHERE Status IS NULL AND " &
                                      "(Name LIKE N'%' + @Keyword + N'%' OR ProductCode LIKE N'%' + @Keyword + N'%' OR Barcode LIKE N'%' + @Keyword + N'%') " &
                                      "ORDER BY ProductCode"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = keyword
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        If Not rd.Read() Then
                            MessageBox.Show("ไม่พบสินค้าที่ค้นหา: " & keyword, "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                        selectedProductPN = Convert.ToInt64(rd.GetValue(0), CultureInfo.InvariantCulture)
                        selectedProductCode = Convert.ToString(rd.GetValue(1)).Trim()
                        selectedProductBarcode = Convert.ToString(rd.GetValue(2)).Trim()
                        selectedProductName = Convert.ToString(rd.GetValue(3)).Trim()
                        selectedProductPrice = Convert.ToDecimal(rd.GetValue(4), CultureInfo.InvariantCulture)
                        selectedProductUnit = Convert.ToString(rd.GetValue(5)).Trim()
                        selectedProductAllowDecimal = Convert.ToBoolean(rd.GetValue(6), CultureInfo.InvariantCulture)
                        txtProductKey.Text = selectedProductName
                        txtProductCode.Text = selectedProductCode
                        Return True
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ค้นหาสินค้าไม่สำเร็จ: " & ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub ClearSelectedProduct()
        selectedProductPN = 0
        selectedProductCode = ""
        selectedProductBarcode = ""
        selectedProductName = ""
        selectedProductUnit = ""
        selectedProductPrice = 0D
        selectedProductAllowDecimal = False
        txtProductKey.Clear()
        txtProductCode.Clear()
        txtQuantity.Text = "1"
        txtProductKey.Focus()
    End Sub

    Private Sub RenumberItems()
        'ใบซื้อเดิมอาจมีเลข Item ที่ไม่ต่อเนื่องและถูกอ้างอิงอยู่ใน BuyD
        'จึงห้ามเรียงเลขใหม่ระหว่างแก้ไขย้อนหลัง
        If isEditMode Then Return

        Dim item As Integer = 1
        For Each row As DataRow In dtItems.Select("", "ProductCode ASC, Item ASC")
            row("Item") = item
            item += 1
        Next
    End Sub

    Private Function GetNextItemNumber() As Integer
        Dim maxItem As Integer = 0
        For Each row As DataRow In dtItems.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            maxItem = Math.Max(maxItem, Convert.ToInt32(row("Item"), decCulture))
        Next
        Return maxItem + 1
    End Function

    Private Sub AddHeaderParameters(cmd As SqlCommand, subtotal As Decimal, discountPercent As Decimal, discountCash As Decimal, vatPercent As Decimal, beforeVat As Decimal, total As Decimal, exchangeRate As Decimal)
        cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
        cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
        cmd.Parameters.Add("@DocNumber", SqlDbType.Int).Value = currentBuyNumber
        cmd.Parameters.Add("@EditCount", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@SupplierNumber", SqlDbType.Int).Value = supplierNumber
        cmd.Parameters.Add("@BillNumber", SqlDbType.NVarChar, 100).Value = SqlValue(txtVendorBillNumber.Text.Trim())
        cmd.Parameters.Add("@Credit", SqlDbType.Int).Value = CInt(Math.Max(0D, ToDecimalValue(txtCredit.Text, 0D)))
        cmd.Parameters.Add("@VatType", SqlDbType.Int).Value = Math.Max(0, cbVatType.SelectedIndex)
        cmd.Parameters.Add("@BuyOrderNumber", SqlDbType.Int).Value = DBNull.Value
        cmd.Parameters.Add("@PayType", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@Money", SqlDbType.Decimal).Value = subtotal
        cmd.Parameters.Add("@DCPercent", SqlDbType.Decimal).Value = discountPercent
        cmd.Parameters.Add("@DCCash", SqlDbType.Decimal).Value = discountCash
        cmd.Parameters.Add("@PercentVat", SqlDbType.Decimal).Value = vatPercent
        cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = dtpDate.Value
        cmd.Parameters.Add("@PriceBV", SqlDbType.Decimal).Value = beforeVat
        cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = total
        cmd.Parameters.Add("@Paid", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@PaidMoney", SqlDbType.Decimal).Value = 0D
        cmd.Parameters.Add("@HiddenCostType", SqlDbType.Int).Value = Math.Max(0, cboCostType.SelectedIndex)
        cmd.Parameters.Add("@HiddenCost", SqlDbType.Decimal).Value = 0D
        cmd.Parameters.Add("@HiddenDiscount", SqlDbType.Decimal).Value = 0D
        cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = 1
        cmd.Parameters.Add("@ExchangeRate", SqlDbType.Decimal).Value = exchangeRate
        cmd.Parameters.Add("@DateTimeTax", SqlDbType.DateTime).Value = dtpTaxDate.Value
        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 1000).Value = SqlValue(txtComment.Text.Trim())
        cmd.Parameters.Add("@CreateOfficer", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
        cmd.Parameters.Add("@UpdateOfficer", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If supplierNumber <= 0 Then
            MessageBox.Show("กรุณาระบุผู้ขายก่อนบันทึก", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If dtItems.Rows.Count = 0 Then
            MessageBox.Show("กรุณาเพิ่มรายการสินค้าอย่างน้อย 1 รายการ", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim subtotal As Decimal = dtItems.AsEnumerable().Sum(Function(r) r.Field(Of Decimal)("SumPrice"))
        Dim discountPercent As Decimal = Math.Max(0D, ToDecimalValue(KryptonTextBox1.Text, 0D))
        Dim discountCash As Decimal = Math.Max(0D, ToDecimalValue(KryptonTextBox6.Text, 0D))
        Dim vatPercent As Decimal = If(cbHaveTax.SelectedIndex = 1, Math.Max(0D, ToDecimalValue(KryptonTextBox2.Text, 0D)), 0D)
        Dim percentDiscountMoney As Decimal = Math.Round(subtotal * discountPercent / 100D, 2, MidpointRounding.AwayFromZero)
        Dim beforeVat As Decimal = Math.Max(0D, subtotal - percentDiscountMoney - discountCash)
        Dim total As Decimal = beforeVat

        If cbVatType.SelectedIndex = 0 Then
            total = beforeVat + Math.Round(beforeVat * vatPercent / 100D, 2, MidpointRounding.AwayFromZero)
        End If

        KryptonTextBox5.Text = percentDiscountMoney.ToString("N2")
        KryptonTextBox7.Text = beforeVat.ToString("N2")
        txtVat.Text = Math.Round(total - beforeVat, 2, MidpointRounding.AwayFromZero).ToString("N2")
        KryptonTextBox8.Text = total.ToString("N2")
        txtTotalMoney.Text = total.ToString("N2")

        If isEditMode Then
            If Not UpdateExistingBuy(subtotal, discountPercent, discountCash, vatPercent, beforeVat, total) Then Return

            isSaved = True
            btnPrint.Enabled = True
            Dim successMessage As String = If(increaseOnlyAfterSale,
                                              "เพิ่มจำนวนซื้อย้อนหลังในใบเลขที่ " & currentBuyNumber.ToString(decCulture) & " เรียบร้อยแล้ว" &
                                              If(convertedAdjustmentCount > 0, Environment.NewLine & "ระบบยกเลิกรายการปรับสต๊อกเดิมที่ตรงกันแล้ว", "") &
                                              Environment.NewLine & "ต้องการพิมพ์ใบซื้อหรือไม่?",
                                              "แก้ไขใบซื้อเลขที่ " & currentBuyNumber.ToString(decCulture) & " เรียบร้อยแล้ว" & Environment.NewLine & "ต้องการพิมพ์ใบซื้อหรือไม่?")
            If MessageBox.Show(successMessage, "สำเร็จ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                PrintBuyReport()
            End If
            Close()
            Return
        End If

        Try
            Cursor = Cursors.WaitCursor
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Try
                        Using numberCmd As New SqlCommand("SELECT ISNULL(MAX(BuyNumber), 0) + 1 FROM Buy WITH (UPDLOCK, HOLDLOCK) WHERE BranchNumber = @BranchNumber", conn, tr)
                            numberCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                            currentBuyNumber = Convert.ToInt32(numberCmd.ExecuteScalar(), CultureInfo.InvariantCulture)
                        End Using

                        Const headerSql As String =
                            "INSERT INTO Buy (BranchNumber,BuyNumber,DocNumber,EditCount,SupplierNumber,BillNumber,Credit,VatType,BuyOrderNumber,PayType,Money,DCPercent,DCCash,PercentVat,DateTime,Paid,PaidMoney,PriceBV,TotalPrice,HiddenCostType,HiddenCost,HiddenDiscount,CurrencyID,ExchangeRate,DateTimeTax,Comment,CONumber,UONumber,CTime,UTime) " &
                            "VALUES (@BranchNumber,@BuyNumber,@DocNumber,@EditCount,@SupplierNumber,@BillNumber,@Credit,@VatType,@BuyOrderNumber,@PayType,@Money,@DCPercent,@DCCash,@PercentVat,@DateTime,@Paid,@PaidMoney,@PriceBV,@TotalPrice,@HiddenCostType,@HiddenCost,@HiddenDiscount,@CurrencyID,@ExchangeRate,@DateTimeTax,@Comment,@CreateOfficer,@UpdateOfficer,GETDATE(),GETDATE())"
                        Using headerCmd As New SqlCommand(headerSql, conn, tr)
                            AddHeaderParameters(headerCmd, subtotal, discountPercent, discountCash, vatPercent, beforeVat, total, 1D)
                            headerCmd.ExecuteNonQuery()
                        End Using

                        For Each row As DataRow In dtItems.Rows
                            InsertBuyDetail(conn, tr, row)
                            InsertBuyStock(conn, tr, row, vatPercent)
                        Next
                        tr.Commit()
                    Catch
                        Try
                            tr.Rollback()
                        Catch
                        End Try
                        Throw
                    End Try
                End Using
            End Using

            lbDocNumber.Text = currentBuyNumber.ToString(CultureInfo.InvariantCulture)
            isSaved = True
            btnPrint.Enabled = True
            If MessageBox.Show("บันทึกใบซื้อเรียบร้อย เลขที่ " & currentBuyNumber.ToString(CultureInfo.InvariantCulture) & Environment.NewLine & "ต้องการพิมพ์ใบซื้อหรือไม่?", "สำเร็จ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                PrintBuyReport()
            End If
            Close()
        Catch ex As Exception
            ShowError("บันทึกใบซื้อไม่สำเร็จ", ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function UpdateExistingBuy(subtotal As Decimal, discountPercent As Decimal, discountCash As Decimal,
                                       vatPercent As Decimal, beforeVat As Decimal, total As Decimal) As Boolean
        If Not CanCurrentUserEditBuy() Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์แก้ไขรายการใบซื้อ", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If increaseOnlyAfterSale Then
            Return UpdateExistingBuyIncreaseOnly(subtotal, discountPercent, discountCash, vatPercent, beforeVat, total)
        End If

        Dim newQuantities As New Dictionary(Of Integer, Decimal)()
        Dim newRows As New Dictionary(Of Integer, DataRow)()
        For Each row As DataRow In dtItems.Rows
            Dim pn As Integer = Convert.ToInt32(row("PN"), decCulture)
            newQuantities(pn) = Convert.ToDecimal(row("Number"), decCulture)
            newRows(pn) = row
        Next

        Try
            Cursor = Cursors.WaitCursor
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Try
                        Const stockReadSql As String =
                            "SELECT bs.PN, ISNULL(bs.Number,0) AS OldNumber, s.PN AS StockPN, ISNULL(s.Number,0) AS CurrentNumber " &
                            "FROM BuyStock bs WITH (UPDLOCK,HOLDLOCK) " &
                            "LEFT JOIN Stock s WITH (UPDLOCK,HOLDLOCK) ON s.BranchNumber=bs.BranchNumber AND s.BillType=@BillType " &
                            "AND s.BillNumber=bs.BuyNumber AND s.PN=bs.PN " &
                            "WHERE bs.BranchNumber=@BranchNumber AND bs.BuyNumber=@BuyNumber"
                        Dim oldQuantities As New Dictionary(Of Integer, Decimal)()
                        Using cmd As New SqlCommand(stockReadSql, conn, tr)
                            cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
                            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                            cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                            Using reader As SqlDataReader = cmd.ExecuteReader()
                                While reader.Read()
                                    Dim pn As Integer = Convert.ToInt32(reader("PN"), decCulture)
                                    Dim oldQuantity As Decimal = DbDecimal(reader("OldNumber"))
                                    If reader("StockPN") Is DBNull.Value OrElse DbDecimal(reader("CurrentNumber")) < oldQuantity Then
                                        Throw New InvalidOperationException("ใบซื้อนี้มีรายการที่ถูกนำไปขายหรือตัดสต๊อกแล้ว")
                                    End If
                                    oldQuantities(pn) = oldQuantity
                                End While
                            End Using
                        End Using

                        Const updateHeaderSql As String =
                            "UPDATE Buy SET SupplierNumber=@SupplierNumber, BillNumber=@BillNumber, Credit=@Credit, VatType=@VatType, " &
                            "Money=@Money, DCPercent=@DCPercent, DCCash=@DCCash, PercentVat=@PercentVat, DateTime=@DateTime, " &
                            "PriceBV=@PriceBV, TotalPrice=@TotalPrice, HiddenCostType=@HiddenCostType, ExchangeRate=@ExchangeRate, " &
                            "DateTimeTax=@DateTimeTax, Comment=@Comment, UONumber=@UpdateOfficer, UTime=GETDATE(), " &
                            "EditCount=ISNULL(EditCount,0)+1 " &
                            "WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND Status IS NULL"
                        Using cmd As New SqlCommand(updateHeaderSql, conn, tr)
                            AddHeaderParameters(cmd, subtotal, discountPercent, discountCash, vatPercent, beforeVat, total, 1D)
                            cmd.ExecuteNonQuery()
                        End Using

                        For Each oldItem As KeyValuePair(Of Integer, Decimal) In oldQuantities
                            Dim pn As Integer = oldItem.Key
                            If newQuantities.ContainsKey(pn) Then
                                Dim newQuantity As Decimal = newQuantities(pn)
                                Dim delta As Decimal = newQuantity - oldItem.Value
                                Dim item As DataRow = newRows(pn)
                                Using cmd As New SqlCommand("UPDATE BuyStock SET Number=@Number, BuyPrice=@BuyPrice, BuyPercentVat=@Vat WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND PN=@PN", conn, tr)
                                    cmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = newQuantity
                                    cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = item("Price")
                                    cmd.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
                                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                    cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                    cmd.ExecuteNonQuery()
                                End Using
                                If delta <> 0D Then
                                    Using cmd As New SqlCommand("UPDATE Stock SET NumberNS=ISNULL(NumberNS,0)+@Delta, Number=ISNULL(Number,0)+@Delta, BuyPrice=@BuyPrice, PercentVat=@Vat WHERE BranchNumber=@BranchNumber AND BillType=@BillType AND BillNumber=@BuyNumber AND PN=@PN", conn, tr)
                                        cmd.Parameters.Add("@Delta", SqlDbType.Decimal).Value = delta
                                        cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = item("Price")
                                        cmd.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
                                        cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                        cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
                                        cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                        cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                        cmd.ExecuteNonQuery()
                                    End Using
                                End If
                            Else
                                Using cmd As New SqlCommand("DELETE FROM BuyStock WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND PN=@PN; DELETE FROM Stock WHERE BranchNumber=@BranchNumber AND BillType=@BillType AND BillNumber=@BuyNumber AND PN=@PN", conn, tr)
                                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                    cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                    cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
                                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If
                        Next

                        For Each newItem As KeyValuePair(Of Integer, DataRow) In newRows
                            If Not oldQuantities.ContainsKey(newItem.Key) Then
                                InsertBuyStock(conn, tr, newItem.Value, vatPercent)
                            End If
                        Next

                        Using cmd As New SqlCommand("DELETE FROM BuyD WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber", conn, tr)
                            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                            cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                            cmd.ExecuteNonQuery()
                        End Using
                        For Each row As DataRow In dtItems.Rows
                            InsertBuyDetail(conn, tr, row)
                        Next
                        tr.Commit()
                    Catch
                        Try
                            tr.Rollback()
                        Catch
                        End Try
                        Throw
                    End Try
                End Using
            End Using
            Return True
        Catch ex As Exception
            ShowError("แก้ไขใบซื้อไม่สำเร็จ", ex)
            Return False
        Finally
            Cursor = Cursors.Default
        End Try
    End Function

    Private Function UpdateExistingBuyIncreaseOnly(subtotal As Decimal, discountPercent As Decimal, discountCash As Decimal,
                                                   vatPercent As Decimal, beforeVat As Decimal, total As Decimal) As Boolean
        Dim changed As Boolean = False
        Dim currentProductPNs As New HashSet(Of Integer)()
        For Each row As DataRow In dtItems.Rows
            Dim pn As Integer = Convert.ToInt32(row("PN"), decCulture)
            currentProductPNs.Add(pn)
            Dim newQuantity As Decimal = DbDecimal(row("Number"))
            If newQuantity <= 0D Then
                MessageBox.Show("จำนวนสินค้าต้องมากกว่า 0", "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            If originalBuyQuantities.ContainsKey(pn) AndAlso newQuantity < originalBuyQuantities(pn) Then
                MessageBox.Show("ไม่อนุญาตให้ลดจำนวนซื้อย้อนหลัง", "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            If Not originalBuyQuantities.ContainsKey(pn) OrElse newQuantity > originalBuyQuantities(pn) Then
                changed = True
            End If
        Next
        For Each originalPN As Integer In originalBuyQuantities.Keys
            If Not currentProductPNs.Contains(originalPN) Then
                MessageBox.Show("ไม่อนุญาตให้ลบรายการสินค้าในโหมดนี้", "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Next
        If Not changed Then
            MessageBox.Show("ยังไม่มีจำนวนซื้อที่เพิ่มขึ้น", "แก้จำนวนซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Try
            Cursor = Cursors.WaitCursor
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Try
                        convertedAdjustmentCount = 0
                        For Each row As DataRow In dtItems.Rows
                            Dim pn As Integer = Convert.ToInt32(row("PN"), decCulture)
                            Dim newQuantity As Decimal = DbDecimal(row("Number"))

                            'สินค้าใหม่ไม่มีรายการเดิมให้ปรับ จึงเพิ่ม BuyStock/Stock ใน Transaction เดียวกัน
                            If Not originalBuyQuantities.ContainsKey(pn) Then
                                InsertBuyStock(conn, tr, row, vatPercent)
                                Continue For
                            End If

                            Dim oldQuantity As Decimal = originalBuyQuantities(pn)
                            Dim delta As Decimal = newQuantity - oldQuantity
                            If delta <= 0D Then Continue For

                            'ถ้ามีการปรับสต๊อกชดเชยส่วนต่างไว้แล้ว และยังไม่ถูกขาย
                            'ให้ยกเลิกเอกสารนั้นใน Transaction เดียวกัน เพื่อไม่ให้นับซ้ำ
                            CancelUnusedMatchingAdjustment(conn, tr, pn, delta, dtpDate.Value.Date)

                            Using cmd As New SqlCommand("UPDATE BuyStock SET Number=@Number, BuyPrice=@BuyPrice, BuyPercentVat=@Vat " &
                                                       "WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND PN=@PN", conn, tr)
                                cmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = newQuantity
                                cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = row("Price")
                                cmd.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
                                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("ไม่พบรายการ BuyStock ของสินค้า " & pn.ToString(decCulture))
                            End Using

                            Using cmd As New SqlCommand("UPDATE BuyD SET Number=@Number, SumPrice=@SumPrice " &
                                                       "WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND PN=@PN AND Item=@Item", conn, tr)
                                cmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = newQuantity
                                cmd.Parameters.Add("@SumPrice", SqlDbType.Decimal).Value = row("SumPrice")
                                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                cmd.Parameters.Add("@Item", SqlDbType.Int).Value = row("Item")
                                If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("ไม่พบรายละเอียดใบซื้อของสินค้า " & pn.ToString(decCulture))
                            End Using

                            Using cmd As New SqlCommand("UPDATE Stock SET NumberNS=ISNULL(NumberNS,0)+@Delta, Number=ISNULL(Number,0)+@Delta, BuyPrice=@BuyPrice, PercentVat=@Vat " &
                                                       "WHERE BranchNumber=@BranchNumber AND BillType=@BillType AND BillNumber=@BuyNumber AND PN=@PN", conn, tr)
                                cmd.Parameters.Add("@Delta", SqlDbType.Decimal).Value = delta
                                cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = row("Price")
                                cmd.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
                                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
                                cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                If cmd.ExecuteNonQuery() = 0 Then
                                    Using insert As New SqlCommand("INSERT INTO Stock(BranchNumber,BillType,BillNumber,PN,NumberNS,NumberS,Number,BuyPrice,PercentVat,CanUseTime) " &
                                                                   "VALUES(@BranchNumber,@BillType,@BuyNumber,@PN,@Delta,0,@Delta,@BuyPrice,@Vat,@CanUseTime)", conn, tr)
                                        insert.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                                        insert.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
                                        insert.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                                        insert.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                                        insert.Parameters.Add("@Delta", SqlDbType.Decimal).Value = delta
                                        insert.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = row("Price")
                                        insert.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
                                        insert.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = dtpDate.Value
                                        insert.ExecuteNonQuery()
                                    End Using
                                End If
                            End Using
                        Next

                        'บันทึกรายละเอียดของสินค้าใหม่ที่เพิ่งเพิ่มเข้ามาในใบซื้อ
                        For Each row As DataRow In dtItems.Rows
                            Dim pn As Integer = Convert.ToInt32(row("PN"), decCulture)
                            If Not originalBuyQuantities.ContainsKey(pn) Then
                                InsertBuyDetail(conn, tr, row)
                            End If
                        Next

                        Using cmd As New SqlCommand("UPDATE Buy SET Money=@Money, DCPercent=@DCPercent, DCCash=@DCCash, PercentVat=@PercentVat, " &
                                                   "PriceBV=@PriceBV, TotalPrice=@TotalPrice, UONumber=@UpdateOfficer, UTime=GETDATE(), EditCount=ISNULL(EditCount,0)+1 " &
                                                   "WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND Status IS NULL", conn, tr)
                            cmd.Parameters.Add("@Money", SqlDbType.Decimal).Value = subtotal
                            cmd.Parameters.Add("@DCPercent", SqlDbType.Decimal).Value = discountPercent
                            cmd.Parameters.Add("@DCCash", SqlDbType.Decimal).Value = discountCash
                            cmd.Parameters.Add("@PercentVat", SqlDbType.Decimal).Value = vatPercent
                            cmd.Parameters.Add("@PriceBV", SqlDbType.Decimal).Value = beforeVat
                            cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = total
                            cmd.Parameters.Add("@UpdateOfficer", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
                            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                            cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                            If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("ไม่พบใบซื้อที่ต้องการแก้ไข")
                        End Using
                        tr.Commit()
                    Catch
                        Try
                            tr.Rollback()
                        Catch
                        End Try
                        Throw
                    End Try
                End Using
            End Using
            Return True
        Catch ex As Exception
            ShowError("แก้จำนวนซื้อย้อนหลังไม่สำเร็จ", ex)
            Return False
        Finally
            Cursor = Cursors.Default
        End Try
    End Function

    Private Sub CancelUnusedMatchingAdjustment(conn As SqlConnection, tr As SqlTransaction, pn As Integer, delta As Decimal, adjustmentDate As DateTime)
        Dim candidates As New DataTable()
        Using cmd As New SqlCommand("SELECT SA.ID, SAD.NumAdjust " &
                                   "FROM StockAdjustD SAD INNER JOIN StockAdjust SA ON SA.BranchNumber=SAD.BranchNumber AND SA.ID=SAD.ID " &
                                   "WHERE SA.BranchNumber=@BranchNumber AND SAD.PN=@PN AND SAD.NumAdjust>0 AND SA.Status IS NULL " &
                                   "AND SA.DateTime>=@DateFrom AND SA.DateTime<@DateTo ORDER BY SA.ID DESC", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = adjustmentDate.Date
            cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = adjustmentDate.Date.AddDays(1)
            Using da As New SqlDataAdapter(cmd)
                da.Fill(candidates)
            End Using
        End Using

        If candidates.Rows.Count = 0 Then Return
        If candidates.Rows.Count <> 1 OrElse DbDecimal(candidates.Rows(0)("NumAdjust")) <> delta Then
            Throw New InvalidOperationException("พบรายการปรับสต๊อกของสินค้าในวันเดียวกัน แต่จำนวนไม่ตรงกับส่วนต่าง " & delta.ToString("N2") & " แก้ว กรุณาตรวจสอบก่อนแก้ใบซื้อ เพื่อป้องกันยอดซ้ำ")
        End If

        Dim adjustmentId As Integer = Convert.ToInt32(candidates.Rows(0)("ID"), decCulture)
        Using countCmd As New SqlCommand("SELECT COUNT(1) FROM StockAdjustD WHERE BranchNumber=@BranchNumber AND ID=@ID", conn, tr)
            countCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            countCmd.Parameters.Add("@ID", SqlDbType.Int).Value = adjustmentId
            If Convert.ToInt32(countCmd.ExecuteScalar(), decCulture) <> 1 Then
                Throw New InvalidOperationException("เอกสารปรับสต๊อก Doc " & adjustmentId.ToString(decCulture) & " มีหลายรายการ จึงยังไม่ยกเลิกอัตโนมัติ")
            End If
        End Using

        Dim adjustmentStock As Decimal = 0D
        Using stockCmd As New SqlCommand("SELECT ISNULL(Number,0) FROM Stock WHERE BranchNumber=@BranchNumber AND BillType=6 AND BillNumber=@ID AND PN=@PN", conn, tr)
            stockCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            stockCmd.Parameters.Add("@ID", SqlDbType.Int).Value = adjustmentId
            stockCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            Dim value = stockCmd.ExecuteScalar()
            If value IsNot Nothing AndAlso value IsNot DBNull.Value Then adjustmentStock = Convert.ToDecimal(value, decCulture)
        End Using
        If adjustmentStock <> delta Then
            Throw New InvalidOperationException("พบรายการปรับสต๊อก Doc " & adjustmentId.ToString(decCulture) & " แต่จำนวนถูกนำไปใช้แล้วหรือไม่ตรงกัน จึงไม่ยกเลิกอัตโนมัติ")
        End If

        Using cmd As New SqlCommand("UPDATE StockAdjust SET Status=1,UONumber=@User,UTime=GETDATE() WHERE BranchNumber=@BranchNumber AND ID=@ID; " &
                                   "DELETE FROM Stock WHERE BranchNumber=@BranchNumber AND BillType=6 AND BillNumber=@ID AND PN=@PN", conn, tr)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = adjustmentId
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            cmd.ExecuteNonQuery()
        End Using
        convertedAdjustmentCount += 1
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'เลือกใบซื้อย้อนหลัง แล้วเปิดใบที่เลือกด้วย rptBuyInvoice.rdlc
        Using reportForm As New frmBuyHistory()
            reportForm.ShowDialog(Me)
        End Using
    End Sub

    Public Shared Sub PreviewSavedBuy(buyNumber As Integer, branchNumber As Integer, owner As IWin32Window)
        'โหลดรายงานโดยไม่สร้างฟอร์ม frmBuy จริง เพื่อไม่ให้การ Dispose ตาราง Krypton
        'ระหว่างปิดรายงานทำให้เกิด ArrayList index out of range
        Using reportContext As New frmBuy(True)
            reportContext.currentBuyNumber = buyNumber
            reportContext.LoadBuyPrintDataForBranch(branchNumber)
            Using viewer As New frmReportViewer()
                viewer.LoadBuyInvoiceReport(reportContext.BuildBuyInvoiceTable())
                viewer.ShowDialog(owner)
            End Using
        End Using
    End Sub

    Private Sub PreviewUnsavedBuyReport()
        Try
            LoadPreviewBuyData()
            Using viewer As New frmReportViewer()
                viewer.LoadBuyInvoiceReport(BuildBuyInvoiceTable())
                viewer.ShowDialog(Me)
            End Using
        Catch ex As Exception
            ShowError("เปิดตัวอย่างใบซื้อไม่สำเร็จ", ex)
        End Try
    End Sub

    Private Sub LoadPreviewBuyData()
        printHeader = New DataTable("Buy")
        For Each name As String In {"DocNumber", "BillNumber", "PriceBV", "TotalPrice", "DateTime", "Credit", "Money", "DCPercent", "DCCash", "PercentVat", "Comment", "SupplierName", "SupplierAddress", "ShopName", "ShopBranch", "ShopAddress", "ShopTel", "ShopFax", "ShopTaxCode"}
            printHeader.Columns.Add(name, GetType(Object))
        Next
        Dim h As DataRow = printHeader.NewRow()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT TOP 1 Name,BranchName,Address,Tel,Fax,TaxCode FROM Branch WHERE BranchNumber=@BranchNumber", conn)
                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        h("ShopName") = reader("Name")
                        h("ShopBranch") = reader("BranchName")
                        h("ShopAddress") = reader("Address")
                        h("ShopTel") = reader("Tel")
                        h("ShopFax") = reader("Fax")
                        h("ShopTaxCode") = reader("TaxCode")
                    End If
                End Using
            End Using
            If supplierNumber > 0 Then
                Using cmd As New SqlCommand("SELECT TOP 1 Name,Address FROM Supplier WHERE SupplierNumber=@SupplierNumber", conn)
                    cmd.Parameters.Add("@SupplierNumber", SqlDbType.Int).Value = supplierNumber
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            h("SupplierName") = reader("Name")
                            h("SupplierAddress") = reader("Address")
                        End If
                    End Using
                End Using
            End If
        End Using
        Dim subtotal As Decimal = dtItems.AsEnumerable().Sum(Function(r) Convert.ToDecimal(r("SumPrice"), decCulture))
        Dim discountPercent As Decimal = Math.Max(0D, ToDecimalValue(KryptonTextBox1.Text, 0D))
        Dim discountCash As Decimal = Math.Max(0D, ToDecimalValue(KryptonTextBox6.Text, 0D))
        h("DocNumber") = "ตัวอย่าง"
        h("BillNumber") = ""
        h("PriceBV") = Math.Max(0D, subtotal - Math.Round(subtotal * discountPercent / 100D, 2) - discountCash)
        h("TotalPrice") = ToDecimalValue(txtTotalMoney.Text, subtotal)
        h("DateTime") = dtpDate.Value
        h("Credit") = ToDecimalValue(txtCredit.Text, 0D)
        h("Money") = subtotal
        h("DCPercent") = discountPercent
        h("DCCash") = discountCash
        h("PercentVat") = ToDecimalValue(txtVat.Text, 0D)
        h("Comment") = txtComment.Text
        printHeader.Rows.Add(h)
        printDetails = dtItems.Copy()
    End Sub

    Private Sub PrintBuyReport()
        Try
            LoadBuyPrintData()
            Using viewer As New frmReportViewer()
                viewer.LoadBuyInvoiceReport(BuildBuyInvoiceTable())
                viewer.ShowDialog(Me)
            End Using
        Catch ex As Exception
            ShowError("พิมพ์ใบซื้อไม่สำเร็จ", ex)
        End Try
    End Sub

    Private Sub LoadBuyPrintData()
        LoadBuyPrintDataForBranch(BranchNumber)
    End Sub

    Private Sub LoadBuyPrintDataForBranch(branchNumber As Integer)
        printHeader = New DataTable("Buy")
        printDetails = New DataTable("BuyD")
        Using conn As SqlConnection = modVariable.OpenConnection()
            Const headerSql As String = "SELECT TOP 1 Buy.DocNumber,Buy.BillNumber,Buy.PriceBV,Buy.TotalPrice,Buy.DateTime,Buy.Credit,Buy.Money,Buy.DCPercent,Buy.DCCash,Buy.PercentVat,Buy.Comment," &
                "ISNULL(RTRIM(Supplier.Name),'') AS SupplierName,ISNULL(RTRIM(Supplier.Address),'') AS SupplierAddress," &
                "ISNULL(RTRIM(Branch.Name),'') AS ShopName,ISNULL(RTRIM(Branch.BranchName),'') AS ShopBranch,ISNULL(RTRIM(Branch.Address),'') AS ShopAddress,ISNULL(RTRIM(Branch.Tel),'') AS ShopTel,ISNULL(RTRIM(Branch.Fax),'') AS ShopFax,ISNULL(RTRIM(Branch.TaxCode),'') AS ShopTaxCode " &
                "FROM Buy LEFT JOIN Supplier ON Buy.SupplierNumber=Supplier.SupplierNumber LEFT JOIN Branch ON Buy.BranchNumber=Branch.BranchNumber WHERE Buy.BranchNumber=@BranchNumber AND Buy.BuyNumber=@BuyNumber"
            Using cmd As New SqlCommand(headerSql, conn)
                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(printHeader)
                End Using
            End Using
            Const detailSql As String = "SELECT BuyD.Item,ISNULL(RTRIM(Product.ProductCode),'') AS ProductCode,ISNULL(RTRIM(Product.Name),'') AS ProductName,BuyD.Number,ISNULL(RTRIM(BuyD.LUnit),'') AS LUnit,BuyD.PriceBD,BuyD.DiscountCash,BuyD.DiscountP,BuyD.Price,BuyD.SumPrice FROM BuyD LEFT JOIN Product ON BuyD.PN=Product.PN WHERE BuyD.BranchNumber=@BranchNumber AND BuyD.BuyNumber=@BuyNumber ORDER BY ISNULL(RTRIM(Product.ProductCode),''), BuyD.Item"
            Using cmd As New SqlCommand(detailSql, conn)
                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(printDetails)
                End Using
            End Using
        End Using
    End Sub

    Private Function BuildBuyInvoiceTable() As DataTable
        Dim result As New DataTable("dsBuyInvoice")
        For Each name As String In {"CompanyName", "BranchName", "CompanyAddress", "CompanyTel", "Fax", "TaxId", "SupplierName", "SupplierAddress", "DocNo", "InvoiceNo", "Credit", "ProductName", "UnitName", "Receiver", "Approver"}
            result.Columns.Add(name, GetType(String))
        Next
        result.Columns.Add("BuyDate", GetType(DateTime))
        result.Columns.Add("RowNumber", GetType(Integer))
        For Each name As String In {"Quantity", "Price", "Discount", "NetPrice", "LineTotal", "TotalQuantity", "TotalDiscount", "TotalBeforeVat", "VatPercent", "TotalVat", "GrandTotal"}
            result.Columns.Add(name, GetType(Decimal))
        Next
        If printHeader Is Nothing OrElse printHeader.Rows.Count = 0 Then Return result
        Dim h As DataRow = printHeader.Rows(0)
        Dim orderedDetails As DataRow()
        If printDetails.Columns.Contains("ProductCode") Then
            orderedDetails = printDetails.Select("", "ProductCode ASC, Item ASC")
        Else
            orderedDetails = printDetails.Select("", "Item ASC")
        End If

        Dim totalQty As Decimal = 0D
        For Each d As DataRow In orderedDetails
            totalQty += PrintDecimal(d("Number"))
        Next
        Dim beforeVat As Decimal = PrintDecimal(h("PriceBV"))
        Dim grandTotal As Decimal = PrintDecimal(h("TotalPrice"))
        Dim totalDiscount As Decimal = PrintDecimal(h("DCPercent")) + PrintDecimal(h("DCCash"))
        Dim rowNumber As Integer = 1
        For Each d As DataRow In orderedDetails
            Dim r As DataRow = result.NewRow()
            r("CompanyName") = PrintText(h, "ShopName")
            r("BranchName") = PrintText(h, "ShopBranch")
            r("CompanyAddress") = PrintText(h, "ShopAddress")
            r("CompanyTel") = PrintText(h, "ShopTel")
            r("Fax") = PrintText(h, "ShopFax")
            r("TaxId") = PrintText(h, "ShopTaxCode")
            r("SupplierName") = PrintText(h, "SupplierName")
            r("SupplierAddress") = PrintText(h, "SupplierAddress")
            r("DocNo") = PrintText(h, "DocNumber")
            r("InvoiceNo") = PrintText(h, "BillNumber")
            r("BuyDate") = Convert.ToDateTime(h("DateTime"))
            r("Credit") = PrintText(h, "Credit") & " วัน"
            r("RowNumber") = rowNumber
            r("ProductName") = PrintText(d, "ProductName")
            r("Quantity") = PrintDecimal(d("Number"))
            r("UnitName") = PrintText(d, "LUnit")
            r("Price") = PrintDecimal(d("PriceBD"))
            r("Discount") = If(PrintDecimal(d("DiscountCash")) = 0D, PrintDecimal(d("DiscountP")), PrintDecimal(d("DiscountCash")))
            r("NetPrice") = PrintDecimal(d("Price"))
            r("LineTotal") = PrintDecimal(d("SumPrice"))
            r("TotalQuantity") = totalQty
            r("TotalDiscount") = totalDiscount
            r("TotalBeforeVat") = beforeVat
            r("VatPercent") = PrintDecimal(h("PercentVat"))
            r("TotalVat") = grandTotal - beforeVat
            r("GrandTotal") = grandTotal
            result.Rows.Add(r)
            rowNumber += 1
        Next
        Return result
    End Function

    Private Shared Function PrintText(row As DataRow, column As String) As String
        If row Is Nothing OrElse Not row.Table.Columns.Contains(column) OrElse row.IsNull(column) Then Return ""
        Return Convert.ToString(row(column), CultureInfo.CurrentCulture).Trim()
    End Function

    Private Shared Function PrintDecimal(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
        Dim result As Decimal
        If Decimal.TryParse(Convert.ToString(value, CultureInfo.CurrentCulture), NumberStyles.Any, CultureInfo.CurrentCulture, result) Then Return result
        Return 0D
    End Function

    Private Sub InsertBuyDetail(conn As SqlConnection, tr As SqlTransaction, row As DataRow)
        Using cmd As New SqlCommand("INSERT INTO BuyD (BranchNumber,BuyNumber,Type,Item,SItem,LineText,PN,Number,LUnit,NumSPerLUnit,PriceBD,DiscountCash,DiscountPString,DiscountP,Price,SumPrice) VALUES (@BranchNumber,@BuyNumber,@Type,@Item,@SItem,NULL,@PN,@Number,@LUnit,1,@PriceBD,@DiscountCash,@DiscountPString,@DiscountP,@Price,@SumPrice)", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            cmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = row("Type")
            cmd.Parameters.Add("@Item", SqlDbType.Int).Value = row("Item")
            cmd.Parameters.Add("@SItem", SqlDbType.Int).Value = row("SItem")
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = row("PN")
            cmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = row("Number")
            cmd.Parameters.Add("@LUnit", SqlDbType.NVarChar, 50).Value = SqlValue(row("LUnit"))
            cmd.Parameters.Add("@PriceBD", SqlDbType.Decimal).Value = row("PriceBD")
            cmd.Parameters.Add("@DiscountCash", SqlDbType.Decimal).Value = row("DiscountCash")
            cmd.Parameters.Add("@DiscountPString", SqlDbType.NVarChar, 50).Value = row("DiscountPString")
            cmd.Parameters.Add("@DiscountP", SqlDbType.Decimal).Value = row("DiscountP")
            cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = row("Price")
            cmd.Parameters.Add("@SumPrice", SqlDbType.Decimal).Value = row("SumPrice")
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub InsertBuyStock(conn As SqlConnection, tr As SqlTransaction, row As DataRow, vatPercent As Decimal)
        Using buyStock As New SqlCommand("INSERT INTO BuyStock (BranchNumber,BuyNumber,PN,Number,BuyPrice,BuyPercentVat) VALUES (@BranchNumber,@BuyNumber,@PN,@Number,@BuyPrice,@Vat)", conn, tr)
            buyStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            buyStock.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = currentBuyNumber
            buyStock.Parameters.Add("@PN", SqlDbType.Int).Value = row("PN")
            buyStock.Parameters.Add("@Number", SqlDbType.Decimal).Value = row("Number")
            buyStock.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = row("Price")
            buyStock.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
            buyStock.ExecuteNonQuery()
        End Using

        Using stock As New SqlCommand("INSERT INTO Stock (BranchNumber,BillType,BillNumber,PN,NumberNS,NumberS,Number,BuyPrice,PercentVat,CanUseTime) VALUES (@BranchNumber,@BillType,@BillNumber,@PN,@Number,0,@Number,@BuyPrice,@Vat,@CanUseTime)", conn, tr)
            stock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            stock.Parameters.Add("@BillType", SqlDbType.Int).Value = BuyBillType
            stock.Parameters.Add("@BillNumber", SqlDbType.Int).Value = currentBuyNumber
            stock.Parameters.Add("@PN", SqlDbType.Int).Value = row("PN")
            stock.Parameters.Add("@Number", SqlDbType.Decimal).Value = row("Number")
            stock.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = row("Price")
            stock.Parameters.Add("@Vat", SqlDbType.Decimal).Value = vatPercent
            stock.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = dtpDate.Value
            stock.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub ShowError(prefix As String, ex As Exception)
        MessageBox.Show(prefix & Environment.NewLine & ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub frmBuy_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not isSaved AndAlso dtItems.Rows.Count > 0 Then
            If MessageBox.Show("ยังไม่ได้บันทึกข้อมูล ต้องการปิดหน้าต่างหรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then e.Cancel = True
        End If
    End Sub


End Class
