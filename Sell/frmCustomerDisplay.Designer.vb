<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCustomerDisplay
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerDisplay))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New DevExpress.XtraEditors.PanelControl()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bannerTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.dgvCustomer = New Krypton.Toolkit.KryptonDataGridView()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblChangeAmount = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.lblCashAmount = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblTypePay = New System.Windows.Forms.Label()
        Me.panelPhone = New DevExpress.XtraEditors.PanelControl()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.lblHint = New System.Windows.Forms.Label()
        Me.PnlPicture = New DevExpress.XtraEditors.PanelControl()
        Me.picBanner = New System.Windows.Forms.PictureBox()
        Me.picQR = New System.Windows.Forms.PictureBox()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.panelPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelPhone.SuspendLayout()
        CType(Me.PnlPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlPicture.SuspendLayout()
        CType(Me.picBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.pnlHeader.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.pnlHeader.Controls.Add(Me.PictureBox1)
        Me.pnlHeader.Controls.Add(Me.Label2)
        Me.pnlHeader.Controls.Add(Me.Label1)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1006, 90)
        Me.pnlHeader.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Honeydew
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(73, 72)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Honeydew
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Cordia New", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.Label2.Location = New System.Drawing.Point(0, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1006, 45)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = " ยินดีต้อนรับ :)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Honeydew
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Cordia New", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1006, 45)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "The Factory Kafe' by smile heart"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'bannerTimer
        '
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.74603!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.25397!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelControl3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PnlPicture, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.56757!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1006, 531)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'PanelControl3
        '
        Me.PanelControl3.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.PanelControl3.Controls.Add(Me.dgvCustomer)
        Me.PanelControl3.Controls.Add(Me.PanelControl4)
        Me.PanelControl3.Controls.Add(Me.panelPhone)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(4, 4)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(563, 523)
        Me.PanelControl3.TabIndex = 9
        '
        'dgvCustomer
        '
        Me.dgvCustomer.AllowUserToAddRows = False
        Me.dgvCustomer.AllowUserToDeleteRows = False
        Me.dgvCustomer.AllowUserToResizeRows = False
        Me.dgvCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvCustomer.ColumnHeadersHeight = 48
        Me.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCustomer.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Sheet
        Me.dgvCustomer.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dgvCustomer.GridStyles.StyleColumn = Krypton.Toolkit.GridStyle.Sheet
        Me.dgvCustomer.GridStyles.StyleDataCells = Krypton.Toolkit.GridStyle.Sheet
        Me.dgvCustomer.GridStyles.StyleRow = Krypton.Toolkit.GridStyle.Sheet
        Me.dgvCustomer.Location = New System.Drawing.Point(0, 201)
        Me.dgvCustomer.MultiSelect = False
        Me.dgvCustomer.Name = "dgvCustomer"
        Me.dgvCustomer.RowHeadersVisible = False
        Me.dgvCustomer.RowHeadersWidth = 51
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgvCustomer.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCustomer.RowTemplate.Height = 36
        Me.dgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCustomer.Size = New System.Drawing.Size(563, 151)
        Me.dgvCustomer.StateCommon.Background.Color1 = System.Drawing.Color.White
        Me.dgvCustomer.StateCommon.Background.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid
        Me.dgvCustomer.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dgvCustomer.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.White
        Me.dgvCustomer.StateCommon.HeaderColumn.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvCustomer.StateCommon.HeaderRow.Back.Color1 = System.Drawing.Color.White
        Me.dgvCustomer.StateCommon.HeaderRow.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvCustomer.TabIndex = 49
        '
        'PanelControl4
        '
        Me.PanelControl4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelControl4.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.PanelControl4.Controls.Add(Me.Label5)
        Me.PanelControl4.Controls.Add(Me.lblTotal)
        Me.PanelControl4.Controls.Add(Me.Label3)
        Me.PanelControl4.Controls.Add(Me.lblChangeAmount)
        Me.PanelControl4.Controls.Add(Me.Label7)
        Me.PanelControl4.Controls.Add(Me.lblTotalAmount)
        Me.PanelControl4.Controls.Add(Me.lblCashAmount)
        Me.PanelControl4.Controls.Add(Me.Label9)
        Me.PanelControl4.Controls.Add(Me.lblDiscount)
        Me.PanelControl4.Controls.Add(Me.lblTypePay)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl4.Location = New System.Drawing.Point(0, 352)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(563, 171)
        Me.PanelControl4.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(176, 29)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "ยอดที่ต้องชำระ"
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(330, 66)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(228, 29)
        Me.lblTotal.TabIndex = 9
        Me.lblTotal.Text = "0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 29)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "ยอดรวม"
        '
        'lblChangeAmount
        '
        Me.lblChangeAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblChangeAmount.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangeAmount.Location = New System.Drawing.Point(330, 128)
        Me.lblChangeAmount.Name = "lblChangeAmount"
        Me.lblChangeAmount.Size = New System.Drawing.Size(228, 29)
        Me.lblChangeAmount.TabIndex = 5
        Me.lblChangeAmount.Text = "0.00"
        Me.lblChangeAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 29)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "ส่วนลด"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalAmount.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.Location = New System.Drawing.Point(330, 5)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(228, 29)
        Me.lblTotalAmount.TabIndex = 1
        Me.lblTotalAmount.Text = "0.00"
        Me.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCashAmount
        '
        Me.lblCashAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCashAmount.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashAmount.Location = New System.Drawing.Point(330, 97)
        Me.lblCashAmount.Name = "lblCashAmount"
        Me.lblCashAmount.Size = New System.Drawing.Size(228, 29)
        Me.lblCashAmount.TabIndex = 3
        Me.lblCashAmount.Text = "0.00"
        Me.lblCashAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 127)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 30)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "เงินทอน"
        '
        'lblDiscount
        '
        Me.lblDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.Location = New System.Drawing.Point(330, 35)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(228, 29)
        Me.lblDiscount.TabIndex = 7
        Me.lblDiscount.Text = "0.00"
        Me.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTypePay
        '
        Me.lblTypePay.Font = New System.Drawing.Font("Segoe UI", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTypePay.Location = New System.Drawing.Point(6, 96)
        Me.lblTypePay.Name = "lblTypePay"
        Me.lblTypePay.Size = New System.Drawing.Size(131, 29)
        Me.lblTypePay.TabIndex = 2
        Me.lblTypePay.Text = "รับเงิน"
        '
        'panelPhone
        '
        Me.panelPhone.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.panelPhone.Controls.Add(Me.txtPhone)
        Me.panelPhone.Controls.Add(Me.lblHint)
        Me.panelPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelPhone.Location = New System.Drawing.Point(0, 0)
        Me.panelPhone.Name = "panelPhone"
        Me.panelPhone.Size = New System.Drawing.Size(563, 201)
        Me.panelPhone.TabIndex = 11
        '
        'txtPhone
        '
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtPhone.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.Location = New System.Drawing.Point(0, 64)
        Me.txtPhone.Multiline = True
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(563, 130)
        Me.txtPhone.TabIndex = 6
        Me.txtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblHint
        '
        Me.lblHint.BackColor = System.Drawing.Color.White
        Me.lblHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHint.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHint.Font = New System.Drawing.Font("Cordia New", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblHint.Location = New System.Drawing.Point(0, 0)
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Size = New System.Drawing.Size(563, 64)
        Me.lblHint.TabIndex = 5
        Me.lblHint.Text = "กรุณากรอกเบอร์โทร 10 หลัก"
        Me.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlPicture
        '
        Me.PnlPicture.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.PnlPicture.Controls.Add(Me.picBanner)
        Me.PnlPicture.Controls.Add(Me.picQR)
        Me.PnlPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlPicture.Location = New System.Drawing.Point(574, 4)
        Me.PnlPicture.Name = "PnlPicture"
        Me.PnlPicture.Size = New System.Drawing.Size(428, 523)
        Me.PnlPicture.TabIndex = 13
        '
        'picBanner
        '
        Me.picBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picBanner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBanner.Location = New System.Drawing.Point(0, 0)
        Me.picBanner.Name = "picBanner"
        Me.picBanner.Size = New System.Drawing.Size(428, 523)
        Me.picBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBanner.TabIndex = 0
        Me.picBanner.TabStop = False
        '
        'picQR
        '
        Me.picQR.Location = New System.Drawing.Point(100, 60)
        Me.picQR.Name = "picQR"
        Me.picQR.Size = New System.Drawing.Size(165, 131)
        Me.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picQR.TabIndex = 1
        Me.picQR.TabStop = False
        '
        'GridView1
        '
        Me.GridView1.ColumnPanelRowHeight = 42
        Me.GridView1.DetailHeight = 539
        Me.GridView1.FocusedRowHandle = -1
        Me.GridView1.GridControl = Nothing
        Me.GridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.GridView1.Name = "GridView1"
        Me.GridView1.RowHeight = 40
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.PanelControl1.Controls.Add(Me.Label4)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 531)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1006, 100)
        Me.PanelControl1.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Green
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Cordia New", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1006, 100)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "ขอบคุณที่ใช้บริการ :)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PanelControl2
        '
        Me.PanelControl2.BorderStyleEx = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.PanelControl2.Controls.Add(Me.TableLayoutPanel1)
        Me.PanelControl2.Controls.Add(Me.PanelControl1)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 90)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(1006, 631)
        Me.PanelControl2.TabIndex = 2
        '
        'frmCustomerDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 721)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmCustomerDisplay"
        Me.Text = "frmCustomerDisplay"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.pnlHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.dgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.panelPhone, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelPhone.ResumeLayout(False)
        Me.panelPhone.PerformLayout()
        CType(Me.PnlPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlPicture.ResumeLayout(False)
        CType(Me.picBanner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picQR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents bannerTimer As Timer
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents panelPhone As DevExpress.XtraEditors.PanelControl
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents lblHint As Label
    Friend WithEvents PnlPicture As DevExpress.XtraEditors.PanelControl
    Friend WithEvents picBanner As PictureBox
    Friend WithEvents picQR As PictureBox
    Friend WithEvents lblChangeAmount As Label
    Friend WithEvents lblTypePay As Label
    Friend WithEvents lblDiscount As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblCashAmount As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblTotalAmount As Label
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label4 As Label
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label5 As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents dgvCustomer As Krypton.Toolkit.KryptonDataGridView
End Class
