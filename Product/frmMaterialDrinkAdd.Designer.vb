<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMaterialDrinkAdd
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents root As TableLayoutPanel
    Friend WithEvents titleLabel As Label
    Friend WithEvents fieldsPanel As TableLayoutPanel
    Friend WithEvents tableTitleLabel As Label
    Friend WithEvents gridMaterials As DataGridView
    Friend WithEvents actionsPanel As FlowLayoutPanel
    Friend WithEvents lblMaterialID As Label
    Friend WithEvents lblMaterialDetail As Label
    Friend WithEvents lblBrand As Label
    Friend WithEvents lblUnitUse As Label
    Friend WithEvents lblUnitPerPackage As Label
    Friend WithEvents lblUnitPrice As Label
    Friend WithEvents lblWtToVol As Label
    Friend WithEvents txtMaterialID As TextBox
    Friend WithEvents txtMaterialDetail As TextBox
    Friend WithEvents txtBrand As TextBox
    Friend WithEvents txtUnitUse As TextBox
    Friend WithEvents nudUnitPerPackage As NumericUpDown
    Friend WithEvents nudUnitPrice As NumericUpDown
    Friend WithEvents nudWtToVol As NumericUpDown
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.root = New System.Windows.Forms.TableLayoutPanel()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.fieldsPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMaterialID = New System.Windows.Forms.Label()
        Me.txtMaterialID = New System.Windows.Forms.TextBox()
        Me.lblMaterialDetail = New System.Windows.Forms.Label()
        Me.txtMaterialDetail = New System.Windows.Forms.TextBox()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.txtBrand = New System.Windows.Forms.TextBox()
        Me.lblUnitUse = New System.Windows.Forms.Label()
        Me.txtUnitUse = New System.Windows.Forms.TextBox()
        Me.lblUnitPerPackage = New System.Windows.Forms.Label()
        Me.nudUnitPerPackage = New System.Windows.Forms.NumericUpDown()
        Me.lblUnitPrice = New System.Windows.Forms.Label()
        Me.nudUnitPrice = New System.Windows.Forms.NumericUpDown()
        Me.lblWtToVol = New System.Windows.Forms.Label()
        Me.nudWtToVol = New System.Windows.Forms.NumericUpDown()
        Me.tableTitleLabel = New System.Windows.Forms.Label()
        Me.gridMaterials = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.actionsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.root.SuspendLayout()
        Me.fieldsPanel.SuspendLayout()
        CType(Me.nudUnitPerPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudUnitPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWtToVol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridMaterials, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.actionsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'root
        '
        Me.root.ColumnCount = 1
        Me.root.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.root.Controls.Add(Me.titleLabel, 0, 0)
        Me.root.Controls.Add(Me.fieldsPanel, 0, 1)
        Me.root.Controls.Add(Me.tableTitleLabel, 0, 2)
        Me.root.Controls.Add(Me.gridMaterials, 0, 3)
        Me.root.Controls.Add(Me.actionsPanel, 0, 4)
        Me.root.Dock = System.Windows.Forms.DockStyle.Fill
        Me.root.Location = New System.Drawing.Point(0, 0)
        Me.root.Name = "root"
        Me.root.Padding = New System.Windows.Forms.Padding(18)
        Me.root.RowCount = 5
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.root.Size = New System.Drawing.Size(1100, 720)
        Me.root.TabIndex = 0
        '
        'titleLabel
        '
        Me.titleLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.titleLabel.Font = New System.Drawing.Font("Cordia New", 22.0!, System.Drawing.FontStyle.Bold)
        Me.titleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.titleLabel.Location = New System.Drawing.Point(21, 18)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.titleLabel.Size = New System.Drawing.Size(1058, 60)
        Me.titleLabel.TabIndex = 0
        Me.titleLabel.Text = "เพิ่มวัตถุดิบเครื่องดื่ม"
        Me.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fieldsPanel
        '
        Me.fieldsPanel.ColumnCount = 4
        Me.fieldsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.fieldsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.fieldsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.fieldsPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.fieldsPanel.Controls.Add(Me.lblMaterialID, 0, 0)
        Me.fieldsPanel.Controls.Add(Me.txtMaterialID, 1, 0)
        Me.fieldsPanel.Controls.Add(Me.lblMaterialDetail, 2, 0)
        Me.fieldsPanel.Controls.Add(Me.txtMaterialDetail, 3, 0)
        Me.fieldsPanel.Controls.Add(Me.lblBrand, 0, 1)
        Me.fieldsPanel.Controls.Add(Me.txtBrand, 1, 1)
        Me.fieldsPanel.Controls.Add(Me.lblUnitUse, 2, 1)
        Me.fieldsPanel.Controls.Add(Me.txtUnitUse, 3, 1)
        Me.fieldsPanel.Controls.Add(Me.lblUnitPerPackage, 0, 2)
        Me.fieldsPanel.Controls.Add(Me.nudUnitPerPackage, 1, 2)
        Me.fieldsPanel.Controls.Add(Me.lblUnitPrice, 2, 2)
        Me.fieldsPanel.Controls.Add(Me.nudUnitPrice, 3, 2)
        Me.fieldsPanel.Controls.Add(Me.lblWtToVol, 0, 3)
        Me.fieldsPanel.Controls.Add(Me.nudWtToVol, 1, 3)
        Me.fieldsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fieldsPanel.Location = New System.Drawing.Point(21, 81)
        Me.fieldsPanel.Name = "fieldsPanel"
        Me.fieldsPanel.Padding = New System.Windows.Forms.Padding(8, 20, 8, 8)
        Me.fieldsPanel.RowCount = 4
        Me.fieldsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51.0!))
        Me.fieldsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61.0!))
        Me.fieldsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54.0!))
        Me.fieldsPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.fieldsPanel.Size = New System.Drawing.Size(1058, 244)
        Me.fieldsPanel.TabIndex = 1
        '
        'lblMaterialID
        '
        Me.lblMaterialID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMaterialID.Location = New System.Drawing.Point(11, 20)
        Me.lblMaterialID.Name = "lblMaterialID"
        Me.lblMaterialID.Size = New System.Drawing.Size(164, 51)
        Me.lblMaterialID.TabIndex = 0
        Me.lblMaterialID.Text = "รหัสวัตถุดิบ *"
        Me.lblMaterialID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMaterialID
        '
        Me.txtMaterialID.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.txtMaterialID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMaterialID.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.txtMaterialID.Location = New System.Drawing.Point(181, 28)
        Me.txtMaterialID.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.txtMaterialID.Name = "txtMaterialID"
        Me.txtMaterialID.Size = New System.Drawing.Size(336, 47)
        Me.txtMaterialID.TabIndex = 1
        '
        'lblMaterialDetail
        '
        Me.lblMaterialDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMaterialDetail.Location = New System.Drawing.Point(532, 20)
        Me.lblMaterialDetail.Name = "lblMaterialDetail"
        Me.lblMaterialDetail.Size = New System.Drawing.Size(164, 51)
        Me.lblMaterialDetail.TabIndex = 2
        Me.lblMaterialDetail.Text = "รายการวัตถุดิบ *"
        Me.lblMaterialDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMaterialDetail
        '
        Me.txtMaterialDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.txtMaterialDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMaterialDetail.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.txtMaterialDetail.Location = New System.Drawing.Point(702, 28)
        Me.txtMaterialDetail.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.txtMaterialDetail.Name = "txtMaterialDetail"
        Me.txtMaterialDetail.Size = New System.Drawing.Size(336, 47)
        Me.txtMaterialDetail.TabIndex = 3
        '
        'lblBrand
        '
        Me.lblBrand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBrand.Location = New System.Drawing.Point(11, 71)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(164, 61)
        Me.lblBrand.TabIndex = 4
        Me.lblBrand.Text = "ยี่ห้อ"
        Me.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBrand
        '
        Me.txtBrand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBrand.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.txtBrand.Location = New System.Drawing.Point(181, 79)
        Me.txtBrand.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.txtBrand.Name = "txtBrand"
        Me.txtBrand.Size = New System.Drawing.Size(336, 47)
        Me.txtBrand.TabIndex = 5
        '
        'lblUnitUse
        '
        Me.lblUnitUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUnitUse.Location = New System.Drawing.Point(532, 71)
        Me.lblUnitUse.Name = "lblUnitUse"
        Me.lblUnitUse.Size = New System.Drawing.Size(164, 61)
        Me.lblUnitUse.TabIndex = 6
        Me.lblUnitUse.Text = "หน่วยใช้ *"
        Me.lblUnitUse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUnitUse
        '
        Me.txtUnitUse.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.txtUnitUse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUnitUse.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.txtUnitUse.Location = New System.Drawing.Point(702, 79)
        Me.txtUnitUse.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.txtUnitUse.Name = "txtUnitUse"
        Me.txtUnitUse.Size = New System.Drawing.Size(336, 47)
        Me.txtUnitUse.TabIndex = 7
        '
        'lblUnitPerPackage
        '
        Me.lblUnitPerPackage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUnitPerPackage.Location = New System.Drawing.Point(11, 132)
        Me.lblUnitPerPackage.Name = "lblUnitPerPackage"
        Me.lblUnitPerPackage.Size = New System.Drawing.Size(164, 54)
        Me.lblUnitPerPackage.TabIndex = 8
        Me.lblUnitPerPackage.Text = "จำนวนต่อแพ็ก"
        Me.lblUnitPerPackage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'nudUnitPerPackage
        '
        Me.nudUnitPerPackage.DecimalPlaces = 2
        Me.nudUnitPerPackage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nudUnitPerPackage.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.nudUnitPerPackage.Location = New System.Drawing.Point(181, 140)
        Me.nudUnitPerPackage.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.nudUnitPerPackage.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nudUnitPerPackage.Name = "nudUnitPerPackage"
        Me.nudUnitPerPackage.Size = New System.Drawing.Size(336, 47)
        Me.nudUnitPerPackage.TabIndex = 9
        Me.nudUnitPerPackage.ThousandsSeparator = True
        Me.nudUnitPerPackage.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblUnitPrice
        '
        Me.lblUnitPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUnitPrice.Location = New System.Drawing.Point(532, 132)
        Me.lblUnitPrice.Name = "lblUnitPrice"
        Me.lblUnitPrice.Size = New System.Drawing.Size(164, 54)
        Me.lblUnitPrice.TabIndex = 10
        Me.lblUnitPrice.Text = "ราคาต่อหน่วย"
        Me.lblUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'nudUnitPrice
        '
        Me.nudUnitPrice.DecimalPlaces = 2
        Me.nudUnitPrice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nudUnitPrice.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.nudUnitPrice.Location = New System.Drawing.Point(702, 140)
        Me.nudUnitPrice.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.nudUnitPrice.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nudUnitPrice.Name = "nudUnitPrice"
        Me.nudUnitPrice.Size = New System.Drawing.Size(336, 47)
        Me.nudUnitPrice.TabIndex = 11
        Me.nudUnitPrice.ThousandsSeparator = True
        '
        'lblWtToVol
        '
        Me.lblWtToVol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWtToVol.Location = New System.Drawing.Point(11, 186)
        Me.lblWtToVol.Name = "lblWtToVol"
        Me.lblWtToVol.Size = New System.Drawing.Size(164, 66)
        Me.lblWtToVol.TabIndex = 12
        Me.lblWtToVol.Text = "น้ำหนัก/ปริมาตร"
        Me.lblWtToVol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'nudWtToVol
        '
        Me.nudWtToVol.DecimalPlaces = 2
        Me.nudWtToVol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nudWtToVol.Font = New System.Drawing.Font("Cordia New", 17.0!)
        Me.nudWtToVol.Location = New System.Drawing.Point(181, 194)
        Me.nudWtToVol.Margin = New System.Windows.Forms.Padding(3, 8, 12, 8)
        Me.nudWtToVol.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.nudWtToVol.Name = "nudWtToVol"
        Me.nudWtToVol.Size = New System.Drawing.Size(336, 47)
        Me.nudWtToVol.TabIndex = 13
        Me.nudWtToVol.ThousandsSeparator = True
        '
        'tableTitleLabel
        '
        Me.tableTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableTitleLabel.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tableTitleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.tableTitleLabel.Location = New System.Drawing.Point(21, 328)
        Me.tableTitleLabel.Name = "tableTitleLabel"
        Me.tableTitleLabel.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.tableTitleLabel.Size = New System.Drawing.Size(1058, 34)
        Me.tableTitleLabel.TabIndex = 2
        Me.tableTitleLabel.Text = "รายการวัตถุดิบเครื่องดื่มที่มีอยู่"
        Me.tableTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gridMaterials
        '
        Me.gridMaterials.AllowUserToAddRows = False
        Me.gridMaterials.AllowUserToDeleteRows = False
        Me.gridMaterials.AllowUserToResizeRows = False
        Me.gridMaterials.BackgroundColor = System.Drawing.Color.White
        Me.gridMaterials.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single
        Me.gridMaterials.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.gridMaterials.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridMaterials.ColumnHeadersHeight = 38
        Me.gridMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.gridMaterials.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7})
        Me.gridMaterials.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridMaterials.EnableHeadersVisualStyles = False
        Me.gridMaterials.GridColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.gridMaterials.AutoGenerateColumns = False
        Me.gridMaterials.Location = New System.Drawing.Point(21, 365)
        Me.gridMaterials.MultiSelect = False
        Me.gridMaterials.Name = "gridMaterials"
        Me.gridMaterials.ReadOnly = True
        Me.gridMaterials.RowHeadersVisible = False
        Me.gridMaterials.RowHeadersWidth = 51
        Me.gridMaterials.RowTemplate.Height = 31
        Me.gridMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridMaterials.Size = New System.Drawing.Size(1058, 268)
        Me.gridMaterials.TabIndex = 3
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "MaterialID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสวัตถุดิบ"
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 125
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Material_Detail"
        Me.DataGridViewTextBoxColumn2.HeaderText = "รายการวัตถุดิบ"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 125
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Brand"
        Me.DataGridViewTextBoxColumn3.HeaderText = "ยี่ห้อ"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 125
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Unit_Use"
        Me.DataGridViewTextBoxColumn4.HeaderText = "หน่วยใช้"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 125
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Unit_per_Package"
        Me.DataGridViewTextBoxColumn5.HeaderText = "จำนวน/แพ็ก"
        Me.DataGridViewTextBoxColumn5.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle With {.Format = "N2", .Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
        Me.DataGridViewTextBoxColumn5.Width = 125
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Unit_Price"
        Me.DataGridViewTextBoxColumn6.HeaderText = "ราคา/แพ็ก"
        Me.DataGridViewTextBoxColumn6.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle With {.Format = "N2", .Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
        Me.DataGridViewTextBoxColumn6.Width = 125
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Wt_to_Vol"
        Me.DataGridViewTextBoxColumn7.HeaderText = "กรัม/มล."
        Me.DataGridViewTextBoxColumn7.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle With {.Format = "N2", .Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight}
        Me.DataGridViewTextBoxColumn7.Width = 125
        '
        'actionsPanel
        '
        Me.actionsPanel.Controls.Add(Me.btnCancel)
        Me.actionsPanel.Controls.Add(Me.btnSave)
        Me.actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.actionsPanel.Location = New System.Drawing.Point(21, 639)
        Me.actionsPanel.Name = "actionsPanel"
        Me.actionsPanel.Padding = New System.Windows.Forms.Padding(8, 8, 8, 0)
        Me.actionsPanel.Size = New System.Drawing.Size(1058, 60)
        Me.actionsPanel.TabIndex = 4
        Me.actionsPanel.WrapContents = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(907, 8)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(135, 44)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(766, 8)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(135, 44)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'frmMaterialDrinkAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1100, 720)
        Me.Controls.Add(Me.root)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMaterialDrinkAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "เพิ่มวัตถุดิบเครื่องดื่ม"
        Me.root.ResumeLayout(False)
        Me.fieldsPanel.ResumeLayout(False)
        Me.fieldsPanel.PerformLayout()
        CType(Me.nudUnitPerPackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudUnitPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWtToVol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridMaterials, System.ComponentModel.ISupportInitialize).EndInit()
        Me.actionsPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
End Class
