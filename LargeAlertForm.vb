Imports System.Drawing
Imports System.Windows.Forms

Public Class LargeAlertForm
    Inherits Form

    Public Sub New(message As String)
        Text = "แจ้งเตือน"
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False
        ClientSize = New Size(620, 260)
        Font = New Font("Segoe UI", 14.0!, FontStyle.Regular)

        Dim iconLabel As New Label With {
            .AutoSize = False,
            .Text = "⚠",
            .ForeColor = Color.FromArgb(245, 158, 11),
            .Font = New Font("Segoe UI Symbol", 42.0!, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(22, 62),
            .Size = New Size(82, 82)
        }

        Dim messageLabel As New Label With {
            .AutoSize = False,
            .Text = message,
            .Font = New Font("Segoe UI", 24.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(30, 41, 59),
            .TextAlign = ContentAlignment.MiddleLeft,
            .Location = New Point(112, 48),
            .Size = New Size(480, 110)
        }

        Dim okButton As New Button With {
            .Text = "OK",
            .Font = New Font("Segoe UI", 16.0!, FontStyle.Bold),
            .DialogResult = DialogResult.OK,
            .Size = New Size(150, 54),
            .Location = New Point(235, 185)
        }

        Controls.Add(iconLabel)
        Controls.Add(messageLabel)
        Controls.Add(okButton)
        AcceptButton = okButton
    End Sub
End Class
