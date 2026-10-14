<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmFreeCupSelect
    Inherits Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents itemGrid As DataGridView
    Friend WithEvents lblInstruction As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnConfirm As Button
    Friend WithEvents titleLabel As Label
    Friend WithEvents buttonPanel As TableLayoutPanel

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
        Me.itemGrid = New System.Windows.Forms.DataGridView()
        Me.chooseColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.nameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.priceColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.statusColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblInstruction = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.buttonPanel = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.itemGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.buttonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'itemGrid
        '
        Me.itemGrid.AllowUserToAddRows = False
        Me.itemGrid.AllowUserToDeleteRows = False
        Me.itemGrid.AllowUserToResizeRows = False
        Me.itemGrid.AutoGenerateColumns = False
        Me.itemGrid.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.itemGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.itemGrid.ColumnHeadersHeight = 40
        Me.itemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.itemGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chooseColumn, Me.nameColumn, Me.priceColumn, Me.statusColumn})
        Me.itemGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.itemGrid.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.itemGrid.Location = New System.Drawing.Point(0, 50)
        Me.itemGrid.MultiSelect = False
        Me.itemGrid.Name = "itemGrid"
        Me.itemGrid.RowHeadersVisible = False
        Me.itemGrid.RowHeadersWidth = 51
        Me.itemGrid.RowTemplate.Height = 34
        Me.itemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.itemGrid.Size = New System.Drawing.Size(760, 354)
        Me.itemGrid.TabIndex = 0
        '
        'chooseColumn
        '
        Me.chooseColumn.MinimumWidth = 6
        Me.chooseColumn.HeaderText = "เลือก"
        Me.chooseColumn.Name = "Choose"
        Me.chooseColumn.Width = 75
        '
        'nameColumn
        '
        Me.nameColumn.MinimumWidth = 6
        Me.nameColumn.HeaderText = "เมนู"
        Me.nameColumn.Name = "MenuName"
        Me.nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Me.nameColumn.MinimumWidth = 430
        '
        'priceColumn
        '
        Me.priceColumn.MinimumWidth = 6
        Me.priceColumn.HeaderText = "ราคา"
        Me.priceColumn.Name = "Price"
        Me.priceColumn.Width = 105
        Me.priceColumn.DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2", .Alignment = DataGridViewContentAlignment.MiddleRight}
        '
        'statusColumn
        '
        Me.statusColumn.MinimumWidth = 6
        Me.statusColumn.HeaderText = "สถานะ"
        Me.statusColumn.Name = "Status"
        Me.statusColumn.Width = 145
        '
        'lblInstruction
        '
        Me.lblInstruction.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.lblInstruction.Location = New System.Drawing.Point(0, 404)
        Me.lblInstruction.Name = "lblInstruction"
        Me.lblInstruction.Size = New System.Drawing.Size(760, 38)
        Me.lblInstruction.TabIndex = 1
        Me.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(122, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(135, 42)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "ยกเลิก"
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnConfirm.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(502, 8)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(135, 42)
        Me.btnConfirm.TabIndex = 1
        Me.btnConfirm.Text = "ยืนยัน"
        Me.btnConfirm.UseVisualStyleBackColor = False
        '
        'titleLabel
        '
        Me.titleLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.titleLabel.Font = New System.Drawing.Font("Cordia New", 17.0!, System.Drawing.FontStyle.Bold)
        Me.titleLabel.Location = New System.Drawing.Point(0, 0)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Size = New System.Drawing.Size(760, 50)
        Me.titleLabel.TabIndex = 3
        Me.titleLabel.Text = "เลือกรายการที่ต้องการใช้สิทธิ์แก้วฟรี"
        Me.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'buttonPanel
        '
        Me.buttonPanel.ColumnCount = 2
        Me.buttonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.buttonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.buttonPanel.Controls.Add(Me.btnCancel, 0, 0)
        Me.buttonPanel.Controls.Add(Me.btnConfirm, 1, 0)
        Me.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.buttonPanel.Location = New System.Drawing.Point(0, 442)
        Me.buttonPanel.Name = "buttonPanel"
        Me.buttonPanel.RowCount = 1
        Me.buttonPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.buttonPanel.Size = New System.Drawing.Size(760, 58)
        Me.buttonPanel.TabIndex = 2
        '
        'FrmFreeCupSelect
        '
        Me.AcceptButton = Me.btnConfirm
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(760, 500)
        Me.Controls.Add(Me.itemGrid)
        Me.Controls.Add(Me.lblInstruction)
        Me.Controls.Add(Me.buttonPanel)
        Me.Controls.Add(Me.titleLabel)
        Me.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFreeCupSelect"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "เลือกเมนูสำหรับใช้สิทธิ์แก้วฟรี"
        CType(Me.itemGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.buttonPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents chooseColumn As DataGridViewCheckBoxColumn
    Friend WithEvents nameColumn As DataGridViewTextBoxColumn
    Friend WithEvents priceColumn As DataGridViewTextBoxColumn
    Friend WithEvents statusColumn As DataGridViewTextBoxColumn
End Class
