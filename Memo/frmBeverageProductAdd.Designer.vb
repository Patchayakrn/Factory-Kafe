<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBeverageProductAdd
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblProductID As Label
    Friend WithEvents lblProductDetail As Label
    Friend WithEvents txtProductID As TextBox
    Friend WithEvents txtProductDetail As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button

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
        Me.lblTitle = New Label()
        Me.lblProductID = New Label()
        Me.lblProductDetail = New Label()
        Me.txtProductID = New TextBox()
        Me.txtProductDetail = New TextBox()
        Me.btnSave = New Button()
        Me.btnCancel = New Button()
        Me.SuspendLayout()
        Me.lblTitle.Dock = DockStyle.Top
        Me.lblTitle.Height = 50
        Me.lblTitle.BackColor = Color.FromArgb(230, 242, 252)
        Me.lblTitle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        Me.lblTitle.ForeColor = Color.FromArgb(30, 75, 110)
        Me.lblTitle.Padding = New Padding(16, 0, 0, 0)
        Me.lblTitle.Text = "เพิ่มรายการเครื่องดื่ม"
        Me.lblTitle.TextAlign = ContentAlignment.MiddleLeft
        Me.lblProductID.AutoSize = True
        Me.lblProductID.Location = New Point(24, 82)
        Me.lblProductID.Text = "รหัสเครื่องดื่ม:"
        Me.lblProductDetail.AutoSize = True
        Me.lblProductDetail.Location = New Point(24, 136)
        Me.lblProductDetail.Text = "ชื่อเครื่องดื่ม:"
        Me.txtProductID.Font = New Font("Cordia New", 16.0!)
        Me.txtProductID.Location = New Point(155, 76)
        Me.txtProductID.Size = New Size(300, 35)
        Me.txtProductDetail.Font = New Font("Cordia New", 16.0!)
        Me.txtProductDetail.Location = New Point(155, 130)
        Me.txtProductDetail.Size = New Size(420, 35)
        Me.btnSave.BackColor = Color.FromArgb(38, 132, 74)
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.ForeColor = Color.White
        Me.btnSave.Location = New Point(330, 205)
        Me.btnSave.Size = New Size(125, 42)
        Me.btnSave.Text = "บันทึก"
        Me.btnCancel.BackColor = Color.FromArgb(190, 75, 75)
        Me.btnCancel.FlatStyle = FlatStyle.Flat
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.ForeColor = Color.White
        Me.btnCancel.Location = New Point(465, 205)
        Me.btnCancel.Size = New Size(125, 42)
        Me.btnCancel.Text = "ยกเลิก"
        Me.AutoScaleDimensions = New SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(620, 275)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtProductDetail)
        Me.Controls.Add(Me.txtProductID)
        Me.Controls.Add(Me.lblProductDetail)
        Me.Controls.Add(Me.lblProductID)
        Me.Controls.Add(Me.lblTitle)
        Me.Font = New Font("Cordia New", 16.0!)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBeverageProductAdd"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "เพิ่มรายการเครื่องดื่ม"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class
