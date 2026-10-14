'  
' Type: NewStock.fOfficerAddEdit
' Assembly: SabuySoft, Version=1.0.7471.10171, Culture=neutral, PublicKeyToken=null
' MVID: A0E28F6D-1A8F-4615-A343-F46533641B26
' Assembly location: \SabuySoft.exe

Imports Janus.Windows.GridEX
Imports NewStock.Properties
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Namespace NewStock

    Public Class fOfficerAddEdit
        Inherits Form
        Private pbPicture As PictureBox
        Public lbPicName As Label
        Private btPicName As Button
        Private label14 As Label
        Public tbMobile As TextBox
        Private label8 As Label
        Public tbTel As TextBox
        Public tbAddress As TextBox
        Private label6 As Label
        Private label3 As Label
        Private btCancel As Button
        Private btOK As Button
        Private label7 As Label
        Private label4 As Label
        Private label2 As Label
        Public tbName As TextBox
        Private label1 As Label
        Public lbOfficerNumber As Label
        Public tbSalary As TextBox
        Public tbPeopleID As TextBox
        Public cbSex As ComboBox
        Public tbPos As TextBox
        Private label5 As Label
        Public tbBarcode As TextBox
        Private label9 As Label
        Private cbAutoGenCode As CheckBox
        Private lbStatus As Label
        Public cbStatus As CheckBox
        Public tbOfficerCode As TextBox
        Private label16 As Label
        Private btCheckAll As Button
        Private btClear As Button
        Public lbNumberItemsBranch As Label
        Private label10 As Label
        Public dtGBranch As Janus.Windows.GridEX.GridEX
        Private tabControl1 As TabControl
        Private tabPage1 As TabPage
        Private tabPage2 As TabPage
        Private label11 As Label
        Public cbWebAppLimitBrand As ComboBox
        Private label12 As Label
        Private components As Container = Nothing
        Public iMode As Integer
        Public fParent As fOfficer
        Public [myClass] As [MyClass]
        Public lLoginONumber As Long = 0
        Public lWebAppBrandID As Long = 0
        Private iFX As Integer
        Private iFW As Integer
        Private iFH As Integer
        Private iFRatio As Double
        Public BranchDataSet As DataSet = New DataSet()
        Public BrandDataSet As DataSet = New DataSet()

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            pbPicture = New PictureBox()
            lbPicName = New Label()
            btPicName = New Button()
            tbSalary = New TextBox()
            label14 = New Label()
            tbMobile = New TextBox()
            label8 = New Label()
            lbOfficerNumber = New Label()
            tbPeopleID = New TextBox()
            tbTel = New TextBox()
            tbAddress = New TextBox()
            label6 = New Label()
            label3 = New Label()
            btCancel = New Button()
            btOK = New Button()
            label7 = New Label()
            label4 = New Label()
            label2 = New Label()
            tbName = New TextBox()
            label1 = New Label()
            cbSex = New ComboBox()
            tbPos = New TextBox()
            label5 = New Label()
            tbBarcode = New TextBox()
            label9 = New Label()
            cbAutoGenCode = New CheckBox()
            lbStatus = New Label()
            cbStatus = New CheckBox()
            tbOfficerCode = New TextBox()
            label16 = New Label()
            btCheckAll = New Button()
            btClear = New Button()
            lbNumberItemsBranch = New Label()
            label10 = New Label()
            dtGBranch = New Janus.Windows.GridEX.GridEX()
            tabControl1 = New TabControl()
            tabPage1 = New TabPage()
            tabPage2 = New TabPage()
            label11 = New Label()
            cbWebAppLimitBrand = New ComboBox()
            label12 = New Label()
            CType(pbPicture, ISupportInitialize).BeginInit()
            CType(dtGBranch, ISupportInitialize).BeginInit()
            tabControl1.SuspendLayout()
            tabPage1.SuspendLayout()
            tabPage2.SuspendLayout()
            SuspendLayout()
            pbPicture.BorderStyle = BorderStyle.Fixed3D
            pbPicture.Location = New Point(421, 239)
            pbPicture.Name = "pbPicture"
            pbPicture.Size = New Size(251, 201)
            pbPicture.SizeMode = PictureBoxSizeMode.StretchImage
            pbPicture.TabIndex = 57
            pbPicture.TabStop = False
            lbPicName.BackColor = Color.Transparent
            lbPicName.BorderStyle = BorderStyle.Fixed3D
            lbPicName.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbPicName.ForeColor = Color.Green
            lbPicName.Location = New Point(206, 186)
            lbPicName.Name = "lbPicName"
            lbPicName.Size = New Size(466, 50)
            lbPicName.TabIndex = 14
            btPicName.BackColor = Color.Lavender
            btPicName.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btPicName.Location = New Point(6, 186)
            btPicName.Name = "btPicName"
            btPicName.Size = New Size(194, 25)
            btPicName.TabIndex = 13
            btPicName.Text = "&Picture Name (ไฟล์รูปภาพ)"
            btPicName.UseVisualStyleBackColor = False
            AddHandler btPicName.Click, New EventHandler(AddressOf btPicName_Click)
            tbSalary.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbSalary.ForeColor = Color.Red
            tbSalary.Location = New Point(206, 339)
            tbSalary.MaxLength = 12
            tbSalary.Name = "tbSalary"
            tbSalary.Size = New Size(139, 22)
            tbSalary.TabIndex = 24
            tbSalary.Text = "0"
            tbSalary.TextAlign = HorizontalAlignment.Right
            AddHandler tbSalary.Leave, New EventHandler(AddressOf tbSalary_Leave)
            AddHandler tbSalary.KeyPress, New KeyPressEventHandler(AddressOf tbSalary_KeyPress)
            label14.BackColor = Color.Transparent
            label14.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label14.Location = New Point(8, 339)
            label14.Name = "label14"
            label14.Size = New Size(194, 23)
            label14.TabIndex = 23
            label14.Text = "Salar&y (เงินเดือน)"
            label14.TextAlign = ContentAlignment.MiddleRight
            tbMobile.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbMobile.ForeColor = Color.Blue
            tbMobile.Location = New Point(206, 264)
            tbMobile.MaxLength = 49
            tbMobile.Name = "tbMobile"
            tbMobile.Size = New Size(209, 22)
            tbMobile.TabIndex = 18
            AddHandler tbMobile.KeyPress, New KeyPressEventHandler(AddressOf tbMobile_KeyPress)
            AddHandler tbMobile.Enter, New EventHandler(AddressOf tbMobile_Enter)
            label8.BackColor = Color.Transparent
            label8.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label8.Location = New Point(6, 264)
            label8.Name = "label8"
            label8.Size = New Size(194, 23)
            label8.TabIndex = 17
            label8.Text = "&Mobile (เบอร์โทรศัพท์มือถือ)"
            label8.TextAlign = ContentAlignment.MiddleRight
            lbOfficerNumber.BackColor = Color.Transparent
            lbOfficerNumber.BorderStyle = BorderStyle.Fixed3D
            lbOfficerNumber.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbOfficerNumber.ForeColor = Color.Green
            lbOfficerNumber.Location = New Point(206, 5)
            lbOfficerNumber.Name = "lbOfficerNumber"
            lbOfficerNumber.Size = New Size(112, 23)
            lbOfficerNumber.TabIndex = 1
            lbOfficerNumber.TextAlign = ContentAlignment.MiddleRight
            tbPeopleID.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbPeopleID.ForeColor = Color.Blue
            tbPeopleID.Location = New Point(206, 289)
            tbPeopleID.MaxLength = 19
            tbPeopleID.Name = "tbPeopleID"
            tbPeopleID.Size = New Size(209, 22)
            tbPeopleID.TabIndex = 20
            AddHandler tbPeopleID.KeyPress, New KeyPressEventHandler(AddressOf tbPeopleID_KeyPress)
            AddHandler tbPeopleID.Enter, New EventHandler(AddressOf tbPeopleID_Enter)
            tbTel.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbTel.ForeColor = Color.Blue
            tbTel.Location = New Point(206, 239)
            tbTel.MaxLength = 49
            tbTel.Name = "tbTel"
            tbTel.Size = New Size(209, 22)
            tbTel.TabIndex = 16
            AddHandler tbTel.KeyPress, New KeyPressEventHandler(AddressOf tbTel_KeyPress)
            AddHandler tbTel.Enter, New EventHandler(AddressOf tbTel_Enter)
            tbAddress.BackColor = SystemColors.Window
            tbAddress.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbAddress.ForeColor = Color.Blue
            tbAddress.Location = New Point(206, 133)
            tbAddress.MaxLength = 199
            tbAddress.Multiline = True
            tbAddress.Name = "tbAddress"
            tbAddress.ScrollBars = ScrollBars.Vertical
            tbAddress.Size = New Size(466, 50)
            tbAddress.TabIndex = 12
            AddHandler tbAddress.KeyPress, New KeyPressEventHandler(AddressOf tbAddress_KeyPress)
            AddHandler tbAddress.Enter, New EventHandler(AddressOf tbAddress_Enter)
            label6.BackColor = Color.Transparent
            label6.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label6.Location = New Point(6, 289)
            label6.Name = "label6"
            label6.Size = New Size(194, 23)
            label6.TabIndex = 19
            label6.Text = "People &ID (เลขบัตรประชาชน)"
            label6.TextAlign = ContentAlignment.MiddleRight
            label3.BackColor = Color.Transparent
            label3.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label3.Location = New Point(6, 239)
            label3.Name = "label3"
            label3.Size = New Size(194, 23)
            label3.TabIndex = 15
            label3.Text = "&Tel (เบอร์โทรศัพท์)"
            label3.TextAlign = ContentAlignment.MiddleRight
            btCancel.BackColor = Color.Salmon
            btCancel.DialogResult = DialogResult.Cancel
            btCancel.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btCancel.Image = CType(Resources.Cancelsmall, Image)
            btCancel.Location = New Point(894, 492)
            btCancel.Name = "btCancel"
            btCancel.Size = New Size(102, 32)
            btCancel.TabIndex = 2
            btCancel.Text = "&Cancel"
            btCancel.TextImageRelation = TextImageRelation.ImageBeforeText
            btCancel.UseVisualStyleBackColor = False
            btOK.BackColor = Color.Yellow
            btOK.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btOK.Image = CType(Resources.OKsmall, Image)
            btOK.Location = New Point(781, 492)
            btOK.Name = "btOK"
            btOK.Size = New Size(107, 32)
            btOK.TabIndex = 1
            btOK.Text = "&OK (แก้ไข)"
            btOK.TextImageRelation = TextImageRelation.ImageBeforeText
            btOK.UseVisualStyleBackColor = False
            AddHandler btOK.Click, New EventHandler(AddressOf btOK_Click)
            label7.BackColor = Color.Transparent
            label7.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label7.Location = New Point(6, 106)
            label7.Name = "label7"
            label7.Size = New Size(194, 23)
            label7.TabIndex = 9
            label7.Text = "Se&x (เพศ)"
            label7.TextAlign = ContentAlignment.MiddleRight
            label4.BackColor = Color.Transparent
            label4.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label4.Location = New Point(6, 133)
            label4.Name = "label4"
            label4.Size = New Size(194, 23)
            label4.TabIndex = 11
            label4.Text = "&Address (ที่อยู่)"
            label4.TextAlign = ContentAlignment.MiddleRight
            label2.BackColor = Color.Transparent
            label2.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label2.ForeColor = Color.Red
            label2.Location = New Point(6, 31)
            label2.Name = "label2"
            label2.Size = New Size(194, 23)
            label2.TabIndex = 2
            label2.Text = "* &Name (ชื่อพนักงาน)"
            label2.TextAlign = ContentAlignment.MiddleRight
            tbName.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbName.ForeColor = Color.Blue
            tbName.Location = New Point(206, 31)
            tbName.MaxLength = 99
            tbName.Name = "tbName"
            tbName.Size = New Size(768, 22)
            tbName.TabIndex = 3
            AddHandler tbName.KeyPress, New KeyPressEventHandler(AddressOf tbName_KeyPress)
            AddHandler tbName.Enter, New EventHandler(AddressOf tbName_Enter)
            label1.BackColor = Color.Transparent
            label1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label1.ForeColor = SystemColors.ControlText
            label1.Location = New Point(6, 5)
            label1.Name = "label1"
            label1.Size = New Size(194, 23)
            label1.TabIndex = 0
            label1.Text = "Officer ID (ID พนักงาน)"
            label1.TextAlign = ContentAlignment.MiddleRight
            cbSex.DropDownStyle = ComboBoxStyle.DropDownList
            cbSex.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbSex.ForeColor = Color.Brown
            cbSex.Items.AddRange(New Object(1) {"ชาย", "หญิง"})
            cbSex.Location = New Point(206, 106)
            cbSex.Name = "cbSex"
            cbSex.Size = New Size(112, 24)
            cbSex.TabIndex = 10
            AddHandler cbSex.KeyPress, New KeyPressEventHandler(AddressOf cbSex_KeyPress)
            tbPos.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbPos.ForeColor = Color.Blue
            tbPos.Location = New Point(206, 314)
            tbPos.MaxLength = 49
            tbPos.Name = "tbPos"
            tbPos.Size = New Size(209, 22)
            tbPos.TabIndex = 22
            AddHandler tbPos.KeyPress, New KeyPressEventHandler(AddressOf tbPos_KeyPress)
            AddHandler tbPos.Enter, New EventHandler(AddressOf tbPos_Enter)
            label5.BackColor = Color.Transparent
            label5.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label5.Location = New Point(8, 314)
            label5.Name = "label5"
            label5.Size = New Size(194, 23)
            label5.TabIndex = 21
            label5.Text = "Po&sition (ตำแหน่ง)"
            label5.TextAlign = ContentAlignment.MiddleRight
            tbBarcode.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbBarcode.ForeColor = Color.Blue
            tbBarcode.Location = New Point(206, 81)
            tbBarcode.MaxLength = 49
            tbBarcode.Name = "tbBarcode"
            tbBarcode.Size = New Size(248, 22)
            tbBarcode.TabIndex = 7
            AddHandler tbBarcode.KeyPress, New KeyPressEventHandler(AddressOf tbBarcode_KeyPress)
            AddHandler tbBarcode.Enter, New EventHandler(AddressOf tbBarcode_Enter)
            label9.BackColor = Color.Transparent
            label9.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label9.ForeColor = SystemColors.ControlText
            label9.Location = New Point(112, 80)
            label9.Name = "label9"
            label9.Size = New Size(88, 23)
            label9.TabIndex = 6
            label9.Text = "&BarCode"
            label9.TextAlign = ContentAlignment.MiddleRight
            cbAutoGenCode.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbAutoGenCode.Location = New Point(460, 80)
            cbAutoGenCode.Name = "cbAutoGenCode"
            cbAutoGenCode.Size = New Size(192, 23)
            cbAutoGenCode.TabIndex = 8
            cbAutoGenCode.Text = "&Auto Generate Code"
            AddHandler cbAutoGenCode.KeyPress, New KeyPressEventHandler(AddressOf cbAutoGenCode_KeyPress)
            AddHandler cbAutoGenCode.CheckedChanged, New EventHandler(AddressOf cbAutoGenCode_CheckedChanged)
            lbStatus.BackColor = Color.Transparent
            lbStatus.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbStatus.ForeColor = SystemColors.ControlText
            lbStatus.Location = New Point(1, 417)
            lbStatus.Name = "lbStatus"
            lbStatus.Size = New Size(199, 23)
            lbStatus.TabIndex = 25
            lbStatus.Text = "Status (สถานะ)"
            lbStatus.TextAlign = ContentAlignment.MiddleRight
            cbStatus.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbStatus.Location = New Point(206, 417)
            cbStatus.Name = "cbStatus"
            cbStatus.Size = New Size(176, 23)
            cbStatus.TabIndex = 26
            cbStatus.Text = "Not &use (ไม่ใช้แล้ว)"
            AddHandler cbStatus.KeyPress, New KeyPressEventHandler(AddressOf cbStatus_KeyPress)
            tbOfficerCode.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbOfficerCode.ForeColor = Color.Blue
            tbOfficerCode.Location = New Point(206, 56)
            tbOfficerCode.MaxLength = 49
            tbOfficerCode.Name = "tbOfficerCode"
            tbOfficerCode.Size = New Size(209, 22)
            tbOfficerCode.TabIndex = 5
            AddHandler tbOfficerCode.KeyPress, New KeyPressEventHandler(AddressOf tbOfficerCode_KeyPress)
            AddHandler tbOfficerCode.Enter, New EventHandler(AddressOf tbOfficerCode_Enter)
            label16.BackColor = Color.Transparent
            label16.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label16.ForeColor = SystemColors.ControlText
            label16.Location = New Point(6, 57)
            label16.Name = "label16"
            label16.Size = New Size(194, 23)
            label16.TabIndex = 4
            label16.Text = "O&fficer Code (รหัสพนักงาน)"
            label16.TextAlign = ContentAlignment.MiddleRight
            btCheckAll.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btCheckAll.BackColor = Color.Violet
            btCheckAll.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btCheckAll.Location = New Point(832, 408)
            btCheckAll.Name = "btCheckAll"
            btCheckAll.Size = New Size(142, 32)
            btCheckAll.TabIndex = 31
            btCheckAll.Text = "Chec&k All"
            btCheckAll.TextImageRelation = TextImageRelation.ImageBeforeText
            btCheckAll.UseVisualStyleBackColor = False
            AddHandler btCheckAll.Click, New EventHandler(AddressOf btCheckAll_Click)
            btClear.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btClear.BackColor = Color.Violet
            btClear.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btClear.Location = New Point(680, 408)
            btClear.Name = "btClear"
            btClear.Size = New Size(150, 32)
            btClear.TabIndex = 30
            btClear.Text = "&Uncheck All"
            btClear.TextImageRelation = TextImageRelation.ImageBeforeText
            btClear.UseVisualStyleBackColor = False
            AddHandler btClear.Click, New EventHandler(AddressOf btClear_Click)
            lbNumberItemsBranch.BackColor = Color.Transparent
            lbNumberItemsBranch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbNumberItemsBranch.ForeColor = SystemColors.WindowText
            lbNumberItemsBranch.Location = New Point(884, 58)
            lbNumberItemsBranch.Name = "lbNumberItemsBranch"
            lbNumberItemsBranch.Size = New Size(90, 22)
            lbNumberItemsBranch.TabIndex = 27
            lbNumberItemsBranch.Text = "0 รายการ"
            lbNumberItemsBranch.TextAlign = ContentAlignment.BottomRight
            label10.BackColor = Color.Transparent
            label10.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label10.ForeColor = SystemColors.WindowText
            label10.Location = New Point(678, 57)
            label10.Name = "label10"
            label10.Size = New Size(144, 22)
            label10.TabIndex = 28
            label10.Text = "Branc&h (สาขา)"
            label10.TextAlign = ContentAlignment.BottomLeft
            dtGBranch.AlternatingRowFormatStyle.BackColor = Color.AliceBlue
            dtGBranch.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
            dtGBranch.AutoEdit = True
            dtGBranch.CardColumnHeaderFormatStyle.BackColor = Color.White
            dtGBranch.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCellsAndHeader
            dtGBranch.EditorsControlStyle.ButtonAppearance = ButtonAppearance.Regular
            dtGBranch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0)
            dtGBranch.GroupByBoxFormatStyle.BackColor = Color.Silver
            dtGBranch.GroupByBoxFormatStyle.BackColorGradient = SystemColors.ControlDarkDark
            dtGBranch.GroupByBoxInfoFormatStyle.BackColor = Color.White
            dtGBranch.GroupByBoxInfoFormatStyle.ForeColor = SystemColors.ControlDarkDark
            dtGBranch.GroupByBoxVisible = False
            dtGBranch.GroupMode = GroupMode.Collapsed
            dtGBranch.GroupRowFormatStyle.BackColor = Color.White
            dtGBranch.HideSelection = HideSelection.HighlightInactive
            dtGBranch.Location = New Point(680, 80)
            dtGBranch.Name = "dtGBranch"
            dtGBranch.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
            dtGBranch.Size = New Size(294, 326)
            dtGBranch.TabIndex = 29
            dtGBranch.VisualStyle = VisualStyle.Office2010
            tabControl1.Controls.Add(tabPage1)
            tabControl1.Controls.Add(tabPage2)
            tabControl1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tabControl1.Location = New Point(8, 4)
            tabControl1.Name = "tabControl1"
            tabControl1.SelectedIndex = 0
            tabControl1.Size = New Size(988, 478)
            tabControl1.TabIndex = 0
            tabPage1.BackColor = Color.LightCyan
            tabPage1.Controls.Add(label1)
            tabPage1.Controls.Add(tbName)
            tabPage1.Controls.Add(label2)
            tabPage1.Controls.Add(lbStatus)
            tabPage1.Controls.Add(btCheckAll)
            tabPage1.Controls.Add(cbStatus)
            tabPage1.Controls.Add(btClear)
            tabPage1.Controls.Add(tbPos)
            tabPage1.Controls.Add(label4)
            tabPage1.Controls.Add(label5)
            tabPage1.Controls.Add(label7)
            tabPage1.Controls.Add(pbPicture)
            tabPage1.Controls.Add(lbNumberItemsBranch)
            tabPage1.Controls.Add(lbPicName)
            tabPage1.Controls.Add(tbAddress)
            tabPage1.Controls.Add(btPicName)
            tabPage1.Controls.Add(tbSalary)
            tabPage1.Controls.Add(label10)
            tabPage1.Controls.Add(label14)
            tabPage1.Controls.Add(lbOfficerNumber)
            tabPage1.Controls.Add(tbMobile)
            tabPage1.Controls.Add(CType(dtGBranch, Control))
            tabPage1.Controls.Add(label8)
            tabPage1.Controls.Add(cbSex)
            tabPage1.Controls.Add(tbPeopleID)
            tabPage1.Controls.Add(tbOfficerCode)
            tabPage1.Controls.Add(tbTel)
            tabPage1.Controls.Add(cbAutoGenCode)
            tabPage1.Controls.Add(label6)
            tabPage1.Controls.Add(label16)
            tabPage1.Controls.Add(label3)
            tabPage1.Controls.Add(label9)
            tabPage1.Controls.Add(tbBarcode)
            tabPage1.Location = New Point(4, 25)
            tabPage1.Name = "tabPage1"
            tabPage1.Padding = New Padding(3)
            tabPage1.Size = New Size(980, 449)
            tabPage1.TabIndex = 0
            tabPage1.Text = "Officer Data"
            tabPage2.BackColor = Color.LightCyan
            tabPage2.Controls.Add(label12)
            tabPage2.Controls.Add(label11)
            tabPage2.Controls.Add(cbWebAppLimitBrand)
            tabPage2.Location = New Point(4, 25)
            tabPage2.Name = "tabPage2"
            tabPage2.Padding = New Padding(3)
            tabPage2.Size = New Size(980, 449)
            tabPage2.TabIndex = 1
            tabPage2.Text = "Web Application"
            label11.BackColor = Color.Transparent
            label11.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label11.Location = New Point(6, 17)
            label11.Name = "label11"
            label11.Size = New Size(195, 24)
            label11.TabIndex = 0
            label11.Text = "Web App Limit Brand View"
            label11.TextAlign = ContentAlignment.MiddleRight
            cbWebAppLimitBrand.DropDownStyle = ComboBoxStyle.DropDownList
            cbWebAppLimitBrand.DropDownWidth = 104
            cbWebAppLimitBrand.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbWebAppLimitBrand.ForeColor = Color.Brown
            cbWebAppLimitBrand.Location = New Point(207, 17)
            cbWebAppLimitBrand.MaxDropDownItems = 10
            cbWebAppLimitBrand.MaxLength = 9
            cbWebAppLimitBrand.Name = "cbWebAppLimitBrand"
            cbWebAppLimitBrand.Size = New Size(268, 24)
            cbWebAppLimitBrand.TabIndex = 1
            label12.BackColor = Color.Transparent
            label12.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label12.Location = New Point(207, 44)
            label12.Name = "label12"
            label12.Size = New Size(748, 25)
            label12.TabIndex = 2
            label12.Text = "จำกัดการดูยี่ห้อในหน้า ""Stock (คลังสินค้า)"" และ ""Sell Item View (รายการขายแยกตามสินค้า)"" ในระบบ Web Application"
            AutoScaleBaseSize = New Size(5, 13)
            BackColor = Color.Bisque
            CancelButton = btCancel
            ClientSize = New Size(1008, 532)
            Controls.Add(tabControl1)
            Controls.Add(btCancel)
            Controls.Add(btOK)
            MaximizeBox = False
            MaximumSize = New Size(1024, 570)
            MinimizeBox = False
            MinimumSize = New Size(1024, 570)
            Name = NameOf(fOfficerAddEdit)
            ShowInTaskbar = False
            StartPosition = FormStartPosition.CenterParent
            Text = "Officer Edit ( แก้ไขรายการพนักงาน )"
            AddHandler Load, New EventHandler(AddressOf fOfficerAddEdit_Load)
            AddHandler Shown, New EventHandler(AddressOf fOfficerAddEdit_Shown)
            CType(pbPicture, ISupportInitialize).EndInit()
            CType(dtGBranch, ISupportInitialize).EndInit()
            tabControl1.ResumeLayout(False)
            tabPage1.ResumeLayout(False)
            tabPage1.PerformLayout()
            tabPage2.ResumeLayout(False)
            ResumeLayout(False)
        End Sub

        Private Sub fOfficerAddEdit_Load(sender As Object, e As EventArgs)
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim flag = True
            Dim text = ""
            Cursor = Cursors.WaitCursor
            sqlConnection.Open()
            iFX = pbPicture.Location.X
            iFW = pbPicture.Width
            iFH = pbPicture.Height
            iFRatio = iFW / iFH
            If flag Then
                command.CommandText = "SELECT CAST(0 AS bit) AS 'Login',Branch.BranchNumber,RTRIM(Branch.BranchName) AS 'BranchName' FROM Branch WHERE Branch.Status IS NULL ORDER BY Branch.BranchNumber ASC "
                Try
                    Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter()
                    sqlDataAdapter.SelectCommand = command
                    BranchDataSet.Clear()
                    sqlDataAdapter.Fill(BranchDataSet, "Branch")
                    sqlDataAdapter.Dispose()
                    dtGBranch.SetDataBinding(BranchDataSet, "Branch")
                    dtGBranch.RetrieveStructure()
                    ChangeGridFormatBranch()
                    ChangeColumnCaptionBranch()
                    HideGridColumnBranch()
                    dtGBranch.AutoSizeColumns()
                    SetEditColumn()
                    lbNumberItemsBranch.Text = BranchDataSet.Tables("Branch").Rows.Count.ToString() & " รายการ"
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            If flag Then
                Try
                    command.CommandText = "SELECT Brand.ID,RTRIM(Brand.Name) AS 'Name' FROM Brand WHERE Brand.Status IS NULL ORDER BY Brand.Name ASC "
                    Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter()
                    sqlDataAdapter.SelectCommand = command
                    BrandDataSet.Clear()
                    sqlDataAdapter.Fill(BrandDataSet, "Brand")
                    sqlDataAdapter.Dispose()
                    Dim row As DataRow = BrandDataSet.Tables("Brand").NewRow()
                    row("ID") = 0
                    row("Name") = "- ไม่จำกัดยี่ห้อในการดู -"
                    BrandDataSet.Tables("Brand").Rows.InsertAt(row, 0)
                    cbWebAppLimitBrand.ValueMember = "Brand.ID"
                    cbWebAppLimitBrand.DisplayMember = "Brand.Name"
                    cbWebAppLimitBrand.DataSource = BrandDataSet
                    cbWebAppLimitBrand.SelectedIndex = 0
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            sqlConnection.Close()
            Cursor = Cursors.Default
            If iMode = 0 Then
                Me.Text = "เพิ่มรายการพนักงาน ( Add new Officer )"
                btOK.Text = "&OK (เพิ่ม)"
                cbSex.SelectedIndex = 0
                cbStatus.Visible = False
                lbStatus.Visible = False
                tbSalary.Text = 0D.ToString([myClass].GetDecimalPlacesFormatString())
                If BranchDataSet.Tables("Branch").Rows.Count = 1 Then BranchDataSet.Tables("Branch").Rows(0)("Login") = True
                lbOfficerNumber.Text = GetThisOfficerNumber().ToString()
            Else
                Me.Text = "แก้ไขรายการพนักงาน ( Edit Officer )"
                btOK.Text = "&OK (แก้ไข)"
                GetOfficerBranch()
                If Convert.ToInt32(lbOfficerNumber.Text) = 1 Then
                    tbName.Enabled = False
                    tbAddress.Enabled = False
                    btPicName.Enabled = False
                    tbTel.Enabled = False
                    tbMobile.Enabled = False
                    tbPeopleID.Enabled = False
                    tbPos.Enabled = False
                    tbSalary.Enabled = False
                    cbStatus.Enabled = False
                    btClear.Enabled = False
                    btCheckAll.Enabled = False
                    cbWebAppLimitBrand.Enabled = False
                End If
                If lWebAppBrandID > 0L Then cbWebAppLimitBrand.SelectedValue = lWebAppBrandID
                ShowPicture()
            End If
            If flag Then Return
            Dim num As Integer = MessageBox.Show(text, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Close()
        End Sub

        Private Function GetThisOfficerNumber() As Integer
            Dim thisOfficerNumber = 1
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            sqlConnection.Open()
            command.CommandText = "SELECT MAX(OfficerNumber) FROM Officer "
            Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
            While sqlDataReader.Read()
                If Not sqlDataReader.IsDBNull(0) Then thisOfficerNumber = 1 + sqlDataReader.GetInt32(0)
            End While
            sqlDataReader.Close()
            sqlConnection.Close()
            Return thisOfficerNumber
        End Function

        Private Sub tbName_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            tbOfficerCode.Focus()
        End Sub

        Private Sub tbBarcode_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            cbSex.Focus()
            cbSex.SelectAll()
        End Sub

        Private Sub cbAutoGenCode_KeyPress(sender As Object, e As KeyPressEventArgs)
            If e.KeyChar <> ChrW(13) Then Return
            cbSex.Focus()
            cbSex.SelectAll()
        End Sub

        Private Sub cbSex_KeyPress(sender As Object, e As KeyPressEventArgs)
            If e.KeyChar <> ChrW(13) Then Return
            If tbAddress.Enabled Then
                tbAddress.Focus()
                tbAddress.SelectAll()
            ElseIf iMode = 1 Then
                tbOfficerCode.Focus()
            End If
        End Sub

        Private Sub tbAddress_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            e.Handled = True
            tbTel.Focus()
            tbTel.SelectAll()
        End Sub

        Private Sub tbTel_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            tbMobile.Focus()
            tbMobile.SelectAll()
        End Sub

        Private Sub tbMobile_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            tbPeopleID.Focus()
            tbPeopleID.SelectAll()
        End Sub

        Private Sub tbPeopleID_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            tbPos.Focus()
            tbPos.SelectAll()
        End Sub

        Private Sub tbPos_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            tbSalary.Focus()
            tbSalary.SelectAll()
        End Sub

        Private Sub tbSalary_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckMoney(e.KeyChar, tbSalary.Text, tbSalary.SelectionStart, tbSalary.SelectionLength) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            If iMode = 0 Then
                InsertData()
            Else
                cbStatus.Focus()
            End If
        End Sub

        Private Sub tbSalary_Leave(sender As Object, e As EventArgs)
            tbSalaryLeave()
        End Sub

        Private Sub tbSalaryLeave()
            Dim num As Decimal
            Try
                num = Convert.ToDecimal(tbSalary.Text)
            Catch
                num = 0D
            End Try
            tbSalary.Text = num.ToString([myClass].GetDecimalPlacesFormatString())
        End Sub

        Private Sub btPicName_Click(sender As Object, e As EventArgs)
            Dim path As String = [myClass].NewStockPath.ToString() & "Pictures"
            Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
            If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg,*.bmp,*.png)|*.jpg;*.jpeg;*.bmp;*.png|All files (*.*)|*.*"
            openFileDialog.InitialDirectory = path
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                lbPicName.Text = openFileDialog.FileName.Replace([myClass].NewStockPath.Substring(0, [myClass].NewStockPath.Length - 1), "")
            Else
                lbPicName.Text = ""
            End If
            Directory.SetCurrentDirectory([myClass].NewStockPath)
            ShowPicture()
            tbTel.Focus()
            tbTel.SelectAll()
        End Sub

        Private Sub ShowPicture()
            If Not Equals(lbPicName.Text, "") Then
                Dim image As Image = Nothing
                Dim flag = True
                Dim currentDirectory As String = Directory.GetCurrentDirectory()
                If lbPicName.Text(0) = "\"c Then
                    Try
                        image = Image.FromFile(currentDirectory & lbPicName.Text)
                    Catch ex As FileNotFoundException
                        flag = False
                    End Try
                Else
                    Try
                        image = Image.FromFile(lbPicName.Text)
                    Catch ex As FileNotFoundException
                        flag = False
                    End Try
                End If
                If Not flag Then Return
                Dim width = image.Width
                Dim height = image.Height
                If iFRatio <= width / height Then
                    pbPicture.Width = iFW
                    pbPicture.Height = CInt(height * CDbl(iFW) / width)
                    pbPicture.Location = New Point(iFX, pbPicture.Location.Y)
                Else
                    pbPicture.Width = CInt(width * CDbl(iFH) / height)
                    pbPicture.Height = iFH
                    pbPicture.Location = New Point(iFX + CInt((iFW - pbPicture.Width) / 2.0), pbPicture.Location.Y)
                End If
                pbPicture.Image = image
            Else
                pbPicture.Image = Nothing
                pbPicture.Location = New Point(iFX, pbPicture.Location.Y)
                pbPicture.Width = iFW
                pbPicture.Height = iFH
            End If
        End Sub

        Private Sub btOK_Click(sender As Object, e As EventArgs)
            If iMode = 0 Then
                InsertData()
            Else
                EditData()
            End If
        End Sub

        Private Sub MakeTrueValue()
            tbSalaryLeave()
        End Sub

        Private Sub InsertData()
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim sqlTransaction1 As SqlTransaction = CType(Nothing, SqlTransaction)
            Dim now = Date.Now
            Dim text = ""
            Dim flag = True
            Cursor = Cursors.WaitCursor
            MakeTrueValue()
            lbOfficerNumber.Text = GetThisOfficerNumber().ToString()
            If flag AndAlso Equals(tbName.Text, "") Then
                flag = False
                text = "กรุณาใส่ชื่อพนักงาน ก่อนกดปุ่ม OK !"
                tbName.Focus()
                tbName.SelectAll()
            End If
            If flag AndAlso BranchDataSet.Tables("Branch").Select("Login = true").Length = 0 Then
                Dim fWarningMessage As fWarningMessage = New fWarningMessage()
                fWarningMessage.lbHead.Text = "WARNING (คำเตือน)"
                fWarningMessage.lbMessage.Text = "คุณยังไม่ได้ทำการระบุสาขาที่สามารถ Login เข้าใช้งานได้" & vbLf & "คุณต้องการที่จะทำรายการต่อใช่หรือไม่ ?"
                fWarningMessage.btOK.Text = "Yes"
                fWarningMessage.btCancel.Text = "No"
                fWarningMessage.m_bDefaultButton = 2
                If fWarningMessage.ShowDialog() <> DialogResult.OK Then
                    flag = False
                    text = ""
                End If
            End If
            If flag Then
                Try
                    sqlConnection.Open()
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            If flag AndAlso Not Equals(tbOfficerCode.Text, "") Then
                command.CommandText = $"SELECT Name FROM Officer WHERE OfficerCode = N'{tbOfficerCode.Text}'"
                Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
                If sqlDataReader.HasRows Then
                    Dim str = ""
                    If sqlDataReader.Read() Then str = sqlDataReader.GetString(0).TrimEnd()
                    flag = False
                    text = $"Officer Code (รหัสพนักงาน) ซ้ำกับพนักงาน ""{str}""
