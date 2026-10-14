<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVatSell
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblMonth As Label
    Friend WithEvents lblBranch As Label
    Friend WithEvents lblViewType As Label
    Friend WithEvents lblCompany As Label
    Friend WithEvents dpMonth As DateTimePicker
    Friend WithEvents cbBranch As ComboBox
    Friend WithEvents cbViewType As ComboBox
    Friend WithEvents cbCompany As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents lblSummary As Label
    Friend WithEvents lblCount As Label
    Friend WithEvents grid As DevExpress.XtraGrid.GridControl
    Friend WithEvents view As DevExpress.XtraGrid.Views.Grid.GridView

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblViewType = New System.Windows.Forms.Label()
        Me.cbViewType = New System.Windows.Forms.ComboBox()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.dpMonth = New System.Windows.Forms.DateTimePicker()
        Me.lblBranch = New System.Windows.Forms.Label()
        Me.cbBranch = New System.Windows.Forms.ComboBox()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.cbCompany = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.grid = New DevExpress.XtraGrid.GridControl()
        Me.view = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlTop.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.view, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Controls.Add(Me.lblViewType)
        Me.pnlTop.Controls.Add(Me.cbViewType)
        Me.pnlTop.Controls.Add(Me.lblMonth)
        Me.pnlTop.Controls.Add(Me.dpMonth)
        Me.pnlTop.Controls.Add(Me.lblBranch)
        Me.pnlTop.Controls.Add(Me.cbBranch)
        Me.pnlTop.Controls.Add(Me.lblCompany)
        Me.pnlTop.Controls.Add(Me.cbCompany)
        Me.pnlTop.Controls.Add(Me.btnSearch)
        Me.pnlTop.Controls.Add(Me.btnPrint)
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Controls.Add(Me.lblSummary)
        Me.pnlTop.Controls.Add(Me.lblCount)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlTop.Size = New System.Drawing.Size(1420, 173)
        Me.pnlTop.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Indigo
        Me.lblTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(350, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Vat Sell (รายการภาษีขาย)"
        '
        'lblViewType
        '
        Me.lblViewType.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewType.Location = New System.Drawing.Point(13, 50)
        Me.lblViewType.Name = "lblViewType"
        Me.lblViewType.Size = New System.Drawing.Size(120, 22)
        Me.lblViewType.TabIndex = 1
        Me.lblViewType.Text = "VAT Type"
        '
        'cbViewType
        '
        Me.cbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbViewType.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbViewType.Items.AddRange(New Object() {"เฉพาะมี VAT", "เฉพาะไม่มี VAT", "ทั้งหมด"})
        Me.cbViewType.Location = New System.Drawing.Point(15, 76)
        Me.cbViewType.Name = "cbViewType"
        Me.cbViewType.Size = New System.Drawing.Size(170, 40)
        Me.cbViewType.TabIndex = 2
        '
        'lblMonth
        '
        Me.lblMonth.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(203, 50)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(120, 22)
        Me.lblMonth.TabIndex = 3
        Me.lblMonth.Text = "Month / Year"
        '
        'dpMonth
        '
        Me.dpMonth.CustomFormat = "MMMM / yyyy"
        Me.dpMonth.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dpMonth.Location = New System.Drawing.Point(209, 77)
        Me.dpMonth.Name = "dpMonth"
        Me.dpMonth.Size = New System.Drawing.Size(170, 39)
        Me.dpMonth.TabIndex = 4
        '
        'lblBranch
        '
        Me.lblBranch.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.Location = New System.Drawing.Point(393, 50)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(120, 22)
        Me.lblBranch.TabIndex = 5
        Me.lblBranch.Text = "Branch"
        '
        'cbBranch
        '
        Me.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBranch.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBranch.Location = New System.Drawing.Point(399, 77)
        Me.cbBranch.Name = "cbBranch"
        Me.cbBranch.Size = New System.Drawing.Size(260, 40)
        Me.cbBranch.TabIndex = 6
        '
        'lblCompany
        '
        Me.lblCompany.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany.Location = New System.Drawing.Point(673, 50)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(120, 22)
        Me.lblCompany.TabIndex = 7
        Me.lblCompany.Text = "Company"
        '
        'cbCompany
        '
        Me.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCompany.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCompany.Location = New System.Drawing.Point(679, 77)
        Me.cbCompany.Name = "cbCompany"
        Me.cbCompany.Size = New System.Drawing.Size(320, 40)
        Me.cbCompany.TabIndex = 8
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(1027, 71)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(110, 45)
        Me.btnSearch.TabIndex = 9
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(1154, 71)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(110, 45)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1285, 71)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 45)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblSummary
        '
        Me.lblSummary.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSummary.Location = New System.Drawing.Point(15, 135)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(900, 26)
        Me.lblSummary.TabIndex = 12
        '
        'lblCount
        '
        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCount.Location = New System.Drawing.Point(2400, 112)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(170, 26)
        Me.lblCount.TabIndex = 13
        Me.lblCount.Text = "0 รายการ"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 173)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1420, 677)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grid)
        Me.TabPage1.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.TabPage1.Location = New System.Drawing.Point(4, 46)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1412, 627)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "รายงานปกติ"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.BackgroundColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.2!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grid.ColumnHeadersHeight = 29
        Me.grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid.Location = New System.Drawing.Point(3, 3)
        Me.grid.MainView = Me.view
        Me.grid.MultiSelect = False
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowHeadersVisible = False
        Me.grid.RowHeadersWidth = 51
        Me.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grid.Size = New System.Drawing.Size(1406, 621)
        Me.grid.TabIndex = 0
        '
        'view
        '
        Me.view.ColumnPanelRowHeight = 0
        Me.view.DetailHeight = 0
        Me.view.FocusedRowHandle = -1
        Me.view.GridControl = Me.grid
        Me.view.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.view.Name = Nothing
        Me.view.RowHeight = 0
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 46)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(1412, 627)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "รายงาน (รายเดือน)"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'frmVatSell
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1420, 850)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(1100, 650)
        Me.Name = "frmVatSell"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Vat Sell (รายงานภาษีขาย)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
End Class
