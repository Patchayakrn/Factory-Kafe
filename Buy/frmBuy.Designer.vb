Partial Class frmBuy
    Private components As System.ComponentModel.IContainer
    Private grpHeader As GroupBox
    Private grpProduct As GroupBox
    Private grpItems As GroupBox
    Private grpSummary As GroupBox
    Private txtSupplierName As TextBox
    Private WithEvents btnLoadSupplier As Button
    Private txtCredit As TextBox
    Private txtComment As TextBox
    Private WithEvents txtProductKey As TextBox
    Private WithEvents btnFindProduct As Button
    Private txtProductCode As TextBox
    Private txtQuantity As TextBox
    Private WithEvents btnAddItem As Button
    Private WithEvents btnRemoveItem As Button
    Private WithEvents btnSave As Button
    Private WithEvents btnPrint As Button
    Private WithEvents btnCancel As Button

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                components.Dispose()
            Catch ex As ArgumentOutOfRangeException
                'Krypton controls in this legacy form can throw while their
                'internal item collection is being disposed.  Continue with
                'the Form disposal so closing the edit window is safe.
            End Try
        End If

        Try
            MyBase.Dispose(disposing)
        Catch ex As ArgumentOutOfRangeException
            'Known Krypton Toolkit disposal issue; the form is already closing.
        End Try
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBuy))
        Me.grpHeader = New System.Windows.Forms.GroupBox()
        Me.txtMoneyChange = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbTypeMoney = New Krypton.Toolkit.KryptonComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbVatType = New Krypton.Toolkit.KryptonComboBox()
        Me.cbHaveTax = New Krypton.Toolkit.KryptonComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpTaxDate = New Krypton.Toolkit.KryptonDateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtVendorBillNumber = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDate = New Krypton.Toolkit.KryptonDateTimePicker()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnLoadSupplier = New System.Windows.Forms.Button()
        Me.txtCredit = New System.Windows.Forms.TextBox()
        Me.grpProduct = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtProductKey = New System.Windows.Forms.TextBox()
        Me.btnFindProduct = New System.Windows.Forms.Button()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnRemoveItem = New System.Windows.Forms.Button()
        Me.grpItems = New System.Windows.Forms.GroupBox()
        Me.dgvItems = New Krypton.Toolkit.KryptonDataGridView()
        Me.grpSummary = New System.Windows.Forms.GroupBox()
        Me.txtTotalMoney = New DevExpress.XtraEditors.TextEdit()
        Me.lblGrandTotalCaption = New System.Windows.Forms.Label()
        Me.KryptonTextBox8 = New Krypton.Toolkit.KryptonTextBox()
        Me.KryptonTextBox7 = New Krypton.Toolkit.KryptonTextBox()
        Me.KryptonTextBox6 = New Krypton.Toolkit.KryptonTextBox()
        Me.KryptonTextBox5 = New Krypton.Toolkit.KryptonTextBox()
        Me.KryptonTextBox4 = New Krypton.Toolkit.KryptonTextBox()
        Me.KryptonTextBox3 = New Krypton.Toolkit.KryptonTextBox()
        Me.cboCostType = New Krypton.Toolkit.KryptonComboBox()
        Me.txtVat = New Krypton.Toolkit.KryptonTextBox()
        Me.KryptonTextBox2 = New Krypton.Toolkit.KryptonTextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.KryptonTextBox1 = New Krypton.Toolkit.KryptonTextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblBranch = New System.Windows.Forms.Label()
        Me.lbDocNumber = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.grpHeader.SuspendLayout()
        CType(Me.cbTypeMoney, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbVatType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbHaveTax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProduct.SuspendLayout()
        Me.grpItems.SuspendLayout()
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSummary.SuspendLayout()
        CType(Me.txtTotalMoney, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCostType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpHeader
        '
        Me.grpHeader.Controls.Add(Me.txtMoneyChange)
        Me.grpHeader.Controls.Add(Me.Label13)
        Me.grpHeader.Controls.Add(Me.cbTypeMoney)
        Me.grpHeader.Controls.Add(Me.Label12)
        Me.grpHeader.Controls.Add(Me.Label4)
        Me.grpHeader.Controls.Add(Me.cbVatType)
        Me.grpHeader.Controls.Add(Me.cbHaveTax)
        Me.grpHeader.Controls.Add(Me.Label10)
        Me.grpHeader.Controls.Add(Me.Label11)
        Me.grpHeader.Controls.Add(Me.Label7)
        Me.grpHeader.Controls.Add(Me.txtComment)
        Me.grpHeader.Controls.Add(Me.TextBox2)
        Me.grpHeader.Controls.Add(Me.Label14)
        Me.grpHeader.Controls.Add(Me.dtpTaxDate)
        Me.grpHeader.Controls.Add(Me.Label6)
        Me.grpHeader.Controls.Add(Me.Button1)
        Me.grpHeader.Controls.Add(Me.Label5)
        Me.grpHeader.Controls.Add(Me.txtVendorBillNumber)
        Me.grpHeader.Controls.Add(Me.Label3)
        Me.grpHeader.Controls.Add(Me.dtpDate)
        Me.grpHeader.Controls.Add(Me.txtSupplierName)
        Me.grpHeader.Controls.Add(Me.btnLoadSupplier)
        Me.grpHeader.Controls.Add(Me.txtCredit)
        Me.grpHeader.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpHeader.Location = New System.Drawing.Point(12, 52)
        Me.grpHeader.Name = "grpHeader"
        Me.grpHeader.Size = New System.Drawing.Size(1900, 232)
        Me.grpHeader.TabIndex = 0
        Me.grpHeader.TabStop = False
        Me.grpHeader.Text = "ข้อมูลใบซื้อ"
        '
        'txtMoneyChange
        '
        Me.txtMoneyChange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMoneyChange.Enabled = False
        Me.txtMoneyChange.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoneyChange.Location = New System.Drawing.Point(705, 129)
        Me.txtMoneyChange.Name = "txtMoneyChange"
        Me.txtMoneyChange.Size = New System.Drawing.Size(347, 39)
        Me.txtMoneyChange.TabIndex = 88
        Me.txtMoneyChange.Text = "1.0000000000"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(581, 131)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(118, 32)
        Me.Label13.TabIndex = 87
        Me.Label13.Text = "อัตราแลกเปลี่ยน"
        '
        'cbTypeMoney
        '
        Me.cbTypeMoney.DropDownHeight = 300
        Me.cbTypeMoney.IntegralHeight = False
        Me.cbTypeMoney.Items.AddRange(New Object() {"บาท"})
        Me.cbTypeMoney.Location = New System.Drawing.Point(364, 129)
        Me.cbTypeMoney.Name = "cbTypeMoney"
        Me.cbTypeMoney.Size = New System.Drawing.Size(166, 38)
        Me.cbTypeMoney.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTypeMoney.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbTypeMoney.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTypeMoney.TabIndex = 86
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(297, 131)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 32)
        Me.Label12.TabIndex = 85
        Me.Label12.Text = "เงินตรา"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(55, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 32)
        Me.Label4.TabIndex = 84
        Me.Label4.Text = "หมายเหตุ"
        '
        'cbVatType
        '
        Me.cbVatType.DropDownHeight = 300
        Me.cbVatType.IntegralHeight = False
        Me.cbVatType.Items.AddRange(New Object() {"รายการสินค้า ยังไม่รวมภาษี (แยกนอก)", "ราคาสินค้า รวมภาษีแล้ว (รวมใน)"})
        Me.cbVatType.Location = New System.Drawing.Point(705, 79)
        Me.cbVatType.Name = "cbVatType"
        Me.cbVatType.Size = New System.Drawing.Size(347, 38)
        Me.cbVatType.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbVatType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbVatType.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbVatType.TabIndex = 83
        Me.cbVatType.Text = "รายการสินค้า ยังไม่รวมภาษี (แยกนอก)"
        '
        'cbHaveTax
        '
        Me.cbHaveTax.DropDownHeight = 300
        Me.cbHaveTax.IntegralHeight = False
        Me.cbHaveTax.Items.AddRange(New Object() {"ยกเว้นภาษี", "คิดภาษี"})
        Me.cbHaveTax.Location = New System.Drawing.Point(1241, 72)
        Me.cbHaveTax.Name = "cbHaveTax"
        Me.cbHaveTax.Size = New System.Drawing.Size(265, 38)
        Me.cbHaveTax.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHaveTax.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbHaveTax.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbHaveTax.TabIndex = 82
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(629, 84)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 32)
        Me.Label10.TabIndex = 81
        Me.Label10.Text = "Vat Type"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1139, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 32)
        Me.Label11.TabIndex = 80
        Me.Label11.Text = "Tax (ภาษี)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(75, 131)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 32)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "เครดิต"
        '
        'txtComment
        '
        Me.txtComment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComment.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.Location = New System.Drawing.Point(146, 179)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(1360, 45)
        Me.txtComment.TabIndex = 16
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(1241, 31)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(265, 39)
        Me.TextBox2.TabIndex = 77
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(1152, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 32)
        Me.Label14.TabIndex = 75
        Me.Label14.Text = "Doc No."
        '
        'dtpTaxDate
        '
        Me.dtpTaxDate.CustomFormat = "MM/yyyy"
        Me.dtpTaxDate.CustomNullText = "MM/yyyy"
        Me.dtpTaxDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTaxDate.Location = New System.Drawing.Point(1241, 131)
        Me.dtpTaxDate.Name = "dtpTaxDate"
        Me.dtpTaxDate.PaletteMode = Krypton.Toolkit.PaletteMode.Office2010Blue
        Me.dtpTaxDate.Size = New System.Drawing.Size(265, 37)
        Me.dtpTaxDate.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTaxDate.TabIndex = 73
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1116, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 32)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "ด / ปีที่ยื่นภาษี"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(457, 81)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(76, 43)
        Me.Button1.TabIndex = 71
        Me.Button1.Text = "ตกลง"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(22, 84)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 32)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "จากใบสั่งซื้อ ID"
        '
        'txtVendorBillNumber
        '
        Me.txtVendorBillNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVendorBillNumber.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVendorBillNumber.Location = New System.Drawing.Point(146, 84)
        Me.txtVendorBillNumber.Name = "txtVendorBillNumber"
        Me.txtVendorBillNumber.Size = New System.Drawing.Size(265, 39)
        Me.txtVendorBillNumber.TabIndex = 69
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(639, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 32)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "วันที่ซื้อ"
        '
        'dtpDate
        '
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(705, 36)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.PaletteMode = Krypton.Toolkit.PaletteMode.Office2010Blue
        Me.dtpDate.Size = New System.Drawing.Size(347, 37)
        Me.dtpDate.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.TabIndex = 67
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Info
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Location = New System.Drawing.Point(146, 35)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.ReadOnly = True
        Me.txtSupplierName.Size = New System.Drawing.Size(387, 39)
        Me.txtSupplierName.TabIndex = 8
        '
        'btnLoadSupplier
        '
        Me.btnLoadSupplier.BackColor = System.Drawing.Color.Lavender
        Me.btnLoadSupplier.Location = New System.Drawing.Point(7, 32)
        Me.btnLoadSupplier.Name = "btnLoadSupplier"
        Me.btnLoadSupplier.Size = New System.Drawing.Size(124, 41)
        Me.btnLoadSupplier.TabIndex = 9
        Me.btnLoadSupplier.Text = "Supplier"
        Me.btnLoadSupplier.UseVisualStyleBackColor = False
        '
        'txtCredit
        '
        Me.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCredit.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredit.Location = New System.Drawing.Point(146, 129)
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.Size = New System.Drawing.Size(90, 39)
        Me.txtCredit.TabIndex = 13
        '
        'grpProduct
        '
        Me.grpProduct.Controls.Add(Me.Label16)
        Me.grpProduct.Controls.Add(Me.Label15)
        Me.grpProduct.Controls.Add(Me.txtProductKey)
        Me.grpProduct.Controls.Add(Me.btnFindProduct)
        Me.grpProduct.Controls.Add(Me.txtQuantity)
        Me.grpProduct.Controls.Add(Me.btnAddItem)
        Me.grpProduct.Controls.Add(Me.btnRemoveItem)
        Me.grpProduct.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpProduct.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpProduct.Location = New System.Drawing.Point(3, 35)
        Me.grpProduct.Name = "grpProduct"
        Me.grpProduct.Size = New System.Drawing.Size(1453, 96)
        Me.grpProduct.TabIndex = 1
        Me.grpProduct.TabStop = False
        Me.grpProduct.Text = "เพิ่มสินค้า"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(561, 35)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 39)
        Me.Label16.TabIndex = 86
        Me.Label16.Text = "จำนวน"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(57, 32)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 39)
        Me.Label15.TabIndex = 85
        Me.Label15.Text = "สินค้า"
        '
        'txtProductKey
        '
        Me.txtProductKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductKey.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductKey.Location = New System.Drawing.Point(125, 30)
        Me.txtProductKey.Name = "txtProductKey"
        Me.txtProductKey.Size = New System.Drawing.Size(305, 45)
        Me.txtProductKey.TabIndex = 8
        '
        'btnFindProduct
        '
        Me.btnFindProduct.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnFindProduct.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindProduct.Location = New System.Drawing.Point(436, 29)
        Me.btnFindProduct.Name = "btnFindProduct"
        Me.btnFindProduct.Size = New System.Drawing.Size(91, 46)
        Me.btnFindProduct.TabIndex = 9
        Me.btnFindProduct.Text = "ค้นหา"
        Me.btnFindProduct.UseVisualStyleBackColor = False
        '
        'txtQuantity
        '
        Me.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuantity.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.Location = New System.Drawing.Point(657, 30)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(75, 45)
        Me.txtQuantity.TabIndex = 12
        Me.txtQuantity.Text = "1"
        Me.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnAddItem
        '
        Me.btnAddItem.BackColor = System.Drawing.Color.MediumSpringGreen
        Me.btnAddItem.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddItem.Location = New System.Drawing.Point(770, 30)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(132, 45)
        Me.btnAddItem.TabIndex = 17
        Me.btnAddItem.Text = "เพิ่มรายการ"
        Me.btnAddItem.UseVisualStyleBackColor = False
        '
        'btnRemoveItem
        '
        Me.btnRemoveItem.BackColor = System.Drawing.Color.LightCoral
        Me.btnRemoveItem.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveItem.Location = New System.Drawing.Point(908, 30)
        Me.btnRemoveItem.Name = "btnRemoveItem"
        Me.btnRemoveItem.Size = New System.Drawing.Size(129, 45)
        Me.btnRemoveItem.TabIndex = 18
        Me.btnRemoveItem.Text = "ลบรายการ"
        Me.btnRemoveItem.UseVisualStyleBackColor = False
        '
        'grpItems
        '
        Me.grpItems.Controls.Add(Me.dgvItems)
        Me.grpItems.Controls.Add(Me.grpProduct)
        Me.grpItems.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpItems.Location = New System.Drawing.Point(12, 290)
        Me.grpItems.Name = "grpItems"
        Me.grpItems.Size = New System.Drawing.Size(1459, 545)
        Me.grpItems.TabIndex = 2
        Me.grpItems.TabStop = False
        Me.grpItems.Text = "รายการสินค้า"
        '
        'dgvItems
        '
        Me.dgvItems.AllowUserToAddRows = False
        Me.dgvItems.AllowUserToDeleteRows = False
        Me.dgvItems.AllowUserToResizeRows = False
        Me.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvItems.ColumnHeadersHeight = 48
        Me.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItems.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Sheet
        Me.dgvItems.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dgvItems.GridStyles.StyleColumn = Krypton.Toolkit.GridStyle.Sheet
        Me.dgvItems.GridStyles.StyleDataCells = Krypton.Toolkit.GridStyle.Sheet
        Me.dgvItems.GridStyles.StyleRow = Krypton.Toolkit.GridStyle.Sheet
        Me.dgvItems.Location = New System.Drawing.Point(3, 131)
        Me.dgvItems.MultiSelect = False
        Me.dgvItems.Name = "dgvItems"
        Me.dgvItems.RowHeadersVisible = False
        Me.dgvItems.RowHeadersWidth = 51
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        Me.dgvItems.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvItems.RowTemplate.Height = 36
        Me.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvItems.Size = New System.Drawing.Size(1453, 411)
        Me.dgvItems.StateCommon.Background.Color1 = System.Drawing.Color.White
        Me.dgvItems.StateCommon.Background.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid
        Me.dgvItems.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dgvItems.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.White
        Me.dgvItems.StateCommon.HeaderColumn.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvItems.StateCommon.HeaderRow.Back.Color1 = System.Drawing.Color.White
        Me.dgvItems.StateCommon.HeaderRow.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvItems.TabIndex = 49
        '
        'grpSummary
        '
        Me.grpSummary.Controls.Add(Me.txtTotalMoney)
        Me.grpSummary.Controls.Add(Me.lblGrandTotalCaption)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox8)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox7)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox6)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox5)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox4)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox3)
        Me.grpSummary.Controls.Add(Me.cboCostType)
        Me.grpSummary.Controls.Add(Me.txtVat)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox2)
        Me.grpSummary.Controls.Add(Me.Label20)
        Me.grpSummary.Controls.Add(Me.Label21)
        Me.grpSummary.Controls.Add(Me.Label19)
        Me.grpSummary.Controls.Add(Me.Label23)
        Me.grpSummary.Controls.Add(Me.KryptonTextBox1)
        Me.grpSummary.Controls.Add(Me.Label24)
        Me.grpSummary.Controls.Add(Me.Label25)
        Me.grpSummary.Controls.Add(Me.Label18)
        Me.grpSummary.Controls.Add(Me.Label17)
        Me.grpSummary.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSummary.Location = New System.Drawing.Point(1489, 354)
        Me.grpSummary.Name = "grpSummary"
        Me.grpSummary.Size = New System.Drawing.Size(423, 481)
        Me.grpSummary.TabIndex = 3
        Me.grpSummary.TabStop = False
        Me.grpSummary.Text = "สรุปยอด"
        '
        'txtTotalMoney
        '
        Me.txtTotalMoney.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.txtTotalMoney.EditValue = "0.00"
        Me.txtTotalMoney.EnterMoveNextControl = False
        Me.txtTotalMoney.Font = New System.Drawing.Font("Cordia New", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalMoney.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalMoney.Location = New System.Drawing.Point(124, 406)
        Me.txtTotalMoney.Multiline = True
        Me.txtTotalMoney.Name = "txtTotalMoney"
        Me.txtTotalMoney.Size = New System.Drawing.Size(293, 62)
        Me.txtTotalMoney.TabIndex = 108
        Me.txtTotalMoney.Text = "0.00"
        '
        'lblGrandTotalCaption
        '
        Me.lblGrandTotalCaption.Font = New System.Drawing.Font("Cordia New", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblGrandTotalCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblGrandTotalCaption.Location = New System.Drawing.Point(18, 425)
        Me.lblGrandTotalCaption.Name = "lblGrandTotalCaption"
        Me.lblGrandTotalCaption.Size = New System.Drawing.Size(117, 38)
        Me.lblGrandTotalCaption.TabIndex = 107
        Me.lblGrandTotalCaption.Text = "รวมทั้งสิ้น"
        '
        'KryptonTextBox8
        '
        Me.KryptonTextBox8.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox8.Location = New System.Drawing.Point(282, 350)
        Me.KryptonTextBox8.Name = "KryptonTextBox8"
        Me.KryptonTextBox8.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox8.Size = New System.Drawing.Size(135, 46)
        Me.KryptonTextBox8.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox8.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox8.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.KryptonTextBox8.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox8.TabIndex = 106
        Me.KryptonTextBox8.Text = "0.00"
        Me.KryptonTextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'KryptonTextBox7
        '
        Me.KryptonTextBox7.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox7.Location = New System.Drawing.Point(282, 298)
        Me.KryptonTextBox7.Name = "KryptonTextBox7"
        Me.KryptonTextBox7.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox7.Size = New System.Drawing.Size(135, 46)
        Me.KryptonTextBox7.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox7.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox7.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.KryptonTextBox7.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox7.TabIndex = 105
        Me.KryptonTextBox7.Text = "0.00"
        Me.KryptonTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'KryptonTextBox6
        '
        Me.KryptonTextBox6.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox6.Location = New System.Drawing.Point(282, 246)
        Me.KryptonTextBox6.Name = "KryptonTextBox6"
        Me.KryptonTextBox6.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox6.Size = New System.Drawing.Size(135, 46)
        Me.KryptonTextBox6.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox6.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox6.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.KryptonTextBox6.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox6.TabIndex = 104
        Me.KryptonTextBox6.Text = "0.00"
        Me.KryptonTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'KryptonTextBox5
        '
        Me.KryptonTextBox5.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox5.Location = New System.Drawing.Point(282, 194)
        Me.KryptonTextBox5.Name = "KryptonTextBox5"
        Me.KryptonTextBox5.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox5.Size = New System.Drawing.Size(135, 46)
        Me.KryptonTextBox5.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox5.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox5.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.KryptonTextBox5.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox5.TabIndex = 103
        Me.KryptonTextBox5.Text = "0.00"
        Me.KryptonTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'KryptonTextBox4
        '
        Me.KryptonTextBox4.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox4.Location = New System.Drawing.Point(282, 90)
        Me.KryptonTextBox4.Name = "KryptonTextBox4"
        Me.KryptonTextBox4.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox4.Size = New System.Drawing.Size(135, 46)
        Me.KryptonTextBox4.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox4.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox4.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.KryptonTextBox4.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox4.TabIndex = 102
        Me.KryptonTextBox4.Text = "0.00"
        Me.KryptonTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'KryptonTextBox3
        '
        Me.KryptonTextBox3.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox3.Location = New System.Drawing.Point(282, 38)
        Me.KryptonTextBox3.Name = "KryptonTextBox3"
        Me.KryptonTextBox3.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox3.Size = New System.Drawing.Size(135, 46)
        Me.KryptonTextBox3.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox3.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox3.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.KryptonTextBox3.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox3.TabIndex = 101
        Me.KryptonTextBox3.Text = "0.00"
        Me.KryptonTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboCostType
        '
        Me.cboCostType.DropDownHeight = 300
        Me.cboCostType.IntegralHeight = False
        Me.cboCostType.Items.AddRange(New Object() {"ต้นทุนแผง (เฉลี่ยตามราคา)", "ต้นทุนแผง (เฉลี่ยตามจำนวน)", "ต้นทุนแผง (เฉลี่ยตามน้ำหนัก)"})
        Me.cboCostType.Location = New System.Drawing.Point(15, 35)
        Me.cboCostType.Name = "cboCostType"
        Me.cboCostType.Size = New System.Drawing.Size(211, 44)
        Me.cboCostType.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCostType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cboCostType.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCostType.TabIndex = 100
        '
        'txtVat
        '
        Me.txtVat.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.txtVat.Location = New System.Drawing.Point(282, 142)
        Me.txtVat.Name = "txtVat"
        Me.txtVat.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.txtVat.Size = New System.Drawing.Size(135, 46)
        Me.txtVat.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtVat.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVat.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Me.txtVat.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVat.TabIndex = 99
        Me.txtVat.Text = "0.00"
        Me.txtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'KryptonTextBox2
        '
        Me.KryptonTextBox2.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox2.Location = New System.Drawing.Point(124, 350)
        Me.KryptonTextBox2.Name = "KryptonTextBox2"
        Me.KryptonTextBox2.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox2.Size = New System.Drawing.Size(76, 46)
        Me.KryptonTextBox2.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox2.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox2.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox2.TabIndex = 98
        Me.KryptonTextBox2.Text = "7.00"
        Me.KryptonTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label20.Location = New System.Drawing.Point(199, 354)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(50, 34)
        Me.Label20.TabIndex = 97
        Me.Label20.Text = "%"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.Label21.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label21.Location = New System.Drawing.Point(19, 350)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(78, 34)
        Me.Label21.TabIndex = 96
        Me.Label21.Text = "Vat"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label19.Location = New System.Drawing.Point(18, 298)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(120, 34)
        Me.Label19.TabIndex = 95
        Me.Label19.Text = "รวมเป็นเงิน"
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label23.Location = New System.Drawing.Point(18, 246)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(186, 34)
        Me.Label23.TabIndex = 94
        Me.Label23.Text = "ส่วนลดเงินสด"
        '
        'KryptonTextBox1
        '
        Me.KryptonTextBox1.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.KryptonTextBox1.Location = New System.Drawing.Point(124, 194)
        Me.KryptonTextBox1.Name = "KryptonTextBox1"
        Me.KryptonTextBox1.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.KryptonTextBox1.Size = New System.Drawing.Size(76, 46)
        Me.KryptonTextBox1.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KryptonTextBox1.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox1.StateNormal.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonTextBox1.TabIndex = 93
        Me.KryptonTextBox1.Text = "0.00"
        Me.KryptonTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label24.Location = New System.Drawing.Point(199, 197)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(50, 34)
        Me.Label24.TabIndex = 92
        Me.Label24.Text = "%"
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label25.Location = New System.Drawing.Point(18, 194)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(120, 34)
        Me.Label25.TabIndex = 91
        Me.Label25.Text = "ส่วนลด"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(18, 142)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(99, 39)
        Me.Label18.TabIndex = 90
        Me.Label18.Text = "จำนวนเงิน"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(18, 90)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(241, 39)
        Me.Label17.TabIndex = 89
        Me.Label17.Text = "ส่วนลดแฝง(เฉลี่ยนตามราคา)"
        '
        'txtProductCode
        '
        Me.txtProductCode.Location = New System.Drawing.Point(0, 0)
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.Size = New System.Drawing.Size(100, 22)
        Me.txtProductCode.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.LightGreen
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(1612, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(135, 46)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "    บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.LightBlue
        Me.btnPrint.Enabled = True
        Me.btnPrint.Location = New System.Drawing.Point(1454, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(135, 46)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "ดูรายงานย้อนหลัง"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(1770, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(135, 46)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblBranch)
        Me.Panel1.Controls.Add(Me.lbDocNumber)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1924, 46)
        Me.Panel1.TabIndex = 6
        '
        'lblBranch
        '
        Me.lblBranch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.ForeColor = System.Drawing.Color.White
        Me.lblBranch.Location = New System.Drawing.Point(1231, 13)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(333, 35)
        Me.lblBranch.TabIndex = 25
        '
        'lbDocNumber
        '
        Me.lbDocNumber.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDocNumber.ForeColor = System.Drawing.Color.White
        Me.lbDocNumber.Location = New System.Drawing.Point(1675, 12)
        Me.lbDocNumber.Name = "lbDocNumber"
        Me.lbDocNumber.Size = New System.Drawing.Size(147, 35)
        Me.lbDocNumber.TabIndex = 23
        Me.lbDocNumber.Text = "กำลังสร้าง..."
        Me.lbDocNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1577, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 35)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Doc No. :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cordia New", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(11, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(162, 41)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "ซื้อสินค้า (Buy)"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Controls.Add(Me.btnPrint)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 841)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1924, 57)
        Me.Panel2.TabIndex = 7
        '
        'frmBuy
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1924, 898)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grpHeader)
        Me.Controls.Add(Me.grpItems)
        Me.Controls.Add(Me.grpSummary)
        Me.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1050, 680)
        Me.Name = "frmBuy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buy - ซื้อสินค้า"
        Me.grpHeader.ResumeLayout(False)
        Me.grpHeader.PerformLayout()
        CType(Me.cbTypeMoney, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbVatType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbHaveTax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProduct.ResumeLayout(False)
        Me.grpProduct.PerformLayout()
        Me.grpItems.ResumeLayout(False)
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSummary.ResumeLayout(False)
        Me.grpSummary.PerformLayout()
        CType(Me.txtTotalMoney, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCostType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents lbDocNumber As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpDate As Krypton.Toolkit.KryptonDateTimePicker
    Friend WithEvents lblBranch As Label
    Friend WithEvents Label5 As Label
    Private WithEvents txtVendorBillNumber As TextBox
    Private WithEvents Button1 As Button
    Friend WithEvents dtpTaxDate As Krypton.Toolkit.KryptonDateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label14 As Label
    Private WithEvents TextBox2 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cbVatType As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents cbHaveTax As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents cbTypeMoney As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents Label12 As Label
    Private WithEvents txtMoneyChange As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents dgvItems As Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents KryptonTextBox1 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents txtVat As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents KryptonTextBox2 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents cboCostType As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents KryptonTextBox3 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents KryptonTextBox5 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents KryptonTextBox4 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents KryptonTextBox6 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents KryptonTextBox8 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents KryptonTextBox7 As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents lblGrandTotalCaption As Label
    Friend WithEvents txtTotalMoney As DevExpress.XtraEditors.TextEdit
End Class
