<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSellBillEdit
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents topPanel As Panel
    Friend WithEvents itemPanel As Panel
    Friend WithEvents itemLayout As TableLayoutPanel
    Friend WithEvents productPanel As Panel
    Friend WithEvents itemHeader As Panel
    Friend WithEvents bottomPanel As Panel
    Friend WithEvents titleLabel As Label
    Friend WithEvents productLabel As Label
    Friend WithEvents itemTitle As Label
    Friend WithEvents lblBillNumberCaption As Label
    Friend WithEvents lblDateCaption As Label
    Friend WithEvents lblVatCaption As Label
    Friend WithEvents lblPayCaption As Label
    Friend WithEvents lblDiscountCaption As Label
    Friend WithEvents lblCommentCaption As Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.topPanel = New System.Windows.Forms.Panel()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lblBillNumberCaption = New System.Windows.Forms.Label()
        Me.txtBillNumber = New System.Windows.Forms.TextBox()
        Me.lblDateCaption = New System.Windows.Forms.Label()
        Me.dtpBillDate = New System.Windows.Forms.DateTimePicker()
        Me.lblVatCaption = New System.Windows.Forms.Label()
        Me.cboVatType = New System.Windows.Forms.ComboBox()
        Me.lblPayCaption = New System.Windows.Forms.Label()
        Me.cboPayType = New System.Windows.Forms.ComboBox()
        Me.lblDiscountCaption = New System.Windows.Forms.Label()
        Me.nudDiscount = New System.Windows.Forms.NumericUpDown()
        Me.lblCommentCaption = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.itemPanel = New System.Windows.Forms.Panel()
        Me.itemLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.productPanel = New System.Windows.Forms.Panel()
        Me.productLabel = New System.Windows.Forms.Label()
        Me.txtProductSearch = New System.Windows.Forms.TextBox()
        Me.btnAddProduct = New System.Windows.Forms.Button()
        Me.btnDeleteItem = New System.Windows.Forms.Button()
        Me.btnClearItems = New System.Windows.Forms.Button()
        Me.itemHeader = New System.Windows.Forms.Panel()
        Me.itemTitle = New System.Windows.Forms.Label()
        Me.lblItemCount = New System.Windows.Forms.Label()
        Me.dgvItems = New System.Windows.Forms.DataGridView()
        Me.bottomPanel = New System.Windows.Forms.Panel()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.topPanel.SuspendLayout()
        CType(Me.nudDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.itemPanel.SuspendLayout()
        Me.itemLayout.SuspendLayout()
        Me.productPanel.SuspendLayout()
        Me.itemHeader.SuspendLayout()
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bottomPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'topPanel
        '
        Me.topPanel.BackColor = System.Drawing.Color.White
        Me.topPanel.Controls.Add(Me.titleLabel)
        Me.topPanel.Controls.Add(Me.lblCustomer)
        Me.topPanel.Controls.Add(Me.lblBillNumberCaption)
        Me.topPanel.Controls.Add(Me.txtBillNumber)
        Me.topPanel.Controls.Add(Me.lblDateCaption)
        Me.topPanel.Controls.Add(Me.dtpBillDate)
        Me.topPanel.Controls.Add(Me.lblVatCaption)
        Me.topPanel.Controls.Add(Me.cboVatType)
        Me.topPanel.Controls.Add(Me.lblPayCaption)
        Me.topPanel.Controls.Add(Me.cboPayType)
        Me.topPanel.Controls.Add(Me.lblDiscountCaption)
        Me.topPanel.Controls.Add(Me.nudDiscount)
        Me.topPanel.Controls.Add(Me.lblCommentCaption)
        Me.topPanel.Controls.Add(Me.txtComment)
        Me.topPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topPanel.Location = New System.Drawing.Point(0, 0)
        Me.topPanel.Name = "topPanel"
        Me.topPanel.Padding = New System.Windows.Forms.Padding(16)
        Me.topPanel.Size = New System.Drawing.Size(1150, 200)
        Me.topPanel.TabIndex = 2
        '
        'titleLabel
        '
        Me.titleLabel.AutoSize = True
        Me.titleLabel.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.titleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.titleLabel.Location = New System.Drawing.Point(16, 12)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(117, 33)
        Me.titleLabel.TabIndex = 0
        Me.titleLabel.Text = "แก้ไขบิลขาย"
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.lblCustomer.Location = New System.Drawing.Point(18, 45)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(0, 34)
        Me.lblCustomer.TabIndex = 1
        '
        'lblBillNumberCaption
        '
        Me.lblBillNumberCaption.AutoSize = True
        Me.lblBillNumberCaption.Location = New System.Drawing.Point(19, 91)
        Me.lblBillNumberCaption.Name = "lblBillNumberCaption"
        Me.lblBillNumberCaption.Size = New System.Drawing.Size(78, 37)
        Me.lblBillNumberCaption.TabIndex = 2
        Me.lblBillNumberCaption.Text = "เลขที่บิล"
        '
        'txtBillNumber
        '
        Me.txtBillNumber.Location = New System.Drawing.Point(103, 84)
        Me.txtBillNumber.Name = "txtBillNumber"
        Me.txtBillNumber.Size = New System.Drawing.Size(170, 44)
        Me.txtBillNumber.TabIndex = 3
        '
        'lblDateCaption
        '
        Me.lblDateCaption.AutoSize = True
        Me.lblDateCaption.Location = New System.Drawing.Point(317, 91)
        Me.lblDateCaption.Name = "lblDateCaption"
        Me.lblDateCaption.Size = New System.Drawing.Size(50, 37)
        Me.lblDateCaption.TabIndex = 4
        Me.lblDateCaption.Text = "วันที่"
        '
        'dtpBillDate
        '
        Me.dtpBillDate.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBillDate.Location = New System.Drawing.Point(384, 81)
        Me.dtpBillDate.Name = "dtpBillDate"
        Me.dtpBillDate.Size = New System.Drawing.Size(170, 44)
        Me.dtpBillDate.TabIndex = 5
        '
        'lblVatCaption
        '
        Me.lblVatCaption.AutoSize = True
        Me.lblVatCaption.Location = New System.Drawing.Point(605, 91)
        Me.lblVatCaption.Name = "lblVatCaption"
        Me.lblVatCaption.Size = New System.Drawing.Size(49, 37)
        Me.lblVatCaption.TabIndex = 6
        Me.lblVatCaption.Text = "VAT"
        '
        'cboVatType
        '
        Me.cboVatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVatType.Items.AddRange(New Object() {"ไม่มี VAT", "เฉพาะมี VAT"})
        Me.cboVatType.Location = New System.Drawing.Point(669, 81)
        Me.cboVatType.Name = "cboVatType"
        Me.cboVatType.Size = New System.Drawing.Size(130, 45)
        Me.cboVatType.TabIndex = 7
        '
        'lblPayCaption
        '
        Me.lblPayCaption.AutoSize = True
        Me.lblPayCaption.Location = New System.Drawing.Point(853, 91)
        Me.lblPayCaption.Name = "lblPayCaption"
        Me.lblPayCaption.Size = New System.Drawing.Size(54, 37)
        Me.lblPayCaption.TabIndex = 8
        Me.lblPayCaption.Text = "ชำระ"
        '
        'cboPayType
        '
        Me.cboPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPayType.Items.AddRange(New Object() {"เงินสด", "โอน/QR"})
        Me.cboPayType.Location = New System.Drawing.Point(923, 80)
        Me.cboPayType.Name = "cboPayType"
        Me.cboPayType.Size = New System.Drawing.Size(150, 45)
        Me.cboPayType.TabIndex = 9
        '
        'lblDiscountCaption
        '
        Me.lblDiscountCaption.AutoSize = True
        Me.lblDiscountCaption.Location = New System.Drawing.Point(19, 148)
        Me.lblDiscountCaption.Name = "lblDiscountCaption"
        Me.lblDiscountCaption.Size = New System.Drawing.Size(126, 37)
        Me.lblDiscountCaption.TabIndex = 10
        Me.lblDiscountCaption.Text = "ส่วนลดท้ายบิล"
        '
        'nudDiscount
        '
        Me.nudDiscount.DecimalPlaces = 2
        Me.nudDiscount.Location = New System.Drawing.Point(163, 141)
        Me.nudDiscount.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.nudDiscount.Name = "nudDiscount"
        Me.nudDiscount.Size = New System.Drawing.Size(110, 44)
        Me.nudDiscount.TabIndex = 11
        Me.nudDiscount.ThousandsSeparator = True
        '
        'lblCommentCaption
        '
        Me.lblCommentCaption.AutoSize = True
        Me.lblCommentCaption.Location = New System.Drawing.Point(317, 148)
        Me.lblCommentCaption.Name = "lblCommentCaption"
        Me.lblCommentCaption.Size = New System.Drawing.Size(89, 37)
        Me.lblCommentCaption.TabIndex = 12
        Me.lblCommentCaption.Text = "หมายเหตุ"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(424, 140)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(649, 44)
        Me.txtComment.TabIndex = 13
        '
        'itemPanel
        '
        Me.itemPanel.BackColor = System.Drawing.Color.White
        Me.itemPanel.Controls.Add(Me.itemLayout)
        Me.itemPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.itemPanel.Location = New System.Drawing.Point(0, 200)
        Me.itemPanel.Name = "itemPanel"
        Me.itemPanel.Padding = New System.Windows.Forms.Padding(16, 12, 16, 8)
        Me.itemPanel.Size = New System.Drawing.Size(1150, 462)
        Me.itemPanel.TabIndex = 0
        '
        'itemLayout
        '
        Me.itemLayout.BackColor = System.Drawing.Color.White
        Me.itemLayout.ColumnCount = 1
        Me.itemLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1118.0!))
        Me.itemLayout.Controls.Add(Me.productPanel, 0, 0)
        Me.itemLayout.Controls.Add(Me.itemHeader, 0, 1)
        Me.itemLayout.Controls.Add(Me.dgvItems, 0, 2)
        Me.itemLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.itemLayout.Location = New System.Drawing.Point(16, 12)
        Me.itemLayout.Name = "itemLayout"
        Me.itemLayout.RowCount = 3
        Me.itemLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.itemLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61.0!))
        Me.itemLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.itemLayout.Size = New System.Drawing.Size(1118, 442)
        Me.itemLayout.TabIndex = 0
        '
        'productPanel
        '
        Me.productPanel.BackColor = System.Drawing.Color.White
        Me.productPanel.Controls.Add(Me.productLabel)
        Me.productPanel.Controls.Add(Me.txtProductSearch)
        Me.productPanel.Controls.Add(Me.btnAddProduct)
        Me.productPanel.Controls.Add(Me.btnDeleteItem)
        Me.productPanel.Controls.Add(Me.btnClearItems)
        Me.productPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.productPanel.Location = New System.Drawing.Point(3, 3)
        Me.productPanel.Name = "productPanel"
        Me.productPanel.Size = New System.Drawing.Size(1112, 70)
        Me.productPanel.TabIndex = 0
        '
        'productLabel
        '
        Me.productLabel.AutoSize = True
        Me.productLabel.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.productLabel.Location = New System.Drawing.Point(27, 18)
        Me.productLabel.Name = "productLabel"
        Me.productLabel.Size = New System.Drawing.Size(196, 37)
        Me.productLabel.TabIndex = 0
        Me.productLabel.Text = "Product (รายการสินค้า)"
        '
        'txtProductSearch
        '
        Me.txtProductSearch.Location = New System.Drawing.Point(229, 15)
        Me.txtProductSearch.Name = "txtProductSearch"
        Me.txtProductSearch.Size = New System.Drawing.Size(533, 44)
        Me.txtProductSearch.TabIndex = 1
        '
        'btnAddProduct
        '
        Me.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddProduct.ForeColor = System.Drawing.Color.White
        Me.btnAddProduct.Location = New System.Drawing.Point(797, 12)
        Me.btnAddProduct.Name = "btnAddProduct"
        Me.btnAddProduct.Size = New System.Drawing.Size(91, 48)
        Me.btnAddProduct.TabIndex = 2
        Me.btnAddProduct.Text = "เพิ่ม"
        Me.btnAddProduct.UseVisualStyleBackColor = False
        '
        'btnDeleteItem
        '
        Me.btnDeleteItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnDeleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteItem.ForeColor = System.Drawing.Color.White
        Me.btnDeleteItem.Location = New System.Drawing.Point(889, 12)
        Me.btnDeleteItem.Name = "btnDeleteItem"
        Me.btnDeleteItem.Size = New System.Drawing.Size(102, 48)
        Me.btnDeleteItem.TabIndex = 3
        Me.btnDeleteItem.Text = "ลบรายการ"
        Me.btnDeleteItem.UseVisualStyleBackColor = False
        '
        'btnClearItems
        '
        Me.btnClearItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.btnClearItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearItems.ForeColor = System.Drawing.Color.White
        Me.btnClearItems.Location = New System.Drawing.Point(997, 12)
        Me.btnClearItems.Name = "btnClearItems"
        Me.btnClearItems.Size = New System.Drawing.Size(112, 48)
        Me.btnClearItems.TabIndex = 4
        Me.btnClearItems.Text = "ล้างทั้งหมด"
        Me.btnClearItems.UseVisualStyleBackColor = False
        '
        'itemHeader
        '
        Me.itemHeader.BackColor = System.Drawing.Color.White
        Me.itemHeader.Controls.Add(Me.itemTitle)
        Me.itemHeader.Controls.Add(Me.lblItemCount)
        Me.itemHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.itemHeader.Location = New System.Drawing.Point(3, 79)
        Me.itemHeader.Name = "itemHeader"
        Me.itemHeader.Size = New System.Drawing.Size(1112, 55)
        Me.itemHeader.TabIndex = 1
        '
        'itemTitle
        '
        Me.itemTitle.AutoSize = True
        Me.itemTitle.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.itemTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.itemTitle.Location = New System.Drawing.Point(28, 14)
        Me.itemTitle.Name = "itemTitle"
        Me.itemTitle.Size = New System.Drawing.Size(194, 33)
        Me.itemTitle.TabIndex = 0
        Me.itemTitle.Text = "รายละเอียดสินค้าที่ซื้อ"
        '
        'lblItemCount
        '
        Me.lblItemCount.AutoSize = True
        Me.lblItemCount.Location = New System.Drawing.Point(190, 10)
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Size = New System.Drawing.Size(0, 37)
        Me.lblItemCount.TabIndex = 1
        '
        'dgvItems
        '
        Me.dgvItems.AllowUserToAddRows = False
        Me.dgvItems.AllowUserToDeleteRows = False
        Me.dgvItems.BackgroundColor = System.Drawing.Color.White
        Me.dgvItems.ColumnHeadersHeight = 55
        Me.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItems.Location = New System.Drawing.Point(3, 140)
        Me.dgvItems.MultiSelect = False
        Me.dgvItems.Name = "dgvItems"
        Me.dgvItems.RowHeadersVisible = False
        Me.dgvItems.RowHeadersWidth = 60
        Me.dgvItems.RowTemplate.Height = 42
        Me.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvItems.Size = New System.Drawing.Size(1112, 299)
        Me.dgvItems.TabIndex = 2
        '
        'bottomPanel
        '
        Me.bottomPanel.BackColor = System.Drawing.Color.White
        Me.bottomPanel.Controls.Add(Me.lblTotal)
        Me.bottomPanel.Controls.Add(Me.btnSave)
        Me.bottomPanel.Controls.Add(Me.btnClose)
        Me.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bottomPanel.Location = New System.Drawing.Point(0, 662)
        Me.bottomPanel.Name = "bottomPanel"
        Me.bottomPanel.Padding = New System.Windows.Forms.Padding(16)
        Me.bottomPanel.Size = New System.Drawing.Size(1150, 58)
        Me.bottomPanel.TabIndex = 1
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(16, 22)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(0, 28)
        Me.lblTotal.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(888, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 38)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1008, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 38)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmSellBillEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1150, 720)
        Me.Controls.Add(Me.itemPanel)
        Me.Controls.Add(Me.bottomPanel)
        Me.Controls.Add(Me.topPanel)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.MinimumSize = New System.Drawing.Size(980, 640)
        Me.Name = "frmSellBillEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "แก้ไขบิลขาย"
        Me.topPanel.ResumeLayout(False)
        Me.topPanel.PerformLayout()
        CType(Me.nudDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.itemPanel.ResumeLayout(False)
        Me.itemLayout.ResumeLayout(False)
        Me.productPanel.ResumeLayout(False)
        Me.productPanel.PerformLayout()
        Me.itemHeader.ResumeLayout(False)
        Me.itemHeader.PerformLayout()
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bottomPanel.ResumeLayout(False)
        Me.bottomPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
End Class
