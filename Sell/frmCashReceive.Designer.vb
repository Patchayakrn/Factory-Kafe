<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashReceive
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCashReceive))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btCustomer = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.lblChange = New Krypton.Toolkit.KryptonLabel()
        CType(Me.btCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cordia New", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(262, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(242, 100)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "เงินทอน"
        '
        'btCustomer
        '
        Me.btCustomer.AllowFocus = True
        Me.btCustomer.Location = New System.Drawing.Point(961, 511)
        Me.btCustomer.Margin = New System.Windows.Forms.Padding(6)
        Me.btCustomer.Name = "btCustomer"
        Me.btCustomer.Size = New System.Drawing.Size(238, 98)
        Me.btCustomer.TabIndex = 35
        Me.btCustomer.Text = "OK"
        '
        'btnOK
        '
        Me.btnOK.AllowFocus = True
        Me.btnOK.Location = New System.Drawing.Point(640, 431)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(144, 50)
        Me.btnOK.TabIndex = 37
        Me.btnOK.Text = "OK"
        '
        'lblChange
        '
        Me.lblChange.AutoSize = False
        Me.lblChange.Location = New System.Drawing.Point(1, 112)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(783, 303)
        Me.lblChange.StateCommon.Draw = Krypton.Toolkit.InheritBool.[True]
        Me.lblChange.StateCommon.DrawFocus = Krypton.Toolkit.InheritBool.[True]
        Me.lblChange.StateCommon.ShortText.Color1 = System.Drawing.Color.Lime
        Me.lblChange.StateCommon.ShortText.Font = New System.Drawing.Font("Dubai", 90.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChange.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center
        Me.lblChange.StateCommon.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center
        Me.lblChange.TabIndex = 38
        Me.lblChange.Values.Text = "0.00"
        '
        'frmCashReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(786, 493)
        Me.Controls.Add(Me.lblChange)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btCustomer)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCashReceive"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เงินทอน"
        CType(Me.btCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents btCustomer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblChange As Krypton.Toolkit.KryptonLabel
End Class
