'  
' Type: NewStock.fOfficer
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

    Public Class fOfficer
        Inherits Form
        Private label4 As Label
        Private pbPicture As PictureBox
        Private label2 As Label
        Private btCancel As Button
        Private btOK As Button
        Private btDelete As Button
        Private btAdd As Button
        Private btPrint As Button
        Private btSearch As Button
        Private tbName As TextBox
        Public lbNumberItems As Label
        Private btEdit As Button
        Private label5 As Label
        Private cbStatus As ComboBox
        Private btAccessLevel As Button
        Private bSetUserNamePassword As Button
        Public dtGOfficer As Janus.Windows.GridEX.GridEX
        Private tbCopy As TextBox
        Private ctMenuStrip As ContextMenuStrip
        Private ctCopy As ToolStripMenuItem
        Private ctSelectAllRows As ToolStripMenuItem
        Public lbNumberItems2 As Label
        Private label8 As Label
        Public dtGOfficerBranch As Janus.Windows.GridEX.GridEX
        Private btSetLimitSearch As Button
        Private components As IContainer
        Public cOfficerNumber As Integer = 0
        Public cBarcode As Integer = 1
        Public cSex As Integer = 2
        Public cName As Integer = 3
        Public cPos As Integer = 4
        Public cPeopleID As Integer = 5
        Public cSalary As Integer = 6
        Public cTel As Integer = 7
        Public cMobile As Integer = 8
        Public cAddress As Integer = 9
        Public cPicName As Integer = 10
        Public cStatus As Integer = 11
        Public iMode As Integer
        Public sOrderBy As String = ""
        Public sSortBy As String = ""
        Public iColumnSelect As Integer = 0
        Public bColumnSearch As Boolean = False
        Public sSearch As String = ""
        Public sOfficerNumber As String = ""
        Public sName As String = ""
        Public [myClass] As [MyClass]
        Public lLoginONumber As Long = 0
        Public strStatus As String = "ไม่ใช้แล้ว"
        Private iFX As Integer
        Private iFW As Integer
        Private iFH As Integer
        Private iFRatio As Double
        Private m_bOnSelectAllRowsOfficer As Boolean = False
        Private m_bCanManageAccessLevel As Boolean = False
        Private m_bCanManageUserNamePassword As Boolean = False
        Private m_bSetOfficerLimitSearch As Boolean = False
        Private m_bCanAddOfficer As Boolean = False
        Private m_bCanEditCancelOfficer As Boolean = False
        Private m_bCanCopyGridDataToClipboard As Boolean = False
        Public ColorZeroFore As Color = Color.FromArgb(230, 230, 230)
        Public OfficerDataSet As DataSet = New DataSet()
        Public OfficerBranchDataSet As DataSet = New DataSet()

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            components = CType(New Container(), IContainer)
            label4 = New Label()
            pbPicture = New PictureBox()
            label2 = New Label()
            btCancel = New Button()
            btOK = New Button()
            btDelete = New Button()
            btAdd = New Button()
            btPrint = New Button()
            btSearch = New Button()
            tbName = New TextBox()
            lbNumberItems = New Label()
            btEdit = New Button()
            label5 = New Label()
            cbStatus = New ComboBox()
            btAccessLevel = New Button()
            bSetUserNamePassword = New Button()
            dtGOfficer = New Janus.Windows.GridEX.GridEX()
            ctMenuStrip = New ContextMenuStrip(components)
            ctCopy = New ToolStripMenuItem()
            ctSelectAllRows = New ToolStripMenuItem()
            tbCopy = New TextBox()
            lbNumberItems2 = New Label()
            label8 = New Label()
            dtGOfficerBranch = New Janus.Windows.GridEX.GridEX()
            btSetLimitSearch = New Button()
            CType(pbPicture, ISupportInitialize).BeginInit()
            CType(dtGOfficer, ISupportInitialize).BeginInit()
            ctMenuStrip.SuspendLayout()
            CType(dtGOfficerBranch, ISupportInitialize).BeginInit()
            SuspendLayout()
            label4.BackColor = Color.Transparent
            label4.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label4.ForeColor = SystemColors.WindowText
            label4.Location = New Point(8, 72)
            label4.Name = "label4"
            label4.Size = New Size(208, 23)
            label4.TabIndex = 5
            label4.Text = "Officer De&tail (รายการพนักงาน)"
            label4.TextAlign = ContentAlignment.BottomLeft
            pbPicture.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            pbPicture.BorderStyle = BorderStyle.Fixed3D
            pbPicture.Location = New Point(8, 333)
            pbPicture.Name = "pbPicture"
            pbPicture.Size = New Size(232, 171)
            pbPicture.SizeMode = PictureBoxSizeMode.StretchImage
            pbPicture.TabIndex = 45
            pbPicture.TabStop = False
            label2.BackColor = Color.Transparent
            label2.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label2.ForeColor = SystemColors.WindowText
            label2.Location = New Point(8, 8)
            label2.Name = "label2"
            label2.Size = New Size(272, 23)
            label2.TabIndex = 0
            label2.Text = "Officer &Name (ชื่อพนักงาน)"
            label2.TextAlign = ContentAlignment.BottomLeft
            btCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btCancel.BackColor = Color.Salmon
            btCancel.DialogResult = DialogResult.Cancel
            btCancel.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btCancel.Image = CType(Resources.Closesmall, Image)
            btCancel.Location = New Point(241, 472)
            btCancel.Name = "btCancel"
            btCancel.Size = New Size(109, 32)
            btCancel.TabIndex = 19
            btCancel.Text = "&Close"
            btCancel.TextImageRelation = TextImageRelation.ImageBeforeText
            btCancel.UseVisualStyleBackColor = False
            AddHandler btCancel.Click, New EventHandler(AddressOf btCancel_Click)
            btOK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btOK.BackColor = Color.Yellow
            btOK.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btOK.Image = CType(Resources.OKsmall, Image)
            btOK.Location = New Point(241, 434)
            btOK.Name = "btOK"
            btOK.Size = New Size(109, 32)
            btOK.TabIndex = 18
            btOK.Text = "&OK"
            btOK.TextImageRelation = TextImageRelation.ImageBeforeText
            btOK.UseVisualStyleBackColor = False
            btOK.Visible = False
            AddHandler btOK.Click, New EventHandler(AddressOf btOK_Click)
            btDelete.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btDelete.BackColor = Color.PaleGreen
            btDelete.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btDelete.Image = CType(Resources.Deletesmall, Image)
            btDelete.Location = New Point(241, 299)
            btDelete.Name = "btDelete"
            btDelete.Size = New Size(126, 32)
            btDelete.TabIndex = 12
            btDelete.Text = "Cance&l (ยกเลิก)"
            btDelete.TextImageRelation = TextImageRelation.ImageBeforeText
            btDelete.UseVisualStyleBackColor = False
            AddHandler btDelete.Click, New EventHandler(AddressOf btDelete_Click)
            btAdd.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btAdd.BackColor = Color.PaleGreen
            btAdd.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btAdd.Image = CType(Resources.addsmall, Image)
            btAdd.Location = New Point(8, 299)
            btAdd.Name = "btAdd"
            btAdd.Size = New Size(110, 32)
            btAdd.TabIndex = 10
            btAdd.Text = "&Add (เพิ่ม)"
            btAdd.TextImageRelation = TextImageRelation.ImageBeforeText
            btAdd.UseVisualStyleBackColor = False
            AddHandler btAdd.Click, New EventHandler(AddressOf btAdd_Click)
            btPrint.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btPrint.BackColor = Color.Ivory
            btPrint.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btPrint.Image = CType(Resources.Printsmall, Image)
            btPrint.Location = New Point(570, 331)
            btPrint.Name = "btPrint"
            btPrint.Size = New Size(118, 32)
            btPrint.TabIndex = 17
            btPrint.Text = "&Print (พิมพ์)"
            btPrint.TextImageRelation = TextImageRelation.ImageBeforeText
            btPrint.UseVisualStyleBackColor = False
            btPrint.Visible = False
            AddHandler btPrint.Click, New EventHandler(AddressOf btPrint_Click)
            btSearch.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            btSearch.BackColor = Color.Ivory
            btSearch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btSearch.Image = CType(Resources.searchsmall, Image)
            btSearch.Location = New Point(834, 299)
            btSearch.Name = "btSearch"
            btSearch.Size = New Size(132, 32)
            btSearch.TabIndex = 15
            btSearch.Text = "&Search (ค้นหา)"
            btSearch.TextImageRelation = TextImageRelation.ImageBeforeText
            btSearch.UseVisualStyleBackColor = False
            AddHandler btSearch.Click, New EventHandler(AddressOf btSearch_Click)
            tbName.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbName.ForeColor = Color.Blue
            tbName.Location = New Point(8, 32)
            tbName.MaxLength = 99
            tbName.Name = "tbName"
            tbName.Size = New Size(272, 22)
            tbName.TabIndex = 1
            AddHandler tbName.TextChanged, New EventHandler(AddressOf tbName_TextChanged)
            AddHandler tbName.KeyPress, New KeyPressEventHandler(AddressOf tbName_KeyPress)
            AddHandler tbName.Enter, New EventHandler(AddressOf tbName_Enter)
            lbNumberItems.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            lbNumberItems.BackColor = Color.Transparent
            lbNumberItems.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbNumberItems.ForeColor = SystemColors.WindowText
            lbNumberItems.Location = New Point(635, 72)
            lbNumberItems.Name = "lbNumberItems"
            lbNumberItems.Size = New Size(88, 23)
            lbNumberItems.TabIndex = 4
            lbNumberItems.Text = "0 รายการ"
            lbNumberItems.TextAlign = ContentAlignment.BottomRight
            btEdit.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btEdit.BackColor = Color.PaleGreen
            btEdit.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btEdit.Image = CType(Resources.Editsmall, Image)
            btEdit.Location = New Point(118, 299)
            btEdit.Name = "btEdit"
            btEdit.Size = New Size(123, 32)
            btEdit.TabIndex = 11
            btEdit.Text = "&Edit (แก้ไข)"
            btEdit.TextImageRelation = TextImageRelation.ImageBeforeText
            btEdit.UseVisualStyleBackColor = False
            AddHandler btEdit.Click, New EventHandler(AddressOf btEdit_Click)
            label5.BackColor = Color.Transparent
            label5.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label5.ForeColor = SystemColors.WindowText
            label5.Location = New Point(286, 8)
            label5.Name = "label5"
            label5.Size = New Size(242, 23)
            label5.TabIndex = 2
            label5.Text = "Status"
            label5.TextAlign = ContentAlignment.BottomLeft
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList
            cbStatus.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbStatus.ForeColor = Color.Brown
            cbStatus.FormattingEnabled = True
            cbStatus.Location = New Point(286, 32)
            cbStatus.Name = "cbStatus"
            cbStatus.Size = New Size(272, 24)
            cbStatus.TabIndex = 3
            btAccessLevel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btAccessLevel.BackColor = Color.Plum
            btAccessLevel.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btAccessLevel.Location = New Point(367, 299)
            btAccessLevel.Name = "btAccessLevel"
            btAccessLevel.Size = New Size(203, 32)
            btAccessLevel.TabIndex = 13
            btAccessLevel.Text = "Access Le&vel (เมนูที่ใช้งานได้)"
            btAccessLevel.TextImageRelation = TextImageRelation.ImageBeforeText
            btAccessLevel.UseVisualStyleBackColor = False
            AddHandler btAccessLevel.Click, New EventHandler(AddressOf btAccessLevel_Click)
            bSetUserNamePassword.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            bSetUserNamePassword.BackColor = Color.Plum
            bSetUserNamePassword.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            bSetUserNamePassword.Location = New Point(570, 299)
            bSetUserNamePassword.Name = "bSetUserNamePassword"
            bSetUserNamePassword.Size = New Size(245, 32)
            bSetUserNamePassword.TabIndex = 14
            bSetUserNamePassword.Text = "Set &User Name Password (ตั้งรหัสผ่าน)"
            bSetUserNamePassword.TextImageRelation = TextImageRelation.ImageBeforeText
            bSetUserNamePassword.UseVisualStyleBackColor = False
            AddHandler bSetUserNamePassword.Click, New EventHandler(AddressOf bSetUserNamePassword_Click)
            dtGOfficer.AllowEdit = InheritableBoolean.[False]
            dtGOfficer.AlternatingRowFormatStyle.BackColor = Color.AliceBlue
            dtGOfficer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            dtGOfficer.AutoEdit = True
            dtGOfficer.CardColumnHeaderFormatStyle.BackColor = Color.White
            dtGOfficer.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCellsAndHeader
            dtGOfficer.ContextMenuStrip = ctMenuStrip
            dtGOfficer.EditorsControlStyle.ButtonAppearance = ButtonAppearance.Regular
            dtGOfficer.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0)
            dtGOfficer.GroupByBoxFormatStyle.BackColor = Color.Silver
            dtGOfficer.GroupByBoxFormatStyle.BackColorGradient = SystemColors.ControlDarkDark
            dtGOfficer.GroupByBoxInfoFormatStyle.BackColor = Color.White
            dtGOfficer.GroupByBoxInfoFormatStyle.ForeColor = SystemColors.ControlDarkDark
            dtGOfficer.GroupByBoxVisible = False
            dtGOfficer.GroupMode = GroupMode.Collapsed
            dtGOfficer.GroupRowFormatStyle.BackColor = Color.White
            dtGOfficer.HideSelection = HideSelection.HighlightInactive
            dtGOfficer.Location = New Point(8, 98)
            dtGOfficer.Name = "dtGOfficer"
            dtGOfficer.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
            dtGOfficer.Size = New Size(715, 200)
            dtGOfficer.TabIndex = 6
            dtGOfficer.VisualStyle = VisualStyle.Office2010
            AddHandler dtGOfficer.RowDoubleClick, New RowActionEventHandler(AddressOf Me.dtGOfficer_RowDoubleClick)
            AddHandler dtGOfficer.FormattingRow, New RowLoadEventHandler(AddressOf Me.dtGOfficer_FormattingRow)
            AddHandler dtGOfficer.SelectionChanged, New EventHandler(AddressOf Me.dtGOfficer_SelectionChanged)
            ctMenuStrip.Items.AddRange(New ToolStripItem(1) {ctCopy, ctSelectAllRows})
            ctMenuStrip.Name = "ctMenuStrip"
            ctMenuStrip.Size = New Size(172, 48)
            ctCopy.Name = "ctCopy"
            ctCopy.Size = New Size(171, 22)
            ctCopy.Text = "Copy to Clipboard"
            AddHandler ctCopy.Click, New EventHandler(AddressOf ctCopy_Click)
            ctSelectAllRows.Name = "ctSelectAllRows"
            ctSelectAllRows.Size = New Size(171, 22)
            ctSelectAllRows.Text = "Select All Rows"
            AddHandler ctSelectAllRows.Click, New EventHandler(AddressOf ctSelectAllRows_Click)
            tbCopy.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            tbCopy.Enabled = False
            tbCopy.Location = New Point(928, 8)
            tbCopy.Name = "tbCopy"
            tbCopy.Size = New Size(38, 20)
            tbCopy.TabIndex = 19
            tbCopy.Visible = False
            lbNumberItems2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            lbNumberItems2.BackColor = Color.Transparent
            lbNumberItems2.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbNumberItems2.ForeColor = SystemColors.WindowText
            lbNumberItems2.Location = New Point(874, 79)
            lbNumberItems2.Name = "lbNumberItems2"
            lbNumberItems2.Size = New Size(89, 16)
            lbNumberItems2.TabIndex = 7
            lbNumberItems2.Text = "0 รายการ"
            lbNumberItems2.TextAlign = ContentAlignment.BottomRight
            label8.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            label8.BackColor = Color.Transparent
            label8.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label8.ForeColor = SystemColors.WindowText
            label8.Location = New Point(726, 72)
            label8.Name = "label8"
            label8.Size = New Size(154, 23)
            label8.TabIndex = 8
            label8.Text = "Branch (สาขาที่ login ได้)"
            label8.TextAlign = ContentAlignment.BottomLeft
            dtGOfficerBranch.AllowEdit = InheritableBoolean.[False]
            dtGOfficerBranch.AlternatingRowFormatStyle.BackColor = Color.AliceBlue
            dtGOfficerBranch.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
            dtGOfficerBranch.AutoEdit = True
            dtGOfficerBranch.CardColumnHeaderFormatStyle.BackColor = Color.White
            dtGOfficerBranch.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCellsAndHeader
            dtGOfficerBranch.EditorsControlStyle.ButtonAppearance = ButtonAppearance.Regular
            dtGOfficerBranch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0)
            dtGOfficerBranch.GroupByBoxFormatStyle.BackColor = Color.Silver
            dtGOfficerBranch.GroupByBoxFormatStyle.BackColorGradient = SystemColors.ControlDarkDark
            dtGOfficerBranch.GroupByBoxInfoFormatStyle.BackColor = Color.White
            dtGOfficerBranch.GroupByBoxInfoFormatStyle.ForeColor = SystemColors.ControlDarkDark
            dtGOfficerBranch.GroupByBoxVisible = False
            dtGOfficerBranch.GroupMode = GroupMode.Collapsed
            dtGOfficerBranch.GroupRowFormatStyle.BackColor = Color.White
            dtGOfficerBranch.HideSelection = HideSelection.HighlightInactive
            dtGOfficerBranch.Location = New Point(726, 98)
            dtGOfficerBranch.Name = "dtGOfficerBranch"
            dtGOfficerBranch.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection
            dtGOfficerBranch.Size = New Size(240, 200)
            dtGOfficerBranch.TabIndex = 9
            dtGOfficerBranch.VisualStyle = VisualStyle.Office2010
            btSetLimitSearch.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
            btSetLimitSearch.BackColor = Color.Pink
            btSetLimitSearch.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btSetLimitSearch.Location = New Point(241, 331)
            btSetLimitSearch.Name = "btSetLimitSearch"
            btSetLimitSearch.Size = New Size(329, 32)
            btSetLimitSearch.TabIndex = 16
            btSetLimitSearch.Text = "Set Limit Searc&h (ตั้งค่าการจำกัดการค้นหา)"
            btSetLimitSearch.TextImageRelation = TextImageRelation.ImageBeforeText
            btSetLimitSearch.UseVisualStyleBackColor = False
            AddHandler btSetLimitSearch.Click, New EventHandler(AddressOf btSetLimitSearch_Click)
            AutoScaleDimensions = New SizeF(6F, 13F)
            AutoScaleMode = AutoScaleMode.Font
            BackColor = Color.LightSteelBlue
            CancelButton = btCancel
            ClientSize = New Size(978, 516)
            Controls.Add(btSetLimitSearch)
            Controls.Add(label8)
            Controls.Add(lbNumberItems2)
            Controls.Add(CType(dtGOfficerBranch, Control))
            Controls.Add(tbCopy)
            Controls.Add(CType(dtGOfficer, Control))
            Controls.Add(bSetUserNamePassword)
            Controls.Add(btAccessLevel)
            Controls.Add(label5)
            Controls.Add(cbStatus)
            Controls.Add(label4)
            Controls.Add(pbPicture)
            Controls.Add(label2)
            Controls.Add(btCancel)
            Controls.Add(btOK)
            Controls.Add(btDelete)
            Controls.Add(btAdd)
            Controls.Add(btSearch)
            Controls.Add(tbName)
            Controls.Add(btPrint)
            Controls.Add(btEdit)
            Controls.Add(lbNumberItems)
            Name = NameOf(fOfficer)
            ShowInTaskbar = False
            Text = "Officer (พนักงาน)"
            WindowState = FormWindowState.Maximized
            AddHandler Load, New EventHandler(AddressOf fOfficer_Load)
            CType(pbPicture, ISupportInitialize).EndInit()
            CType(dtGOfficer, ISupportInitialize).EndInit()
            ctMenuStrip.ResumeLayout(False)
            CType(dtGOfficerBranch, ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Private Sub fOfficer_Load(sender As Object, e As EventArgs)
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
            cbStatus.Items.Insert(0, "ดูทั้งที่ใช้อยู่และไม่ได้ใช้แล้ว")
            cbStatus.Items.Insert(1, "ดูเฉพาะที่ใช้อยู่")
            cbStatus.Items.Insert(2, "ดูเฉพาะที่ไม่ได้ใช้แล้ว")
            cbStatus.SelectedIndex = 1
            m_bCanManageAccessLevel = [myClass].GetAccessLevelAllow("ManageAccessLevel")
            m_bCanManageUserNamePassword = [myClass].GetAccessLevelAllow("ManageUserNamePassword")
            m_bSetOfficerLimitSearch = [myClass].GetAccessLevelAllow("SetOfficerLimitSearch")
            m_bCanAddOfficer = [myClass].GetAccessLevelAllow("AddOfficer")
            m_bCanEditCancelOfficer = [myClass].GetAccessLevelAllow("EditCancelOfficer")
            m_bCanCopyGridDataToClipboard = [myClass].GetAccessLevelAllow("CopyGridDataToClipboard")
            If Not m_bCanCopyGridDataToClipboard Then ctCopy.Enabled = False
            If m_bCanManageAccessLevel Then
                btAccessLevel.Enabled = True
                btAccessLevel.Visible = True
            Else
                btAccessLevel.Enabled = False
                btAccessLevel.Visible = False
            End If
            If m_bCanManageUserNamePassword Then
                bSetUserNamePassword.Enabled = True
                bSetUserNamePassword.Visible = True
            Else
                bSetUserNamePassword.Enabled = False
                bSetUserNamePassword.Visible = False
            End If
            If m_bSetOfficerLimitSearch Then
                btSetLimitSearch.Enabled = True
                btSetLimitSearch.Visible = True
            Else
                btSetLimitSearch.Enabled = False
                btSetLimitSearch.Visible = False
            End If
            If m_bCanAddOfficer Then
                btAdd.Enabled = True
                btAdd.Visible = True
            Else
                btAdd.Enabled = False
                btAdd.Visible = False
            End If
            If m_bCanEditCancelOfficer Then
                btEdit.Enabled = True
                btEdit.Visible = True
                btDelete.Enabled = True
                btDelete.Visible = True
            Else
                btEdit.Enabled = False
                btEdit.Visible = False
                btDelete.Enabled = False
                btDelete.Visible = False
            End If
            If flag Then
                command.CommandText = "SELECT TOP 1 Officer.OfficerNumber,RTRIM(Officer.OfficerCode) AS 'OfficerCode',RTRIM(Officer.Barcode) AS 'Barcode',Officer.Sex,RTRIM(Officer.Name) AS 'Name',RTRIM(Officer.Pos) AS 'Pos',RTRIM(Officer.PeopleID) AS 'PeoPleID',Officer.Salary,RTRIM(Officer.Tel) AS 'Tel',RTRIM(Officer.Mobile) AS 'Mobile',RTRIM(Officer.Address) AS 'Address',RTRIM(Officer.PicName) AS 'PicName',RTRIM(Officer.UN) AS 'UN',Officer.PW,Officer.Class,Officer.WebAppBrandID,RTRIM(Brand.Name) AS 'WebAppBrandName',Officer.Status,RTRIM(COff.Name) AS 'COName',Officer.CTime,RTRIM(UOff.Name) AS 'UOName',Officer.UTime FROM Officer LEFT OUTER JOIN Brand ON Officer.WebAppBrandID = Brand.ID LEFT OUTER JOIN Officer COff ON Officer.CONumber = COff.OfficerNumber LEFT OUTER JOIN Officer UOff ON Officer.UONumber = UOff.OfficerNumber "
                Try
                    Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter()
                    sqlDataAdapter.SelectCommand = command
                    OfficerDataSet.Clear()
                    sqlDataAdapter.Fill(OfficerDataSet, "Officer")
                    sqlDataAdapter.Dispose()
                    OfficerDataSet.Tables("Officer").Rows.Clear()
                    dtGOfficer.SetDataBinding(OfficerDataSet, "Officer")
                    dtGOfficer.RetrieveStructure()
                    ChangeGridFormatOfficer()
                    SetColumnColorOfficer()
                    HideGridColumnOfficer()
                    ChangeColumnCaptionOfficer()
                    dtGOfficer.AutoSizeColumns()
                    lbNumberItems.Text = OfficerDataSet.Tables("Officer").Rows.Count.ToString() & " รายการ"
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            If flag Then
                command.CommandText = "SELECT TOP 1 OfficerBranch.BranchNumber,RTRIM(Branch.BranchName) AS 'BranchName' FROM OfficerBranch LEFT OUTER JOIN Branch ON OfficerBranch.BranchNumber = Branch.BranchNumber "
                Try
                    Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter()
                    sqlDataAdapter.SelectCommand = command
                    OfficerBranchDataSet.Clear()
                    sqlDataAdapter.Fill(OfficerBranchDataSet, "OfficerBranch")
                    sqlDataAdapter.Dispose()
                    OfficerBranchDataSet.Tables("OfficerBranch").Rows.Clear()
                    dtGOfficerBranch.SetDataBinding(OfficerBranchDataSet, "OfficerBranch")
                    dtGOfficerBranch.RetrieveStructure()
                    ChangeGridFormatOfficerBranch()
                    ChangeColumnCaptionOfficerBranch()
                    HideGridColumnOfficerBranch()
                    dtGOfficerBranch.AutoSizeColumns()
                    lbNumberItems.Text = OfficerBranchDataSet.Tables("OfficerBranch").Rows.Count.ToString() & " รายการ"
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            If iMode <> 0 Then
                Dim width = Screen.PrimaryScreen.WorkingArea.Width
                Dim height = Screen.PrimaryScreen.WorkingArea.Height
                Me.Text = "Select Officer ( เลือกพนักงานขาย )"
                SetBounds(width * 0.4, 1, width - CInt(width * 0.4), height)
                btOK.Visible = True
                btSearch_Click(sender, e)
            End If
            sqlConnection.Close()
            Cursor = Cursors.Default
            If Not flag Then
                Dim num As Integer = MessageBox.Show(text, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End If
            If iMode <> 0 Then Return
            WindowState = FormWindowState.Maximized
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If msg.WParam.ToInt32() = 27 Then
                Close()
                Return True
            End If
            If msg.WParam.ToInt32() <> 13 OrElse Not dtGOfficer.Focused Then Return MyBase.ProcessCmdKey(msg, keyData)
            If iMode = 0 Then
                If m_bCanEditCancelOfficer Then EditOfficer()
            Else
                OKClick()
                tbName.Focus()
                tbName.SelectAll()
            End If
            Return True
        End Function

        Private Sub ChangeGridFormatOfficer()
            For Each column As DataColumn In CType(OfficerDataSet.Tables("Officer").Columns, InternalDataCollectionBase)
                Dim dataType = column.DataType
                If dataType Is Type.GetType("System.Int16") OrElse dataType Is Type.GetType("System.Int32") OrElse dataType Is Type.GetType("System.Int64") Then
                    dtGOfficer.RootTable.Columns(column.ColumnName).FormatString = "N0"
                    dtGOfficer.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Far
                ElseIf dataType Is Type.GetType("System.Decimal") Then
                    dtGOfficer.RootTable.Columns(column.ColumnName).FormatString = [myClass].GetDecimalPlacesFormatStringWithComma()
                    dtGOfficer.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Far
                ElseIf dataType Is Type.GetType("System.DateTime") Then
                    dtGOfficer.RootTable.Columns(column.ColumnName).FormatString = "dd MMMM yyyy (HH:mm:ss)"
                    dtGOfficer.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Center
                End If
            Next
        End Sub

        Private Sub SetColumnColorOfficer()
        End Sub

        Private Sub HideGridColumnOfficer()
            dtGOfficer.RootTable.Columns("UN").Visible = False
            dtGOfficer.RootTable.Columns("PW").Visible = False
            dtGOfficer.RootTable.Columns("Class").Visible = False
            dtGOfficer.RootTable.Columns("WebAppBrandID").Visible = False
        End Sub

        Private Sub ChangeColumnCaptionOfficer()
            dtGOfficer.RootTable.Columns("OfficerNumber").Caption = "ID"
            dtGOfficer.RootTable.Columns("OfficerCode").Caption = "รหัสพนักงาน"
            dtGOfficer.RootTable.Columns("Sex").Caption = "เพศ"
            dtGOfficer.RootTable.Columns("Name").Caption = "ชื่อพนักงาน"
            dtGOfficer.RootTable.Columns("Pos").Caption = "ตำแหน่ง"
            dtGOfficer.RootTable.Columns("PeopleID").Caption = "เลขบัตรประชาชน"
            dtGOfficer.RootTable.Columns("Salary").Caption = "เงินเดือน"
            dtGOfficer.RootTable.Columns("Tel").Caption = "เบอร์โทรศัพท์"
            dtGOfficer.RootTable.Columns("Mobile").Caption = "เบอร์มือถือ"
            dtGOfficer.RootTable.Columns("Address").Caption = "ที่อยู่"
            dtGOfficer.RootTable.Columns("PicName").Caption = "ชื่อรูป"
            dtGOfficer.RootTable.Columns("WebAppBrandName").Caption = "ยี่ห้อที่จำกัดให้ดูใน WebApp"
            dtGOfficer.RootTable.Columns("COName").Caption = "Create Officer"
            dtGOfficer.RootTable.Columns("CTime").Caption = "Create Time"
            dtGOfficer.RootTable.Columns("UOName").Caption = "Update Officer"
            dtGOfficer.RootTable.Columns("UTime").Caption = "Update Time"
        End Sub

        Private Sub btCancel_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub btAdd_Click(sender As Object, e As EventArgs)
            Dim num As Integer = CInt(New fOfficerAddEdit() With {
          .iMode = 0,
          .fParent = Me,
          .lLoginONumber = lLoginONumber,
          .[myClass] = [myClass]
        }.ShowDialog())
        End Sub

        Private Sub btSearch_Click(sender As Object, e As EventArgs)
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Cursor = Cursors.WaitCursor
            Dim str = ""
            If Not Equals(tbName.Text, "") Then
                If Not Equals(str, "") Then str += " AND "
                str = $"{str}Officer.Name LIKE N'%{tbName.Text}%'"
            End If
            If cbStatus.SelectedIndex = 1 Then
                If Not Equals(str, "") Then str += " AND "
                str += "Officer.Status IS NULL"
            ElseIf cbStatus.SelectedIndex = 2 Then
                If Not Equals(str, "") Then str += " AND "
                str += "Officer.Status = 1"
            End If
            command.CommandText = "SELECT Officer.OfficerNumber,RTRIM(Officer.OfficerCode) AS 'OfficerCode',RTRIM(Officer.Barcode) AS 'Barcode',Officer.Sex,RTRIM(Officer.Name) AS 'Name',RTRIM(Officer.Pos) AS 'Pos',RTRIM(Officer.PeopleID) AS 'PeoPleID',Officer.Salary,RTRIM(Officer.Tel) AS 'Tel',RTRIM(Officer.Mobile) AS 'Mobile',RTRIM(Officer.Address) AS 'Address',RTRIM(Officer.PicName) AS 'PicName',RTRIM(Officer.UN) AS 'UN',Officer.PW,Officer.Class,Officer.WebAppBrandID,RTRIM(Brand.Name) AS 'WebAppBrandName',Officer.Status,RTRIM(COff.Name) AS 'COName',Officer.CTime,RTRIM(UOff.Name) AS 'UOName',Officer.UTime FROM Officer LEFT OUTER JOIN Brand ON Officer.WebAppBrandID = Brand.ID LEFT OUTER JOIN Officer COff ON Officer.CONumber = COff.OfficerNumber LEFT OUTER JOIN Officer UOff ON Officer.UONumber = UOff.OfficerNumber "
            If Not Equals(str, "") Then
                Dim sqlCommand As SqlCommand = command
                sqlCommand.CommandText = $"{sqlCommand.CommandText} WHERE {str}"
            End If
            command.CommandText += " ORDER BY Officer.Name"
            sqlConnection.Open()
            Try
                Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter()
                sqlDataAdapter.SelectCommand = command
                OfficerDataSet.Clear()
                sqlDataAdapter.Fill(OfficerDataSet, "Officer")
                sqlDataAdapter.Dispose()
                dtGOfficer.SetDataBinding(OfficerDataSet, "Officer")
                dtGOfficer.RetrieveStructure()
                ChangeGridFormatOfficer()
                SetColumnColorOfficer()
                HideGridColumnOfficer()
                ChangeColumnCaptionOfficer()
                dtGOfficer.AutoSizeColumns()
                lbNumberItems.Text = OfficerDataSet.Tables("Officer").Rows.Count.ToString() & " รายการ"
            Catch ex As Exception
                Dim num As Integer = MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End Try
            sqlConnection.Close()
            ClearPicture()
            If dtGOfficer.RowCount > 0 Then ShowPicture()
            Cursor = Cursors.Default
        End Sub

        Private Sub btEdit_Click(sender As Object, e As EventArgs)
            EditOfficer()
        End Sub

        Private Sub EditOfficer()
            If dtGOfficer.SelectedItems.Count = 1 Then
                Dim int32_1 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerNumber")).Value)
                Dim fOfficerAddEdit As fOfficerAddEdit = New fOfficerAddEdit()
                Dim int32_2 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Sex")).Value)
                fOfficerAddEdit.lLoginONumber = lLoginONumber
                fOfficerAddEdit.myClass = [myClass]
                fOfficerAddEdit.fParent = Me
                fOfficerAddEdit.iMode = 1
                fOfficerAddEdit.lbOfficerNumber.Text = int32_1.ToString()
                fOfficerAddEdit.tbOfficerCode.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerCode")).Value).TrimEnd()
                fOfficerAddEdit.tbBarcode.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Barcode")).Value).TrimEnd()
                fOfficerAddEdit.tbName.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Name")).Value).TrimEnd()
                If int32_2 = 0L Then
                    fOfficerAddEdit.cbSex.SelectedIndex = 0
                Else
                    fOfficerAddEdit.cbSex.SelectedIndex = 1
                End If
                fOfficerAddEdit.tbAddress.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Address")).Value).TrimEnd()
                fOfficerAddEdit.lbPicName.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("PicName")).Value).TrimEnd()
                fOfficerAddEdit.tbTel.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Tel")).Value).TrimEnd()
                fOfficerAddEdit.tbMobile.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Mobile")).Value).TrimEnd()
                fOfficerAddEdit.tbPeopleID.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("PeopleID")).Value).TrimEnd()
                fOfficerAddEdit.tbPos.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Pos")).Value).TrimEnd()
                fOfficerAddEdit.tbSalary.Text = Convert.ToDecimal(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Salary")).Value).ToString([myClass].GetDecimalPlacesFormatString())
                If dtGOfficer.GetRow(dtGOfficer.Row).Cells("WebAppBrandID").Value IsNot DBNull.Value Then fOfficerAddEdit.lWebAppBrandID = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("WebAppBrandID")).Value)
                If Not OfficerDataSet.Tables(CStr("Officer")).Rows(dtGOfficer.GetRow(dtGOfficer.Row).RowIndex).IsNull("Status") Then fOfficerAddEdit.cbStatus.Checked = True
                Dim num As Integer = CInt(fOfficerAddEdit.ShowDialog())
                ShowPicture()
                ShowOfficerBranch()
            Else
                Dim num As Integer = MessageBox.Show("กรุณาเลือกรายการที่ต้องการแก้ไขเพียง 1 รายการ ก่อนกดปุ่ม Edit", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If dtGOfficer.RowCount > 0 Then dtGOfficer.Focus()
            End If
        End Sub

        Private Sub btPrint_Click(sender As Object, e As EventArgs)
            Dim num As Integer = MessageBox.Show("UNDER CONSTRUCTION!")
        End Sub

        Private Sub btDelete_Click(sender As Object, e As EventArgs)
            Dim flag1 = False
            If dtGOfficer.SelectedItems.Count > 0 Then
                If MessageBox.Show("คุณต้อง ยกเลิก รายการที่ได้เลือกไว้ ใช่หรือไม่?", "Comfirm message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return
                Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
                Dim command As SqlCommand = sqlConnection.CreateCommand()
                Cursor = Cursors.WaitCursor
                sqlConnection.Open()
                Dim index As Integer = dtGOfficer.SelectedItems.Count - 1

                While index >= 0
                    Dim int32 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.SelectedItems(CInt(index)).Position).Cells(CStr("OfficerNumber")).Value)
                    Dim str As String = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.SelectedItems(CInt(index)).Position).Cells(CStr("Name")).Value).TrimEnd()
                    If int32 > 1L Then
                        command.CommandText = $"UPDATE Officer SET Status = 1,UONumber = {lLoginONumber.ToString()},UTime = GetDate()  WHERE OfficerNumber = {int32.ToString()}"
                        Dim flag2 = True
                        Try
                            command.ExecuteNonQuery()
                        Catch ex As Exception
                            flag2 = False
                        End Try
                        If Not flag2 Then
                            Dim num As Integer = MessageBox.Show($"ไม่สามารถยกเลิกรายการพนักงานชื่อ {str} เป็นพนักงานที่ไม่ใช้แล้ว!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            OfficerDataSet.Tables(CStr("Officer")).Rows(dtGOfficer.GetRow(dtGOfficer.SelectedItems(CInt(index)).Position).RowIndex)("Status") = 1
                            OfficerDataSet.Tables(CStr("Officer")).Rows(dtGOfficer.GetRow(dtGOfficer.SelectedItems(CInt(index)).Position).RowIndex)("UOName") = CObj(lLoginONumber.ToString())
                            OfficerDataSet.Tables(CStr("Officer")).Rows(dtGOfficer.GetRow(dtGOfficer.SelectedItems(CInt(index)).Position).RowIndex)("UTime") = Date.Now
                            flag1 = True
                        End If
                    Else
                        Dim num1 As Integer = MessageBox.Show("ไม่สามารถยกเลิกข้อมูลของ Admin ได้!", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    End If

                    Threading.Interlocked.Decrement(index)
                End While
                sqlConnection.Close()
                Cursor = Cursors.Default
                If flag1 Then
                    ClearPicture()
                    dtGOfficer.AutoSizeColumns()
                    Dim num As Integer = MessageBox.Show("โปรแกรมได้ทำการ ยกเลิกข้อมูลเสร็จเรียบร้อยแล้ว.", "Delete message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If
            Else
                Dim num2 As Integer = MessageBox.Show("กรุณาเลือกรายการที่ต้องการลบ ก่อนกดปุ่ม Delete", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub

        Private Sub lvOfficer_SelectedIndexChanged(sender As Object, e As EventArgs)
            ShowPicture()
        End Sub

        Private Sub ShowPicture()
            If dtGOfficer.SelectedItems.Count <> 1 Then Return
            Dim filename As String = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("PicName")).Value).TrimEnd()
            If Not Equals(filename, "") Then
                Dim image As Image = Nothing
                Dim flag = True
                If filename(0) = "\"c Then
                    Try
                        image = Drawing.Image.FromFile([myClass].NewStockPath.ToString() & filename.Substring(1))
                    Catch ex As FileNotFoundException
                        flag = False
                    End Try
                Else
                    Try
                        image = Image.FromFile(filename)
                    Catch ex As FileNotFoundException
                        flag = False
                    End Try
                End If
                If flag Then
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
                    ClearPicture()
                End If
            Else
                ClearPicture()
            End If
        End Sub

        Private Sub ClearPicture()
            pbPicture.Image = Nothing
            pbPicture.Location = New Point(iFX, pbPicture.Location.Y)
            pbPicture.Width = iFW
            pbPicture.Height = iFH
        End Sub

        Private Sub lvOfficer_DoubleClick(sender As Object, e As EventArgs)
            If iMode = 0 Then
                If Not m_bCanEditCancelOfficer Then Return
                btEdit_Click(sender, e)
            Else
                btOK_Click(sender, e)
            End If
        End Sub

        Private Sub lvOfficer_KeyPress(sender As Object, e As KeyPressEventArgs)
            If e.KeyChar <> ChrW(13) Then Return
            If iMode = 0 Then
                If m_bCanEditCancelOfficer Then btEdit_Click(sender, e)
            Else
                btOK_Click(sender, e)
                tbName.Focus()
                tbName.SelectAll()
            End If
        End Sub

        Private Sub tbName_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If iMode <= 0 OrElse e.KeyChar <> ChrW(13) OrElse dtGOfficer.RowCount <= 0 Then Return
            dtGOfficer.Focus()
        End Sub

        Private Sub btOK_Click(sender As Object, e As EventArgs)
            OKClick()
        End Sub

        Private Sub OKClick()
            If dtGOfficer.SelectedItems.Count = 1 Then
                sOfficerNumber = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerNumber")).Value).ToString()
                sName = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Name")).Value).TrimEnd()
                DialogResult = DialogResult.OK
                Close()
            Else
                Dim num As Integer = MessageBox.Show("กรุณาเลือกรายการที่ต้องการเพียง 1 รายการ ก่อนกดปุ่ม OK", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub

        Private Sub btAccessLevel_Click(sender As Object, e As EventArgs)
            If dtGOfficer.SelectedItems.Count = 1 Then
                Dim int32 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerNumber")).Value)
                If int32 > 1L Then
                    Dim fAccessLevel As fAccessLevel = New fAccessLevel()
                    fAccessLevel.lbOfficerNumber.Text = int32.ToString()
                    fAccessLevel.lbOfficerName.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Name")).Value).TrimEnd()
                    fAccessLevel.lLoginONumber = lLoginONumber
                    fAccessLevel.[myClass] = [myClass]
                    Dim num As Integer = CInt(fAccessLevel.ShowDialog())
                Else
                    Dim num1 As Integer = MessageBox.Show("ไม่สามารถ Set Access Level ของ Admin ได้!", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If
            Else
                Dim num As Integer = MessageBox.Show("กรุณาเลือกรายการที่ต้องการ Set Access Level กดปุ่ม Access Level", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If dtGOfficer.RowCount > 0 Then dtGOfficer.Focus()
            End If
        End Sub

        Private Sub bSetUserNamePassword_Click(sender As Object, e As EventArgs)
            If dtGOfficer.SelectedItems.Count = 1 Then
                Dim int32 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerNumber")).Value)
                If int32 > 1L Then
                    Dim userNamePassword As fSetUserNamePassword = New fSetUserNamePassword()
                    userNamePassword.lbOfficerNumber.Text = int32.ToString()
                    userNamePassword.lbOfficerName.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Name")).Value).TrimEnd()
                    userNamePassword.lLoginONumber = lLoginONumber
                    userNamePassword.[myClass] = [myClass]
                    Dim num As Integer = CInt(userNamePassword.ShowDialog())
                ElseIf lLoginONumber = 1L AndAlso int32 = 1L Then
                    Dim userNamePassword As fSetUserNamePassword = New fSetUserNamePassword()
                    userNamePassword.lbOfficerNumber.Text = int32.ToString()
                    userNamePassword.lbOfficerName.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Name")).Value).TrimEnd()
                    userNamePassword.lLoginONumber = lLoginONumber
                    userNamePassword.[myClass] = [myClass]
                    Dim num As Integer = CInt(userNamePassword.ShowDialog())
                Else
                    Dim num1 As Integer = MessageBox.Show("ไม่สามารถ Set UserName และ Password ของ Admin ได้!", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If
            Else
                Dim num As Integer = MessageBox.Show("กรุณาเลือกรายการที่ต้องการ Set UserName and Password" & vbLf & "ก่อนกดปุ่ม Set UserName Password", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If dtGOfficer.RowCount > 0 Then dtGOfficer.Focus()
            End If
        End Sub

        Private Sub dtGOfficer_SelectionChanged(sender As Object, e As EventArgs)
            If m_bOnSelectAllRowsOfficer Then Return
            ShowPicture()
            ShowOfficerBranch()
        End Sub

        Private Sub tbName_Enter(sender As Object, e As EventArgs)
            tbName.SelectAll()
        End Sub

        Private Sub dtGOfficer_FormattingRow(sender As Object, e As RowLoadEventArgs)
            If e.Row.RowType IsNot RowType.Record Then Return
            Dim num As Long
            If OfficerDataSet.Tables(CStr("Officer")).Rows(e.Row.RowIndex).IsNull("Status") Then
                num = 0L
            Else
                num = 1L
                e.Row.Cells("Status").Text = "ไม่ใช้แล้ว"
            End If
            e.Row.Cells("Sex").Text = If(Convert.ToInt32(OfficerDataSet.Tables(CStr("Officer")).Rows(e.Row.RowIndex)("Sex")) <> 0, "หญิง", "ชาย")
            If Convert.ToDecimal(e.Row.Cells(CStr("Salary")).Value) = 0D Then
                e.Row.Cells("Salary").FormatStyle = New GridEXFormatStyle() With {
            .ForeColor = ColorZeroFore
          }
            Else
                e.Row.Cells("Salary").FormatStyle = New GridEXFormatStyle() With {
            .ForeColor = Color.Black
          }
            End If
            If e.Row.Cells("WebAppBrandName").Value Is DBNull.Value Then e.Row.Cells("WebAppBrandName").Text = "- ไม่จำกัด -"
            If num = 1L Then e.Row.RowStyle = New GridEXFormatStyle() With {
    .ForeColor = [myClass].ColorCancelFore,
    .BackColor = [myClass].ColorCancelBack
  }
        End Sub

        Private Sub tbName_TextChanged(sender As Object, e As EventArgs)
            If tbName.Text.Length < 3 Then Return
            btSearch_Click(sender, e)
        End Sub

        Private Sub dtGOfficer_RowDoubleClick(sender As Object, e As RowActionEventArgs)
            If iMode = 0 Then
                If Not m_bCanEditCancelOfficer Then Return
                EditOfficer()
            Else
                OKClick()
                tbName.Focus()
                tbName.SelectAll()
            End If
        End Sub

        Private Sub ctCopy_Click(sender As Object, e As EventArgs)
            If dtGOfficer.Focused Then
                tbCopy.Text = dtGOfficer.GetClipString()
                tbCopy.SelectAll()
                tbCopy.Copy()
            Else
                If Not dtGOfficerBranch.Focused Then Return
                tbCopy.Text = dtGOfficerBranch.GetClipString()
                tbCopy.SelectAll()
                tbCopy.Copy()
            End If
        End Sub

        Private Sub ctSelectAllRows_Click(sender As Object, e As EventArgs)
            If dtGOfficer.Focused Then
                m_bOnSelectAllRowsOfficer = True
                dtGOfficer.SelectedItems.Clear()
                Dim position = 0

                While position < dtGOfficer.RowCount
                    dtGOfficer.SelectedItems.Add(position)
                    Threading.Interlocked.Increment(position)
                End While
                m_bOnSelectAllRowsOfficer = False
            Else
                If Not dtGOfficerBranch.Focused Then Return
                dtGOfficerBranch.SelectedItems.Clear()
                Dim position = 0

                While position < dtGOfficerBranch.RowCount
                    dtGOfficerBranch.SelectedItems.Add(position)
                    Threading.Interlocked.Increment(position)
                End While
            End If
        End Sub

        Private Sub ChangeGridFormatOfficerBranch()
            For Each column As DataColumn In CType(OfficerBranchDataSet.Tables("OfficerBranch").Columns, InternalDataCollectionBase)
                Dim dataType = column.DataType
                If dataType Is Type.GetType("System.Int16") OrElse dataType Is Type.GetType("System.Int32") OrElse dataType Is Type.GetType("System.Int64") Then
                    dtGOfficerBranch.RootTable.Columns(column.ColumnName).FormatString = "N0"
                    dtGOfficerBranch.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Far
                ElseIf dataType Is Type.GetType("System.Decimal") Then
                    dtGOfficerBranch.RootTable.Columns(column.ColumnName).FormatString = [myClass].GetDecimalPlacesFormatStringWithComma()
                    dtGOfficerBranch.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Far
                ElseIf dataType Is Type.GetType("System.DateTime") Then
                    dtGOfficerBranch.RootTable.Columns(column.ColumnName).FormatString = "dd MMMM yyyy"
                    dtGOfficerBranch.RootTable.Columns(column.ColumnName).TextAlignment = TextAlignment.Center
                End If
            Next
        End Sub

        Private Sub HideGridColumnOfficerBranch()
        End Sub

        Private Sub ChangeColumnCaptionOfficerBranch()
            dtGOfficerBranch.RootTable.Columns("BranchNumber").Caption = "ID"
            dtGOfficerBranch.RootTable.Columns("BranchName").Caption = "Branch Name (ชื่อสาขา)"
        End Sub

        Private Sub ShowOfficerBranch()
            OfficerBranchDataSet.Tables("OfficerBranch").Rows.Clear()
            If dtGOfficer.RowCount <= 0 Then Return
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim int32 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerNumber")).Value)
            If int32 = 1L Then
                command.CommandText = "SELECT Branch.BranchNumber,RTRIM(Branch.BranchName) AS 'BranchName' FROM Branch  ORDER BY Branch.BranchNumber ASC"
            Else
                command.CommandText = $"SELECT OfficerBranch.BranchNumber,RTRIM(Branch.BranchName) AS 'BranchName' FROM OfficerBranch LEFT OUTER JOIN Branch ON OfficerBranch.BranchNumber = Branch.BranchNumber WHERE OfficerBranch.OfficerNumber = {int32.ToString()} ORDER BY OfficerBranch.BranchNumber ASC"
            End If
            sqlConnection.Open()
            Try
                Dim sqlDataAdapter As SqlDataAdapter = New SqlDataAdapter()
                sqlDataAdapter.SelectCommand = command
                OfficerBranchDataSet.Clear()
                sqlDataAdapter.Fill(OfficerBranchDataSet, "OfficerBranch")
                sqlDataAdapter.Dispose()
                dtGOfficerBranch.SetDataBinding(OfficerBranchDataSet, "OfficerBranch")
                dtGOfficerBranch.RetrieveStructure()
                ChangeGridFormatOfficerBranch()
                HideGridColumnOfficerBranch()
                ChangeColumnCaptionOfficerBranch()
                dtGOfficerBranch.AutoSizeColumns()
                lbNumberItems2.Text = OfficerBranchDataSet.Tables("OfficerBranch").Rows.Count.ToString() & " รายการ"
            Catch ex As Exception
                Dim num As Integer = MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End Try
            sqlConnection.Close()
        End Sub

        Private Sub btSetLimitSearch_Click(sender As Object, e As EventArgs)
            If dtGOfficer.SelectedItems.Count = 1 Then
                Dim int32 As Long = Convert.ToInt32(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("OfficerNumber")).Value)
                If int32 > 1L Then
                    Dim officerSetLimitSearch As fOfficerSetLimitSearch = New fOfficerSetLimitSearch()
                    officerSetLimitSearch.m_lEditOficerNumber = int32
                    officerSetLimitSearch.lbOfficerNumber.Text = int32.ToString()
                    officerSetLimitSearch.lbOfficerName.Text = Convert.ToString(dtGOfficer.GetRow(dtGOfficer.Row).Cells(CStr("Name")).Value).TrimEnd()
                    officerSetLimitSearch.lLoginONumber = lLoginONumber
                    officerSetLimitSearch.myClass = [myClass]
                    Dim num As Integer = CInt(officerSetLimitSearch.ShowDialog())
                Else
                    Dim num1 As Integer = MessageBox.Show("ไม่สามารถ Set Limit Search ของ Admin ได้!", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                End If
            Else
                Dim num As Integer = MessageBox.Show("กรุณาเลือกรายการพนักงานก่อนกดปุ่ม Set Limit Search", "Guide message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If dtGOfficer.RowCount > 0 Then dtGOfficer.Focus()
            End If
        End Sub
    End Class
End Namespace