กรุณาป้อนใหม่อีกครั้ง!"
                    tbOfficerCode.Focus()
                    tbOfficerCode.SelectAll()
                End If
                sqlDataReader.Close()
            End If
            If flag AndAlso Not Equals(tbBarcode.Text, "") Then
                command.CommandText = $"SELECT Name FROM Officer WHERE Barcode = N'{tbBarcode.Text}'"
                Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
                If sqlDataReader.HasRows Then
                    Dim str = ""
                    If sqlDataReader.Read() Then str = sqlDataReader.GetString(0).TrimEnd()
                    flag = False
                    text = $"Barcode พนักงาน ซ้ำกับพนักงาน ""{str}""
กรุณาป้อนใหม่อีกครั้ง!"
                    tbBarcode.Focus()
                    tbBarcode.SelectAll()
                End If
                sqlDataReader.Close()
            End If
            If flag Then
                sqlTransaction1 = sqlConnection.BeginTransaction()
                command.Transaction = sqlTransaction1
                Try
                    command.CommandText = $"INSERT INTO Officer(OfficerNumber,OfficerCode,Name,Sex,Barcode,Address,Tel,Mobile,PeopleID,Pos,PicName,Salary,UN,PW,Class,WebAppBrandID,Status,CONumber,UONumber,CTime,UTime) Values({lbOfficerNumber.Text},"
                    If Not Equals(tbOfficerCode.Text, "") Then
                        Dim sqlCommand As SqlCommand = command
                        sqlCommand.CommandText = $"{sqlCommand.CommandText}N'{tbOfficerCode.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    Dim sqlCommand1 As SqlCommand = command
                    sqlCommand1.CommandText = $"{sqlCommand1.CommandText}N'{tbName.Text}',"
                    Dim sqlCommand2 As SqlCommand = command
                    sqlCommand2.CommandText = $"{sqlCommand2.CommandText}{cbSex.SelectedIndex.ToString()},"
                    If Not Equals(tbBarcode.Text, "") Then
                        Dim sqlCommand3 As SqlCommand = command
                        sqlCommand3.CommandText = $"{sqlCommand3.CommandText}N'{tbBarcode.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    If Not Equals(tbAddress.Text, "") Then
                        Dim sqlCommand4 As SqlCommand = command
                        sqlCommand4.CommandText = $"{sqlCommand4.CommandText}N'{tbAddress.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    If Not Equals(tbTel.Text, "") Then
                        Dim sqlCommand5 As SqlCommand = command
                        sqlCommand5.CommandText = $"{sqlCommand5.CommandText}N'{tbTel.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    If Not Equals(tbMobile.Text, "") Then
                        Dim sqlCommand6 As SqlCommand = command
                        sqlCommand6.CommandText = $"{sqlCommand6.CommandText}N'{tbMobile.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    If Not Equals(tbPeopleID.Text, "") Then
                        Dim sqlCommand7 As SqlCommand = command
                        sqlCommand7.CommandText = $"{sqlCommand7.CommandText}N'{tbPeopleID.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    If Not Equals(tbPos.Text, "") Then
                        Dim sqlCommand8 As SqlCommand = command
                        sqlCommand8.CommandText = $"{sqlCommand8.CommandText}N'{tbPos.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    If Not Equals(lbPicName.Text, "") Then
                        Dim sqlCommand9 As SqlCommand = command
                        sqlCommand9.CommandText = $"{sqlCommand9.CommandText}N'{lbPicName.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    Dim sqlCommand10 As SqlCommand = command
                    sqlCommand10.CommandText = $"{sqlCommand10.CommandText}{tbSalary.Text},"
                    command.CommandText += "'',0,0,"
                    If cbWebAppLimitBrand.SelectedIndex = 0 Then
                        command.CommandText += "NULL,"
                    Else
                        Dim sqlCommand11 As SqlCommand = command
                        sqlCommand11.CommandText = $"{sqlCommand11.CommandText}{cbWebAppLimitBrand.SelectedValue.ToString()},"
                    End If
                    Dim sqlCommand12 As SqlCommand = command
                    sqlCommand12.CommandText = $"{sqlCommand12.CommandText}NULL,{lLoginONumber.ToString()},{lLoginONumber.ToString()},GetDate(),GetDate())"
                    command.ExecuteNonQuery()
                    For Each dataRow In BranchDataSet.Tables("Branch").Select("Login = true", "BranchNumber ASC")
                        Dim int32 As Long = Convert.ToInt32(dataRow("BranchNumber"))
                        command.CommandText = $"INSERT INTO OfficerBranch(OfficerNumber,BranchNumber) Values({lbOfficerNumber.Text},{int32.ToString()})"
                        command.ExecuteNonQuery()
                    Next
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            Dim sqlTransaction2 As SqlTransaction
            If flag Then
                sqlTransaction1.Commit()
                sqlTransaction2 = CType(Nothing, SqlTransaction)
                Dim row As DataRow = fParent.OfficerDataSet.Tables("Officer").NewRow()
                row("OfficerNumber") = Convert.ToInt32(Me.lbOfficerNumber.Text)
                row("OfficerCode") = tbOfficerCode.Text
                row("Barcode") = tbBarcode.Text
                row("Sex") = cbSex.SelectedIndex
                row("Name") = tbName.Text
                row("Pos") = tbPos.Text
                row("PeopleID") = tbPeopleID.Text
                row("Salary") = Convert.ToDecimal(tbSalary.Text)
                row("Tel") = tbTel.Text
                row("Mobile") = tbMobile.Text
                row("Address") = tbAddress.Text
                row("PicName") = lbPicName.Text
                If cbWebAppLimitBrand.SelectedIndex = 0 Then
                    row("WebAppBrandID") = DBNull.Value
                    row("WebAppBrandName") = DBNull.Value
                Else
                    row("WebAppBrandID") = Convert.ToInt32(cbWebAppLimitBrand.SelectedValue)
                    row("WebAppBrandName") = cbWebAppLimitBrand.Text
                End If
                row("COName") = CObj(lLoginONumber.ToString())
                row("CTime") = Date.Now
                row("UOName") = CObj(lLoginONumber.ToString())
                row("UTime") = Date.Now
                fParent.OfficerDataSet.Tables("Officer").Rows.Add(row)
                fParent.dtGOfficer.AutoSizeColumns()
                Dim lbNumberItems = fParent.lbNumberItems
                Dim num As Integer = fParent.dtGOfficer.RowCount
                Dim str1 As String = num.ToString() & " รายการ"
                lbNumberItems.Text = str1
                fParent.dtGOfficer.AutoSizeColumns()
                fParent.dtGOfficer.Row = fParent.dtGOfficer.RowCount - 1
                tbOfficerCode.Text = ""
                tbName.Text = ""
                tbAddress.Text = ""
                lbPicName.Text = ""
                tbTel.Text = ""
                tbMobile.Text = ""
                tbPeopleID.Text = ""
                tbPos.Text = ""
                tbSalary.Text = 0D.ToString([myClass].GetDecimalPlacesFormatString())
                Dim lbOfficerNumber = Me.lbOfficerNumber
                num = GetThisOfficerNumber()
                Dim str2 As String = num.ToString()
                lbOfficerNumber.Text = str2
                If cbAutoGenCode.Checked Then
                    Dim tbBarcode = Me.tbBarcode
                    Dim strArray = New String(5) {}
                    num = now.Year
                    strArray(0) = num.ToString()
                    num = now.Month
                    strArray(1) = num.ToString()
                    num = now.Day
                    strArray(2) = num.ToString()
                    num = now.Hour
                    strArray(3) = num.ToString()
                    num = now.Minute
                    strArray(4) = num.ToString()
                    num = now.Second
                    strArray(5) = num.ToString()
                    Dim str3 = String.Concat(strArray)
                    tbBarcode.Text = str3
                Else
                    tbBarcode.Text = ""
                End If
                tbName.Focus()
            ElseIf sqlTransaction1 IsNot Nothing Then
                sqlTransaction1.Dispose()
                sqlTransaction2 = CType(Nothing, SqlTransaction)
            End If
            sqlConnection.Close()
            Cursor = Cursors.Default
            If flag OrElse Not (Not Equals(text, "")) Then Return
            Dim num1 As Integer = MessageBox.Show(text, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Sub

        Private Sub EditData()
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim sqlTransaction1 As SqlTransaction = CType(Nothing, SqlTransaction)
            Dim now = Date.Now
            Dim text = ""
            Dim flag = True
            Cursor = Cursors.WaitCursor
            MakeTrueValue()
            If flag AndAlso Equals(tbName.Text, "") Then
                flag = False
                text = "กรุณาป้อนชื่อพนักงาน ก่อนกดปุ่ม OK"
                tbName.Focus()
            End If
            If flag Then
                Try
                    sqlConnection.Open()
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            If flag AndAlso Not Equals(tbOfficerCode.Text, "") Then
                command.CommandText = $"SELECT Name FROM Officer WHERE OfficerCode = N'{tbOfficerCode.Text}' AND OfficerNumber != {lbOfficerNumber.Text}"
                Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
                If sqlDataReader.HasRows Then
                    Dim str = ""
                    If sqlDataReader.Read() Then str = sqlDataReader.GetString(0).TrimEnd()
                    flag = False
                    text = $"Officer Code (รหัสพนักงาน) ซ้ำกับพนักงาน ""{str}""
กรุณาป้อนใหม่อีกครั้ง!"
                    tbOfficerCode.Focus()
                    tbOfficerCode.SelectAll()
                End If
                sqlDataReader.Close()
            End If
            If flag AndAlso Not Equals(tbBarcode.Text, "") Then
                command.CommandText = $"SELECT Name FROM Officer WHERE Barcode = N'{tbBarcode.Text}' AND OfficerNumber != {lbOfficerNumber.Text}"
                Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
                If sqlDataReader.HasRows Then
                    Dim str = ""
                    If sqlDataReader.Read() Then str = sqlDataReader.GetString(0).TrimEnd()
                    flag = False
                    text = $"Barcode พนักงาน ซ้ำกับพนักงาน ""{str}""
กรุณาป้อนใหม่อีกครั้ง!"
                    tbBarcode.Focus()
                    tbBarcode.SelectAll()
                End If
                sqlDataReader.Close()
            End If
            If flag Then
                sqlTransaction1 = sqlConnection.BeginTransaction()
                command.Transaction = sqlTransaction1
                Try
                    command.CommandText = "UPDATE Officer SET "
                    command.CommandText += "OfficerCode="
                    If Not Equals(tbOfficerCode.Text, "") Then
                        Dim sqlCommand As SqlCommand = command
                        sqlCommand.CommandText = $"{sqlCommand.CommandText}N'{tbOfficerCode.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    Dim sqlCommand1 As SqlCommand = command
                    sqlCommand1.CommandText = $"{sqlCommand1.CommandText}Name=N'{tbName.Text}',"
                    Dim sqlCommand2 As SqlCommand = command
                    sqlCommand2.CommandText = $"{sqlCommand2.CommandText}Sex={cbSex.SelectedIndex},"
                    command.CommandText += "Barcode="
                    If Not Equals(tbBarcode.Text, "") Then
                        Dim sqlCommand3 As SqlCommand = command
                        sqlCommand3.CommandText = $"{sqlCommand3.CommandText}N'{tbBarcode.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    command.CommandText += "Address="
                    If Not Equals(tbAddress.Text, "") Then
                        Dim sqlCommand4 As SqlCommand = command
                        sqlCommand4.CommandText = $"{sqlCommand4.CommandText}N'{tbAddress.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    command.CommandText += "PicName="
                    If Not Equals(lbPicName.Text, "") Then
                        Dim sqlCommand5 As SqlCommand = command
                        sqlCommand5.CommandText = $"{sqlCommand5.CommandText}N'{lbPicName.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    command.CommandText += "Tel="
                    If Not Equals(tbTel.Text, "") Then
                        Dim sqlCommand6 As SqlCommand = command
                        sqlCommand6.CommandText = $"{sqlCommand6.CommandText}N'{tbTel.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    command.CommandText += "Mobile="
                    If Not Equals(tbMobile.Text, "") Then
                        Dim sqlCommand7 As SqlCommand = command
                        sqlCommand7.CommandText = $"{sqlCommand7.CommandText}N'{tbMobile.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    command.CommandText += "PeopleID="
                    If Not Equals(tbPeopleID.Text, "") Then
                        Dim sqlCommand8 As SqlCommand = command
                        sqlCommand8.CommandText = $"{sqlCommand8.CommandText}N'{tbPeopleID.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    command.CommandText += "Pos="
                    If Not Equals(tbPos.Text, "") Then
                        Dim sqlCommand9 As SqlCommand = command
                        sqlCommand9.CommandText = $"{sqlCommand9.CommandText}N'{tbPos.Text}',"
                    Else
                        command.CommandText += "NULL,"
                    End If
                    Dim sqlCommand10 As SqlCommand = command
                    sqlCommand10.CommandText = $"{sqlCommand10.CommandText}Salary={tbSalary.Text},"
                    If cbWebAppLimitBrand.SelectedIndex > 0 Then
                        Dim sqlCommand11 As SqlCommand = command
                        sqlCommand11.CommandText = $"{sqlCommand11.CommandText}WebAppBrandID={Convert.ToInt32(cbWebAppLimitBrand.SelectedValue).ToString()},"
                    Else
                        command.CommandText += "WebAppBrandID=NULL,"
                    End If
                    If cbStatus.Checked Then
                        command.CommandText += "Status=1,"
                    Else
                        command.CommandText += "Status=NULL,"
                    End If
                    Dim sqlCommand12 As SqlCommand = command
                    sqlCommand12.CommandText = $"{sqlCommand12.CommandText}UONumber={lLoginONumber.ToString()},"
                    Dim sqlCommand13 As SqlCommand = command
                    sqlCommand13.CommandText = $"{sqlCommand13.CommandText}UTime=GetDate() WHERE OfficerNumber={lbOfficerNumber.Text}"
                    command.ExecuteNonQuery()
                    If Convert.ToInt32(lbOfficerNumber.Text) > 1 Then
                        command.CommandText = "DELETE FROM OfficerBranch WHERE OfficerBranch.OfficerNumber = " & lbOfficerNumber.Text
                        command.ExecuteNonQuery()
                        For Each dataRow In BranchDataSet.Tables("Branch").Select("Login = true", "BranchNumber ASC")
                            Dim int32 As Long = Convert.ToInt32(dataRow("BranchNumber"))
                            command.CommandText = $"INSERT INTO OfficerBranch(OfficerNumber,BranchNumber) Values({lbOfficerNumber.Text},{int32.ToString()})"
                            command.ExecuteNonQuery()
                        Next
                    End If
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            Dim sqlTransaction2 As SqlTransaction
            If flag Then
                sqlTransaction1.Commit()
                sqlTransaction2 = CType(Nothing, SqlTransaction)
                Dim row = fParent.OfficerDataSet.Tables(CStr("Officer")).Rows(fParent.dtGOfficer.GetRow(fParent.dtGOfficer.Row).RowIndex)
                row("OfficerCode") = tbOfficerCode.Text
                row("Barcode") = tbBarcode.Text
                row("Sex") = cbSex.SelectedIndex
                row("Name") = tbName.Text
                row("Pos") = tbPos.Text
                row("Salary") = Convert.ToDecimal(tbSalary.Text)
                row("Tel") = tbTel.Text
                row("Mobile") = tbMobile.Text
                row("PeopleID") = tbPeopleID.Text
                row("Address") = tbAddress.Text
                row("PicName") = lbPicName.Text
                If cbWebAppLimitBrand.SelectedIndex > 0 Then
                    row("WebAppBrandID") = Convert.ToInt32(cbWebAppLimitBrand.SelectedValue)
                    row("WebAppBrandName") = cbWebAppLimitBrand.Text
                Else
                    row("WebAppBrandID") = DBNull.Value
                    row("WebAppBrandName") = DBNull.Value
                End If
                row("Status") = If(Not cbStatus.Checked, DBNull.Value, CObj(1))
                row("UOName") = CObj(lLoginONumber.ToString())
                row("UTime") = Date.Now
                fParent.dtGOfficer.AutoSizeColumns()
            ElseIf sqlTransaction1 IsNot Nothing Then
                sqlTransaction1.Dispose()
                sqlTransaction2 = CType(Nothing, SqlTransaction)
            End If
            sqlConnection.Close()
            Cursor = Cursors.Default
            If flag Then
                Dim num As Integer = MessageBox.Show("แก้ไขรายการเรียบร้อยแล้ว", "Edit success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                DialogResult = DialogResult.OK
                Close()
            Else
                Dim num1 As Integer = MessageBox.Show(text, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
        End Sub

        Private Sub cbAutoGenCode_CheckedChanged(sender As Object, e As EventArgs)
            Dim now = Date.Now
            If Not cbAutoGenCode.Checked Then Return
            Dim tbBarcode = Me.tbBarcode
            Dim strArray = New String(5) {}
            Dim num = now.Year
            strArray(0) = num.ToString()
            num = now.Month
            strArray(1) = num.ToString()
            num = now.Day
            strArray(2) = num.ToString()
            num = now.Hour
            strArray(3) = num.ToString()
            num = now.Minute
            strArray(4) = num.ToString()
            num = now.Second
            strArray(5) = num.ToString()
            Dim str = String.Concat(strArray)
            tbBarcode.Text = str
        End Sub

        Private Sub cbStatus_KeyPress(sender As Object, e As KeyPressEventArgs)
            If e.KeyChar <> ChrW(13) Then Return
            tbName.Focus()
            tbName.SelectAll()
        End Sub

        Private Sub tbBarcode_Enter(sender As Object, e As EventArgs)
            tbBarcode.SelectAll()
        End Sub

        Private Sub tbName_Enter(sender As Object, e As EventArgs)
            tbName.SelectAll()
        End Sub

        Private Sub tbAddress_Enter(sender As Object, e As EventArgs)
            tbAddress.SelectAll()
        End Sub

        Private Sub tbTel_Enter(sender As Object, e As EventArgs)
            tbTel.SelectAll()
        End Sub

        Private Sub tbMobile_Enter(sender As Object, e As EventArgs)
            tbMobile.SelectAll()
        End Sub

        Private Sub tbPeopleID_Enter(sender As Object, e As EventArgs)
            tbPeopleID.SelectAll()
        End Sub

        Private Sub tbPos_Enter(sender As Object, e As EventArgs)
            tbPos.SelectAll()
        End Sub

        Private Sub tbOfficerCode_Enter(sender As Object, e As EventArgs)
            tbOfficerCode.SelectAll()
        End Sub

        Private Sub tbOfficerCode_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            tbBarcode.Focus()
            tbBarcode.SelectAll()
        End Sub

        Private Sub ChangeGridFormatBranch()
            For Each column As DataColumn In CType(BranchDataSet.Tables("Branch").Columns, InternalDataCollectionBase)
                Dim dataType = column.DataType
                If dataType Is Type.GetType("System.Int16") OrElse dataType Is Type.GetType("System.Int32") OrElse dataType Is Type.GetType("System.Int64") Then
                    dtGBranch.RootTable.Columns(column.ColumnName).FormatString = "N0"
                    dtGBranch.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Far
                ElseIf dataType Is Type.GetType("System.Decimal") Then
                    dtGBranch.RootTable.Columns(column.ColumnName).FormatString = [myClass].GetDecimalPlacesFormatStringWithComma()
                    dtGBranch.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Far
                ElseIf dataType Is Type.GetType("System.DateTime") Then
                    dtGBranch.RootTable.Columns(column.ColumnName).FormatString = "dd MMMM yyyy"
                    dtGBranch.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Center
                End If
            Next
        End Sub

        Private Sub HideGridColumnBranch()
        End Sub

        Private Sub ChangeColumnCaptionBranch()
            dtGBranch.RootTable.Columns("BranchNumber").Caption = "ID"
            dtGBranch.RootTable.Columns("BranchName").Caption = "Branch Name (ชื่อสาขา)"
        End Sub

        Private Sub SetEditColumn()
            Dim num As Long = 0
            If iMode = 1 Then num = Convert.ToInt32(lbOfficerNumber.Text)
            Dim index = 0

            While index < dtGBranch.RootTable.Columns.Count
                If dtGBranch.RootTable.Columns(index).Caption IsNot "Login" Then
                    dtGBranch.RootTable.Columns(index).EditType = EditType.NoEdit
                ElseIf iMode = 1 AndAlso num = 1L Then
                    dtGBranch.RootTable.Columns(index).EditType = EditType.NoEdit
                End If

                Threading.Interlocked.Increment(index)
            End While
        End Sub

        Private Sub btClear_Click(sender As Object, e As EventArgs)
            Dim index = 0

            While index < BranchDataSet.Tables("Branch").Rows.Count
                BranchDataSet.Tables("Branch").Rows(index)("Login") = False
                Threading.Interlocked.Increment(index)
            End While
        End Sub

        Private Sub btCheckAll_Click(sender As Object, e As EventArgs)
            btCheckAllClick()
        End Sub

        Private Sub btCheckAllClick()
            Dim index = 0

            While index < BranchDataSet.Tables("Branch").Rows.Count
                BranchDataSet.Tables("Branch").Rows(index)("Login") = True
                Threading.Interlocked.Increment(index)
            End While
        End Sub

        Private Sub GetOfficerBranch()
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Cursor = Cursors.WaitCursor
            sqlConnection.Open()
            If Convert.ToInt32(lbOfficerNumber.Text) = 1 Then
                Dim index = 0

                While index < BranchDataSet.Tables("Branch").Rows.Count
                    BranchDataSet.Tables("Branch").Rows(index)("Login") = True
                    Threading.Interlocked.Increment(index)
                End While
            Else
                command.CommandText = "SELECT OfficerBranch.BranchNumber FROM OfficerBranch WHERE OfficerBranch.OfficerNumber = " & lbOfficerNumber.Text
                Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
                While sqlDataReader.Read()
                    Dim ordinal As Integer = sqlDataReader.GetOrdinal("BranchNumber")
                    Dim dataRowArray As DataRow() = BranchDataSet.Tables(CStr("Branch")).[Select]("BranchNumber = " & CLng(sqlDataReader.GetInt32(CInt(ordinal))).ToString())
                    If dataRowArray.Length <> 0 Then dataRowArray(0)("Login") = True
                End While
                sqlDataReader.Close()
            End If
            sqlConnection.Close()
            Cursor = Cursors.Default
        End Sub

        Private Sub fOfficerAddEdit_Shown(sender As Object, e As EventArgs)
            tbName.Focus()
        End Sub
    End Class
End Namespace
