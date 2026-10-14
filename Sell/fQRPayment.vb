Imports QRCoder
Imports System.Drawing
Public Class fQRPayment
    Public Property Amount As Decimal
    Public Property BillNo As String

    Public Property BankName As String
    Public Property AccountNo As String
    Public Property AccountName As String
    Public Property QrImagePath As String
    Public Property CustomerDisplayForm As frmCustomerDisplay
    Public Property UsePromotion As Boolean
    Public Property PromotionName As String
    Private Sub fQRPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblAmount.Text = Amount.ToString("N2")
        lblBank.Text = BankName
        lblAccountNo.Text = AccountNo
        lblBill.Text = BillNo
        lblAccountName.Text = AccountName

        If IO.File.Exists(QrImagePath) Then
            picQR.Image = Image.FromFile(QrImagePath)
            picQR.SizeMode = PictureBoxSizeMode.Zoom
        Else
            MessageBox.Show("ไม่พบไฟล์ QR Code", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub btnPaid_Click(sender As Object, e As EventArgs) Handles btnPaid.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub btnCopyAccount_Click(sender As Object, e As EventArgs)
        Clipboard.SetText(AccountNo)
        MessageBox.Show("คัดลอกเลขที่บัญชีแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public ReadOnly Property QRImageForCustomer As Image
        Get
            If picQR.Image Is Nothing Then Return Nothing   ' เปลี่ยน PictureBox1 เป็นชื่อจริงของ QR
            Return CType(picQR.Image.Clone(), Image)
        End Get
    End Property

    'Public ReadOnly Property BillNoText As String
    '    Get
    '        Return lblBillNo.Text   ' เปลี่ยนเป็นชื่อ label จริง
    '    End Get
    'End Property

    Public ReadOnly Property TotalAmountValue As Decimal
        Get
            Return Val(lblAmount.Text)   ' เปลี่ยนเป็นชื่อ label จริง
        End Get
    End Property
    Private Sub fQRPayment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If CustomerDisplayForm IsNot Nothing AndAlso QRImageForCustomer IsNot Nothing Then
            CustomerDisplayForm.ShowQRPanel(QRImageForCustomer, TotalAmountValue)
        End If
    End Sub
    Private Sub fQRPayment_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If CustomerDisplayForm IsNot Nothing Then
            CustomerDisplayForm.ShowDefaultScreen()
        End If
    End Sub
End Class
