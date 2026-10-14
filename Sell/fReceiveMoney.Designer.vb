<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fReceiveMoney
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fReceiveMoney))
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtReceive = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnBack = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDot = New DevExpress.XtraEditors.SimpleButton()
        Me.btn0 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn9 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn8 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn7 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn6 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn5 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn4 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn3 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn2 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn1 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn20 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn50 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn100 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn500 = New DevExpress.XtraEditors.SimpleButton()
        Me.btn1000 = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtReceive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(210, 41)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "ยอดที่ต้องชำระ"
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI Semibold", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Red
        Me.lblTotal.Location = New System.Drawing.Point(250, 24)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(209, 54)
        Me.lblTotal.TabIndex = 21
        Me.lblTotal.Text = "0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblTotal)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(491, 120)
        Me.GroupBox1.TabIndex = 22
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(453, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 41)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "฿"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtReceive)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 136)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(491, 120)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        '
        'txtReceive
        '
        Me.txtReceive.EditValue = "0.00"
        Me.txtReceive.Location = New System.Drawing.Point(256, 21)
        Me.txtReceive.Name = "txtReceive"
        Me.txtReceive.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtReceive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceive.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.txtReceive.Properties.Appearance.Options.UseBorderColor = True
        Me.txtReceive.Properties.Appearance.Options.UseFont = True
        Me.txtReceive.Properties.Appearance.Options.UseForeColor = True
        Me.txtReceive.Properties.Appearance.Options.UseTextOptions = True
        Me.txtReceive.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtReceive.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtReceive.Size = New System.Drawing.Size(201, 58)
        Me.txtReceive.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(453, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 41)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "฿"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(241, 41)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "เงินที่รับจากลูกค้า"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lblChange)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 263)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(491, 120)
        Me.GroupBox3.TabIndex = 24
        Me.GroupBox3.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(453, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 41)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "฿"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 34)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 41)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "เงินทอน"
        '
        'lblChange
        '
        Me.lblChange.Font = New System.Drawing.Font("Segoe UI Semibold", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblChange.Location = New System.Drawing.Point(249, 24)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(210, 54)
        Me.lblChange.TabIndex = 21
        Me.lblChange.Text = "0.00"
        Me.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnOK)
        Me.PanelControl1.Controls.Add(Me.btnCancel)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 399)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(861, 75)
        Me.PanelControl1.TabIndex = 25
        '
        'btnOK
        '
        Me.btnOK.Appearance.BackColor = System.Drawing.Color.Lime
        Me.btnOK.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Appearance.ForeColor = System.Drawing.Color.White
        Me.btnOK.Appearance.Options.UseBackColor = True
        Me.btnOK.Appearance.Options.UseFont = True
        Me.btnOK.Appearance.Options.UseForeColor = True
        Me.btnOK.ImageOptions.Image = CType(resources.GetObject("btnOK.ImageOptions.Image"), System.Drawing.Image)
        Me.btnOK.Location = New System.Drawing.Point(668, 10)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(144, 50)
        Me.btnOK.TabIndex = 36
        Me.btnOK.Text = "รับเงิน"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Segoe UI", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Appearance.Options.UseBackColor = True
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.Options.UseForeColor = True
        Me.btnCancel.ImageOptions.Image = CType(resources.GetObject("btnCancel.ImageOptions.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(78, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(144, 50)
        Me.btnCancel.TabIndex = 35
        Me.btnCancel.Text = "ยกเลิก"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnBack)
        Me.GroupBox4.Controls.Add(Me.btnDot)
        Me.GroupBox4.Controls.Add(Me.btn0)
        Me.GroupBox4.Controls.Add(Me.btn9)
        Me.GroupBox4.Controls.Add(Me.btn8)
        Me.GroupBox4.Controls.Add(Me.btn7)
        Me.GroupBox4.Controls.Add(Me.btn6)
        Me.GroupBox4.Controls.Add(Me.btn5)
        Me.GroupBox4.Controls.Add(Me.btn4)
        Me.GroupBox4.Controls.Add(Me.btn3)
        Me.GroupBox4.Controls.Add(Me.btn2)
        Me.GroupBox4.Controls.Add(Me.btn1)
        Me.GroupBox4.Controls.Add(Me.btn20)
        Me.GroupBox4.Controls.Add(Me.btn50)
        Me.GroupBox4.Controls.Add(Me.btn100)
        Me.GroupBox4.Controls.Add(Me.btn500)
        Me.GroupBox4.Controls.Add(Me.btn1000)
        Me.GroupBox4.Controls.Add(Me.btnClear)
        Me.GroupBox4.Location = New System.Drawing.Point(500, 7)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(361, 376)
        Me.GroupBox4.TabIndex = 23
        Me.GroupBox4.TabStop = False
        '
        'btnBack
        '
        Me.btnBack.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btnBack.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnBack.Appearance.Options.UseBackColor = True
        Me.btnBack.Appearance.Options.UseFont = True
        Me.btnBack.Appearance.Options.UseForeColor = True
        Me.btnBack.Location = New System.Drawing.Point(187, 20)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(166, 47)
        Me.btnBack.TabIndex = 54
        Me.btnBack.Text = "<="
        '
        'btnDot
        '
        Me.btnDot.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btnDot.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDot.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnDot.Appearance.Options.UseBackColor = True
        Me.btnDot.Appearance.Options.UseFont = True
        Me.btnDot.Appearance.Options.UseForeColor = True
        Me.btnDot.Location = New System.Drawing.Point(237, 308)
        Me.btnDot.Name = "btnDot"
        Me.btnDot.Size = New System.Drawing.Size(100, 47)
        Me.btnDot.TabIndex = 53
        Me.btnDot.Text = "."
        '
        'btn0
        '
        Me.btn0.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn0.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn0.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn0.Appearance.Options.UseBackColor = True
        Me.btn0.Appearance.Options.UseFont = True
        Me.btn0.Appearance.Options.UseForeColor = True
        Me.btn0.Location = New System.Drawing.Point(15, 308)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(211, 47)
        Me.btn0.TabIndex = 52
        Me.btn0.Text = "0"
        '
        'btn9
        '
        Me.btn9.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn9.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn9.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn9.Appearance.Options.UseBackColor = True
        Me.btn9.Appearance.Options.UseFont = True
        Me.btn9.Appearance.Options.UseForeColor = True
        Me.btn9.Location = New System.Drawing.Point(237, 255)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(100, 47)
        Me.btn9.TabIndex = 51
        Me.btn9.Text = "9"
        '
        'btn8
        '
        Me.btn8.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn8.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn8.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn8.Appearance.Options.UseBackColor = True
        Me.btn8.Appearance.Options.UseFont = True
        Me.btn8.Appearance.Options.UseForeColor = True
        Me.btn8.Location = New System.Drawing.Point(126, 255)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(100, 47)
        Me.btn8.TabIndex = 50
        Me.btn8.Text = "8"
        '
        'btn7
        '
        Me.btn7.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn7.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn7.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn7.Appearance.Options.UseBackColor = True
        Me.btn7.Appearance.Options.UseFont = True
        Me.btn7.Appearance.Options.UseForeColor = True
        Me.btn7.Location = New System.Drawing.Point(15, 255)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(100, 47)
        Me.btn7.TabIndex = 49
        Me.btn7.Text = "7"
        '
        'btn6
        '
        Me.btn6.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn6.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn6.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn6.Appearance.Options.UseBackColor = True
        Me.btn6.Appearance.Options.UseFont = True
        Me.btn6.Appearance.Options.UseForeColor = True
        Me.btn6.Location = New System.Drawing.Point(237, 202)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(100, 47)
        Me.btn6.TabIndex = 48
        Me.btn6.Text = "6"
        '
        'btn5
        '
        Me.btn5.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn5.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn5.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn5.Appearance.Options.UseBackColor = True
        Me.btn5.Appearance.Options.UseFont = True
        Me.btn5.Appearance.Options.UseForeColor = True
        Me.btn5.Location = New System.Drawing.Point(126, 202)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(100, 47)
        Me.btn5.TabIndex = 47
        Me.btn5.Text = "5"
        '
        'btn4
        '
        Me.btn4.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn4.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn4.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn4.Appearance.Options.UseBackColor = True
        Me.btn4.Appearance.Options.UseFont = True
        Me.btn4.Appearance.Options.UseForeColor = True
        Me.btn4.Location = New System.Drawing.Point(15, 202)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(100, 47)
        Me.btn4.TabIndex = 46
        Me.btn4.Text = "4"
        '
        'btn3
        '
        Me.btn3.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn3.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn3.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn3.Appearance.Options.UseBackColor = True
        Me.btn3.Appearance.Options.UseFont = True
        Me.btn3.Appearance.Options.UseForeColor = True
        Me.btn3.Location = New System.Drawing.Point(237, 149)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(100, 47)
        Me.btn3.TabIndex = 45
        Me.btn3.Text = "3"
        '
        'btn2
        '
        Me.btn2.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn2.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn2.Appearance.Options.UseBackColor = True
        Me.btn2.Appearance.Options.UseFont = True
        Me.btn2.Appearance.Options.UseForeColor = True
        Me.btn2.Location = New System.Drawing.Point(126, 149)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(100, 47)
        Me.btn2.TabIndex = 44
        Me.btn2.Text = "2"
        '
        'btn1
        '
        Me.btn1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn1.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn1.Appearance.Options.UseBackColor = True
        Me.btn1.Appearance.Options.UseFont = True
        Me.btn1.Appearance.Options.UseForeColor = True
        Me.btn1.Location = New System.Drawing.Point(15, 149)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(100, 47)
        Me.btn1.TabIndex = 43
        Me.btn1.Text = "1"
        '
        'btn20
        '
        Me.btn20.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn20.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn20.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn20.Appearance.Options.UseBackColor = True
        Me.btn20.Appearance.Options.UseFont = True
        Me.btn20.Appearance.Options.UseForeColor = True
        Me.btn20.Location = New System.Drawing.Point(273, 74)
        Me.btn20.Name = "btn20"
        Me.btn20.Size = New System.Drawing.Size(80, 47)
        Me.btn20.TabIndex = 42
        Me.btn20.Text = "20 ฿"
        '
        'btn50
        '
        Me.btn50.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn50.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn50.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn50.Appearance.Options.UseBackColor = True
        Me.btn50.Appearance.Options.UseFont = True
        Me.btn50.Appearance.Options.UseForeColor = True
        Me.btn50.Location = New System.Drawing.Point(187, 73)
        Me.btn50.Name = "btn50"
        Me.btn50.Size = New System.Drawing.Size(80, 47)
        Me.btn50.TabIndex = 41
        Me.btn50.Text = "50 ฿"
        '
        'btn100
        '
        Me.btn100.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn100.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn100.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn100.Appearance.Options.UseBackColor = True
        Me.btn100.Appearance.Options.UseFont = True
        Me.btn100.Appearance.Options.UseForeColor = True
        Me.btn100.Location = New System.Drawing.Point(101, 73)
        Me.btn100.Name = "btn100"
        Me.btn100.Size = New System.Drawing.Size(80, 47)
        Me.btn100.TabIndex = 40
        Me.btn100.Text = "100 ฿"
        '
        'btn500
        '
        Me.btn500.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn500.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn500.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn500.Appearance.Options.UseBackColor = True
        Me.btn500.Appearance.Options.UseFont = True
        Me.btn500.Appearance.Options.UseForeColor = True
        Me.btn500.Location = New System.Drawing.Point(15, 73)
        Me.btn500.Name = "btn500"
        Me.btn500.Size = New System.Drawing.Size(80, 47)
        Me.btn500.TabIndex = 39
        Me.btn500.Text = "500 ฿"
        '
        'btn1000
        '
        Me.btn1000.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btn1000.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn1000.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btn1000.Appearance.Options.UseBackColor = True
        Me.btn1000.Appearance.Options.UseFont = True
        Me.btn1000.Appearance.Options.UseForeColor = True
        Me.btn1000.Location = New System.Drawing.Point(101, 20)
        Me.btn1000.Name = "btn1000"
        Me.btn1000.Size = New System.Drawing.Size(80, 47)
        Me.btn1000.TabIndex = 38
        Me.btn1000.Text = "1000 ฿"
        '
        'btnClear
        '
        Me.btnClear.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.btnClear.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Appearance.ForeColor = System.Drawing.Color.Black
        Me.btnClear.Appearance.Options.UseBackColor = True
        Me.btnClear.Appearance.Options.UseFont = True
        Me.btnClear.Appearance.Options.UseForeColor = True
        Me.btnClear.Location = New System.Drawing.Point(15, 20)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(80, 47)
        Me.btnClear.TabIndex = 37
        Me.btnClear.Text = "Clear"
        '
        'fReceiveMoney
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(861, 474)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI Semilight", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "fReceiveMoney"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ชำระเงินสด"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtReceive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label7 As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblChange As Label
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn20 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn50 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn100 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn500 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn1000 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn9 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn8 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn7 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn6 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn5 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDot As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btn0 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtReceive As DevExpress.XtraEditors.TextEdit
End Class
