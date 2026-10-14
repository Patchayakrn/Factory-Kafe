<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fCustomer
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fCustomer))
        Me.dtGCustomer = New Krypton.Toolkit.KryptonDataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkCustomerCertified = New Krypton.Toolkit.KryptonCheckBox()
        Me.tbFreeCupOpening = New Krypton.Toolkit.KryptonTextBox()
        Me.lblFreeCupTarget = New System.Windows.Forms.Label()
        Me.lblFreeCupAvailable = New System.Windows.Forms.Label()
        Me.lblFreeCupAvailableValue = New System.Windows.Forms.Label()
        Me.btnFreeCupHistory = New Krypton.Toolkit.KryptonButton()
        Me.btSave = New Krypton.Toolkit.KryptonButton()
        Me.btcancel = New Krypton.Toolkit.KryptonButton()
        Me.btSearch = New Krypton.Toolkit.KryptonButton()
        Me.cbStatus = New Krypton.Toolkit.KryptonComboBox()
        Me.cbLimitSearch = New Krypton.Toolkit.KryptonComboBox()
        Me.tbMobile = New Krypton.Toolkit.KryptonTextBox()
        Me.tbName = New Krypton.Toolkit.KryptonTextBox()
        Me.tbCustomerCode = New Krypton.Toolkit.KryptonTextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tbAddress = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblFreeCupOpening = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.dtGCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbLimitSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtGCustomer
        '
        Me.dtGCustomer.AllowUserToAddRows = False
        Me.dtGCustomer.AllowUserToDeleteRows = False
        Me.dtGCustomer.AllowUserToResizeRows = False
        Me.dtGCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dtGCustomer.ColumnHeadersHeight = 48
        Me.dtGCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dtGCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtGCustomer.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Sheet
        Me.dtGCustomer.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dtGCustomer.GridStyles.StyleColumn = Krypton.Toolkit.GridStyle.Sheet
        Me.dtGCustomer.GridStyles.StyleDataCells = Krypton.Toolkit.GridStyle.Sheet
        Me.dtGCustomer.GridStyles.StyleRow = Krypton.Toolkit.GridStyle.Sheet
        Me.dtGCustomer.Location = New System.Drawing.Point(3, 41)
        Me.dtGCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.dtGCustomer.MultiSelect = False
        Me.dtGCustomer.Name = "dtGCustomer"
        Me.dtGCustomer.ReadOnly = True
        Me.dtGCustomer.RowHeadersVisible = False
        Me.dtGCustomer.RowHeadersWidth = 51
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtGCustomer.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dtGCustomer.RowTemplate.Height = 37
        Me.dtGCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtGCustomer.Size = New System.Drawing.Size(900, 756)
        Me.dtGCustomer.StateCommon.Background.Color1 = System.Drawing.Color.White
        Me.dtGCustomer.StateCommon.Background.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid
        Me.dtGCustomer.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dtGCustomer.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.White
        Me.dtGCustomer.StateCommon.HeaderColumn.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtGCustomer.StateCommon.HeaderRow.Back.Color1 = System.Drawing.Color.White
        Me.dtGCustomer.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.chkCustomerCertified)
        Me.GroupBox1.Controls.Add(Me.tbFreeCupOpening)
        Me.GroupBox1.Controls.Add(Me.lblFreeCupTarget)
        Me.GroupBox1.Controls.Add(Me.lblFreeCupAvailable)
        Me.GroupBox1.Controls.Add(Me.lblFreeCupAvailableValue)
        Me.GroupBox1.Controls.Add(Me.btnFreeCupHistory)
        Me.GroupBox1.Controls.Add(Me.btSave)
        Me.GroupBox1.Controls.Add(Me.btcancel)
        Me.GroupBox1.Controls.Add(Me.btSearch)
        Me.GroupBox1.Controls.Add(Me.cbStatus)
        Me.GroupBox1.Controls.Add(Me.cbLimitSearch)
        Me.GroupBox1.Controls.Add(Me.tbMobile)
        Me.GroupBox1.Controls.Add(Me.tbName)
        Me.GroupBox1.Controls.Add(Me.tbCustomerCode)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.tbAddress)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblFreeCupOpening)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(562, 800)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'chkCustomerCertified
        '
        Me.chkCustomerCertified.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCustomerCertified.Location = New System.Drawing.Point(245, 620)
        Me.chkCustomerCertified.Name = "chkCustomerCertified"
        Me.chkCustomerCertified.Size = New System.Drawing.Size(123, 43)
        Me.chkCustomerCertified.StateNormal.ShortText.Color1 = System.Drawing.Color.Fuchsia
        Me.chkCustomerCertified.StateNormal.ShortText.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomerCertified.TabIndex = 131
        Me.chkCustomerCertified.Values.Text = "รับรองลูกค้า"
        '
        'tbFreeCupOpening
        '
        Me.tbFreeCupOpening.Location = New System.Drawing.Point(247, 591)
        Me.tbFreeCupOpening.Name = "tbFreeCupOpening"
        Me.tbFreeCupOpening.Size = New System.Drawing.Size(130, 44)
        Me.tbFreeCupOpening.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFreeCupOpening.TabIndex = 132
        Me.tbFreeCupOpening.Text = "0"
        Me.tbFreeCupOpening.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblFreeCupTarget
        '
        Me.lblFreeCupTarget.AutoSize = True
        Me.lblFreeCupTarget.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFreeCupTarget.ForeColor = System.Drawing.Color.DimGray
        Me.lblFreeCupTarget.Location = New System.Drawing.Point(383, 591)
        Me.lblFreeCupTarget.Name = "lblFreeCupTarget"
        Me.lblFreeCupTarget.Size = New System.Drawing.Size(82, 37)
        Me.lblFreeCupTarget.TabIndex = 134
        Me.lblFreeCupTarget.Text = "/ 10 แก้ว"
        '
        'lblFreeCupAvailable
        '
        Me.lblFreeCupAvailable.AutoSize = True
        Me.lblFreeCupAvailable.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.lblFreeCupAvailable.Location = New System.Drawing.Point(78, 646)
        Me.lblFreeCupAvailable.Name = "lblFreeCupAvailable"
        Me.lblFreeCupAvailable.Size = New System.Drawing.Size(126, 32)
        Me.lblFreeCupAvailable.TabIndex = 135
        Me.lblFreeCupAvailable.Text = "สิทธิ์แก้วฟรีที่ใช้ได้"
        '
        'lblFreeCupAvailableValue
        '
        Me.lblFreeCupAvailableValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFreeCupAvailableValue.Font = New System.Drawing.Font("Cordia New", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFreeCupAvailableValue.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblFreeCupAvailableValue.Location = New System.Drawing.Point(247, 642)
        Me.lblFreeCupAvailableValue.Name = "lblFreeCupAvailableValue"
        Me.lblFreeCupAvailableValue.Size = New System.Drawing.Size(130, 45)
        Me.lblFreeCupAvailableValue.TabIndex = 136
        Me.lblFreeCupAvailableValue.Text = "0 ใบ"
        Me.lblFreeCupAvailableValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnFreeCupHistory
        '
        Me.btnFreeCupHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFreeCupHistory.Location = New System.Drawing.Point(393, 735)
        Me.btnFreeCupHistory.Name = "btnFreeCupHistory"
        Me.btnFreeCupHistory.Size = New System.Drawing.Size(159, 53)
        Me.btnFreeCupHistory.StateCommon.Back.Color1 = System.Drawing.Color.LightSkyBlue
        Me.btnFreeCupHistory.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFreeCupHistory.TabIndex = 137
        Me.btnFreeCupHistory.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btnFreeCupHistory.Values.Text = "ประวัติแต้ม / สิทธิ"
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(261, 735)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(125, 53)
        Me.btSave.StateCommon.Back.Color1 = System.Drawing.Color.PaleGreen
        Me.btSave.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btSave.TabIndex = 128
        Me.btSave.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btSave.Values.Image = CType(resources.GetObject("btSave.Values.Image"), System.Drawing.Image)
        Me.btSave.Values.Text = "บันทึก"
        Me.btSave.Values.UACShieldIconSize = Krypton.Toolkit.IconSize.MediumSmall
        '
        'btcancel
        '
        Me.btcancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btcancel.Location = New System.Drawing.Point(132, 735)
        Me.btcancel.Name = "btcancel"
        Me.btcancel.Size = New System.Drawing.Size(125, 53)
        Me.btcancel.StateCommon.Back.Color1 = System.Drawing.Color.Salmon
        Me.btcancel.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btcancel.TabIndex = 127
        Me.btcancel.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btcancel.Values.Image = CType(resources.GetObject("btcancel.Values.Image"), System.Drawing.Image)
        Me.btcancel.Values.Text = "ยกเลิก"
        Me.btcancel.Values.UACShieldIconSize = Krypton.Toolkit.IconSize.MediumSmall
        '
        'btSearch
        '
        Me.btSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSearch.Location = New System.Drawing.Point(3, 735)
        Me.btSearch.Name = "btSearch"
        Me.btSearch.Size = New System.Drawing.Size(125, 53)
        Me.btSearch.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btSearch.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btSearch.TabIndex = 126
        Me.btSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btSearch.Values.Image = CType(resources.GetObject("btSearch.Values.Image"), System.Drawing.Image)
        Me.btSearch.Values.Text = "ค้นหา"
        Me.btSearch.Values.UACShieldIconSize = Krypton.Toolkit.IconSize.MediumSmall
        '
        'cbStatus
        '
        Me.cbStatus.DropDownHeight = 300
        Me.cbStatus.IntegralHeight = False
        Me.cbStatus.Items.AddRange(New Object() {"- ไม่ระบุสถานะ -", "ดูเฉพาะที่ใช้อยู่", "ดูเฉพาะที่ไม่ได้ใช้แล้ว"})
        Me.cbStatus.Location = New System.Drawing.Point(247, 530)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(301, 38)
        Me.cbStatus.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbStatus.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.TabIndex = 125
        Me.cbStatus.Text = "- ไม่จำกัดการค้นหา -"
        '
        'cbLimitSearch
        '
        Me.cbLimitSearch.DropDownHeight = 300
        Me.cbLimitSearch.IntegralHeight = False
        Me.cbLimitSearch.Items.AddRange(New Object() {"- ไม่จำกัดการค้นหา -", "แสดงเฉพาะ 50 รายการแรก", "แสดงเฉพาะ 100 รายการแรก", "แสดงเฉพาะ 200 รายการแรก", "แสดงเฉพาะ 300 รายการแรก", "แสดงเฉพาะ 400 รายการแรก", "แสดงเฉพาะ 500 รายการแรก", "แสดงเฉพาะ 1,000 รายการแรก"})
        Me.cbLimitSearch.Location = New System.Drawing.Point(247, 478)
        Me.cbLimitSearch.Name = "cbLimitSearch"
        Me.cbLimitSearch.Size = New System.Drawing.Size(301, 38)
        Me.cbLimitSearch.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLimitSearch.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbLimitSearch.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLimitSearch.TabIndex = 124
        Me.cbLimitSearch.Text = "- ไม่จำกัดการค้นหา -"
        '
        'tbMobile
        '
        Me.tbMobile.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.tbMobile.Location = New System.Drawing.Point(247, 333)
        Me.tbMobile.Name = "tbMobile"
        Me.tbMobile.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.tbMobile.Size = New System.Drawing.Size(301, 45)
        Me.tbMobile.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMobile.TabIndex = 123
        Me.tbMobile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbName
        '
        Me.tbName.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.tbName.Location = New System.Drawing.Point(247, 272)
        Me.tbName.Name = "tbName"
        Me.tbName.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.tbName.Size = New System.Drawing.Size(301, 45)
        Me.tbName.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbName.TabIndex = 122
        Me.tbName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbCustomerCode
        '
        Me.tbCustomerCode.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.tbCustomerCode.Location = New System.Drawing.Point(247, 206)
        Me.tbCustomerCode.Name = "tbCustomerCode"
        Me.tbCustomerCode.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.tbCustomerCode.Size = New System.Drawing.Size(301, 45)
        Me.tbCustomerCode.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCustomerCode.TabIndex = 121
        Me.tbCustomerCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(199, 25)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(169, 148)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 116
        Me.PictureBox1.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Label14.Location = New System.Drawing.Point(32, 206)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(195, 32)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Customer Code (รหัสลูกค้า)"
        '
        'tbAddress
        '
        Me.tbAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbAddress.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.tbAddress.ForeColor = System.Drawing.Color.Black
        Me.tbAddress.Location = New System.Drawing.Point(247, 395)
        Me.tbAddress.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbAddress.Multiline = True
        Me.tbAddress.Name = "tbAddress"
        Me.tbAddress.Size = New System.Drawing.Size(301, 66)
        Me.tbAddress.TabIndex = 29
        Me.tbAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Label16.Location = New System.Drawing.Point(114, 530)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(113, 32)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Status (สถานะ)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Label15.Location = New System.Drawing.Point(20, 478)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(207, 32)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Limit Search (จำกัดการค้นหา)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Label4.Location = New System.Drawing.Point(36, 272)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(191, 32)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Customer Name (ชื่อลูกค้า)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Label5.Location = New System.Drawing.Point(88, 333)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(139, 32)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Mobile (เบอร์มือถือ)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Label8.Location = New System.Drawing.Point(114, 395)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 32)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Address (ที่อยู่)"
        '
        'lblFreeCupOpening
        '
        Me.lblFreeCupOpening.AutoSize = True
        Me.lblFreeCupOpening.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.lblFreeCupOpening.Location = New System.Drawing.Point(112, 594)
        Me.lblFreeCupOpening.Name = "lblFreeCupOpening"
        Me.lblFreeCupOpening.Size = New System.Drawing.Size(115, 32)
        Me.lblFreeCupOpening.TabIndex = 24
        Me.lblFreeCupOpening.Text = "แต้มสะสมรอบนี้"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.dtGCustomer)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(562, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(906, 800)
        Me.GroupBox2.TabIndex = 123
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Customer Detail (รายการลูกค้า)"
        '
        'GridView1
        '
        Me.GridView1.ColumnPanelRowHeight = 50
        Me.GridView1.DetailHeight = 431
        Me.GridView1.FocusedRowHandle = -1
        Me.GridView1.GridControl = Nothing
        Me.GridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.GridView1.Name = "GridView1"
        Me.GridView1.RowHeight = 37
        '
        'fCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1468, 800)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "fCustomer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา - เพิ่ม ลูกค้า"
        CType(Me.dtGCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbLimitSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtGCustomer As Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents tbMobile As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents tbName As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents tbCustomerCode As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents tbAddress As TextBox
    Friend WithEvents cbLimitSearch As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents cbStatus As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents btSearch As Krypton.Toolkit.KryptonButton
    Friend WithEvents btSave As Krypton.Toolkit.KryptonButton
    Friend WithEvents btcancel As Krypton.Toolkit.KryptonButton
    Friend WithEvents chkCustomerCertified As Krypton.Toolkit.KryptonCheckBox
    Friend WithEvents tbFreeCupOpening As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents lblFreeCupOpening As Label
    Friend WithEvents lblFreeCupTarget As Label
    Friend WithEvents lblFreeCupAvailable As Label
    Friend WithEvents lblFreeCupAvailableValue As Label
    Friend WithEvents btnFreeCupHistory As Krypton.Toolkit.KryptonButton
End Class
