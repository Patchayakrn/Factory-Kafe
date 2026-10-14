Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmMemoProductPriceApprovalHistory

    Private components As IContainer
    Private lblTitle As Label
    Private pnlSearch As Panel
    Private lblSearch As Label
    Private txtSearch As TextBox
    Private btnSearch As Button
    Private btnSelect As Button
    Private btnClose As Button
    Private grid As DataGridView

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Dim headerStyle As New DataGridViewCellStyle()
        Dim cellStyle As New DataGridViewCellStyle()
        Me.lblTitle = New Label()
        Me.pnlSearch = New Panel()
        Me.btnClose = New Button()
        Me.btnSelect = New Button()
        Me.btnSearch = New Button()
        Me.txtSearch = New TextBox()
        Me.lblSearch = New Label()
        Me.grid = New DataGridView()
        Me.pnlSearch.SuspendLayout()
        CType(Me.grid, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = Color.FromArgb(230, 242, 252)
        Me.lblTitle.Dock = DockStyle.Top
        Me.lblTitle.Font = New Font("Cordia New", 20.0!, FontStyle.Bold)
        Me.lblTitle.ForeColor = Color.FromArgb(30, 70, 110)
        Me.lblTitle.Location = New Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New Padding(12, 0, 0, 0)
        Me.lblTitle.Size = New Size(1034, 73)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "รายการอนุมัติราคาจำหน่ายสินค้าย้อนหลัง"
        Me.lblTitle.TextAlign = ContentAlignment.MiddleLeft
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = Color.WhiteSmoke
        Me.pnlSearch.Controls.Add(Me.btnClose)
        Me.pnlSearch.Controls.Add(Me.btnSelect)
        Me.pnlSearch.Controls.Add(Me.btnSearch)
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Dock = DockStyle.Top
        Me.pnlSearch.Location = New Point(0, 73)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New Padding(12, 8, 12, 8)
        Me.pnlSearch.Size = New Size(1034, 75)
        Me.pnlSearch.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnClose.BackColor = Color.FromArgb(190, 80, 75)
        Me.btnClose.DialogResult = DialogResult.Cancel
        Me.btnClose.FlatStyle = FlatStyle.Flat
        Me.btnClose.Font = New Font("Cordia New", 16.2!)
        Me.btnClose.ForeColor = Color.White
        Me.btnClose.Location = New Point(910, 17)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New Size(105, 39)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnSelect.BackColor = Color.FromArgb(45, 115, 185)
        Me.btnSelect.FlatStyle = FlatStyle.Flat
        Me.btnSelect.Font = New Font("Cordia New", 16.2!)
        Me.btnSelect.ForeColor = Color.White
        Me.btnSelect.Location = New Point(768, 17)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New Size(127, 39)
        Me.btnSelect.TabIndex = 3
        Me.btnSelect.Text = "เลือกเอกสาร"
        Me.btnSelect.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = Color.FromArgb(45, 130, 70)
        Me.btnSearch.FlatStyle = FlatStyle.Flat
        Me.btnSearch.Font = New Font("Cordia New", 16.2!)
        Me.btnSearch.ForeColor = Color.White
        Me.btnSearch.Location = New Point(541, 14)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New Size(105, 45)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Font = New Font("Cordia New", 16.2!)
        Me.txtSearch.Location = New Point(217, 14)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New Size(300, 45)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New Font("Cordia New", 16.2!)
        Me.lblSearch.Location = New Point(12, 17)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New Size(188, 39)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "ค้นหาเลขที่/เรื่อง:"
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.AllowUserToDeleteRows = False
        Me.grid.AllowUserToResizeRows = False
        Me.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.grid.BackgroundColor = Color.White
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        headerStyle.BackColor = SystemColors.Control
        headerStyle.Font = New Font("Cordia New", 16.2!)
        headerStyle.ForeColor = SystemColors.WindowText
        headerStyle.SelectionBackColor = SystemColors.Highlight
        headerStyle.SelectionForeColor = SystemColors.HighlightText
        headerStyle.WrapMode = DataGridViewTriState.True
        Me.grid.ColumnHeadersDefaultCellStyle = headerStyle
        Me.grid.ColumnHeadersHeight = 40
        cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        cellStyle.BackColor = SystemColors.Window
        cellStyle.Font = New Font("Cordia New", 16.2!)
        cellStyle.ForeColor = SystemColors.ControlText
        cellStyle.SelectionBackColor = SystemColors.Highlight
        cellStyle.SelectionForeColor = SystemColors.HighlightText
        cellStyle.WrapMode = DataGridViewTriState.False
        Me.grid.DefaultCellStyle = cellStyle
        Me.grid.Dock = DockStyle.Fill
        Me.grid.Location = New Point(0, 148)
        Me.grid.MultiSelect = False
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowHeadersVisible = False
        Me.grid.RowTemplate.Height = 32
        Me.grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.grid.Size = New Size(1034, 462)
        Me.grid.TabIndex = 2
        '
        'frmMemoProductPriceApprovalHistory
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New SizeF(9.0!, 32.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New Size(1034, 610)
        Me.Controls.Add(Me.grid)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.lblTitle)
        Me.Font = New Font("Cordia New", 14.0!)
        Me.MinimumSize = New Size(800, 450)
        Me.Name = "frmMemoProductPriceApprovalHistory"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "รายการอนุมัติราคาจำหน่ายสินค้าย้อนหลัง"
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.grid, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
End Class
