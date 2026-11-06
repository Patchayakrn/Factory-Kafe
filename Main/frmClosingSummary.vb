Imports System.Drawing
Imports System.Windows.Forms

Public Class frmClosingSummary
    Inherits Form

    Private ReadOnly summaryText As String
    Private ReadOnly txtSummary As TextBox

    Public Sub New(summary As String)
        summaryText = If(summary, String.Empty)

        Text = "สรุปยอดปิดกะ"
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MinimizeBox = False
        MaximizeBox = False
        ShowInTaskbar = False
        ClientSize = New Size(520, 620)
        BackColor = Color.White
        Font = New Font("Cordia New", 15.0!, FontStyle.Regular)

        Dim title As New Label With {
            .Text = "สรุปยอดปิดกะ",
            .Dock = DockStyle.Top,
            .Height = 48,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Cordia New", 19.0!, FontStyle.Bold),
            .BackColor = Color.FromArgb(235, 242, 249),
            .ForeColor = Color.FromArgb(35, 75, 110)
        }

        txtSummary = New TextBox With {
            .Multiline = True,
            .ReadOnly = True,
            .ScrollBars = ScrollBars.Vertical,
            .Dock = DockStyle.Fill,
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.White,
            .ForeColor = Color.FromArgb(45, 45, 45),
            .Font = New Font("Cordia New", 16.0!, FontStyle.Regular),
            .Text = summaryText,
            .Padding = New Padding(8)
        }

        Dim buttons As New FlowLayoutPanel With {
            .Dock = DockStyle.Bottom,
            .Height = 58,
            .FlowDirection = FlowDirection.RightToLeft,
            .WrapContents = False,
            .Padding = New Padding(8),
            .BackColor = Color.FromArgb(245, 247, 250)
        }

        Dim btnClose As New Button With {
            .Text = "ปิดโปรแกรม",
            .Width = 125,
            .Height = 38,
            .BackColor = Color.FromArgb(202, 75, 75),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Cordia New", 15.0!, FontStyle.Bold),
            .DialogResult = DialogResult.OK,
            .Margin = New Padding(6, 2, 0, 2)
        }
        btnClose.FlatAppearance.BorderSize = 0

        Dim btnCancel As New Button With {
            .Text = "ยกเลิก",
            .Width = 100,
            .Height = 38,
            .BackColor = Color.FromArgb(120, 120, 120),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Cordia New", 15.0!, FontStyle.Bold),
            .DialogResult = DialogResult.Cancel,
            .Margin = New Padding(6, 2, 0, 2)
        }
        btnCancel.FlatAppearance.BorderSize = 0

        Dim btnCopy As New Button With {
            .Text = "Copy",
            .Width = 90,
            .Height = 38,
            .BackColor = Color.FromArgb(45, 125, 205),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Arial", 11.0!, FontStyle.Bold),
            .Margin = New Padding(6, 2, 0, 2)
        }
        btnCopy.FlatAppearance.BorderSize = 0
        AddHandler btnCopy.Click, AddressOf CopySummary

        buttons.Controls.Add(btnClose)
        buttons.Controls.Add(btnCancel)
        buttons.Controls.Add(btnCopy)
        Controls.Add(txtSummary)
        Controls.Add(buttons)
        Controls.Add(title)
        AcceptButton = btnClose
        CancelButton = btnCancel
    End Sub

    Private Sub CopySummary(sender As Object, e As EventArgs)
        Try
            Clipboard.SetText(summaryText)
            MessageBox.Show("คัดลอกสรุปยอดแล้ว สามารถนำไปวางในไลน์ได้เลย", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("คัดลอกข้อความไม่สำเร็จ: " & ex.Message, "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
