Public Class DrinkMenuCard

    Private ReadOnly priceLabel As New Label()
    Private ReadOnly disabledOverlay As New Panel()

    Public Event CardClicked As EventHandler
    Public Property ProductNumber As Long
    Public Property ProductCode As String = ""
    Public Property Barcode As String = ""
    Public Property MenuProductName As String = ""
    Public Property UnitName As String = ""
    Public Property SellPrice As Decimal
    Public Property ProductComment As String = ""
    Public Property HasHot As Boolean
    Public Property HasIced As Boolean
    Public Property HasBlended As Boolean
    Public Property HotPrice As Decimal
    Public Property IcedPrice As Decimal
    Public Property BlendedPrice As Decimal
    Public Property SignaturePrice As Decimal
    'ตัวตนของสินค้าแต่ละชนิดต้องแยกจาก PN หลักของการ์ด
    Public Property HotProductNumber As Long
    Public Property IcedProductNumber As Long
    Public Property BlendedProductNumber As Long
    Public Property SignatureProductNumber As Long
    Public Property HotProductCode As String = ""
    Public Property IcedProductCode As String = ""
    Public Property BlendedProductCode As String = ""
    Public Property SignatureProductCode As String = ""
    Public Property HotBarcode As String = ""
    Public Property IcedBarcode As String = ""
    Public Property BlendedBarcode As String = ""
    Public Property SignatureBarcode As String = ""
    Public Property SkipBeverageOption As Boolean
    Public Property ProductGroupName As String = ""

    Public Sub New()
        InitializeComponent()

        Me.Cursor = Cursors.Hand
        Me.AutoSize = False
        Me.BackColor = Color.White
        Me.BorderStyle = BorderStyle.FixedSingle
        Me.Padding = New Padding(3)
        PictureBox1.Cursor = Cursors.Hand
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Margin = New Padding(0)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.BackColor = Color.FromArgb(248, 250, 252)
        KryptonLabel1.Cursor = Cursors.Hand
        KryptonLabel1.AutoSize = False
        KryptonLabel1.Dock = DockStyle.Bottom
        KryptonLabel1.Height = 26
        KryptonLabel1.StateCommon.ShortText.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        KryptonLabel1.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center
        KryptonLabel1.StateCommon.ShortText.Color1 = Color.FromArgb(37, 99, 235)
        priceLabel.Visible = False

        'แผ่นสีจางสำหรับบอกผู้ใช้ว่าการ์ดยังเลือกไม่ได้
        disabledOverlay.Dock = DockStyle.Fill
        disabledOverlay.BackColor = Color.FromArgb(150, 238, 242, 247)
        disabledOverlay.Visible = False
        disabledOverlay.Enabled = False
        Me.Controls.Add(disabledOverlay)
        disabledOverlay.BringToFront()

        AddHandler Me.Click, AddressOf RaiseCardClicked
        AddHandler PictureBox1.Click, AddressOf RaiseCardClicked
        AddHandler KryptonLabel1.Click, AddressOf RaiseCardClicked
    End Sub

    Public Sub SetSelectionVisualState(canSelect As Boolean)
        disabledOverlay.Visible = Not canSelect
        Me.Cursor = If(canSelect, Cursors.Hand, Cursors.Default)
        PictureBox1.Cursor = If(canSelect, Cursors.Hand, Cursors.Default)
        KryptonLabel1.Cursor = If(canSelect, Cursors.Hand, Cursors.Default)
        KryptonLabel1.StateCommon.ShortText.Color1 = If(canSelect,
                                                         Color.FromArgb(37, 99, 235),
                                                         Color.FromArgb(145, 155, 170))
        If Not canSelect Then
            Me.BackColor = Color.FromArgb(238, 242, 247)
            PictureBox1.BackColor = Color.FromArgb(232, 236, 241)
        Else
            Me.BackColor = Color.White
            PictureBox1.BackColor = Color.FromArgb(248, 250, 252)
        End If
    End Sub

    Public Sub SetProduct(productNumber As Long, productCode As String, barcode As String,
                           productNameValue As String, unitNameValue As String, price As Decimal,
                           comment As String, productImage As Image)
        ProductNumber = productNumber
        ProductCode = productCode
        Barcode = barcode
        MenuProductName = productNameValue
        UnitName = unitNameValue
        SellPrice = price
        ProductComment = comment
        KryptonLabel1.Values.Text = MenuProductName
        priceLabel.Text = SellPrice.ToString("N2") & " บาท"

        If PictureBox1.Image IsNot Nothing Then PictureBox1.Image.Dispose()
        PictureBox1.Image = productImage
        If PictureBox1.Image Is Nothing Then
            PictureBox1.Image = CreateImagePlaceholder()
        End If
    End Sub

    Private Function CreateImagePlaceholder() As Image
        Dim image As New Bitmap(Math.Max(1, PictureBox1.Width), Math.Max(1, PictureBox1.Height))
        Using g As Graphics = Graphics.FromImage(image)
            g.Clear(Color.FromArgb(248, 250, 252))
            Using pen As New Pen(Color.FromArgb(190, 200, 212), 2.0F)
                Dim box As New Rectangle((image.Width - 42) \ 2, (image.Height - 34) \ 2, 42, 34)
                g.DrawRectangle(pen, box)
                g.DrawLine(pen, box.Left + 5, box.Bottom - 7, box.Left + 16, box.Top + 15)
                g.DrawLine(pen, box.Left + 16, box.Top + 15, box.Left + 25, box.Bottom - 11)
                g.DrawLine(pen, box.Left + 25, box.Bottom - 11, box.Right - 5, box.Top + 7)
                g.FillEllipse(Brushes.LightGray, box.Left + 27, box.Top + 6, 6, 6)
            End Using
            Using font As New Font("Cordia New", 12.0F)
                Using brush As New SolidBrush(Color.FromArgb(120, 130, 145))
                    Dim text As String = "ยังไม่มีรูปสินค้า"
                    Dim size As SizeF = g.MeasureString(text, font)
                    g.DrawString(text, font, brush, (image.Width - size.Width) / 2.0F, image.Height - size.Height - 8.0F)
                End Using
            End Using
        End Using
        Return image
    End Function

    Private Sub RaiseCardClicked(sender As Object, e As EventArgs)
        RaiseEvent CardClicked(Me, e)
    End Sub

End Class
