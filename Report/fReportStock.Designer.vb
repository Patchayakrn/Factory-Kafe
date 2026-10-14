<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fReportStock
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents filterPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents splitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents dgvProducts As System.Windows.Forms.DataGridView
    Friend WithEvents dgvMovement As System.Windows.Forms.DataGridView
    Friend WithEvents lblStatus As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.filterPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.splitContainer = New System.Windows.Forms.SplitContainer()
        Me.dgvProducts = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvMovement = New System.Windows.Forms.DataGridView()
        Me.DocDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumIn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumOut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumRemain = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.filterPanel.SuspendLayout()
        CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer.Panel1.SuspendLayout()
        Me.splitContainer.Panel2.SuspendLayout()
        Me.splitContainer.SuspendLayout()
        CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMovement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'filterPanel
        '
        Me.filterPanel.ColumnCount = 9
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.filterPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203.0!))
        Me.filterPanel.Controls.Add(Me.cboBranch, 1, 0)
        Me.filterPanel.Controls.Add(Me.dtpFrom, 3, 0)
        Me.filterPanel.Controls.Add(Me.dtpTo, 5, 0)
        Me.filterPanel.Controls.Add(Me.btnSearch, 7, 0)
        Me.filterPanel.Controls.Add(Me.btnPrint, 8, 0)
        Me.filterPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.filterPanel.Location = New System.Drawing.Point(0, 0)
        Me.filterPanel.Name = "filterPanel"
        Me.filterPanel.Padding = New System.Windows.Forms.Padding(10)
        Me.filterPanel.RowCount = 1
        Me.filterPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.filterPanel.Size = New System.Drawing.Size(1200, 76)
        Me.filterPanel.TabIndex = 0
        '
        'cboBranch
        '
        Me.cboBranch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBranch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(88, 13)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(244, 45)
        Me.cboBranch.TabIndex = 1
        '
        'dtpFrom
        '
        Me.dtpFrom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFrom.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(440, 13)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(163, 45)
        Me.dtpFrom.TabIndex = 2
        '
        'dtpTo
        '
        Me.dtpTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpTo.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(623, 13)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(163, 45)
        Me.dtpTo.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSearch.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(850, 13)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(134, 50)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "ค้นหารายงาน"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPrint.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(990, 13)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(197, 50)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "พิมพ์รายงาน"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'splitContainer
        '
        Me.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer.Location = New System.Drawing.Point(0, 76)
        Me.splitContainer.Name = "splitContainer"
        '
        'splitContainer.Panel1
        '
        Me.splitContainer.Panel1.Controls.Add(Me.dgvProducts)
        '
        'splitContainer.Panel2
        '
        Me.splitContainer.Panel2.Controls.Add(Me.dgvMovement)
        Me.splitContainer.Size = New System.Drawing.Size(1200, 696)
        Me.splitContainer.SplitterDistance = 424
        Me.splitContainer.TabIndex = 1
        '
        'dgvProducts
        '
        Me.dgvProducts.AllowUserToAddRows = False
        Me.dgvProducts.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvProducts.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvProducts.BackgroundColor = System.Drawing.Color.White
        Me.dgvProducts.ColumnHeadersHeight = 45
        Me.dgvProducts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column1, Me.Column7, Me.Column3, Me.Column4})
        Me.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvProducts.EnableHeadersVisualStyles = False
        Me.dgvProducts.Location = New System.Drawing.Point(0, 0)
        Me.dgvProducts.Name = "dgvProducts"
        Me.dgvProducts.ReadOnly = True
        Me.dgvProducts.RowHeadersWidth = 45
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 16.2!)
        Me.dgvProducts.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvProducts.RowTemplate.Height = 36
        Me.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProducts.Size = New System.Drawing.Size(424, 696)
        Me.dgvProducts.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "PN"
        Me.Column1.HeaderText = "PN"
        Me.Column1.MinimumWidth = 6
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "ProductName"
        Me.Column7.HeaderText = "ชื่อสินค้า"
        Me.Column7.MinimumWidth = 6
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 170
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "ProductCode"
        Me.Column2.HeaderText = "รหัสสินค้า"
        Me.Column2.MinimumWidth = 6
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 110
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "Barcode"
        Me.Column3.HeaderText = "Barcode"
        Me.Column3.MinimumWidth = 6
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 115
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "ProductGroup"
        Me.Column4.HeaderText = "กลุ่มสินค้า"
        Me.Column4.MinimumWidth = 6
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 125
        '
        'dgvMovement
        '
        Me.dgvMovement.AllowUserToAddRows = False
        Me.dgvMovement.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovement.ColumnHeadersHeight = 45
        Me.dgvMovement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvMovement.AutoGenerateColumns = False
        Me.dgvMovement.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DocDate, Me.ProductCode, Me.ProductName, Me.BillType, Me.DocNumber, Me.BillNumber, Me.NumIn, Me.NumOut, Me.NumRemain})
        Me.dgvMovement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovement.EnableHeadersVisualStyles = False
        Me.dgvMovement.Location = New System.Drawing.Point(0, 0)
        Me.dgvMovement.Name = "dgvMovement"
        Me.dgvMovement.ReadOnly = True
        Me.dgvMovement.RowHeadersWidth = 51
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvMovement.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvMovement.RowTemplate.Height = 36
        Me.dgvMovement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovement.Size = New System.Drawing.Size(772, 696)
        Me.dgvMovement.TabIndex = 0
        '
        'DocDate
        '
        Me.DocDate.DataPropertyName = "DocDate"
        Me.DocDate.HeaderText = "วันที่"
        Me.DocDate.MinimumWidth = 6
        Me.DocDate.Name = "DocDate"
        Me.DocDate.ReadOnly = True
        Me.DocDate.Width = 120
        '
        'ProductCode
        '
        Me.ProductCode.DataPropertyName = "ProductCode"
        Me.ProductCode.HeaderText = "รหัสสินค้า"
        Me.ProductCode.MinimumWidth = 6
        Me.ProductCode.Name = "ProductCode"
        Me.ProductCode.ReadOnly = True
        Me.ProductCode.Width = 125
        '
        'ProductName
        '
        Me.ProductName.DataPropertyName = "ProductName"
        Me.ProductName.HeaderText = "ชื่อสินค้า"
        Me.ProductName.MinimumWidth = 6
        Me.ProductName.Name = "ProductName"
        Me.ProductName.ReadOnly = True
        Me.ProductName.Width = 170
        '
        'BillType
        '
        Me.BillType.DataPropertyName = "BillType"
        Me.BillType.HeaderText = "ประเภทบิล"
        Me.BillType.MinimumWidth = 6
        Me.BillType.Name = "BillType"
        Me.BillType.ReadOnly = True
        Me.BillType.Width = 125
        '
        'DocNumber
        '
        Me.DocNumber.DataPropertyName = "DocNumber"
        Me.DocNumber.HeaderText = "Doc No."
        Me.DocNumber.MinimumWidth = 6
        Me.DocNumber.Name = "DocNumber"
        Me.DocNumber.ReadOnly = True
        Me.DocNumber.Width = 125
        '
        'BillNumber
        '
        Me.BillNumber.DataPropertyName = "BillNumber"
        Me.BillNumber.HeaderText = "เลขที่ใบกำกับ / หมายเหตุ"
        Me.BillNumber.MinimumWidth = 6
        Me.BillNumber.Name = "BillNumber"
        Me.BillNumber.ReadOnly = True
        Me.BillNumber.Width = 220
        '
        'NumIn
        '
        Me.NumIn.DataPropertyName = "NumIn"
        Me.NumIn.HeaderText = "จำนวนเข้า"
        Me.NumIn.MinimumWidth = 6
        Me.NumIn.Name = "NumIn"
        Me.NumIn.ReadOnly = True
        Me.NumIn.Width = 125
        '
        'NumOut
        '
        Me.NumOut.DataPropertyName = "NumOut"
        Me.NumOut.HeaderText = "จำนวนออก"
        Me.NumOut.MinimumWidth = 6
        Me.NumOut.Name = "NumOut"
        Me.NumOut.ReadOnly = True
        Me.NumOut.Width = 125
        '
        'NumRemain
        '
        Me.NumRemain.DataPropertyName = "NumRemain"
        Me.NumRemain.HeaderText = "คงเหลือ"
        Me.NumRemain.MinimumWidth = 6
        Me.NumRemain.Name = "NumRemain"
        Me.NumRemain.ReadOnly = True
        Me.NumRemain.Width = 125
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStatus.Location = New System.Drawing.Point(0, 772)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblStatus.Size = New System.Drawing.Size(1200, 28)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Column6
        '
        Me.Column6.HeaderText = "Status"
        Me.Column6.MinimumWidth = 6
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 125
        '
        'Column9
        '
        Me.Column9.HeaderText = "ยี่ห้อ"
        Me.Column9.MinimumWidth = 6
        Me.Column9.Name = "Column9"
        Me.Column9.Width = 125
        '
        'Column8
        '
        Me.Column8.HeaderText = "status"
        Me.Column8.MinimumWidth = 6
        Me.Column8.Name = "Column8"
        Me.Column8.Width = 125
        '
        'fReportStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 800)
        Me.Controls.Add(Me.splitContainer)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.filterPanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.MinimumSize = New System.Drawing.Size(1100, 700)
        Me.Name = "fReportStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Stock Card (รายงานความเคลื่อนไหวของสินค้าและวัตถุดิบ)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.filterPanel.ResumeLayout(False)
        Me.splitContainer.Panel1.ResumeLayout(False)
        Me.splitContainer.Panel2.ResumeLayout(False)
        CType(Me.splitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer.ResumeLayout(False)
        CType(Me.dgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMovement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewTextBoxColumn
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
    Friend WithEvents DocDate As DataGridViewTextBoxColumn
    Friend WithEvents ProductCode As DataGridViewTextBoxColumn
    Friend WithEvents ProductName As DataGridViewTextBoxColumn
    Friend WithEvents BillType As DataGridViewTextBoxColumn
    Friend WithEvents DocNumber As DataGridViewTextBoxColumn
    Friend WithEvents BillNumber As DataGridViewTextBoxColumn
    Friend WithEvents NumIn As DataGridViewTextBoxColumn
    Friend WithEvents NumOut As DataGridViewTextBoxColumn
    Friend WithEvents NumRemain As DataGridViewTextBoxColumn
End Class
