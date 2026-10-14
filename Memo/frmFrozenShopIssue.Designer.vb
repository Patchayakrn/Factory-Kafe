Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Partial Public Class frmFrozenShopIssue

    Private components As IContainer
    Private lblTitle As Label
    Private lblDocumentNo As Label
    Private lblOrderDate As Label
    Private lblReceiveDate As Label
    Private lblDestination As Label
    Private lblHeaderRemark As Label
    Private txtDocumentNo As TextBox
    Private dtpOrderDate As DateTimePicker
    Private dtpReceiveDate As DateTimePicker
    Private txtDestination As TextBox
    Private txtHeaderRemark As TextBox
    Private lblProductCode As Label
    Private lblProductName As Label
    Private lblCrates As Label
    Private lblKilograms As Label
    Private lblLineRemark As Label
    Private txtProductCode As TextBox
    Private btnSelectProduct As Button
    Private txtProductName As TextBox
    Private nudCrates As NumericUpDown
    Private nudKilograms As NumericUpDown
    Private txtLineRemark As TextBox
    Private btnAdd As Button
    Private btnRemove As Button
    Private dgvDetails As DataGridView
    Private colLineNo As DataGridViewTextBoxColumn
    Private colProductCode As DataGridViewTextBoxColumn
    Private colProductName As DataGridViewTextBoxColumn
    Private colCrates As DataGridViewTextBoxColumn
    Private colKilograms As DataGridViewTextBoxColumn
    Private colRemark As DataGridViewTextBoxColumn
    Private lblTotal As Label
    Private btnNew As Button
    Private btnCancel As Button
    Private btnHistory As Button
    Private btnSave As Button
    Private btnPrint As Button
    Private btnClose As Button

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblDocumentNo = New System.Windows.Forms.Label()
        Me.lblOrderDate = New System.Windows.Forms.Label()
        Me.lblReceiveDate = New System.Windows.Forms.Label()
        Me.lblDestination = New System.Windows.Forms.Label()
        Me.lblHeaderRemark = New System.Windows.Forms.Label()
        Me.txtDocumentNo = New System.Windows.Forms.TextBox()
        Me.dtpOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpReceiveDate = New System.Windows.Forms.DateTimePicker()
        Me.txtDestination = New System.Windows.Forms.TextBox()
        Me.txtHeaderRemark = New System.Windows.Forms.TextBox()
        Me.lblProductCode = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.lblCrates = New System.Windows.Forms.Label()
        Me.lblKilograms = New System.Windows.Forms.Label()
        Me.lblLineRemark = New System.Windows.Forms.Label()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.btnSelectProduct = New System.Windows.Forms.Button()
        Me.txtProductName = New System.Windows.Forms.TextBox()
        Me.nudCrates = New System.Windows.Forms.NumericUpDown()
        Me.nudKilograms = New System.Windows.Forms.NumericUpDown()
        Me.txtLineRemark = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.dgvDetails = New System.Windows.Forms.DataGridView()
        Me.LineNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Crates = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kilograms = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.nudCrates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudKilograms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.BackColor = System.Drawing.Color.White
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(1200, 50)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "ใบเบิกสินค้า Shop หน้าโรงงาน"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocumentNo.Location = New System.Drawing.Point(12, 62)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(90, 27)
        Me.lblDocumentNo.TabIndex = 1
        Me.lblDocumentNo.Text = "เลขที่เอกสาร"
        Me.lblDocumentNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrderDate
        '
        Me.lblOrderDate.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderDate.Location = New System.Drawing.Point(300, 62)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(70, 27)
        Me.lblOrderDate.TabIndex = 2
        Me.lblOrderDate.Text = "วันที่สั่ง"
        Me.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReceiveDate
        '
        Me.lblReceiveDate.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceiveDate.Location = New System.Drawing.Point(552, 62)
        Me.lblReceiveDate.Name = "lblReceiveDate"
        Me.lblReceiveDate.Size = New System.Drawing.Size(105, 27)
        Me.lblReceiveDate.TabIndex = 3
        Me.lblReceiveDate.Text = "วันที่รับสินค้า"
        Me.lblReceiveDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDestination
        '
        Me.lblDestination.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDestination.Location = New System.Drawing.Point(838, 62)
        Me.lblDestination.Name = "lblDestination"
        Me.lblDestination.Size = New System.Drawing.Size(70, 27)
        Me.lblDestination.TabIndex = 4
        Me.lblDestination.Text = "ปลายทาง"
        Me.lblDestination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHeaderRemark
        '
        Me.lblHeaderRemark.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeaderRemark.Location = New System.Drawing.Point(12, 119)
        Me.lblHeaderRemark.Name = "lblHeaderRemark"
        Me.lblHeaderRemark.Size = New System.Drawing.Size(90, 27)
        Me.lblHeaderRemark.TabIndex = 5
        Me.lblHeaderRemark.Text = "หมายเหตุ"
        Me.lblHeaderRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.txtDocumentNo.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentNo.Location = New System.Drawing.Point(105, 58)
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.ReadOnly = True
        Me.txtDocumentNo.Size = New System.Drawing.Size(175, 45)
        Me.txtDocumentNo.TabIndex = 6
        '
        'dtpOrderDate
        '
        Me.dtpOrderDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpOrderDate.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOrderDate.Location = New System.Drawing.Point(375, 58)
        Me.dtpOrderDate.Name = "dtpOrderDate"
        Me.dtpOrderDate.Size = New System.Drawing.Size(145, 45)
        Me.dtpOrderDate.TabIndex = 7
        '
        'dtpReceiveDate
        '
        Me.dtpReceiveDate.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpReceiveDate.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReceiveDate.Location = New System.Drawing.Point(663, 58)
        Me.dtpReceiveDate.Name = "dtpReceiveDate"
        Me.dtpReceiveDate.ShowUpDown = True
        Me.dtpReceiveDate.Size = New System.Drawing.Size(150, 45)
        Me.dtpReceiveDate.TabIndex = 8
        '
        'txtDestination
        '
        Me.txtDestination.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDestination.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestination.Location = New System.Drawing.Point(912, 58)
        Me.txtDestination.Name = "txtDestination"
        Me.txtDestination.Size = New System.Drawing.Size(268, 45)
        Me.txtDestination.TabIndex = 9
        Me.txtDestination.Text = "Shop หน้าโรงงาน"
        '
        'txtHeaderRemark
        '
        Me.txtHeaderRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHeaderRemark.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeaderRemark.Location = New System.Drawing.Point(105, 113)
        Me.txtHeaderRemark.Name = "txtHeaderRemark"
        Me.txtHeaderRemark.Size = New System.Drawing.Size(1075, 45)
        Me.txtHeaderRemark.TabIndex = 10
        '
        'lblProductCode
        '
        Me.lblProductCode.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.Location = New System.Drawing.Point(12, 167)
        Me.lblProductCode.Name = "lblProductCode"
        Me.lblProductCode.Size = New System.Drawing.Size(90, 37)
        Me.lblProductCode.TabIndex = 11
        Me.lblProductCode.Text = "รหัสสินค้า"
        Me.lblProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProductName
        '
        Me.lblProductName.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.Location = New System.Drawing.Point(139, 167)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(265, 37)
        Me.lblProductName.TabIndex = 12
        Me.lblProductName.Text = "รายการ"
        Me.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCrates
        '
        Me.lblCrates.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCrates.Location = New System.Drawing.Point(404, 167)
        Me.lblCrates.Name = "lblCrates"
        Me.lblCrates.Size = New System.Drawing.Size(78, 37)
        Me.lblCrates.TabIndex = 13
        Me.lblCrates.Text = "ลัง"
        Me.lblCrates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblKilograms
        '
        Me.lblKilograms.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKilograms.Location = New System.Drawing.Point(488, 167)
        Me.lblKilograms.Name = "lblKilograms"
        Me.lblKilograms.Size = New System.Drawing.Size(90, 37)
        Me.lblKilograms.TabIndex = 14
        Me.lblKilograms.Text = "กิโลกรัม"
        Me.lblKilograms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLineRemark
        '
        Me.lblLineRemark.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLineRemark.Location = New System.Drawing.Point(624, 167)
        Me.lblLineRemark.Name = "lblLineRemark"
        Me.lblLineRemark.Size = New System.Drawing.Size(220, 37)
        Me.lblLineRemark.TabIndex = 15
        Me.lblLineRemark.Text = "หมายเหตุรายการ"
        Me.lblLineRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProductCode
        '
        Me.txtProductCode.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.Location = New System.Drawing.Point(18, 207)
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.Size = New System.Drawing.Size(115, 45)
        Me.txtProductCode.TabIndex = 16
        '
        'btnSelectProduct
        '
        Me.btnSelectProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnSelectProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectProduct.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectProduct.ForeColor = System.Drawing.Color.White
        Me.btnSelectProduct.Location = New System.Drawing.Point(100, 164)
        Me.btnSelectProduct.Name = "btnSelectProduct"
        Me.btnSelectProduct.Size = New System.Drawing.Size(33, 27)
        Me.btnSelectProduct.TabIndex = 17
        Me.btnSelectProduct.Text = "..."
        Me.btnSelectProduct.UseVisualStyleBackColor = False
        '
        'txtProductName
        '
        Me.txtProductName.BackColor = System.Drawing.Color.White
        Me.txtProductName.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductName.Location = New System.Drawing.Point(139, 207)
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.ReadOnly = True
        Me.txtProductName.Size = New System.Drawing.Size(265, 45)
        Me.txtProductName.TabIndex = 17
        '
        'nudCrates
        '
        Me.nudCrates.DecimalPlaces = 2
        Me.nudCrates.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudCrates.Location = New System.Drawing.Point(410, 208)
        Me.nudCrates.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudCrates.Name = "nudCrates"
        Me.nudCrates.Size = New System.Drawing.Size(78, 45)
        Me.nudCrates.TabIndex = 18
        Me.nudCrates.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudCrates.ThousandsSeparator = True
        '
        'nudKilograms
        '
        Me.nudKilograms.DecimalPlaces = 2
        Me.nudKilograms.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudKilograms.Location = New System.Drawing.Point(494, 207)
        Me.nudKilograms.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nudKilograms.Name = "nudKilograms"
        Me.nudKilograms.Size = New System.Drawing.Size(130, 45)
        Me.nudKilograms.TabIndex = 19
        Me.nudKilograms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudKilograms.ThousandsSeparator = True
        '
        'txtLineRemark
        '
        Me.txtLineRemark.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLineRemark.Location = New System.Drawing.Point(630, 207)
        Me.txtLineRemark.Name = "txtLineRemark"
        Me.txtLineRemark.Size = New System.Drawing.Size(550, 45)
        Me.txtLineRemark.TabIndex = 20
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.White
        Me.btnAdd.Location = New System.Drawing.Point(895, 258)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(143, 44)
        Me.btnAdd.TabIndex = 21
        Me.btnAdd.Text = "เพิ่มรายการ"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.ForeColor = System.Drawing.Color.White
        Me.btnRemove.Location = New System.Drawing.Point(1044, 258)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(138, 44)
        Me.btnRemove.TabIndex = 22
        Me.btnRemove.Text = "ลบรายการ"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'dgvDetails
        '
        Me.dgvDetails.AllowUserToAddRows = False
        Me.dgvDetails.AllowUserToDeleteRows = False
        Me.dgvDetails.AllowUserToResizeRows = False
        Me.dgvDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDetails.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDetails.ColumnHeadersHeight = 45
        Me.dgvDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LineNo, Me.ProductCode, Me.ProductName, Me.Crates, Me.Kilograms, Me.Remark})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetails.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDetails.EnableHeadersVisualStyles = False
        Me.dgvDetails.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvDetails.Location = New System.Drawing.Point(12, 308)
        Me.dgvDetails.Name = "dgvDetails"
        Me.dgvDetails.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDetails.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvDetails.RowHeadersVisible = False
        Me.dgvDetails.RowHeadersWidth = 60
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvDetails.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None
        Me.dgvDetails.RowTemplate.Height = 40
        Me.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetails.Size = New System.Drawing.Size(1176, 307)
        Me.dgvDetails.TabIndex = 23
        '
        'LineNo
        '
        Me.LineNo.HeaderText = "ลำดับ"
        Me.LineNo.MinimumWidth = 6
        Me.LineNo.Name = "LineNo"
        Me.LineNo.ReadOnly = True
        Me.LineNo.Width = 80
        '
        'ProductCode
        '
        Me.ProductCode.HeaderText = "รหัสสินค้า"
        Me.ProductCode.MinimumWidth = 6
        Me.ProductCode.Name = "ProductCode"
        Me.ProductCode.ReadOnly = True
        Me.ProductCode.Width = 150
        '
        'ProductName
        '
        Me.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ProductName.HeaderText = "รายการสินค้า"
        Me.ProductName.MinimumWidth = 220
        Me.ProductName.Name = "ProductName"
        Me.ProductName.ReadOnly = True
        '
        'Crates
        '
        Me.Crates.HeaderText = "จำนวนลัง"
        Me.Crates.MinimumWidth = 6
        Me.Crates.Name = "Crates"
        Me.Crates.ReadOnly = True
        Me.Crates.Width = 130
        '
        'Kilograms
        '
        Me.Kilograms.HeaderText = "กิโลกรัม"
        Me.Kilograms.MinimumWidth = 6
        Me.Kilograms.Name = "Kilograms"
        Me.Kilograms.ReadOnly = True
        Me.Kilograms.Width = 130
        '
        'Remark
        '
        Me.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Remark.HeaderText = "หมายเหตุ"
        Me.Remark.MinimumWidth = 180
        Me.Remark.Name = "Remark"
        Me.Remark.ReadOnly = True
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(12, 618)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(1176, 34)
        Me.lblTotal.TabIndex = 24
        Me.lblTotal.Text = "รวมลัง 0.00   |   รวมกิโลกรัม 0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.Location = New System.Drawing.Point(12, 655)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(125, 46)
        Me.btnNew.TabIndex = 25
        Me.btnNew.Text = "รายการใหม่"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(145, 655)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(160, 46)
        Me.btnCancel.TabIndex = 30
        Me.btnCancel.Text = "ยกเลิก/เคลียร์"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistory.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.ForeColor = System.Drawing.Color.White
        Me.btnHistory.Location = New System.Drawing.Point(311, 655)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(209, 46)
        Me.btnHistory.TabIndex = 29
        Me.btnHistory.Text = "เรียกย้อนหลัง"
        Me.btnHistory.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(1068, 655)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 46)
        Me.btnSave.TabIndex = 28
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(948, 655)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(110, 46)
        Me.btnPrint.TabIndex = 27
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(840, 655)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 46)
        Me.btnClose.TabIndex = 26
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmFrozenShopIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblDocumentNo)
        Me.Controls.Add(Me.lblOrderDate)
        Me.Controls.Add(Me.lblReceiveDate)
        Me.Controls.Add(Me.lblDestination)
        Me.Controls.Add(Me.lblHeaderRemark)
        Me.Controls.Add(Me.txtDocumentNo)
        Me.Controls.Add(Me.dtpOrderDate)
        Me.Controls.Add(Me.dtpReceiveDate)
        Me.Controls.Add(Me.txtDestination)
        Me.Controls.Add(Me.txtHeaderRemark)
        Me.Controls.Add(Me.lblProductCode)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.lblCrates)
        Me.Controls.Add(Me.lblKilograms)
        Me.Controls.Add(Me.lblLineRemark)
        Me.Controls.Add(Me.txtProductCode)
        Me.Controls.Add(Me.btnSelectProduct)
        Me.Controls.Add(Me.txtProductName)
        Me.Controls.Add(Me.nudCrates)
        Me.Controls.Add(Me.nudKilograms)
        Me.Controls.Add(Me.txtLineRemark)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.dgvDetails)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnHistory)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.MinimumSize = New System.Drawing.Size(900, 620)
        Me.Name = "frmFrozenShopIssue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เบิกสินค้า Shop หน้าโรงงาน"
        CType(Me.nudCrates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudKilograms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents LineNo As DataGridViewTextBoxColumn
    Private WithEvents ProductCode As DataGridViewTextBoxColumn
    Private WithEvents ProductName As DataGridViewTextBoxColumn
    Private WithEvents Crates As DataGridViewTextBoxColumn
    Private WithEvents Kilograms As DataGridViewTextBoxColumn
    Private WithEvents Remark As DataGridViewTextBoxColumn
End Class
