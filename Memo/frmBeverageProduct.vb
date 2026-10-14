Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' รายการเครื่องดื่มจาก ProductDrink สำหรับเลือกไปสร้างหรือแก้ไขสูตร BOM
''' </summary>
Public Class frmBeverageProduct
    Inherits Form

    Public Sub New()
        InitializeComponent()
        AddHandler Load, AddressOf FormLoaded
        AddHandler txtSearch.TextChanged, AddressOf SearchChanged
        AddHandler btnAddProduct.Click, AddressOf AddProductClick
        AddHandler btnDeleteProduct.Click, AddressOf DeleteProductClick
        AddHandler btnOpenBom.Click, AddressOf OpenBomClick
        AddHandler btnClose.Click, Sub(sender, e) Close()
        AddHandler gridProducts.CellDoubleClick, AddressOf GridDoubleClick
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        Dim canView As Boolean = PermissionService.HasPermission("ProductDrink.View")
        Dim canEdit As Boolean = PermissionService.HasPermission("ProductDrink.Add")
        btnAddProduct.Enabled = canEdit
        btnDeleteProduct.Enabled = canEdit
        btnOpenBom.Enabled = canView
        LoadProducts()
    End Sub

    Private Sub SearchChanged(sender As Object, e As EventArgs)
        If IsHandleCreated Then LoadProducts()
    End Sub

    Private Sub AddProductClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("ProductDrink.Add") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์เพิ่มรายการเครื่องดื่ม", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Using editor As New frmBeverageProductAdd()
            If editor.ShowDialog(Me) = DialogResult.OK Then LoadProducts()
        End Using
    End Sub

    Private Sub DeleteProductClick(sender As Object, e As EventArgs)
        Dim productCode As String = SelectedProductCode()
        If productCode = "" Then
            MessageBox.Show("กรุณาเลือกรายการเครื่องดื่มก่อน", "ลบรายการ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim productName As String = ""
        If gridProducts.SelectedRows.Count > 0 Then
            productName = Convert.ToString(gridProducts.SelectedRows(0).Cells("ProductDetail").Value).Trim()
        End If

        Dim message As String =
            "ต้องการลบรายการเครื่องดื่มนี้หรือไม่?" & Environment.NewLine &
            "รหัส: " & productCode &
            If(productName = "", "", Environment.NewLine & "รายการ: " & productName) &
            Environment.NewLine & Environment.NewLine &
            "ระบบจะลบสูตร BOM ที่ผูกกับรายการนี้ด้วย" & Environment.NewLine &
            "ประวัติการขายจะไม่ถูกลบ"

        If MessageBox.Show(message, "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using transaction As SqlTransaction = conn.BeginTransaction()
                    Try
                        Using deleteBom As New SqlCommand("DELETE FROM dbo.BOM WHERE ProductID=@ProductID", conn, transaction)
                            deleteBom.Parameters.Add("@ProductID", SqlDbType.NVarChar, 50).Value = productCode
                            deleteBom.ExecuteNonQuery()
                        End Using

                        Dim deletedProductCount As Integer
                        Using deleteProduct As New SqlCommand("DELETE FROM dbo.ProductDrink WHERE ProductID=@ProductID", conn, transaction)
                            deleteProduct.Parameters.Add("@ProductID", SqlDbType.NVarChar, 50).Value = productCode
                            deletedProductCount = deleteProduct.ExecuteNonQuery()
                        End Using

                        If deletedProductCount = 0 Then
                            Throw New InvalidOperationException("ไม่พบรายการเครื่องดื่มที่ต้องการลบ")
                        End If

                        transaction.Commit()
                    Catch
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            LoadProducts()
            MessageBox.Show("ลบรายการเครื่องดื่มและสูตร BOM เรียบร้อยแล้ว", "ลบรายการ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("ลบรายการไม่สำเร็จ: " & ex.Message, "ลบรายการ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadProducts()
        Try
            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT PM.ProductID, PM.Product_Detail, COUNT(B.MaterialID) AS BomCount " &
                    "FROM dbo.ProductDrink PM LEFT JOIN dbo.BOM B ON B.ProductID=PM.ProductID " &
                    "WHERE @Keyword=N'' OR PM.ProductID LIKE N'%' + @Keyword + N'%' " &
                    "OR PM.Product_Detail LIKE N'%' + @Keyword + N'%' " &
                    "GROUP BY PM.ProductID, PM.Product_Detail ORDER BY PM.ProductID"
                Using adapter As New SqlDataAdapter(sql, conn)
                    adapter.SelectCommand.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = txtSearch.Text.Trim()
                    adapter.Fill(table)
                End Using
            End Using

            gridProducts.Rows.Clear()
            For Each source As DataRow In table.Rows
                Dim index As Integer = gridProducts.Rows.Add()
                Dim row As DataGridViewRow = gridProducts.Rows(index)
                Dim bomCount As Integer = Convert.ToInt32(source("BomCount"))
                row.Cells("ProductID").Value = Convert.ToString(source("ProductID")).Trim()
                row.Cells("ProductDetail").Value = Convert.ToString(source("Product_Detail")).Trim()
                row.Cells("BomCount").Value = bomCount
                row.Cells("BomStatus").Value = If(bomCount > 0, "มีสูตร BOM", "ยังไม่มีสูตร BOM")
            Next
        Catch ex As Exception
            MessageBox.Show("โหลดรายการเครื่องดื่มไม่สำเร็จ: " & ex.Message, "รายการเครื่องดื่ม", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Function SelectedProductCode() As String
        If gridProducts.SelectedRows.Count = 0 Then Return ""
        Return Convert.ToString(gridProducts.SelectedRows(0).Cells("ProductID").Value).Trim()
    End Function

    Private Sub GridDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then OpenBomClick(btnOpenBom, EventArgs.Empty)
    End Sub

    Private Sub OpenBomClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("ProductDrink.View") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์ดูสูตรเครื่องดื่ม", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim productCode As String = SelectedProductCode()
        If productCode = "" Then
            MessageBox.Show("กรุณาเลือกเครื่องดื่มก่อน", "รายการเครื่องดื่ม", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim bomForm As New frmBeverageBom(productCode)
        If MdiParent IsNot Nothing Then
            bomForm.MdiParent = MdiParent
            bomForm.Show()
        Else
            bomForm.ShowDialog(Me)
        End If
    End Sub
End Class
