<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMaterialUsage
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents rootLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents titleLabel As System.Windows.Forms.Label
    Friend WithEvents filterPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents branchLabel As System.Windows.Forms.Label
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents monthLabel As System.Windows.Forms.Label
    Friend WithEvents cboMonth As System.Windows.Forms.ComboBox
    Friend WithEvents yearLabel As System.Windows.Forms.Label
    Friend WithEvents nudYear As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnAutoAdjust As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents infoPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblDoc As System.Windows.Forms.Label
    Friend WithEvents lblSummary As System.Windows.Forms.Label
    Friend WithEvents grid As System.Windows.Forms.DataGridView

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
        Me.rootLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.filterPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.branchLabel = New System.Windows.Forms.Label()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.monthLabel = New System.Windows.Forms.Label()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me.yearLabel = New System.Windows.Forms.Label()
        Me.nudYear = New System.Windows.Forms.NumericUpDown()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAutoAdjust = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.infoPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDoc = New System.Windows.Forms.Label()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.rootLayout.SuspendLayout()
        Me.filterPanel.SuspendLayout()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.infoPanel.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rootLayout
        '
        Me.rootLayout.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.rootLayout.ColumnCount = 1
        Me.rootLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.rootLayout.Controls.Add(Me.titleLabel, 0, 0)
        Me.rootLayout.Controls.Add(Me.filterPanel, 0, 1)
        Me.rootLayout.Controls.Add(Me.infoPanel, 0, 2)
        Me.rootLayout.Controls.Add(Me.grid, 0, 3)
        Me.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rootLayout.Location = New System.Drawing.Point(0, 0)
        Me.rootLayout.Name = "rootLayout"
        Me.rootLayout.Padding = New System.Windows.Forms.Padding(16)
        Me.rootLayout.RowCount = 4
        Me.rootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.rootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.rootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.rootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.rootLayout.Size = New System.Drawing.Size(1200, 720)
        Me.rootLayout.TabIndex = 0
        '
        'titleLabel
        '
        Me.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.titleLabel.Font = New System.Drawing.Font("Cordia New", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.titleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.titleLabel.Location = New System.Drawing.Point(19, 16)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(1162, 58)
        Me.titleLabel.TabIndex = 0
        Me.titleLabel.Text = "คำนวณการใช้วัตถุดิบรายเดือน"
        Me.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'filterPanel
        '
        Me.filterPanel.Controls.Add(Me.branchLabel)
        Me.filterPanel.Controls.Add(Me.cboBranch)
        Me.filterPanel.Controls.Add(Me.monthLabel)
        Me.filterPanel.Controls.Add(Me.cboMonth)
        Me.filterPanel.Controls.Add(Me.yearLabel)
        Me.filterPanel.Controls.Add(Me.nudYear)
        Me.filterPanel.Controls.Add(Me.btnSearch)
        Me.filterPanel.Controls.Add(Me.btnAutoAdjust)
        Me.filterPanel.Controls.Add(Me.btnRefresh)
        Me.filterPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.filterPanel.Location = New System.Drawing.Point(19, 77)
        Me.filterPanel.Name = "filterPanel"
        Me.filterPanel.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.filterPanel.Size = New System.Drawing.Size(1162, 52)
        Me.filterPanel.TabIndex = 1
        Me.filterPanel.WrapContents = False
        '
        'branchLabel
        '
        Me.branchLabel.AutoSize = True
        Me.branchLabel.Location = New System.Drawing.Point(0, 14)
        Me.branchLabel.Margin = New System.Windows.Forms.Padding(0, 8, 6, 0)
        Me.branchLabel.Name = "branchLabel"
        Me.branchLabel.Size = New System.Drawing.Size(56, 37)
        Me.branchLabel.TabIndex = 0
        Me.branchLabel.Text = "สาขา"
        '
        'cboBranch
        '
        Me.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBranch.Location = New System.Drawing.Point(62, 6)
        Me.cboBranch.Margin = New System.Windows.Forms.Padding(0)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(210, 45)
        Me.cboBranch.TabIndex = 1
        '
        'monthLabel
        '
        Me.monthLabel.AutoSize = True
        Me.monthLabel.Location = New System.Drawing.Point(290, 14)
        Me.monthLabel.Margin = New System.Windows.Forms.Padding(18, 8, 6, 0)
        Me.monthLabel.Name = "monthLabel"
        Me.monthLabel.Size = New System.Drawing.Size(56, 37)
        Me.monthLabel.TabIndex = 2
        Me.monthLabel.Text = "เดือน"
        '
        'cboMonth
        '
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.Location = New System.Drawing.Point(352, 6)
        Me.cboMonth.Margin = New System.Windows.Forms.Padding(0)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.Size = New System.Drawing.Size(145, 45)
        Me.cboMonth.TabIndex = 3
        '
        'yearLabel
        '
        Me.yearLabel.AutoSize = True
        Me.yearLabel.Location = New System.Drawing.Point(515, 14)
        Me.yearLabel.Margin = New System.Windows.Forms.Padding(18, 8, 6, 0)
        Me.yearLabel.Name = "yearLabel"
        Me.yearLabel.Size = New System.Drawing.Size(66, 37)
        Me.yearLabel.TabIndex = 4
        Me.yearLabel.Text = "ปี ค.ศ."
        '
        'nudYear
        '
        Me.nudYear.Location = New System.Drawing.Point(590, 9)
        Me.nudYear.Maximum = New Decimal(New Integer() {2100, 0, 0, 0})
        Me.nudYear.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.nudYear.Name = "nudYear"
        Me.nudYear.Size = New System.Drawing.Size(90, 44)
        Me.nudYear.TabIndex = 5
        Me.nudYear.Value = New Decimal(New Integer() {2026, 0, 0, 0})
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(701, 6)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(110, 40)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnAutoAdjust
        '
        Me.btnAutoAdjust.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(132, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnAutoAdjust.FlatAppearance.BorderSize = 0
        Me.btnAutoAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAutoAdjust.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAutoAdjust.ForeColor = System.Drawing.Color.White
        Me.btnAutoAdjust.Location = New System.Drawing.Point(829, 6)
        Me.btnAutoAdjust.Margin = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.btnAutoAdjust.Name = "btnAutoAdjust"
        Me.btnAutoAdjust.Size = New System.Drawing.Size(250, 40)
        Me.btnAutoAdjust.TabIndex = 7
        Me.btnAutoAdjust.Text = "สร้าง/ปรับปรุงเอกสารตัดใช้"
        Me.btnAutoAdjust.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1097, 6)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 40)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "รีเฟรช"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        '
        'infoPanel
        '
        Me.infoPanel.Controls.Add(Me.lblDoc)
        Me.infoPanel.Controls.Add(Me.lblSummary)
        Me.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.infoPanel.Location = New System.Drawing.Point(19, 135)
        Me.infoPanel.Name = "infoPanel"
        Me.infoPanel.Size = New System.Drawing.Size(1162, 36)
        Me.infoPanel.TabIndex = 2
        Me.infoPanel.WrapContents = False
        '
        'lblDoc
        '
        Me.lblDoc.AutoSize = True
        Me.lblDoc.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDoc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblDoc.Location = New System.Drawing.Point(3, 0)
        Me.lblDoc.Name = "lblDoc"
        Me.lblDoc.Size = New System.Drawing.Size(0, 33)
        Me.lblDoc.TabIndex = 0
        '
        'lblSummary
        '
        Me.lblSummary.AutoSize = True
        Me.lblSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.lblSummary.Location = New System.Drawing.Point(34, 0)
        Me.lblSummary.Margin = New System.Windows.Forms.Padding(28, 0, 0, 0)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(0, 37)
        Me.lblSummary.TabIndex = 1
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.AllowUserToDeleteRows = False
        Me.grid.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(130, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grid.ColumnHeadersHeight = 42
        Me.grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid.EnableHeadersVisualStyles = False
        Me.grid.Location = New System.Drawing.Point(19, 177)
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowHeadersVisible = False
        Me.grid.RowHeadersWidth = 51
        Me.grid.RowTemplate.Height = 34
        Me.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grid.Size = New System.Drawing.Size(1162, 524)
        Me.grid.TabIndex = 3
        '
        'frmMaterialUsage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 720)
        Me.Controls.Add(Me.rootLayout)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "frmMaterialUsage"
        Me.Text = "คำนวณการใช้วัตถุดิบรายเดือน"
        Me.rootLayout.ResumeLayout(False)
        Me.filterPanel.ResumeLayout(False)
        Me.filterPanel.PerformLayout()
        CType(Me.nudYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.infoPanel.ResumeLayout(False)
        Me.infoPanel.PerformLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
