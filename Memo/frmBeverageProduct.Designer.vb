<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBeverageProduct
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents gridProducts As System.Windows.Forms.DataGridView
    Friend WithEvents btnOpenBom As System.Windows.Forms.Button
    Friend WithEvents btnAddProduct As System.Windows.Forms.Button
    Friend WithEvents btnDeleteProduct As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents layoutMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents colProductID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProductDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBomCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBomStatus As System.Windows.Forms.DataGridViewTextBoxColumn

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
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.gridProducts = New System.Windows.Forms.DataGridView()
        Me.ProductID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductDetail = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BomCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BomStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnOpenBom = New System.Windows.Forms.Button()
        Me.btnAddProduct = New System.Windows.Forms.Button()
        Me.btnDeleteProduct = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.FlowLayoutPanel()
        Me.layoutMain = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.gridProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.layoutMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.txtSearch.Location = New System.Drawing.Point(88, 10)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(988, 44)
        Me.txtSearch.TabIndex = 0
        '
        'gridProducts
        '
        Me.gridProducts.AllowUserToAddRows = False
        Me.gridProducts.AllowUserToDeleteRows = False
        Me.gridProducts.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.gridProducts.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridProducts.ColumnHeadersHeight = 45
        Me.gridProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProductID, Me.ProductDetail, Me.BomCount, Me.BomStatus})
        Me.gridProducts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridProducts.Location = New System.Drawing.Point(3, 135)
        Me.gridProducts.MultiSelect = False
        Me.gridProducts.Name = "gridProducts"
        Me.gridProducts.RowHeadersVisible = False
        Me.gridProducts.RowHeadersWidth = 51
        Me.gridProducts.RowTemplate.Height = 38
        Me.gridProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridProducts.Size = New System.Drawing.Size(1094, 458)
        Me.gridProducts.TabIndex = 0
        '
        'ProductID
        '
        Me.ProductID.HeaderText = "รหัสเครื่องดื่ม"
        Me.ProductID.MinimumWidth = 6
        Me.ProductID.Name = "ProductID"
        Me.ProductID.ReadOnly = True
        Me.ProductID.Width = 180
        '
        'ProductDetail
        '
        Me.ProductDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ProductDetail.HeaderText = "รายการเครื่องดื่ม"
        Me.ProductDetail.MinimumWidth = 6
        Me.ProductDetail.Name = "ProductDetail"
        Me.ProductDetail.ReadOnly = True
        '
        'BomCount
        '
        Me.BomCount.HeaderText = "จำนวนวัตถุดิบ"
        Me.BomCount.MinimumWidth = 6
        Me.BomCount.Name = "BomCount"
        Me.BomCount.ReadOnly = True
        Me.BomCount.Width = 180
        '
        'BomStatus
        '
        Me.BomStatus.HeaderText = "สถานะสูตร"
        Me.BomStatus.MinimumWidth = 6
        Me.BomStatus.Name = "BomStatus"
        Me.BomStatus.ReadOnly = True
        Me.BomStatus.Width = 180
        '
        'btnOpenBom
        '
        Me.btnOpenBom.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnOpenBom.FlatAppearance.BorderSize = 0
        Me.btnOpenBom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenBom.ForeColor = System.Drawing.Color.White
        Me.btnOpenBom.Location = New System.Drawing.Point(813, 10)
        Me.btnOpenBom.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnOpenBom.Name = "btnOpenBom"
        Me.btnOpenBom.Size = New System.Drawing.Size(145, 40)
        Me.btnOpenBom.TabIndex = 1
        Me.btnOpenBom.Text = "เปิดสูตร BOM"
        Me.btnOpenBom.UseVisualStyleBackColor = False
        '
        'btnAddProduct
        '
        Me.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.btnAddProduct.FlatAppearance.BorderSize = 0
        Me.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddProduct.ForeColor = System.Drawing.Color.White
        Me.btnAddProduct.Location = New System.Drawing.Point(647, 10)
        Me.btnAddProduct.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnAddProduct.Name = "btnAddProduct"
        Me.btnAddProduct.Size = New System.Drawing.Size(160, 40)
        Me.btnAddProduct.TabIndex = 2
        Me.btnAddProduct.Text = "เพิ่มรายการ"
        Me.btnAddProduct.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(964, 10)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 40)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnDeleteProduct
        '
        Me.btnDeleteProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnDeleteProduct.FlatAppearance.BorderSize = 0
        Me.btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteProduct.ForeColor = System.Drawing.Color.White
        Me.btnDeleteProduct.Location = New System.Drawing.Point(526, 10)
        Me.btnDeleteProduct.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.btnDeleteProduct.Name = "btnDeleteProduct"
        Me.btnDeleteProduct.Size = New System.Drawing.Size(115, 40)
        Me.btnDeleteProduct.TabIndex = 4
        Me.btnDeleteProduct.Text = "ลบรายการ"
        Me.btnDeleteProduct.UseVisualStyleBackColor = False
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1094, 52)
        Me.lblTitle.TabIndex = 3
        Me.lblTitle.Text = "รายการเครื่องดื่มจาก ProductDrink"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.White
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Location = New System.Drawing.Point(3, 55)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(18, 10, 18, 10)
        Me.pnlSearch.Size = New System.Drawing.Size(1094, 74)
        Me.pnlSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.lblSearch.Location = New System.Drawing.Point(18, 10)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(70, 54)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "ค้นหา:"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.btnClose)
        Me.pnlBottom.Controls.Add(Me.btnOpenBom)
        Me.pnlBottom.Controls.Add(Me.btnDeleteProduct)
        Me.pnlBottom.Controls.Add(Me.btnAddProduct)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlBottom.Location = New System.Drawing.Point(3, 599)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlBottom.Size = New System.Drawing.Size(1094, 58)
        Me.pnlBottom.TabIndex = 2
        '
        'layoutMain
        '
        Me.layoutMain.ColumnCount = 1
        Me.layoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMain.Controls.Add(Me.lblTitle, 0, 0)
        Me.layoutMain.Controls.Add(Me.pnlSearch, 0, 1)
        Me.layoutMain.Controls.Add(Me.gridProducts, 0, 2)
        Me.layoutMain.Controls.Add(Me.pnlBottom, 0, 3)
        Me.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutMain.Location = New System.Drawing.Point(0, 0)
        Me.layoutMain.Name = "layoutMain"
        Me.layoutMain.RowCount = 4
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.layoutMain.Size = New System.Drawing.Size(1100, 660)
        Me.layoutMain.TabIndex = 5
        '
        'frmBeverageProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1100, 660)
        Me.Controls.Add(Me.layoutMain)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.MinimumSize = New System.Drawing.Size(900, 560)
        Me.Name = "frmBeverageProduct"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รายการเครื่องดื่ม"
        CType(Me.gridProducts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.layoutMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProductID As DataGridViewTextBoxColumn
    Friend WithEvents ProductDetail As DataGridViewTextBoxColumn
    Friend WithEvents BomCount As DataGridViewTextBoxColumn
    Friend WithEvents BomStatus As DataGridViewTextBoxColumn
End Class
