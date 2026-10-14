Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class fsupplier
    Public Property SelectedSupplierNumber As Integer
    Public Property SelectedSupplierName As String = ""
    Public Property SelectedCredit As Integer
    Public Property SelectedDiscountP As Decimal
    Public Property SelectedCurrencyID As Integer

    Private Sub fsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliers()
    End Sub

    Private Sub LoadSuppliers()
        Try
            Cursor.Current = Cursors.WaitCursor
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand()
                    cmd.Connection = conn
                    cmd.CommandText = "SELECT " &
                        "Supplier.SupplierNumber, " &
                        "RTRIM(Supplier.SupplierCode) AS SupplierCode, " &
                        "RTRIM(Supplier.Name) AS Name, " &
                        "RTRIM(Supplier.Tel) AS Tel, " &
                        "Supplier.Credit, Supplier.DiscountP, " &
                        "CASE WHEN Supplier.Status IS NULL THEN N'ใช้งานอยู่' ELSE N'ไม่ใช้งาน' END AS StatusText " &
                        "FROM Supplier " &
                        "WHERE 1=1 " &
                        "AND RTRIM(ISNULL(Supplier.Name, N'')) <> N'test' "

                    If txtSupplierName.Text.Trim() <> "" Then
                        cmd.CommandText &= "AND Supplier.Name LIKE @Name "
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = "%" & txtSupplierName.Text.Trim() & "%"
                    End If
                    If txtSupplierCode.Text.Trim() <> "" Then
                        cmd.CommandText &= "AND Supplier.SupplierCode LIKE @Code "
                        cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = "%" & txtSupplierCode.Text.Trim() & "%"
                    End If
                    If cboStatus.SelectedIndex = 0 Then
                        cmd.CommandText &= "AND Supplier.Status IS NULL "
                    ElseIf cboStatus.SelectedIndex = 2 Then
                        cmd.CommandText &= "AND Supplier.Status = 1 "
                    End If
                    cmd.CommandText &= "ORDER BY Supplier.Name ASC"

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        dgvSupplier.Rows.Clear()
                        While reader.Read()
                            Dim rowIndex = dgvSupplier.Rows.Add(
                                reader("SupplierNumber"),
                                If(reader.IsDBNull(reader.GetOrdinal("SupplierCode")), "", reader("SupplierCode").ToString()),
                                If(reader.IsDBNull(reader.GetOrdinal("Name")), "", reader("Name").ToString()),
                                If(reader.IsDBNull(reader.GetOrdinal("Tel")), "", reader("Tel").ToString()),
                                If(reader.IsDBNull(reader.GetOrdinal("Credit")), 0, Convert.ToInt32(reader("Credit"))),
                                If(reader.IsDBNull(reader.GetOrdinal("DiscountP")), 0D, Convert.ToDecimal(reader("DiscountP")).ToString("N2")),
                                reader("StatusText").ToString())
                            If reader("StatusText").ToString() = "ไม่ใช้งาน" Then dgvSupplier.Rows(rowIndex).DefaultCellStyle.ForeColor = Color.FromArgb(160, 105, 40)
                        End While
                    End Using
                End Using
            End Using
            lblResult.Text = $"แสดง {dgvSupplier.Rows.Count} รายการ"
            If dgvSupplier.Rows.Count > 0 Then dgvSupplier.Rows(0).Selected = True
        Catch ex As Exception
            MessageBox.Show("ไม่สามารถโหลดข้อมูล Supplier ได้" & Environment.NewLine & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub ShowSelectedSupplier()
        If dgvSupplier.SelectedRows.Count = 0 Then Return
        Dim row = dgvSupplier.SelectedRows(0)
        SelectedSupplierNumber = Convert.ToInt32(row.Cells(0).Value)
        SelectedSupplierName = Convert.ToString(row.Cells(2).Value).Trim()
        SelectedCredit = Convert.ToInt32(row.Cells(4).Value)
        Decimal.TryParse(Convert.ToString(row.Cells(5).Value), SelectedDiscountP)
        lblDetailId.Text = SelectedSupplierNumber.ToString()
        lblDetailName.Text = SelectedSupplierName
        lblDetailTel.Text = Convert.ToString(row.Cells(3).Value).Trim()
        LoadSupplierContacts(SelectedSupplierNumber)
    End Sub

    Private Sub LoadSupplierContacts(supplierNumber As Integer)
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT RTRIM(Name) AS Name, Status FROM SupplierContact WHERE SupplierNumber=@SupplierNumber ORDER BY Name", conn)
                    cmd.Parameters.Add("@SupplierNumber", SqlDbType.Int).Value = supplierNumber
                    Using reader = cmd.ExecuteReader()
                        dgvContact.Rows.Clear()
                        While reader.Read()
                            dgvContact.Rows.Add(reader("Name").ToString(), "", If(reader.IsDBNull(reader.GetOrdinal("Status")), "ใช้งานอยู่", "ไม่ใช้งาน"))
                        End While
                    End Using
                End Using
            End Using
        Catch
            dgvContact.Rows.Clear()
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadSuppliers()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtSupplierName.Clear() : txtSupplierCode.Clear() : cboStatus.SelectedIndex = 0 : LoadSuppliers()
    End Sub

    Private Sub SearchTextChanged(sender As Object, e As EventArgs) Handles txtSupplierName.TextChanged, txtSupplierCode.TextChanged
        If txtSupplierName.Text.Trim().Length >= 3 OrElse txtSupplierCode.Text.Trim().Length >= 1 Then LoadSuppliers()
    End Sub

    Private Sub dgvSupplier_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSupplier.SelectionChanged
        ShowSelectedSupplier()
    End Sub

    Private Sub dgvSupplier_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSupplier.CellDoubleClick
        If e.RowIndex >= 0 Then DialogResult = DialogResult.OK
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If dgvSupplier.SelectedRows.Count > 0 Then DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel : Close()
    End Sub
End Class
