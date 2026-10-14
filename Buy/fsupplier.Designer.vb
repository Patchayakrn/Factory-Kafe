<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fsupplier
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Private pnlTitle As Panel
    Private lblTitle As Label
    Private lblSubtitle As Label
    Private grpSearch As GroupBox
    Private lblSupplierName As Label
    Private lblSupplierCode As Label
    Private lblStatus As Label
    Private WithEvents txtSupplierName As TextBox
    Private WithEvents txtSupplierCode As TextBox
    Private cboStatus As ComboBox
    Private WithEvents btnSearch As Button
    Private WithEvents btnClear As Button
    Private lblListTitle As Label
    Private lblResult As Label
    Private WithEvents dgvSupplier As DataGridView
    Private grpDetail As GroupBox
    Private lblDetailId As Label
    Private lblDetailName As Label
    Private lblDetailTel As Label
    Private grpContact As GroupBox
    Private dgvContact As DataGridView
    Private lblHint As Label
    Private btnAdd As Button
    Private btnEdit As Button
    Private WithEvents btnOk As Button
    Private WithEvents btnCancel As Button

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.grpSearch = New System.Windows.Forms.GroupBox()
        Me.lblSupplierName = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.lblSupplierCode = New System.Windows.Forms.Label()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblListTitle = New System.Windows.Forms.Label()
        Me.lblResult = New System.Windows.Forms.Label()
        Me.dgvSupplier = New System.Windows.Forms.DataGridView()
        Me.grpDetail = New System.Windows.Forms.GroupBox()
        Me.detailCaption1 = New System.Windows.Forms.Label()
        Me.lblDetailId = New System.Windows.Forms.Label()
        Me.detailCaption2 = New System.Windows.Forms.Label()
        Me.lblDetailName = New System.Windows.Forms.Label()
        Me.detailCaption3 = New System.Windows.Forms.Label()
        Me.lblDetailTel = New System.Windows.Forms.Label()
        Me.grpContact = New System.Windows.Forms.GroupBox()
        Me.dgvContact = New System.Windows.Forms.DataGridView()
        Me.lblHint = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlTitle.SuspendLayout()
        Me.grpSearch.SuspendLayout()
        CType(Me.dgvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetail.SuspendLayout()
        CType(Me.dgvContact, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Controls.Add(Me.lblSubtitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(1082, 56)
        Me.pnlTitle.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(257, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Supplier (เจ้าหนี้ / ผู้จำหน่าย)"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Cordia New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(907, 9)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(175, 28)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "fSupplier  •  Select Supplier"
        '
        'grpSearch
        '
        Me.grpSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSearch.Controls.Add(Me.lblSupplierName)
        Me.grpSearch.Controls.Add(Me.txtSupplierName)
        Me.grpSearch.Controls.Add(Me.lblSupplierCode)
        Me.grpSearch.Controls.Add(Me.txtSupplierCode)
        Me.grpSearch.Controls.Add(Me.lblStatus)
        Me.grpSearch.Controls.Add(Me.cboStatus)
        Me.grpSearch.Controls.Add(Me.btnSearch)
        Me.grpSearch.Controls.Add(Me.btnClear)
        Me.grpSearch.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.grpSearch.Location = New System.Drawing.Point(16, 62)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(1050, 125)
        Me.grpSearch.TabIndex = 1
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "ค้นหา Supplier"
        '
        'lblSupplierName
        '
        Me.lblSupplierName.AutoSize = True
        Me.lblSupplierName.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.ForeColor = System.Drawing.Color.Black
        Me.lblSupplierName.Location = New System.Drawing.Point(14, 22)
        Me.lblSupplierName.Name = "lblSupplierName"
        Me.lblSupplierName.Size = New System.Drawing.Size(226, 39)
        Me.lblSupplierName.TabIndex = 0
        Me.lblSupplierName.Text = "Supplier Name (ชื่อเจ้าหนี้)"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.Location = New System.Drawing.Point(19, 64)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(330, 45)
        Me.txtSupplierName.TabIndex = 1
        '
        'lblSupplierCode
        '
        Me.lblSupplierCode.AutoSize = True
        Me.lblSupplierCode.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierCode.ForeColor = System.Drawing.Color.Black
        Me.lblSupplierCode.Location = New System.Drawing.Point(383, 22)
        Me.lblSupplierCode.Name = "lblSupplierCode"
        Me.lblSupplierCode.Size = New System.Drawing.Size(133, 39)
        Me.lblSupplierCode.TabIndex = 2
        Me.lblSupplierCode.Text = "Supplier Code"
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.Location = New System.Drawing.Point(390, 64)
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.Size = New System.Drawing.Size(180, 45)
        Me.txtSupplierCode.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(596, 22)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(71, 39)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "สถานะ"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Items.AddRange(New Object() {"ใช้งานอยู่", "ทั้งหมด", "ไม่ใช้งาน"})
        Me.cboStatus.Location = New System.Drawing.Point(603, 64)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(150, 40)
        Me.cboStatus.TabIndex = 5
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(856, 62)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(90, 47)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Location = New System.Drawing.Point(964, 62)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(80, 47)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "ล้าง"
        '
        'lblListTitle
        '
        Me.lblListTitle.AutoSize = True
        Me.lblListTitle.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblListTitle.Location = New System.Drawing.Point(20, 202)
        Me.lblListTitle.Name = "lblListTitle"
        Me.lblListTitle.Size = New System.Drawing.Size(123, 29)
        Me.lblListTitle.TabIndex = 2
        Me.lblListTitle.Text = "รายการ Supplier"
        '
        'lblResult
        '
        Me.lblResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResult.AutoSize = True
        Me.lblResult.Font = New System.Drawing.Font("Cordia New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblResult.Location = New System.Drawing.Point(972, 202)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(99, 28)
        Me.lblResult.TabIndex = 3
        Me.lblResult.Text = "แสดง 0 รายการ"
        '
        'dgvSupplier
        '
        Me.dgvSupplier.AllowUserToAddRows = False
        Me.dgvSupplier.AllowUserToDeleteRows = False
        Me.dgvSupplier.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSupplier.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSupplier.ColumnHeadersHeight = 32
        Me.dgvSupplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSupplier.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvSupplier.EnableHeadersVisualStyles = False
        Me.dgvSupplier.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgvSupplier.Location = New System.Drawing.Point(21, 235)
        Me.dgvSupplier.MultiSelect = False
        Me.dgvSupplier.Name = "dgvSupplier"
        Me.dgvSupplier.ReadOnly = True
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSupplier.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvSupplier.RowHeadersVisible = False
        Me.dgvSupplier.RowHeadersWidth = 51
        Me.dgvSupplier.RowTemplate.Height = 30
        Me.dgvSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSupplier.Size = New System.Drawing.Size(1050, 226)
        Me.dgvSupplier.TabIndex = 4
        '
        'grpDetail
        '
        Me.grpDetail.Controls.Add(Me.lblDetailId)
        Me.grpDetail.Controls.Add(Me.lblDetailName)
        Me.grpDetail.Controls.Add(Me.lblDetailTel)
        Me.grpDetail.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.grpDetail.Location = New System.Drawing.Point(21, 478)
        Me.grpDetail.Name = "grpDetail"
        Me.grpDetail.Size = New System.Drawing.Size(510, 125)
        Me.grpDetail.TabIndex = 5
        Me.grpDetail.TabStop = False
        Me.grpDetail.Text = "ข้อมูล Supplier ที่เลือก"
        '
        'detailCaption1
        '
        Me.detailCaption1.Location = New System.Drawing.Point(320, 606)
        Me.detailCaption1.Name = "detailCaption1"
        Me.detailCaption1.Size = New System.Drawing.Size(100, 23)
        Me.detailCaption1.TabIndex = 0
        '
        'lblDetailId
        '
        Me.lblDetailId.AutoSize = True
        Me.lblDetailId.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailId.Location = New System.Drawing.Point(14, 45)
        Me.lblDetailId.Name = "lblDetailId"
        Me.lblDetailId.Size = New System.Drawing.Size(65, 35)
        Me.lblDetailId.TabIndex = 1
        Me.lblDetailId.Text = "10001"
        '
        'detailCaption2
        '
        Me.detailCaption2.Location = New System.Drawing.Point(320, 614)
        Me.detailCaption2.Name = "detailCaption2"
        Me.detailCaption2.Size = New System.Drawing.Size(100, 23)
        Me.detailCaption2.TabIndex = 2
        '
        'lblDetailName
        '
        Me.lblDetailName.AutoSize = True
        Me.lblDetailName.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailName.Location = New System.Drawing.Point(260, 45)
        Me.lblDetailName.Name = "lblDetailName"
        Me.lblDetailName.Size = New System.Drawing.Size(209, 35)
        Me.lblDetailName.TabIndex = 3
        Me.lblDetailName.Text = "บริษัท กาแฟไทย จำกัด"
        '
        'detailCaption3
        '
        Me.detailCaption3.Location = New System.Drawing.Point(468, 614)
        Me.detailCaption3.Name = "detailCaption3"
        Me.detailCaption3.Size = New System.Drawing.Size(100, 23)
        Me.detailCaption3.TabIndex = 4
        '
        'lblDetailTel
        '
        Me.lblDetailTel.AutoSize = True
        Me.lblDetailTel.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailTel.Location = New System.Drawing.Point(14, 84)
        Me.lblDetailTel.Name = "lblDetailTel"
        Me.lblDetailTel.Size = New System.Drawing.Size(117, 35)
        Me.lblDetailTel.TabIndex = 5
        Me.lblDetailTel.Text = "02-123-4567"
        '
        'grpContact
        '
        Me.grpContact.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpContact.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpContact.ForeColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.grpContact.Location = New System.Drawing.Point(545, 478)
        Me.grpContact.Name = "grpContact"
        Me.grpContact.Size = New System.Drawing.Size(526, 125)
        Me.grpContact.TabIndex = 6
        Me.grpContact.TabStop = False
        Me.grpContact.Text = "Supplier Contact"
        '
        'dgvContact
        '
        Me.dgvContact.AllowUserToAddRows = False
        Me.dgvContact.AllowUserToDeleteRows = False
        Me.dgvContact.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvContact.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(123, Byte), Integer))
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvContact.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvContact.ColumnHeadersHeight = 32
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvContact.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvContact.EnableHeadersVisualStyles = False
        Me.dgvContact.GridColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgvContact.Location = New System.Drawing.Point(10, 25)
        Me.dgvContact.MultiSelect = False
        Me.dgvContact.Name = "dgvContact"
        Me.dgvContact.ReadOnly = True
        Me.dgvContact.RowHeadersWidth = 51
        Me.dgvContact.RowTemplate.Height = 30
        Me.dgvContact.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvContact.Size = New System.Drawing.Size(504, 88)
        Me.dgvContact.TabIndex = 0
        '
        'lblHint
        '
        Me.lblHint.AutoSize = True
        Me.lblHint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblHint.Location = New System.Drawing.Point(21, 622)
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Size = New System.Drawing.Size(230, 20)
        Me.lblHint.TabIndex = 7
        Me.lblHint.Text = "Double-click แถวเพื่อเลือก Supplier"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(755, 614)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 34)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "เพิ่ม"
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(836, 614)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 34)
        Me.btnEdit.TabIndex = 9
        Me.btnEdit.Text = "แก้ไข"
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(917, 614)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 34)
        Me.btnOk.TabIndex = 10
        Me.btnOk.Text = "ตกลง"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(998, 614)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 34)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "ยกเลิก"
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัสเจ้าหนี้"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 130
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn3.HeaderText = "ชื่อเจ้าหนี้"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 200
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn4.HeaderText = "โทรศัพท์"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 125
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn5.HeaderText = "เครดิต (วัน)"
        Me.DataGridViewTextBoxColumn5.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 125
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn6.HeaderText = "ส่วนลด (%)"
        Me.DataGridViewTextBoxColumn6.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 125
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn7.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn7.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 125
        '
        'fsupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1082, 652)
        Me.Controls.Add(Me.detailCaption1)
        Me.Controls.Add(Me.detailCaption2)
        Me.Controls.Add(Me.detailCaption3)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.grpSearch)
        Me.Controls.Add(Me.lblListTitle)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.dgvSupplier)
        Me.Controls.Add(Me.grpDetail)
        Me.Controls.Add(Me.grpContact)
        Me.Controls.Add(Me.lblHint)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnCancel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(850, 580)
        Me.Name = "fsupplier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Supplier (เจ้าหนี้)"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTitle.PerformLayout()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.dgvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDetail.ResumeLayout(False)
        Me.grpDetail.PerformLayout()
        CType(Me.dgvContact, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents detailCaption1 As Label
    Friend WithEvents detailCaption2 As Label
    Friend WithEvents detailCaption3 As Label
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
End Class
