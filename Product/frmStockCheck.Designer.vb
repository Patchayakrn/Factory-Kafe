<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStockCheck
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblYear As Label
    Friend WithEvents nudYear As NumericUpDown
    Friend WithEvents lblMonth As Label
    Friend WithEvents cboMonth As ComboBox
    Friend WithEvents lblMaterialID As Label
    Friend WithEvents txtMaterialID As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnAddAll As Button
    Friend WithEvents lblFilter As Label
    Friend WithEvents txtFilter As TextBox
    Friend WithEvents btnLoad As Button
    Friend WithEvents dgvStock As DataGridView
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents pnlActions As FlowLayoutPanel
    Friend WithEvents lblSummary As Label
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnClose As Button

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
        Me.components = New System.ComponentModel.Container()
        Me.pnlHeader = New Panel()
        Me.lblTitle = New Label()
        Me.lblYear = New Label()
        Me.nudYear = New NumericUpDown()
        Me.lblMonth = New Label()
        Me.cboMonth = New ComboBox()
        Me.lblMaterialID = New Label()
        Me.txtMaterialID = New TextBox()
        Me.btnAdd = New Button()
        Me.btnAddAll = New Button()
        Me.lblFilter = New Label()
        Me.txtFilter = New TextBox()
        Me.btnLoad = New Button()
        Me.dgvStock = New DataGridView()
        Me.pnlFooter = New Panel()
        Me.pnlActions = New FlowLayoutPanel()
        Me.lblSummary = New Label()
        Me.btnDelete = New Button()
        Me.btnSave = New Button()
        Me.btnClose = New Button()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlHeader.BackColor = Color.FromArgb(225, 240, 252)
        Me.pnlHeader.Dock = DockStyle.Top
        Me.pnlHeader.Height = 158
        Me.pnlHeader.Padding = New Padding(18, 8, 18, 8)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblYear)
        Me.pnlHeader.Controls.Add(Me.nudYear)
        Me.pnlHeader.Controls.Add(Me.lblMonth)
        Me.pnlHeader.Controls.Add(Me.cboMonth)
        Me.pnlHeader.Controls.Add(Me.lblMaterialID)
        Me.pnlHeader.Controls.Add(Me.txtMaterialID)
        Me.pnlHeader.Controls.Add(Me.btnAdd)
        Me.pnlHeader.Controls.Add(Me.btnAddAll)
        Me.pnlHeader.Controls.Add(Me.lblFilter)
        Me.pnlHeader.Controls.Add(Me.txtFilter)
        Me.pnlHeader.Controls.Add(Me.btnLoad)

        Me.lblTitle.AutoSize = False
        Me.lblTitle.Location = New Point(18, 8)
        Me.lblTitle.Size = New Size(700, 40)
        Me.lblTitle.Text = "บันทึกผลตรวจสต็อกวัตถุดิบ"
        Me.lblTitle.Font = New Font("Cordia New", 22.0!, FontStyle.Bold)
        Me.lblTitle.ForeColor = Color.FromArgb(30, 82, 130)
        Me.lblTitle.TextAlign = ContentAlignment.MiddleLeft

        Me.lblYear.AutoSize = False
        Me.lblYear.Location = New Point(18, 58)
        Me.lblYear.Size = New Size(70, 32)
        Me.lblYear.Text = "ปี พ.ศ."
        Me.lblYear.TextAlign = ContentAlignment.MiddleLeft
        Me.nudYear.Location = New Point(88, 58)
        Me.nudYear.Maximum = 3000D
        Me.nudYear.Minimum = 2000D
        Me.nudYear.Size = New Size(120, 32)
        Me.nudYear.TabIndex = 1
        Me.nudYear.TextAlign = HorizontalAlignment.Right

        Me.lblMonth.AutoSize = False
        Me.lblMonth.Location = New Point(226, 58)
        Me.lblMonth.Size = New Size(70, 32)
        Me.lblMonth.Text = "เดือน"
        Me.lblMonth.TextAlign = ContentAlignment.MiddleLeft
        Me.cboMonth.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboMonth.Location = New Point(296, 58)
        Me.cboMonth.Size = New Size(190, 32)
        Me.cboMonth.TabIndex = 2

        Me.lblMaterialID.AutoSize = False
        Me.lblMaterialID.Location = New Point(18, 108)
        Me.lblMaterialID.Size = New Size(140, 32)
        Me.lblMaterialID.Text = "รหัส/รายการวัตถุดิบ"
        Me.lblMaterialID.TextAlign = ContentAlignment.MiddleLeft
        Me.txtMaterialID.Location = New Point(158, 108)
        Me.txtMaterialID.Size = New Size(300, 32)
        Me.txtMaterialID.TabIndex = 3
        Me.btnAdd.BackColor = Color.FromArgb(42, 126, 189)
        Me.btnAdd.FlatStyle = FlatStyle.Flat
        Me.btnAdd.ForeColor = Color.White
        Me.btnAdd.Location = New Point(470, 105)
        Me.btnAdd.Size = New Size(110, 38)
        Me.btnAdd.Text = "เพิ่มรายการ"
        Me.btnAdd.UseVisualStyleBackColor = False
        Me.btnAddAll.BackColor = Color.FromArgb(91, 106, 126)
        Me.btnAddAll.FlatStyle = FlatStyle.Flat
        Me.btnAddAll.ForeColor = Color.White
        Me.btnAddAll.Location = New Point(590, 105)
        Me.btnAddAll.Size = New Size(170, 38)
        Me.btnAddAll.Text = "เพิ่มวัตถุดิบทั้งหมด"
        Me.btnAddAll.UseVisualStyleBackColor = False

        Me.lblFilter.AutoSize = False
        Me.lblFilter.Location = New Point(790, 58)
        Me.lblFilter.Size = New Size(80, 32)
        Me.lblFilter.Text = "ค้นหา"
        Me.lblFilter.TextAlign = ContentAlignment.MiddleLeft
        Me.txtFilter.Location = New Point(870, 58)
        Me.txtFilter.Size = New Size(260, 32)
        Me.txtFilter.TabIndex = 4
        Me.btnLoad.BackColor = Color.ForestGreen
        Me.btnLoad.FlatStyle = FlatStyle.Flat
        Me.btnLoad.ForeColor = Color.White
        Me.btnLoad.Location = New Point(1145, 55)
        Me.btnLoad.Size = New Size(110, 38)
        Me.btnLoad.Text = "โหลดข้อมูล"
        Me.btnLoad.UseVisualStyleBackColor = False

        Me.dgvStock.AllowUserToAddRows = False
        Me.dgvStock.AllowUserToDeleteRows = False
        Me.dgvStock.AllowUserToResizeRows = False
        Me.dgvStock.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvStock.ColumnHeadersHeight = 44
        Me.dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvStock.Dock = DockStyle.Fill
        Me.dgvStock.Location = New Point(0, 158)
        Me.dgvStock.Name = "dgvStock"
        Me.dgvStock.RowHeadersVisible = False
        Me.dgvStock.TabIndex = 2

        Me.pnlFooter.BackColor = Color.FromArgb(238, 245, 252)
        Me.pnlFooter.Dock = DockStyle.Bottom
        Me.pnlFooter.Height = 64
        Me.pnlFooter.Padding = New Padding(12, 8, 12, 8)
        Me.pnlFooter.Controls.Add(Me.lblSummary)
        Me.pnlFooter.Controls.Add(Me.pnlActions)
        Me.pnlActions.Dock = DockStyle.Right
        Me.pnlActions.FlowDirection = FlowDirection.RightToLeft
        Me.pnlActions.WrapContents = False
        Me.pnlActions.Padding = New Padding(0)
        Me.pnlActions.Margin = New Padding(0)
        Me.pnlActions.Size = New Size(405, 48)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Controls.Add(Me.btnSave)
        Me.pnlActions.Controls.Add(Me.btnDelete)
        Me.lblSummary.AutoSize = False
        Me.lblSummary.Dock = DockStyle.Fill
        Me.lblSummary.Size = New Size(500, 48)
        Me.lblSummary.Text = "0 รายการ"
        Me.lblSummary.TextAlign = ContentAlignment.MiddleLeft
        Me.btnDelete.Dock = DockStyle.None
        Me.btnDelete.BackColor = Color.FromArgb(190, 74, 74)
        Me.btnDelete.FlatStyle = FlatStyle.Flat
        Me.btnDelete.ForeColor = Color.White
        Me.btnDelete.Margin = New Padding(4, 0, 4, 0)
        Me.btnDelete.Size = New Size(125, 44)
        Me.btnDelete.Text = "ลบแถว"
        Me.btnDelete.UseVisualStyleBackColor = False
        Me.btnSave.Dock = DockStyle.None
        Me.btnSave.BackColor = Color.ForestGreen
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.ForeColor = Color.White
        Me.btnSave.Margin = New Padding(4, 0, 4, 0)
        Me.btnSave.Size = New Size(125, 44)
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        Me.btnClose.Dock = DockStyle.None
        Me.btnClose.BackColor = Color.FromArgb(105, 105, 105)
        Me.btnClose.FlatStyle = FlatStyle.Flat
        Me.btnClose.ForeColor = Color.White
        Me.btnClose.Margin = New Padding(4, 0, 4, 0)
        Me.btnClose.Size = New Size(125, 44)
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False

        Me.AutoScaleDimensions = New SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.BackColor = Color.FromArgb(238, 245, 252)
        Me.ClientSize = New Size(1280, 760)
        Me.Controls.Add(Me.dgvStock)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New Font("Cordia New", 16.0!)
        Me.MinimumSize = New Size(1000, 600)
        Me.Name = "frmStockCheck"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "บันทึกผลตรวจสต็อกวัตถุดิบ"
        Me.WindowState = FormWindowState.Maximized

        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlFooter.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
End Class
