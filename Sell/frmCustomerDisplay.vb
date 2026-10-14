Imports System.Drawing
Imports System.ComponentModel
Imports System.Text
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.ColorPick
Imports DevExpress.XtraGrid
Public Class frmCustomerDisplay
    Public Event CustomerSelected(customerNumber As Integer, customerName As String, customerCode As String, customerPoint As Decimal, memberLevel As Integer)
    Public Event CustomerLookupFailed(message As String)
    Dim dtCustomer As New DataTable
    Private bannerImages As New List(Of String)
    Private rnd As New Random()
    Private customerDisplay As frmCustomerDisplay
    Private rawPhone As String = ""
    Dim realMobile As String = ""
    Private summaryLayout As TableLayoutPanel
    Private bannerOverlay As Panel
    Private lblBannerTag As Label
    Private lblBannerName As Label
    Private lblBannerPrice As Label
    Private fadeTimer As Timer
    Private fadeOldImage As Image
    Private fadeNewImage As Image
    Private fadeStep As Integer = 0
    Private Const FadeSteps As Integer = 10
    Private Const FreeCupTargetQty As Decimal = 10D
    Private Sub frmCustomerDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsDesignTime() Then Return

        Dim screen As Screen = Screen.AllScreens(1)
        Me.BackColor = Color.FromArgb(250, 246, 242)
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = screen.Bounds.Location
        Me.Size = screen.Bounds.Size
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        ApplyRedCustomerTheme()

        Dim path As String = ResolveCustomerAdsPath()
        LoadBannerImages(path)
        ShowDefaultScreen()
        InitBannerTimer()
        bannerTimer.Start()
    End Sub

    Private Function ResolveCustomerAdsPath() As String
        Return ImageAssetStore.GetCustomerAdsPath()
    End Function
    Private Sub ApplyRedCustomerTheme()
        Dim redDark As Color = Color.FromArgb(38, 105, 140)
        Dim redMain As Color = Color.FromArgb(96, 169, 204)
        Dim redSoft As Color = Color.FromArgb(231, 246, 252)
        Dim ink As Color = Color.FromArgb(35, 48, 56)
        Dim paper As Color = Color.FromArgb(248, 253, 255)
        Dim softFont As String = "Cordia New"

        Me.BackColor = paper

        pnlHeader.Height = 118
        pnlHeader.Appearance.BackColor = Color.FromArgb(67, 150, 190)
        pnlHeader.Appearance.Options.UseBackColor = True
        pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder

        Label1.Height = 68
        Label1.Padding = New Padding(0, 4, 0, 8)
        Label1.BackColor = Color.FromArgb(67, 150, 190)
        Label1.ForeColor = Color.White
        Label1.Font = New Font(softFont, 28.0!, FontStyle.Bold)
        Label1.TextAlign = ContentAlignment.MiddleCenter

        Label2.BackColor = Color.FromArgb(67, 150, 190)
        Label2.ForeColor = Color.White
        Label2.Font = New Font(softFont, 16.0!, FontStyle.Bold)

        PictureBox1.BackColor = Color.FromArgb(67, 150, 190)
        PictureBox1.BorderStyle = BorderStyle.None
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.Size = New Size(100, 100)
        MakeLogoBackgroundTransparent()
        PositionHeaderLogo()
        PictureBox1.BringToFront()

        PanelControl2.Appearance.BackColor = paper
        PanelControl2.Appearance.Options.UseBackColor = True
        PanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder

        TableLayoutPanel1.BackColor = paper
        TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None

        PanelControl3.Appearance.BackColor = paper
        PanelControl3.Appearance.Options.UseBackColor = True
        PanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        PanelControl3.Padding = New Padding(8)
        dgvCustomer.Dock = DockStyle.Fill
        dgvCustomer.Margin = New Padding(0)
        dgvCustomer.BringToFront()


        PnlPicture.Padding = New Padding(12)
        PnlPicture.Appearance.BackColor = Color.FromArgb(231, 246, 252)
        PnlPicture.Appearance.Options.UseBackColor = True
        PnlPicture.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        picBanner.BackColor = Color.White
        picBanner.BorderStyle = BorderStyle.None
        picBanner.SizeMode = PictureBoxSizeMode.Zoom

        picQR.BackColor = Color.White
        picQR.BorderStyle = BorderStyle.FixedSingle
        picQR.SizeMode = PictureBoxSizeMode.Zoom
        ResizeQR()

        PanelControl4.Visible = False
        PanelControl4.Height = 0
        PanelControl4.Appearance.BackColor = Color.White
        PanelControl4.Appearance.Options.UseBackColor = True
        PanelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        BuildSummaryLayout(redDark, redMain, ink)

        For Each caption As Label In New Label() {Label3, Label5, Label7, Label9, lblTypePay}
            caption.ForeColor = ink
            caption.Font = New Font(softFont, 15.0!, FontStyle.Regular)
        Next

        For Each amount As Label In New Label() {lblTotalAmount, lblDiscount, lblTotal, lblCashAmount, lblChangeAmount}
            amount.ForeColor = redDark
            amount.Font = New Font("Segoe UI", 18.0!, FontStyle.Bold)
        Next

        Label5.ForeColor = Color.FromArgb(18, 121, 168)
        Label5.Font = New Font(softFont, 16.0!, FontStyle.Bold)
        lblTotal.ForeColor = Color.FromArgb(18, 121, 168)
        lblTotal.Font = New Font("Segoe UI", 24.0!, FontStyle.Bold)

        PanelControl1.Height = 154
        PanelControl1.Appearance.BackColor = Color.FromArgb(255, 250, 246)
        PanelControl1.Appearance.Options.UseBackColor = True
        PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Label4.Height = 42
        Label4.BackColor = Color.FromArgb(217, 45, 62)
        Label4.ForeColor = Color.White
        Label4.Font = New Font(softFont, 16.0!, FontStyle.Bold)

        panelPhone.Appearance.BackColor = redSoft
        panelPhone.Appearance.Options.UseBackColor = True
        panelPhone.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        panelPhone.Height = 204
        lblHint.BackColor = Color.FromArgb(96, 169, 204)
        lblHint.ForeColor = Color.White
        lblHint.BorderStyle = BorderStyle.None
        lblHint.Font = New Font(softFont, 18.0!, FontStyle.Bold)
        lblHint.Height = 64
        txtPhone.BackColor = Color.White
        txtPhone.ForeColor = redDark
        txtPhone.BorderStyle = BorderStyle.FixedSingle
        txtPhone.Height = 132

        dgvCustomer.BackColor = paper
        GridView1.Appearance.Empty.BackColor = paper
        GridView1.Appearance.Empty.Options.UseBackColor = True
        GridView1.Appearance.Row.BackColor = Color.White
        GridView1.Appearance.Row.ForeColor = ink
        GridView1.Appearance.Row.Font = New Font(softFont, 14.0!, FontStyle.Regular)
        GridView1.Appearance.Row.Options.UseBackColor = True
        GridView1.Appearance.Row.Options.UseForeColor = True
        GridView1.Appearance.Row.Options.UseFont = True
        GridView1.Appearance.HeaderPanel.BackColor = Color.FromArgb(96, 169, 204)
        GridView1.Appearance.HeaderPanel.ForeColor = Color.White
        GridView1.Appearance.HeaderPanel.Font = New Font(softFont, 13.0!, FontStyle.Bold)
        GridView1.Appearance.HeaderPanel.Options.UseBackColor = True
        GridView1.Appearance.HeaderPanel.Options.UseForeColor = True
        GridView1.Appearance.HeaderPanel.Options.UseFont = True
        GridView1.Appearance.FooterPanel.BackColor = redSoft
        GridView1.Appearance.FooterPanel.ForeColor = redDark
        GridView1.Appearance.FooterPanel.Options.UseBackColor = True
        GridView1.Appearance.FooterPanel.Options.UseForeColor = True
        GridView1.RowHeight = 46
        GridView1.ColumnPanelRowHeight = 46
        GridView1.OptionsView.EnableAppearanceEvenRow = True
        GridView1.Appearance.EvenRow.BackColor = Color.FromArgb(241, 250, 253)
        GridView1.Appearance.EvenRow.Options.UseBackColor = True

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
            col.AppearanceCell.BackColor = Color.Transparent
            col.AppearanceCell.ForeColor = ink
            col.AppearanceCell.Font = New Font(softFont, 14.0!, FontStyle.Regular)
            col.AppearanceCell.Options.UseBackColor = True
            col.AppearanceCell.Options.UseForeColor = True
            col.AppearanceCell.Options.UseFont = True
            col.AppearanceHeader.BackColor = Color.FromArgb(96, 169, 204)
            col.AppearanceHeader.ForeColor = Color.White
            col.AppearanceHeader.Font = New Font(softFont, 13.0!, FontStyle.Bold)
            col.AppearanceHeader.Options.UseBackColor = True
            col.AppearanceHeader.Options.UseForeColor = True
            col.AppearanceHeader.Options.UseFont = True
        Next
    End Sub
    Private Sub MakeLogoBackgroundTransparent()
        If PictureBox1 Is Nothing OrElse PictureBox1.Image Is Nothing Then Return

        Dim logo As New Bitmap(PictureBox1.Image)
        Dim centerX As Double = (logo.Width - 1) / 2.0R
        Dim centerY As Double = (logo.Height - 1) / 2.0R
        Dim radiusX As Double = logo.Width * 0.47R
        Dim radiusY As Double = logo.Height * 0.47R

        For y As Integer = 0 To logo.Height - 1
            For x As Integer = 0 To logo.Width - 1
                Dim normalizedX As Double = (x - centerX) / radiusX
                Dim normalizedY As Double = (y - centerY) / radiusY
                If (normalizedX * normalizedX) + (normalizedY * normalizedY) > 1.0R Then
                    logo.SetPixel(x, y, Color.Transparent)
                End If
            Next
        Next

        PictureBox1.Image = logo
    End Sub

    Private Sub PositionHeaderLogo()
        If IsDesignTime() Then Exit Sub
        If pnlHeader Is Nothing OrElse Label1 Is Nothing OrElse PictureBox1 Is Nothing Then Exit Sub

        Dim textWidth As Integer
        Using g As Graphics = Label1.CreateGraphics()
            textWidth = CInt(g.MeasureString(Label1.Text, Label1.Font).Width)
        End Using

        Dim titleLeft As Integer = Math.Max(0, (pnlHeader.ClientSize.Width - textWidth) \ 2)
        'Keep the logo close to the left edge so it does not crowd the title.
        PictureBox1.Left = 0
        PictureBox1.Top = Math.Max(8, (pnlHeader.Height - PictureBox1.Height) \ 2 - 2)
    End Sub
    Private Sub frmCustomerDisplay_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        PositionHeaderLogo()
    End Sub
    Private Sub BuildSummaryLayout(redDark As Color, redMain As Color, ink As Color)
        If summaryLayout IsNot Nothing Then Return

        Dim softFont As String = "Leelawadee UI"
        Dim accent As Color = Color.FromArgb(217, 45, 62)
        Dim accentSoft As Color = Color.FromArgb(255, 238, 235)
        Dim cardLine As Color = Color.FromArgb(255, 167, 158)

        summaryLayout = New TableLayoutPanel()
        summaryLayout.Dock = DockStyle.Fill
        summaryLayout.BackColor = Color.FromArgb(255, 250, 246)
        summaryLayout.ColumnCount = 5
        summaryLayout.RowCount = 1
        summaryLayout.Padding = New Padding(12, 10, 12, 10)
        summaryLayout.Margin = New Padding(0)
        summaryLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18.0!))
        summaryLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 17.0!))
        summaryLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30.0!))
        summaryLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 17.0!))
        summaryLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 18.0!))
        summaryLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        PanelControl1.Controls.Clear()

        Label4.Text = "♡  ขอบคุณที่ใช้บริการ  ♡"
        Label4.Dock = DockStyle.Bottom
        Label4.TextAlign = ContentAlignment.MiddleCenter

        PanelControl1.Controls.Add(summaryLayout)
        PanelControl1.Controls.Add(Label4)

        summaryLayout.Controls.Add(CreateSummaryCard("🧾", Label3, lblTotalAmount, False, softFont, ink, accent, accentSoft, cardLine), 0, 0)
        summaryLayout.Controls.Add(CreateSummaryCard("%", Label7, lblDiscount, False, softFont, ink, accent, accentSoft, cardLine), 1, 0)
        summaryLayout.Controls.Add(CreateSummaryCard("", Label5, lblTotal, True, softFont, ink, accent, accentSoft, cardLine), 2, 0)
        summaryLayout.Controls.Add(CreateSummaryCard("💵", lblTypePay, lblCashAmount, False, softFont, ink, accent, accentSoft, cardLine), 3, 0)
        summaryLayout.Controls.Add(CreateSummaryCard("฿", Label9, lblChangeAmount, False, softFont, ink, accent, accentSoft, cardLine), 4, 0)
    End Sub
    Private Function CreateSummaryCard(iconText As String,
                                       caption As Label,
                                       amount As Label,
                                       isMain As Boolean,
                                       softFont As String,
                                       ink As Color,
                                       accent As Color,
                                       accentSoft As Color,
                                       cardLine As Color) As Panel
        Dim card As New Panel()
        card.Dock = DockStyle.Fill
        card.Margin = New Padding(5, 2, 5, 8)
        card.Padding = New Padding(8, 8, 8, 8)
        card.BackColor = If(isMain, accentSoft, Color.FromArgb(255, 252, 248))

        AddHandler card.Paint,
            Sub(sender As Object, e As PaintEventArgs)
                Using pen As New Pen(cardLine, 1)
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1)
                End Using
            End Sub

        If isMain Then
            Dim mainPanel As New TableLayoutPanel()
            mainPanel.Dock = DockStyle.Fill
            mainPanel.ColumnCount = 1
            mainPanel.RowCount = 2
            mainPanel.BackColor = Color.Transparent
            mainPanel.Margin = New Padding(0)
            mainPanel.Padding = New Padding(0)
            mainPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 42.0!))
            mainPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 58.0!))

            caption.Dock = DockStyle.Fill
            caption.TextAlign = ContentAlignment.MiddleCenter
            caption.Font = New Font(softFont, 12.0!, FontStyle.Bold)
            caption.ForeColor = accent
            caption.BackColor = Color.Transparent

            amount.Dock = DockStyle.Fill
            amount.TextAlign = ContentAlignment.MiddleCenter
            amount.Font = New Font("Segoe UI", 27.0!, FontStyle.Bold)
            amount.ForeColor = accent
            amount.BackColor = Color.Transparent

            mainPanel.Controls.Add(caption, 0, 0)
            mainPanel.Controls.Add(amount, 0, 1)
            card.Controls.Add(mainPanel)
            Return card
        End If

        Dim icon As New Label()
        icon.Dock = DockStyle.Left
        icon.Width = 52
        icon.Text = iconText
        icon.TextAlign = ContentAlignment.MiddleCenter
        icon.Font = New Font("Segoe UI", 18.0!, FontStyle.Bold)
        icon.ForeColor = Color.White
        icon.BackColor = Color.FromArgb(250, 132, 136)
        AddHandler icon.Resize,
            Sub(sender As Object, e As EventArgs)
                Dim r As New System.Drawing.Drawing2D.GraphicsPath()
                r.AddEllipse(4, 4, icon.Width - 8, icon.Height - 8)
                icon.Region = New Region(r)
            End Sub

        Dim textPanel As New TableLayoutPanel()
        textPanel.Dock = DockStyle.Fill
        textPanel.ColumnCount = 1
        textPanel.RowCount = 2
        textPanel.BackColor = Color.Transparent
        textPanel.Padding = New Padding(6, 0, 0, 0)
        textPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 45.0!))
        textPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 55.0!))

        caption.Dock = DockStyle.Fill
        caption.TextAlign = ContentAlignment.BottomCenter
        caption.Font = New Font(softFont, 11.0!, FontStyle.Bold)
        caption.ForeColor = ink
        caption.BackColor = Color.Transparent

        amount.Dock = DockStyle.Fill
        amount.TextAlign = ContentAlignment.TopCenter
        amount.Font = New Font("Segoe UI", 17.0!, FontStyle.Bold)
        amount.ForeColor = ink
        amount.BackColor = Color.Transparent

        textPanel.Controls.Add(caption, 0, 0)
        textPanel.Controls.Add(amount, 0, 1)

        card.Controls.Add(textPanel)
        card.Controls.Add(icon)
        Return card
    End Function
    Private Sub BuildBannerOverlay(softFont As String, ink As Color, accent As Color)
        If bannerOverlay IsNot Nothing Then
            bannerOverlay.BringToFront()
            Return
        End If

        bannerOverlay = New Panel()
        bannerOverlay.Dock = DockStyle.Bottom
        bannerOverlay.Height = 104
        bannerOverlay.Padding = New Padding(18, 10, 18, 12)
        bannerOverlay.BackColor = Color.FromArgb(248, 253, 255)
        bannerOverlay.Visible = False

        AddHandler bannerOverlay.Paint,
            Sub(sender As Object, e As PaintEventArgs)
                Using pen As New Pen(Color.FromArgb(170, 218, 235), 1)
                    e.Graphics.DrawLine(pen, 0, 0, bannerOverlay.Width, 0)
                End Using
            End Sub

        Dim infoLayout As New TableLayoutPanel()
        infoLayout.Dock = DockStyle.Fill
        infoLayout.ColumnCount = 2
        infoLayout.RowCount = 2
        infoLayout.BackColor = Color.Transparent
        infoLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 68.0!))
        infoLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 32.0!))
        infoLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 34.0!))
        infoLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 66.0!))

        lblBannerTag = New Label()
        lblBannerTag.Dock = DockStyle.Fill
        lblBannerTag.Text = "เมนูแนะนำ"
        lblBannerTag.TextAlign = ContentAlignment.MiddleLeft
        lblBannerTag.Font = New Font(softFont, 13.0!, FontStyle.Bold)
        lblBannerTag.ForeColor = accent
        lblBannerTag.BackColor = Color.Transparent

        lblBannerName = New Label()
        lblBannerName.Dock = DockStyle.Fill
        lblBannerName.Text = "เมนูแนะนำวันนี้"
        lblBannerName.TextAlign = ContentAlignment.MiddleLeft
        lblBannerName.Font = New Font(softFont, 22.0!, FontStyle.Bold)
        lblBannerName.ForeColor = ink
        lblBannerName.BackColor = Color.Transparent
        lblBannerName.AutoEllipsis = True

        lblBannerPrice = New Label()
        lblBannerPrice.Dock = DockStyle.Fill
        lblBannerPrice.Text = ""
        lblBannerPrice.TextAlign = ContentAlignment.MiddleRight
        lblBannerPrice.Font = New Font("Segoe UI", 24.0!, FontStyle.Bold)
        lblBannerPrice.ForeColor = accent
        lblBannerPrice.BackColor = Color.Transparent

        infoLayout.Controls.Add(lblBannerTag, 0, 0)
        infoLayout.SetColumnSpan(lblBannerTag, 2)
        infoLayout.Controls.Add(lblBannerName, 0, 1)
        infoLayout.Controls.Add(lblBannerPrice, 1, 1)

        bannerOverlay.Controls.Add(infoLayout)
        PnlPicture.Controls.Add(bannerOverlay)
        bannerOverlay.BringToFront()
    End Sub
    Private Sub fCustomerDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsDesignTime() Then Return
        EnsureCustomerTable()
    End Sub

    Private Function IsDesignTime() As Boolean
        Return LicenseManager.UsageMode = LicenseUsageMode.Designtime OrElse
               (Site IsNot Nothing AndAlso Site.DesignMode)
    End Function

    Private Sub EnsureCustomerTable()
        If dtCustomer Is Nothing Then dtCustomer = New DataTable()

        If Not dtCustomer.Columns.Contains("ProductName") Then
            dtCustomer.Columns.Add("ProductName", GetType(String))
        End If
        If Not dtCustomer.Columns.Contains("Qty") Then
            dtCustomer.Columns.Add("Qty", GetType(Decimal))
        End If
        If Not dtCustomer.Columns.Contains("Priediscount") Then
            dtCustomer.Columns.Add("Priediscount", GetType(Decimal))
        End If
        If Not dtCustomer.Columns.Contains("Amount") Then
            dtCustomer.Columns.Add("Amount", GetType(Decimal))
        End If

        If Not Object.ReferenceEquals(dgvCustomer.DataSource, dtCustomer) Then
            dgvCustomer.DataSource = dtCustomer
        End If
        ConfigureCustomerGrid()
    End Sub

    Private Sub ConfigureCustomerGrid()
        If dgvCustomer Is Nothing Then Return

        dgvCustomer.AutoGenerateColumns = True
        dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCustomer.AllowUserToAddRows = False
        dgvCustomer.AllowUserToDeleteRows = False
        dgvCustomer.ReadOnly = True
        dgvCustomer.RowHeadersVisible = False
        dgvCustomer.EnableHeadersVisualStyles = False
        dgvCustomer.ColumnHeadersHeight = 50
        dgvCustomer.RowTemplate.Height = 54
        dgvCustomer.BackgroundColor = Color.FromArgb(248, 253, 255)
        dgvCustomer.BorderStyle = BorderStyle.None
        dgvCustomer.GridColor = Color.FromArgb(215, 225, 232)
        dgvCustomer.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(224, 239, 248)
        dgvCustomer.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 56)
        dgvCustomer.ColumnHeadersDefaultCellStyle.Font = New Font("Leelawadee UI", 17.0!, FontStyle.Bold)
        dgvCustomer.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvCustomer.DefaultCellStyle.Font = New Font("Leelawadee UI", 17.0!, FontStyle.Regular)
        dgvCustomer.DefaultCellStyle.BackColor = Color.White
        dgvCustomer.DefaultCellStyle.ForeColor = Color.FromArgb(35, 48, 56)
        dgvCustomer.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 190, 80)
        dgvCustomer.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 48, 56)
        dgvCustomer.DefaultCellStyle.Padding = New Padding(8, 0, 8, 0)

        If dgvCustomer.Columns.Contains("ProductName") Then
            dgvCustomer.Columns("ProductName").HeaderText = "รายการ"
            dgvCustomer.Columns("ProductName").FillWeight = 220
            dgvCustomer.Columns("ProductName").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End If
        If dgvCustomer.Columns.Contains("Qty") Then
            dgvCustomer.Columns("Qty").HeaderText = "จำนวน"
            dgvCustomer.Columns("Qty").FillWeight = 70
            dgvCustomer.Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvCustomer.Columns("Qty").DefaultCellStyle.Format = "N2"
        End If
        If dgvCustomer.Columns.Contains("Priediscount") Then
            dgvCustomer.Columns("Priediscount").HeaderText = "ราคา/หน่วย"
            dgvCustomer.Columns("Priediscount").FillWeight = 100
            dgvCustomer.Columns("Priediscount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvCustomer.Columns("Priediscount").DefaultCellStyle.Format = "N2"
        End If
        If dgvCustomer.Columns.Contains("Amount") Then
            dgvCustomer.Columns("Amount").HeaderText = "รวมเงิน"
            dgvCustomer.Columns("Amount").FillWeight = 100
            dgvCustomer.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvCustomer.Columns("Amount").DefaultCellStyle.Format = "N2"
        End If
    End Sub

    Public Sub ClearItems()
        EnsureCustomerTable()
        dtCustomer.Rows.Clear()

        lblTotalAmount.Text = "0.00"
        lblDiscount.Text = "0.00"
        lblTotal.Text = "0.00"
        lblCashAmount.Text = "0.00"
        lblChangeAmount.Text = "0.00"
        lblTypePay.Text = "รับเงิน"
        ShowDefaultScreen()
    End Sub

    Public Sub AddItem(itemName As String, qty As Decimal, price As Decimal, amount As Decimal)
        EnsureCustomerTable()

        Dim r = dtCustomer.NewRow()
        r("ProductName") = itemName
        r("Qty") = qty
        r("Priediscount") = price
        r("Amount") = amount

        dtCustomer.Rows.Add(r)
        dgvCustomer.Refresh()
        GridView1.RefreshData()
    End Sub
    Public Sub SetTotal(totalAmount As Decimal, discountAmount As Decimal, TotalM As Decimal)
        lblTotalAmount.Text = totalAmount.ToString("N2")
        lblDiscount.Text = discountAmount.ToString("N2")
        lblTotal.Text = TotalM.ToString("N2")
    End Sub

    Public Sub SetCustomerInfo(customerName As String, customerPoint As Decimal)
        Dim displayName As String = If(customerName, "").Trim()
        If displayName = "" Then displayName = "ลูกค้าทั่วไป"
        Label2.Text = "ยินดีต้อนรับ :) คุณ" & displayName &
                      " Point ของคุณ = " & customerPoint.ToString("N2") & " Point" &
                      " | สิทธิ์แก้วฟรี 0 สิทธิ์"
    End Sub

    Public Sub SetCustomerInfo(customerNumber As Integer, customerName As String, customerPoint As Decimal)
        Dim displayName As String = If(customerName, "").Trim()
        If displayName = "" Then displayName = "ลูกค้าทั่วไป"

        Dim cupBalance As Decimal = GetCustomerCupBalance(customerNumber)
        Dim freeCupBalance As Integer = GetCustomerFreeCupBalance(customerNumber)
        Label2.Text = "ยินดีต้อนรับ :) คุณ" & displayName &
                      " Point ของคุณ = " & customerPoint.ToString("N2") & " Point" &
                      " | สะสมแก้ว " & cupBalance.ToString("N0") & "/" & FreeCupTargetQty.ToString("N0") & " แก้ว" &
                      " | สิทธิ์แก้วฟรี " & freeCupBalance.ToString("N0") & " สิทธิ์"
    End Sub

    Public Sub ClearCustomerInfo()
        SetCustomerInfo("ลูกค้าทั่วไป", 0D)
    End Sub

    Private Function GetCustomerCupBalance(customerNumber As Integer) As Decimal
        If customerNumber <= 0 Then Return 0D

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT SellNumber, CupIn, FreeCupIn, FreeCupOut, CupBalance, FreeCupBalance, Detail " &
                    "FROM CustomerFreeCup " &
                    "WHERE CustomerNumber = @CustomerNumber " &
                    "ORDER BY ID ASC"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                    Dim table As New DataTable()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        table.Load(reader)
                    End Using
                    Dim cupBalance As Decimal = 0D
                    Dim freeCupBalance As Integer = 0
                    CustomerFreeCupBalance.ReadLatestBalance(table, cupBalance, freeCupBalance)
                    Return cupBalance
                End Using
            End Using
        Catch
            'The customer display must still show the customer even if the
            'free-cup table is not available on this database.
        End Try

        Return 0D
    End Function

    Private Function GetCustomerFreeCupBalance(customerNumber As Integer) As Integer
        If customerNumber <= 0 Then Return 0

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT SellNumber, CupIn, FreeCupIn, FreeCupOut, CupBalance, FreeCupBalance, Detail " &
                    "FROM CustomerFreeCup " &
                    "WHERE CustomerNumber = @CustomerNumber " &
                    "ORDER BY ID ASC"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                    Dim table As New DataTable()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        table.Load(reader)
                    End Using
                    Dim cupBalance As Decimal = 0D
                    Dim freeCupBalance As Integer = 0
                    CustomerFreeCupBalance.ReadLatestBalance(table, cupBalance, freeCupBalance)
                    Return Math.Max(0, freeCupBalance)
                End Using
            End Using
        Catch
            Return 0
        End Try
    End Function

    Public Sub ShowProductImage(productImage As Image)
        If productImage Is Nothing OrElse picBanner Is Nothing Then Return

        If bannerTimer IsNot Nothing Then bannerTimer.Stop()
        StopBannerFade()
        If picBanner.Image IsNot Nothing Then picBanner.Image.Dispose()
        picBanner.Image = New Bitmap(productImage)
        picBanner.SizeMode = PictureBoxSizeMode.Zoom
        picBanner.Visible = True
        picQR.Visible = False
        panelPhone.Visible = False
        If bannerOverlay IsNot Nothing Then bannerOverlay.Visible = False
        picBanner.BringToFront()
    End Sub
    Public Sub LoadBannerImages(folderPath As String)
        bannerImages.Clear()
        If Not IO.Directory.Exists(folderPath) Then Exit Sub
        bannerImages.AddRange(IO.Directory.GetFiles(folderPath, "*.jpg"))
        bannerImages.AddRange(IO.Directory.GetFiles(folderPath, "*.png"))
        bannerImages.AddRange(IO.Directory.GetFiles(folderPath, "*.jpeg"))
    End Sub
    Private Function LoadImageNoLock(filePath As String) As Image
        Using fs As New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
            Return CType(Image.FromStream(fs).Clone(), Image)
        End Using
    End Function
    Public Sub ShowRandomBanner()
        If bannerImages.Count = 0 Then Exit Sub

        Dim index As Integer = rnd.Next(0, bannerImages.Count)
        Dim filePath As String = bannerImages(index)

        ShowBannerImage(filePath, False)
    End Sub
    Private lastIndex As Integer = -1

    Public Sub ShowRandomNoRepeat()
        If bannerImages.Count = 0 OrElse Not picBanner.Visible Then Exit Sub

        Dim index As Integer

        If bannerImages.Count = 1 Then
            index = 0
        Else
            Do
                index = rnd.Next(0, bannerImages.Count)
            Loop While index = lastIndex
        End If

        lastIndex = index

        Dim filePath As String = bannerImages(index)

        ShowBannerImage(filePath, False)
    End Sub
    Private Sub ShowBannerImage(filePath As String, useFade As Boolean)
        Try
            Dim nextImage As Image = LoadImageNoLock(filePath)

            If useFade Then
                StartBannerFade(nextImage)
            Else
                ReplaceBannerImage(nextImage)
            End If
        Catch
            ReplaceBannerImage(Nothing)
        End Try
    End Sub
    Private Sub ReplaceBannerImage(nextImage As Image)
        StopBannerFade()

        If picBanner.Image IsNot Nothing Then
            picBanner.Image.Dispose()
            picBanner.Image = Nothing
        End If

        picBanner.Image = nextImage
    End Sub
    Private Sub StartBannerFade(nextImage As Image)
        If nextImage Is Nothing Then
            ReplaceBannerImage(Nothing)
            Exit Sub
        End If

        If picBanner.Image Is Nothing OrElse picBanner.Width <= 0 OrElse picBanner.Height <= 0 Then
            ReplaceBannerImage(nextImage)
            Exit Sub
        End If

        StopBannerFade()
        fadeOldImage = CType(picBanner.Image.Clone(), Image)
        fadeNewImage = nextImage
        fadeStep = 0

        If fadeTimer Is Nothing Then
            fadeTimer = New Timer()
            fadeTimer.Interval = 35
            AddHandler fadeTimer.Tick, AddressOf fadeTimer_Tick
        End If

        fadeTimer.Start()
    End Sub
    Private Sub fadeTimer_Tick(sender As Object, e As EventArgs)
        If fadeOldImage Is Nothing OrElse fadeNewImage Is Nothing Then
            StopBannerFade()
            Exit Sub
        End If

        fadeStep += 1
        Dim alpha As Single = Math.Min(1.0!, CSng(fadeStep) / CSng(FadeSteps))
        Dim blended As Image = BlendBannerImages(fadeOldImage, fadeNewImage, alpha)
        Dim previous As Image = picBanner.Image
        picBanner.Image = blended

        If previous IsNot Nothing AndAlso
           Not Object.ReferenceEquals(previous, fadeOldImage) AndAlso
           Not Object.ReferenceEquals(previous, fadeNewImage) Then
            previous.Dispose()
        End If

        If fadeStep >= FadeSteps Then
            fadeTimer.Stop()
            previous = picBanner.Image
            picBanner.Image = fadeNewImage
            If previous IsNot Nothing AndAlso Not Object.ReferenceEquals(previous, fadeNewImage) Then previous.Dispose()
            fadeNewImage = Nothing
            If fadeOldImage IsNot Nothing Then fadeOldImage.Dispose()
            fadeOldImage = Nothing
        End If
    End Sub
    Private Function BlendBannerImages(oldImage As Image, newImage As Image, alpha As Single) As Image
        Dim w As Integer = Math.Max(1, picBanner.Width)
        Dim h As Integer = Math.Max(1, picBanner.Height)
        Dim bmp As New Bitmap(w, h)

        Using g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.White)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            DrawImageWithAlpha(g, oldImage, GetZoomRectangle(oldImage, New Size(w, h)), 1.0! - alpha)
            DrawImageWithAlpha(g, newImage, GetZoomRectangle(newImage, New Size(w, h)), alpha)
        End Using

        Return bmp
    End Function
    Private Sub DrawImageWithAlpha(g As Graphics, img As Image, dest As Rectangle, alpha As Single)
        If img Is Nothing Then Exit Sub

        Dim matrix As New System.Drawing.Imaging.ColorMatrix()
        matrix.Matrix33 = Math.Max(0.0!, Math.Min(1.0!, alpha))

        Using attributes As New System.Drawing.Imaging.ImageAttributes()
            attributes.SetColorMatrix(matrix,
                                      System.Drawing.Imaging.ColorMatrixFlag.Default,
                                      System.Drawing.Imaging.ColorAdjustType.Bitmap)
            g.DrawImage(img, dest, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attributes)
        End Using
    End Sub
    Private Function GetZoomRectangle(img As Image, bounds As Size) As Rectangle
        If img Is Nothing OrElse img.Width = 0 OrElse img.Height = 0 Then
            Return New Rectangle(Point.Empty, bounds)
        End If

        Dim ratio As Single = Math.Min(CSng(bounds.Width) / CSng(img.Width), CSng(bounds.Height) / CSng(img.Height))
        Dim w As Integer = Math.Max(1, CInt(img.Width * ratio))
        Dim h As Integer = Math.Max(1, CInt(img.Height * ratio))
        Dim x As Integer = (bounds.Width - w) \ 2
        Dim y As Integer = (bounds.Height - h) \ 2
        Return New Rectangle(x, y, w, h)
    End Function
    Private Sub StopBannerFade()
        If fadeTimer IsNot Nothing Then fadeTimer.Stop()

        If fadeOldImage IsNot Nothing Then
            fadeOldImage.Dispose()
            fadeOldImage = Nothing
        End If

        If fadeNewImage IsNot Nothing Then
            fadeNewImage.Dispose()
            fadeNewImage = Nothing
        End If

        fadeStep = 0
    End Sub
    Private Sub UpdateRecommendedOverlay(filePath As String)
        Dim productName As String = ""
        Dim priceText As String = ""

        Try
            Dim pn As Integer
            If Integer.TryParse(IO.Path.GetFileNameWithoutExtension(filePath), pn) Then
                GetRecommendedProductText("Product.PN = " & pn.ToString(), productName, priceText)
            End If

            If String.IsNullOrWhiteSpace(productName) Then
                GetRecommendedProductText("(ProductGroup.CategoryID = 5 OR Category.Name = N'เครื่องดื่ม')", productName, priceText, "NEWID()")
            End If
        Catch
            productName = ""
            priceText = ""
        End Try

        If String.IsNullOrWhiteSpace(productName) Then productName = IO.Path.GetFileNameWithoutExtension(filePath)
        If String.IsNullOrWhiteSpace(productName) Then productName = "เมนูแนะนำวันนี้"

        If lblBannerName IsNot Nothing Then lblBannerName.Text = productName
        If lblBannerPrice IsNot Nothing Then lblBannerPrice.Text = priceText
        If lblBannerTag IsNot Nothing Then lblBannerTag.Text = "เมนูแนะนำ"
    End Sub
    Private Sub GetRecommendedProductText(whereClause As String,
                                          ByRef productName As String,
                                          ByRef priceText As String,
                                          Optional orderBy As String = "Product.Name ASC")
        Dim sql As New clsSQLQuery()
        sql.Selecting("TOP 1 RTRIM(Product.Name) AS ProductName, Product.Price1",
                      "Product LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID",
                      "Product.Status IS NULL AND " & whereClause,
                      orderBy)

        If sql.DTb Is Nothing OrElse sql.DTb.Rows.Count = 0 Then Exit Sub

        productName = sql.DTb.Rows(0)("ProductName").ToString()
        Dim price As Decimal
        If Decimal.TryParse(sql.DTb.Rows(0)("Price1").ToString(), price) Then
            priceText = price.ToString("N2") & " บาท"
        End If
    End Sub
    Private Sub InitBannerTimer()
        bannerTimer = New Timer()
        bannerTimer.Interval = 3000 ' 3 วินาที
        AddHandler bannerTimer.Tick, AddressOf bannerTimer_Tick
    End Sub

    Private Sub bannerTimer_Tick(sender As Object, e As EventArgs) Handles bannerTimer.Tick
        ShowRandomNoRepeat()
    End Sub
    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If bannerTimer IsNot Nothing Then
            bannerTimer.Stop()
            bannerTimer.Dispose()
        End If
        StopBannerFade()
        If fadeTimer IsNot Nothing Then
            fadeTimer.Dispose()
            fadeTimer = Nothing
        End If

        MyBase.OnFormClosing(e)
    End Sub
    Public Sub ShowQRPanel(qrImage As Image, amount As Decimal)
        If bannerTimer IsNot Nothing Then bannerTimer.Stop()
        StopBannerFade()
        If picBanner.Image IsNot Nothing Then
            picBanner.Image.Dispose()
            picBanner.Image = Nothing
        End If

        If picQR.Image IsNot Nothing Then
            picQR.Image.Dispose()
            picQR.Image = Nothing
        End If

        picQR.Image = CType(qrImage.Clone(), Image)
        picBanner.Visible = False
        If bannerOverlay IsNot Nothing Then bannerOverlay.Visible = False
        picQR.Visible = True
        picQR.BringToFront()
        ResizeQR()

        lblTypePay.Text = "เงินโอน"
        Label9.Visible = False
        lblChangeAmount.Visible = False
        lblTotalAmount.Text = amount.ToString("N2")
        lblTotal.Text = amount.ToString("N2")
    End Sub
    Public Sub SetPaymentInfo(totalAmount As Decimal, cashAmount As Decimal, changeAmount As Decimal)
        ShowDefaultScreen()
        lblTotalAmount.Text = totalAmount.ToString("N2")
        lblCashAmount.Text = cashAmount.ToString("N2")
        lblChangeAmount.Text = changeAmount.ToString("N2")
        lblTypePay.Text = "รับเงิน"
    End Sub
    Public Sub ShowDefaultScreen()
        dgvCustomer.Visible = True
        panelPhone.Visible = False
        picQR.Visible = False
        picBanner.Visible = True
        If bannerOverlay IsNot Nothing Then
            bannerOverlay.Visible = False
        End If
        Label9.Visible = True
        lblChangeAmount.Visible = True

        If bannerTimer IsNot Nothing Then bannerTimer.Start()
        If picBanner.Image Is Nothing Then ShowRandomNoRepeat()
    End Sub
    Private Sub ResizeQR()
        If PnlPicture Is Nothing OrElse picQR Is Nothing Then Return

        'ขยาย QR ตามความกว้าง/ความสูงที่เหลือของพื้นที่หน้าจอลูกค้า
        Dim availableWidth As Integer = Math.Max(1, PnlPicture.ClientSize.Width - 24)
        Dim availableHeight As Integer = Math.Max(1, PnlPicture.ClientSize.Height - 24)
        Dim qrSize As Integer = Math.Min(availableWidth, availableHeight)
        picQR.Size = New Size(qrSize, qrSize)

        CenterQR()
    End Sub

    Private Sub CenterQR()
        picQR.Left = Math.Max(20, (PnlPicture.ClientSize.Width - picQR.Width) \ 2)
        picQR.Top = Math.Max(20, (PnlPicture.ClientSize.Height - picQR.Height) \ 2)
    End Sub

    Private Sub PnlPicture_SizeChanged(sender As Object, e As EventArgs) Handles PnlPicture.SizeChanged
        If picQR IsNot Nothing AndAlso picQR.Visible Then ResizeQR()
    End Sub

    Public ReadOnly Property IsPhoneEntryVisible As Boolean
        Get
            Return panelPhone IsNot Nothing AndAlso panelPhone.Visible
        End Get
    End Property

    Public ReadOnly Property CanEnterPhone As Boolean
        Get
            Return IsPhoneEntryVisible AndAlso txtPhone IsNot Nothing AndAlso txtPhone.Enabled
        End Get
    End Property

    Public Sub ShowPhoneScreen(Optional phoneNumber As String = "")
        dgvCustomer.Visible = False
        panelPhone.Visible = True
        txtPhone.Enabled = True
        txtPhone.ReadOnly = False
        lblHint.Text = "กรุณากรอกเบอร์โทร 10 หลัก"
        lblHint.ForeColor = Color.White
        rawPhone = ""
        realMobile = ""
        txtPhone.Clear()
        txtPhone.Focus()
    End Sub
    Private Sub frmCustomerDisplay_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Activate()
        Me.BringToFront()
        txtPhone.Focus()
    End Sub
    Public Sub AppendPhoneDigit(digit As String)
        If Not CanEnterPhone Then Exit Sub

        If rawPhone.Length < 10 Then
            rawPhone &= digit
            realMobile = rawPhone
            txtPhone.Text = FormatPhone(rawPhone)
            txtPhone.SelectionStart = txtPhone.Text.Length

        End If
    End Sub

    Public Sub BackspacePhone()
        If Not CanEnterPhone Then Exit Sub

        If rawPhone.Length > 0 Then
            rawPhone = rawPhone.Substring(0, rawPhone.Length - 1)
            realMobile = rawPhone
            txtPhone.Text = FormatPhone(rawPhone)
            txtPhone.SelectionStart = txtPhone.Text.Length
        End If
    End Sub

    Public Sub ClearPhone()
        rawPhone = ""
        realMobile = ""
        txtPhone.Clear()
    End Sub

    Public Sub ShowPhoneError(message As String)
        If IsDisposed Then Return

        panelPhone.Visible = True
        panelPhone.BringToFront()
        txtPhone.Enabled = False
        txtPhone.ReadOnly = True
        lblHint.Text = message
        lblHint.ForeColor = Color.FromArgb(255, 235, 59)
        Label2.Text = "กรุณาติดต่อพนักงาน"
        lblHint.Refresh()
        Label2.Refresh()
        panelPhone.Refresh()
    End Sub

    Private Function FormatPhone(phone As String) As String
        If String.IsNullOrEmpty(phone) Then Return ""

        If phone.Length < 10 Then
            Return phone
        End If

        ' 0961237979 -> 096XXXX979
        Return phone.Substring(0, 3) &
           "XXXX" &
           phone.Substring(7, 3)
    End Function
    Private Sub GetBarcodeData()
        Try
            Dim mobile As String = If(String.IsNullOrWhiteSpace(rawPhone), realMobile, rawPhone)
            If mobile.Length <> 10 Then Exit Sub

            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("Customer.CustomerNumber,")
                sb.Append("RTRIM(Customer.CustomerCode) AS CustomerCode,")
                sb.Append("RTRIM(Customer.Name) AS Name,")
                sb.Append("Customer.MemberLevel,")
                sb.Append("Customer.Point,")
                sb.Append("Customer.Mobile ")
                .Selecting(sb.ToString, "Customer", "Customer.Mobile LIKE '%" & mobile.Replace("'", "''") & "%'", "Customer.Name ASC")

                If .DTb Is Nothing OrElse .DTb.Rows.Count = 0 Then
                    'Show a button-free message on the customer display while
                    'the cashier receives the actual OK dialog.
                    ShowPhoneError("ไม่พบเบอร์โทรศัพท์ในระบบ กรุณาติดต่อพนักงาน")
                    RaiseEvent CustomerLookupFailed("ไม่พบเบอร์โทรศัพท์ในระบบ")
                    Exit Sub
                End If

                Dim row As DataRow = .DTb.Rows(0)
                Dim customerNumber As Integer = Convert.ToInt32(row("CustomerNumber"))
                Dim customerName As String = row("Name").ToString()
                Dim customerCode As String = If(row.IsNull("CustomerCode"), "", row("CustomerCode").ToString())
                Dim memberLevel As Integer = If(row.IsNull("MemberLevel"), 0, Convert.ToInt32(row("MemberLevel")))
                Dim point As Decimal = If(row.IsNull("Point"), 0D, Convert.ToDecimal(row("Point")))
                Dim cupBalance As Decimal = GetCustomerCupBalance(customerNumber)
                Dim freeCupBalance As Integer = GetCustomerFreeCupBalance(customerNumber)

                Label2.Text = "ยินดีต้อนรับ :) คุณ" & customerName &
                              " Point ของคุณ = " & Format(point, "0.00") & " Point" &
                              " | สะสมแก้ว " & cupBalance.ToString("N0") & "/" & FreeCupTargetQty.ToString("N0") & " แก้ว" &
                              " | สิทธิ์แก้วฟรี " & freeCupBalance.ToString("N0") & " ใบ"
                lblHint.ForeColor = Color.Blue
                RaiseEvent CustomerSelected(customerNumber, customerName, customerCode, point, memberLevel)
                ShowDefaultScreen()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        If Not txtPhone.Enabled Then
            e.Handled = True
            Exit Sub
        End If

        If Char.IsDigit(e.KeyChar) Then
            e.Handled = True

            If realMobile.Length < 10 Then
                realMobile &= e.KeyChar
                rawPhone = realMobile
                txtPhone.Text = FormatPhone(realMobile)
                txtPhone.SelectionStart = txtPhone.Text.Length

            End If

        ElseIf e.KeyChar = ChrW(Keys.Back) Then
            e.Handled = True

            If realMobile.Length > 0 Then
                realMobile = realMobile.Substring(0, realMobile.Length - 1)
                rawPhone = realMobile
                txtPhone.Text = FormatPhone(realMobile)
                txtPhone.SelectionStart = txtPhone.Text.Length
            End If

        ElseIf e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True

            If realMobile.Length = 10 Then
                rawPhone = realMobile
                GetBarcodeData()
            Else
                MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ให้ครบ 10 หลัก")
                txtPhone.Focus()
                txtPhone.SelectAll()
            End If

        Else
            e.Handled = True
        End If

    End Sub
End Class







