Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>
''' หน้าสร้าง/แก้ไข BOM สูตรเครื่องดื่ม
''' ใช้ dbo.BOM และ dbo.MaterialDrink เป็นตารางเก็บสูตรมาตรฐานของสินค้าและวัตถุดิบ
''' </summary>
Public Class frmBeverageBom
    Inherits Form

    Private selectedFinishedProductCode As String
    Private requestedProductCode As String
    Private isLoading As Boolean

    Public Sub New(Optional productCodeValue As String = "")
        MyBase.New()
        requestedProductCode = If(productCodeValue, "").Trim()
        InitializeComponent()
        AddHandler MyBase.Load, AddressOf FormLoaded
        AddHandler cboFinishedProduct.SelectedIndexChanged, AddressOf FinishedProductChanged
        AddHandler btnNew.Click, AddressOf NewRecipeClick
        AddHandler btnAddMaterial.Click, AddressOf AddMaterialClick
        AddHandler btnRemoveMaterial.Click, AddressOf RemoveMaterialClick
        AddHandler btnSave.Click, AddressOf SaveRecipeClick
        AddHandler btnClose.Click, AddressOf CloseForm
        AddHandler gridMaterials.CellValidating, AddressOf MaterialsCellValidating
        AddHandler gridMaterials.CellEndEdit, AddressOf MaterialsCellEndEdit
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        FitToMdiWorkspace()
        ApplyPermissionState()
        LoadFinishedProducts()
    End Sub

    Private Sub ApplyPermissionState()
        Dim canEdit As Boolean = PermissionService.HasPermission("ProductDrink.Add")
        btnNew.Enabled = canEdit
        btnAddMaterial.Enabled = canEdit
        btnRemoveMaterial.Enabled = canEdit
        btnSave.Enabled = canEdit
        If gridMaterials.Columns("Number") IsNot Nothing Then
            gridMaterials.Columns("Number").ReadOnly = Not canEdit
        End If
    End Sub

    Private Function CanEditRecipe() As Boolean
        If PermissionService.HasPermission("ProductDrink.Add") Then Return True
        MessageBox.Show("ผู้ใช้ปัจจุบันมีสิทธิ์ดูสูตรเท่านั้น ไม่สามารถเพิ่มหรือแก้ไขสูตรได้", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
    End Function

    Private Sub FitToMdiWorkspace()
        If MdiParent Is Nothing Then Return
        Dim mdiClient As MdiClient = Nothing
        For Each child As Control In MdiParent.Controls
            If TypeOf child Is MdiClient Then
                mdiClient = DirectCast(child, MdiClient)
                Exit For
            End If
        Next
        If mdiClient Is Nothing Then Return

        FormBorderStyle = FormBorderStyle.None
        StartPosition = FormStartPosition.Manual
        Location = Point.Empty
        Size = mdiClient.ClientSize
    End Sub

    Private Sub LoadFinishedProducts()
        isLoading = True
        Try
            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT ProductID AS ProductCode, Product_Detail AS ProductName, N'แก้ว' AS Unit " &
                    "FROM dbo.ProductDrink ORDER BY ProductID"
                Using adapter As New SqlDataAdapter(sql, conn)
                    adapter.Fill(table)
                End Using
            End Using

            table.Columns.Add("ProductDisplay", GetType(String))
            For Each row As DataRow In table.Rows
                row("ProductDisplay") = String.Format(CultureInfo.CurrentCulture, "{0} - {1} ({2})", Convert.ToString(row("ProductCode")).Trim(), Convert.ToString(row("ProductName")).Trim(), Convert.ToString(row("Unit")).Trim())
            Next

            cboFinishedProduct.DataSource = table
            If table.Rows.Count = 0 Then
                txtFinishedProduct.Clear()
                lblRecipeStatus.Text = "ไม่พบสินค้าเครื่องดื่มที่ใช้งานอยู่"
                Return
            End If
            cboFinishedProduct.SelectedIndex = 0
            If requestedProductCode <> "" Then
                For index As Integer = 0 To cboFinishedProduct.Items.Count - 1
                    Dim item As DataRowView = TryCast(cboFinishedProduct.Items(index), DataRowView)
                    If item IsNot Nothing AndAlso String.Equals(Convert.ToString(item("ProductCode")).Trim(), requestedProductCode, StringComparison.OrdinalIgnoreCase) Then
                        cboFinishedProduct.SelectedIndex = index
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("โหลดรายการเครื่องดื่มไม่สำเร็จ: " & ex.Message, "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            isLoading = False
        End Try

        LoadSelectedRecipe()
    End Sub

    Private Sub FinishedProductChanged(sender As Object, e As EventArgs)
        If isLoading Then Return
        LoadSelectedRecipe()
    End Sub

    Private Function SelectedFinishedRow() As DataRowView
        Return TryCast(cboFinishedProduct.SelectedItem, DataRowView)
    End Function

    Private Sub LoadSelectedRecipe()
        Dim selected As DataRowView = SelectedFinishedRow()
        gridMaterials.Rows.Clear()
        selectedFinishedProductCode = ""
        If selected Is Nothing Then Return

        selectedFinishedProductCode = Convert.ToString(selected("ProductCode")).Trim()
        txtFinishedProduct.Text = selectedFinishedProductCode & " - " & Convert.ToString(selected("ProductName")).Trim()

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT B.MaterialID, B.[Use], RTRIM(ISNULL(M.Material_Detail,N'')) AS MaterialDetail, " &
                    "RTRIM(ISNULL(M.Unit_Use,N'')) AS UnitUse, M.Unit_per_Package " &
                    "FROM dbo.BOM B LEFT JOIN dbo.MaterialDrink M ON M.MaterialID=B.MaterialID " &
                    "WHERE B.ProductID=@ProductID ORDER BY B.MaterialID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@ProductID", SqlDbType.NVarChar, 30).Value = selectedFinishedProductCode
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            AppendMaterialGridRow(
                                Convert.ToString(reader("MaterialID")).Trim(),
                                Convert.ToString(reader("MaterialDetail")).Trim(),
                                reader("Use"),
                                Convert.ToString(reader("UnitUse")).Trim(),
                                reader("Unit_per_Package"))
                        End While
                    End Using
                End Using
            End Using
            If gridMaterials.Rows.Count = 0 Then
                lblRecipeStatus.Text = If(PermissionService.HasPermission("ProductDrink.Add"), "ยังไม่มีสูตร BOM สำหรับสินค้านี้ กด เพิ่มวัตถุดิบ เพื่อสร้างสูตร", "ยังไม่มีสูตร BOM สำหรับสินค้านี้")
            Else
                lblRecipeStatus.Text = If(PermissionService.HasPermission("ProductDrink.Add"), "โหลดสูตร BOM เดิมแล้ว สามารถแก้ไขจำนวนและบันทึกทับได้", "โหลดสูตร BOM เดิมแล้ว (ดูอย่างเดียว)")
            End If
        Catch ex As Exception
            MessageBox.Show("โหลดสูตร BOM ไม่สำเร็จ: " & ex.Message, "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub NewRecipeClick(sender As Object, e As EventArgs)
        If Not CanEditRecipe() Then Return
        If cboFinishedProduct.Items.Count = 0 Then Return
        gridMaterials.Rows.Clear()
        lblRecipeStatus.Text = "เริ่มสูตรใหม่สำหรับ " & txtFinishedProduct.Text
    End Sub

    Private Sub AddMaterialClick(sender As Object, e As EventArgs)
        If Not CanEditRecipe() Then Return
        Using picker As New frmMaterialPicker()
            If picker.ShowDialog(Me) <> DialogResult.OK Then Return

            For Each selected As MaterialSelection In picker.SelectedMaterials
                If MaterialAlreadyAdded(selected.MaterialID) Then Continue For
                AppendMaterialGridRow(selected.MaterialID, selected.MaterialDetail, 1D, selected.UnitUse, selected.UnitPerPackage)
            Next
            UpdateLineNumbers()
        End Using
    End Sub

    Private Sub AppendMaterialGridRow(materialID As String, materialDetail As String, useAmount As Object, unitUse As String, unitPerPackage As Object)
        Dim rowIndex As Integer = gridMaterials.Rows.Add()
        Dim row As DataGridViewRow = gridMaterials.Rows(rowIndex)
        row.Cells("LineNo").Value = rowIndex + 1
        row.Cells("MaterialID").Value = materialID
        row.Cells("ProductCode").Value = materialID
        row.Cells("ProductName").Value = materialDetail
        row.Cells("Number").Value = If(useAmount Is Nothing OrElse useAmount Is DBNull.Value, 0D, useAmount)
        row.Cells("LUnit").Value = unitUse
        row.Cells("NumSPerLUnit").Value = If(unitPerPackage Is Nothing OrElse unitPerPackage Is DBNull.Value, 1D, unitPerPackage)
    End Sub

    Private Function MaterialAlreadyAdded(materialID As String) As Boolean
        For Each row As DataGridViewRow In gridMaterials.Rows
            If row.IsNewRow Then Continue For
            If String.Equals(Convert.ToString(row.Cells("MaterialID").Value).Trim(), materialID.Trim(), StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    Private Sub RemoveMaterialClick(sender As Object, e As EventArgs)
        If Not CanEditRecipe() Then Return
        For Each row As DataGridViewRow In gridMaterials.SelectedRows.Cast(Of DataGridViewRow)().OrderByDescending(Function(r) r.Index).ToList()
            If Not row.IsNewRow Then gridMaterials.Rows.Remove(row)
        Next
        UpdateLineNumbers()
    End Sub

    Private Sub UpdateLineNumbers()
        For index As Integer = 0 To gridMaterials.Rows.Count - 1
            gridMaterials.Rows(index).Cells("LineNo").Value = index + 1
        Next
    End Sub

    Private Sub MaterialsCellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
        If e.RowIndex < 0 OrElse e.RowIndex >= gridMaterials.Rows.Count Then Return
        If gridMaterials.Columns(e.ColumnIndex).Name <> "Number" AndAlso gridMaterials.Columns(e.ColumnIndex).Name <> "NumSPerLUnit" Then Return

        Dim number As Decimal
        If Not TryParseDecimal(Convert.ToString(e.FormattedValue), number) OrElse number <= 0D Then
            e.Cancel = True
            MessageBox.Show("กรุณากรอกจำนวนให้มากกว่า 0", "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub MaterialsCellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then UpdateLineNumbers()
    End Sub

    Private Sub SaveRecipeClick(sender As Object, e As EventArgs)
        If Not CanEditRecipe() Then Return
        If String.IsNullOrWhiteSpace(selectedFinishedProductCode) Then
            MessageBox.Show("กรุณาเลือกสินค้าเครื่องดื่มก่อนบันทึก", "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If gridMaterials.Rows.Count = 0 Then
            MessageBox.Show("กรุณาเพิ่มวัตถุดิบในสูตรอย่างน้อย 1 รายการ", "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim materials As New List(Of BomMaterial)()
        For Each row As DataGridViewRow In gridMaterials.Rows
            Dim number As Decimal
            Dim conversion As Decimal
            Dim materialID As String = Convert.ToString(row.Cells("MaterialID").Value).Trim()
            If String.IsNullOrWhiteSpace(materialID) Then
                MessageBox.Show("พบรายการที่ไม่มีรหัสวัตถุดิบ กรุณาลบแล้วเพิ่มรายการใหม่", "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not TryParseDecimal(Convert.ToString(row.Cells("Number").Value), number) OrElse number <= 0D Then
                MessageBox.Show("กรุณาตรวจสอบจำนวนใช้ของวัตถุดิบให้มากกว่า 0", "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not TryParseDecimal(Convert.ToString(row.Cells("NumSPerLUnit").Value), conversion) OrElse conversion <= 0D Then conversion = 1D
            materials.Add(New BomMaterial With {
                .MaterialID = materialID,
                .Number = number,
                .LUnit = Convert.ToString(row.Cells("LUnit").Value).Trim(),
                .NumSPerLUnit = conversion
            })
        Next

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    Const deleteSql As String = "DELETE FROM dbo.BOM WHERE ProductID=@ProductID"
                    Using deleteCommand As New SqlCommand(deleteSql, conn, tr)
                        deleteCommand.Parameters.Add("@ProductID", SqlDbType.NVarChar, 30).Value = selectedFinishedProductCode
                        deleteCommand.ExecuteNonQuery()
                    End Using

                    Const insertSql As String =
                        "INSERT INTO dbo.BOM (ProductID, MaterialID, [Use]) VALUES (@ProductID, @MaterialID, @Use)"
                    For Each material As BomMaterial In materials
                        Using insert As New SqlCommand(insertSql, conn, tr)
                            insert.Parameters.Add("@ProductID", SqlDbType.NVarChar, 30).Value = selectedFinishedProductCode
                            insert.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 30).Value = material.MaterialID
                            insert.Parameters.Add("@Use", SqlDbType.Decimal).Value = material.Number
                            insert.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using
            lblRecipeStatus.Text = "บันทึกสูตร BOM เรียบร้อยแล้ว"
            MessageBox.Show("บันทึกสูตร BOM เรียบร้อยแล้ว", "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("บันทึกสูตร BOM ไม่สำเร็จ: " & ex.Message, "สร้าง BOM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function TryParseDecimal(text As String, ByRef value As Decimal) As Boolean
        If Decimal.TryParse(If(text, "").Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, value) Then Return True
        Return Decimal.TryParse(If(text, "").Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, value)
    End Function

    Private Sub CloseForm(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Class BomMaterial
        Public Property MaterialID As String
        Public Property Number As Decimal
        Public Property LUnit As String
        Public Property NumSPerLUnit As Decimal
    End Class
End Class

Public Class MaterialSelection
    Public Property MaterialID As String
    Public Property MaterialDetail As String
    Public Property Brand As String
    Public Property UnitUse As String
    Public Property UnitPerPackage As Decimal
    Public Property CurrentStock As Decimal
End Class

''' <summary>
''' หน้าต่างเลือกวัตถุดิบจาก dbo.MaterialDrink โดยตรง
''' </summary>
Public Class frmMaterialPicker
    Inherits Form

    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly btnSelect As New Button()
    Private ReadOnly btnCancel As New Button()
    Public ReadOnly Property SelectedMaterials As New List(Of MaterialSelection)

    Public Sub New()
        Text = "เลือกวัตถุดิบ"
        StartPosition = FormStartPosition.CenterParent
        Size = New Size(900, 600)
        MinimumSize = New Size(700, 450)
        Font = New Font("Cordia New", 16.0!)

        Dim top As New Panel With {.Dock = DockStyle.Top, .Height = 68, .Padding = New Padding(12)}
        Dim searchLabel As New Label With {.Text = "ค้นหา:", .Dock = DockStyle.Left, .Width = 65, .TextAlign = ContentAlignment.MiddleLeft}
        txtSearch.Dock = DockStyle.Fill
        top.Controls.Add(txtSearch)
        top.Controls.Add(searchLabel)
        Controls.Add(top)

        Dim bottom As New FlowLayoutPanel With {
            .Dock = DockStyle.Bottom, .Height = 58, .FlowDirection = FlowDirection.RightToLeft,
            .Padding = New Padding(10), .BackColor = Color.FromArgb(242, 248, 253)
        }
        ConfigureButton(btnSelect, "เลือก", Color.FromArgb(38, 132, 74), 120)
        ConfigureButton(btnCancel, "ยกเลิก", Color.FromArgb(190, 75, 75), 120)
        bottom.Controls.Add(btnCancel)
        bottom.Controls.Add(btnSelect)
        Controls.Add(bottom)

        grid.Dock = DockStyle.Fill
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AutoGenerateColumns = False
        grid.RowHeadersVisible = False
        grid.ColumnHeadersVisible = True
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = 42
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(219, 232, 244)
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30)
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = True
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        grid.DefaultCellStyle.Font = New Font("Cordia New", 16.0!)
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        grid.RowTemplate.Height = 38
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MaterialID", .HeaderText = "รหัสวัตถุดิบ", .Width = 140, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "MaterialDetail", .HeaderText = "รายการวัตถุดิบ", .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Brand", .HeaderText = "ยี่ห้อ", .Width = 150, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitUse", .HeaderText = "หน่วยใช้", .Width = 100, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "UnitPerPackage", .HeaderText = "จำนวน/แพ็ค", .Width = 110, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "CurrentStock", .HeaderText = "คงเหลือล่าสุด", .Width = 140, .ReadOnly = True})
        grid.Columns("UnitPerPackage").DefaultCellStyle.Format = "#,##0.00"
        grid.Columns("CurrentStock").DefaultCellStyle.Format = "#,##0.00"
        Dim gridHost As New Panel With {
            .Dock = DockStyle.Fill,
            .Padding = New Padding(0, 12, 0, 0),
            .BackColor = Color.White
        }
        gridHost.Controls.Add(grid)
        Controls.Add(gridHost)
        gridHost.BringToFront()
        top.BringToFront()
        bottom.BringToFront()

        AddHandler Load, AddressOf PickerLoaded
        AddHandler txtSearch.TextChanged, AddressOf SearchChanged
        AddHandler btnSelect.Click, AddressOf SelectClick
        AddHandler btnCancel.Click, Sub(sender, e) Close()
        AddHandler grid.CellDoubleClick, AddressOf GridDoubleClick
    End Sub

    Private Sub ConfigureButton(button As Button, caption As String, backColor As Color, width As Integer)
        button.Text = caption
        button.Width = width
        button.Height = 36
        button.BackColor = backColor
        button.ForeColor = Color.White
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub PickerLoaded(sender As Object, e As EventArgs)
        LoadMaterials()
        txtSearch.Focus()
    End Sub

    Private Sub SearchChanged(sender As Object, e As EventArgs)
        If IsHandleCreated Then LoadMaterials()
    End Sub

    Private Sub LoadMaterials()
        Try
            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "WITH LatestStock AS (" &
                    "    SELECT MaterialID, Rest_Qty, " &
                    "           ROW_NUMBER() OVER (PARTITION BY MaterialID ORDER BY [Year] DESC, [Month] DESC) AS RowNo " &
                    "    FROM dbo.StockDrinkCheck" &
                    ") " &
                    "SELECT M.MaterialID, M.Material_Detail, ISNULL(M.Brand,N'') AS Brand, " &
                    "       M.Unit_Use, M.Unit_per_Package, ISNULL(S.Rest_Qty,0) AS CurrentStock " &
                    "FROM dbo.MaterialDrink AS M " &
                    "LEFT JOIN LatestStock AS S ON S.MaterialID=M.MaterialID AND S.RowNo=1 " &
                    "WHERE @Keyword=N'' OR M.MaterialID LIKE N'%' + @Keyword + N'%' " &
                    "OR M.Material_Detail LIKE N'%' + @Keyword + N'%' OR ISNULL(M.Brand,N'') LIKE N'%' + @Keyword + N'%' " &
                    "ORDER BY M.MaterialID"
                Using adapter As New SqlDataAdapter(sql, conn)
                    adapter.SelectCommand.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = txtSearch.Text.Trim()
                    adapter.Fill(table)
                End Using
            End Using
            grid.Rows.Clear()
            For Each source As DataRow In table.Rows
                Dim index As Integer = grid.Rows.Add()
                Dim row As DataGridViewRow = grid.Rows(index)
                row.Cells("MaterialID").Value = Convert.ToString(source("MaterialID")).Trim()
                row.Cells("MaterialDetail").Value = Convert.ToString(source("Material_Detail")).Trim()
                row.Cells("Brand").Value = Convert.ToString(source("Brand")).Trim()
                row.Cells("UnitUse").Value = Convert.ToString(source("Unit_Use")).Trim()
                row.Cells("UnitPerPackage").Value = source("Unit_per_Package")
                row.Cells("CurrentStock").Value = source("CurrentStock")
            Next
        Catch ex As Exception
            MessageBox.Show("โหลดรายการวัตถุดิบไม่สำเร็จ: " & ex.Message, "เลือกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub GridDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then SelectClick(btnSelect, EventArgs.Empty)
    End Sub

    Private Sub SelectClick(sender As Object, e As EventArgs)
        SelectedMaterials.Clear()
        For Each row As DataGridViewRow In grid.SelectedRows
            If row Is Nothing OrElse row.IsNewRow Then Continue For
            Dim amount As Decimal = 1D
            Decimal.TryParse(Convert.ToString(row.Cells("UnitPerPackage").Value), amount)
            SelectedMaterials.Add(New MaterialSelection With {
                .MaterialID = Convert.ToString(row.Cells("MaterialID").Value).Trim(),
                .MaterialDetail = Convert.ToString(row.Cells("MaterialDetail").Value).Trim(),
                .Brand = Convert.ToString(row.Cells("Brand").Value).Trim(),
                .UnitUse = Convert.ToString(row.Cells("UnitUse").Value).Trim(),
                .UnitPerPackage = amount,
                .CurrentStock = ReadDecimalCell(row.Cells("CurrentStock").Value)
            })
        Next
        If SelectedMaterials.Count = 0 Then
            MessageBox.Show("กรุณาเลือกวัตถุดิบอย่างน้อย 1 รายการ", "เลือกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Shared Function ReadDecimalCell(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
        Dim result As Decimal = 0D
        Decimal.TryParse(Convert.ToString(value), result)
        Return result
    End Function
End Class
