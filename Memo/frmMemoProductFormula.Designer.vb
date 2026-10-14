Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmMemoProductFormula

    Private components As IContainer
    Private lblTitle As Label
    Private pnlMain As Panel
    Private lblDocumentDate As Label
    Private dtpDocumentDate As DateTimePicker
    Private lblDocumentNo As Label
    Private txtDocumentNo As TextBox
    Private lblBody As Label
    Private txtBody As TextBox
    Private btnAddRow As Button
    Private btnRemoveRow As Button
    Private btnNew As Button
    Private WithEvents btnHistory As Button
    Private WithEvents btnSave As Button
    Private btnPrint As Button
    Private btnClose As Button

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            'ถอดคอนโทรลที่สร้างแบบไดนามิกออกจาก TabControl ก่อนให้ Form จัดการ Dispose
            'เพื่อป้องกัน WinForms เกิด IndexOutOfRangeException ระหว่างปิดฟอร์ม
            If bomTabs IsNot Nothing Then
                If gridEmployees IsNot Nothing AndAlso Not gridEmployees.IsDisposed Then
                    Try
                        gridEmployees.CancelEdit()
                        gridEmployees.CurrentCell = Nothing
                    Catch
                    End Try
                End If
                If gridMoreBom IsNot Nothing AndAlso Not gridMoreBom.IsDisposed Then
                    Try
                        gridMoreBom.CancelEdit()
                        gridMoreBom.CurrentCell = Nothing
                    Catch
                    End Try
                End If
                If gridEmployees IsNot Nothing AndAlso gridEmployees.Parent IsNot Nothing Then
                    gridEmployees.Parent.Controls.Remove(gridEmployees)
                End If
                If gridMoreBom IsNot Nothing AndAlso gridMoreBom.Parent IsNot Nothing Then
                    gridMoreBom.Parent.Controls.Remove(gridMoreBom)
                End If
                If bomTabs.Parent IsNot Nothing Then bomTabs.Parent.Controls.Remove(bomTabs)
                bomTabs.TabPages.Clear()
                bomTabs.Dispose()
                bomTabs = Nothing
            End If

            If gridMoreBom IsNot Nothing AndAlso Not gridMoreBom.IsDisposed Then gridMoreBom.Dispose()
            If gridEmployees IsNot Nothing AndAlso Not gridEmployees.IsDisposed Then gridEmployees.Dispose()
            If components IsNot Nothing Then components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnRemoveRow = New System.Windows.Forms.Button()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.gridEmployees = New System.Windows.Forms.DataGridView()
        Me.colLine = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEmployeeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colOldHoliday = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNewHoliday = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPackagePrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblBody = New System.Windows.Forms.Label()
        Me.txtBody = New System.Windows.Forms.TextBox()
        Me.lblSalesManager = New System.Windows.Forms.Label()
        Me.txtSalesManager = New System.Windows.Forms.TextBox()
        Me.lblDocumentNo = New System.Windows.Forms.Label()
        Me.txtDocumentNo = New System.Windows.Forms.TextBox()
        Me.lblDocumentDate = New System.Windows.Forms.Label()
        Me.dtpDocumentDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlMain.SuspendLayout()
        CType(Me.gridEmployees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Cordia New", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(18, 0, 0, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1277, 60)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "บันทึกเอกสารงานพิมพ์ / ชี้แจงการใช้วัตถุดิบ"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.TextBox2)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.btnClose)
        Me.pnlMain.Controls.Add(Me.btnHistory)
        Me.pnlMain.Controls.Add(Me.btnSave)
        Me.pnlMain.Controls.Add(Me.btnPrint)
        Me.pnlMain.Controls.Add(Me.btnNew)
        Me.pnlMain.Controls.Add(Me.btnRemoveRow)
        Me.pnlMain.Controls.Add(Me.btnAddRow)
        Me.pnlMain.Controls.Add(Me.gridEmployees)
        Me.pnlMain.Controls.Add(Me.lblBody)
        Me.pnlMain.Controls.Add(Me.txtBody)
        Me.pnlMain.Controls.Add(Me.lblSalesManager)
        Me.pnlMain.Controls.Add(Me.txtSalesManager)
        Me.pnlMain.Controls.Add(Me.lblDocumentNo)
        Me.pnlMain.Controls.Add(Me.txtDocumentNo)
        Me.pnlMain.Controls.Add(Me.lblDocumentDate)
        Me.pnlMain.Controls.Add(Me.dtpDocumentDate)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 60)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(18)
        Me.pnlMain.Size = New System.Drawing.Size(1277, 593)
        Me.pnlMain.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.TextBox2.Location = New System.Drawing.Point(145, 77)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(253, 44)
        Me.TextBox2.TabIndex = 32
        Me.TextBox2.Text = "กรรมการผู้จัดการ"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.Label1.Location = New System.Drawing.Point(70, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 37)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "เรียน"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(1094, 527)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 45)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnHistory
        '
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistory.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnHistory.ForeColor = System.Drawing.Color.White
        Me.btnHistory.Location = New System.Drawing.Point(179, 527)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(160, 45)
        Me.btnHistory.TabIndex = 4
        Me.btnHistory.Text = "รายการย้อนหลัง"
        Me.btnHistory.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(764, 527)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(160, 45)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(934, 527)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(150, 45)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.Location = New System.Drawing.Point(19, 527)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(150, 45)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = "เอกสารใหม่"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnRemoveRow
        '
        Me.btnRemoveRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnRemoveRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveRow.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnRemoveRow.ForeColor = System.Drawing.Color.White
        Me.btnRemoveRow.Location = New System.Drawing.Point(1104, 156)
        Me.btnRemoveRow.Name = "btnRemoveRow"
        Me.btnRemoveRow.Size = New System.Drawing.Size(130, 42)
        Me.btnRemoveRow.TabIndex = 11
        Me.btnRemoveRow.Text = "ลบรายการ"
        Me.btnRemoveRow.UseVisualStyleBackColor = False
        '
        'btnAddRow
        '
        Me.btnAddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnAddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddRow.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.btnAddRow.ForeColor = System.Drawing.Color.White
        Me.btnAddRow.Location = New System.Drawing.Point(964, 156)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(130, 42)
        Me.btnAddRow.TabIndex = 12
        Me.btnAddRow.Text = "เพิ่มรายการ"
        Me.btnAddRow.UseVisualStyleBackColor = False
        '
        'gridEmployees
        '
        Me.gridEmployees.AllowUserToAddRows = False
        Me.gridEmployees.AllowUserToDeleteRows = False
        Me.gridEmployees.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 14.0!)
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridEmployees.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridEmployees.ColumnHeadersHeight = 42
        Me.gridEmployees.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colLine, Me.colEmployeeCode, Me.colEmployeeName, Me.colOldHoliday, Me.colNewHoliday, Me.colPackagePrice})
        Me.gridEmployees.Location = New System.Drawing.Point(20, 220)
        Me.gridEmployees.MultiSelect = False
        Me.gridEmployees.Name = "gridEmployees"
        Me.gridEmployees.RowHeadersVisible = False
        Me.gridEmployees.RowHeadersWidth = 51
        Me.gridEmployees.RowTemplate.Height = 36
        Me.gridEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridEmployees.Size = New System.Drawing.Size(1214, 285)
        Me.gridEmployees.TabIndex = 13
        '
        'colLine
        '
        Me.colLine.HeaderText = "ลำดับ"
        Me.colLine.MinimumWidth = 6
        Me.colLine.Name = "colLine"
        Me.colLine.ReadOnly = True
        Me.colLine.Width = 75
        '
        'colEmployeeCode
        '
        Me.colEmployeeCode.HeaderText = "กรณี"
        Me.colEmployeeCode.MinimumWidth = 6
        Me.colEmployeeCode.Name = "colEmployeeCode"
        Me.colEmployeeCode.Width = 110
        '
        'colEmployeeName
        '
        Me.colEmployeeName.HeaderText = "รหัสวัตถุดิบ"
        Me.colEmployeeName.MinimumWidth = 6
        Me.colEmployeeName.Name = "colEmployeeName"
        Me.colEmployeeName.Width = 170
        '
        'colOldHoliday
        '
        Me.colOldHoliday.HeaderText = "รายการวัตถุดิบ"
        Me.colOldHoliday.MinimumWidth = 6
        Me.colOldHoliday.Name = "colOldHoliday"
        Me.colOldHoliday.Width = 420
        '
        'colNewHoliday
        '
        Me.colNewHoliday.HeaderText = "จำนวน"
        Me.colNewHoliday.MinimumWidth = 6
        Me.colNewHoliday.Name = "colNewHoliday"
        Me.colNewHoliday.Width = 150
        '
        'colPackagePrice
        '
        Me.colPackagePrice.HeaderText = "หน่วย"
        Me.colPackagePrice.MinimumWidth = 6
        Me.colPackagePrice.Name = "colPackagePrice"
        Me.colPackagePrice.Width = 120
        '
        'lblBody
        '
        Me.lblBody.AutoSize = True
        Me.lblBody.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.lblBody.Location = New System.Drawing.Point(19, 141)
        Me.lblBody.Name = "lblBody"
        Me.lblBody.Size = New System.Drawing.Size(104, 37)
        Me.lblBody.TabIndex = 14
        Me.lblBody.Text = "รายละเอียด"
        '
        'txtBody
        '
        Me.txtBody.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.txtBody.Location = New System.Drawing.Point(145, 141)
        Me.txtBody.Multiline = True
        Me.txtBody.Name = "txtBody"
        Me.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBody.Size = New System.Drawing.Size(760, 57)
        Me.txtBody.TabIndex = 15
        '
        'lblSalesManager
        '
        Me.lblSalesManager.AutoSize = True
        Me.lblSalesManager.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.lblSalesManager.Location = New System.Drawing.Point(74, 21)
        Me.lblSalesManager.Name = "lblSalesManager"
        Me.lblSalesManager.Size = New System.Drawing.Size(49, 37)
        Me.lblSalesManager.TabIndex = 16
        Me.lblSalesManager.Text = "เรื่อง"
        '
        'txtSalesManager
        '
        Me.txtSalesManager.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.txtSalesManager.Location = New System.Drawing.Point(145, 21)
        Me.txtSalesManager.Name = "txtSalesManager"
        Me.txtSalesManager.Size = New System.Drawing.Size(625, 44)
        Me.txtSalesManager.TabIndex = 17
        Me.txtSalesManager.Text = "ชี้แจงการใช้วัตถุดิบ"
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.AutoSize = True
        Me.lblDocumentNo.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.lblDocumentNo.Location = New System.Drawing.Point(927, 17)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(110, 37)
        Me.lblDocumentNo.TabIndex = 26
        Me.lblDocumentNo.Text = "เลขที่เอกสาร"
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.BackColor = System.Drawing.Color.LightYellow
        Me.txtDocumentNo.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.txtDocumentNo.Location = New System.Drawing.Point(1054, 14)
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.ReadOnly = True
        Me.txtDocumentNo.Size = New System.Drawing.Size(180, 44)
        Me.txtDocumentNo.TabIndex = 27
        '
        'lblDocumentDate
        '
        Me.lblDocumentDate.AutoSize = True
        Me.lblDocumentDate.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.lblDocumentDate.Location = New System.Drawing.Point(927, 79)
        Me.lblDocumentDate.Name = "lblDocumentDate"
        Me.lblDocumentDate.Size = New System.Drawing.Size(105, 37)
        Me.lblDocumentDate.TabIndex = 28
        Me.lblDocumentDate.Text = "วันที่เอกสาร"
        '
        'dtpDocumentDate
        '
        Me.dtpDocumentDate.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDocumentDate.Location = New System.Drawing.Point(1054, 76)
        Me.dtpDocumentDate.Name = "dtpDocumentDate"
        Me.dtpDocumentDate.Size = New System.Drawing.Size(180, 42)
        Me.dtpDocumentDate.TabIndex = 29
        Me.dtpDocumentDate.Value = New Date(2026, 9, 22, 0, 0, 0, 0)
        '
        'frmMemoProductFormula
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 32.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 653)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.lblTitle)
        Me.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.MinimumSize = New System.Drawing.Size(1000, 700)
        Me.Name = "frmMemoProductFormula"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ขออนุมัติสูตรวัตถุดิบ"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.gridEmployees, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents gridEmployees As DataGridView
    Private WithEvents colLine As DataGridViewTextBoxColumn
    Private WithEvents colEmployeeCode As DataGridViewTextBoxColumn
    Private WithEvents colEmployeeName As DataGridViewTextBoxColumn
    Private WithEvents colOldHoliday As DataGridViewTextBoxColumn
    Private WithEvents colNewHoliday As DataGridViewTextBoxColumn
    Private WithEvents colPackagePrice As DataGridViewTextBoxColumn
    Private WithEvents lblSalesManager As Label
    Private WithEvents txtSalesManager As TextBox
    Private WithEvents TextBox2 As TextBox
    Private WithEvents Label1 As Label
End Class
