<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBeverageOption
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBeverageOption))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtNum = New Krypton.Toolkit.KryptonTextBox()
        Me.txtPrice = New Krypton.Toolkit.KryptonTextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txtNameMenu = New Krypton.Toolkit.KryptonTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSignature = New System.Windows.Forms.Button()
        Me.btnFrappe = New System.Windows.Forms.Button()
        Me.btnHot = New System.Windows.Forms.Button()
        Me.btnIced = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.rdoS120 = New Krypton.Toolkit.KryptonRadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rdoS100 = New Krypton.Toolkit.KryptonRadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.rdoS50 = New Krypton.Toolkit.KryptonRadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rdoS25 = New Krypton.Toolkit.KryptonRadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rdoS0 = New Krypton.Toolkit.KryptonRadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.KryptonCheckBox5 = New Krypton.Toolkit.KryptonCheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.KryptonCheckBox4 = New Krypton.Toolkit.KryptonCheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.KryptonCheckBox3 = New Krypton.Toolkit.KryptonCheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.KryptonCheckBox2 = New Krypton.Toolkit.KryptonCheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.KryptonCheckBox1 = New Krypton.Toolkit.KryptonCheckBox()
        Me.btnAddToBill = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtDetail = New Krypton.Toolkit.KryptonTextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtNum)
        Me.Panel1.Controls.Add(Me.txtPrice)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.txtNameMenu)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(926, 126)
        Me.Panel1.TabIndex = 0
        '
        'txtNum
        '
        Me.txtNum.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.txtNum.Location = New System.Drawing.Point(583, 67)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.txtNum.ReadOnly = True
        Me.txtNum.Size = New System.Drawing.Size(183, 39)
        Me.txtNum.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNum.TabIndex = 45
        '
        'txtPrice
        '
        Me.txtPrice.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.txtPrice.Location = New System.Drawing.Point(277, 66)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(199, 39)
        Me.txtPrice.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.TabIndex = 44
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(96, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(123, 97)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 43
        Me.PictureBox1.TabStop = False
        '
        'txtNameMenu
        '
        Me.txtNameMenu.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.txtNameMenu.Location = New System.Drawing.Point(277, 12)
        Me.txtNameMenu.Name = "txtNameMenu"
        Me.txtNameMenu.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.txtNameMenu.ReadOnly = True
        Me.txtNameMenu.Size = New System.Drawing.Size(489, 49)
        Me.txtNameMenu.StateCommon.Content.Color1 = System.Drawing.Color.Magenta
        Me.txtNameMenu.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameMenu.TabIndex = 42
        Me.txtNameMenu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.btnSignature)
        Me.GroupBox1.Controls.Add(Me.btnFrappe)
        Me.GroupBox1.Controls.Add(Me.btnHot)
        Me.GroupBox1.Controls.Add(Me.btnIced)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 126)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(926, 123)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "1. เลือกประเภท"
        '
        'btnSignature
        '
        Me.btnSignature.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSignature.BackColor = System.Drawing.Color.RosyBrown
        Me.btnSignature.FlatAppearance.BorderSize = 0
        Me.btnSignature.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSignature.ForeColor = System.Drawing.Color.Black
        Me.btnSignature.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSignature.Location = New System.Drawing.Point(731, 43)
        Me.btnSignature.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSignature.Name = "btnSignature"
        Me.btnSignature.Size = New System.Drawing.Size(183, 63)
        Me.btnSignature.TabIndex = 62
        Me.btnSignature.Text = "ซิกเนเจอร์ (Signature)"
        Me.btnSignature.UseVisualStyleBackColor = False
        '
        'btnFrappe
        '
        Me.btnFrappe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFrappe.BackColor = System.Drawing.Color.Honeydew
        Me.btnFrappe.FlatAppearance.BorderSize = 0
        Me.btnFrappe.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrappe.ForeColor = System.Drawing.Color.Black
        Me.btnFrappe.Image = CType(resources.GetObject("btnFrappe.Image"), System.Drawing.Image)
        Me.btnFrappe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFrappe.Location = New System.Drawing.Point(504, 43)
        Me.btnFrappe.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnFrappe.Name = "btnFrappe"
        Me.btnFrappe.Size = New System.Drawing.Size(163, 63)
        Me.btnFrappe.TabIndex = 61
        Me.btnFrappe.Text = "ปั่น (Frappe)"
        Me.btnFrappe.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFrappe.UseVisualStyleBackColor = False
        '
        'btnHot
        '
        Me.btnHot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHot.BackColor = System.Drawing.Color.MistyRose
        Me.btnHot.FlatAppearance.BorderSize = 0
        Me.btnHot.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHot.ForeColor = System.Drawing.Color.Black
        Me.btnHot.Image = CType(resources.GetObject("btnHot.Image"), System.Drawing.Image)
        Me.btnHot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHot.Location = New System.Drawing.Point(29, 43)
        Me.btnHot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnHot.Name = "btnHot"
        Me.btnHot.Size = New System.Drawing.Size(163, 63)
        Me.btnHot.TabIndex = 60
        Me.btnHot.Text = "ร้อน (Hot)"
        Me.btnHot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnHot.UseVisualStyleBackColor = False
        '
        'btnIced
        '
        Me.btnIced.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnIced.BackColor = System.Drawing.Color.PowderBlue
        Me.btnIced.FlatAppearance.BorderSize = 0
        Me.btnIced.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIced.ForeColor = System.Drawing.Color.Black
        Me.btnIced.Image = CType(resources.GetObject("btnIced.Image"), System.Drawing.Image)
        Me.btnIced.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIced.Location = New System.Drawing.Point(265, 43)
        Me.btnIced.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnIced.Name = "btnIced"
        Me.btnIced.Size = New System.Drawing.Size(163, 63)
        Me.btnIced.TabIndex = 59
        Me.btnIced.Text = "เย็น (ICed)"
        Me.btnIced.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnIced.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.rdoS120)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.rdoS100)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.rdoS50)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.rdoS25)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.rdoS0)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 249)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(926, 138)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "2. ระดับความหวาน"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(782, 77)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 39)
        Me.Label10.TabIndex = 73
        Me.Label10.Text = "หวานจึ้ง"
        '
        'rdoS120
        '
        Me.rdoS120.Location = New System.Drawing.Point(781, 43)
        Me.rdoS120.Name = "rdoS120"
        Me.rdoS120.Size = New System.Drawing.Size(82, 43)
        Me.rdoS120.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoS120.TabIndex = 72
        Me.rdoS120.Values.Text = "120 %"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(578, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 39)
        Me.Label8.TabIndex = 71
        Me.Label8.Text = "หวานพอดี"
        '
        'rdoS100
        '
        Me.rdoS100.Location = New System.Drawing.Point(585, 43)
        Me.rdoS100.Name = "rdoS100"
        Me.rdoS100.Size = New System.Drawing.Size(82, 43)
        Me.rdoS100.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoS100.TabIndex = 100
        Me.rdoS100.Values.Text = "100 %"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(395, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 39)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "หวานน้อย"
        '
        'rdoS50
        '
        Me.rdoS50.Location = New System.Drawing.Point(402, 43)
        Me.rdoS50.Name = "rdoS50"
        Me.rdoS50.Size = New System.Drawing.Size(72, 43)
        Me.rdoS50.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoS50.TabIndex = 68
        Me.rdoS50.Values.Text = "50 %"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(201, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 39)
        Me.Label6.TabIndex = 67
        Me.Label6.Text = "หวานจุบจิบ"
        '
        'rdoS25
        '
        Me.rdoS25.Location = New System.Drawing.Point(220, 43)
        Me.rdoS25.Name = "rdoS25"
        Me.rdoS25.Size = New System.Drawing.Size(72, 43)
        Me.rdoS25.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoS25.TabIndex = 66
        Me.rdoS25.Values.Text = "25 %"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(43, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 39)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "ไม่หวาน"
        '
        'rdoS0
        '
        Me.rdoS0.Location = New System.Drawing.Point(47, 43)
        Me.rdoS0.Name = "rdoS0"
        Me.rdoS0.Size = New System.Drawing.Size(62, 43)
        Me.rdoS0.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoS0.TabIndex = 64
        Me.rdoS0.Values.Text = "0 %"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.White
        Me.GroupBox3.Controls.Add(Me.Panel6)
        Me.GroupBox3.Controls.Add(Me.Panel5)
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Controls.Add(Me.Panel3)
        Me.GroupBox3.Controls.Add(Me.Panel2)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(0, 387)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(926, 297)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "3. ท็อปปิ้ง (เลือกได้มากกว่า 1 )"
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Controls.Add(Me.KryptonCheckBox5)
        Me.Panel6.Location = New System.Drawing.Point(731, 44)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(171, 105)
        Me.Panel6.TabIndex = 25
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label4.Location = New System.Drawing.Point(31, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 35)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "+5 บาท"
        '
        'KryptonCheckBox5
        '
        Me.KryptonCheckBox5.Location = New System.Drawing.Point(3, 9)
        Me.KryptonCheckBox5.Name = "KryptonCheckBox5"
        Me.KryptonCheckBox5.Size = New System.Drawing.Size(160, 43)
        Me.KryptonCheckBox5.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonCheckBox5.TabIndex = 0
        Me.KryptonCheckBox5.Values.Text = "โอริโอ้ (Topping)"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.KryptonCheckBox4)
        Me.Panel5.Location = New System.Drawing.Point(21, 175)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(171, 101)
        Me.Panel5.TabIndex = 27
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label3.Location = New System.Drawing.Point(56, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 35)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "+12 บาท"
        '
        'KryptonCheckBox4
        '
        Me.KryptonCheckBox4.Location = New System.Drawing.Point(5, 9)
        Me.KryptonCheckBox4.Name = "KryptonCheckBox4"
        Me.KryptonCheckBox4.Size = New System.Drawing.Size(150, 43)
        Me.KryptonCheckBox4.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonCheckBox4.TabIndex = 0
        Me.KryptonCheckBox4.Values.Text = "เจลลี่บราวชูก้าร์"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.KryptonCheckBox3)
        Me.Panel4.Location = New System.Drawing.Point(496, 44)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(171, 105)
        Me.Panel4.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label2.Location = New System.Drawing.Point(37, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 35)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "+5 บาท"
        '
        'KryptonCheckBox3
        '
        Me.KryptonCheckBox3.Location = New System.Drawing.Point(23, 9)
        Me.KryptonCheckBox3.Name = "KryptonCheckBox3"
        Me.KryptonCheckBox3.Size = New System.Drawing.Size(112, 43)
        Me.KryptonCheckBox3.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonCheckBox3.TabIndex = 0
        Me.KryptonCheckBox3.Values.Text = "เจลลี่น้ำผึ้ง"
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.KryptonCheckBox2)
        Me.Panel3.Location = New System.Drawing.Point(257, 44)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(171, 105)
        Me.Panel3.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label1.Location = New System.Drawing.Point(34, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 35)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "+15 บาท"
        '
        'KryptonCheckBox2
        '
        Me.KryptonCheckBox2.Location = New System.Drawing.Point(26, 9)
        Me.KryptonCheckBox2.Name = "KryptonCheckBox2"
        Me.KryptonCheckBox2.Size = New System.Drawing.Size(110, 43)
        Me.KryptonCheckBox2.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonCheckBox2.TabIndex = 0
        Me.KryptonCheckBox2.Values.Text = "ช็อตมัทฉะ"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.KryptonCheckBox1)
        Me.Panel2.Location = New System.Drawing.Point(21, 44)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(171, 105)
        Me.Panel2.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label9.Location = New System.Drawing.Point(31, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 35)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "+10 บาท"
        '
        'KryptonCheckBox1
        '
        Me.KryptonCheckBox1.Location = New System.Drawing.Point(25, 9)
        Me.KryptonCheckBox1.Name = "KryptonCheckBox1"
        Me.KryptonCheckBox1.Size = New System.Drawing.Size(109, 43)
        Me.KryptonCheckBox1.StateCommon.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KryptonCheckBox1.TabIndex = 0
        Me.KryptonCheckBox1.Values.Text = "ช็อตกาแฟ"
        '
        'btnAddToBill
        '
        Me.btnAddToBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddToBill.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnAddToBill.FlatAppearance.BorderSize = 0
        Me.btnAddToBill.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToBill.ForeColor = System.Drawing.Color.Transparent
        Me.btnAddToBill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddToBill.Location = New System.Drawing.Point(695, 753)
        Me.btnAddToBill.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnAddToBill.Name = "btnAddToBill"
        Me.btnAddToBill.Size = New System.Drawing.Size(207, 48)
        Me.btnAddToBill.TabIndex = 63
        Me.btnAddToBill.Text = "เพิ่มลงบิล"
        Me.btnAddToBill.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackColor = System.Drawing.Color.Gainsboro
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.Transparent
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(534, 753)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(141, 48)
        Me.Button6.TabIndex = 64
        Me.Button6.Text = "ยกเลิก"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'txtDetail
        '
        Me.txtDetail.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.txtDetail.Location = New System.Drawing.Point(12, 690)
        Me.txtDetail.Name = "txtDetail"
        Me.txtDetail.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.txtDetail.ReadOnly = True
        Me.txtDetail.Size = New System.Drawing.Size(889, 53)
        Me.txtDetail.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDetail.TabIndex = 65
        Me.txtDetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'FrmBeverageOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(926, 806)
        Me.Controls.Add(Me.txtDetail)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.btnAddToBill)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FrmBeverageOption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ตัวเลือก"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtNameMenu As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtNum As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents txtPrice As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnHot As Button
    Friend WithEvents btnIced As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents KryptonCheckBox1 As Krypton.Toolkit.KryptonCheckBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents KryptonCheckBox4 As Krypton.Toolkit.KryptonCheckBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents KryptonCheckBox3 As Krypton.Toolkit.KryptonCheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents KryptonCheckBox2 As Krypton.Toolkit.KryptonCheckBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents KryptonCheckBox5 As Krypton.Toolkit.KryptonCheckBox
    Friend WithEvents btnAddToBill As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents btnFrappe As Button
    Friend WithEvents txtDetail As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents btnSignature As Button
    Friend WithEvents rdoS0 As Krypton.Toolkit.KryptonRadioButton
    Friend WithEvents Label6 As Label
    Friend WithEvents rdoS25 As Krypton.Toolkit.KryptonRadioButton
    Friend WithEvents Label5 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents rdoS120 As Krypton.Toolkit.KryptonRadioButton
    Friend WithEvents Label8 As Label
    Friend WithEvents rdoS100 As Krypton.Toolkit.KryptonRadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents rdoS50 As Krypton.Toolkit.KryptonRadioButton
End Class
