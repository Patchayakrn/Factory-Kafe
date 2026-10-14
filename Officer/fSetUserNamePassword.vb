Option Strict On

Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class fSetUserNamePassword
    Inherits Form

    Private ReadOnly officerNumber As Long
    Private ReadOnly txtUserName As New TextBox()
    Private ReadOnly txtPassword As New TextBox()
    Private ReadOnly txtConfirmPassword As New TextBox()
    Private ReadOnly lblOfficer As New Label()
    Private ReadOnly btnSave As New Button()
    Private ReadOnly btnCancel As New Button()

    Public Sub New(officerNumberIn As Long)
        officerNumber = officerNumberIn
        InitializeForm()
        AddHandler Load, AddressOf PasswordLoad
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnCancel.Click, AddressOf CancelClick
    End Sub

    Private Sub InitializeForm()
        Text = "กำหนด User Name และ Password"
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        ClientSize = New Size(600, 310)
        Font = New Font("Cordia New", 16.0!, FontStyle.Regular)

        Dim layout As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 2, .RowCount = 6, .Padding = New Padding(18)}
        layout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 210.0!))
        layout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        For i As Integer = 0 To 3 : layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 42.0!)) : Next
        layout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 50.0!))

        layout.Controls.Add(New Label With {.Text = "พนักงาน", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleRight}, 0, 0)
        lblOfficer.Dock = DockStyle.Fill
        lblOfficer.TextAlign = ContentAlignment.MiddleLeft
        lblOfficer.ForeColor = Color.Blue
        layout.Controls.Add(lblOfficer, 1, 0)
        AddField(layout, 1, "User Name (อย่างน้อย 4 ตัว)", txtUserName)
        AddField(layout, 2, "Password (อย่างน้อย 4 ตัว)", txtPassword)
        AddField(layout, 3, "ยืนยัน Password", txtConfirmPassword)

        txtUserName.MaxLength = 19
        txtPassword.MaxLength = 19
        txtConfirmPassword.MaxLength = 19
        txtPassword.PasswordChar = "*"c
        txtConfirmPassword.PasswordChar = "*"c

        Dim buttonPanel As New FlowLayoutPanel With {.Dock = DockStyle.Fill, .FlowDirection = FlowDirection.RightToLeft, .WrapContents = False}
        btnSave.Text = "บันทึก" : btnSave.Width = 120 : btnSave.Height = 40 : btnSave.BackColor = Color.LightGreen
        btnCancel.Text = "ยกเลิก" : btnCancel.Width = 120 : btnCancel.Height = 40 : btnCancel.BackColor = Color.Salmon
        buttonPanel.Controls.Add(btnCancel) : buttonPanel.Controls.Add(btnSave)
        layout.Controls.Add(buttonPanel, 1, 5)
        AcceptButton = btnSave
        CancelButton = btnCancel
        Controls.Add(layout)
    End Sub

    Private Shared Sub AddField(layout As TableLayoutPanel, row As Integer, caption As String, editor As TextBox)
        layout.Controls.Add(New Label With {.Text = caption, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleRight}, 0, row)
        editor.Dock = DockStyle.Fill
        layout.Controls.Add(editor, 1, row)
    End Sub

    Private Sub PasswordLoad(sender As Object, e As EventArgs)
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT RTRIM(Name), RTRIM(UN) FROM Officer WHERE OfficerNumber=@OfficerNumber", conn)
                    cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        If rd.Read() Then
                            lblOfficer.Text = Convert.ToString(rd.GetValue(0)) & " (ID " & officerNumber.ToString() & ")"
                            txtUserName.Text = Convert.ToString(rd.GetValue(1)).Trim()
                        End If
                    End Using
                End Using
            End Using
            If officerNumber = 1 Then txtUserName.Enabled = False
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูล User ไม่สำเร็จ: " & ex.Message, "Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        Dim userName As String = txtUserName.Text.Trim()
        Dim password As String = txtPassword.Text
        If userName.Length < 4 Then
            MessageBox.Show("User Name ต้องมีอย่างน้อย 4 ตัวอักษร", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUserName.Focus() : Return
        End If
        If password.Length < 4 Then
            MessageBox.Show("Password ต้องมีอย่างน้อย 4 ตัวอักษร", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus() : Return
        End If
        If password <> txtConfirmPassword.Text Then
            MessageBox.Show("Password และการยืนยัน Password ไม่ตรงกัน", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmPassword.Focus() : Return
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using duplicate As New SqlCommand("SELECT COUNT(*) FROM Officer WHERE RTRIM(UN)=@UserName AND OfficerNumber<>@OfficerNumber", conn)
                    duplicate.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userName
                    duplicate.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                    If Convert.ToInt32(duplicate.ExecuteScalar()) > 0 Then
                        MessageBox.Show("User Name นี้มีอยู่แล้ว กรุณาใช้ชื่ออื่น", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        txtUserName.Focus() : Return
                    End If
                End Using

                Dim passwordValue As Decimal = Security.CalPassword(officerNumber, userName, password)
                Using update As New SqlCommand("UPDATE Officer SET UN=@UserName,PW=@Password,UONumber=@UserNumber,UTime=GETDATE() WHERE OfficerNumber=@OfficerNumber", conn)
                    update.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = userName
                    update.Parameters.Add("@Password", SqlDbType.Money).Value = passwordValue
                    update.Parameters.Add("@UserNumber", SqlDbType.Int).Value = Convert.ToInt32(Math.Max(1, ModSession.LoginOfficerID))
                    update.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                    update.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("บันทึก User Name และ Password เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show("บันทึกรหัสผ่านไม่สำเร็จ: " & ex.Message, "Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CancelClick(sender As Object, e As EventArgs)
        Close()
    End Sub
End Class
