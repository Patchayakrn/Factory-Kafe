Option Explicit On
Option Strict On
Imports System.Text
Imports System.Data.SqlClient
Imports System.Globalization
Public Class clsSQLQuery
    Public Shared SuppressErrorMessage As Boolean = False
    Public SqlIns As String                         'เก็บ SQL คำสั่ง
    Public DTb As New DataTable                     'รับค่าข้อมูลในตาราง dt ที่เลือกเข้ามา
    Public IsError As Boolean = False               'ตรวจสอบการข้อผิดพลาด
    Friend IsHasRow As Boolean = False              'ตรวจสอบว่า มีข้อมูลอยู่หรือไม่ 
    Public m_Error As String                        'เก็บค่าที่ Error
    'ประกาศตัวแปรเพื่อบันทึกข้อมูล
    Public m1_Fild As String                        '1 เก็บ Fild ที่จะบันทึก
    Public m2_item As String                        '2 เก็บไอเทมทั้งที่จะบันทึก
    Public m3_Value() As String                     '3 เก็บตัวแปลที่จะบันทึก
    Public m4_Data() As String                      '4 เก็บข้อมูลที่จะบันทึก

    Public Sub Selecting(ByVal SLECTS As String, ByVal FROMS As String, ByVal WHERES As String, Optional ByVal ORDERBY As String = "")
        Dim whereFinal As String = If(String.IsNullOrWhiteSpace(WHERES), "1=1", WHERES)
        sb = New StringBuilder()
        Cursor.Current = Cursors.WaitCursor
        Try
            sb.Append("SELECT ")
            sb.Append(SLECTS)
            sb.Append(" FROM ")
            sb.Append(FROMS)
            sb.Append(" WHERE ")
            sb.Append(whereFinal)   ' << ถ้าว่างจะเป็น 1=1
            sb.Append(" ORDER BY ")
            sb.Append(ORDERBY)

            SqlIns = sb.ToString()

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(SqlIns, conn)
                    cmd.CommandType = CommandType.Text
                    'คำสั่งรายงาน/ค้นหาบางหน้ามี JOIN หลายตารางและอาจใช้เวลามากกว่า
                    'ค่าเริ่มต้น 30 วินาทีของ SqlCommand
                    cmd.CommandTimeout = 120

                    Using r As SqlDataReader = cmd.ExecuteReader()
                        Dim tmp As New DataTable()
                        tmp.Load(r)

                        DTb = tmp
                        IsHasRow = (tmp.Rows.Count > 0)
                        IsError = Not IsHasRow
                    End Using
                End Using
            End Using

        Catch ex As Exception
            IsError = True
            IsHasRow = False
            If Not SuppressErrorMessage Then
                Dim detail As String = ex.ToString()
                If Not String.IsNullOrWhiteSpace(SqlIns) Then
                    detail &= Environment.NewLine & Environment.NewLine & "คำสั่ง SQL:" &
                              Environment.NewLine & SqlIns
                End If
                MessageBox.Show(detail, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Finally
            Cursor.Current = Cursors.Default
            SqlIns = Nothing
        End Try

    End Sub
    Public Sub InsertData(ByVal m_Insert As String,
                      ByVal m_Item As String,
                      ByVal m_Value As String,
                      ByVal m_ValueData() As String,
                      ByVal m_Data() As String)

        sb = New StringBuilder()

        Try
            Cursor.Current = Cursors.WaitCursor

            If m_ValueData Is Nothing OrElse m_Data Is Nothing OrElse m_ValueData.Length <> m_Data.Length Then
                Throw New ArgumentException("m_ValueData และ m_Data ต้องไม่เป็น Nothing และต้องมีจำนวนเท่ากัน")
            End If

            sb.Append("INSERT INTO ").Append(m_Insert).
           Append(" (").Append(m_Item).Append(") ").
           Append(" VALUES (").Append(m_Value).Append(")")

            SqlIns = sb.ToString()

            Using conn As SqlConnection = modVariable.OpenConnection()
                If conn.State <> ConnectionState.Open Then conn.Open()

                Using tr As SqlTransaction = conn.BeginTransaction()
                    Using cmd As New SqlCommand(SqlIns, conn, tr)
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.Clear()

                        Dim fieldNames As String() = m_Item.Split(","c)
                        For i As Integer = 0 To m_ValueData.Length - 1
                            Dim fieldName As String = If(i < fieldNames.Length, fieldNames(i).Trim(), "")
                            Dim isDateField As Boolean = fieldName.Equals("DateTime", StringComparison.OrdinalIgnoreCase) OrElse
                                                         fieldName.Equals("CTime", StringComparison.OrdinalIgnoreCase) OrElse
                                                         fieldName.Equals("UTime", StringComparison.OrdinalIgnoreCase)
                            Dim p = cmd.Parameters.Add(m_ValueData(i), If(isDateField, SqlDbType.DateTime, SqlDbType.NVarChar))

                            If m_Data(i) Is Nothing Then
                                p.Value = DBNull.Value
                            ElseIf isDateField Then
                                Dim dateValue As DateTime
                                If DateTime.TryParseExact(m_Data(i),
                                                          New String() {"yyyy-MM-dd HH:mm:ss", "yyyy-MM-ddTHH:mm:ss"},
                                                          CultureInfo.InvariantCulture,
                                                          DateTimeStyles.None,
                                                          dateValue) Then
                                    p.Value = dateValue
                                Else
                                    p.Value = DBNull.Value
                                End If
                            Else
                                p.Value = m_Data(i)
                            End If
                        Next

                        cmd.ExecuteNonQuery()
                    End Using

                    tr.Commit()
                End Using
            End Using

            IsError = False
            m_Error = Nothing

        Catch ex As Exception
            IsError = True
            m_Error = ex.ToString()
            If Not SuppressErrorMessage Then
                MessageBox.Show(ex.ToString(), "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Finally
            Cursor.Current = Cursors.Default
            SqlIns = Nothing
        End Try
    End Sub
    Public Sub DeleteData(ByVal m_From As String, ByVal m_Where As String)
        Try
            Cursor.Current = Cursors.WaitCursor

            sb = New StringBuilder()
            sb.Append(" DELETE  " & m_From & " WHERE " & m_Where & " ")
            SqlIns = sb.ToString()

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    Try
                        Using cmd As New SqlCommand(SqlIns, conn, tr)
                            cmd.CommandType = CommandType.Text
                            cmd.ExecuteNonQuery()
                        End Using

                        tr.Commit()
                        IsError = False
                        m_Error = Nothing
                    Catch ex As Exception
                        Try : tr.Rollback() : Catch : End Try
                        Throw
                    End Try
                End Using
            End Using

        Catch ex As Exception
            m_Error = ex.ToString()
            IsError = True
            If Not SuppressErrorMessage Then
                MessageBox.Show(ex.ToString(), "DeleteData Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            Cursor.Current = Cursors.Default
            SqlIns = Nothing
        End Try
    End Sub
    Public Function CalPassword(OfficerID As Long, UserNameString As String, PasswordString As String) As Decimal

        Dim num1 As Decimal = 0D
        Dim num2 As Long = OfficerID

        UserNameString = UserNameString.Trim()
        PasswordString = PasswordString.Trim()

        ' ===== Loop Password =====
        For index As Integer = 0 To PasswordString.Length - 1

            Dim num3 As Long = OfficerID + CLng(AscW(PasswordString(index)))

            Select Case index Mod 4
                Case 0
                    num1 += CDec(num3)
                Case 1
                    num1 -= CDec(num3)
                Case 2
                    num1 *= CDec(num3)
                Case 3
                    If num3 = 0L Then num3 = 1L
                    num1 /= CDec(num3)
            End Select

            OfficerID += 1
        Next

        OfficerID = num2

        ' ===== Loop Username =====
        For index As Integer = 0 To UserNameString.Length - 1

            Dim num4 As Long = OfficerID + CLng(AscW(UserNameString(index)))

            Select Case index Mod 4
                Case 0
                    num1 += CDec(num4)
                Case 1
                    num1 *= CDec(num4)
                Case 2
                    num1 -= CDec(num4)
                Case 3
                    num1 += CDec(num4)
            End Select

            OfficerID += 2
        Next

        num1 = Convert.ToDecimal(num1.ToString("#0.0000"))
        Return num1

    End Function

    Public Sub UpdateData(ByVal m_Update As String, ByVal m_SetVal As String, ByVal m_Where As String)
        Dim sql As String = "UPDATE " & m_Update & " SET " & m_SetVal & " WHERE " & m_Where

        Try
            Cursor.Current = Cursors.WaitCursor

            Using conn As SqlConnection = modVariable.OpenConnection()
                If conn.State <> ConnectionState.Open Then conn.Open()

                Using tr As SqlTransaction = conn.BeginTransaction()
                    Try
                        Using cmd As New SqlCommand(sql, conn, tr)
                            cmd.CommandType = CommandType.Text
                            cmd.ExecuteNonQuery()
                        End Using

                        tr.Commit()
                        IsError = False
                    Catch ex As Exception
                        Try : tr.Rollback() : Catch : End Try
                        m_Error = ex.ToString()
                        IsError = True
                    End Try
                End Using
            End Using

        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
End Class

