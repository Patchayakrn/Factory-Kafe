Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>
''' ฟอร์มเพิ่มวัตถุดิบเครื่องดื่มลง dbo.MaterialDrink
''' </summary>
Public Class frmMaterialDrinkAdd
    Inherits Form

    Public Sub New()
        InitializeComponent()
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnCancel.Click, Sub(sender, e) Close()
        AddHandler Me.Load, AddressOf FormLoaded
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        LoadMaterials()
        txtMaterialID.Focus()
    End Sub

    Private Sub LoadMaterials()
        Try
            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT MaterialID, Material_Detail, Brand, Unit_Use, Unit_per_Package, Unit_Price, Wt_to_Vol " &
                    "FROM dbo.MaterialDrink ORDER BY MaterialID"
                Using adapter As New SqlDataAdapter(sql, conn)
                    adapter.Fill(table)
                End Using
            End Using
            gridMaterials.DataSource = table
            tableTitleLabel.Text = "รายการวัตถุดิบเครื่องดื่มที่มีอยู่ (" & table.Rows.Count.ToString("N0", CultureInfo.InvariantCulture) & " รายการ)"
        Catch ex As Exception
            gridMaterials.DataSource = Nothing
            tableTitleLabel.Text = "รายการวัตถุดิบเครื่องดื่มที่มีอยู่"
            MessageBox.Show("โหลดรายการวัตถุดิบไม่สำเร็จ: " & ex.Message, "รายการวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        If Not PermissionService.HasPermission("MaterialDrink.Add") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์เพิ่มวัตถุดิบ", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim materialID As String = txtMaterialID.Text.Trim()
        Dim materialDetail As String = txtMaterialDetail.Text.Trim()
        Dim unitUse As String = txtUnitUse.Text.Trim()
        If materialID = "" OrElse materialDetail = "" OrElse unitUse = "" Then
            MessageBox.Show("กรุณากรอกรหัส รายการวัตถุดิบ และหน่วยใช้ให้ครบ", "เพิ่มวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If nudUnitPerPackage.Value <= 2D Then
            MessageBox.Show("จำนวนต่อแพ็กต้องมากกว่า 0", "เพิ่มวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "IF EXISTS (SELECT 1 FROM dbo.MaterialDrink WHERE LTRIM(RTRIM(MaterialID))=@MaterialID) " &
                    "    THROW 51000, N'รหัสวัตถุดิบนี้มีอยู่แล้ว', 1; " &
                    "INSERT INTO dbo.MaterialDrink(MaterialID,Material_Detail,Brand,Unit_Use,Unit_per_Package,Unit_Price,Wt_to_Vol) " &
                    "VALUES(@MaterialID,@MaterialDetail,@Brand,@UnitUse,@UnitPerPackage,@UnitPrice,@WtToVol)"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = materialID
                    cmd.Parameters.Add("@MaterialDetail", SqlDbType.NVarChar, 500).Value = materialDetail
                    cmd.Parameters.Add("@Brand", SqlDbType.NVarChar, 200).Value = If(txtBrand.Text.Trim() = "", CType(DBNull.Value, Object), txtBrand.Text.Trim())
                    cmd.Parameters.Add("@UnitUse", SqlDbType.NVarChar, 50).Value = unitUse
                    AddDecimal(cmd, "@UnitPerPackage", nudUnitPerPackage.Value)
                    AddDecimal(cmd, "@UnitPrice", nudUnitPrice.Value)
                    AddDecimal(cmd, "@WtToVol", nudWtToVol.Value)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("เพิ่มวัตถุดิบเรียบร้อยแล้ว", "เพิ่มวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearEntryFields()
            LoadMaterials()
            txtMaterialID.Focus()
        Catch ex As Exception
            MessageBox.Show("เพิ่มวัตถุดิบไม่สำเร็จ: " & ex.Message, "เพิ่มวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub ClearEntryFields()
        txtMaterialID.Clear()
        txtMaterialDetail.Clear()
        txtBrand.Clear()
        txtUnitUse.Clear()
        nudUnitPerPackage.Value = 1D
        nudUnitPrice.Value = 0D
        nudWtToVol.Value = 0D
    End Sub

    Private Sub AddDecimal(command As SqlCommand, name As String, value As Decimal)
        Dim parameter As SqlParameter = command.Parameters.Add(name, SqlDbType.Decimal)
        parameter.Precision = 18
        parameter.Scale = 4
        parameter.Value = value
    End Sub
End Class
