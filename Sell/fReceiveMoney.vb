Imports System.Data.Linq

Public Class fReceiveMoney
    Public Property TotalDue As Decimal
    Public Property UsePromotion As Boolean
    Public Property CustomerDisplayForm As frmCustomerDisplay

    Public ReadOnly Property ReceiveMoney As Decimal
        Get
            Return _receive
        End Get
    End Property

    Public ReadOnly Property ChangeMoney As Decimal
        Get
            Return _change
        End Get
    End Property

    Private _input As String = "0"
    Private _handlingKeyboardInput As Boolean = False
    Private _receive As Decimal = 0D
    Private _change As Decimal = 0D

    Private Sub fReceiveMoney_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblTotal.Text = TotalDue.ToString("N2")
        SetInput("0")
        UpdateCalc()
        WireKeypad(Me)
        Me.KeyPreview = True
        BeginInvoke(New MethodInvoker(AddressOf FocusReceiveInput))
    End Sub

    Private Sub FocusReceiveInput()
        If txtReceive Is Nothing Then Exit Sub
        txtReceive.Focus()
        txtReceive.SelectAll()
    End Sub
    Private Sub WireKeypad(parent As Control)
        For Each c As Control In parent.Controls
            If TypeOf c Is Button Then
                Dim b = DirectCast(c, Button)
                If b.Tag IsNot Nothing Then
                    AddHandler b.Click, AddressOf Keypad_Click
                End If
            End If
            If c.HasChildren Then WireKeypad(c)
        Next
    End Sub
    Private Sub Keypad_Click(sender As Object, e As EventArgs)
        Dim b = DirectCast(sender, Button)
        Dim k As String = Convert.ToString(b.Tag)

        If k = "." Then
            If Not _input.Contains("."c) Then
                _input &= "."
            End If
        Else
            ' ตัวเลข
            If _input = "0" Then
                _input = k
            Else
                _input &= k
            End If
        End If

        CommitInput()
    End Sub
    Private Sub AddCash(amount As Decimal)
        CommitInput() ' ให้ parse ก่อน
        _receive += amount
        _input = _receive.ToString("0.##") ' เก็บเป็น string ต่อได้
        CommitInput()
    End Sub

    Private Sub btn20_Click(sender As Object, e As EventArgs) Handles btn20.Click
        AddCash(20D)
    End Sub
    Private Sub btn50_Click(sender As Object, e As EventArgs) Handles btn50.Click
        AddCash(50D)
    End Sub
    Private Sub btn100_Click(sender As Object, e As EventArgs) Handles btn100.Click
        AddCash(100D)
    End Sub
    Private Sub btn500_Click(sender As Object, e As EventArgs) Handles btn500.Click
        AddCash(500D)
    End Sub
    Private Sub btn1000_Click(sender As Object, e As EventArgs) Handles btn1000.Click
        AddCash(1000D)
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        SetInput("0")
        UpdateCalc()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If _input.Length <= 1 Then
            _input = "0"
        Else
            _input = _input.Substring(0, _input.Length - 1)
            If _input = "-" OrElse _input = "" Then _input = "0"
        End If
        CommitInput()
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        CommitInput()

        If _receive < TotalDue Then
            MessageBox.Show("เงินที่รับน้อยกว่ายอดชำระ", "แจ้งเตือน",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim changeAmt As Decimal = _receive - TotalDue

        Using frmChange As New frmCashReceive
            frmChange.ChangeMoney = changeAmt

            If frmChange.ShowDialog() = DialogResult.OK Then
                _change = _receive - TotalDue

                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End Using

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub fReceiveCash_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOK.PerformClick()
            e.Handled = True
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancel.PerformClick()
            e.Handled = True
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Delete Then
            btnClear.PerformClick()
            e.Handled = True
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Back Then
            btnBack.PerformClick()
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub fReceiveMoney_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If _handlingKeyboardInput Then Return

        If Char.IsDigit(e.KeyChar) Then
            _handlingKeyboardInput = True
            AppendNumber(e.KeyChar.ToString())
            FocusReceiveInput()
            _handlingKeyboardInput = False
            e.Handled = True
        ElseIf e.KeyChar = "."c Then
            _handlingKeyboardInput = True
            If Not _input.Contains(".") Then
                _input &= "."
                CommitInput()
            End If
            FocusReceiveInput()
            _handlingKeyboardInput = False
            e.Handled = True
        End If
    End Sub

    Private Sub SetInput(s As String)
        _input = If(String.IsNullOrWhiteSpace(s), "0", s)
        CommitInput()
    End Sub

    Private Sub CommitInput()
        Dim tmp As Decimal
        If Decimal.TryParse(_input, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, tmp) _
           OrElse Decimal.TryParse(_input, tmp) Then
            _receive = tmp
        Else
            _receive = 0D
        End If

        txtReceive.Text = _receive.ToString("N2")
        UpdateCalc()
    End Sub
    Private Sub UpdateCalc()
        _change = _receive - TotalDue
        If _change < 0D Then _change = 0D
        lblChange.Text = _change.ToString("N2")
        ' ส่งไปจอลูกค้า
        UpdateCustomerPaymentDisplay()

    End Sub
    Private Sub AppendNumber(n As String)

        If _input = "0" Then
            _input = n
        Else
            _input &= n
        End If

        CommitInput()

    End Sub
    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        AppendNumber("1")
    End Sub
    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        AppendNumber("2")
    End Sub
    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        AppendNumber("3")
    End Sub
    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        AppendNumber("4")
    End Sub
    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        AppendNumber("5")
    End Sub
    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        AppendNumber("6")
    End Sub

    Private Sub btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click
        AppendNumber("7")
    End Sub

    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        AppendNumber("8")
    End Sub

    Private Sub btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click
        AppendNumber("9")
    End Sub

    Private Sub btn0_Click(sender As Object, e As EventArgs) Handles btn0.Click
        AppendNumber("0")
    End Sub

    Private Sub btnDot_Click(sender As Object, e As EventArgs) Handles btnDot.Click
        If Not _input.Contains(".") Then
            _input &= "."
            CommitInput()
        End If
    End Sub
    Private Sub UpdateCustomerPaymentDisplay()
        If CustomerDisplayForm Is Nothing OrElse CustomerDisplayForm.IsDisposed Then Exit Sub

        Dim totalAmount As Decimal = Val(lblTotal.Text)
        Dim cashAmount As Decimal = Val(txtReceive.Text)
        Dim changeAmount As Decimal = Val(lblChange.Text)

        CustomerDisplayForm.SetPaymentInfo(totalAmount, cashAmount, changeAmount)
    End Sub


End Class

