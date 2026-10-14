<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fInventoryCount
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Friend WithEvents pnlTop As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents cboGroup As ComboBox
    Friend WithEvents cboBrand As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents dgvStock As DataGridView
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblCount As Label
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cboGroup = New System.Windows.Forms.ComboBox()
        Me.cboBrand = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.dgvStock = New System.Windows.Forms.DataGridView()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Moccasin
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Controls.Add(Me.txtSearch)
        Me.pnlTop.Controls.Add(Me.cboGroup)
        Me.pnlTop.Controls.Add(Me.cboBrand)
        Me.pnlTop.Controls.Add(Me.btnSearch)
        Me.pnlTop.Controls.Add(Me.btnSave)
        Me.pnlTop.Controls.Add(Me.btnPrint)
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlTop.Size = New System.Drawing.Size(1500, 127)
        Me.pnlTop.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(419, 38)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Inventory Count (ตรวจนับสินค้าในคลังสินค้า)"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(18, 67)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(270, 45)
        Me.txtSearch.TabIndex = 1
        '
        'cboGroup
        '
        Me.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroup.Items.AddRange(New Object() {"ทุกกลุ่มสินค้า"})
        Me.cboGroup.Location = New System.Drawing.Point(301, 67)
        Me.cboGroup.Name = "cboGroup"
        Me.cboGroup.Size = New System.Drawing.Size(170, 45)
        Me.cboGroup.TabIndex = 2
        '
        'cboBrand
        '
        Me.cboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBrand.Items.AddRange(New Object() {"ทุกยี่ห้อ"})
        Me.cboBrand.Location = New System.Drawing.Point(486, 67)
        Me.cboBrand.Name = "cboBrand"
        Me.cboBrand.Size = New System.Drawing.Size(150, 45)
        Me.cboBrand.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.ForestGreen
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(651, 63)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 49)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(761, 63)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(150, 49)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "บันทึกผลตรวจนับ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.DarkOrange
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(921, 63)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(130, 49)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "พิมพ์รายงาน"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1061, 63)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 49)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "ปิด"
        '
        'dgvStock
        '
        Me.dgvStock.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvStock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvStock.ColumnHeadersHeight = 40
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 16.2!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvStock.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvStock.Location = New System.Drawing.Point(0, 127)
        Me.dgvStock.Name = "dgvStock"
        Me.dgvStock.RowHeadersWidth = 51
        Me.dgvStock.Size = New System.Drawing.Size(1500, 645)
        Me.dgvStock.TabIndex = 0
        '
        'lblCount
        '
        Me.lblCount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblCount.Location = New System.Drawing.Point(0, 772)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Padding = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.lblCount.Size = New System.Drawing.Size(1500, 28)
        Me.lblCount.TabIndex = 1
        Me.lblCount.Text = "0 รายการ"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'fInventoryCount
        '
        Me.ClientSize = New System.Drawing.Size(1500, 800)
        Me.Controls.Add(Me.dgvStock)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "fInventoryCount"
        Me.Text = "Inventory Count Stock (ตรวจนับสินค้า ในคลังสินค้า)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
