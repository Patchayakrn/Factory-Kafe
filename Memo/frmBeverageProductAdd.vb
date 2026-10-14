Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmBeverageProductAdd
    Inherits Form

    Public Sub New()
        InitializeComponent()
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnCancel.Click, Sub(sender, e) Close()
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("ProductDrink.Add") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์เพิ่มรายการเครื่องดื่ม", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim productID As String = txtProductID.Text.Trim()
        Dim productDetail As String = txtProductDetail.Text.Trim()
        If productID = "" OrElse productDetail = "" Then
            MessageBox.Show("กรุณากรอกรหัสและชื่อเครื่องดื่ม", "เพิ่มรายการเครื่องดื่ม", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "IF EXISTS (SELECT 1 FROM dbo.ProductDrink WHERE ProductID=@ProductID) " &
                    "    THROW 51000, N'รหัสเครื่องดื่มนี้มีอยู่แล้ว', 1; " &
                    "INSERT INTO dbo.ProductDrink(ProductID, Product_Detail) VALUES(@ProductID,@ProductDetail)"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@ProductID", SqlDbType.NVarChar, 30).Value = productID
                    cmd.Parameters.Add("@ProductDetail", SqlDbType.NVarChar, 500).Value = productDetail
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show("เพิ่มรายการเครื่องดื่มไม่สำเร็จ: " & ex.Message, "เพิ่มรายการเครื่องดื่ม", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
