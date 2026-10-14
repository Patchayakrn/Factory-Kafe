Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>
''' หน้าจอเบิกวัตถุดิบ ใช้ dbo.[Transaction] และ dbo.TransactionDetail
''' </summary>
Public Class frmMaterialIssue
    Inherits Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents txtDocumentNo As TextBox
    Friend WithEvents dtpIssueDate As DateTimePicker
    Friend WithEvents cboPurpose As ComboBox
    Friend WithEvents txtHeaderRemark As TextBox
    Friend WithEvents txtMaterialID As TextBox
    Friend WithEvents txtMaterialDetail As TextBox
    Friend WithEvents txtUnitUse As TextBox
    Friend WithEvents nudPack As NumericUpDown
    Friend WithEvents gridDetails As DataGridView
    Friend WithEvents lblSummary As Label
    Friend WithEvents btnSelectMaterial As Button
    Friend WithEvents btnAddDetail As Button
    Friend WithEvents btnRemoveDetail As Button
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnClose As Button

    Private selectedMaterial As MaterialChoice
    Private editingExistingDocument As Boolean
    Friend WithEvents root As TableLayoutPanel
    Friend WithEvents title As Label
    Friend WithEvents header As TableLayoutPanel
    Friend WithEvents lblDocument As Label
    Friend WithEvents lblDate As Label
    Friend WithEvents lblPurpose As Label
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
        ConfigureDatePicker(dtpIssueDate)
        ConfigureTextBox(txtHeaderRemark, False)
        ConfigureTextBox(txtMaterialID, False)
        ConfigureTextBox(txtMaterialDetail, True)
        ConfigureTextBox(txtUnitUse, True)

        ConfigureNumber(nudPack)
        StyleButton(btnSelectMaterial, Color.FromArgb(45, 116, 190), 115)
        StyleButton(btnAddDetail, Color.FromArgb(38, 132, 74), 115)
        StyleButton(btnNew, Color.FromArgb(45, 116, 190), 115)
        StyleButton(btnSave, Color.FromArgb(38, 132, 74), 115)
        StyleButton(btnPrint, Color.FromArgb(117, 82, 160), 115)
        StyleButton(btnRemoveDetail, Color.FromArgb(190, 75, 75), 115)
        StyleButton(btnHistory, Color.FromArgb(102, 87, 145), 125)
        StyleButton(btnClose, Color.FromArgb(105, 105, 105), 90)
        AddHandler Load, AddressOf FormLoaded
        AddHandler btnSelectMaterial.Click, AddressOf SelectMaterialClick
        AddHandler txtMaterialID.KeyDown, AddressOf MaterialCodeKeyDown
        AddHandler txtMaterialID.Leave, AddressOf MaterialCodeLeave
        AddHandler btnAddDetail.Click, AddressOf AddDetailClick
        AddHandler btnRemoveDetail.Click, AddressOf RemoveDetailClick
        AddHandler btnHistory.Click, AddressOf HistoryClick
        AddHandler btnNew.Click, AddressOf NewDocumentClick
        AddHandler btnSave.Click, AddressOf SaveDocumentClick
        AddHandler btnPrint.Click, AddressOf PrintDocumentClick
        AddHandler btnClose.Click, Sub(sender, e) Close()
        AddHandler gridDetails.RowsAdded, AddressOf GridChanged
        AddHandler gridDetails.RowsRemoved, AddressOf GridChanged
        AddHandler gridDetails.CellEndEdit, AddressOf MaterialGridCellEndEdit
    End Sub

    Private Sub InitializeComponent()
        Me.txtDocumentNo = New System.Windows.Forms.TextBox()
        Me.dtpIssueDate = New System.Windows.Forms.DateTimePicker()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me.txtHeaderRemark = New System.Windows.Forms.TextBox()
        Me.txtMaterialID = New System.Windows.Forms.TextBox()
        Me.txtMaterialDetail = New System.Windows.Forms.TextBox()
        Me.txtUnitUse = New System.Windows.Forms.TextBox()
        Me.nudPack = New System.Windows.Forms.NumericUpDown()
        Me.gridDetails = New System.Windows.Forms.DataGridView()
        Me.lblSummary = New System.Windows.Forms.Label()
        Me.btnSelectMaterial = New System.Windows.Forms.Button()
        Me.btnAddDetail = New System.Windows.Forms.Button()
        Me.btnRemoveDetail = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.root = New System.Windows.Forms.TableLayoutPanel()
        Me.title = New System.Windows.Forms.Label()
        Me.header = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDocument = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblPurpose = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.detailInput = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.lblPack = New System.Windows.Forms.Label()
        Me.lblUseUnit = New System.Windows.Forms.Label()
        Me.footer = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.nudPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.root.SuspendLayout()
        Me.header.SuspendLayout()
        Me.detailInput.SuspendLayout()
        Me.footer.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.txtDocumentNo.Location = New System.Drawing.Point(131, 11)
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(135, 44)
        Me.txtDocumentNo.TabIndex = 4
        '
        'dtpIssueDate
        '
        Me.dtpIssueDate.Location = New System.Drawing.Point(351, 11)
        Me.dtpIssueDate.Name = "dtpIssueDate"
        Me.dtpIssueDate.Size = New System.Drawing.Size(202, 44)
        Me.dtpIssueDate.TabIndex = 5
        '
        'cboPurpose
        '
        Me.cboPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Items.AddRange(New Object() {"ใช้", "เสียหาย/หมดอายุ", "ตัดเทส"})
        Me.cboPurpose.Location = New System.Drawing.Point(663, 11)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.Size = New System.Drawing.Size(193, 45)
        Me.cboPurpose.TabIndex = 6
        '
        'txtHeaderRemark
        '
        Me.txtHeaderRemark.Location = New System.Drawing.Point(968, 11)
        Me.txtHeaderRemark.Name = "txtHeaderRemark"
        Me.txtHeaderRemark.Size = New System.Drawing.Size(95, 44)
        Me.txtHeaderRemark.TabIndex = 7
        '
        'txtMaterialID
        '
        Me.txtMaterialID.Location = New System.Drawing.Point(91, 7)
        Me.txtMaterialID.Name = "txtMaterialID"
        Me.txtMaterialID.Size = New System.Drawing.Size(100, 44)
        Me.txtMaterialID.TabIndex = 4
        '
        'txtMaterialDetail
        '
        Me.txtMaterialDetail.Location = New System.Drawing.Point(353, 7)
        Me.txtMaterialDetail.Name = "txtMaterialDetail"
        Me.txtMaterialDetail.Size = New System.Drawing.Size(100, 44)
        Me.txtMaterialDetail.TabIndex = 6
        '
        'txtUnitUse
        '
        Me.txtUnitUse.Location = New System.Drawing.Point(876, 7)
        Me.txtUnitUse.Name = "txtUnitUse"
        Me.txtUnitUse.Size = New System.Drawing.Size(99, 44)
        Me.txtUnitUse.TabIndex = 8
        '
        'nudPack
        '
        Me.nudPack.Location = New System.Drawing.Point(702, 7)
        Me.nudPack.Name = "nudPack"
        Me.nudPack.Size = New System.Drawing.Size(90, 44)
        Me.nudPack.TabIndex = 7
        '
        'gridDetails
        '
        Me.gridDetails.ColumnHeadersHeight = 29
        Me.gridDetails.Location = New System.Drawing.Point(17, 227)
        Me.gridDetails.Name = "gridDetails"
        Me.gridDetails.RowHeadersWidth = 51
        Me.gridDetails.Size = New System.Drawing.Size(1216, 412)
        Me.gridDetails.TabIndex = 3
        '
        'lblSummary
        '
        Me.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSummary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.lblSummary.Location = New System.Drawing.Point(11, 8)
        Me.lblSummary.Name = "lblSummary"
        Me.lblSummary.Size = New System.Drawing.Size(602, 42)
        Me.lblSummary.TabIndex = 0
        Me.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSelectMaterial
        '
        Me.btnSelectMaterial.Location = New System.Drawing.Point(238, 7)
        Me.btnSelectMaterial.Name = "btnSelectMaterial"
        Me.btnSelectMaterial.Size = New System.Drawing.Size(72, 23)
        Me.btnSelectMaterial.TabIndex = 5
        Me.btnSelectMaterial.Text = "เลือก"
        '
        'btnAddDetail
        '
        Me.btnAddDetail.Location = New System.Drawing.Point(981, 7)
        Me.btnAddDetail.Name = "btnAddDetail"
        Me.btnAddDetail.Size = New System.Drawing.Size(75, 23)
        Me.btnAddDetail.TabIndex = 11
        Me.btnAddDetail.Text = "เพิ่มรายการ"
        '
        'btnRemoveDetail
        '
        Me.btnRemoveDetail.Location = New System.Drawing.Point(1096, 7)
        Me.btnRemoveDetail.Name = "btnRemoveDetail"
        Me.btnRemoveDetail.Size = New System.Drawing.Size(75, 36)
        Me.btnRemoveDetail.TabIndex = 2
        Me.btnRemoveDetail.Text = "ลบแถว"
        '
        'btnHistory
        '
        Me.btnHistory.Location = New System.Drawing.Point(619, 11)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(100, 36)
        Me.btnHistory.TabIndex = 6
        Me.btnHistory.Text = "รายการย้อนหลัง"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(744, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(101, 36)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "เอกสารใหม่"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(869, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 36)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "บันทึก"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(994, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 36)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "พิมพ์เอกสาร"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1119, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 36)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "ปิด"
        '
        'root
        '
        Me.root.ColumnCount = 1
        Me.root.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.root.Controls.Add(Me.title, 0, 0)
        Me.root.Controls.Add(Me.header, 0, 1)
        Me.root.Controls.Add(Me.detailInput, 0, 2)
        Me.root.Controls.Add(Me.footer, 0, 4)
        Me.root.Controls.Add(Me.gridDetails, 0, 3)
        Me.root.Dock = System.Windows.Forms.DockStyle.Fill
        Me.root.Location = New System.Drawing.Point(0, 0)
        Me.root.Name = "root"
        Me.root.Padding = New System.Windows.Forms.Padding(14)
        Me.root.RowCount = 5
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.root.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.root.Size = New System.Drawing.Size(1250, 760)
        Me.root.TabIndex = 0
        '
        'title
        '
        Me.title.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.title.Font = New System.Drawing.Font("Cordia New", 24.0!, System.Drawing.FontStyle.Bold)
        Me.title.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.title.Location = New System.Drawing.Point(17, 14)
        Me.title.Name = "title"
        Me.title.Padding = New System.Windows.Forms.Padding(14, 0, 0, 0)
        Me.title.Size = New System.Drawing.Size(1216, 52)
        Me.title.TabIndex = 0
        Me.title.Text = "เบิกวัตถุดิบ"
        Me.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'header
        '
        Me.header.ColumnCount = 8
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.header.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.header.Controls.Add(Me.lblDocument, 0, 0)
        Me.header.Controls.Add(Me.lblDate, 2, 0)
        Me.header.Controls.Add(Me.lblPurpose, 4, 0)
        Me.header.Controls.Add(Me.lblRemark, 6, 0)
        Me.header.Controls.Add(Me.txtDocumentNo, 1, 0)
        Me.header.Controls.Add(Me.dtpIssueDate, 3, 0)
        Me.header.Controls.Add(Me.cboPurpose, 5, 0)
        Me.header.Controls.Add(Me.txtHeaderRemark, 7, 0)
        Me.header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.header.Location = New System.Drawing.Point(17, 69)
        Me.header.Name = "header"
        Me.header.Padding = New System.Windows.Forms.Padding(8, 8, 8, 4)
        Me.header.RowCount = 1
        Me.header.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.header.Size = New System.Drawing.Size(1216, 76)
        Me.header.TabIndex = 1
        '
        'lblDocument
        '
        Me.lblDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDocument.Location = New System.Drawing.Point(11, 8)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(114, 64)
        Me.lblDocument.TabIndex = 0
        Me.lblDocument.Text = "เลขที่เอกสาร"
        Me.lblDocument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDate
        '
        Me.lblDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDate.Location = New System.Drawing.Point(272, 8)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(73, 64)
        Me.lblDate.TabIndex = 1
        Me.lblDate.Text = "วันที่"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPurpose
        '
        Me.lblPurpose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPurpose.Location = New System.Drawing.Point(566, 8)
        Me.lblPurpose.Name = "lblPurpose"
        Me.lblPurpose.Size = New System.Drawing.Size(91, 64)
        Me.lblPurpose.TabIndex = 2
        Me.lblPurpose.Text = "เบิกเพื่อ"
        Me.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRemark
        '
        Me.lblRemark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRemark.Location = New System.Drawing.Point(862, 8)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(100, 64)
        Me.lblRemark.TabIndex = 3
        Me.lblRemark.Text = "หมายเหตุ"
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'detailInput
        '
        Me.detailInput.ColumnCount = 10
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.detailInput.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.detailInput.Controls.Add(Me.lblMaterial, 0, 0)
        Me.detailInput.Controls.Add(Me.lblPack, 4, 0)
        Me.detailInput.Controls.Add(Me.lblUseUnit, 6, 0)
        Me.detailInput.Controls.Add(Me.txtMaterialID, 1, 0)
        Me.detailInput.Controls.Add(Me.btnSelectMaterial, 2, 0)
        Me.detailInput.Controls.Add(Me.txtMaterialDetail, 3, 0)
        Me.detailInput.Controls.Add(Me.nudPack, 5, 0)
        Me.detailInput.Controls.Add(Me.txtUnitUse, 7, 0)
        Me.detailInput.Controls.Add(Me.btnAddDetail, 8, 0)
        Me.detailInput.Controls.Add(Me.btnRemoveDetail, 9, 0)
        Me.detailInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.detailInput.Location = New System.Drawing.Point(17, 151)
        Me.detailInput.Name = "detailInput"
        Me.detailInput.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
        Me.detailInput.RowCount = 1
        Me.detailInput.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.detailInput.Size = New System.Drawing.Size(1216, 70)
        Me.detailInput.TabIndex = 2
        '
        'lblMaterial
        '
        Me.lblMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMaterial.Location = New System.Drawing.Point(11, 4)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(74, 58)
        Me.lblMaterial.TabIndex = 0
        Me.lblMaterial.Text = "วัตถุดิบ"
        Me.lblMaterial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPack
        '
        Me.lblPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPack.Location = New System.Drawing.Point(640, 4)
        Me.lblPack.Name = "lblPack"
        Me.lblPack.Size = New System.Drawing.Size(56, 58)
        Me.lblPack.TabIndex = 1
        Me.lblPack.Text = "แพ็ก"
        Me.lblPack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUseUnit
        '
        Me.lblUseUnit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblUseUnit.Location = New System.Drawing.Point(798, 4)
        Me.lblUseUnit.Name = "lblUseUnit"
        Me.lblUseUnit.Size = New System.Drawing.Size(72, 58)
        Me.lblUseUnit.TabIndex = 2
        Me.lblUseUnit.Text = "หน่วยใช้"
        Me.lblUseUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'footer
        '
        Me.footer.ColumnCount = 6
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.footer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.footer.Controls.Add(Me.lblSummary, 0, 0)
        Me.footer.Controls.Add(Me.btnHistory, 1, 0)
        Me.footer.Controls.Add(Me.btnNew, 2, 0)
        Me.footer.Controls.Add(Me.btnSave, 3, 0)
        Me.footer.Controls.Add(Me.btnPrint, 4, 0)
        Me.footer.Controls.Add(Me.btnClose, 5, 0)
        Me.footer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.footer.Location = New System.Drawing.Point(17, 685)
        Me.footer.Name = "footer"
        Me.footer.Padding = New System.Windows.Forms.Padding(8, 8, 0, 8)
        Me.footer.RowCount = 1
        Me.footer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.footer.Size = New System.Drawing.Size(1216, 58)
        Me.footer.TabIndex = 4
        '
        'frmMaterialIssue
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1250, 760)
        Me.Controls.Add(Me.root)
        Me.Font = New System.Drawing.Font("Cordia New", 16.0!)
        Me.MinimumSize = New System.Drawing.Size(1100, 650)
        Me.Name = "frmMaterialIssue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เบิกวัตถุดิบ"
        CType(Me.nudPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.root.ResumeLayout(False)
        Me.header.ResumeLayout(False)
        Me.header.PerformLayout()
        Me.detailInput.ResumeLayout(False)
        Me.detailInput.PerformLayout()
        Me.footer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private Sub ConfigureGrid()
        gridDetails.Dock = DockStyle.Fill
        gridDetails.AllowUserToAddRows = False
        gridDetails.AllowUserToDeleteRows = False
        gridDetails.BackgroundColor = Color.White
        gridDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        gridDetails.MultiSelect = True
        gridDetails.RowHeadersVisible = False
        gridDetails.RowTemplate.Height = 30
        gridDetails.AutoGenerateColumns = False
        gridDetails.ColumnHeadersHeight = 38
        gridDetails.EnableHeadersVisualStyles = False
        gridDetails.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
            .BackColor = Color.Black, .ForeColor = Color.White,
            .Font = New Font("Cordia New", 14.0!, FontStyle.Bold),
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        }
        gridDetails.DefaultCellStyle.Font = New Font("Cordia New", 14.0!, FontStyle.Regular)
        gridDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 250)
        gridDetails.DefaultCellStyle.SelectionForeColor = Color.Black
        AddGridColumn("LineNo", "", 40)
        gridDetails.Columns("LineNo").Visible = False
        AddGridColumn("MaterialID", "รหัส", 100)
        AddGridColumn("MaterialDetail", "ชื่อวัตถุดิบ", 430)
        AddGridColumn("Brand", "ยี่ห้อ", 150)
        AddGridNumberColumn("PackQty", "Pack", 80, True)
        AddGridNumberColumn("UseQty", "จำนวน", 105, False)
        AddGridColumn("UnitUse", "หน่วย", 80)
        AddGridNumberColumn("UnitPerPackage", "", 80, True)
        gridDetails.Columns("UnitPerPackage").Visible = False
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
            .Format = "N0", .Alignment = DataGridViewContentAlignment.MiddleRight,
            .BackColor = If(readOnlyValue, Color.White, Color.FromArgb(255, 250, 215))
        }
        gridDetails.Columns.Add(column)
    End Sub

    Private Sub AddLabel(parent As TableLayoutPanel, label As Label, text As String, column As Integer)
        label.Text = text
        label.Dock = DockStyle.Fill
        label.TextAlign = ContentAlignment.MiddleLeft
        label.Margin = New Padding(3, 0, 3, 0)
        parent.Controls.Add(label, column, If(parent.RowCount = 1, 0, 0))
    End Sub

    Private Sub ConfigureTextBox(textBox As TextBox, readOnlyValue As Boolean)
        textBox.Dock = DockStyle.Fill
        textBox.ReadOnly = readOnlyValue
        textBox.Font = New Font("Cordia New", 16.0!)
        textBox.Margin = New Padding(3, 3, 3, 3)
    End Sub

    Private Sub ConfigureDatePicker(picker As DateTimePicker)
        picker.Dock = DockStyle.Fill
        picker.Format = DateTimePickerFormat.Custom
        picker.CustomFormat = "dd/MM/yyyy HH:mm"
        picker.ShowUpDown = True
        picker.Margin = New Padding(3, 3, 3, 3)
    End Sub

    Private Sub ConfigureNumber(numberBox As NumericUpDown)
        numberBox.Dock = DockStyle.Fill
        numberBox.DecimalPlaces = 4
        numberBox.Maximum = 100000000D
        numberBox.Minimum = 0D
        numberBox.Increment = 1D
        numberBox.ThousandsSeparator = True
        numberBox.Font = New Font("Cordia New", 16.0!)
        numberBox.Margin = New Padding(3, 3, 3, 3)
    End Sub

    Private Sub StyleButton(button As Button, backColor As Color, width As Integer)
        button.Dock = DockStyle.None
        button.Anchor = AnchorStyles.None
        button.Width = width
        button.Height = 42
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.BackColor = backColor
        button.ForeColor = Color.White
        button.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        button.Margin = New Padding(4)
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        If MdiParent IsNot Nothing Then WindowState = FormWindowState.Maximized
        Try
            DbSchemaChecker.EnsureSupportTables()
            NewDocumentClick(btnNew, EventArgs.Empty)
        Catch ex As Exception
            MessageBox.Show("เปิดหน้าจอเบิกวัตถุดิบไม่สำเร็จ: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewDocumentClick(sender As Object, e As EventArgs)
        editingExistingDocument = False
        gridDetails.Rows.Clear()
        dtpIssueDate.Value = DateTime.Now
        If cboPurpose.Items.Count > 0 Then cboPurpose.SelectedIndex = 0
        txtHeaderRemark.Clear()
        ClearMaterialInput()
        GenerateDocumentNo()
        UpdateSummary()
        btnSave.Text = "บันทึก"
        btnSave.Enabled = True
        btnPrint.Enabled = False
    End Sub

    Private Sub HistoryClick(sender As Object, e As EventArgs)
        Using picker As New MaterialIssueHistoryPicker()
            If picker.ShowDialog(Me) = DialogResult.OK AndAlso picker.Selected IsNot Nothing Then
                LoadHistoricalDocument(picker.Selected.DocNo)
            End If
        End Using
    End Sub

    Private Sub LoadHistoricalDocument(docNo As String)
        If String.IsNullOrWhiteSpace(docNo) Then Return
        Try
            Cursor = Cursors.WaitCursor
            gridDetails.Rows.Clear()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const headerSql As String =
                    "SELECT [Date], ISNULL(Purpose_Pay,0) AS PurposePay, ISNULL(Remark,N'') AS Remark " &
                    "FROM dbo.[Transaction] WHERE DocNo=@DocNo AND DocType=2"
                Using header As New SqlCommand(headerSql, conn)
                    header.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = docNo
                    Using reader As SqlDataReader = header.ExecuteReader()
                        If Not reader.Read() Then
                            MessageBox.Show("ไม่พบเอกสารเบิกเลขที่ " & docNo, "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                        txtDocumentNo.Text = docNo
                        If Not IsDBNull(reader("Date")) Then dtpIssueDate.Value = Convert.ToDateTime(reader("Date"))
                        Dim purposeCode As Integer = Convert.ToInt32(reader("PurposePay"), CultureInfo.InvariantCulture)
                        cboPurpose.SelectedIndex = If(purposeCode >= 1 AndAlso purposeCode <= cboPurpose.Items.Count, purposeCode - 1, -1)
                        txtHeaderRemark.Text = Convert.ToString(reader("Remark"))
                    End Using
                End Using

                Const detailSql As String =
                    "SELECT LTRIM(RTRIM(d.MaterialID)) AS MaterialID, " &
                    "LTRIM(RTRIM(ISNULL(m.Material_Detail,N''))) AS MaterialDetail, " &
                    "LTRIM(RTRIM(ISNULL(m.Brand,N''))) AS Brand, " &
                    "LTRIM(RTRIM(ISNULL(m.Unit_Use,N''))) AS UnitUse, " &
                    "ISNULL(m.Unit_per_Package,0) AS UnitPerPackage, " &
                    "d.Pay_Qty_Pack AS PackQty, d.Pay_Qty_Use_Unit AS UseQty " &
                    "FROM dbo.TransactionDetail d " &
                    "LEFT JOIN dbo.MaterialDrink m ON LTRIM(RTRIM(m.MaterialID))=LTRIM(RTRIM(d.MaterialID)) " &
                    "WHERE d.DocNo=@DocNo ORDER BY d.MaterialID"
                Using detail As New SqlCommand(detailSql, conn)
                    detail.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = docNo
                    Using reader As SqlDataReader = detail.ExecuteReader()
                        While reader.Read()
                            Dim index As Integer = gridDetails.Rows.Add()
                            Dim row As DataGridViewRow = gridDetails.Rows(index)
                            row.Cells("LineNo").Value = index + 1
                            row.Cells("MaterialID").Value = Convert.ToString(reader("MaterialID")).Trim()
                            row.Cells("MaterialDetail").Value = Convert.ToString(reader("MaterialDetail")).Trim()
                            row.Cells("Brand").Value = Convert.ToString(reader("Brand")).Trim()
                            row.Cells("UnitUse").Value = Convert.ToString(reader("UnitUse")).Trim()
                            row.Cells("PackQty").Value = Convert.ToDecimal(reader("PackQty"), CultureInfo.InvariantCulture)
                            row.Cells("UseQty").Value = Convert.ToDecimal(reader("UseQty"), CultureInfo.InvariantCulture)
                            row.Cells("UnitPerPackage").Value = Convert.ToDecimal(reader("UnitPerPackage"), CultureInfo.InvariantCulture)
                        End While
                    End Using
                End Using
            End Using
            editingExistingDocument = True
            btnSave.Text = "บันทึกแก้ไข"
            btnSave.Enabled = True
            btnPrint.Enabled = True
            ClearMaterialInput()
            UpdateSummary()
        Catch ex As Exception
            MessageBox.Show("โหลดเอกสารย้อนหลังไม่สำเร็จ: " & ex.Message, "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadMaterialRows()
        Try
            Cursor = Cursors.WaitCursor
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT LTRIM(RTRIM(MaterialID)) AS MaterialID, " &
                    "LTRIM(RTRIM(ISNULL(Material_Detail,N''))) AS MaterialDetail, " &
                    "LTRIM(RTRIM(ISNULL(Brand,N''))) AS Brand, " &
                    "LTRIM(RTRIM(ISNULL(Unit_Use,N''))) AS UnitUse, " &
                    "ISNULL(Unit_per_Package,0) AS UnitPerPackage " &
                    "FROM dbo.MaterialDrink ORDER BY MaterialID"
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim index As Integer = gridDetails.Rows.Add()
                            Dim row As DataGridViewRow = gridDetails.Rows(index)
                            row.Cells("LineNo").Value = index + 1
                            row.Cells("MaterialID").Value = Convert.ToString(reader("MaterialID")).Trim()
                            row.Cells("MaterialDetail").Value = Convert.ToString(reader("MaterialDetail")).Trim()
                            row.Cells("Brand").Value = Convert.ToString(reader("Brand")).Trim()
                            row.Cells("UnitUse").Value = Convert.ToString(reader("UnitUse")).Trim()
                            row.Cells("PackQty").Value = 0D
                            row.Cells("UseQty").Value = 0D
                            row.Cells("UnitPerPackage").Value = Convert.ToDecimal(reader("UnitPerPackage"), CultureInfo.InvariantCulture)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดรายการวัตถุดิบไม่สำเร็จ: " & ex.Message, "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GenerateDocumentNo()
        'รูปแบบเลขที่เอกสาร: FKP + ปี พ.ศ. 2 หลัก + เดือน 2 หลัก + วันที่ 2 หลัก + เลขรัน 2 หลัก
        'ตัวอย่างวันที่ 09/10/2569: FKP69100901
        Dim issueDate As DateTime = dtpIssueDate.Value
        Dim buddhistYear2 As String = (issueDate.Year + 543).ToString("0000", CultureInfo.InvariantCulture).Substring(2, 2)
        Dim prefix As String = "FKP" & buddhistYear2 & issueDate.ToString("MMdd", CultureInfo.InvariantCulture)
        Dim nextNumber As Integer = 1
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT MAX(DocNo) FROM dbo.[Transaction] WHERE DocNo LIKE @Prefix + N'%'", conn)
                    cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = prefix
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Dim existing As String = Convert.ToString(value).Trim()
                        If existing.Length > prefix.Length Then Integer.TryParse(existing.Substring(prefix.Length), nextNumber)
                        nextNumber += 1
                    End If
                End Using
            End Using
        Catch
        End Try
        txtDocumentNo.Text = prefix & nextNumber.ToString("00", CultureInfo.InvariantCulture)
    End Sub

    Private Sub SelectMaterialClick(sender As Object, e As EventArgs)
        Using picker As New MaterialPickerForm(txtMaterialID.Text.Trim())
            If picker.ShowDialog(Me) = DialogResult.OK Then
                SetMaterial(picker.Selected)
            End If
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
                    "LTRIM(RTRIM(ISNULL(Unit_Use,N''))) AS UnitUse, " &
                    "ISNULL(Unit_per_Package,0) AS UnitPerPackage " &
                    "FROM dbo.MaterialDrink WHERE LTRIM(RTRIM(MaterialID))=@MaterialID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = code
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            SetMaterial(New MaterialChoice(
                                Convert.ToString(reader("MaterialID")).Trim(),
                                Convert.ToString(reader("MaterialDetail")).Trim(),
                                Convert.ToString(reader("Brand")).Trim(),
                                Convert.ToString(reader("UnitUse")).Trim(),
                                Convert.ToDecimal(reader("UnitPerPackage"), CultureInfo.InvariantCulture)))
                        Else
                            selectedMaterial = Nothing
                            txtMaterialDetail.Clear()
                            txtUnitUse.Clear()
                            MessageBox.Show("ไม่พบรหัสวัตถุดิบนี้", "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ค้นหาวัตถุดิบไม่สำเร็จ: " & ex.Message, "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            loading = False
        End Try
    End Sub

    Private Sub SetMaterial(material As MaterialChoice)
        selectedMaterial = material
        If material Is Nothing Then Return
        txtMaterialID.Text = material.MaterialID
        txtMaterialDetail.Text = material.MaterialDetail
        txtUnitUse.Text = material.UnitUse
    End Sub

    Private Shared Function FormatMaterialDetail(materialDetail As String, brand As String) As String
        Dim detail As String = If(materialDetail, String.Empty).Trim()
        Dim brandText As String = If(brand, String.Empty).Trim()
        If brandText = String.Empty Then Return detail
        If detail = String.Empty Then Return brandText
        Return detail & "/" & brandText
    End Function

    Private Sub AddDetailClick(sender As Object, e As EventArgs)
        If selectedMaterial Is Nothing OrElse Not String.Equals(selectedMaterial.MaterialID, txtMaterialID.Text.Trim(), StringComparison.OrdinalIgnoreCase) Then
            LoadMaterialByCode()
        End If
        If selectedMaterial Is Nothing Then Return
        If nudPack.Value <= 0D Then
            MessageBox.Show("กรุณาระบุจำนวนแพ็ก", "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        For Each row As DataGridViewRow In gridDetails.Rows
            If String.Equals(Convert.ToString(row.Cells("MaterialID").Value).Trim(), selectedMaterial.MaterialID, StringComparison.OrdinalIgnoreCase) Then
                MessageBox.Show("วัตถุดิบนี้ถูกเพิ่มแล้ว กรุณาแก้จำนวนในรายการเดิมหรือใช้ลบแถวก่อน", "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Next

        ' หน้าจอนี้เบิกเป็นหน่วย: ให้ค่าในช่อง Pack ถูกบันทึกลงคอลัมน์หน่วย
        ' และคอลัมน์ Pack ต้องเป็น 0 ตามรูปแบบรายงาน
        Dim useQty As Decimal = nudPack.Value
        Dim index As Integer = gridDetails.Rows.Add()
        Dim rowAdded As DataGridViewRow = gridDetails.Rows(index)
        rowAdded.Cells("LineNo").Value = index + 1
        rowAdded.Cells("MaterialID").Value = selectedMaterial.MaterialID
        rowAdded.Cells("MaterialDetail").Value = selectedMaterial.MaterialName
        rowAdded.Cells("Brand").Value = selectedMaterial.Brand
        rowAdded.Cells("UnitUse").Value = selectedMaterial.UnitUse
        rowAdded.Cells("PackQty").Value = 0D
        rowAdded.Cells("UseQty").Value = useQty

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

    Private Sub GridChanged(sender As Object, e As DataGridViewRowsAddedEventArgs)
        UpdateSummary()
    End Sub

    Private Sub GridChanged(sender As Object, e As DataGridViewRowsRemovedEventArgs)
        UpdateSummary()
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
        txtUnitUse.Clear()

        nudPack.Value = 0D
    End Sub

    Private Sub UpdateSummary()
        Dim pack As Decimal = 0D
        Dim useQty As Decimal = 0D
        Dim selectedCount As Integer = 0
        For Each row As DataGridViewRow In gridDetails.Rows
            Dim rowPack As Decimal = CellDecimal(row, "PackQty")
            Dim rowUseQty As Decimal = CellDecimal(row, "UseQty")
            If rowPack <> 0D OrElse rowUseQty <> 0D Then selectedCount += 1
            pack += rowPack
            useQty += rowUseQty
        Next
        lblSummary.Text = "เลือก " & selectedCount.ToString("N0") & " รายการ  |  เบิก " & pack.ToString("N2") & " แพ็ก  |  " & useQty.ToString("N2") & " หน่วย"
    End Sub

    Private Sub MaterialGridCellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        If e.ColumnIndex = gridDetails.Columns("PackQty").Index Then
            Dim row As DataGridViewRow = gridDetails.Rows(e.RowIndex)
            Dim packQty As Decimal = CellDecimal(row, "PackQty")
            Dim unitPerPackage As Decimal = CellDecimal(row, "UnitPerPackage")
            row.Cells("UseQty").Value = packQty * unitPerPackage
        End If
        UpdateSummary()
    End Sub

    Private Function CellDecimal(row As DataGridViewRow, columnName As String) As Decimal
        If row Is Nothing OrElse row.Cells(columnName).Value Is Nothing Then Return 0D
        Dim result As Decimal
        If Decimal.TryParse(Convert.ToString(row.Cells(columnName).Value), NumberStyles.Any, CultureInfo.CurrentCulture, result) Then Return result
        Return 0D
    End Function

    Private Sub SaveDocumentClick(sender As Object, e As EventArgs)
        Dim selectedCount As Integer = 0
        For Each row As DataGridViewRow In gridDetails.Rows
            If CellDecimal(row, "PackQty") <> 0D OrElse CellDecimal(row, "UseQty") <> 0D Then selectedCount += 1
        Next
        If selectedCount = 0 Then
            MessageBox.Show("กรุณาระบุจำนวนเบิกอย่างน้อย 1 รายการ", "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim purposeCode As Integer = cboPurpose.SelectedIndex + 1
        If purposeCode < 1 OrElse purposeCode > 3 Then
            MessageBox.Show("กรุณาเลือกประเภทการเบิก", "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Dim headerSql As String
                    If editingExistingDocument Then
                        headerSql =
                            "UPDATE dbo.[Transaction] SET [Date]=@Date, Purpose_Pay=@Purpose, Remark=@Remark " &
                            "WHERE DocNo=@DocNo AND DocType=2"
                    Else
                        headerSql =
                            "INSERT INTO dbo.[Transaction](DocNo,[Date],DocType,Purpose_Receive,Purpose_Pay,Remark) " &
                            "VALUES(@DocNo,@Date,2,0,@Purpose,@Remark)"
                    End If
                    Using header As New SqlCommand(headerSql, conn, tr)
                        header.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = txtDocumentNo.Text.Trim()
                        header.Parameters.Add("@Date", SqlDbType.DateTime2).Value = dtpIssueDate.Value
                        header.Parameters.Add("@Purpose", SqlDbType.Int).Value = purposeCode
                        header.Parameters.Add("@Remark", SqlDbType.NVarChar, 500).Value = If(txtHeaderRemark.Text.Trim() = "", CType(DBNull.Value, Object), txtHeaderRemark.Text.Trim())
                        header.ExecuteNonQuery()
                    End Using

                    If editingExistingDocument Then
                        Using deleteDetails As New SqlCommand("DELETE FROM dbo.TransactionDetail WHERE DocNo=@DocNo", conn, tr)
                            deleteDetails.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = txtDocumentNo.Text.Trim()
                            deleteDetails.ExecuteNonQuery()
                        End Using
                    End If

                    Const detailSql As String =
                        "INSERT INTO dbo.TransactionDetail(DocNo,MaterialID,Rec_Qty_Pack,Rec_Qty_Use_Unit,Pay_Qty_Pack,Pay_Qty_Use_Unit) " &
                        "VALUES(@DocNo,@MaterialID,0,0,@PackQty,@UseQty)"
                    For Each row As DataGridViewRow In gridDetails.Rows
                        Dim packQty As Decimal = CellDecimal(row, "PackQty")
                        Dim useQty As Decimal = CellDecimal(row, "UseQty")
                        If packQty = 0D AndAlso useQty = 0D Then Continue For
                        Using detail As New SqlCommand(detailSql, conn, tr)
                            detail.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = txtDocumentNo.Text.Trim()
                            detail.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = Convert.ToString(row.Cells("MaterialID").Value).Trim()
                            AddDecimal(detail, "@PackQty", packQty)
                            AddDecimal(detail, "@UseQty", useQty)
                            detail.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using

            MessageBox.Show(If(editingExistingDocument, "แก้ไขใบเบิกวัตถุดิบเรียบร้อยแล้ว เลขที่ ", "บันทึกใบเบิกวัตถุดิบเรียบร้อยแล้ว เลขที่ ") & txtDocumentNo.Text.Trim(), "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'คงรายการเดิมไว้เพื่อให้กดพิมพ์เอกสารได้ หลังจากบันทึกสำเร็จ
            btnSave.Enabled = False
            btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show("บันทึกใบเบิกวัตถุดิบไม่สำเร็จ: " & ex.Message, "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddDecimal(command As SqlCommand, name As String, value As Decimal)
        Dim parameter As SqlParameter = command.Parameters.Add(name, SqlDbType.Decimal)
        parameter.Precision = 18
        parameter.Scale = 4
        parameter.Value = value
    End Sub

    Private Sub PrintDocumentClick(sender As Object, e As EventArgs)
        Try
            Dim reportTable As DataTable = BuildMaterialIssueReportTable()
            If reportTable.Rows.Count = 0 Then
                MessageBox.Show("กรุณาเพิ่มรายการเบิกก่อนพิมพ์เอกสาร", "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Using viewer As New frmReportViewer()
                viewer.LoadMaterialIssueReport(reportTable)
                viewer.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show("พิมพ์เอกสารเบิกไม่สำเร็จ: " & ex.Message, "เบิกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function BuildMaterialIssueReportTable() As DataTable
        Dim result As New DataTable("MaterialIssue")
        result.Columns.Add("CompanyName", GetType(String))
        result.Columns.Add("DocNo", GetType(String))
        result.Columns.Add("IssueDateText", GetType(String))
        result.Columns.Add("PurposeText", GetType(String))
        result.Columns.Add("HeaderRemark", GetType(String))
        result.Columns.Add("LineNo", GetType(Integer))
        result.Columns.Add("MaterialID", GetType(String))
        result.Columns.Add("ItemText", GetType(String))
        result.Columns.Add("Quantity", GetType(Decimal))
        result.Columns.Add("UnitUse", GetType(String))

        Dim purposeText As String = ""
        If cboPurpose.SelectedItem IsNot Nothing Then purposeText = cboPurpose.SelectedItem.ToString()
        Dim issueDateText As String = dtpIssueDate.Value.ToString("dd/MM/", CultureInfo.InvariantCulture) &
                                      (dtpIssueDate.Value.Year + 543).ToString("0000", CultureInfo.InvariantCulture).Substring(2, 2)
        Dim lineNo As Integer = 1
        For Each row As DataGridViewRow In gridDetails.Rows
            Dim quantity As Decimal = CellDecimal(row, "UseQty")
            If quantity = 0D AndAlso CellDecimal(row, "PackQty") = 0D Then Continue For
            Dim item As DataRow = result.NewRow()
            item("CompanyName") = "บริษัท สมายล์ ฮาร์ท ฟู้ดส์ จำกัด"
            item("DocNo") = txtDocumentNo.Text.Trim()
            item("IssueDateText") = issueDateText
            item("PurposeText") = purposeText
            item("HeaderRemark") = txtHeaderRemark.Text.Trim()
            item("LineNo") = lineNo
            item("MaterialID") = Convert.ToString(row.Cells("MaterialID").Value).Trim()
            Dim materialName As String = Convert.ToString(row.Cells("MaterialDetail").Value).Trim()
            Dim brand As String = Convert.ToString(row.Cells("Brand").Value).Trim()
            item("ItemText") = If(brand = "", materialName, materialName & " / " & brand)
            item("Quantity") = quantity
            item("UnitUse") = Convert.ToString(row.Cells("UnitUse").Value).Trim()
            result.Rows.Add(item)
            lineNo += 1
        Next
        Return result
    End Function

    Private NotInheritable Class PurposeChoice
        Public ReadOnly Code As Integer
        Private ReadOnly Caption As String
        Public Sub New(code As Integer, caption As String)
            Me.Code = code
            Me.Caption = caption
        End Sub
        Public Overrides Function ToString() As String
            Return Code.ToString(CultureInfo.InvariantCulture) & " - " & Caption
        End Function
    End Class

    Private NotInheritable Class MaterialChoice
        Public ReadOnly MaterialID As String
        Public ReadOnly MaterialName As String
        Public ReadOnly Brand As String
        Public ReadOnly MaterialDetail As String
        Public ReadOnly UnitUse As String
        Public ReadOnly UnitPerPackage As Decimal
        Public Sub New(materialID As String, materialName As String, brand As String, unitUse As String, unitPerPackage As Decimal)
            Me.MaterialID = materialID
            Me.MaterialName = If(materialName, String.Empty).Trim()
            Me.Brand = If(brand, String.Empty).Trim()
            Me.MaterialDetail = Me.MaterialName
            If Me.Brand <> String.Empty Then Me.MaterialDetail &= "/" & Me.Brand
            Me.UnitUse = unitUse
            Me.UnitPerPackage = unitPerPackage
        End Sub
    End Class

    Private NotInheritable Class IssueDocumentChoice
        Public ReadOnly DocNo As String
        Public ReadOnly IssueDate As DateTime
        Public ReadOnly PurposeCode As Integer
        Public ReadOnly Remark As String
        Public Sub New(docNo As String, issueDate As DateTime, purposeCode As Integer, remark As String)
            Me.DocNo = docNo
            Me.IssueDate = issueDate
            Me.PurposeCode = purposeCode
            Me.Remark = If(remark, String.Empty)
        End Sub
    End Class

    Private NotInheritable Class MaterialIssueHistoryPicker
        Inherits Form

        Private ReadOnly txtSearch As New TextBox()
        Private ReadOnly grid As New DataGridView()
        Private ReadOnly btnSelect As New Button()
        Private ReadOnly btnCancel As New Button()
        Private _Selected As IssueDocumentChoice

        Public Sub New()
            Dim root As New TableLayoutPanel()
            Dim searchPanel As New Panel()
            Dim footer As New FlowLayoutPanel()

            Text = "รายการเบิกย้อนหลัง"
            StartPosition = FormStartPosition.CenterParent
            ClientSize = New Size(980, 620)
            MinimumSize = New Size(780, 480)
            Font = New Font("Cordia New", 16.0!)

            root.Dock = DockStyle.Fill
            root.ColumnCount = 1
            root.RowCount = 3
            root.RowStyles.Add(New RowStyle(SizeType.Absolute, 58.0!))
            root.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
            root.RowStyles.Add(New RowStyle(SizeType.Absolute, 62.0!))

            searchPanel.Dock = DockStyle.Fill
            searchPanel.Padding = New Padding(8)
            txtSearch.Dock = DockStyle.Fill
            txtSearch.Margin = New Padding(0)
            searchPanel.Controls.Add(txtSearch)

            grid.Dock = DockStyle.Fill
            grid.AllowUserToAddRows = False
            grid.ReadOnly = True
            grid.MultiSelect = False
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grid.RowHeadersVisible = False
            grid.RowTemplate.Height = 34
            grid.ColumnHeadersHeight = 42
            grid.EnableHeadersVisualStyles = False
            grid.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
                .BackColor = Color.Black, .ForeColor = Color.White,
                .Font = New Font("Cordia New", 16.0!, FontStyle.Bold),
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            }
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "DocNo", .HeaderText = "เลขที่เอกสาร", .Width = 170})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "IssueDate", .HeaderText = "วันที่", .Width = 170})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Purpose", .HeaderText = "เบิกเพื่อ", .Width = 150})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "LineCount", .HeaderText = "รายการ", .Width = 90})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Remark", .HeaderText = "หมายเหตุ", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            Dim purposeColumn As New DataGridViewTextBoxColumn With {.Name = "PurposeCode", .Visible = False}
            grid.Columns.Add(purposeColumn)

            footer.Dock = DockStyle.Fill
            footer.FlowDirection = FlowDirection.RightToLeft
            footer.Padding = New Padding(8)
            StyleHistoryButton(btnSelect, "เลือก", Color.FromArgb(38, 132, 74), 110)
            StyleHistoryButton(btnCancel, "ยกเลิก", Color.FromArgb(190, 75, 75), 110)
            footer.Controls.Add(btnCancel)
            footer.Controls.Add(btnSelect)

            root.Controls.Add(searchPanel, 0, 0)
            root.Controls.Add(grid, 0, 1)
            root.Controls.Add(footer, 0, 2)
            Controls.Add(root)

            AddHandler Load, Sub(sender, e) LoadDocuments()
            AddHandler txtSearch.TextChanged,
                Sub(sender, e)
                    If IsHandleCreated Then LoadDocuments()
                End Sub
            AddHandler btnSelect.Click, Sub(sender, e) SelectCurrent()
            AddHandler btnCancel.Click, Sub(sender, e) DialogResult = DialogResult.Cancel
            AddHandler grid.CellDoubleClick,
                Sub(sender, e)
                    If e.RowIndex >= 0 Then SelectCurrent()
                End Sub
        End Sub

        Private Shared Sub StyleHistoryButton(button As Button, caption As String, backColor As Color, width As Integer)
            button.Text = caption
            button.Width = width
            button.Height = 40
            button.FlatStyle = FlatStyle.Flat
            button.FlatAppearance.BorderSize = 0
            button.BackColor = backColor
            button.ForeColor = Color.White
            button.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        End Sub

        Private Sub LoadDocuments()
            Try
                grid.Rows.Clear()
                Using conn As SqlConnection = modVariable.OpenConnection()
                    Const sql As String =
                        "SELECT TOP (500) t.DocNo, t.[Date], ISNULL(t.Purpose_Pay,0) AS PurposePay, " &
                        "ISNULL(t.Remark,N'') AS Remark, COUNT(d.MaterialID) AS LineCount " &
                        "FROM dbo.[Transaction] t LEFT JOIN dbo.TransactionDetail d ON d.DocNo=t.DocNo " &
                        "WHERE t.DocType=2 AND (@Search=N'' OR t.DocNo LIKE N'%' + @Search + N'%' OR t.Remark LIKE N'%' + @Search + N'%') " &
                        "GROUP BY t.DocNo, t.[Date], t.Purpose_Pay, t.Remark " &
                        "ORDER BY t.[Date] DESC, t.DocNo DESC"
                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = txtSearch.Text.Trim()
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim index As Integer = grid.Rows.Add()
                                Dim purposeCode As Integer = Convert.ToInt32(reader("PurposePay"), CultureInfo.InvariantCulture)
                                Dim purposeText As String = GetPurposeText(purposeCode)
                                grid.Rows(index).Cells("DocNo").Value = Convert.ToString(reader("DocNo")).Trim()
                                grid.Rows(index).Cells("IssueDate").Value = Convert.ToDateTime(reader("Date")).ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                grid.Rows(index).Cells("Purpose").Value = purposeText
                                grid.Rows(index).Cells("LineCount").Value = Convert.ToInt32(reader("LineCount"), CultureInfo.InvariantCulture)
                                grid.Rows(index).Cells("Remark").Value = Convert.ToString(reader("Remark"))
                                grid.Rows(index).Cells("PurposeCode").Value = purposeCode
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("โหลดรายการย้อนหลังไม่สำเร็จ: " & ex.Message, "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Shared Function GetPurposeText(purposeCode As Integer) As String
            Select Case purposeCode
                Case 1 : Return "1 - ใช้"
                Case 2 : Return "2 - เสียหาย/หมดอายุ"
                Case 3 : Return "3 - ตัดเทส"
                Case Else : Return purposeCode.ToString(CultureInfo.InvariantCulture)
            End Select
        End Function

        Private Sub SelectCurrent()
            If grid.SelectedRows.Count = 0 Then Return
            Dim row As DataGridViewRow = grid.SelectedRows(0)
            Dim purposeCode As Integer = 0
            Integer.TryParse(Convert.ToString(row.Cells("PurposeCode").Value), purposeCode)
            Dim issueDate As DateTime = DateTime.Now
            DateTime.TryParseExact(Convert.ToString(row.Cells("IssueDate").Value), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, issueDate)
            _Selected = New IssueDocumentChoice(
                Convert.ToString(row.Cells("DocNo").Value).Trim(), issueDate, purposeCode,
                Convert.ToString(row.Cells("Remark").Value))
            DialogResult = DialogResult.OK
        End Sub

        Public ReadOnly Property Selected As IssueDocumentChoice
            Get
                Return _Selected
            End Get
        End Property
    End Class

    Private NotInheritable Class MaterialPickerForm
        Inherits Form

        Private ReadOnly txtSearch As New TextBox()
        Private ReadOnly grid As New DataGridView()
        Private ReadOnly btnSelect As New Button()
        Private ReadOnly btnCancel As New Button()
        Public Sub New(searchText As String)
            Dim root As New TableLayoutPanel()
            Dim searchPanel As New Panel()
            Text = "เลือกวัตถุดิบ"
            StartPosition = FormStartPosition.CenterParent
            ClientSize = New Size(900, 600)
            MinimumSize = New Size(700, 450)
            Font = New Font("Cordia New", 16.0!)
            root.Dock = DockStyle.Fill
            root.ColumnCount = 1
            root.RowCount = 3
            root.RowStyles.Add(New RowStyle(SizeType.Absolute, 58.0!))
            root.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
            root.RowStyles.Add(New RowStyle(SizeType.Absolute, 58.0!))
            searchPanel.Dock = DockStyle.Fill
            searchPanel.Padding = New Padding(8, 8, 8, 8)
            txtSearch.Dock = DockStyle.Fill
            txtSearch.Text = searchText
            txtSearch.Margin = New Padding(0)
            searchPanel.Controls.Add(txtSearch)
            grid.Dock = DockStyle.Fill
            grid.AllowUserToAddRows = False
            grid.ReadOnly = True
            grid.MultiSelect = False
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grid.RowHeadersVisible = False
            grid.RowTemplate.Height = 36
            grid.ColumnHeadersVisible = True
            grid.ColumnHeadersHeight = 42
            grid.EnableHeadersVisualStyles = False
            grid.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
                .BackColor = Color.FromArgb(75, 111, 130), .ForeColor = Color.White,
                .Font = New Font("Cordia New", 16.0!, FontStyle.Bold),
                .Alignment = DataGridViewContentAlignment.MiddleCenter
            }
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MaterialID", .HeaderText = "รหัสวัตถุดิบ", .Width = 140})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MaterialDetail", .HeaderText = "รายการวัตถุดิบ", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Brand", .HeaderText = "ยี่ห้อ", .Width = 150})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitUse", .HeaderText = "หน่วย", .Width = 100})
            grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitPerPackage", .HeaderText = "จำนวน/แพ็ก", .Width = 120})
            Dim footer As New FlowLayoutPanel With {.Dock = DockStyle.Bottom, .Height = 58, .FlowDirection = FlowDirection.RightToLeft, .Padding = New Padding(8)}
            StylePickerButton(btnSelect, "เลือก", Color.FromArgb(38, 132, 74), 110)
            StylePickerButton(btnCancel, "ยกเลิก", Color.FromArgb(190, 75, 75), 110)
            footer.Controls.Add(btnCancel)
            footer.Controls.Add(btnSelect)
            root.Controls.Add(searchPanel, 0, 0)
            root.Controls.Add(grid, 0, 1)
            root.Controls.Add(footer, 0, 2)
            Controls.Add(root)
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

        Private Shared Sub StylePickerButton(button As Button, caption As String, backColor As Color, width As Integer)
            button.Text = caption
            button.Width = width
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
                        "SELECT TOP (500) LTRIM(RTRIM(MaterialID)) AS MaterialID, " &
                        "LTRIM(RTRIM(ISNULL(Material_Detail,N''))) AS MaterialDetail, " &
                        "LTRIM(RTRIM(ISNULL(Brand,N''))) AS Brand, " &
                        "LTRIM(RTRIM(ISNULL(Unit_Use,N''))) AS UnitUse, " &
                        "ISNULL(Unit_per_Package,0) AS UnitPerPackage " &
                        "FROM dbo.MaterialDrink " &
                        "WHERE @Search=N'' OR MaterialID LIKE N'%' + @Search + N'%' OR Material_Detail LIKE N'%' + @Search + N'%' OR Brand LIKE N'%' + @Search + N'%' " &
                        "ORDER BY MaterialID"
                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = txtSearch.Text.Trim()
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim index As Integer = grid.Rows.Add()
                                grid.Rows(index).Cells("MaterialID").Value = Convert.ToString(reader("MaterialID")).Trim()
                                grid.Rows(index).Cells("MaterialDetail").Value = Convert.ToString(reader("MaterialDetail")).Trim()
                                grid.Rows(index).Cells("Brand").Value = Convert.ToString(reader("Brand")).Trim()
                                grid.Rows(index).Cells("UnitUse").Value = Convert.ToString(reader("UnitUse")).Trim()
                                grid.Rows(index).Cells("UnitPerPackage").Value = Convert.ToDecimal(reader("UnitPerPackage"), CultureInfo.InvariantCulture).ToString("N2", CultureInfo.InvariantCulture)
                            End While
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("โหลดรายการวัตถุดิบไม่สำเร็จ: " & ex.Message, "เลือกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Sub SelectCurrent()
            If grid.SelectedRows.Count = 0 Then Return
            Dim row As DataGridViewRow = grid.SelectedRows(0)
            Dim perPackage As Decimal
            Decimal.TryParse(Convert.ToString(row.Cells("UnitPerPackage").Value), NumberStyles.Any, CultureInfo.InvariantCulture, perPackage)
            _Selected = New MaterialChoice(
                Convert.ToString(row.Cells("MaterialID").Value).Trim(),
                Convert.ToString(row.Cells("MaterialDetail").Value).Trim(),
                Convert.ToString(row.Cells("Brand").Value).Trim(),
                Convert.ToString(row.Cells("UnitUse").Value).Trim(), perPackage)
            DialogResult = DialogResult.OK
        End Sub

        Private _Selected As MaterialChoice
        Public Shadows ReadOnly Property Selected As MaterialChoice
            Get
                Return _Selected
            End Get
        End Property
    End Class
End Class
