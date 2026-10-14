<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBeverageBom
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents cboFinishedProduct As System.Windows.Forms.ComboBox
    Friend WithEvents txtFinishedProduct As System.Windows.Forms.TextBox
    Friend WithEvents gridMaterials As System.Windows.Forms.DataGridView
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnAddMaterial As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMaterial As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblRecipeStatus As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlActions As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblFinishedProduct As System.Windows.Forms.Label
    Friend WithEvents lblProductCode As System.Windows.Forms.Label
    Friend WithEvents colLineNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMaterialID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProductCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProductName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLUnit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNumSPerLUnit As System.Windows.Forms.DataGridViewTextBoxColumn

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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cboFinishedProduct = New System.Windows.Forms.ComboBox()
        Me.txtFinishedProduct = New System.Windows.Forms.TextBox()
        Me.gridMaterials = New System.Windows.Forms.DataGridView()
        Me.LineNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaterialID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumSPerLUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnAddMaterial = New System.Windows.Forms.Button()
        Me.btnRemoveMaterial = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblRecipeStatus = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFinishedProduct = New System.Windows.Forms.Label()
        Me.lblProductCode = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.FlowLayoutPanel()
        CType(Me.gridMaterials, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboFinishedProduct
        '
        Me.cboFinishedProduct.DisplayMember = "ProductDisplay"
        Me.cboFinishedProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboFinishedProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFinishedProduct.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFinishedProduct.FormattingEnabled = True
        Me.cboFinishedProduct.Location = New System.Drawing.Point(166, 15)
        Me.cboFinishedProduct.Name = "cboFinishedProduct"
        Me.cboFinishedProduct.Size = New System.Drawing.Size(414, 45)
        Me.cboFinishedProduct.TabIndex = 1
        Me.cboFinishedProduct.ValueMember = "PN"
        '
        'txtFinishedProduct
        '
        Me.txtFinishedProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.txtFinishedProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFinishedProduct.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinishedProduct.Location = New System.Drawing.Point(741, 15)
        Me.txtFinishedProduct.Name = "txtFinishedProduct"
        Me.txtFinishedProduct.ReadOnly = True
        Me.txtFinishedProduct.Size = New System.Drawing.Size(338, 44)
        Me.txtFinishedProduct.TabIndex = 3
        '
        'gridMaterials
        '
        Me.gridMaterials.AllowUserToAddRows = False
        Me.gridMaterials.AllowUserToDeleteRows = False
        Me.gridMaterials.AllowUserToResizeRows = False
        Me.gridMaterials.BackgroundColor = System.Drawing.Color.White
        Me.gridMaterials.ColumnHeadersHeight = 38
        Me.gridMaterials.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LineNo, Me.MaterialID, Me.ProductCode, Me.ProductName, Me.Number, Me.LUnit, Me.NumSPerLUnit})
        Me.gridMaterials.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridMaterials.Location = New System.Drawing.Point(0, 238)
        Me.gridMaterials.Name = "gridMaterials"
        Me.gridMaterials.RowHeadersVisible = False
        Me.gridMaterials.RowHeadersWidth = 51
        Me.gridMaterials.RowTemplate.Height = 34
        Me.gridMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridMaterials.Size = New System.Drawing.Size(1100, 358)
        Me.gridMaterials.TabIndex = 4
        '
        'LineNo
        '
        Me.LineNo.HeaderText = "ลำดับ"
        Me.LineNo.MinimumWidth = 6
        Me.LineNo.Name = "LineNo"
        Me.LineNo.ReadOnly = True
        Me.LineNo.Width = 70
        '
        'MaterialID
        '
        Me.MaterialID.HeaderText = "MaterialID"
        Me.MaterialID.MinimumWidth = 6
        Me.MaterialID.Name = "MaterialID"
        Me.MaterialID.Visible = False
        Me.MaterialID.Width = 125
        '
        'ProductCode
        '
        Me.ProductCode.HeaderText = "รหัสวัตถุดิบ"
        Me.ProductCode.MinimumWidth = 6
        Me.ProductCode.Name = "ProductCode"
        Me.ProductCode.ReadOnly = True
        Me.ProductCode.Width = 150
        '
        'ProductName
        '
        Me.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ProductName.HeaderText = "รายการวัตถุดิบ"
        Me.ProductName.MinimumWidth = 6
        Me.ProductName.Name = "ProductName"
        Me.ProductName.ReadOnly = True
        '
        'Number
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "#,##0.00"
        Me.Number.DefaultCellStyle = DataGridViewCellStyle1
        Me.Number.HeaderText = "จำนวนใช้"
        Me.Number.MinimumWidth = 6
        Me.Number.Name = "Number"
        Me.Number.Width = 150
        '
        'LUnit
        '
        Me.LUnit.HeaderText = "หน่วย"
        Me.LUnit.MinimumWidth = 6
        Me.LUnit.Name = "LUnit"
        Me.LUnit.Width = 130
        '
        'NumSPerLUnit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "#,##0.00"
        Me.NumSPerLUnit.DefaultCellStyle = DataGridViewCellStyle2
        Me.NumSPerLUnit.HeaderText = "จำนวนต่อหน่วยใหญ่"
        Me.NumSPerLUnit.MinimumWidth = 6
        Me.NumSPerLUnit.Name = "NumSPerLUnit"
        Me.NumSPerLUnit.Width = 170
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.btnNew.FlatAppearance.BorderSize = 0
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.Location = New System.Drawing.Point(708, 10)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(110, 40)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = "สูตรใหม่"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnAddMaterial
        '
        Me.btnAddMaterial.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnAddMaterial.FlatAppearance.BorderSize = 0
        Me.btnAddMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddMaterial.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddMaterial.ForeColor = System.Drawing.Color.White
        Me.btnAddMaterial.Location = New System.Drawing.Point(24, 5)
        Me.btnAddMaterial.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnAddMaterial.Name = "btnAddMaterial"
        Me.btnAddMaterial.Size = New System.Drawing.Size(135, 40)
        Me.btnAddMaterial.TabIndex = 0
        Me.btnAddMaterial.Text = "เพิ่มวัตถุดิบ"
        Me.btnAddMaterial.UseVisualStyleBackColor = False
        '
        'btnRemoveMaterial
        '
        Me.btnRemoveMaterial.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnRemoveMaterial.FlatAppearance.BorderSize = 0
        Me.btnRemoveMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveMaterial.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveMaterial.ForeColor = System.Drawing.Color.White
        Me.btnRemoveMaterial.Location = New System.Drawing.Point(165, 5)
        Me.btnRemoveMaterial.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnRemoveMaterial.Name = "btnRemoveMaterial"
        Me.btnRemoveMaterial.Size = New System.Drawing.Size(120, 40)
        Me.btnRemoveMaterial.TabIndex = 1
        Me.btnRemoveMaterial.Text = "ลบรายการ"
        Me.btnRemoveMaterial.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(824, 10)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(140, 40)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึกสูตร"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(970, 10)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 40)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblRecipeStatus
        '
        Me.pnlTop.SetColumnSpan(Me.lblRecipeStatus, 4)
        Me.lblRecipeStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRecipeStatus.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecipeStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblRecipeStatus.Location = New System.Drawing.Point(21, 56)
        Me.lblRecipeStatus.Name = "lblRecipeStatus"
        Me.lblRecipeStatus.Size = New System.Drawing.Size(1058, 61)
        Me.lblRecipeStatus.TabIndex = 4
        Me.lblRecipeStatus.Text = "เลือกสินค้าเพื่อดูหรือสร้างสูตร BOM"
        Me.lblRecipeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1100, 52)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "สร้าง BOM สูตรเครื่องดื่ม"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.btnClose)
        Me.pnlBottom.Controls.Add(Me.btnSave)
        Me.pnlBottom.Controls.Add(Me.btnNew)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlBottom.Location = New System.Drawing.Point(0, 596)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlBottom.Size = New System.Drawing.Size(1100, 64)
        Me.pnlBottom.TabIndex = 1
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.White
        Me.pnlTop.ColumnCount = 4
        Me.pnlTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145.0!))
        Me.pnlTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.pnlTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155.0!))
        Me.pnlTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.pnlTop.Controls.Add(Me.lblFinishedProduct, 0, 0)
        Me.pnlTop.Controls.Add(Me.cboFinishedProduct, 1, 0)
        Me.pnlTop.Controls.Add(Me.lblProductCode, 2, 0)
        Me.pnlTop.Controls.Add(Me.txtFinishedProduct, 3, 0)
        Me.pnlTop.Controls.Add(Me.lblRecipeStatus, 0, 1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 52)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(18, 12, 18, 8)
        Me.pnlTop.RowCount = 2
        Me.pnlTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44.0!))
        Me.pnlTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.pnlTop.Size = New System.Drawing.Size(1100, 125)
        Me.pnlTop.TabIndex = 2
        '
        'lblFinishedProduct
        '
        Me.lblFinishedProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFinishedProduct.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinishedProduct.Location = New System.Drawing.Point(21, 12)
        Me.lblFinishedProduct.Name = "lblFinishedProduct"
        Me.lblFinishedProduct.Size = New System.Drawing.Size(139, 44)
        Me.lblFinishedProduct.TabIndex = 0
        Me.lblFinishedProduct.Text = "สินค้าเครื่องดื่ม:"
        Me.lblFinishedProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProductCode
        '
        Me.lblProductCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProductCode.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.Location = New System.Drawing.Point(586, 12)
        Me.lblProductCode.Name = "lblProductCode"
        Me.lblProductCode.Size = New System.Drawing.Size(149, 44)
        Me.lblProductCode.TabIndex = 2
        Me.lblProductCode.Text = "รหัส/ชื่อสินค้า:"
        Me.lblProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlActions.Controls.Add(Me.btnAddMaterial)
        Me.pnlActions.Controls.Add(Me.btnRemoveMaterial)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlActions.Location = New System.Drawing.Point(0, 177)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(18, 5, 18, 5)
        Me.pnlActions.Size = New System.Drawing.Size(1100, 61)
        Me.pnlActions.TabIndex = 3
        '
        'frmBeverageBom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1100, 660)
        Me.Controls.Add(Me.gridMaterials)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.lblTitle)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.MinimumSize = New System.Drawing.Size(900, 560)
        Me.Name = "frmBeverageBom"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "สร้าง BOM สูตรเครื่องดื่ม"
        CType(Me.gridMaterials, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LineNo As DataGridViewTextBoxColumn
    Friend WithEvents MaterialID As DataGridViewTextBoxColumn
    Friend WithEvents ProductCode As DataGridViewTextBoxColumn
    Friend WithEvents ProductName As DataGridViewTextBoxColumn
    Friend WithEvents Number As DataGridViewTextBoxColumn
    Friend WithEvents LUnit As DataGridViewTextBoxColumn
    Friend WithEvents NumSPerLUnit As DataGridViewTextBoxColumn
End Class
