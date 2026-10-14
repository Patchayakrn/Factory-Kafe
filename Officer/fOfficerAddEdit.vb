Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class fOfficerAddEdit
    Inherits Form

    Private ReadOnly officerNumber As Long

    Public Sub New(number As Long)
        officerNumber = number
        InitializeComponent()
        Text = If(number = 0, "เพิ่มพนักงาน", "แก้ไขพนักงาน")
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler Load, AddressOf DialogLoad
    End Sub

    Private Sub DialogLoad(sender As Object, e As EventArgs)
        If officerNumber = 0 Then Return
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT OfficerCode, Barcode, Name, Pos, Mobile, Status FROM Officer WHERE OfficerNumber=@OfficerNumber", conn)
                    cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtCode.Text = If(reader.IsDBNull(0), "", reader.GetString(0).Trim())
                            txtBarcode.Text = If(reader.IsDBNull(1), "", reader.GetString(1).Trim())
                            txtName.Text = If(reader.IsDBNull(2), "", reader.GetString(2).Trim())
                            txtPosition.Text = If(reader.IsDBNull(3), "", reader.GetString(3).Trim())
                            txtMobile.Text = If(reader.IsDBNull(4), "", reader.GetString(4).Trim())
                            chkInactive.Checked = Not reader.IsDBNull(5) AndAlso Convert.ToInt32(reader.GetValue(5)) = 1
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ไม่สามารถโหลดข้อมูล Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DialogResult = DialogResult.Cancel
        End Try
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("กรุณาระบุชื่อพนักงาน", "Officer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                If officerNumber = 0 Then
                    Dim nextNumber As Integer
                    Using nextCmd As New SqlCommand("SELECT ISNULL(MAX(OfficerNumber), 0) + 1 FROM Officer", conn)
                        nextNumber = Convert.ToInt32(nextCmd.ExecuteScalar())
                    End Using

                    Dim insertSql As String =
                        "INSERT INTO Officer (OfficerNumber, OfficerCode, Name, Sex, Barcode, Pos, Salary, Mobile, UN, PW, Class, Status, CONumber, UONumber, CTime, UTime) " &
                        "VALUES (@OfficerNumber, @OfficerCode, @Name, 0, @Barcode, @Pos, @Salary, @Mobile, '', 0, 0, @Status, @UserNumber, @UserNumber, GETDATE(), GETDATE())"
                    Using cmd As New SqlCommand(insertSql, conn)
                        AddParameters(cmd, nextNumber)
                        cmd.ExecuteNonQuery()
                    End Using
                Else
                    Dim updateSql As String =
                        "UPDATE Officer SET OfficerCode=@OfficerCode, Name=@Name, Barcode=@Barcode, Pos=@Pos, Mobile=@Mobile, " &
                        "Status=@Status, UONumber=@UserNumber, UTime=GETDATE() WHERE OfficerNumber=@OfficerNumber"
                    Using cmd As New SqlCommand(updateSql, conn)
                        AddParameters(cmd, Convert.ToInt32(officerNumber))
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            End Using
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ไม่สามารถบันทึก Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddParameters(cmd As SqlCommand, number As Integer)
        cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = number
        AddNullableText(cmd, "@OfficerCode", txtCode.Text)
        AddNullableText(cmd, "@Name", txtName.Text)
        AddNullableText(cmd, "@Barcode", txtBarcode.Text)
        AddNullableText(cmd, "@Pos", txtPosition.Text)
        cmd.Parameters.Add("@Salary", SqlDbType.Money).Value = 0D
        AddNullableText(cmd, "@Mobile", txtMobile.Text)
        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(chkInactive.Checked, 1, CType(DBNull.Value, Object))
        cmd.Parameters.Add("@UserNumber", SqlDbType.Int).Value = Convert.ToInt32(ModSession.LoginOfficerID)
    End Sub

    Private Shared Sub AddNullableText(cmd As SqlCommand, name As String, value As String)
        cmd.Parameters.Add(name, SqlDbType.NVarChar, 255).Value = If(String.IsNullOrWhiteSpace(value), CType(DBNull.Value, Object), value.Trim())
    End Sub


End Class
