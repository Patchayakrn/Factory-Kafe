<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fStock
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents cboBranch As ComboBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents cboCategory As ComboBox
    Friend WithEvents cboGroup As ComboBox
    Friend WithEvents cboBrand As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnCopy As Button
    Friend WithEvents lblCount As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents gridStock As DataGridView
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblBranch As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents lblGroup As Label
    Friend WithEvents lblBrand As Label

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cboBranch = New ComboBox()
        Me.txtSearch = New TextBox()
        Me.cboCategory = New ComboBox()
        Me.cboGroup = New ComboBox()
        Me.cboBrand = New ComboBox()
        Me.btnSearch = New Button()
        Me.btnRefresh = New Button()
        Me.btnCopy = New Button()
        Me.lblCount = New Label()
        Me.lblTotal = New Label()
        Me.gridStock = New DataGridView()
        Me.pnlFilters = New Panel()
        Me.lblBranch = New Label()
        Me.lblSearch = New Label()
        Me.lblCategory = New Label()
        Me.lblGroup = New Label()
        Me.lblBrand = New Label()
        CType(Me.gridStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilters.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlFilters.BackColor = Color.Linen
        Me.pnlFilters.Dock = DockStyle.Top
        Me.pnlFilters.Height = 82
        Me.pnlFilters.Padding = New Padding(8)
        Me.pnlFilters.Controls.AddRange(New Control() {Me.lblBranch, Me.cboBranch, Me.lblSearch, Me.txtSearch, Me.lblCategory, Me.cboCategory, Me.lblGroup, Me.cboGroup, Me.lblBrand, Me.cboBrand, Me.btnSearch, Me.btnRefresh, Me.btnCopy, Me.lblCount})

        Me.lblBranch.AutoSize = False : Me.lblBranch.Location = New Point(8, 8) : Me.lblBranch.Size = New Size(130, 20) : Me.lblBranch.Text = "สาขา"
        Me.lblSearch.AutoSize = False : Me.lblSearch.Location = New Point(148, 8) : Me.lblSearch.Size = New Size(210, 20) : Me.lblSearch.Text = "ค้นหา (ชื่อ / Barcode / รหัส)"
        Me.lblCategory.AutoSize = False : Me.lblCategory.Location = New Point(366, 8) : Me.lblCategory.Size = New Size(140, 20) : Me.lblCategory.Text = "ประเภท"
        Me.lblGroup.AutoSize = False : Me.lblGroup.Location = New Point(514, 8) : Me.lblGroup.Size = New Size(160, 20) : Me.lblGroup.Text = "กลุ่มสินค้า"
        Me.lblBrand.AutoSize = False : Me.lblBrand.Location = New Point(682, 8) : Me.lblBrand.Size = New Size(140, 20) : Me.lblBrand.Text = "ยี่ห้อ"
        Me.cboBranch.DropDownStyle = ComboBoxStyle.DropDownList : Me.cboBranch.Location = New Point(8, 32) : Me.cboBranch.Size = New Size(130, 24)
        Me.txtSearch.Location = New Point(148, 32) : Me.txtSearch.Size = New Size(210, 24)
        Me.cboCategory.DropDownStyle = ComboBoxStyle.DropDownList : Me.cboCategory.Location = New Point(366, 32) : Me.cboCategory.Size = New Size(140, 24)
        Me.cboGroup.DropDownStyle = ComboBoxStyle.DropDownList : Me.cboGroup.Location = New Point(514, 32) : Me.cboGroup.Size = New Size(160, 24)
        Me.cboBrand.DropDownStyle = ComboBoxStyle.DropDownList : Me.cboBrand.Location = New Point(682, 32) : Me.cboBrand.Size = New Size(140, 24)
        Me.btnSearch.Location = New Point(830, 30) : Me.btnSearch.Size = New Size(80, 28) : Me.btnSearch.Text = "ค้นหา" : Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnRefresh.Location = New Point(918, 30) : Me.btnRefresh.Size = New Size(80, 28) : Me.btnRefresh.Text = "รีเฟรช" : Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnCopy.Location = New Point(1006, 30) : Me.btnCopy.Size = New Size(80, 28) : Me.btnCopy.Text = "คัดลอก" : Me.btnCopy.UseVisualStyleBackColor = True
        Me.lblCount.AutoSize = True
        Me.lblCount.Location = New Point(1098, 38)
        Me.lblCount.Text = "0 รายการ"

        Me.gridStock.AllowUserToAddRows = False
        Me.gridStock.AllowUserToDeleteRows = False
        Me.gridStock.AllowUserToResizeRows = False
        Me.gridStock.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        Me.gridStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.gridStock.AutoGenerateColumns = True
        Me.gridStock.Dock = DockStyle.Fill
        Me.gridStock.MultiSelect = True
        Me.gridStock.ReadOnly = True
        Me.gridStock.RowHeadersVisible = False
        Me.gridStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Me.lblTotal.BackColor = Color.Beige
        Me.lblTotal.Dock = DockStyle.Bottom
        Me.lblTotal.Height = 30
        Me.lblTotal.Padding = New Padding(0, 0, 12, 0)
        Me.lblTotal.TextAlign = ContentAlignment.MiddleRight
        Me.lblTotal.Text = "รวมคงเหลือ: 0.00"

        Me.AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1200, 650)
        Me.Controls.Add(Me.gridStock)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.pnlFilters)
        Me.Font = New Font("Tahoma", 9.0F)
        Me.MinimumSize = New Size(900, 450)
        Me.Name = "fStock"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Stock (คลังสินค้า)"
        Me.WindowState = FormWindowState.Maximized
        AddHandler Me.Load, AddressOf StockLoad
        AddHandler Me.txtSearch.KeyDown, AddressOf SearchKeyDown
        AddHandler Me.btnSearch.Click, AddressOf SearchClick
        AddHandler Me.btnRefresh.Click, AddressOf SearchClick
        AddHandler Me.btnCopy.Click, AddressOf CopyClick
        AddHandler Me.gridStock.CellFormatting, AddressOf GridCellFormatting
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        CType(Me.gridStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

End Class
