Option Strict On

Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class fOfficerLimitSearch
    Inherits Form

    Private ReadOnly officerNumber As Long
    Private ReadOnly checkSell As New CheckBox()
    Private ReadOnly checkDrawer As New CheckBox()
    Private ReadOnly checkExpense As New CheckBox()
    Private ReadOnly daysSell As New TextBox()
    Private ReadOnly daysDrawer As New TextBox()
    Private ReadOnly daysExpense As New TextBox()
    Private ReadOnly btnSave As New Button()
    Private ReadOnly btnCancel As New Button()

    Public Sub New(officerNumberIn As Long)
        officerNumber = officerNumberIn
        InitializeForm()
        AddHandler Load, AddressOf LimitLoad
        AddHandler checkSell.CheckedChanged, AddressOf CheckChanged
        AddHandler checkDrawer.CheckedChanged, AddressOf CheckChanged
        AddHandler checkExpense.CheckedChanged, AddressOf CheckChanged
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnCancel.Click, AddressOf CancelClick
    End Sub

    Private Sub InitializeForm()
        Text = "จำกัดการค้นหาเอกสารของพนักงาน"
        StartPosition = FormStartPosition.CenterParent
        Size = New Size(620, 340)
        MinimumSize = New Size(520, 300)
        Font = New Font("Cordia New", 15.0!, FontStyle.Regular)

        Dim title As New Label With {.Text = "กำหนดจำนวนวันย้อนหลังที่อนุญาตให้ค้นหา", .Dock = DockStyle.Top, .Height = 52, .TextAlign = ContentAlignment.MiddleCenter, .BackColor = Color.Bisque, .Font = New Font("Cordia New", 18.0!, FontStyle.Bold)}
        Dim table As New TableLayoutPanel With {.Dock = DockStyle.Fill, .ColumnCount = 3, .RowCount = 3, .Padding = New Padding(20, 16, 20, 8)}
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55.0!))
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100.0!))
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.0!))
        For i As Integer = 0 To 2 : table.RowStyles.Add(New RowStyle(SizeType.Percent, 33.33!)) : Next
        checkSell.Text = "ค้นหารายการขาย"
        checkDrawer.Text = "ค้นหาลิ้นชักเงินสด"
        checkExpense.Text = "ค้นหารายการค่าใช้จ่าย"
        daysSell.Text = "0" : daysDrawer.Text = "0" : daysExpense.Text = "0"
        For Each box As TextBox In New TextBox() {daysSell, daysDrawer, daysExpense}
            box.TextAlign = HorizontalAlignment.Right
            box.MaxLength = 9
            box.Dock = DockStyle.Fill
        Next
        table.Controls.Add(checkSell, 0, 0) : table.Controls.Add(daysSell, 1, 0) : table.Controls.Add(New Label With {.Text = "วัน", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 2, 0)
        table.Controls.Add(checkDrawer, 0, 1) : table.Controls.Add(daysDrawer, 1, 1) : table.Controls.Add(New Label With {.Text = "วัน", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 2, 1)
        table.Controls.Add(checkExpense, 0, 2) : table.Controls.Add(daysExpense, 1, 2) : table.Controls.Add(New Label With {.Text = "วัน", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 2, 2)

        Dim bottom As New FlowLayoutPanel With {.Dock = DockStyle.Bottom, .Height = 62, .FlowDirection = FlowDirection.RightToLeft, .Padding = New Padding(8), .WrapContents = False}
        btnSave.Text = "บันทึก" : btnSave.Width = 120 : btnSave.Height = 40 : btnSave.BackColor = Color.LightGreen
        btnCancel.Text = "ยกเลิก" : btnCancel.Width = 120 : btnCancel.Height = 40 : btnCancel.BackColor = Color.Salmon
        bottom.Controls.Add(btnCancel) : bottom.Controls.Add(btnSave)
        Controls.Add(table) : Controls.Add(bottom) : Controls.Add(title)
    End Sub

    Private Sub LimitLoad(sender As Object, e As EventArgs)
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                For Each item As LimitItem In New LimitItem() {New LimitItem(1, checkSell, daysSell), New LimitItem(2, checkDrawer, daysDrawer), New LimitItem(3, checkExpense, daysExpense)}
                    Using cmd As New SqlCommand("SELECT NumDays FROM OfficerLimitSearch WHERE OfficerNumber=@Officer AND DocType=@DocType", conn)
                        cmd.Parameters.Add("@Officer", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                        cmd.Parameters.Add("@DocType", SqlDbType.Int).Value = item.DocType
                        Dim value As Object = cmd.ExecuteScalar()
                        If value Is Nothing OrElse value Is DBNull.Value Then
                            item.CheckBox.Checked = False
                            item.TextBox.Text = "0"
                        Else
                            item.CheckBox.Checked = True
                            item.TextBox.Text = Convert.ToInt32(value).ToString()
                        End If
                    End Using
                Next
            End Using
            UpdateEnabled()
        Catch ex As Exception
            MessageBox.Show("โหลดการจำกัดการค้นหาไม่สำเร็จ: " & ex.Message, "Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Class LimitItem
        Public ReadOnly DocType As Integer
        Public ReadOnly CheckBox As CheckBox
        Public ReadOnly TextBox As TextBox
        Public Sub New(docTypeIn As Integer, checkBoxIn As CheckBox, textBoxIn As TextBox)
            DocType = docTypeIn : CheckBox = checkBoxIn : TextBox = textBoxIn
        End Sub
    End Class

    Private Sub CheckChanged(sender As Object, e As EventArgs)
        UpdateEnabled()
    End Sub

    Private Sub UpdateEnabled()
        daysSell.Enabled = checkSell.Checked
        daysDrawer.Enabled = checkDrawer.Checked
        daysExpense.Enabled = checkExpense.Checked
    End Sub

    Private Function Days(textBox As TextBox) As Integer
        Dim value As Integer
        If Not Integer.TryParse(textBox.Text.Trim(), value) OrElse value < 0 Then Throw New Exception("จำนวนวันต้องเป็นเลขจำนวนเต็มตั้งแต่ 0 ขึ้นไป")
        Return value
    End Function

    Private Sub SaveClick(sender As Object, e As EventArgs)
        Try
            Dim values As New Dictionary(Of Integer, Nullable(Of Integer)) From {{1, If(checkSell.Checked, CType(Days(daysSell), Integer?), Nothing)}, {2, If(checkDrawer.Checked, CType(Days(daysDrawer), Integer?), Nothing)}, {3, If(checkExpense.Checked, CType(Days(daysExpense), Integer?), Nothing)}}
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    For Each pair As KeyValuePair(Of Integer, Nullable(Of Integer)) In values
                        Using cmd As New SqlCommand("IF EXISTS (SELECT 1 FROM OfficerLimitSearch WHERE OfficerNumber=@Officer AND DocType=@DocType) UPDATE OfficerLimitSearch SET NumDays=@NumDays WHERE OfficerNumber=@Officer AND DocType=@DocType ELSE INSERT INTO OfficerLimitSearch(OfficerNumber,DocType,NumDays) VALUES(@Officer,@DocType,@NumDays)", conn, tr)
                            cmd.Parameters.Add("@Officer", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                            cmd.Parameters.Add("@DocType", SqlDbType.Int).Value = pair.Key
                            Dim p = cmd.Parameters.Add("@NumDays", SqlDbType.Int)
                            p.Value = If(pair.Value.HasValue, CType(pair.Value.Value, Object), DBNull.Value)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show("บันทึกการจำกัดการค้นหาไม่สำเร็จ: " & ex.Message, "Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CancelClick(sender As Object, e As EventArgs)
        Close()
    End Sub
End Class
