Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Partial Public Class fOfficer
    Inherits Form

    Public Sub New()
        InitializeComponent()
        Text = "พนักงาน (Officer)"
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        MinimumSize = New Size(800, 500)

        AddHandler btnSearch.Click, AddressOf SearchClick
        AddHandler btnAdd.Click, AddressOf AddClick
        AddHandler btnEdit.Click, AddressOf EditClick
        AddHandler btnDisable.Click, AddressOf DisableClick
        AddHandler btnAccessLevel.Click, AddressOf AccessLevelClick
        AddHandler btnLimitSearch.Click, AddressOf LimitSearchClick
        AddHandler btnUserPassword.Click, AddressOf UserPasswordClick
        AddHandler grid.CellDoubleClick, AddressOf GridDoubleClick
        AddHandler txtSearch.KeyDown, AddressOf SearchKeyDown
        AddHandler searchPanel.Resize, AddressOf SearchPanelResize
        AddHandler Load, AddressOf OfficerLoad
        AddHandler Shown, AddressOf OfficerShown
    End Sub

    Private Sub OfficerShown(sender As Object, e As EventArgs)
        'กำหนดซ้ำหลังจากถูกใส่เป็น MDI Child แล้ว เพื่อให้เต็มพื้นที่ MDI จริง
        If MdiParent IsNot Nothing Then
            BeginInvoke(New MethodInvoker(AddressOf FillMdiClient))
        End If
    End Sub

    Private Sub FillMdiClient()
        If IsDisposed OrElse MdiParent Is Nothing Then Return

        Dim mdiClient As MdiClient = TryCast(Parent, MdiClient)
        If mdiClient Is Nothing Then
            For Each control As Control In MdiParent.Controls
                mdiClient = TryCast(control, MdiClient)
                If mdiClient IsNot Nothing Then Exit For
            Next
        End If
        If mdiClient Is Nothing Then Return

        'ใช้พื้นที่ของ MDI Client โดยตรง เพราะบางเครื่อง WindowState ของ MDI Child
        'ไม่ขยายเต็มพื้นที่หลังจากเปลี่ยน DPI หรือขนาดหน้าต่างหลัก
        FormBorderStyle = FormBorderStyle.None
        WindowState = FormWindowState.Normal
        Dock = DockStyle.Fill
        Bounds = mdiClient.ClientRectangle
    End Sub

    Private Sub OfficerLoad(sender As Object, e As EventArgs)
        ApplyPermissionState()
        LoadOfficers()
    End Sub

    Private Sub ApplyPermissionState()
        SetPermissionButtonState(btnAdd, PermissionService.HasPermission("Officer.Add"))
        SetPermissionButtonState(btnEdit, PermissionService.HasPermission("Officer.Edit"))
        SetPermissionButtonState(btnDisable, PermissionService.HasPermission("Officer.Edit"))
        SetPermissionButtonState(btnAccessLevel, PermissionService.HasPermission("Admin.ManagePermissions"))
        SetPermissionButtonState(btnUserPassword, PermissionService.HasPermission("Admin.ManagePassword"))
    End Sub

    Private Sub SetPermissionButtonState(button As Button, allowed As Boolean)
        button.Enabled = allowed
        If allowed Then
            button.BackColor = SystemColors.Control
            button.ForeColor = SystemColors.ControlText
        Else
            button.BackColor = Color.FromArgb(225, 225, 225)
            button.ForeColor = Color.FromArgb(145, 145, 145)
        End If
    End Sub

    Private Sub SearchKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadOfficers()
        End If
    End Sub

    Private Sub SearchClick(sender As Object, e As EventArgs)
        LoadOfficers()
    End Sub

    Private Sub LoadOfficers()
        Cursor = Cursors.WaitCursor
        Try
            Dim sql As String =
                "SELECT OfficerNumber, RTRIM(OfficerCode) AS OfficerCode, " &
                "RTRIM(Barcode) AS Barcode, RTRIM(Name) AS Name, " &
                "RTRIM(Pos) AS Pos, RTRIM(Mobile) AS Mobile, Status " &
                "FROM Officer WHERE 1=1"

            If txtSearch.Text.Trim().Length > 0 Then
                sql &= " AND (Name LIKE @Search OR OfficerCode LIKE @Search OR Barcode LIKE @Search)"
            End If

            If cboStatus.SelectedIndex = 0 Then
                sql &= " AND Status IS NULL"
            ElseIf cboStatus.SelectedIndex = 1 Then
                sql &= " AND Status = 1"
            End If
            sql &= " ORDER BY OfficerNumber ASC"

            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    If txtSearch.Text.Trim().Length > 0 Then
                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = "%" & txtSearch.Text.Trim() & "%"
                    End If
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                End Using
            End Using

            grid.DataSource = table
            If grid.Columns.Contains("OfficerNumber") Then grid.Columns("OfficerNumber").HeaderText = "ID"
            If grid.Columns.Contains("OfficerCode") Then grid.Columns("OfficerCode").HeaderText = "รหัสพนักงาน"
            If grid.Columns.Contains("Barcode") Then grid.Columns("Barcode").HeaderText = "บาร์โค้ด"
            If grid.Columns.Contains("Name") Then grid.Columns("Name").HeaderText = "ชื่อพนักงาน"
            If grid.Columns.Contains("Pos") Then grid.Columns("Pos").HeaderText = "ตำแหน่ง"
            If grid.Columns.Contains("Mobile") Then grid.Columns("Mobile").HeaderText = "มือถือ"
            If grid.Columns.Contains("Status") Then grid.Columns("Status").HeaderText = "สถานะ"
            lblCount.Text = table.Rows.Count.ToString() & " รายการ"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ไม่สามารถโหลดข้อมูล Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function SelectedOfficerNumber() As Long
        If grid.CurrentRow Is Nothing OrElse grid.CurrentRow.Cells("OfficerNumber").Value Is Nothing Then Return 0
        Return Convert.ToInt64(grid.CurrentRow.Cells("OfficerNumber").Value)
    End Function

    Private Sub AddClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("Officer.Add") Then Return
        Using dialog As New fOfficerAddEdit(0)
            If dialog.ShowDialog(Me) = DialogResult.OK Then LoadOfficers()
        End Using
    End Sub

    Private Sub EditClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("Officer.Edit") Then Return
        Dim officerNumber As Long = SelectedOfficerNumber()
        If officerNumber <= 0 Then
            MessageBox.Show("กรุณาเลือกพนักงานที่ต้องการแก้ไข", "Officer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using dialog As New fOfficerAddEdit(officerNumber)
            If dialog.ShowDialog(Me) = DialogResult.OK Then LoadOfficers()
        End Using
    End Sub

    Private Sub GridDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then EditClick(sender, EventArgs.Empty)
    End Sub

    Private Sub DisableClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("Officer.Edit") Then Return
        Dim officerNumber As Long = SelectedOfficerNumber()
        If officerNumber <= 1 Then
            MessageBox.Show("ไม่สามารถยกเลิก Admin หลักได้", "Officer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If MessageBox.Show("ต้องการยกเลิกการใช้งานพนักงานรายการนี้หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("UPDATE Officer SET Status=1, UONumber=@UONumber, UTime=GETDATE() WHERE OfficerNumber=@OfficerNumber", conn)
                    cmd.Parameters.Add("@UONumber", SqlDbType.Int).Value = Convert.ToInt32(ModSession.LoginOfficerID)
                    cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            LoadOfficers()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ไม่สามารถยกเลิก Officer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchPanelResize(sender As Object, e As EventArgs)
        Dim usedWidth As Integer = searchPanel.Padding.Horizontal
        For Each control As Control In searchPanel.Controls
            If control Is countSpacer OrElse control Is lblCount Then Continue For
            usedWidth += control.Width + control.Margin.Horizontal
        Next
        Dim available As Integer = searchPanel.ClientSize.Width - usedWidth - lblCount.Width - lblCount.Margin.Horizontal
        countSpacer.Width = Math.Max(0, available)
    End Sub

    Private Sub AccessLevelClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("Admin.ManagePermissions") Then Return
        Dim officerNumber As Long = SelectedOfficerNumber()
        If officerNumber <= 1 Then
            MessageBox.Show("ไม่สามารถกำหนดสิทธิ์ให้ Admin หลักจากหน้านี้ได้", "Access Level", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using dialog As New fAccessLevel(officerNumber, Convert.ToInt32(Math.Max(1, ModSession.LoginOfficerID)))
            dialog.ShowDialog(Me)
        End Using
    End Sub

    Private Sub LimitSearchClick(sender As Object, e As EventArgs)
        Dim officerNumber As Long = SelectedOfficerNumber()
        If officerNumber <= 1 Then
            MessageBox.Show("ไม่สามารถกำหนดการจำกัดการค้นหาให้ Admin หลักจากหน้านี้ได้", "Officer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using dialog As New fOfficerLimitSearch(officerNumber)
            dialog.ShowDialog(Me)
        End Using
    End Sub

    Private Sub UserPasswordClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("Admin.ManagePassword") Then Return
        Dim officerNumber As Long = SelectedOfficerNumber()
        If officerNumber <= 0 Then
            MessageBox.Show("กรุณาเลือกพนักงานก่อนตั้ง User Name และ Password", "Officer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If officerNumber = 1 AndAlso ModSession.LoginOfficerID <> 1 Then
            MessageBox.Show("ไม่สามารถตั้ง User Name และ Password ของ Admin หลักได้", "Officer", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using dialog As New fSetUserNamePassword(officerNumber)
            dialog.ShowDialog(Me)
        End Using
    End Sub

End Class
