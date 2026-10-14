Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class fOfficerAddEdit

    Private components As IContainer
    Private layoutMain As TableLayoutPanel
    Private lblCode As Label
    Private lblBarcode As Label
    Private lblName As Label
    Private lblPosition As Label
    Private lblMobile As Label
    Private txtCode As TextBox
    Private txtBarcode As TextBox
    Private txtName As TextBox
    Private txtPosition As TextBox
    Private txtMobile As TextBox
    Private chkInactive As CheckBox
    Private pnlButtons As FlowLayoutPanel
    Private btnSave As Button
    Private btnCancel As Button

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.layoutMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblBarcode = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblPosition = New System.Windows.Forms.Label()
        Me.txtPosition = New System.Windows.Forms.TextBox()
        Me.lblMobile = New System.Windows.Forms.Label()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.pnlButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.chkInactive = New System.Windows.Forms.CheckBox()
        Me.layoutMain.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'layoutMain
        '
        Me.layoutMain.ColumnCount = 2
        Me.layoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.layoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMain.Controls.Add(Me.lblCode, 0, 0)
        Me.layoutMain.Controls.Add(Me.txtCode, 1, 0)
        Me.layoutMain.Controls.Add(Me.lblBarcode, 0, 1)
        Me.layoutMain.Controls.Add(Me.txtBarcode, 1, 1)
        Me.layoutMain.Controls.Add(Me.lblName, 0, 2)
        Me.layoutMain.Controls.Add(Me.txtName, 1, 2)
        Me.layoutMain.Controls.Add(Me.lblPosition, 0, 3)
        Me.layoutMain.Controls.Add(Me.txtPosition, 1, 3)
        Me.layoutMain.Controls.Add(Me.lblMobile, 0, 4)
        Me.layoutMain.Controls.Add(Me.txtMobile, 1, 4)
        Me.layoutMain.Controls.Add(Me.pnlButtons, 1, 6)
        Me.layoutMain.Controls.Add(Me.chkInactive, 1, 5)
        Me.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutMain.Location = New System.Drawing.Point(0, 0)
        Me.layoutMain.Name = "layoutMain"
        Me.layoutMain.Padding = New System.Windows.Forms.Padding(12)
        Me.layoutMain.RowCount = 7
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.layoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutMain.Size = New System.Drawing.Size(805, 480)
        Me.layoutMain.TabIndex = 0
        '
        'lblCode
        '
        Me.lblCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCode.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.lblCode.Location = New System.Drawing.Point(15, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(119, 64)
        Me.lblCode.TabIndex = 0
        Me.lblCode.Text = "รหัสพนักงาน"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCode
        '
        Me.txtCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCode.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.txtCode.Location = New System.Drawing.Point(140, 15)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(650, 42)
        Me.txtCode.TabIndex = 1
        '
        'lblBarcode
        '
        Me.lblBarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBarcode.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.lblBarcode.Location = New System.Drawing.Point(15, 76)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(119, 62)
        Me.lblBarcode.TabIndex = 2
        Me.lblBarcode.Text = "บาร์โค้ด"
        Me.lblBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBarcode
        '
        Me.txtBarcode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtBarcode.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.txtBarcode.Location = New System.Drawing.Point(140, 79)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(650, 42)
        Me.txtBarcode.TabIndex = 3
        '
        'lblName
        '
        Me.lblName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblName.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.lblName.Location = New System.Drawing.Point(15, 138)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(119, 58)
        Me.lblName.TabIndex = 4
        Me.lblName.Text = "ชื่อพนักงาน *"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtName
        '
        Me.txtName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtName.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.txtName.Location = New System.Drawing.Point(140, 141)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(650, 42)
        Me.txtName.TabIndex = 5
        '
        'lblPosition
        '
        Me.lblPosition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPosition.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.lblPosition.Location = New System.Drawing.Point(15, 196)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(119, 65)
        Me.lblPosition.TabIndex = 6
        Me.lblPosition.Text = "ตำแหน่ง"
        Me.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPosition
        '
        Me.txtPosition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPosition.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.txtPosition.Location = New System.Drawing.Point(140, 199)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(650, 42)
        Me.txtPosition.TabIndex = 7
        '
        'lblMobile
        '
        Me.lblMobile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMobile.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.lblMobile.Location = New System.Drawing.Point(15, 261)
        Me.lblMobile.Name = "lblMobile"
        Me.lblMobile.Size = New System.Drawing.Size(119, 62)
        Me.lblMobile.TabIndex = 8
        Me.lblMobile.Text = "เบอร์มือถือ"
        Me.lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMobile
        '
        Me.txtMobile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMobile.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.txtMobile.Location = New System.Drawing.Point(140, 264)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(650, 42)
        Me.txtMobile.TabIndex = 9
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Controls.Add(Me.btnSave)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.pnlButtons.Location = New System.Drawing.Point(140, 390)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(650, 75)
        Me.pnlButtons.TabIndex = 11
        Me.pnlButtons.WrapContents = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(75, Byte), Integer))
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(542, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(105, 38)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Cordia New", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(431, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(105, 38)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'chkInactive
        '
        Me.chkInactive.AutoSize = True
        Me.chkInactive.Font = New System.Drawing.Font("Cordia New", 15.0!)
        Me.chkInactive.Location = New System.Drawing.Point(140, 326)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(128, 38)
        Me.chkInactive.TabIndex = 10
        Me.chkInactive.Text = "ไม่ใช้งานแล้ว"
        Me.chkInactive.UseVisualStyleBackColor = True
        '
        'fOfficerAddEdit
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 32.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(805, 480)
        Me.Controls.Add(Me.layoutMain)
        Me.Font = New System.Drawing.Font("Cordia New", 14.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fOfficerAddEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "เพิ่มพนักงาน"
        Me.layoutMain.ResumeLayout(False)
        Me.layoutMain.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class
