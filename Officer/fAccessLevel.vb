Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Public Class fAccessLevel
    Inherits Form

    Private ReadOnly officerNumber As Long
    Private ReadOnly loginOfficerNumber As Integer
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly btnSave As New Button()
    Private ReadOnly btnCancel As New Button()
    Private ReadOnly btnSales As New Button()
    Private ReadOnly btnAll As New Button()
    Private ReadOnly btnNone As New Button()
    Private ReadOnly lblTitle As New Label()

    Private Class AccessItem
        Public Property GroupName As String
        Public Property AccessKey As String
        Public Property Detail As String
    End Class

    Public Sub New(officerNumberIn As Long, loginOfficerNumberIn As Integer)
        officerNumber = officerNumberIn
        loginOfficerNumber = If(loginOfficerNumberIn > 0, loginOfficerNumberIn, 1)
        InitializeForm()
        AddHandler Load, AddressOf AccessLevelLoad
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnCancel.Click, AddressOf CancelClick
        AddHandler btnSales.Click, AddressOf SalesPresetClick
        AddHandler btnAll.Click, AddressOf AllPresetClick
        AddHandler btnNone.Click, AddressOf NonePresetClick
        AddHandler grid.CurrentCellDirtyStateChanged, AddressOf GridCurrentCellDirtyStateChanged
    End Sub

    Private Sub InitializeForm()
        Text = "กำหนดสิทธิ์การใช้งาน"
        StartPosition = FormStartPosition.CenterParent
        Size = New Size(1100, 700)
        MinimumSize = New Size(850, 500)
        Font = New Font("Cordia New", 14.0!, FontStyle.Regular)

        lblTitle.Text = "กำหนดสิทธิ์เมนูของพนักงาน ID " & officerNumber.ToString(CultureInfo.InvariantCulture)
        lblTitle.Dock = DockStyle.Top
        lblTitle.Height = 46
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        lblTitle.BackColor = Color.Bisque
        lblTitle.Font = New Font("Cordia New", 18.0!, FontStyle.Bold)

        Dim topPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Top, .Height = 58, .Padding = New Padding(8),
            .WrapContents = False
        }
        ConfigureButton(btnSales, "สิทธิ์การขาย", 130)
        ConfigureButton(btnAll, "เลือกทั้งหมด", 130)
        ConfigureButton(btnNone, "ไม่ให้สิทธิ์", 130)
        topPanel.Controls.AddRange(New Control() {btnSales, btnAll, btnNone})

        grid.Dock = DockStyle.Fill
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.AutoGenerateColumns = False
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.RowHeadersVisible = False
        grid.BackgroundColor = Color.White
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = 54
        grid.Columns.Add(New DataGridViewCheckBoxColumn With {.Name = "Allow", .HeaderText = "อนุญาต", .FillWeight = 12, .SortMode = DataGridViewColumnSortMode.NotSortable})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "GroupName", .HeaderText = "กลุ่มเมนู", .FillWeight = 22, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "AccessKey", .HeaderText = "รหัสสิทธิ์", .FillWeight = 22, .ReadOnly = True})
        grid.Columns.Add(New DataGridViewTextBoxColumn With {.Name = "Detail", .HeaderText = "รายละเอียด", .FillWeight = 44, .ReadOnly = True})

        Dim bottomPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Bottom, .Height = 62, .FlowDirection = FlowDirection.RightToLeft,
            .Padding = New Padding(8), .WrapContents = False
        }
        ConfigureButton(btnCancel, "ยกเลิก", 120)
        ConfigureButton(btnSave, "บันทึก", 140)
        btnSave.BackColor = Color.LightGreen
        btnCancel.BackColor = Color.Salmon
        bottomPanel.Controls.AddRange(New Control() {btnCancel, btnSave})

        Controls.Add(grid)
        Controls.Add(bottomPanel)
        Controls.Add(topPanel)
        Controls.Add(lblTitle)
    End Sub

    Private Sub ConfigureButton(button As Button, caption As String, width As Integer)
        button.Text = caption
        button.Width = width
        button.Height = 42
        button.Font = New Font("Cordia New", 15.0!, FontStyle.Bold)
        button.UseVisualStyleBackColor = False
        button.Margin = New Padding(4, 1, 4, 1)
    End Sub

    Private Sub GridCurrentCellDirtyStateChanged(sender As Object, e As EventArgs)
        If grid.IsCurrentCellDirty AndAlso TypeOf grid.CurrentCell Is DataGridViewCheckBoxCell Then
            grid.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub AccessLevelLoad(sender As Object, e As EventArgs)
        LoadAccessLevels()
    End Sub

    Private Sub LoadAccessLevels()
        Try
            PermissionService.EnsurePermissionSchema()
            Dim items As List(Of AccessItem) = LoadCatalogFromDatabase()
            Dim values As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT p.PermissionKey, " &
                    "CAST(CASE WHEN op.PermissionID IS NOT NULL THEN op.AllowValue " &
                    "WHEN EXISTS(SELECT 1 FROM AppOfficerRole ore INNER JOIN AppRolePermission rp ON rp.RoleID=ore.RoleID " &
                    "WHERE ore.OfficerNumber=@OfficerNumber AND rp.PermissionID=p.PermissionID AND rp.AllowValue=1) THEN 1 ELSE 0 END AS bit) AS AllowValue " &
                    "FROM AppPermission p LEFT JOIN AppOfficerPermission op ON op.PermissionID=p.PermissionID AND op.OfficerNumber=@OfficerNumber " &
                    "WHERE p.IsActive=1"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            values(Convert.ToString(rd("PermissionKey")).Trim()) = Convert.ToBoolean(rd("AllowValue"))
                        End While
                    End Using
                End Using
            End Using

            grid.Rows.Clear()
            For Each item As AccessItem In items
                Dim allowed As Boolean = False
                values.TryGetValue(item.AccessKey, allowed)
                grid.Rows.Add(allowed, item.GroupName, item.AccessKey, item.Detail)
            Next
        Catch ex As Exception
            MessageBox.Show("โหลดสิทธิ์ไม่สำเร็จ: " & ex.Message, "กำหนดสิทธิ์", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Function LoadCatalogFromDatabase() As List(Of AccessItem)
        Dim result As New List(Of AccessItem)()
        Dim seen As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT PermissionKey,PermissionName,ModuleName FROM AppPermission WHERE IsActive=1 ORDER BY ModuleName,PermissionKey", conn)
                Using rd As SqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        Dim key As String = Convert.ToString(rd("PermissionKey")).Trim()
                        If key.Length > 0 AndAlso seen.Add(key) Then
                            result.Add(New AccessItem With {
                                .AccessKey = key,
                                .GroupName = Convert.ToString(rd("ModuleName")).Trim(),
                                .Detail = Convert.ToString(rd("PermissionName")).Trim()
                            })
                        End If
                    End While
                End Using
            End Using
        End Using

        If result.Count = 0 Then
            For Each key As String In New String() {"Admin.ManagePermissions", "Admin.ManagePassword", "Officer.View", "Officer.Add", "Officer.Edit", "Product.View", "Product.Add", "Product.Edit", "Product.Cancel", "ProductDrink.View", "ProductDrink.Add", "MaterialDrink.Add", "Buy.View", "Buy.Add", "Buy.Edit", "Buy.Cancel", "Buy.Print", "Stock.View", "Stock.In", "Stock.Out", "Sell.View", "Sell.Add", "Sell.Edit", "Sell.Cancel", "Sell.DeleteItem", "Report.View", "Report.Print"}
                result.Add(New AccessItem With {.AccessKey = key, .GroupName = GroupForKey(key), .Detail = DetailForKey(key)})
            Next
        End If
        Return result
    End Function

    Private Function CoreKeys() As String()
        Return New String() {"ManageAccessLevel", "ManageUserNamePassword", "SetOfficerLimitSearch", "CanViewCost", "CanViewProfit", "CanApprove", "BackupDatabase", "Product", "AddProduct", "EditProduct", "CancelProduct", "Officer", "AddOfficer", "EditCancelOfficer", "GroupBuy", "Buy", "BuyDetail", "EditBuy", "CancelBuy", "GroupStock", "Stock", "StockIn", "StockOut", "GroupSell", "Sell", "SellSelectCustomer", "SellChangeUnit", "SellUseEditButton", "SellUseDeleteButton", "SellDetail", "ReceiveMoneyFromSell", "ReceiveTransfer", "WorkingTimeOpen", "WorkingTimeClose"}
    End Function

    Private Function GroupForKey(key As String) As String
        If key.StartsWith("Group", StringComparison.OrdinalIgnoreCase) Then Return "กลุ่มเมนู"
        If key.StartsWith("Buy", StringComparison.OrdinalIgnoreCase) OrElse key.Contains("Supplier") OrElse key.EndsWith("PO", StringComparison.OrdinalIgnoreCase) OrElse key.EndsWith("PR", StringComparison.OrdinalIgnoreCase) Then Return "4. Buy (ซื้อ)"
        If key.StartsWith("Sell", StringComparison.OrdinalIgnoreCase) OrElse key.StartsWith("Receive", StringComparison.OrdinalIgnoreCase) OrElse key.StartsWith("WorkingTime", StringComparison.OrdinalIgnoreCase) OrElse key.Contains("Coupon") Then Return "6. Sell (ขาย)"
        If key.StartsWith("Stock", StringComparison.OrdinalIgnoreCase) OrElse key.StartsWith("Inventory", StringComparison.OrdinalIgnoreCase) OrElse key.StartsWith("Produce", StringComparison.OrdinalIgnoreCase) Then Return "5. Stock (คลังสินค้า)"
        If key.Contains("Officer") OrElse key.Contains("Password") OrElse key.Contains("Access") Then Return "0. Admin / พนักงาน"
        Return "เมนูอื่น ๆ"
    End Function

    Private Function DetailForKey(key As String) As String
        Select Case key
            Case "ManageAccessLevel" : Return "กำหนดสิทธิ์การใช้งานของพนักงาน"
            Case "ProductDrink.View" : Return "ดูรายการเครื่องดื่มและสูตร"
            Case "ProductDrink.Add" : Return "เพิ่มรายการเครื่องดื่ม"
            Case "MaterialDrink.Add" : Return "เพิ่มวัตถุดิบเครื่องดื่ม"
            Case "SetOfficerLimitSearch" : Return "กำหนดจำนวนวันย้อนหลังในการค้นหาเอกสาร"
            Case "SellUseEditButton" : Return "ใช้ปุ่มแก้ไขรายการขาย"
            Case "SellUseDeleteButton" : Return "ใช้ปุ่มลบรายการขาย"
            Case "CanViewCost" : Return "ดูต้นทุนสินค้า"
            Case "CanViewProfit" : Return "ดูกำไร"
            Case Else : Return "สิทธิ์เมนู " & key
        End Select
    End Function

    Private Function CalAccessValue(officerId As Long, key As String) As Decimal
        Dim value As Decimal = 0D
        For index As Integer = 0 To key.Length - 1
            Dim number As Long = officerId + Convert.ToInt64(AscW(key(index)))
            Select Case index Mod 4
                Case 0 : value += number
                Case 1 : value -= number
                Case 2 : value *= number
                Case Else
                    If number = 0 Then number = 1
                    value /= number
            End Select
            officerId += 1
        Next
        Return Decimal.Parse(value.ToString("#0.00", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
    End Function

    Private Sub SaveClick(sender As Object, e As EventArgs)
        Try
            'ยืนยันค่า Checkbox ที่กำลังแก้ไขอยู่ก่อนอ่านค่าไปบันทึก
            grid.EndEdit()
            PermissionService.EnsurePermissionSchema()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    Using reset As New SqlCommand("DELETE FROM AppOfficerPermission WHERE OfficerNumber=@Officer", conn, tr)
                        reset.Parameters.Add("@Officer", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                        reset.ExecuteNonQuery()
                    End Using

                    For Each row As DataGridViewRow In grid.Rows
                        Dim key As String = Convert.ToString(row.Cells("AccessKey").Value).Trim()
                        Using insert As New SqlCommand("INSERT INTO AppOfficerPermission(OfficerNumber,PermissionID,AllowValue) SELECT @Officer,PermissionID,@Allow FROM AppPermission WHERE PermissionKey=@Key", conn, tr)
                            insert.Parameters.Add("@Officer", SqlDbType.Int).Value = Convert.ToInt32(officerNumber)
                            insert.Parameters.Add("@Key", SqlDbType.NVarChar, 100).Value = key
                            insert.Parameters.Add("@Allow", SqlDbType.Bit).Value = Convert.ToBoolean(row.Cells("Allow").Value)
                            insert.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show("บันทึกสิทธิ์ไม่สำเร็จ: " & ex.Message, "กำหนดสิทธิ์", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SalesPresetClick(sender As Object, e As EventArgs)
        Dim keys As New HashSet(Of String)(New String() {"Sell.View", "Sell.Add", "Sell.Edit", "Sell.Cancel", "Sell.DeleteItem"}, StringComparer.OrdinalIgnoreCase)
        For Each row As DataGridViewRow In grid.Rows
            row.Cells("Allow").Value = keys.Contains(Convert.ToString(row.Cells("AccessKey").Value))
        Next
    End Sub

    Private Sub AllPresetClick(sender As Object, e As EventArgs)
        For Each row As DataGridViewRow In grid.Rows
            row.Cells("Allow").Value = True
        Next
    End Sub

    Private Sub NonePresetClick(sender As Object, e As EventArgs)
        For Each row As DataGridViewRow In grid.Rows
            row.Cells("Allow").Value = False
        Next
    End Sub

    Private Sub CancelClick(sender As Object, e As EventArgs)
        Close()
    End Sub
End Class
