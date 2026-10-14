Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>
''' หน้ารับวัตถุดิบเข้าสต็อก ใช้ dbo.[Transaction] DocType=1 และ dbo.TransactionDetail
''' </summary>
Public Class frmMaterialReceive
    Inherits Form

    Friend WithEvents txtDocumentNo As TextBox
    Friend WithEvents dtpReceiveDate As DateTimePicker
    Friend WithEvents cboPurpose As ComboBox
    Friend WithEvents txtSupplier As TextBox
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents txtMaterialID As TextBox
    Friend WithEvents txtMaterialDetail As TextBox
    Friend WithEvents txtBrand As TextBox
    Friend WithEvents txtUnitUse As TextBox
    Friend WithEvents nudPack As NumericUpDown
    Friend WithEvents nudUseUnit As NumericUpDown
    Friend WithEvents gridDetails As DataGridView
    Friend WithEvents lblSummary As Label
    Friend WithEvents btnSelect As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnRemove As Button
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnClose As Button
    Private printDocument As PrintDocument

    Private selectedMaterial As MaterialChoice
    Private printRowIndex As Integer
    Friend WithEvents root As TableLayoutPanel
    Friend WithEvents title As Label
    Friend WithEvents header As TableLayoutPanel
    Friend WithEvents lblDocument As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents lblPurpose As Label
    Friend WithEvents lblSupplier As Label
    Friend WithEvents lblRemark As Label
    Friend WithEvents detailInput As TableLayoutPanel
    Friend WithEvents lblMaterial As Label
    Friend WithEvents lblPack As Label
    Friend WithEvents lblUseUnit As Label
    Friend WithEvents footer As TableLayoutPanel
    Private loading As Boolean

    Public Sub New()
        InitializeComponent()
        ConfigureGrid()
        ConfigureTextBox(txtDocumentNo, True)
        ConfigureTextBox(txtSupplier, False)
        ConfigureTextBox(txtRemark, False)
        ConfigureTextBox(txtMaterialID, False)
        ConfigureTextBox(txtMaterialDetail, True)
        ConfigureTextBox(txtBrand, True)
        ConfigureTextBox(txtUnitUse, True)
        ConfigureDatePicker(dtpReceiveDate)
        ConfigureNumber(nudPack)
        ConfigureNumber(nudUseUnit)
        StyleButton(btnSelect, Color.FromArgb(45, 116, 190), 78)
        StyleButton(btnAdd, Color.FromArgb(38, 132, 74), 115)
        StyleButton(btnRemove, Color.FromArgb(190, 75, 75), 100)
        StyleButton(btnHistory, Color.FromArgb(102, 87, 145), 125)
        StyleButton(btnNew, Color.FromArgb(45, 116, 190), 110)
        StyleButton(btnSave, Color.FromArgb(38, 132, 74), 110)
        StyleButton(btnPrint, Color.FromArgb(117, 82, 160), 125)
        StyleButton(btnClose, Color.FromArgb(105, 105, 105), 85)
        AddHandler Load, AddressOf FormLoaded
        AddHandler btnSelect.Click, AddressOf SelectMaterialClick
        AddHandler txtMaterialID.KeyDown, AddressOf MaterialCodeKeyDown
        AddHandler txtMaterialID.Leave, AddressOf MaterialCodeLeave
        AddHandler btnAdd.Click, AddressOf AddDetailClick
        AddHandler btnRemove.Click, AddressOf RemoveDetailClick
        AddHandler btnHistory.Click, AddressOf HistoryClick
        AddHandler btnNew.Click, AddressOf NewDocumentClick
        AddHandler btnSave.Click, AddressOf SaveDocumentClick
        AddHandler btnPrint.Click, AddressOf PrintDocumentClick
        AddHandler btnClose.Click, Sub(sender, e) Close()
        AddHandler gridDetails.CellEndEdit, AddressOf GridCellEndEdit
        AddHandler printDocument.PrintPage, AddressOf PrintPage
    End Sub

    Private Sub InitializeComponent()
        Me.txtDocumentNo = New System.Windows.Forms.TextBox()
        Me.dtpReceiveDate = New System.Windows.Forms.DateTimePicker()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.txtMaterialID = New System.Windows.Forms.TextBox()
        Me.txtMaterialDetail = New System.Windows.Forms.TextBox()
        Me.txtBrand = New System.Windows.Forms.TextBox()
        Me.txtUnitUse = New System.Windows.Forms.TextBox()
        Me.nudPack = New System.Windows.Forms.NumericUpDown()
        Me.nudUseUnit = New System.Windows.Forms.NumericUpDown()
        Me.gridDetails = New System.Windows.Forms.DataGridView()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.printDocument = New System.Drawing.Printing.PrintDocument()
        Me.root = New System.Windows.Forms.TableLayoutPanel()
        Me.title = New System.Windows.Forms.Label()
        Me.header = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDocument = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblPurpose = New System.Windows.Forms.Label()
        Me.lblSupplier = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.detailInput = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.lblPack = New System.Windows.Forms.Label()
        Me.lblUseUnit = New System.Windows.Forms.Label()
        Me.footer = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.nudPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudUseUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.root.SuspendLayout()
        Me.header.SuspendLayout()
        Me.detailInput.SuspendLayout()
        Me.footer.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.Location = New System.Drawing.Point(101, 11)
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(100, 44)
        Me.txtDocumentNo.TabIndex = 5
        '
        'dtpReceiveDate
        '
        Me.dtpReceiveDate.Location = New System.Drawing.Point(322, 11)
        Me.dtpReceiveDate.Name = "dtpReceiveDate"
        Me.dtpReceiveDate.Size = New System.Drawing.Size(168, 44)
        Me.dtpReceiveDate.TabIndex = 6
        '
        'cboPurpose
        '
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Items.AddRange(New Object() {"ซื้อ ", "ยกมา", "อื่นๆ"})
        Me.cboPurpose.Location = New System.Drawing.Point(580, 11)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.Size = New System.Drawing.Size(120, 45)
        Me.cboPurpose.TabIndex = 7
        '
        'txtSupplier
        '
        Me.txtSupplier.Location = New System.Drawing.Point(771, 11)
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.Size = New System.Drawing.Size(100, 44)
        Me.txtSupplier.TabIndex = 8
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(1016, 11)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(100, 44)
        Me.txtRemark.TabIndex = 9
        '
        'txtMaterialID
        '
        Me.txtMaterialID.Location = New System.Drawing.Point(86, 11)
        Me.txtMaterialID.Name = "txtMaterialID"
        Me.txtMaterialID.Size = New System.Drawing.Size(100, 44)
        Me.txtMaterialID.TabIndex = 3
        '
        'txtMaterialDetail
        '
        Me.txtMaterialDetail.Location = New System.Drawing.Point(294, 11)
        Me.txtMaterialDetail.Name = "txtMaterialDetail"
        Me.txtMaterialDetail.Size = New System.Drawing.Size(100, 44)
        Me.txtMaterialDetail.TabIndex = 5
        '
        'txtBrand
        '
        Me.txtBrand.Location = New System.Drawing.Point(0, 0)
        Me.txtBrand.Name = "txtBrand"
        Me.txtBrand.Size = New System.Drawing.Size(100, 22)
        Me.txtBrand.TabIndex = 0
        '
        'txtUnitUse
        '
        Me.txtUnitUse.Location = New System.Drawing.Point(812, 11)
        Me.txtUnitUse.Name = "txtUnitUse"
        Me.txtUnitUse.Size = New System.Drawing.Size(99, 44)
        Me.txtUnitUse.TabIndex = 7
        '
        'nudPack
        '
        Me.nudPack.Location = New System.Drawing.Point(637, 11)
        Me.nudPack.Name = "nudPack"
        Me.nudPack.Size = New System.Drawing.Size(94, 44)
        Me.nudPack.TabIndex = 6
        '
        'nudUseUnit
        '
        Me.nudUseUnit.Location = New System.Drawing.Point(917, 11)
        Me.nudUseUnit.Name = "nudUseUnit"
        Me.nudUseUnit.Size = New System.Drawing.Size(72, 44)
        Me.nudUseUnit.TabIndex = 8
        '
        'gridDetails
        '
        Me.gridDetails.ColumnHeadersHeight = 29
        Me.gridDetails.Location = New System.Drawing.Point(15, 253)
        Me.gridDetails.Name = "gridDetails"
        Me.gridDetails.RowHeadersWidth = 51
        Me.gridDetails.Size = New System.Drawing.Size(240, 150)
        Me.gridDetails.TabIndex = 3
        '
        'lblSummary
        '
        Me.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSummary.Location = New System.Drawing.Point(7, 4)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(611, 52)
        Me.lblSummary.TabIndex = 0
        Me.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(216, 11)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(72, 23)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "เลือก"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(995, 11)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "เพิ่มรายการ"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(1110, 11)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 10
        Me.btnRemove.Text = "ลบแถว"
        '
        'btnHistory
        '
        Me.btnHistory.Location = New System.Drawing.Point(624, 7)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(75, 23)
        Me.btnHistory.TabIndex = 1
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(749, 7)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(869, 7)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 3
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(989, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 4
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1124, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 5
        '
        'root
        '
        Me.root.ColumnCount = 1
        Me.root.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.root.Controls.Add(Me.title, 0, 0)
        Me.root.Controls.Add(Me.header, 0, 1)
        Me.root.Controls.Add(Me.detailInput, 0, 2)
        Me.root.Controls.Add(Me.gridDetails, 0, 3)
        Me.root.Controls.Add(Me.footer, 0, 4)
        Me.root.Dock = System.Windows.Forms.DockStyle.Fill
        Me.root.Location = New System.Drawing.Point(0, 0)
        Me.root.Name = "root"
        Me.root.Padding = New System.Windows.Forms.Padding(12)
        Me.root.RowCount = 5
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.root.Size = New System.Drawing.Size(1250, 760)
        Me.root.TabIndex = 0
        '
        'title
        '
        Me.title.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.title.Font = New System.Drawing.Font("Cordia New", 22.0!, System.Drawing.FontStyle.Bold)
        Me.title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.title.Location = New System.Drawing.Point(15, 12)
        Me.title.Name = "title"
        Me.title.Padding = New System.Windows.Forms.Padding(14, 0, 0, 0)
        Me.title.Size = New System.Drawing.Size(1220, 58)
        Me.title.TabIndex = 0
        Me.title.Text = "รับวัตถุดิบเข้าสต็อก"
        Me.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'header
        '
        Me.header.ColumnCount = 10
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.header.Controls.Add(Me.lblDocument, 0, 0)
        Me.header.Controls.Add(Me.lblDate, 2, 0)
        Me.header.Controls.Add(Me.lblPurpose, 4, 0)
        Me.header.Controls.Add(Me.lblSupplier, 6, 0)
        Me.header.Controls.Add(Me.lblRemark, 8, 0)
        Me.header.Controls.Add(Me.txtDocumentNo, 1, 0)
        Me.header.Controls.Add(Me.dtpReceiveDate, 3, 0)
        Me.header.Controls.Add(Me.cboPurpose, 5, 0)
        Me.header.Controls.Add(Me.txtSupplier, 7, 0)
        Me.header.Controls.Add(Me.txtRemark, 9, 0)
        Me.header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.header.Location = New System.Drawing.Point(15, 73)
        Me.header.Name = "header"
        Me.header.Padding = New System.Windows.Forms.Padding(8, 8, 8, 4)
        Me.header.RowCount = 1
        Me.header.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.header.Size = New System.Drawing.Size(1220, 98)
        Me.header.TabIndex = 1
        '
        'lblDocument
        '
        Me.lblDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDocument.Location = New System.Drawing.Point(11, 8)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(84, 86)
        Me.lblDocument.TabIndex = 0
        Me.lblDocument.Text = "เลขที่รับ"
        Me.lblDocument.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDate
        '
        Me.lblDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDate.Location = New System.Drawing.Point(256, 8)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(60, 86)
        Me.lblDate.TabIndex = 1
        Me.lblDate.Text = "วันที่"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPurpose
        '
        Me.lblPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPurpose.Location = New System.Drawing.Point(496, 8)
        Me.lblPurpose.Name = "lblPurpose"
        Me.lblPurpose.Size = New System.Drawing.Size(78, 86)
        Me.lblPurpose.TabIndex = 2
        Me.lblPurpose.Text = "ประเภท"
        Me.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSupplier
        '
        Me.lblSupplier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSupplier.Location = New System.Drawing.Point(706, 8)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.Size = New System.Drawing.Size(59, 86)
        Me.lblSupplier.TabIndex = 3
        Me.lblSupplier.Text = "ผู้ขาย"
        Me.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRemark
        '
        Me.lblRemark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRemark.Location = New System.Drawing.Point(921, 8)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(89, 86)
        Me.lblRemark.TabIndex = 4
        Me.lblRemark.Text = "หมายเหตุ"
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'detailInput
        '
        Me.detailInput.ColumnCount = 11
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.detailInput.Controls.Add(Me.lblMaterial, 0, 0)
        Me.detailInput.Controls.Add(Me.lblPack, 4, 0)
        Me.detailInput.Controls.Add(Me.lblUseUnit, 6, 0)
        Me.detailInput.Controls.Add(Me.txtMaterialID, 1, 0)
        Me.detailInput.Controls.Add(Me.btnSelect, 2, 0)
        Me.detailInput.Controls.Add(Me.txtMaterialDetail, 3, 0)
        Me.detailInput.Controls.Add(Me.nudPack, 5, 0)
        Me.detailInput.Controls.Add(Me.txtUnitUse, 7, 0)
        Me.detailInput.Controls.Add(Me.nudUseUnit, 8, 0)
        Me.detailInput.Controls.Add(Me.btnAdd, 9, 0)
        Me.detailInput.Controls.Add(Me.btnRemove, 10, 0)
        Me.detailInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.detailInput.Location = New System.Drawing.Point(15, 177)
        Me.detailInput.Name = "detailInput"
        Me.detailInput.Padding = New System.Windows.Forms.Padding(8, 8, 8, 4)
        Me.detailInput.RowCount = 1
        Me.detailInput.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.detailInput.Size = New System.Drawing.Size(1220, 70)
        Me.detailInput.TabIndex = 2
        '
        'lblMaterial
        '
        Me.lblMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMaterial.Location = New System.Drawing.Point(11, 8)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(69, 58)
        Me.lblMaterial.TabIndex = 0
        Me.lblMaterial.Text = "วัตถุดิบ"
        Me.lblMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPack
        '
        Me.lblPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPack.Location = New System.Drawing.Point(564, 8)
        Me.lblPack.Name = "lblPack"
        Me.lblPack.Size = New System.Drawing.Size(67, 58)
        Me.lblPack.TabIndex = 1
        Me.lblPack.Text = "แพ็ก"
        Me.lblPack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUseUnit
        '
        Me.lblUseUnit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUseUnit.Location = New System.Drawing.Point(737, 8)
        Me.lblUseUnit.Name = "lblUseUnit"
        Me.lblUseUnit.Size = New System.Drawing.Size(69, 58)
        Me.lblUseUnit.TabIndex = 2
        Me.lblUseUnit.Text = "จำนวนรับ"
        Me.lblUseUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'footer
        '
        Me.footer.ColumnCount = 6
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95.0!))
        Me.footer.Controls.Add(Me.lblSummary, 0, 0)
        Me.footer.Controls.Add(Me.btnHistory, 1, 0)
        Me.footer.Controls.Add(Me.btnNew, 2, 0)
        Me.footer.Controls.Add(Me.btnSave, 3, 0)
        Me.footer.Controls.Add(Me.btnPrint, 4, 0)
        Me.footer.Controls.Add(Me.btnClose, 5, 0)
        Me.btnHistory.Text = "รายการย้อนหลัง"
        Me.btnNew.Text = "เอกสารใหม่"
        Me.btnSave.Text = "บันทึก"
        Me.btnPrint.Text = "พิมพ์เอกสาร"
        Me.btnClose.Text = "ปิด"
        Me.footer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.footer.Location = New System.Drawing.Point(15, 685)
        Me.footer.Name = "footer"
        Me.footer.Padding = New System.Windows.Forms.Padding(4)
        Me.footer.RowCount = 1
        Me.footer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.footer.Size = New System.Drawing.Size(1220, 60)
        Me.footer.TabIndex = 4
        '
        'frmMaterialReceive
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1250, 760)
        Me.Controls.Add(Me.root)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.MinimumSize = New System.Drawing.Size(1100, 650)
        Me.Name = "frmMaterialReceive"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "รับวัตถุดิบเข้าสต็อก"
        CType(Me.nudPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudUseUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.root.ResumeLayout(False)
        Me.header.ResumeLayout(False)
        Me.header.PerformLayout()
        Me.detailInput.ResumeLayout(False)
        Me.detailInput.PerformLayout()
        Me.footer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private Shared Sub ConfigureTable(table As TableLayoutPanel, count As Integer, widths As Integer())
        table.Dock = DockStyle.Fill
        table.Padding = New Padding(8, 8, 8, 4)
        table.ColumnCount = count
        For Each width As Integer In widths
            table.ColumnStyles.Add(New ColumnStyle(If(width = 1, SizeType.Percent, SizeType.Absolute), If(width = 1, 100.0!, width)))
        Next
        table.RowCount = 1
        table.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
    End Sub

    Private Shared Sub AddLabel(parent As TableLayoutPanel, label As Label, caption As String, column As Integer)
        label.Text = caption
        label.Dock = DockStyle.Fill
        label.TextAlign = ContentAlignment.MiddleLeft
        label.Margin = New Padding(3, 0, 3, 0)
        parent.Controls.Add(label, column, 0)
    End Sub

    Private Shared Sub ConfigureTextBox(box As TextBox, readOnlyValue As Boolean)
        box.Dock = DockStyle.Fill
        box.ReadOnly = readOnlyValue
        box.Font = New Font("Cordia New", 16.0!)
        box.Margin = New Padding(3, 5, 3, 5)
    End Sub

    Private Shared Sub ConfigureDatePicker(picker As DateTimePicker)
        picker.Dock = DockStyle.Fill
        picker.Format = DateTimePickerFormat.Custom
        picker.CustomFormat = "dd/MM/yyyy HH:mm"
        picker.ShowUpDown = True
        picker.Margin = New Padding(3, 5, 3, 5)
    End Sub

    Private Shared Sub ConfigureNumber(numberBox As NumericUpDown)
        numberBox.Dock = DockStyle.Fill
        numberBox.DecimalPlaces = 2
        numberBox.Maximum = 100000000D
        numberBox.Minimum = 0D
        numberBox.ThousandsSeparator = True
        numberBox.Font = New Font("Cordia New", 16.0!)
        numberBox.Margin = New Padding(3, 5, 3, 5)
    End Sub

    Private Shared Sub StyleButton(button As Button, backColor As Color, width As Integer)
        button.Width = width
        button.Height = 42
        button.Anchor = AnchorStyles.None
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.BackColor = backColor
        button.ForeColor = Color.White
        button.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        button.Margin = New Padding(4)
    End Sub

    Private Sub ConfigureGrid()
        gridDetails.Dock = DockStyle.Fill
        gridDetails.AllowUserToAddRows = False
        gridDetails.AllowUserToDeleteRows = False
        gridDetails.AllowUserToResizeRows = False
        gridDetails.BackgroundColor = Color.White
        gridDetails.CellBorderStyle = DataGridViewCellBorderStyle.Single
        gridDetails.GridColor = Color.FromArgb(180, 180, 180)
        gridDetails.RowHeadersVisible = False
        gridDetails.RowTemplate.Height = 31
        gridDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridDetails.MultiSelect = True
        gridDetails.ColumnHeadersHeight = 38
        gridDetails.EnableHeadersVisualStyles = False
        gridDetails.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
            .BackColor = Color.Black, .ForeColor = Color.White,
            .Font = New Font("Cordia New", 15.0!, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        }
        gridDetails.DefaultCellStyle = New DataGridViewCellStyle With {
            .Font = New Font("Cordia New", 14.0!),
            .SelectionBackColor = Color.FromArgb(30, 125, 205),
            .SelectionForeColor = Color.White
        }
        AddGridColumn("LineNo", "ลำดับ", 55)
        AddGridColumn("MaterialID", "รหัสวัตถุดิบ", 125)
        AddGridColumn("MaterialDetail", "ชื่อวัตถุดิบ", 430)
        AddGridColumn("Brand", "ยี่ห้อ", 150)
        AddGridColumn("UnitUse", "หน่วย", 80)
        AddGridNumberColumn("PackQty", "Pack", 90, True)
        AddGridNumberColumn("UseQty", "จำนวนรับ", 120, False)
    End Sub

    Private Sub AddGridColumn(name As String, header As String, width As Integer)
        gridDetails.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = name, .HeaderText = header, .Width = width, .ReadOnly = True
        })
    End Sub

    Private Sub AddGridNumberColumn(name As String, header As String, width As Integer, readOnlyValue As Boolean)
        Dim column As New DataGridViewTextBoxColumn With {
            .Name = name, .HeaderText = header, .Width = width, .ReadOnly = readOnlyValue
        }
        column.DefaultCellStyle = New DataGridViewCellStyle With {
            .Format = "N2", .Alignment = DataGridViewContentAlignment.MiddleRight,
            .BackColor = If(readOnlyValue, Color.White, Color.FromArgb(255, 250, 215))
        }
        gridDetails.Columns.Add(column)
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        If MdiParent IsNot Nothing Then WindowState = FormWindowState.Maximized
        Try
            DbSchemaChecker.EnsureSupportTables()
            NewDocumentClick(btnNew, EventArgs.Empty)
        Catch ex As Exception
            MessageBox.Show("เปิดหน้ารับวัตถุดิบไม่สำเร็จ: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewDocumentClick(sender As Object, e As EventArgs)
        gridDetails.Rows.Clear()
        dtpReceiveDate.Value = DateTime.Now
        cboPurpose.SelectedIndex = 0
        txtSupplier.Clear()
        txtRemark.Clear()
        ClearMaterialInput()
        GenerateDocumentNo()
        UpdateSummary()
        btnSave.Enabled = True
        btnPrint.Enabled = False
    End Sub

    Private Sub GenerateDocumentNo()
        Dim dateValue As DateTime = dtpReceiveDate.Value
        Dim buddhistYear2 As String = (dateValue.Year + 543).ToString("0000", CultureInfo.InvariantCulture).Substring(2, 2)
        Dim prefix As String = "FGR" & buddhistYear2 & dateValue.ToString("MMdd", CultureInfo.InvariantCulture)
        Dim nextNumber As Integer = 1
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT MAX(DocNo) FROM dbo.[Transaction] WHERE DocNo LIKE @Prefix + N'%'", conn)
                    cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = prefix
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Dim current As String = Convert.ToString(value).Trim()
                        If current.Length > prefix.Length Then Integer.TryParse(current.Substring(prefix.Length), nextNumber)
                        nextNumber += 1
                    End If
                End Using
            End Using
        Catch
        End Try
        txtDocumentNo.Text = prefix & nextNumber.ToString("00", CultureInfo.InvariantCulture)
    End Sub

    Private Sub SelectMaterialClick(sender As Object, e As EventArgs)
        Using picker As New ReceiveMaterialPicker(txtMaterialID.Text.Trim())
            If picker.ShowDialog(Me) = DialogResult.OK Then SetMaterial(picker.Selected)
        End Using
    End Sub

    Private Sub MaterialCodeKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadMaterialByCode()
        End If
    End Sub

    Private Sub MaterialCodeLeave(sender As Object, e As EventArgs)
        If Not loading AndAlso txtMaterialID.Text.Trim() <> "" Then LoadMaterialByCode()
    End Sub

    Private Sub LoadMaterialByCode()
        Dim code As String = txtMaterialID.Text.Trim()
        If code = "" Then Return
        Try
            loading = True
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 LTRIM(RTRIM(MaterialID)) AS MaterialID, " &
                    "LTRIM(RTRIM(ISNULL(Material_Detail,N''))) AS MaterialDetail, " &
                    "LTRIM(RTRIM(ISNULL(Brand,N''))) AS Brand, " &
                    "LTRIM(RTRIM(ISNULL(Unit_Use,N''))) AS UnitUse " &
                    "FROM dbo.MaterialDrink WHERE LTRIM(RTRIM(MaterialID))=@MaterialID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = code
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            SetMaterial(New MaterialChoice(Convert.ToString(reader("MaterialID")).Trim(), Convert.ToString(reader("MaterialDetail")).Trim(), Convert.ToString(reader("Brand")).Trim(), Convert.ToString(reader("UnitUse")).Trim()))
                        Else
                            selectedMaterial = Nothing
                            txtMaterialDetail.Clear()
                            txtBrand.Clear()
                            txtUnitUse.Clear()
                            MessageBox.Show("ไม่พบรหัสวัตถุดิบนี้", "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ค้นหาวัตถุดิบไม่สำเร็จ: " & ex.Message, "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            loading = False
        End Try
    End Sub

    Private Sub SetMaterial(material As MaterialChoice)
        selectedMaterial = material
        If material Is Nothing Then Return
        txtMaterialID.Text = material.MaterialID
        txtMaterialDetail.Text = material.MaterialDetail
        txtBrand.Text = material.Brand
        txtUnitUse.Text = material.UnitUse
    End Sub

    Private Sub AddDetailClick(sender As Object, e As EventArgs)
        If selectedMaterial Is Nothing OrElse Not String.Equals(selectedMaterial.MaterialID, txtMaterialID.Text.Trim(), StringComparison.OrdinalIgnoreCase) Then LoadMaterialByCode()
        If selectedMaterial Is Nothing Then Return
        If nudPack.Value <= 0D AndAlso nudUseUnit.Value <= 0D Then
            MessageBox.Show("กรุณาระบุจำนวนรับอย่างน้อย 1 รายการ", "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        For Each row As DataGridViewRow In gridDetails.Rows
            If String.Equals(Convert.ToString(row.Cells("MaterialID").Value).Trim(), selectedMaterial.MaterialID, StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("วัตถุดิบนี้ถูกเพิ่มแล้ว กรุณาแก้จำนวนในรายการเดิม", "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Next
        Dim index As Integer = gridDetails.Rows.Add()
        Dim rowAdded As DataGridViewRow = gridDetails.Rows(index)
        rowAdded.Cells("LineNo").Value = index + 1
        rowAdded.Cells("MaterialID").Value = selectedMaterial.MaterialID
        rowAdded.Cells("MaterialDetail").Value = selectedMaterial.MaterialDetail
        rowAdded.Cells("Brand").Value = selectedMaterial.Brand
        rowAdded.Cells("UnitUse").Value = selectedMaterial.UnitUse
        rowAdded.Cells("PackQty").Value = nudPack.Value
        rowAdded.Cells("UseQty").Value = nudUseUnit.Value
        ClearMaterialInput()
        UpdateSummary()
    End Sub

    Private Sub RemoveDetailClick(sender As Object, e As EventArgs)
        For i As Integer = gridDetails.SelectedRows.Count - 1 To 0 Step -1
            gridDetails.Rows.RemoveAt(gridDetails.SelectedRows(i).Index)
        Next
        RenumberRows()
        UpdateSummary()
    End Sub

    Private Sub GridCellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then UpdateSummary()
    End Sub

    Private Sub RenumberRows()
        For i As Integer = 0 To gridDetails.Rows.Count - 1
            gridDetails.Rows(i).Cells("LineNo").Value = i + 1
        Next
    End Sub

    Private Sub ClearMaterialInput()
        selectedMaterial = Nothing
        txtMaterialID.Clear()
        txtMaterialDetail.Clear()
        txtBrand.Clear()
        txtUnitUse.Clear()
        nudPack.Value = 0D
        nudUseUnit.Value = 0D
    End Sub

    Private Sub UpdateSummary()
        Dim rows As Integer = 0
        Dim pack As Decimal = 0D
        Dim units As Decimal = 0D
        For Each row As DataGridViewRow In gridDetails.Rows
            Dim p As Decimal = CellDecimal(row, "PackQty")
            Dim u As Decimal = CellDecimal(row, "UseQty")
            If p <> 0D OrElse u <> 0D Then rows += 1
            pack += p
            units += u
        Next
        lblSummary.Text = "รับ " & rows.ToString("N0") & " รายการ  |  " & pack.ToString("N2") & " แพ็ก  |  " & units.ToString("N2") & " หน่วย"
    End Sub

    Private Shared Function CellDecimal(row As DataGridViewRow, columnName As String) As Decimal
        If row Is Nothing OrElse row.Cells(columnName).Value Is Nothing Then Return 0D
        Dim result As Decimal
        If Decimal.TryParse(Convert.ToString(row.Cells(columnName).Value), NumberStyles.Any, CultureInfo.CurrentCulture, result) Then Return result
        Return 0D
    End Function

    Private Sub SaveDocumentClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("Stock.In") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์รับวัตถุดิบเข้าสต็อก", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim rowCount As Integer = gridDetails.Rows.Cast(Of DataGridViewRow)().Count(Function(row) CellDecimal(row, "PackQty") <> 0D OrElse CellDecimal(row, "UseQty") <> 0D)
        If rowCount = 0 Then
            MessageBox.Show("กรุณาเพิ่มรายการและระบุจำนวนรับก่อนบันทึก", "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Const headerSql As String =
                        "INSERT INTO dbo.[Transaction](DocNo,[Date],DocType,Purpose_Receive,Purpose_Pay,Remark,SupplierID) " &
                        "VALUES(@DocNo,@Date,1,@Purpose,0,@Remark,@SupplierID)"
                    Using header As New SqlCommand(headerSql, conn, tr)
                        header.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = txtDocumentNo.Text.Trim()
                        header.Parameters.Add("@Date", SqlDbType.DateTime2).Value = dtpReceiveDate.Value
                        header.Parameters.Add("@Purpose", SqlDbType.Int).Value = cboPurpose.SelectedIndex + 1
                        header.Parameters.Add("@Remark", SqlDbType.NVarChar, 500).Value = If(txtRemark.Text.Trim() = "", CType(DBNull.Value, Object), txtRemark.Text.Trim())
                        header.Parameters.Add("@SupplierID", SqlDbType.NVarChar, 50).Value = If(txtSupplier.Text.Trim() = "", CType(DBNull.Value, Object), txtSupplier.Text.Trim())
                        header.ExecuteNonQuery()
                    End Using
                    Const detailSql As String =
                        "INSERT INTO dbo.TransactionDetail(DocNo,MaterialID,Rec_Qty_Pack,Rec_Qty_Use_Unit,Pay_Qty_Pack,Pay_Qty_Use_Unit) " &
                        "VALUES(@DocNo,@MaterialID,@PackQty,@UseQty,0,0)"
                    For Each row As DataGridViewRow In gridDetails.Rows
                        Dim pack As Decimal = CellDecimal(row, "PackQty")
                        Dim units As Decimal = CellDecimal(row, "UseQty")
                        If pack = 0D AndAlso units = 0D Then Continue For
                        Using detail As New SqlCommand(detailSql, conn, tr)
                            detail.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = txtDocumentNo.Text.Trim()
                            detail.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = Convert.ToString(row.Cells("MaterialID").Value).Trim()
                            AddDecimal(detail, "@PackQty", pack)
                            AddDecimal(detail, "@UseQty", units)
                            detail.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using
            MessageBox.Show("บันทึกใบรับวัตถุดิบเรียบร้อยแล้ว เลขที่ " & txtDocumentNo.Text.Trim(), "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnSave.Enabled = False
            btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show("บันทึกใบรับวัตถุดิบไม่สำเร็จ: " & ex.Message, "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Sub AddDecimal(command As SqlCommand, name As String, value As Decimal)
        Dim parameter As SqlParameter = command.Parameters.Add(name, SqlDbType.Decimal)
        parameter.Precision = 18
        parameter.Scale = 4
        parameter.Value = value
    End Sub

    Private Sub HistoryClick(sender As Object, e As EventArgs)
        Using picker As New ReceiveHistoryPicker()
            picker.ShowDialog(Me)
        End Using
    End Sub

    Private Sub PrintDocumentClick(sender As Object, e As EventArgs)
        If gridDetails.Rows.Count = 0 Then
            MessageBox.Show("ไม่มีรายการสำหรับพิมพ์", "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Try
            Using viewer As New frmReportViewer()
                viewer.LoadMaterialReceiveReport(BuildMaterialReceiveReportTable())
                viewer.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show("เปิดรายงานใบรับสินค้าไม่สำเร็จ: " & ex.Message, "รับวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BuildMaterialReceiveReportTable() As DataTable
        Dim result As New DataTable("MaterialReceive")
        result.Columns.Add("CompanyName", GetType(String))
        result.Columns.Add("DocNo", GetType(String))
        result.Columns.Add("ReceiveDateText", GetType(String))
        result.Columns.Add("PO", GetType(String))
        result.Columns.Add("PR", GetType(String))
        result.Columns.Add("Supplier", GetType(String))
        result.Columns.Add("Remark", GetType(String))
        result.Columns.Add("LineNo", GetType(Integer))
        result.Columns.Add("MaterialID", GetType(String))
        result.Columns.Add("ItemText", GetType(String))
        result.Columns.Add("Quantity", GetType(Decimal))
        result.Columns.Add("UnitUse", GetType(String))

        Dim lineNo As Integer = 1
        For Each row As DataGridViewRow In gridDetails.Rows
            Dim pack As Decimal = CellDecimal(row, "PackQty")
            Dim units As Decimal = CellDecimal(row, "UseQty")
            If pack = 0D AndAlso units = 0D Then Continue For
            Dim item As DataRow = result.NewRow()
            item("CompanyName") = "บริษัท สมายล์ ฮาร์ท ฟู้ดส์ จำกัด"
            item("DocNo") = txtDocumentNo.Text.Trim()
            item("ReceiveDateText") = dtpReceiveDate.Value.ToString("dd/MM/", CultureInfo.InvariantCulture) & (dtpReceiveDate.Value.Year + 543).ToString("0000", CultureInfo.InvariantCulture).Substring(2, 2)
            item("PO") = ""
            item("PR") = ""
            item("Supplier") = txtSupplier.Text.Trim()
            item("Remark") = txtRemark.Text.Trim()
            item("LineNo") = lineNo
            item("MaterialID") = Convert.ToString(row.Cells("MaterialID").Value).Trim()
            Dim materialText As String = Convert.ToString(row.Cells("MaterialDetail").Value).Trim()
            Dim brandText As String = Convert.ToString(row.Cells("Brand").Value).Trim()
            item("ItemText") = If(brandText = "", materialText, materialText & " / " & brandText)
            item("Quantity") = If(units <> 0D, units, pack)
            item("UnitUse") = Convert.ToString(row.Cells("UnitUse").Value).Trim()
            result.Rows.Add(item)
            lineNo += 1
        Next
        Return result
    End Function

    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)
        Using titleFont As New Font("Cordia New", 20.0!, FontStyle.Bold), bodyFont As New Font("Cordia New", 14.0!), headerFont As New Font("Cordia New", 14.0!, FontStyle.Bold)
            Dim x As Integer = e.MarginBounds.Left
            Dim y As Integer = e.MarginBounds.Top
            e.Graphics.DrawString("ใบรับวัตถุดิบเข้าสต็อก", titleFont, Brushes.Black, x, y)
            y += 34
            e.Graphics.DrawString("เลขที่ " & txtDocumentNo.Text.Trim() & "    วันที่ " & dtpReceiveDate.Value.ToString("dd/MM/yyyy HH:mm") & "    ผู้ขาย " & txtSupplier.Text.Trim(), bodyFont, Brushes.Black, x, y)
            y += 34
            e.Graphics.DrawString("รหัส", headerFont, Brushes.Black, x, y)
            e.Graphics.DrawString("รายการวัตถุดิบ", headerFont, Brushes.Black, x + 100, y)
            e.Graphics.DrawString("ยี่ห้อ", headerFont, Brushes.Black, x + 430, y)
            e.Graphics.DrawString("หน่วย", headerFont, Brushes.Black, x + 570, y)
            e.Graphics.DrawString("แพ็ก", headerFont, Brushes.Black, x + 650, y)
            e.Graphics.DrawString("จำนวนรับ", headerFont, Brushes.Black, x + 740, y)
            y += 28
            While printRowIndex < gridDetails.Rows.Count AndAlso y < e.MarginBounds.Bottom - 30
                Dim row As DataGridViewRow = gridDetails.Rows(printRowIndex)
                e.Graphics.DrawString(Convert.ToString(row.Cells("MaterialID").Value), bodyFont, Brushes.Black, x, y)
                e.Graphics.DrawString(Convert.ToString(row.Cells("MaterialDetail").Value), bodyFont, Brushes.Black, x + 100, y)
                e.Graphics.DrawString(Convert.ToString(row.Cells("Brand").Value), bodyFont, Brushes.Black, x + 430, y)
                e.Graphics.DrawString(Convert.ToString(row.Cells("UnitUse").Value), bodyFont, Brushes.Black, x + 570, y)
                e.Graphics.DrawString(CellDecimal(row, "PackQty").ToString("N2"), bodyFont, Brushes.Black, x + 650, y)
                e.Graphics.DrawString(CellDecimal(row, "UseQty").ToString("N2"), bodyFont, Brushes.Black, x + 740, y)
                y += 25
                printRowIndex += 1
            End While
            e.HasMorePages = printRowIndex < gridDetails.Rows.Count
            If Not e.HasMorePages Then printRowIndex = 0
        End Using
    End Sub

    Private NotInheritable Class MaterialChoice
        Public ReadOnly MaterialID As String
        Public ReadOnly MaterialDetail As String
        Public ReadOnly Brand As String
        Public ReadOnly UnitUse As String
        Public Sub New(materialID As String, materialDetail As String, brand As String, unitUse As String)
            Me.MaterialID = materialID
            Me.MaterialDetail = materialDetail
            Me.Brand = brand
            Me.UnitUse = unitUse
        End Sub
    End Class

    Private NotInheritable Class ReceiveMaterialPicker
        Inherits Form

        Private ReadOnly txtSearch As New TextBox()
        Private ReadOnly grid As New DataGridView()
        Private ReadOnly btnSelect As New Button()
        Private ReadOnly btnCancel As New Button()
        Private _selected As MaterialChoice
        Public ReadOnly Property Selected As MaterialChoice
            Get
                Return _selected
            End Get
        End Property

        Public Sub New(searchText As String)
            Text = "เลือกวัตถุดิบ"
            StartPosition = FormStartPosition.CenterParent
            ClientSize = New Size(1000, 600)
            Font = New Font("Cordia New", 16.0!)
            txtSearch.Dock = DockStyle.Top
            txtSearch.Height = 45
            txtSearch.Text = searchText
            txtSearch.Margin = New Padding(8)
            grid.Dock = DockStyle.Fill
            grid.AllowUserToAddRows = False
            grid.ReadOnly = True
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grid.RowHeadersVisible = False
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            grid.ColumnHeadersHeight = 38
            grid.Columns.Add("MaterialID", "รหัสวัตถุดิบ")
            grid.Columns.Add("MaterialDetail", "รายการวัตถุดิบ")
            grid.Columns.Add("Brand", "ยี่ห้อ")
            grid.Columns.Add("UnitUse", "หน่วย")
            Dim footer As New FlowLayoutPanel With {.Dock = DockStyle.Bottom, .Height = 58, .FlowDirection = FlowDirection.RightToLeft, .Padding = New Padding(8)}
            StylePickerButton(btnSelect, "เลือก", Color.FromArgb(38, 132, 74))
            StylePickerButton(btnCancel, "ยกเลิก", Color.FromArgb(190, 75, 75))
            footer.Controls.Add(btnCancel)
            footer.Controls.Add(btnSelect)
            Controls.Add(grid)
            Controls.Add(txtSearch)
            Controls.Add(footer)
            AddHandler Load, Sub(sender, e) LoadMaterials()
            AddHandler txtSearch.TextChanged,
                Sub(sender, e)
                    If IsHandleCreated Then LoadMaterials()
                End Sub
            AddHandler btnSelect.Click, Sub(sender, e) SelectCurrent()
            AddHandler btnCancel.Click, Sub(sender, e) DialogResult = DialogResult.Cancel
            AddHandler grid.CellDoubleClick,
                Sub(sender, e)
                    If e.RowIndex >= 0 Then SelectCurrent()
                End Sub
        End Sub

        Private Shared Sub StylePickerButton(button As Button, caption As String, backColor As Color)
            button.Text = caption
            button.Width = 110
            button.Height = 40
            button.FlatStyle = FlatStyle.Flat
            button.FlatAppearance.BorderSize = 0
            button.BackColor = backColor
            button.ForeColor = Color.White
            button.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        End Sub

        Private Sub LoadMaterials()
            Try
                grid.Rows.Clear()
                Using conn As SqlConnection = modVariable.OpenConnection()
                    Const sql As String =
                        "SELECT LTRIM(RTRIM(MaterialID)) AS MaterialID, LTRIM(RTRIM(ISNULL(Material_Detail,N''))) AS MaterialDetail, " &
                        "LTRIM(RTRIM(ISNULL(Brand,N''))) AS Brand, LTRIM(RTRIM(ISNULL(Unit_Use,N''))) AS UnitUse " &
                        "FROM dbo.MaterialDrink WHERE @Search=N'' OR MaterialID LIKE N'%' + @Search + N'%' OR Material_Detail LIKE N'%' + @Search + N'%' OR Brand LIKE N'%' + @Search + N'%' ORDER BY MaterialID"
                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = txtSearch.Text.Trim()
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                grid.Rows.Add(Convert.ToString(reader("MaterialID")).Trim(), Convert.ToString(reader("MaterialDetail")).Trim(), Convert.ToString(reader("Brand")).Trim(), Convert.ToString(reader("UnitUse")).Trim())
                            End While
                        End Using
                    End Using
                End Using
                If grid.Rows.Count > 0 Then grid.Rows(0).Selected = True
            Catch ex As Exception
                MessageBox.Show("โหลดรายการวัตถุดิบไม่สำเร็จ: " & ex.Message, "เลือกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub SelectCurrent()
            If grid.SelectedRows.Count = 0 Then Return
            Dim row As DataGridViewRow = grid.SelectedRows(0)
            _selected = New MaterialChoice(Convert.ToString(row.Cells("MaterialID").Value).Trim(), Convert.ToString(row.Cells("MaterialDetail").Value).Trim(), Convert.ToString(row.Cells("Brand").Value).Trim(), Convert.ToString(row.Cells("UnitUse").Value).Trim())
            DialogResult = DialogResult.OK
        End Sub
    End Class

    Private NotInheritable Class ReceiveHistoryPicker
        Inherits Form

        Private ReadOnly grid As New DataGridView()
        Private ReadOnly btnClose As New Button()

        Public Sub New()
            Text = "รายการรับวัตถุดิบย้อนหลัง"
            StartPosition = FormStartPosition.CenterParent
            ClientSize = New Size(900, 520)
            grid.Dock = DockStyle.Fill
            grid.ReadOnly = True
            grid.AllowUserToAddRows = False
            grid.RowHeadersVisible = False
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            grid.Columns.Add("DocNo", "เลขที่รับ")
            grid.Columns.Add("Date", "วันที่")
            grid.Columns.Add("Purpose", "ประเภท")
            grid.Columns.Add("Supplier", "ผู้ขาย")
            grid.Columns.Add("Lines", "จำนวนรายการ")
            btnClose.Text = "ปิด"
            btnClose.Dock = DockStyle.Bottom
            btnClose.Height = 45
            AddHandler Load, AddressOf LoadHistory
            AddHandler btnClose.Click, Sub(sender, e) Close()
            Controls.Add(grid)
            Controls.Add(btnClose)
        End Sub

        Private Sub LoadHistory(sender As Object, e As EventArgs)
            Try
                Using conn As SqlConnection = modVariable.OpenConnection()
                    Const sql As String =
                        "SELECT t.DocNo, t.[Date], ISNULL(t.Purpose_Receive,0) AS PurposeCode, ISNULL(t.SupplierID,N'') AS SupplierID, COUNT(d.MaterialID) AS Lines " &
                        "FROM dbo.[Transaction] t LEFT JOIN dbo.TransactionDetail d ON d.DocNo=t.DocNo " &
                        "WHERE t.DocType=1 GROUP BY t.DocNo,t.[Date],t.Purpose_Receive,t.SupplierID ORDER BY t.[Date] DESC,t.DocNo DESC"
                    Using cmd As New SqlCommand(sql, conn)
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                grid.Rows.Add(Convert.ToString(reader("DocNo")), Convert.ToDateTime(reader("Date")).ToString("dd/MM/yyyy HH:mm"), PurposeText(Convert.ToInt32(reader("PurposeCode"))), Convert.ToString(reader("SupplierID")), Convert.ToInt32(reader("Lines")).ToString("N0"))
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("โหลดรายการรับย้อนหลังไม่สำเร็จ: " & ex.Message, "รายการรับย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Shared Function PurposeText(code As Integer) As String
            Select Case code
                Case 1 : Return "ซื้อ"
                Case 2 : Return "ยกมา"
                Case 3 : Return "อื่นๆ"
                Case Else : Return code.ToString(CultureInfo.InvariantCulture)
            End Select
        End Function
    End Class
End Class
