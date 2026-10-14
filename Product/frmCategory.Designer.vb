<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCategory
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnCancelCategory = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatusFilter = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.dgvCategory = New System.Windows.Forms.DataGridView()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        CType(Me.dgvCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1280, 78)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(22, 6)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(217, 46)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "ประเภทสินค้า"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Cordia New", 12.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(219, 234, 254)
        Me.lblSubtitle.Location = New System.Drawing.Point(26, 48)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(251, 27)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "ค้นหา เพิ่ม แก้ไข และยกเลิกประเภทสินค้า"
        '
        'pnlFilter
        '
        Me.pnlFilter.BackColor = System.Drawing.Color.White
        Me.pnlFilter.Controls.Add(Me.btnClose)
        Me.pnlFilter.Controls.Add(Me.btnCancelCategory)
        Me.pnlFilter.Controls.Add(Me.btnEdit)
        Me.pnlFilter.Controls.Add(Me.btnAdd)
        Me.pnlFilter.Controls.Add(Me.btnSearch)
        Me.pnlFilter.Controls.Add(Me.cboStatus)
        Me.pnlFilter.Controls.Add(Me.lblStatusFilter)
        Me.pnlFilter.Controls.Add(Me.txtSearch)
        Me.pnlFilter.Controls.Add(Me.lblSearch)
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilter.Location = New System.Drawing.Point(0, 78)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(1280, 104)
        Me.pnlFilter.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Cordia New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(20, 8)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(125, 31)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "ชื่อประเภทสินค้า"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.txtSearch.Location = New System.Drawing.Point(22, 40)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(330, 41)
        Me.txtSearch.TabIndex = 1
        '
        'lblStatusFilter
        '
        Me.lblStatusFilter.AutoSize = True
        Me.lblStatusFilter.Font = New System.Drawing.Font("Cordia New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusFilter.Location = New System.Drawing.Point(368, 8)
        Me.lblStatusFilter.Name = "lblStatusFilter"
        Me.lblStatusFilter.Size = New System.Drawing.Size(57, 31)
        Me.lblStatusFilter.TabIndex = 2
        Me.lblStatusFilter.Text = "สถานะ"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(370, 40)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(220, 42)
        Me.cboStatus.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(59, 130, 246)
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(606, 39)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 44)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(20, 184, 166)
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnAdd.ForeColor = System.Drawing.Color.White
        Me.btnAdd.Location = New System.Drawing.Point(750, 39)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(112, 44)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "เพิ่ม"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(14, 165, 233)
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnEdit.ForeColor = System.Drawing.Color.White
        Me.btnEdit.Location = New System.Drawing.Point(870, 39)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(112, 44)
        Me.btnEdit.TabIndex = 6
        Me.btnEdit.Text = "แก้ไข"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnCancelCategory
        '
        Me.btnCancelCategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelCategory.BackColor = System.Drawing.Color.FromArgb(239, 68, 68)
        Me.btnCancelCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelCategory.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelCategory.ForeColor = System.Drawing.Color.White
        Me.btnCancelCategory.Location = New System.Drawing.Point(990, 39)
        Me.btnCancelCategory.Name = "btnCancelCategory"
        Me.btnCancelCategory.Size = New System.Drawing.Size(140, 44)
        Me.btnCancelCategory.TabIndex = 7
        Me.btnCancelCategory.Text = "ยกเลิกการใช้"
        Me.btnCancelCategory.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105)
        Me.btnClose.Location = New System.Drawing.Point(1138, 39)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(120, 44)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'dgvCategory
        '
        Me.dgvCategory.AllowUserToAddRows = False
        Me.dgvCategory.AllowUserToDeleteRows = False
        Me.dgvCategory.AllowUserToResizeRows = False
        Me.dgvCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCategory.BackgroundColor = System.Drawing.Color.White
        Me.dgvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvCategory.ColumnHeadersHeight = 46
        Me.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCategory.EnableHeadersVisualStyles = False
        Me.dgvCategory.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.dgvCategory.Location = New System.Drawing.Point(0, 182)
        Me.dgvCategory.MultiSelect = True
        Me.dgvCategory.Name = "dgvCategory"
        Me.dgvCategory.ReadOnly = True
        Me.dgvCategory.RowHeadersVisible = False
        Me.dgvCategory.RowTemplate.Height = 36
        Me.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCategory.Size = New System.Drawing.Size(1280, 512)
        Me.dgvCategory.TabIndex = 2
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.FromArgb(241, 245, 249)
        Me.pnlStatus.Controls.Add(Me.lblCount)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStatus.Location = New System.Drawing.Point(0, 694)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(1280, 42)
        Me.pnlStatus.TabIndex = 3
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Cordia New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblCount.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85)
        Me.lblCount.Location = New System.Drawing.Point(20, 7)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(66, 31)
        Me.lblCount.TabIndex = 0
        Me.lblCount.Text = "0 รายการ"
        '
        'frmCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1280, 736)
        Me.Controls.Add(Me.dgvCategory)
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.pnlFilter)
        Me.Controls.Add(Me.pnlHeader)
        Me.MinimumSize = New System.Drawing.Size(1100, 650)
        Me.Name = "frmCategory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ประเภทสินค้า"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        CType(Me.dgvCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents pnlFilter As Panel
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblStatusFilter As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnCancelCategory As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents dgvCategory As DataGridView
    Friend WithEvents pnlStatus As Panel
    Friend WithEvents lblCount As Label
End Class
