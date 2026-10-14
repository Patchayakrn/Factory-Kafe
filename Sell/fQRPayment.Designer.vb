<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fQRPayment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fQRPayment))
        Me.picQR = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAccountNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblBill = New System.Windows.Forms.Label()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnPaid = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.btnPaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picQR
        '
        Me.picQR.Image = CType(resources.GetObject("picQR.Image"), System.Drawing.Image)
        Me.picQR.Location = New System.Drawing.Point(12, 24)
        Me.picQR.Name = "picQR"
        Me.picQR.Size = New System.Drawing.Size(190, 208)
        Me.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picQR.TabIndex = 0
        Me.picQR.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblAmount)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblAccountName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblAccountNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblBank)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblBill)
        Me.GroupBox1.Location = New System.Drawing.Point(208, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(569, 312)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(36, 264)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(210, 41)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "ยอดที่ต้องชำระ"
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.ForeColor = System.Drawing.Color.Red
        Me.lblAmount.Location = New System.Drawing.Point(252, 267)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(166, 34)
        Me.lblAmount.TabIndex = 29
        Me.lblAmount.Text = "0.00"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(91, 202)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 38)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "ชื่อบัญชี"
        '
        'lblAccountName
        '
        Me.lblAccountName.Font = New System.Drawing.Font("Segoe UI Semilight", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountName.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblAccountName.Location = New System.Drawing.Point(202, 206)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.Size = New System.Drawing.Size(355, 34)
        Me.lblAccountName.TabIndex = 27
        Me.lblAccountName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(67, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 38)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "เลขที่บัญชี"
        '
        'lblAccountNo
        '
        Me.lblAccountNo.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountNo.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblAccountNo.Location = New System.Drawing.Point(202, 143)
        Me.lblAccountNo.Name = "lblAccountNo"
        Me.lblAccountNo.Size = New System.Drawing.Size(355, 34)
        Me.lblAccountNo.TabIndex = 25
        Me.lblAccountNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(94, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 38)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "ธนาคาร"
        '
        'lblBank
        '
        Me.lblBank.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBank.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblBank.Location = New System.Drawing.Point(202, 84)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(355, 34)
        Me.lblBank.TabIndex = 23
        Me.lblBank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(92, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 38)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "เลขที่บิล"
        '
        'lblBill
        '
        Me.lblBill.Font = New System.Drawing.Font("Segoe UI Semilight", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBill.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblBill.Location = New System.Drawing.Point(202, 31)
        Me.lblBill.Name = "lblBill"
        Me.lblBill.Size = New System.Drawing.Size(355, 34)
        Me.lblBill.TabIndex = 21
        Me.lblBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.PanelControl1.Controls.Add(Me.btnPaid)
        Me.PanelControl1.Controls.Add(Me.btnCancel)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 335)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(8)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(777, 97)
        Me.PanelControl1.TabIndex = 26
        '
        'btnPaid
        '
        Me.btnPaid.AllowFocus = True
        Me.btnPaid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaid.Location = New System.Drawing.Point(111, 14)
        Me.btnPaid.Margin = New System.Windows.Forms.Padding(8)
        Me.btnPaid.Name = "btnPaid"
        Me.btnPaid.Size = New System.Drawing.Size(179, 73)
        Me.btnPaid.TabIndex = 36
        Me.btnPaid.Text = "ชำระเงิน"
        '
        'btnCancel
        '
        Me.btnCancel.AllowFocus = True
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(363, 14)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(179, 73)
        Me.btnCancel.TabIndex = 35
        Me.btnCancel.Text = "ยกเลิก"
        '
        'fQRPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.ClientSize = New System.Drawing.Size(777, 432)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.picQR)
        Me.Name = "fQRPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QRPayment"
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.btnPaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picQR As PictureBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnPaid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label1 As Label
    Friend WithEvents lblBank As Label
    Friend WithEvents lblBill As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblAmount As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblAccountName As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblAccountNo As Label
End Class
