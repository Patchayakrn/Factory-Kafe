<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fCustomerFreeCupHistory
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
    Friend WithEvents titleLabel As System.Windows.Forms.Label
    Friend WithEvents summaryLabel As System.Windows.Forms.Label
    Friend WithEvents grid As System.Windows.Forms.DataGridView
    Friend WithEvents bottomPanel As System.Windows.Forms.Panel
    Friend WithEvents closeButton As System.Windows.Forms.Button
    Friend WithEvents EventTimeColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SellNumberColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CupInColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FreeCupInColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FreeCupOutColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CupBalanceColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FreeCupBalanceColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DetailColumn As System.Windows.Forms.DataGridViewTextBoxColumn

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.titleLabel = New System.Windows.Forms.Label()
        Me.summaryLabel = New System.Windows.Forms.Label()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.bottomPanel = New System.Windows.Forms.Panel()
        Me.closeButton = New System.Windows.Forms.Button()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bottomPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'titleLabel
        '
        Me.titleLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.titleLabel.Font = New System.Drawing.Font("Cordia New", 20.0!, System.Drawing.FontStyle.Bold)
        Me.titleLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.titleLabel.Location = New System.Drawing.Point(0, 0)
        Me.titleLabel.Name = "titleLabel"
        Me.titleLabel.Padding = New System.Windows.Forms.Padding(16, 8, 0, 0)
        Me.titleLabel.Size = New System.Drawing.Size(1180, 58)
        Me.titleLabel.TabIndex = 2
        Me.titleLabel.Text = "ประวัติแต้มและสิทธิ์แก้วฟรี"
        '
        'summaryLabel
        '
        Me.summaryLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.summaryLabel.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.summaryLabel.ForeColor = System.Drawing.Color.DarkGreen
        Me.summaryLabel.Location = New System.Drawing.Point(0, 58)
        Me.summaryLabel.Name = "summaryLabel"
        Me.summaryLabel.Padding = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.summaryLabel.Size = New System.Drawing.Size(1180, 48)
        Me.summaryLabel.TabIndex = 1
        Me.summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.AllowUserToDeleteRows = False
        Me.grid.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.grid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grid.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grid.ColumnHeadersHeight = 48
        Me.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cordia New", 16.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grid.DefaultCellStyle = DataGridViewCellStyle3
        Me.grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid.EnableHeadersVisualStyles = False
        Me.grid.Location = New System.Drawing.Point(0, 106)
        Me.grid.MultiSelect = False
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowHeadersVisible = False
        Me.grid.RowHeadersWidth = 51
        Me.grid.RowTemplate.Height = 34
        Me.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grid.Size = New System.Drawing.Size(1180, 486)
        Me.grid.TabIndex = 0
        '
        'bottomPanel
        '
        Me.bottomPanel.Controls.Add(Me.closeButton)
        Me.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.bottomPanel.Location = New System.Drawing.Point(0, 592)
        Me.bottomPanel.Name = "bottomPanel"
        Me.bottomPanel.Size = New System.Drawing.Size(1180, 58)
        Me.bottomPanel.TabIndex = 3
        '
        'closeButton
        '
        Me.closeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.closeButton.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold)
        Me.closeButton.Location = New System.Drawing.Point(2015, 9)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(130, 40)
        Me.closeButton.TabIndex = 0
        Me.closeButton.Text = "ปิด"
        Me.closeButton.UseVisualStyleBackColor = True
        '
        'fCustomerFreeCupHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.closeButton
        Me.ClientSize = New System.Drawing.Size(1180, 650)
        Me.Controls.Add(Me.grid)
        Me.Controls.Add(Me.summaryLabel)
        Me.Controls.Add(Me.titleLabel)
        Me.Controls.Add(Me.bottomPanel)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.MinimumSize = New System.Drawing.Size(980, 520)
        Me.Name = "fCustomerFreeCupHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ประวัติแต้มและสิทธิ์แก้วฟรี"
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bottomPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class
