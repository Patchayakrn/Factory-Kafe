<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fOfficer
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer
    Friend WithEvents searchPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDisable As System.Windows.Forms.Button
    Friend WithEvents btnAccessLevel As System.Windows.Forms.Button
    Friend WithEvents btnLimitSearch As System.Windows.Forms.Button
    Friend WithEvents btnUserPassword As System.Windows.Forms.Button
    Friend WithEvents countSpacer As System.Windows.Forms.Panel
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents grid As System.Windows.Forms.DataGridView

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.searchPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDisable = New System.Windows.Forms.Button()
        Me.btnAccessLevel = New System.Windows.Forms.Button()
        Me.btnLimitSearch = New System.Windows.Forms.Button()
        Me.btnUserPassword = New System.Windows.Forms.Button()
        Me.countSpacer = New System.Windows.Forms.Panel()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.searchPanel.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'searchPanel
        '
        Me.searchPanel.Controls.Add(Me.lblSearch)
        Me.searchPanel.Controls.Add(Me.txtSearch)
        Me.searchPanel.Controls.Add(Me.cboStatus)
        Me.searchPanel.Controls.Add(Me.btnSearch)
        Me.searchPanel.Controls.Add(Me.btnAdd)
        Me.searchPanel.Controls.Add(Me.btnEdit)
        Me.searchPanel.Controls.Add(Me.btnDisable)
        Me.searchPanel.Controls.Add(Me.btnAccessLevel)
        Me.searchPanel.Controls.Add(Me.btnLimitSearch)
        Me.searchPanel.Controls.Add(Me.btnUserPassword)
        Me.searchPanel.Controls.Add(Me.countSpacer)
        Me.searchPanel.Controls.Add(Me.lblCount)
        Me.searchPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.searchPanel.Location = New System.Drawing.Point(0, 0)
        Me.searchPanel.Name = "searchPanel"
        Me.searchPanel.Padding = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.searchPanel.Size = New System.Drawing.Size(1353, 67)
        Me.searchPanel.TabIndex = 1
        Me.searchPanel.WrapContents = False
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(12, 16)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(3, 7, 5, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(68, 39)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "ค้นหา:"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(85, 11)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(0, 2, 9, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(297, 45)
        Me.txtSearch.TabIndex = 1
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.Items.AddRange(New Object() {"ใช้งานอยู่", "ไม่ใช้งานแล้ว", "ทั้งหมด"})
        Me.cboStatus.Location = New System.Drawing.Point(391, 11)
        Me.cboStatus.Margin = New System.Windows.Forms.Padding(0, 2, 9, 0)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(159, 45)
        Me.cboStatus.TabIndex = 2
        Me.cboStatus.SelectedIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(559, 10)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(0, 1, 5, 0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(103, 45)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(681, 10)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(14, 1, 5, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(103, 45)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "เพิ่ม"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(789, 10)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(0, 1, 5, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(103, 45)
        Me.btnEdit.TabIndex = 5
        Me.btnEdit.Text = "แก้ไข"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnDisable
        '
        Me.btnDisable.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDisable.Location = New System.Drawing.Point(911, 10)
        Me.btnDisable.Margin = New System.Windows.Forms.Padding(0, 1, 5, 0)
        Me.btnDisable.Name = "btnDisable"
        Me.btnDisable.Size = New System.Drawing.Size(153, 45)
        Me.btnDisable.TabIndex = 6
        Me.btnDisable.Text = "ยกเลิกการใช้งาน"
        Me.btnDisable.UseVisualStyleBackColor = True
        '
        'btnAccessLevel
        '
        Me.btnAccessLevel.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccessLevel.Location = New System.Drawing.Point(1069, 10)
        Me.btnAccessLevel.Margin = New System.Windows.Forms.Padding(0, 1, 5, 0)
        Me.btnAccessLevel.Name = "btnAccessLevel"
        Me.btnAccessLevel.Size = New System.Drawing.Size(130, 45)
        Me.btnAccessLevel.TabIndex = 8
        Me.btnAccessLevel.Text = "กำหนดสิทธิ์"
        Me.btnAccessLevel.UseVisualStyleBackColor = True
        '
        'btnLimitSearch
        '
        Me.btnLimitSearch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimitSearch.Location = New System.Drawing.Point(1204, 10)
        Me.btnLimitSearch.Margin = New System.Windows.Forms.Padding(0, 1, 5, 0)
        Me.btnLimitSearch.Name = "btnLimitSearch"
        Me.btnLimitSearch.Size = New System.Drawing.Size(155, 45)
        Me.btnLimitSearch.TabIndex = 9
        Me.btnLimitSearch.Text = "จำกัดวันย้อนหลัง"
        Me.btnLimitSearch.UseVisualStyleBackColor = True
        '
        'btnUserPassword
        '
        Me.btnUserPassword.Font = New System.Drawing.Font("Cordia New", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUserPassword.Location = New System.Drawing.Point(1364, 10)
        Me.btnUserPassword.Margin = New System.Windows.Forms.Padding(0, 1, 5, 0)
        Me.btnUserPassword.Name = "btnUserPassword"
        Me.btnUserPassword.Size = New System.Drawing.Size(170, 45)
        Me.btnUserPassword.TabIndex = 10
        Me.btnUserPassword.Text = "ตั้ง User/Password"
        Me.btnUserPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnUserPassword.UseVisualStyleBackColor = True
        '
        'countSpacer
        '
        Me.countSpacer.Location = New System.Drawing.Point(1364, 10)
        Me.countSpacer.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.countSpacer.Name = "countSpacer"
        Me.countSpacer.Size = New System.Drawing.Size(10, 45)
        Me.countSpacer.TabIndex = 7
        '
        'lblCount
        '
        Me.lblCount.AutoSize = False
        Me.lblCount.Location = New System.Drawing.Point(1374, 10)
        Me.lblCount.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(150, 45)
        Me.lblCount.TabIndex = 10
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCount.BackColor = System.Drawing.Color.Yellow
        Me.lblCount.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.AllowUserToDeleteRows = False
        Me.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grid.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid.ColumnHeadersHeight = 40
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 16.2!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grid.DefaultCellStyle = DataGridViewCellStyle2
        Me.grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid.Location = New System.Drawing.Point(0, 67)
        Me.grid.MultiSelect = False
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowTemplate.Height = 40
        Me.grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None
        Me.grid.RowHeadersWidth = 51
        Me.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grid.Size = New System.Drawing.Size(1353, 638)
        Me.grid.TabIndex = 0
        '
        'fOfficer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1353, 705)
        Me.Controls.Add(Me.grid)
        Me.Controls.Add(Me.searchPanel)
        Me.MinimumSize = New System.Drawing.Size(912, 530)
        Me.Name = "fOfficer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "พนักงาน (Officer)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.searchPanel.ResumeLayout(False)
        Me.searchPanel.PerformLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
