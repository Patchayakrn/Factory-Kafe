<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMaterialStock
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblPeriod As Label
    Friend WithEvents cboPeriod As ComboBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnCopy As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents gridStock As DataGridView
    Friend WithEvents lblSummary As Label

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
        Me.lblSearch = New Label()
        Me.txtSearch = New TextBox()
        Me.lblPeriod = New Label()
        Me.cboPeriod = New ComboBox()
        Me.btnSearch = New Button()
        Me.btnRefresh = New Button()
        Me.btnCopy = New Button()
        Me.btnPrint = New Button()
        Me.gridStock = New DataGridView()
        Me.lblSummary = New Label()
        CType(Me.gridStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()

        Me.pnlHeader.BackColor = Color.FromArgb(225, 240, 252)
        Me.pnlHeader.Dock = DockStyle.None
        Me.pnlHeader.Location = New Point(0, 0)
        Me.pnlHeader.Size = New Size(1200, 132)
        Me.pnlHeader.Padding = New Padding(12, 8, 12, 8)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSearch)
        Me.pnlHeader.Controls.Add(Me.txtSearch)
        Me.pnlHeader.Controls.Add(Me.lblPeriod)
        Me.pnlHeader.Controls.Add(Me.cboPeriod)
        Me.pnlHeader.Controls.Add(Me.btnSearch)
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.btnCopy)
        Me.pnlHeader.Controls.Add(Me.btnPrint)

        Me.lblTitle.AutoSize = False
        Me.lblTitle.Dock = DockStyle.Top
        Me.lblTitle.Height = 36
        Me.lblTitle.Text = "สต็อกวัตถุดิบ (Material Stock)"
        Me.lblTitle.Font = New Font("Cordia New", 20.0!, FontStyle.Bold)
        Me.lblTitle.ForeColor = Color.FromArgb(30, 82, 130)
        Me.lblTitle.TextAlign = ContentAlignment.MiddleLeft

        Me.lblSearch.AutoSize = False
        Me.lblSearch.Location = New Point(12, 54)
        Me.lblSearch.Size = New Size(78, 28)
        Me.lblSearch.Text = "ค้นหา"
        Me.lblSearch.TextAlign = ContentAlignment.MiddleLeft
        Me.txtSearch.Location = New Point(88, 54)
        Me.txtSearch.Size = New Size(310, 28)
        Me.lblPeriod.AutoSize = False
        Me.lblPeriod.Location = New Point(420, 54)
        Me.lblPeriod.Size = New Size(82, 28)
        Me.lblPeriod.Text = "งวดสต็อก"
        Me.lblPeriod.TextAlign = ContentAlignment.MiddleLeft
        Me.cboPeriod.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboPeriod.Location = New Point(502, 54)
        Me.cboPeriod.Size = New Size(130, 28)
        Me.btnSearch.Location = New Point(650, 52)
        Me.btnSearch.Size = New Size(92, 32)
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnRefresh.Location = New Point(750, 52)
        Me.btnRefresh.Size = New Size(92, 32)
        Me.btnRefresh.Text = "รีเฟรช"
        Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnCopy.Location = New Point(850, 52)
        Me.btnCopy.Size = New Size(92, 32)
        Me.btnCopy.Text = "คัดลอก"
        Me.btnCopy.UseVisualStyleBackColor = True
        Me.btnPrint.Location = New Point(950, 52)
        Me.btnPrint.Size = New Size(92, 32)
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = True

        Me.gridStock.AllowUserToAddRows = False
        Me.gridStock.AllowUserToDeleteRows = False
        Me.gridStock.AllowUserToResizeRows = False
        Me.gridStock.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        Me.gridStock.AutoGenerateColumns = True
        Me.gridStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.gridStock.ColumnHeadersVisible = True
        Me.gridStock.ColumnHeadersHeight = 42
        Me.gridStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.gridStock.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(219, 232, 244)
        Me.gridStock.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        Me.gridStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30)
        Me.gridStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.gridStock.EnableHeadersVisualStyles = False
        Me.gridStock.DefaultCellStyle.Font = New Font("Cordia New", 16.0!)
        Me.gridStock.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.gridStock.Dock = DockStyle.None
        Me.gridStock.Location = New Point(0, 132)
        Me.gridStock.Size = New Size(1200, 478)
        Me.gridStock.MultiSelect = True
        Me.gridStock.ReadOnly = True
        Me.gridStock.RowHeadersVisible = False
        Me.gridStock.RowTemplate.Height = 34
        Me.gridStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Me.lblSummary.BackColor = Color.FromArgb(242, 248, 253)
        Me.lblSummary.Dock = DockStyle.None
        Me.lblSummary.Location = New Point(0, 610)
        Me.lblSummary.Size = New Size(1200, 40)
        Me.lblSummary.Padding = New Padding(12, 0, 12, 0)
        Me.lblSummary.Text = "ยังไม่มีข้อมูล"
        Me.lblSummary.TextAlign = ContentAlignment.MiddleLeft

        Me.AutoScaleDimensions = New SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1200, 650)
        Me.Controls.Add(Me.gridStock)
        Me.Controls.Add(Me.lblSummary)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New Font("Cordia New", 16.0!)
        Me.MinimumSize = New Size(900, 500)
        Me.Name = "frmMaterialStock"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "สต็อกวัตถุดิบ (Material Stock)"
        Me.WindowState = FormWindowState.Maximized

        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.gridStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub
End Class
