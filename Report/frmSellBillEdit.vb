Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class frmSellBillEdit
    Inherits Form

    Private ReadOnly branchNumber As Integer
    Private ReadOnly sellNumber As Integer
    Private originalBillNumber As String = ""

    Private txtBillNumber As TextBox
    Private dtpBillDate As DateTimePicker
    Private cboVatType As ComboBox
    Private cboPayType As ComboBox
    Private nudDiscount As NumericUpDown
    Private txtComment As TextBox
    Private lblCustomer As Label
    Private lblTotal As Label
    Private dgvItems As DataGridView
    Private items As DataTable
    Private lblItemCount As Label
    Private txtProductSearch As TextBox
    Private btnAddProduct As Button
    Private btnDeleteItem As Button
    Private btnClearItems As Button
    Private btnSave As Button
    Private btnClose As Button
    Private canEditBillItems As Boolean
    Private canDeleteBillItems As Boolean

    Public Sub New()
        Me.New(1, 0)
    End Sub

    Public Sub New(branchNumber As Integer, sellNumber As Integer)
        Me.branchNumber = branchNumber
        Me.sellNumber = sellNumber
        InitializeComponent()
        dgvItems.RowTemplate.Height = 42
        dgvItems.ColumnHeadersHeight = 55
        dgvItems.DefaultCellStyle.Padding = New Padding(0, 4, 0, 4)
        BuildItemColumns()
        AddHandler txtProductSearch.KeyDown, AddressOf txtProductSearch_KeyDown
        AddHandler txtProductSearch.MouseClick, AddressOf txtProductSearch_MouseClick
        AddHandler btnAddProduct.Click, AddressOf btnAddProduct_Click
        AddHandler btnDeleteItem.Click, AddressOf btnDeleteItem_Click
        AddHandler btnClearItems.Click, AddressOf btnClearItems_Click
        AddHandler dgvItems.CellValueChanged, AddressOf dgvItems_CellValueChanged
        AddHandler dgvItems.CurrentCellDirtyStateChanged, AddressOf dgvItems_CurrentCellDirtyStateChanged
        AddHandler nudDiscount.ValueChanged, AddressOf SummaryValueChanged
        AddHandler btnSave.Click, AddressOf btnSave_Click
        AddHandler btnClose.Click, Sub() DialogResult = DialogResult.Cancel
        AddHandler Load, AddressOf frmSellBillEdit_Load
    End Sub

    Private Sub BuildUI()
        Text = "แก้ไขบิลขาย"
        StartPosition = FormStartPosition.CenterParent
        MinimumSize = New Size(980, 640)
        Size = New Size(1100, 720)
        Font = New Font("Cordia New", 16.0!, FontStyle.Regular)

        Dim top As New Panel With {.Dock = DockStyle.Top, .Height = 148, .Padding = New Padding(16), .BackColor = Color.White}
        Controls.Add(top)

        Dim title As New Label With {
            .Text = "แก้ไขบิลขาย",
            .Font = New Font("Cordia New", 16.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(42, 60, 84),
            .AutoSize = True,
            .Location = New Point(16, 12)
        }
        top.Controls.Add(title)

        lblCustomer = New Label With {.AutoSize = True, .Location = New Point(18, 48), .ForeColor = Color.FromArgb(75, 85, 99)}
        top.Controls.Add(lblCustomer)

        AddLabel(top, "เลขที่บิล", 18, 78)
        txtBillNumber = New TextBox With {.Location = New Point(84, 75), .Width = 150}
        top.Controls.Add(txtBillNumber)

        AddLabel(top, "วันที่", 252, 78)
        dtpBillDate = New DateTimePicker With {.Location = New Point(300, 75), .Width = 170, .Format = DateTimePickerFormat.Custom, .CustomFormat = "dd/MM/yyyy HH:mm"}
        top.Controls.Add(dtpBillDate)

        AddLabel(top, "VAT", 490, 78)
        cboVatType = New ComboBox With {.Location = New Point(532, 75), .Width = 130, .DropDownStyle = ComboBoxStyle.DropDownList}
        cboVatType.Items.AddRange(New Object() {"ไม่มี VAT", "เฉพาะมี VAT"})
        top.Controls.Add(cboVatType)

        AddLabel(top, "ชำระ", 682, 78)
        cboPayType = New ComboBox With {.Location = New Point(732, 75), .Width = 150, .DropDownStyle = ComboBoxStyle.DropDownList}
        cboPayType.Items.AddRange(New Object() {"เงินสด", "โอน/QR"})
        top.Controls.Add(cboPayType)

        AddLabel(top, "ส่วนลดท้ายบิล", 18, 112)
        nudDiscount = New NumericUpDown With {.Location = New Point(124, 109), .Width = 110, .DecimalPlaces = 2, .Maximum = 10000000D, .ThousandsSeparator = True}
        top.Controls.Add(nudDiscount)

        AddLabel(top, "หมายเหตุ", 252, 112)
        txtComment = New TextBox With {.Location = New Point(324, 109), .Width = 558}
        top.Controls.Add(txtComment)

        Dim itemPanel As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(16, 12, 16, 8), .BackColor = Color.White}
        Dim itemLayout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 1, .RowCount = 3, .BackColor = Color.White}
        itemLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 46.0!))
        itemLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 38.0!))
        itemLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        itemPanel.Controls.Add(itemLayout)

        Dim productPanel As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.White}
        itemLayout.Controls.Add(productPanel, 0, 0)

        Dim productLabel As New Label With {
            .Text = "Product (รายการสินค้า)",
            .AutoSize = True,
            .Font = New Font("SCordia New", 16.0!, FontStyle.Regular),
            .ForeColor = Color.FromArgb(31, 41, 55),
            .Location = New Point(0, 12)
        }
        productPanel.Controls.Add(productLabel)

        txtProductSearch = New TextBox With {
            .Location = New Point(185, 8),
            .Width = 430,
            .Font = New Font("Cordia New", 16.0!, FontStyle.Regular)
        }
        productPanel.Controls.Add(txtProductSearch)

        btnAddProduct = New Button With {
            .Text = "เพิ่ม",
            .Location = New Point(625, 6),
            .Size = New Size(84, 32),
            .BackColor = Color.FromArgb(37, 99, 235),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        btnAddProduct.FlatAppearance.BorderSize = 0
        productPanel.Controls.Add(btnAddProduct)

        btnDeleteItem = New Button With {
            .Text = "ลบรายการ",
            .Location = New Point(719, 6),
            .Size = New Size(95, 32),
            .BackColor = Color.FromArgb(239, 68, 68),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        btnDeleteItem.FlatAppearance.BorderSize = 0
        productPanel.Controls.Add(btnDeleteItem)

        btnClearItems = New Button With {
            .Text = "ล้างทั้งหมด",
            .Location = New Point(824, 6),
            .Size = New Size(105, 32),
            .BackColor = Color.FromArgb(148, 82, 78),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        btnClearItems.FlatAppearance.BorderSize = 0
        productPanel.Controls.Add(btnClearItems)

        Dim itemHeader As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.White}
        itemLayout.Controls.Add(itemHeader, 0, 1)

        Dim itemTitle As New Label With {
            .Text = "รายละเอียดสินค้าที่ซื้อ",
            .AutoSize = True,
            .Font = New Font("Cordia New", 16.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(42, 60, 84),
            .Location = New Point(0, 7)
        }
        itemHeader.Controls.Add(itemTitle)

        lblItemCount = New Label With {
            .AutoSize = True,
            .Font = New Font("Cordia New", 16.0!, FontStyle.Regular),
            .ForeColor = Color.FromArgb(75, 85, 99),
            .Location = New Point(190, 10)
        }
        itemHeader.Controls.Add(lblItemCount)

        dgvItems = New DataGridView With {
            .Dock = DockStyle.Fill,
            .AutoGenerateColumns = False,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            .ColumnHeadersHeight = 45,
            .RowHeadersVisible = False,
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
            .ScrollBars = ScrollBars.Both
        }
        dgvItems.EnableHeadersVisualStyles = False
        dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 250)
        dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55)
        dgvItems.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        dgvItems.DefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Regular)
        dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254)
        dgvItems.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 24, 39)
        itemLayout.Controls.Add(dgvItems, 0, 2)
        BuildItemColumns()
        AddHandler txtProductSearch.KeyDown, AddressOf txtProductSearch_KeyDown
        AddHandler btnAddProduct.Click, AddressOf btnAddProduct_Click
        AddHandler btnDeleteItem.Click, AddressOf btnDeleteItem_Click
        AddHandler btnClearItems.Click, AddressOf btnClearItems_Click
        AddHandler dgvItems.CellValueChanged, AddressOf dgvItems_CellValueChanged
        AddHandler dgvItems.CurrentCellDirtyStateChanged, AddressOf dgvItems_CurrentCellDirtyStateChanged
        AddHandler nudDiscount.ValueChanged, AddressOf SummaryValueChanged

        Dim bottom As New Panel With {.Dock = DockStyle.Bottom, .Height = 64, .Padding = New Padding(16), .BackColor = Color.White}
        Controls.Add(bottom)

        lblTotal = New Label With {.AutoSize = True, .Location = New Point(16, 22), .Font = New Font("Segoe UI", 12.0!, FontStyle.Bold), .ForeColor = Color.FromArgb(42, 60, 84)}
        bottom.Controls.Add(lblTotal)

        Dim btnSave As New Button With {.Text = "บันทึก", .Width = 110, .Height = 38, .Anchor = AnchorStyles.Top Or AnchorStyles.Right, .Location = New Point(bottom.Width - 246, 13), .BackColor = Color.FromArgb(20, 184, 166), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler btnSave.Click, AddressOf btnSave_Click
        bottom.Controls.Add(btnSave)

        Dim btnClose As New Button With {.Text = "ปิด", .Width = 110, .Height = 38, .Anchor = AnchorStyles.Top Or AnchorStyles.Right, .Location = New Point(bottom.Width - 126, 13), .BackColor = Color.FromArgb(239, 68, 68), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler btnClose.Click, Sub() DialogResult = DialogResult.Cancel
        bottom.Controls.Add(btnClose)

        AddHandler bottom.Resize, Sub()
                                      btnClose.Location = New Point(bottom.ClientSize.Width - 126, 13)
                                      btnSave.Location = New Point(bottom.ClientSize.Width - 246, 13)
                                  End Sub
        Controls.Add(itemPanel)
        Controls.SetChildIndex(itemPanel, 0)
        AddHandler Load, AddressOf frmSellBillEdit_Load
    End Sub

    Private Sub AddLabel(parent As Control, text As String, x As Integer, y As Integer)
        parent.Controls.Add(New Label With {.Text = text, .AutoSize = True, .Location = New Point(x, y + 4), .ForeColor = Color.FromArgb(55, 65, 81)})
    End Sub

    Private Sub BuildItemColumns()
        dgvItems.Columns.Add(TextCol("Item", "No", 46, True, ""))
        dgvItems.Columns.Add(TextCol("ProductCode", "รหัส", 110, True, ""))
        dgvItems.Columns.Add(TextCol("Barcode", "บาร์โค้ด", 130, True, ""))
        dgvItems.Columns.Add(TextCol("ProductName", "สินค้า", 260, True, ""))
        dgvItems.Columns.Add(TextCol("Qty", "จำนวน", 90, True, "N2"))
        dgvItems.Columns.Add(TextCol("LUnit", "หน่วย", 70, True, ""))
        dgvItems.Columns.Add(TextCol("UnitPrice", "ราคา", 105, False, "N2"))
        dgvItems.Columns.Add(TextCol("DiscountCash", "ลดบาท", 95, False, "N2"))
        dgvItems.Columns.Add(TextCol("DiscountP", "ลด %", 80, False, "N2"))
        dgvItems.Columns.Add(TextCol("SumPrice", "รวม", 110, True, "N2"))
        dgvItems.Columns.Add(CheckCol("VatStatus", "VAT", 58, canEditBillItems))
        dgvItems.Columns.Add(TextCol("Comment", "หมายเหตุ", 180, False, ""))
    End Sub

    Private Function TextCol(name As String, headerText As String, width As Integer, isReadOnly As Boolean, format As String) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn With {.Name = name, .DataPropertyName = name, .HeaderText = headerText, .Width = width, .ReadOnly = isReadOnly}
        If format <> "" Then col.DefaultCellStyle.Format = format
        If format <> "" Then col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Return col
    End Function

    Private Function CheckCol(name As String, headerText As String, width As Integer, canEdit As Boolean) As DataGridViewCheckBoxColumn
        Return New DataGridViewCheckBoxColumn With {
            .Name = name,
            .DataPropertyName = name,
            .HeaderText = headerText,
            .Width = width,
            .ReadOnly = Not canEdit,
            .TrueValue = 1,
            .FalseValue = 0
        }
    End Function

    Private Sub frmSellBillEdit_Load(sender As Object, e As EventArgs)
        If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime OrElse Me.DesignMode Then Return
        canEditBillItems = CanCurrentUserEditBillItems()
        canDeleteBillItems = CanCurrentUserDeleteBillItems()
        ApplyPermissionState()
        LoadBill()
    End Sub

    Private Sub ApplyPermissionState()
        txtProductSearch.Enabled = canEditBillItems
        btnAddProduct.Enabled = canEditBillItems
        btnDeleteItem.Enabled = canDeleteBillItems
        btnClearItems.Enabled = canDeleteBillItems
        'Do not allow changing payment/header data when the user cannot edit the bill.
        btnSave.Enabled = canEditBillItems
        cboPayType.Enabled = canEditBillItems
        If Not canEditBillItems Then
            txtProductSearch.Text = "ไม่มีสิทธิ์แก้ไขรายการสินค้าในบิล"
        End If
    End Sub

    Private Function CanCurrentUserEditBillItems() As Boolean
        If ModSession.LoginOfficerID <= 0 Then Return False
        Return PermissionService.HasPermission("Sell.Edit")
    End Function

    Private Function CanCurrentUserDeleteBillItems() As Boolean
        If ModSession.LoginOfficerID <= 0 Then Return False
        Return PermissionService.HasPermission("Sell.DeleteItem")
    End Function

    Private Function OfficerColumnExists(conn As SqlConnection, columnName As String) As Boolean
        Using cmd As New SqlCommand("SELECT COUNT(1) FROM sys.columns WHERE object_id = OBJECT_ID('Officer') AND name = @ColumnName", conn)
            cmd.Parameters.Add("@ColumnName", SqlDbType.NVarChar, 128).Value = columnName
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Function OfficerFlagIsOn(conn As SqlConnection, columnName As String) As Boolean
        Dim safeColumn As String = "[" & columnName.Replace("]", "]]") & "]"
        Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(CONVERT(int, " & safeColumn & "), 0) FROM Officer WHERE OfficerNumber = @OfficerNumber", conn)
            cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = CInt(ModSession.LoginOfficerID)
            Dim result = cmd.ExecuteScalar()
            Return result IsNot Nothing AndAlso result IsNot DBNull.Value AndAlso Convert.ToInt32(result) = 1
        End Using
    End Function

    Private Sub LoadBill()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT S.BillNumber, S.DateTime, S.VatType, S.PayType, ISNULL(S.DCCash, 0) AS DCCash, ISNULL(S.Comment, '') AS Comment, RTRIM(C.Name) AS CustomerName FROM Sell S LEFT JOIN Customer C ON S.CustomerNumber = C.CustomerNumber WHERE S.BranchNumber = @BranchNumber AND S.SellNumber = @SellNumber AND S.Status IS NULL", conn)
                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                Using rd = cmd.ExecuteReader()
                    If Not rd.Read() Then Throw New Exception("ไม่พบบิลนี้ หรือบิลถูกยกเลิกแล้ว")
                    txtBillNumber.Text = Convert.ToString(rd("BillNumber")).Trim()
                    originalBillNumber = txtBillNumber.Text
                    dtpBillDate.Value = Convert.ToDateTime(rd("DateTime"))
                    cboVatType.SelectedIndex = If(Convert.ToInt32(rd("VatType")) = 1, 1, 0)
                    cboPayType.SelectedIndex = If(Convert.ToInt32(rd("PayType")) = 4, 1, 0)
                    nudDiscount.Value = Math.Min(nudDiscount.Maximum, Math.Max(nudDiscount.Minimum, Convert.ToDecimal(rd("DCCash"))))
                    txtComment.Text = Convert.ToString(rd("Comment")).Trim()
                    lblCustomer.Text = "Sell ID " & sellNumber.ToString() & " | ลูกค้า: " & Convert.ToString(rd("CustomerName")).Trim()
                End Using
            End Using

            Using cmd As New SqlCommand("SELECT SD.Item, SD.SItem, SD.PN, RTRIM(P.ProductCode) AS ProductCode, RTRIM(P.Barcode) AS Barcode, RTRIM(P.Name) AS ProductName, ISNULL(SD.Number, 0) AS Qty, RTRIM(SD.LUnit) AS LUnit, CASE WHEN ISNULL(SD.Price, 0) <> 0 THEN SD.Price ELSE ISNULL(SD.PriceBD, 0) END AS UnitPrice, ISNULL(SD.DiscountCash, 0) AS DiscountCash, ISNULL(SD.DiscountP, 0) AS DiscountP, ISNULL(SD.SumPrice, 0) AS SumPrice, CONVERT(bit, ISNULL(SD.VatStatus, 1)) AS VatStatus, RTRIM(ISNULL(SD.Comment, '')) AS Comment FROM SellD SD LEFT JOIN Product P ON P.PN = SD.PN WHERE SD.BranchNumber = @BranchNumber AND SD.SellNumber = @SellNumber ORDER BY SD.Item, SD.SItem", conn)
                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                Using da As New SqlDataAdapter(cmd)
                    items = New DataTable()
                    da.Fill(items)
                End Using
            End Using
        End Using

        If Not items.Columns.Contains("IsNew") Then
            Dim col = items.Columns.Add("IsNew", GetType(Boolean))
            col.DefaultValue = False
        End If

        dgvItems.DataSource = Nothing
        dgvItems.DataSource = items
        If lblItemCount IsNot Nothing Then lblItemCount.Text = "(" & items.Rows.Count.ToString("N0") & " รายการ)"
        dgvItems.Refresh()

        'ถ้ามีส่วนลดอยู่ในระดับรายการ ให้ใช้ยอดส่วนลดจากรายการแทนส่วนลดหัวบิลเดิม
        'ป้องกันกรณีแก้โปรโมชั่นจาก 18 บาททั้งบิล เหลือ 6 บาทเฉพาะสินค้า แต่ DCCash ยังเป็น 18
        Dim itemDiscount As Decimal = LineDiscountTotal()
        If itemDiscount > 0D Then
            nudDiscount.Value = Math.Min(nudDiscount.Maximum, Math.Max(nudDiscount.Minimum, itemDiscount))
        End If

        RecalculateAllLines()
        items.AcceptChanges()
    End Sub

    Private Sub txtProductSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            AddProductFromSearch()
        End If
    End Sub

    Private Sub txtProductSearch_MouseClick(sender As Object, e As MouseEventArgs)
        If Not EnsureCanEditBillItems() Then Exit Sub

        Using productForm As New fProduct()
            productForm.IsSelectMode = True
            productForm.AllowOutOfStockSelection = True
            productForm.SearchText = txtProductSearch.Text.Trim()

            If productForm.ShowDialog(Me) <> DialogResult.OK Then Exit Sub

            AddSelectedProduct(productForm)
        End Using
    End Sub

    Private Sub AddSelectedProduct(productForm As fProduct)
        If productForm Is Nothing OrElse productForm.SelectedPN <= 0 OrElse items Is Nothing Then Exit Sub

        Dim row As DataRow = items.NewRow()
        row("Item") = GetNextItemNumber()
        row("SItem") = 0
        row("PN") = Convert.ToInt32(productForm.SelectedPN)
        row("ProductCode") = If(productForm.SelectedProductCode, "").Trim()
        row("Barcode") = If(productForm.SelectedBarcode, "").Trim()
        row("ProductName") = If(productForm.SelectedProductName, "").Trim()
        row("Qty") = 1D
        row("LUnit") = If(productForm.SelectedUnit, "").Trim()
        row("UnitPrice") = productForm.SelectedSellPrice
        row("DiscountCash") = 0D
        row("DiscountP") = 0D
        row("SumPrice") = productForm.SelectedSellPrice
        row("Comment") = If(productForm.SelectedComment, "").Trim()
        row("VatStatus") = True
        row("IsNew") = True
        items.Rows.Add(row)

        txtProductSearch.Clear()
        txtProductSearch.Focus()
        RecalculateAllLines()
    End Sub

    Private Sub btnAddProduct_Click(sender As Object, e As EventArgs)
        AddProductFromSearch()
    End Sub

    Private Sub AddProductFromSearch()
        If Not EnsureCanEditBillItems() Then Exit Sub
        Dim keyword As String = txtProductSearch.Text.Trim()
        If keyword = "" Then
            txtProductSearch.Focus()
            Exit Sub
        End If

        If items Is Nothing Then Exit Sub

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT TOP 1 PN, RTRIM(ProductCode) AS ProductCode, RTRIM(Barcode) AS Barcode, " &
                    "RTRIM(Name) AS ProductName, RTRIM(ISNULL(SUnit, '')) AS SUnit, ISNULL(Price1, 0) AS Price1 " &
                    "FROM Product " &
                    "WHERE Product.Status IS NULL AND (" &
                    "Barcode = @Keyword OR ProductCode = @Keyword OR Name LIKE @LikeKeyword) " &
                    "ORDER BY CASE WHEN Barcode = @Keyword THEN 0 WHEN ProductCode = @Keyword THEN 1 ELSE 2 END, Name"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = keyword
                    cmd.Parameters.Add("@LikeKeyword", SqlDbType.NVarChar, 220).Value = "%" & keyword & "%"

                    Using rd = cmd.ExecuteReader()
                        If Not rd.Read() Then
                            MessageBox.Show("ไม่พบสินค้านี้", "รายการสินค้า", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtProductSearch.SelectAll()
                            txtProductSearch.Focus()
                            Exit Sub
                        End If

                        Dim row = items.NewRow()
                        row("Item") = GetNextItemNumber()
                        row("SItem") = 0
                        row("PN") = Convert.ToInt32(rd("PN"))
                        row("ProductCode") = Convert.ToString(rd("ProductCode")).Trim()
                        row("Barcode") = Convert.ToString(rd("Barcode")).Trim()
                        row("ProductName") = Convert.ToString(rd("ProductName")).Trim()
                        row("Qty") = 1D
                        row("LUnit") = Convert.ToString(rd("SUnit")).Trim()
                        row("UnitPrice") = Convert.ToDecimal(rd("Price1"))
                        row("DiscountCash") = 0D
                        row("DiscountP") = 0D
                        row("SumPrice") = Convert.ToDecimal(rd("Price1"))
                        row("Comment") = ""
                        row("VatStatus") = True
                        row("IsNew") = True
                        items.Rows.Add(row)
                    End Using
                End Using
            End Using

            txtProductSearch.Clear()
            txtProductSearch.Focus()
            RecalculateAllLines()
        Catch ex As Exception
            MessageBox.Show("เพิ่มสินค้าไม่สำเร็จ: " & ex.Message, "รายการสินค้า", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteItem_Click(sender As Object, e As EventArgs)
        If Not EnsureCanDeleteBillItems() Then Exit Sub
        If dgvItems.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกรายการที่ต้องการลบ", "ลบรายการ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim drv = TryCast(dgvItems.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub

        If MessageBox.Show("ต้องการลบรายการนี้ออกจากบิลใช่หรือไม่?", "ยืนยันลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub
        drv.Row.Delete()
        RecalculateAllLines()
    End Sub

    Private Sub btnClearItems_Click(sender As Object, e As EventArgs)
        If Not EnsureCanDeleteBillItems() Then Exit Sub
        If items Is Nothing OrElse items.Rows.Count = 0 Then Exit Sub

        If MessageBox.Show("ต้องการล้างรายการสินค้าในบิลนี้ทั้งหมด แล้วคีย์ใหม่ใช่หรือไม่?", "ยืนยันล้างรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Exit Sub
        For i As Integer = items.Rows.Count - 1 To 0 Step -1
            items.Rows(i).Delete()
        Next
        RecalculateAllLines()
    End Sub

    Private Function EnsureCanEditBillItems() As Boolean
        If canEditBillItems Then Return True
        MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์ลบ/เพิ่ม/แก้ไขรายการสินค้าในบิล" & Environment.NewLine &
                        "กรุณาเข้าสู่ระบบด้วยผู้จัดการหรือผู้ดูแลระบบ", "สิทธิ์ไม่เพียงพอ",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
    End Function

    Private Function EnsureCanDeleteBillItems() As Boolean
        If canDeleteBillItems Then Return True
        MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์ลบรายการสินค้าในบิล" & Environment.NewLine &
                        "กรุณาเข้าสู่ระบบด้วยผู้จัดการหรือผู้ดูแลระบบ", "สิทธิ์ไม่เพียงพอ",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
    End Function

    Private Function GetNextItemNumber() As Integer
        Dim maxItem As Integer = 0
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            maxItem = Math.Max(maxItem, Convert.ToInt32(row("Item")))
        Next
        Return maxItem + 1
    End Function

    Private Sub dgvItems_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs)
        If dgvItems.IsCurrentCellDirty Then dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub dgvItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If items Is Nothing OrElse e.RowIndex < 0 Then Exit Sub
        RecalculateRow(CType(dgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView).Row)
        UpdateTotalLabel()
    End Sub

    Private Sub SummaryValueChanged(sender As Object, e As EventArgs)
        UpdateTotalLabel()
    End Sub

    Private Sub RecalculateAllLines()
        If items Is Nothing Then Exit Sub
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            RecalculateRow(row)
        Next
        If lblItemCount IsNot Nothing Then lblItemCount.Text = "(" & ActiveItemCount().ToString("N0") & " รายการ)"
        UpdateTotalLabel()
    End Sub

    Private Sub RecalculateRow(row As DataRow)
        Dim qty = ToDecimal(row("Qty"))
        Dim unitPrice = ToDecimal(row("UnitPrice"))
        Dim discountCash = ToDecimal(row("DiscountCash"))
        Dim discountP = ToDecimal(row("DiscountP"))
        Dim gross = qty * unitPrice
        Dim lineDiscount = discountCash + (gross * discountP / 100D)
        row("SumPrice") = Math.Max(0D, gross - lineDiscount)
    End Sub

    Private Function ToDecimal(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
        Dim result As Decimal = 0D
        Decimal.TryParse(value.ToString(), result)
        Return result
    End Function

    Private Function ItemTotal() As Decimal
        If items Is Nothing Then Return 0D
        Dim total As Decimal = 0D
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            total += ToDecimal(row("SumPrice"))
        Next
        Return total
    End Function

    Private Function LineDiscountTotal() As Decimal
        If items Is Nothing Then Return 0D

        Dim grossTotal As Decimal = 0D
        Dim lineTotal As Decimal = 0D
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For

            Dim qty As Decimal = ToDecimal(row("Qty"))
            Dim unitPrice As Decimal = ToDecimal(row("UnitPrice"))
            grossTotal += qty * unitPrice
            lineTotal += ToDecimal(row("SumPrice"))
        Next

        Return Math.Max(0D, grossTotal - lineTotal)
    End Function

    Private Function ActiveItemCount() As Integer
        If items Is Nothing Then Return 0
        Dim count As Integer = 0
        For Each row As DataRow In items.Rows
            If row.RowState <> DataRowState.Deleted Then count += 1
        Next
        Return count
    End Function

    Private Sub UpdateTotalLabel()
        Dim lineNetTotal As Decimal = ItemTotal()
        Dim itemDiscount As Decimal = LineDiscountTotal()
        Dim effectiveDiscount As Decimal = If(itemDiscount > 0D, itemDiscount, nudDiscount.Value)
        Dim net As Decimal = If(itemDiscount > 0D, lineNetTotal, Math.Max(0D, lineNetTotal - nudDiscount.Value))
        Dim vat = If(cboVatType.SelectedIndex = 1, net - (net / 1.07D), 0D)
        lblTotal.Text = "ยอดสินค้า " & lineNetTotal.ToString("N2") & " | ส่วนลด " & effectiveDiscount.ToString("N2") & " | VAT " & vat.ToString("N2") & " | สุทธิ " & net.ToString("N2")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not canEditBillItems Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์แก้ไขบิล" & Environment.NewLine &
                            "กรุณาเข้าสู่ระบบด้วยผู้จัดการหรือผู้ดูแลระบบ", "สิทธิ์ไม่เพียงพอ",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If items Is Nothing OrElse ActiveItemCount() = 0 Then
            MessageBox.Show("กรุณาคีย์รายการสินค้าอย่างน้อย 1 รายการก่อนบันทึก", "แก้ไขบิล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกการแก้ไขบิลนี้ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Exit Sub

        Try
            SaveBill()
            MessageBox.Show("บันทึกการแก้ไขเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DialogResult = DialogResult.OK
        Catch ex As Exception
            MessageBox.Show("บันทึกไม่สำเร็จ: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveBill()
        If Not canEditBillItems Then
            Throw New UnauthorizedAccessException("ผู้ใช้ปัจจุบันไม่มีสิทธิ์แก้ไขบิล")
        End If

        If Not canEditBillItems AndAlso HasItemChanges() Then
            Throw New Exception("ผู้ใช้ปัจจุบันไม่มีสิทธิ์แก้ไขรายการสินค้าในบิล")
        End If

        RecalculateAllLines()

        Dim subtotal = ItemTotal()
        Dim lineDiscount As Decimal = LineDiscountTotal()
        Dim discountToSave As Decimal = If(lineDiscount > 0D, lineDiscount, nudDiscount.Value)
        Dim grossSubtotal As Decimal = subtotal + lineDiscount
        Dim net As Decimal = If(lineDiscount > 0D, subtotal, Math.Max(0D, subtotal - discountToSave))
        Dim vatType = If(cboVatType.SelectedIndex = 1, 1, 0)
        Dim percentVat = If(vatType = 1, 7D, 0D)
        Dim priceBV = If(vatType = 1, Math.Round(net / 1.07D, 2), net)
        Dim priceVat = If(vatType = 1, net - priceBV, 0D)
        Dim totalPrice = net
        Dim payType = If(cboPayType.SelectedIndex = 1, 4, 1)
        Dim cash = If(payType = 1, totalPrice, 0D)

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using tr = conn.BeginTransaction()
                'When only changing payment type, the bill number is unchanged.
                'Skip the duplicate check in that case; the current bill is allowed to keep its own number.
                If Not String.Equals(txtBillNumber.Text.Trim(), originalBillNumber, StringComparison.OrdinalIgnoreCase) Then
                    Using dupCmd As New SqlCommand("SELECT COUNT(1) FROM Sell WHERE BranchNumber = @BranchNumber AND BillNumber = @BillNumber AND SellNumber <> @SellNumber AND Status IS NULL", conn, tr)
                        dupCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        dupCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                        dupCmd.Parameters.Add("@BillNumber", SqlDbType.NVarChar, 50).Value = txtBillNumber.Text.Trim()
                        If Convert.ToInt32(dupCmd.ExecuteScalar()) > 0 Then Throw New Exception("เลขที่บิลซ้ำกับบิลอื่น")
                    End Using
                End If

                If canEditBillItems AndAlso HasItemChanges() Then
                    RestoreStockFromSellStock(conn, tr)
                    'ลบ Stock ติดลบที่สร้างไว้กับรายการเดิมของบิลนี้ก่อนสร้างรายการใหม่
                    'เช่น แก้จากโกโก้ร้อน PN189 เป็นโกโก้ซิกเนเจอร์ PN293
                    RemoveNegativeStockForEditedBill(conn, tr)
                    ExecuteNonQuery(conn, tr, "DELETE FROM SellStock WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber")
                    ExecuteNonQuery(conn, tr, "DELETE FROM SellD WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber")
                    InsertSellDetails(conn, tr)
                    RecreateSellStock(conn, tr, percentVat)
                Else
                    UpdateSellDetailsOnly(conn, tr)
                End If

                Using cmd As New SqlCommand("UPDATE Sell SET BillNumber = @BillNumber, DateTime = @DateTime, VatType = @VatType, PayType = @PayType, Cash = @Cash, Money = @TotalPrice, DCCash = @DCCash, DiscountMoney = @DCCash, PercentVat = @PercentVat, PriceNV = 0, PriceV = @PriceBV, PriceBV = @PriceBV, PriceVat = @PriceVat, TotalPrice = @TotalPrice, PaidMoney = @PaidMoney, Comment = @Comment, EditCount = ISNULL(EditCount, 0) + 1, UTime = GETDATE() WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber AND Status IS NULL", conn, tr)
                    cmd.Parameters.Add("@BillNumber", SqlDbType.NVarChar, 50).Value = txtBillNumber.Text.Trim()
                    cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = dtpBillDate.Value
                    cmd.Parameters.Add("@VatType", SqlDbType.Int).Value = vatType
                    cmd.Parameters.Add("@PayType", SqlDbType.Int).Value = payType
                    cmd.Parameters.Add("@Cash", SqlDbType.Money).Value = cash
                    cmd.Parameters.Add("@DCCash", SqlDbType.Money).Value = discountToSave
                    cmd.Parameters.Add("@PercentVat", SqlDbType.Money).Value = percentVat
                    cmd.Parameters.Add("@PriceBV", SqlDbType.Money).Value = priceBV
                    cmd.Parameters.Add("@PriceVat", SqlDbType.Money).Value = priceVat
                    cmd.Parameters.Add("@TotalPrice", SqlDbType.Money).Value = totalPrice
                    cmd.Parameters.Add("@PaidMoney", SqlDbType.Money).Value = grossSubtotal
                    cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = txtComment.Text.Trim()
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    If cmd.ExecuteNonQuery() = 0 Then Throw New Exception("ไม่พบบิลนี้ หรือบิลถูกยกเลิกแล้ว")
                End Using

                tr.Commit()
            End Using
        End Using
    End Sub

    Private Function HasItemChanges() As Boolean
        If items Is Nothing Then Return False
        Return items.GetChanges() IsNot Nothing
    End Function

    Private Sub ExecuteNonQuery(conn As SqlConnection, tr As SqlTransaction, sql As String)
        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub UpdateSellDetailsOnly(conn As SqlConnection, tr As SqlTransaction)
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            Using cmd As New SqlCommand("UPDATE SellD SET PriceBD = @Price, Price = @Price, DiscountCash = @DiscountCash, DiscountP = @DiscountP, DiscountPString = @DiscountPString, SumPrice = @SumPrice, Comment = @Comment, VatStatus = @VatStatus WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber AND Item = @Item AND SItem = @SItem AND PN = @PN", conn, tr)
                AddSellDParameters(cmd, row)
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Sub InsertSellDetails(conn As SqlConnection, tr As SqlTransaction)
        Dim itemNo As Integer = 1
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            row("Item") = itemNo
            row("SItem") = 0
            RecalculateRow(row)

            Using cmd As New SqlCommand("INSERT INTO SellD (BranchNumber, SellNumber, Type, Item, SItem, PN, Number, LUnit, NumSPerLUnit, PriceBD, DiscountCash, DiscountPString, DiscountP, Price, SumPrice, Comment, VatStatus) VALUES (@BranchNumber, @SellNumber, 0, @Item, @SItem, @PN, @Number, @LUnit, 1, @Price, @DiscountCash, @DiscountPString, @DiscountP, @Price, @SumPrice, @Comment, @VatStatus)", conn, tr)
                AddSellDParameters(cmd, row)
                cmd.ExecuteNonQuery()
            End Using
            itemNo += 1
        Next
    End Sub

    Private Sub AddSellDParameters(cmd As SqlCommand, row As DataRow)
        cmd.Parameters.Add("@Number", SqlDbType.Money).Value = ToDecimal(row("Qty"))
        cmd.Parameters.Add("@LUnit", SqlDbType.NVarChar, 50).Value = Convert.ToString(row("LUnit"))
        cmd.Parameters.Add("@Price", SqlDbType.Money).Value = ToDecimal(row("UnitPrice"))
        cmd.Parameters.Add("@DiscountCash", SqlDbType.Money).Value = ToDecimal(row("DiscountCash"))
        cmd.Parameters.Add("@DiscountP", SqlDbType.Money).Value = ToDecimal(row("DiscountP"))
        cmd.Parameters.Add("@DiscountPString", SqlDbType.NVarChar, 50).Value = ToDecimal(row("DiscountP")).ToString("0.##")
        cmd.Parameters.Add("@SumPrice", SqlDbType.Money).Value = ToDecimal(row("SumPrice"))
        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = Convert.ToString(row("Comment"))
        cmd.Parameters.Add("@VatStatus", SqlDbType.Int).Value = Convert.ToInt32(row("VatStatus"))
        cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
        cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
        cmd.Parameters.Add("@Item", SqlDbType.Int).Value = Convert.ToInt32(row("Item"))
        cmd.Parameters.Add("@SItem", SqlDbType.Int).Value = Convert.ToInt32(row("SItem"))
        cmd.Parameters.Add("@PN", SqlDbType.Int).Value = Convert.ToInt32(row("PN"))
    End Sub

    Private Sub RestoreStockFromSellStock(conn As SqlConnection, tr As SqlTransaction)
        Dim dt As New DataTable()
        Using cmd As New SqlCommand("SELECT BranchNumber, BillType, BillNumber, PN, Number, BuyPrice, BuyPercentVat, CanUseTime FROM SellStock WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            Using da As New SqlDataAdapter(cmd)
                da.SelectCommand.Transaction = tr
                da.Fill(dt)
            End Using
        End Using

        For Each row As DataRow In dt.Rows
            Using updateStock As New SqlCommand("UPDATE Stock SET NumberNS = ISNULL(NumberNS, 0) + @Qty, Number = ISNULL(Number, 0) + @Qty WHERE BranchNumber = @BranchNumber AND BillType = @BillType AND BillNumber = @BillNumber AND PN = @PN", conn, tr)
                updateStock.Parameters.Add("@Qty", SqlDbType.Decimal).Value = Convert.ToDecimal(row("Number"))
                updateStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = Convert.ToInt32(row("BranchNumber"))
                updateStock.Parameters.Add("@BillType", SqlDbType.Int).Value = Convert.ToInt32(row("BillType"))
                updateStock.Parameters.Add("@BillNumber", SqlDbType.Int).Value = Convert.ToInt32(row("BillNumber"))
                updateStock.Parameters.Add("@PN", SqlDbType.Int).Value = Convert.ToInt32(row("PN"))
                If updateStock.ExecuteNonQuery() = 0 Then
                    Using insertStock As New SqlCommand("INSERT INTO Stock (BranchNumber, BillType, BillNumber, PN, NumberNS, NumberS, Number, BuyPrice, PercentVat, CanUseTime) VALUES (@BranchNumber, @BillType, @BillNumber, @PN, @Qty, 0, @Qty, @BuyPrice, @BuyVat, @CanUseTime)", conn, tr)
                        insertStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = Convert.ToInt32(row("BranchNumber"))
                        insertStock.Parameters.Add("@BillType", SqlDbType.Int).Value = Convert.ToInt32(row("BillType"))
                        insertStock.Parameters.Add("@BillNumber", SqlDbType.Int).Value = Convert.ToInt32(row("BillNumber"))
                        insertStock.Parameters.Add("@PN", SqlDbType.Int).Value = Convert.ToInt32(row("PN"))
                        insertStock.Parameters.Add("@Qty", SqlDbType.Decimal).Value = Convert.ToDecimal(row("Number"))
                        insertStock.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(row("BuyPrice"))
                        insertStock.Parameters.Add("@BuyVat", SqlDbType.Decimal).Value = Convert.ToDecimal(row("BuyPercentVat"))
                        insertStock.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = Convert.ToDateTime(row("CanUseTime"))
                        insertStock.ExecuteNonQuery()
                    End Using
                End If
            End Using
        Next
    End Sub

    Private Sub RecreateSellStock(conn As SqlConnection, tr As SqlTransaction, percentVat As Decimal)
        For Each row As DataRow In items.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            Dim pn As Integer = Convert.ToInt32(row("PN"))
            Dim qtyToSell As Decimal = ToDecimal(row("Qty"))
            Dim sellPrice As Decimal = ToDecimal(row("UnitPrice"))

            'เครื่องดื่มไม่มี Stock ยกมาและไม่ใช้ล็อตเก่ามาตัด FIFO
            If IsNonStockProduct(conn, tr, pn) Then
                InsertNegativeStock(conn, tr, pn, qtyToSell, percentVat)
                Continue For
            End If

            While qtyToSell > 0D
                Dim lot As DataRow = GetFirstStockLot(conn, tr, pn)
                If lot Is Nothing Then
                    Throw New Exception("Stock ไม่พอ สำหรับสินค้า PN = " & pn.ToString())
                End If

                Dim stockQty As Decimal = Convert.ToDecimal(lot("Number"))
                Dim useQty As Decimal = If(qtyToSell >= stockQty, stockQty, qtyToSell)
                Dim billType As Integer = Convert.ToInt32(lot("BillType"))
                Dim billNumber As Integer = Convert.ToInt32(lot("BillNumber"))

                Using cmd As New SqlCommand("INSERT INTO SellStock (BranchNumber, SellNumber, BillType, BillNumber, PN, Number, BuyPrice, BuyPercentVat, SellPrice, SellPercentVat, CanUseTime) VALUES (@BranchNumber, @SellNumber, @BillType, @BillNumber, @PN, @Number, @BuyPrice, @BuyPercentVat, @SellPrice, @SellPercentVat, @CanUseTime)", conn, tr)
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                    cmd.Parameters.Add("@BillNumber", SqlDbType.Int).Value = billNumber
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                    cmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = useQty
                    cmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = Convert.ToDecimal(lot("BuyPrice"))
                    cmd.Parameters.Add("@BuyPercentVat", SqlDbType.Decimal).Value = Convert.ToDecimal(lot("PercentVat"))
                    cmd.Parameters.Add("@SellPrice", SqlDbType.Decimal).Value = sellPrice
                    cmd.Parameters.Add("@SellPercentVat", SqlDbType.Decimal).Value = percentVat
                    cmd.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = Convert.ToDateTime(lot("CanUseTime"))
                    cmd.ExecuteNonQuery()
                End Using

                If stockQty <= useQty Then
                    Using cmd As New SqlCommand("DELETE FROM Stock WHERE BranchNumber = @BranchNumber AND BillType = @BillType AND BillNumber = @BillNumber AND PN = @PN", conn, tr)
                        cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                        cmd.Parameters.Add("@BillNumber", SqlDbType.Int).Value = billNumber
                        cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Using cmd As New SqlCommand("UPDATE Stock SET NumberNS = ISNULL(NumberNS, 0) - @Qty, Number = ISNULL(Number, 0) - @Qty WHERE BranchNumber = @BranchNumber AND BillType = @BillType AND BillNumber = @BillNumber AND PN = @PN AND Number >= @Qty", conn, tr)
                        cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = useQty
                        cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        cmd.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                        cmd.Parameters.Add("@BillNumber", SqlDbType.Int).Value = billNumber
                        cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                        cmd.ExecuteNonQuery()
                    End Using
                End If

                qtyToSell -= useQty
            End While
        Next
    End Sub

    Private Sub RemoveNegativeStockForEditedBill(conn As SqlConnection, tr As SqlTransaction)
        Using cmd As New SqlCommand("DELETE FROM Stock WHERE BranchNumber = @BranchNumber AND BillType = 5 AND BillNumber = @SellNumber", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub InsertNegativeStock(conn As SqlConnection, tr As SqlTransaction, pn As Integer, qty As Decimal, percentVat As Decimal)
        If qty <= 0D Then Return

        Using updateCmd As New SqlCommand("UPDATE Stock SET NumberNS = ISNULL(NumberNS, 0) - @Qty, Number = ISNULL(Number, 0) - @Qty WHERE BranchNumber = @BranchNumber AND BillType = 5 AND BillNumber = @SellNumber AND PN = @PN", conn, tr)
            updateCmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = qty
            updateCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            updateCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            updateCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            If updateCmd.ExecuteNonQuery() > 0 Then Return
        End Using

        Using insertCmd As New SqlCommand("INSERT INTO Stock (BranchNumber, BillType, BillNumber, PN, NumberNS, NumberS, Number, BuyPrice, PercentVat, CanUseTime) VALUES (@BranchNumber, 5, @SellNumber, @PN, @NumberNS, 0, @Number, 0, @PercentVat, @CanUseTime)", conn, tr)
            insertCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            insertCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            insertCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            insertCmd.Parameters.Add("@NumberNS", SqlDbType.Decimal).Value = -qty
            insertCmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = -qty
            insertCmd.Parameters.Add("@PercentVat", SqlDbType.Decimal).Value = percentVat
            insertCmd.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = dtpBillDate.Value
            insertCmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function GetFirstStockLot(conn As SqlConnection, tr As SqlTransaction, pn As Integer) As DataRow
        Dim dt As New DataTable()
        Using cmd As New SqlCommand("SELECT TOP 1 BillType, BillNumber, Number, BuyPrice, PercentVat, CanUseTime FROM Stock WITH (UPDLOCK, HOLDLOCK) WHERE BranchNumber = @BranchNumber AND PN = @PN AND Number > 0 ORDER BY CanUseTime ASC", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            Using da As New SqlDataAdapter(cmd)
                da.SelectCommand.Transaction = tr
                da.Fill(dt)
            End Using
        End Using
        If dt.Rows.Count = 0 Then Return Nothing
        Return dt.Rows(0)
    End Function

    Private Function IsNonStockProduct(conn As SqlConnection, tr As SqlTransaction, pn As Integer) As Boolean
        Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(PG.CategoryID, -1) AS CategoryID, ISNULL(RTRIM(P.ProductCode), '') AS ProductCode, ISNULL(RTRIM(C.Name), '') AS CategoryName, ISNULL(RTRIM(PG.Name), '') AS GroupName, ISNULL(RTRIM(P.SUnit), '') AS SUnit, ISNULL(RTRIM(P.Name), '') AS ProductName FROM Product P LEFT JOIN ProductGroup PG ON PG.ID = P.GroupID LEFT JOIN Category C ON C.ID = PG.CategoryID WHERE P.PN = @PN", conn, tr)
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            Using rd = cmd.ExecuteReader()
                If Not rd.Read() Then Return False
                Dim categoryId As Integer = Convert.ToInt32(rd("CategoryID"))
                Dim productCode As String = Convert.ToString(rd("ProductCode")).Trim()
                Dim categoryName As String = Convert.ToString(rd("CategoryName")).Trim()
                Dim groupName As String = Convert.ToString(rd("GroupName")).Trim()
                Dim unitName As String = Convert.ToString(rd("SUnit")).Trim()
                Dim productName As String = Convert.ToString(rd("ProductName")).Trim()
                Return categoryId = 5 OrElse categoryName = "เครื่องดื่ม" OrElse productCode = "C021"
            End Using
        End Using
    End Function

    Private Sub frmSellBillEdit_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
