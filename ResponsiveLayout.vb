Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Keeps fixed-size legacy forms usable on smaller monitors and different DPI
''' settings without requiring every designer file to be edited by hand.
''' </summary>
Public NotInheritable Class ResponsiveLayout
    Private Shared initialized As Boolean
    Private Shared ReadOnly appliedForms As New HashSet(Of Form)()

    Private Sub New()
    End Sub

    Public Shared Sub Initialize()
        If initialized Then Return
        initialized = True
        AddHandler Application.Idle, AddressOf Application_Idle
    End Sub

    Private Shared Sub Application_Idle(sender As Object, e As EventArgs)
        For Each form As Form In Application.OpenForms
            If form Is Nothing OrElse form.IsDisposed OrElse Not form.Visible Then Continue For
            If appliedForms.Contains(form) Then Continue For

            Apply(form)
            appliedForms.Add(form)
            AddHandler form.FormClosed, AddressOf FormClosed
        Next
    End Sub

    Private Shared Sub FormClosed(sender As Object, e As FormClosedEventArgs)
        Dim form As Form = TryCast(sender, Form)
        If form IsNot Nothing Then appliedForms.Remove(form)
    End Sub

    Private Shared Sub Apply(form As Form)
        If form.WindowState = FormWindowState.Maximized OrElse form.FormBorderStyle = FormBorderStyle.None Then Return

        Dim area As Rectangle = Screen.FromControl(form).WorkingArea
        Dim availableWidth As Integer = Math.Max(320, area.Width - 24)
        Dim availableHeight As Integer = Math.Max(240, area.Height - 48)
        Dim designWidth As Integer = Math.Max(form.ClientSize.Width, form.PreferredSize.Width)
        Dim designHeight As Integer = Math.Max(form.ClientSize.Height, form.PreferredSize.Height)

        Dim scale As Single = Math.Min(1.0F,
            Math.Min(availableWidth / CSng(Math.Max(1, designWidth)),
                     availableHeight / CSng(Math.Max(1, designHeight))))

        If scale < 0.995F Then
            form.SuspendLayout()
            Try
                form.AutoScroll = True
                form.Scale(New SizeF(scale, scale))
                form.ClientSize = New Size(
                    Math.Min(form.ClientSize.Width, availableWidth),
                    Math.Min(form.ClientSize.Height, availableHeight))
                form.StartPosition = FormStartPosition.CenterScreen
            Finally
                form.ResumeLayout(True)
            End Try
        End If
    End Sub
End Class
