Imports System.Drawing
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class FrmBeverageOption
    Public Property HotPrice As Decimal
    Public Property IcedPrice As Decimal
    Public Property BlendedPrice As Decimal
    Public Property SignaturePrice As Decimal
    Public Property SelectedSweetness As String = "0%"
    Public Property EnableSignature As Boolean
    'ใช้กับหมวดสมูทตี้ ซึ่งขายเป็นเมนูปั่นเท่านั้น
    Public Property ForceBlended As Boolean

    Public Property SelectedPrice As Decimal
    Public ReadOnly Property SelectedToppings As New List(Of String)()
    Public Property SelectedToppingAmount As Decimal
    Public Property SelectedType As String = "เย็น"


    Private Sub FrmBeverageOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rdoS100.Checked = True
        btnHot.Enabled = HotPrice > 0D
        btnIced.Enabled = IcedPrice > 0D
        btnFrappe.Enabled = BlendedPrice > 0D
        btnSignature.Enabled = EnableSignature

        If IcedPrice > 0D Then
            SelectedPrice = IcedPrice
            SelectedType = "เย็น"
        ElseIf HotPrice > 0D Then
            SelectedPrice = HotPrice
            SelectedType = "ร้อน"
        Else
            SelectedPrice = BlendedPrice
            SelectedType = "ปั่น"
        End If

        UpdateDetailText()

        AddHandler btnHot.Click, AddressOf HotButton_Click
        AddHandler btnIced.Click, AddressOf IcedButton_Click
        AddHandler btnFrappe.Click, AddressOf BlendedButton_Click

        If ForceBlended Then
            btnHot.Enabled = False
            btnIced.Enabled = False
            btnSignature.Enabled = False
            btnFrappe.Enabled = BlendedPrice > 0D

            'เลือกปั่นให้ทันที และไม่เปิดโอกาสให้กลับไปเลือกเย็น/ร้อน
            If btnFrappe.Enabled Then
                BlendedButton_Click(btnFrappe, EventArgs.Empty)
            Else
                SelectedPrice = 0D
                SelectedType = "ปั่น"
            End If
        End If

        'ทำให้ปุ่มที่ไม่มีตัวเลือกเห็นเป็นสีจางชัดเจน
        ApplyOptionButtonState(btnHot, btnHot.Enabled, Color.MistyRose)
        ApplyOptionButtonState(btnIced, btnIced.Enabled, Color.PowderBlue)
        ApplyOptionButtonState(btnFrappe, btnFrappe.Enabled, Color.Honeydew)
        ApplyOptionButtonState(btnSignature, btnSignature.Enabled, Color.RosyBrown)

    End Sub

    Private Sub ApplyOptionButtonState(button As Button, isAvailable As Boolean, availableColor As Color)
        If button Is Nothing Then Return

        button.UseVisualStyleBackColor = False
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 1
        If isAvailable Then
            button.BackColor = availableColor
            button.ForeColor = Color.Black
            button.Cursor = Cursors.Hand
        Else
            button.BackColor = Color.FromArgb(232, 232, 232)
            button.ForeColor = Color.FromArgb(155, 155, 155)
            button.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub HotButton_Click(sender As Object, e As EventArgs)
        txtPrice.Text = HotPrice.ToString("N2")
        SelectedPrice = HotPrice
        SelectedType = "ร้อน"
        UpdateDetailText()
    End Sub

    Private Sub IcedButton_Click(sender As Object, e As EventArgs)
        txtPrice.Text = IcedPrice.ToString("N2")
        SelectedPrice = IcedPrice
        SelectedType = "เย็น"
        UpdateDetailText()
    End Sub

    Private Sub BlendedButton_Click(sender As Object, e As EventArgs)
        txtPrice.Text = BlendedPrice.ToString("N2")
        SelectedPrice = BlendedPrice
        SelectedType = "ปั่น"
        UpdateDetailText()
    End Sub

    Private Sub UpdateDetailText()
        Dim menuName As String = If(txtNameMenu.Text, "").Trim()
        Dim drinkType As String = If(SelectedType, "").Trim()
        Dim sweetness As String = If(SelectedSweetness, "").Trim()

        Dim detail As String = menuName
        If drinkType <> "" Then detail &= " " & drinkType
        If sweetness <> "" Then detail &= " " & sweetness
        txtDetail.Text = detail.Trim()
    End Sub

    Private Sub rdoS0_CheckedChanged(sender As Object, e As EventArgs) Handles rdoS0.CheckedChanged
        SelectedSweetness = "0% ไม่หวาน"
        UpdateDetailText()
    End Sub
    Private Sub rdoS25_CheckedChanged(sender As Object, e As EventArgs) Handles rdoS25.CheckedChanged
        SelectedSweetness = "25% หวานจุบจิบ"
        UpdateDetailText()
    End Sub

    Private Sub rdoS50_CheckedChanged(sender As Object, e As EventArgs) Handles rdoS50.CheckedChanged
        SelectedSweetness = "50% หวานน้อย"
        UpdateDetailText()
    End Sub

    Private Sub rdoS100_CheckedChanged(sender As Object, e As EventArgs) Handles rdoS100.CheckedChanged
        SelectedSweetness = "100% หวานพอดี"
        UpdateDetailText()
    End Sub

    Private Sub rdoS120_CheckedChanged(sender As Object, e As EventArgs) Handles rdoS120.CheckedChanged
        SelectedSweetness = "120% หวานจึ้ง"
        UpdateDetailText()
    End Sub

    Private Sub btnAddToBill_Click(sender As Object, e As EventArgs) Handles btnAddToBill.Click
        SelectedToppings.Clear()
        SelectedToppingAmount = 0D
        AddToppingIfChecked(KryptonCheckBox1, Label9)
        AddToppingIfChecked(KryptonCheckBox2, Label1)
        AddToppingIfChecked(KryptonCheckBox3, Label2)
        AddToppingIfChecked(KryptonCheckBox4, Label3)
        AddToppingIfChecked(KryptonCheckBox5, Label4)

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub AddToppingIfChecked(checkBox As Krypton.Toolkit.KryptonCheckBox, priceLabel As Label)
        If checkBox Is Nothing OrElse Not checkBox.Checked Then Return

        SelectedToppings.Add(checkBox.Values.Text.Trim())
        If priceLabel Is Nothing Then Return

        Dim match = Regex.Match(priceLabel.Text, "[-+]?\d+(?:\.\d+)?")
        If match.Success Then
            SelectedToppingAmount += Decimal.Parse(match.Value, Globalization.CultureInfo.InvariantCulture)
        End If
    End Sub

    Private Sub btnSignature_Click(sender As Object, e As EventArgs) Handles btnSignature.Click
        SelectedType = "ซิกเนเจอร์"
        If SignaturePrice > 0D Then
            SelectedPrice = SignaturePrice
            txtPrice.Text = SignaturePrice.ToString("N2")
        End If
        UpdateDetailText()
    End Sub

End Class
