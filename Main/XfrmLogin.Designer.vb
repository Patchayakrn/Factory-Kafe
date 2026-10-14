<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class XfrmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XfrmLogin))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblpass = New System.Windows.Forms.Label()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lblDB = New System.Windows.Forms.Label()
        Me.cboDatabase = New System.Windows.Forms.ComboBox()
        Me.lblVer = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtpass = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(35, 153)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(245, 245)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 115
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(354, 186)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 39)
        Me.Label3.TabIndex = 118
        Me.Label3.Text = "👤Username"
        '
        'lblpass
        '
        Me.lblpass.AutoSize = True
        Me.lblpass.BackColor = System.Drawing.Color.Transparent
        Me.lblpass.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpass.ForeColor = System.Drawing.Color.Black
        Me.lblpass.Location = New System.Drawing.Point(353, 260)
        Me.lblpass.Name = "lblpass"
        Me.lblpass.Size = New System.Drawing.Size(133, 39)
        Me.lblpass.TabIndex = 133
        Me.lblpass.Text = "🔑 Password"
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(378, 394)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(175, 56)
        Me.btnLogin.TabIndex = 135
        Me.btnLogin.Text = "LOGIN"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'lblDB
        '
        Me.lblDB.BackColor = System.Drawing.Color.Transparent
        Me.lblDB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDB.ForeColor = System.Drawing.Color.Black
        Me.lblDB.Location = New System.Drawing.Point(354, 333)
        Me.lblDB.Name = "lblDB"
        Me.lblDB.Size = New System.Drawing.Size(132, 31)
        Me.lblDB.TabIndex = 137
        Me.lblDB.Text = "🗄 Database"
        '
        'cboDatabase
        '
        Me.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDatabase.Font = New System.Drawing.Font("Segoe UI", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDatabase.FormattingEnabled = True
        Me.cboDatabase.Items.AddRange(New Object() {"Localhost   NewStock (Windows)", "SQLEXPRESS NewStock (sa)", "", "POS01 NewStockTest (Windows)"})
        Me.cboDatabase.Location = New System.Drawing.Point(492, 333)
        Me.cboDatabase.Name = "cboDatabase"
        Me.cboDatabase.Size = New System.Drawing.Size(277, 33)
        Me.cboDatabase.TabIndex = 145
        '
        'lblVer
        '
        Me.lblVer.BackColor = System.Drawing.Color.Transparent
        Me.lblVer.Font = New System.Drawing.Font("Cordia New", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVer.ForeColor = System.Drawing.Color.Black
        Me.lblVer.Location = New System.Drawing.Point(5, 441)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(200, 31)
        Me.lblVer.TabIndex = 138
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(35, 12)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(86, 76)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 139
        Me.PictureBox3.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("NSimSun", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Tan
        Me.Label1.Location = New System.Drawing.Point(176, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(631, 60)
        Me.Label1.TabIndex = 140
        Me.Label1.Text = "THE FACTORY Kafe'"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtUsername
        '
        Me.txtUsername.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsername.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.ForeColor = System.Drawing.Color.Blue
        Me.txtUsername.Location = New System.Drawing.Point(492, 184)
        Me.txtUsername.Multiline = True
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(277, 49)
        Me.txtUsername.TabIndex = 142
        Me.txtUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtpass
        '
        Me.txtpass.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtpass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpass.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpass.ForeColor = System.Drawing.Color.Blue
        Me.txtpass.Location = New System.Drawing.Point(492, 258)
        Me.txtpass.Multiline = True
        Me.txtpass.Name = "txtpass"
        Me.txtpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpass.Size = New System.Drawing.Size(277, 49)
        Me.txtpass.TabIndex = 143
        Me.txtpass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Salmon
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(594, 394)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(175, 56)
        Me.btnCancel.TabIndex = 144
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("NSimSun", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Tan
        Me.Label2.Location = New System.Drawing.Point(210, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(541, 35)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "By Smile heart"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'XfrmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(854, 480)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboDatabase)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtpass)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.lblVer)
        Me.Controls.Add(Me.lblDB)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lblpass)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "XfrmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Smile Heart Login"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblpass As Label
    Friend WithEvents btnLogin As Button
    Friend WithEvents lblDB As Label
    Friend WithEvents lblVer As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtpass As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents cboDatabase As ComboBox
    Friend WithEvents Label2 As Label
End Class


