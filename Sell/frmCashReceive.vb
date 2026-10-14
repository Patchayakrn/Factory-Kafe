Public Class frmCashReceive
    Public Property ChangeMoney As Decimal
    Private Sub frmCashReceive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblChange.Text = ChangeMoney.ToString("N2")
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class