<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProductGroup
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
        Me.btnCancelGroup = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblStatusFilter = New System.Windows.Forms.Label()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.lblCategoryFilter = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.dgvProductGroup = New System.Windows.Forms.DataGridView()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        CType(Me.dgvProductGroup, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlHeader.Size = New System.Drawing.Size(1400, 78)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(22, 5)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(160, 46)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "กลุ่มสินค้า"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Cordia New", 12.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(219, 234, 254)
        Me.lblSubtitle.Location = New System.Drawing.Point(26, 48)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(238, 27)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "ค้นหา เพิ่ม แก้ไข และยกเลิกกลุ่มสินค้า"
        '
        'pnlFilter
        '
        Me.pnlFilter.BackColor = System.Drawing.Color.White
        Me.pnlFilter.Controls.Add(Me.btnClose)
        Me.pnlFilter.Controls.Add(Me.btnCancelGroup)
        Me.pnlFilter.Controls.Add(Me.btnEdit)
        Me.pnlFilter.Controls.Add(Me.btnAdd)
        Me.pnlFilter.Controls.Add(Me.btnSearch)
        Me.pnlFilter.Controls.Add(Me.cboStatus)
        Me.pnlFilter.Controls.Add(Me.lblStatusFilter)
        Me.pnlFilter.Controls.Add(Me.cboCategory)
        Me.pnlFilter.Controls.Add(Me.lblCategoryFilter)
        Me.pnlFilter.Controls.Add(Me.txtSearch)
        Me.pnlFilter.Controls.Add(Me.lblSearch)
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilter.Location = New System.Drawing.Point(0, 78)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(1400, 108)
        Me.pnlFilter.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Cordia New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(20, 9)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(110, 31)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "ชื่อกลุ่มสินค้า"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.txtSearch.Location = New System.Drawing.Point(22, 42)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(270, 41)
        Me.txtSearch.TabIndex = 1
        '
        'lblCategoryFilter
        '
        Me.lblCategoryFilter.AutoSize = True
        Me.lblCategoryFilter.Font = New System.Drawing.Font("Cordia New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblCategoryFilter.Location = New System.Drawing.Point(306, 9)
        Me.lblCategoryFilter.Name = "lblCategoryFilter"
        Me.lblCategoryFilter.Size = New System.Drawing.Size(100, 31)
        Me.lblCategoryFilter.TabIndex = 2
        Me.lblCategoryFilter.Text = "ประเภทสินค้า"
        '
        'cboCategory
        '
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.cboCategory.FormattingEnabled = True
        Me.cboCategory.Location = New System.Drawing.Point(308, 42)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.Size = New System.Drawing.Size(220, 42)
        Me.cboCategory.TabIndex = 3
        '
        'lblStatusFilter
        '
        Me.lblStatusFilter.AutoSize = True
        Me.lblStatusFilter.Font = New System.Drawing.Font("Cordia New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusFilter.Location = New System.Drawing.Point(542, 9)
        Me.lblStatusFilter.Name = "lblStatusFilter"
        Me.lblStatusFilter.Size = New System.Drawing.Size(57, 31)
        Me.lblStatusFilter.TabIndex = 4
        Me.lblStatusFilter.Text = "สถานะ"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(544, 42)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(205, 42)
        Me.cboStatus.TabIndex = 5
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(59, 130, 246)
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(765, 41)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(105, 44)
        Me.btnSearch.TabIndex = 6
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
        Me.btnAdd.Location = New System.Drawing.Point(878, 41)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(100, 44)
        Me.btnAdd.TabIndex = 7
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
        Me.btnEdit.Location = New System.Drawing.Point(986, 41)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(100, 44)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "แก้ไข"
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnCancelGroup
        '
        Me.btnCancelGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelGroup.BackColor = System.Drawing.Color.FromArgb(239, 68, 68)
        Me.btnCancelGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelGroup.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelGroup.ForeColor = System.Drawing.Color.White
        Me.btnCancelGroup.Location = New System.Drawing.Point(1094, 41)
        Me.btnCancelGroup.Name = "btnCancelGroup"
        Me.btnCancelGroup.Size = New System.Drawing.Size(130, 44)
        Me.btnCancelGroup.TabIndex = 9
        Me.btnCancelGroup.Text = "ยกเลิกการใช้"
        Me.btnCancelGroup.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105)
        Me.btnClose.Location = New System.Drawing.Point(1232, 41)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(90, 44)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'dgvProductGroup
        '
        Me.dgvProductGroup.AllowUserToAddRows = False
        Me.dgvProductGroup.AllowUserToDeleteRows = False
        Me.dgvProductGroup.AllowUserToResizeRows = False
        Me.dgvProductGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProductGroup.BackgroundColor = System.Drawing.Color.White
        Me.dgvProductGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvProductGroup.ColumnHeadersHeight = 46
        Me.dgvProductGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvProductGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProductGroup.EnableHeadersVisualStyles = False
        Me.dgvProductGroup.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.dgvProductGroup.Location = New System.Drawing.Point(0, 186)
        Me.dgvProductGroup.MultiSelect = True
        Me.dgvProductGroup.Name = "dgvProductGroup"
        Me.dgvProductGroup.ReadOnly = True
        Me.dgvProductGroup.RowHeadersVisible = False
        Me.dgvProductGroup.RowTemplate.Height = 36
        Me.dgvProductGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProductGroup.Size = New System.Drawing.Size(1400, 532)
        Me.dgvProductGroup.TabIndex = 2
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.FromArgb(241, 245, 249)
        Me.pnlStatus.Controls.Add(Me.lblCount)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStatus.Location = New System.Drawing.Point(0, 718)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(1400, 42)
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
        'frmProductGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1400, 760)
        Me.Controls.Add(Me.dgvProductGroup)
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.pnlFilter)
        Me.Controls.Add(Me.pnlHeader)
        Me.MinimumSize = New System.Drawing.Size(1380, 650)
        Me.Name = "frmProductGroup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "กลุ่มสินค้า"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        CType(Me.dgvProductGroup, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblCategoryFilter As Label
    Friend WithEvents cboCategory As ComboBox
    Friend WithEvents lblStatusFilter As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnCancelGroup As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents dgvProductGroup As DataGridView
    Friend WithEvents pnlStatus As Panel
    Friend WithEvents lblCount As Label
End Class
