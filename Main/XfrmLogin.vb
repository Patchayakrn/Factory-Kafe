
Imports System.Collections.Specialized.BitVector32
Imports System.Drawing.Drawing2D
Imports System.Data.SqlClient
Imports System.Resources
Imports System.ComponentModel
Public Class XfrmLogin

    Dim Progressbar As Double
    Dim ProgressbarWidth, Progressbarheight, ProgressbarComplete As Integer
    Dim bit As Bitmap
    Dim g As Graphics
    Public IDUser As String
    Public EmpCodeLogin As String
    Dim TextStatus As String
    Public ComID As String
    Dim Empcard As String
    Dim officerName As String
    Private Sub XfrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeRoundedButton(btnLogin, 20) ' ปรับค่า 20 = ความโค้ง
        LoadDatabaseChoices()
        Me.BeginInvoke(New MethodInvoker(Sub() txtUsername.Focus()))
        Me.CenterToScreen()
        lblVer.Text = "Version 26.09.03"

    End Sub

    Private Sub MakeRoundedButton(btn As Button, radius As Integer)
        Dim path As New GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        path.AddArc(New Rectangle(btn.Width - radius, 0, radius, radius), -90, 90)
        path.AddArc(New Rectangle(btn.Width - radius, btn.Height - radius, radius, radius), 0, 90)
        path.AddArc(New Rectangle(0, btn.Height - radius, radius, radius), 90, 90)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim originalString As String = txtUsername.Text
            If txtUsername.Text = "" Then
                txtUsername.Focus()
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "Error !")
                Exit Sub
            Else
                Call CheckLogin()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True 'กันเสียง beep และ refresh textbox
            txtpass.Focus()
        End If
    End Sub
    Private Sub txtpass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpass.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Call CheckLogin()
        End If
    End Sub
    'Function CheckLogin() As Boolean
    '    Try
    '        Dim isLoginSuccess As Boolean = False
    '        clsSQL = New clsSQLQuery
    '        With clsSQL

    '            .Selecting("OfficerNumber,OfficerCode,Name,PW", "Officer", "UN = '" & txtUsername.Text & "'", "Name ASC ")
    '            If .IsHasRow Then
    '                Dim officerID As Long = CLng(.DTb.Rows(0)("OfficerNumber"))
    '                Dim dbPW As Decimal = CDec(.DTb.Rows(0)("PW"))
    '                officerName = CStr(.DTb.Rows(0)("Name"))
    '                ' 2️⃣ คำนวณรหัสผ่านจากสูตร
    '                Dim calPW As Decimal = .CalPassword(officerID, txtUsername.Text, txtpass.Text)
    '                If dbPW = calPW Then

    '                    IDUser = officerID.ToString()
    '                    EmpCodeLogin =
    '                    .DTb.Rows(0)("OfficerCode").ToString()
    '                    fsell.WindowState = FormWindowState.Maximized
    '                    fsell.StartPosition = FormStartPosition.CenterScreen
    '                    fsell.Show()
    '                    isLoginSuccess = True

    '                Else
    '                    MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "Information")
    '                    isLoginSuccess = False
    '                End If

    '            Else
    '                MessageBox.Show("ไม่พบ User นี้", "Information")
    '                txtUsername.Text = ""
    '                isLoginSuccess = False
    '            End If

    '        End With

    '        Return isLoginSuccess

    '    Catch ex As Exception
    '        MessageBox.Show("เกิดข้อผิดพลาด: " & ex.Message,
    '                    "Error",
    '                    MessageBoxButtons.OK,
    '                    MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function
    Function CheckLogin() As Boolean
        Try
            Dim un As String = txtUsername.Text.Trim()
            Dim pwText As String = txtpass.Text.Trim()
            Dim userFound As Boolean = False
            Dim choices As List(Of DatabaseChoice) = GetLoginDatabaseChoices()

            For Each choice As DatabaseChoice In choices
                modVariable.DbConnectionStringOverride = choice.ConnectionString

                If TryLoginWithCurrentDatabase(un, pwText, userFound) Then
                    cboDatabase.SelectedItem = choice
                    OpenMainSellForm()
                    Return True
                End If
            Next

            If userFound Then
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "Information")
                txtpass.Text = ""
                txtpass.Focus()
            Else
                MessageBox.Show("ไม่พบ User นี้ในฐานข้อมูลที่ตรวจพบ", "Information")
                txtUsername.Text = ""
                txtpass.Text = ""
                txtUsername.Focus()
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("เกิดข้อผิดพลาด: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function TryLoginWithCurrentDatabase(un As String, pwText As String, ByRef userFound As Boolean) As Boolean
        Dim oldSuppressErrorMessage As Boolean = clsSQLQuery.SuppressErrorMessage

        Try
            clsSQLQuery.SuppressErrorMessage = True

            Dim unSql As String = un.Replace("'", "''")

            clsSQL = New clsSQLQuery
            With clsSQL
                .Selecting("OfficerNumber,OfficerCode,Name,PW", "Officer", "UN = '" & unSql & "'", "Name ASC")

                If .IsError OrElse Not .IsHasRow Then
                    Return False
                End If

                userFound = True

                Dim officerID As Long = CLng(.DTb.Rows(0)("OfficerNumber"))
                Dim dbPW As Decimal = CDec(.DTb.Rows(0)("PW"))
                Dim officerCode As String = .DTb.Rows(0)("OfficerCode").ToString()
                Dim officerName As String = .DTb.Rows(0)("Name").ToString()
                Dim calPW As Decimal = Security.CalPassword(officerID, un, pwText)

                If dbPW <> calPW Then
                    Return False
                End If

                ModSession.LoginOfficerID = officerID
                ModSession.LoginOfficerCode = officerCode
                ModSession.LoginUserName = un
                ModSession.LoginName = officerName

                IDUser = officerID.ToString()
                EmpCodeLogin = officerCode
                Return True
            End With
        Catch
            Return False
        Finally
            clsSQLQuery.SuppressErrorMessage = oldSuppressErrorMessage
        End Try
    End Function

    Private Function GetLoginDatabaseChoices() As List(Of DatabaseChoice)
        Dim choices As New List(Of DatabaseChoice)()
        Dim selected = TryCast(cboDatabase.SelectedItem, DatabaseChoice)

        If selected IsNot Nothing Then
            choices.Add(selected)
        End If

        For Each item As Object In cboDatabase.Items
            Dim choice = TryCast(item, DatabaseChoice)
            If choice Is Nothing Then Continue For

            Dim exists As Boolean = False
            For Each current As DatabaseChoice In choices
                If String.Equals(current.ConnectionString, choice.ConnectionString, StringComparison.OrdinalIgnoreCase) Then
                    exists = True
                    Exit For
                End If
            Next

            If Not exists Then
                choices.Add(choice)
            End If
        Next

        Return choices
    End Function

    Private Sub OpenMainSellForm()
        Dim frm As New Fmailvb()
        frm.WindowState = FormWindowState.Maximized
        frm.StartPosition = FormStartPosition.CenterScreen
        frm.Show()
        Me.Hide()
    End Sub

    Private Class DatabaseChoice
        Public Property DisplayName As String
        Public Property ConnectionString As String

        Public Sub New(displayName As String, connectionString As String)
            Me.DisplayName = displayName
            Me.ConnectionString = connectionString
        End Sub

        Public Overrides Function ToString() As String
            Return DisplayName
        End Function
    End Class

    Private Sub LoadDatabaseChoices()
        ResponsiveLayout.Initialize()
        cboDatabase.Items.Clear()
        cboDatabase.Items.Add(New DatabaseChoice("Localhost - NewStock (Windows)", "Server=localhost;Database=NewStock;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"))
        cboDatabase.Items.Add(New DatabaseChoice("SQLEXPRESS - NewStock (sa)", "Server=localhost\SQLEXPRESS;Database=NewStock;User Id=sa;Password=GiftGift;TrustServerCertificate=True;"))

        For i As Integer = 0 To cboDatabase.Items.Count - 1
            Dim choice = TryCast(cboDatabase.Items(i), DatabaseChoice)
            If choice IsNot Nothing AndAlso TestDatabaseConnection(choice.ConnectionString) Then
                cboDatabase.SelectedIndex = i
                ApplySelectedDatabase()
                Exit Sub
            End If
        Next

        If cboDatabase.Items.Count > 0 Then
            cboDatabase.SelectedIndex = 0
            ApplySelectedDatabase()
        End If
    End Sub

    Private Function TestDatabaseConnection(connectionString As String) As Boolean
        Try
            Dim builder As New SqlConnectionStringBuilder(connectionString)
            builder.ConnectTimeout = 2

            Using conn As New SqlConnection(builder.ConnectionString)
                conn.Open()
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cboDatabase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDatabase.SelectedIndexChanged
        ApplySelectedDatabase()
    End Sub

    Private Sub ApplySelectedDatabase()
        Dim selected = TryCast(cboDatabase.SelectedItem, DatabaseChoice)
        If selected Is Nothing Then
            modVariable.DbConnectionStringOverride = ""
            Exit Sub
        End If

        modVariable.DbConnectionStringOverride = selected.ConnectionString
    End Sub
End Class


