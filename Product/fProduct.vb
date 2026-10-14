Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports System.Threading.Tasks

Public Class fProduct
    Public Class ProductSelection
        Public Property PN As Long
        Public Property ProductCode As String
        Public Property Barcode As String
        Public Property ProductName As String
        Public Property SellPrice As Decimal
        Public Property Stock As Decimal
        Public Property AllowDecimal As Integer
        Public Property Unit As String
        Public Property Comment As String
    End Class

    Public fSell As fsell
    Public Property SearchText As String
    Public Property SelectedPN As Long
    Public Property SelectedProductCode As String
    Public Property SelectedBarcode As String
    Public Property SelectedProductName As String
    Public Property SelectedSellPrice As Decimal
    Public Property SelectedStock As Decimal
    Public Property SelectedAllowDecimal As Integer
    Public Property SelectedUnit As String
    Public Property SelectedComment As String
    Public Property IsSelectMode As Boolean = False
    Public Property AllowOutOfStockSelection As Boolean = False
    Public Property HideBeverageProducts As Boolean = False
    Public Property OnlyNegativeStockSelection As Boolean = False
    Public ReadOnly Property SelectedProducts As New List(Of ProductSelection)

    Private btnNewProduct As Button
    Private btnEditSelectedProduct As Button
    Private isAddingNewProduct As Boolean = False
    Private isInitializing As Boolean = False
    Private hasLoadedInitialData As Boolean = False

    Private Const AllText As String = "ทั้งหมด"
    Private Const StatusNormalText As String = "ปกติ"
    Private Const StatusCancelText As String = "ยกเลิก"

    Private Sub fProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isInitializing = True
        dtDetail.StateCommon.DataCell.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid

        If IsSelectMode Then
            WindowState = FormWindowState.Normal
            StartPosition = FormStartPosition.CenterParent
            ClientSize = New Size(1500, 850)
            BackColor = Color.White
            TabControl1.Dock = DockStyle.Fill
            TabPage1.Dock = DockStyle.Fill
            TableLayoutPanel1.Dock = DockStyle.Fill
            Panel1.Dock = DockStyle.Top
            dtDetail.Dock = DockStyle.Fill
            btnEditProduct.Visible = False
            btnCloneProduct.Visible = False
            btnAddProduct.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            btnAddProduct.Size = New Size(260, 70)
            btnAddProduct.StateCommon.Content.ShortText.Font = New Font("Cordia New", 18.0!, FontStyle.Bold)
            dtDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dtDetail.MultiSelect = True
            btnAddProduct.Values.Text = "เพิ่มรายการที่เลือกลงใบซื้อ"
        End If

        txtSearch.ForeColor = Color.Black
        txtSearch.Text = If(SearchText, "")
        txtSearch.SelectionStart = txtSearch.TextLength
        txtSearch.Focus()


        isInitializing = False
    End Sub

    Private Sub fProduct_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If hasLoadedInitialData Then Return
        hasLoadedInitialData = True

        'ให้หน้าต่างวาดเสร็จก่อน แล้วจึงโหลดข้อมูลจากฐานข้อมูล
        'ป้องกันหน้าต่างขาว/ดูเหมือนค้างระหว่างเปิดจากหน้าซื้อสินค้า
        'โหลดตรงใน Shown เพื่อให้แน่ใจว่าข้อมูลถูกโหลดทุกครั้งที่เปิด Dialog
        'และไม่ค้างอยู่ในคิว BeginInvoke ของหน้าต่าง Modal
        LoadInitialData()
    End Sub

    Private Sub LoadInitialData()
        isInitializing = True
        LoadFilterCombo()
        LoadProductAsync(txtSearch.Text)
    End Sub

    Private Async Sub LoadProductAsync(keyword As String)
        Try
            Dim searchKeyword As String = If(keyword, "").Trim()
            Dim groupName As String = GetComboText(cbProductGroup)
            Dim categoryName As String = GetComboText(cbCategory)
            Dim brandName As String = GetComboText(cbBrand)
            Dim sqlText As String = BuildProductSql(searchKeyword)

            Dim dtProduct As DataTable = Await Task.Run(Function()
                                                            Return ExecuteProductQuery(sqlText, searchKeyword, groupName, categoryName, brandName)
                                                        End Function)

            If IsDisposed Then Return
            dtDetail.DataSource = dtProduct
            ApplyGridFormat()
            TabControl1.Refresh()
            dtDetail.Refresh()
        Catch ex As Exception
            If Not IsDisposed Then
                MessageBox.Show(ex.Message, "โหลดสินค้าไม่สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            isInitializing = False
        End Try
    End Sub

    Private Function ExecuteProductQuery(sqlText As String, searchKeyword As String,
                                         groupName As String, categoryName As String,
                                         brandName As String) As DataTable
        Dim dtProduct As New DataTable()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sqlText, conn)
                cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = searchKeyword
                If groupName <> "" AndAlso groupName <> AllText Then cmd.Parameters.Add("@GroupName", SqlDbType.NVarChar, 200).Value = groupName
                If categoryName <> "" AndAlso categoryName <> AllText Then cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 200).Value = categoryName
                If brandName <> "" AndAlso brandName <> AllText Then cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar, 200).Value = brandName
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtProduct)
                End Using
            End Using
        End Using
        Return dtProduct
    End Function

    Private Sub ShowSearchTab()
        TabControl1.SelectedTab = TabPage1
    End Sub
    Private Sub LoadFilterCombo()
        LoadComboFromSql(cbProductGroup, "SELECT RTRIM(Name) AS Name FROM ProductGroup WHERE Status IS NULL ORDER BY Name", AllText)
        LoadComboFromSql(cbCategory, "SELECT RTRIM(Name) AS Name FROM Category WHERE Status IS NULL ORDER BY Name", AllText)
        LoadComboFromSql(cbBrand, "SELECT RTRIM(Name) AS Name FROM Brand WHERE Status IS NULL ORDER BY Name", AllText)

        SetComboItems(cbStatus, StatusNormalText, StatusCancelText, AllText)
        cbStatus.SelectedIndex = 0
    End Sub

    Private Sub SetComboItems(combo As Krypton.Toolkit.KryptonComboBox, ParamArray items As String())
        combo.Items.Clear()
        combo.Items.AddRange(items)
    End Sub

    Private Sub LoadComboFromSql(combo As Krypton.Toolkit.KryptonComboBox, sqlText As String, firstItem As String)
        combo.Items.Clear()
        combo.Items.Add(firstItem)

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sqlText, conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        combo.Items.Add(reader("Name").ToString())
                    End While
                End Using
            End Using
        End Using

        combo.SelectedIndex = 0
    End Sub

    Public Function LoadProduct(keyword As String) As Boolean
        Try
            Dim searchKeyword As String = If(keyword, "").Trim()
            Dim dtProduct As New DataTable()
            Dim sqlText As String = BuildProductSql(searchKeyword)

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sqlText, conn)
                    AddSearchParameters(cmd, searchKeyword)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dtProduct)
                    End Using
                End Using
            End Using

            dtDetail.DataSource = dtProduct
            ApplyGridFormat()
            Return dtProduct.Rows.Count > 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function BuildProductSql(searchKeyword As String) As String
        Dim sb As New StringBuilder()

        'โหมดเลือกสินค้าจากหน้ากำหนดโปรโมชั่นต้องค้นหาได้ครบทุก PN
        'เพราะเครื่องดื่มแต่ละแบบ (ร้อน/เย็น/ปั่น) เป็น Product คนละรายการ
        'ถ้าจำกัด TOP 200 ในโหมดนี้ รายการปั่นบางตัวจะหายจากหน้าที่เลือก
        If IsSelectMode Then
            sb.AppendLine("SELECT")
        ElseIf String.IsNullOrWhiteSpace(searchKeyword) Then
            sb.AppendLine("SELECT TOP (200)")
        Else
            sb.AppendLine("SELECT")
        End If
        sb.AppendLine("Product.PN,")
        sb.AppendLine("RTRIM(Product.ProductCode) AS ProductCode,")
        sb.AppendLine("RTRIM(Product.Barcode) AS Barcode,")
        sb.AppendLine("RTRIM(Product.Name) AS ProductName,")
        sb.AppendLine("Product.Price1 AS SellPrice1,")
        sb.AppendLine("Product.Price2 AS SellPrice2,")
        sb.AppendLine("Product.Price3 AS SellPrice3,")
        sb.AppendLine("Product.Price4 AS SellPrice4,")
        sb.AppendLine("Product.Price5 AS SellPrice5,")
        sb.AppendLine("Product.MinPrice AS MinPrice,")
        sb.AppendLine("Product.StandardCost AS StandardCost,")
        sb.AppendLine("Product.SUnit AS SUnit,")
        sb.AppendLine("RTRIM(Product.Name2) AS Name2,")
        sb.AppendLine("Product.NumPrice2,")
        sb.AppendLine("Product.NumPrice3,")
        sb.AppendLine("Product.NumPrice4,")
        sb.AppendLine("Product.NumPrice5,")
        sb.AppendLine("Product.AllowDecimal,")
        sb.AppendLine("Product.MDiscountP1,")
        sb.AppendLine("Product.MDiscountP2,")
        sb.AppendLine("Product.MDiscountP3,")
        sb.AppendLine("Product.Width,")
        sb.AppendLine("Product.Length,")
        sb.AppendLine("Product.Height,")
        sb.AppendLine("Product.Weight,")
        sb.AppendLine("Product.GrossWeight,")
        sb.AppendLine("ProductAlert.NumAlert AS NumAlert,")
        sb.AppendLine("ProductAlert.NumMustHave AS NumHave,")
        sb.AppendLine("RTRIM(ProductGroup.Name) AS GroupName,")
        sb.AppendLine("Category.ID AS CategoryID,")
        sb.AppendLine("RTRIM(Category.Name) AS TypeName,")
        sb.AppendLine("RTRIM(Brand.Name) AS BrandName,")
        sb.AppendLine("RTRIM(Product.MainSupplier) AS MainSupplier,")
        sb.AppendLine("Product.VatStatus,")
        sb.AppendLine("Product.CanCollectPoint,")
        sb.AppendLine("Product.Point,")
        sb.AppendLine("Product.HotProduct,")
        sb.AppendLine("RTRIM(Product.Comment) AS Comment,")
        sb.AppendLine("Product.ShowOnWeb,")
        sb.AppendLine("RTRIM(Product.WebDetail) AS WebDetail,")
        sb.AppendLine("Product.Status,")
        sb.AppendLine("RTRIM(COff.Name) AS CreatOfficer,")
        sb.AppendLine("Product.CTime AS CreatTime,")
        sb.AppendLine("RTRIM(UOff.Name) AS UpdateOficer,")
        sb.AppendLine("Product.UTime AS UpdateTime,")
        sb.AppendLine("ISNULL(StockSum.TotalStock, 0.00) AS Stock")
        sb.AppendLine("FROM Product")
        'คำนวณ Stock เฉพาะสินค้าที่อยู่ในผลลัพธ์ปัจจุบัน
        'การ GROUP BY Stock ทั้งตารางก่อนหน้านี้ทำให้หน้าเลือกสินค้าช้ามาก
        'โดยเฉพาะเมื่อเปิดจากหน้าซื้อสินค้า
        sb.AppendLine("OUTER APPLY (")
        sb.AppendLine("    SELECT SUM(Number) AS TotalStock")
        sb.AppendLine("    FROM Stock")
        sb.AppendLine("    WHERE Stock.PN = Product.PN")
        sb.AppendLine("      AND Stock.BranchNumber = 1")
        sb.AppendLine(") StockSum")
        sb.AppendLine("LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID")
        sb.AppendLine("LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID")
        sb.AppendLine("LEFT JOIN Brand ON Brand.ID = Product.BrandID")
        sb.AppendLine("LEFT JOIN ProductAlert ON ProductAlert.PN = Product.PN")
        sb.AppendLine("LEFT JOIN Officer COff ON COff.OfficerNumber = Product.CONumber")
        sb.AppendLine("LEFT JOIN Officer UOff ON UOff.OfficerNumber = Product.UONumber")
        sb.AppendLine("WHERE 1 = 1")

        If IsSelectMode AndAlso OnlyNegativeStockSelection Then
            sb.AppendLine("AND ISNULL(StockSum.TotalStock, 0.00) < 0")
        End If

        Dim statusText As String = GetComboText(cbStatus)
        If statusText = StatusNormalText OrElse statusText = "" Then
            sb.AppendLine("AND Product.Status IS NULL")
        ElseIf statusText = StatusCancelText Then
            sb.AppendLine("AND Product.Status IS NOT NULL")
        End If

        If GetComboText(cbProductGroup) <> "" AndAlso GetComboText(cbProductGroup) <> AllText Then
            sb.AppendLine("AND ProductGroup.Name = @GroupName")
        End If

        If GetComboText(cbCategory) <> "" AndAlso GetComboText(cbCategory) <> AllText Then
            sb.AppendLine("AND Category.Name = @CategoryName")
        End If

        If GetComboText(cbBrand) <> "" AndAlso GetComboText(cbBrand) <> AllText Then
            sb.AppendLine("AND Brand.Name = @BrandName")
        End If

        If HideBeverageProducts Then
            sb.AppendLine("AND ISNULL(Category.ID, 0) <> 5")
            sb.AppendLine("AND ISNULL(Category.Name, '') NOT LIKE N'%เครื่องดื่ม%'")
            sb.AppendLine("AND ISNULL(ProductGroup.Name, '') NOT LIKE N'%เครื่องดื่ม%'")
            sb.AppendLine("AND ISNULL(Product.SUnit, '') NOT LIKE N'%แก้ว%'")
        End If

        sb.AppendLine("AND (@Keyword = '' OR Product.Name LIKE N'%' + @Keyword + N'%' OR Product.Barcode LIKE N'%' + @Keyword + N'%' OR Product.ProductCode LIKE N'%' + @Keyword + N'%')")
        sb.AppendLine("ORDER BY Product.ProductCode ASC")

        Return sb.ToString()
    End Function

    Private Sub AddSearchParameters(cmd As SqlCommand, searchKeyword As String)
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = searchKeyword

        Dim groupName As String = GetComboText(cbProductGroup)
        If groupName <> "" AndAlso groupName <> AllText Then
            cmd.Parameters.Add("@GroupName", SqlDbType.NVarChar, 200).Value = groupName
        End If

        Dim categoryName As String = GetComboText(cbCategory)
        If categoryName <> "" AndAlso categoryName <> AllText Then
            cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 200).Value = categoryName
        End If

        Dim brandName As String = GetComboText(cbBrand)
        If brandName <> "" AndAlso brandName <> AllText Then
            cmd.Parameters.Add("@BrandName", SqlDbType.NVarChar, 200).Value = brandName
        End If
    End Sub

    Private Function GetComboText(combo As Krypton.Toolkit.KryptonComboBox) As String
        If combo Is Nothing OrElse combo.SelectedItem Is Nothing Then Return ""
        Return combo.SelectedItem.ToString().Trim()
    End Function

    Private Sub ApplyGridFormat()
        Dim settings As Tuple(Of String, String, Integer)() = {
            Tuple.Create("PN", "ID", 50),
            Tuple.Create("ProductCode", "รหัสสินค้า", 120),
            Tuple.Create("Barcode", "Barcode หลัก", 130),
            Tuple.Create("ProductName", "ชื่อสินค้า", 300),
            Tuple.Create("Stock", "Stock", 90),
            Tuple.Create("SellPrice1", "ราคาขาย 1", 100),
            Tuple.Create("SellPrice2", "ราคาขาย 2", 100),
            Tuple.Create("SellPrice3", "ราคาขาย 3", 100),
            Tuple.Create("SellPrice4", "ราคาขาย 4", 100),
            Tuple.Create("SellPrice5", "ราคาขาย 5", 100),
            Tuple.Create("MinPrice", "ราคาขายต่ำสุด", 120),
            Tuple.Create("StandardCost", "ต้นทุนมาตรฐาน", 120),
            Tuple.Create("SUnit", "หน่วยที่เล็กที่สุด", 130),
            Tuple.Create("Name2", "ชื่อสินค้า 2", 250),
            Tuple.Create("NumPrice2", "จำนวนที่ได้ราคาช่อง 2", 160),
            Tuple.Create("NumPrice3", "จำนวนที่ได้ราคาช่อง 3", 160),
            Tuple.Create("NumPrice4", "จำนวนที่ได้ราคาช่อง 4", 160),
            Tuple.Create("NumPrice5", "จำนวนที่ได้ราคาช่อง 5", 160),
            Tuple.Create("AllowDecimal", "จำนวนเป็นทศนิยมได้", 160),
            Tuple.Create("MDiscountP1", "ส่วนลดสมาชิก 1 (%)", 150),
            Tuple.Create("MDiscountP2", "ส่วนลดสมาชิก 2 (%)", 150),
            Tuple.Create("MDiscountP3", "ส่วนลดสมาชิก 3 (%)", 150),
            Tuple.Create("Width", "กว้าง", 90),
            Tuple.Create("Length", "ยาว", 90),
            Tuple.Create("Height", "สูง", 90),
            Tuple.Create("Weight", "น้ำหนัก", 110),
            Tuple.Create("GrossWeight", "น้ำหนักรวมบรรจุ", 140),
            Tuple.Create("NumAlert", "จำนวนที่เตือนใกล้หมด", 160),
            Tuple.Create("NumHave", "จำนวนที่ต้องการให้มี", 160),
            Tuple.Create("TypeName", "ประเภทสินค้า", 140),
            Tuple.Create("GroupName", "กลุ่มสินค้า", 140),
            Tuple.Create("BrandName", "Brand (ยี่ห้อสินค้า)", 150),
            Tuple.Create("MainSupplier", "Supplier หลัก", 170),
            Tuple.Create("VatStatus", "VAT", 60),
            Tuple.Create("CanCollectPoint", "สะสมแต้มได้", 120),
            Tuple.Create("Point", "Point (แต้มที่ได้)", 140),
            Tuple.Create("HotProduct", "Hot Product", 120),
            Tuple.Create("Comment", "Comment (หมายเหตุ)", 180),
            Tuple.Create("ShowOnWeb", "แสดงบน Website", 140),
            Tuple.Create("WebDetail", "คำอธิบาย Website", 180),
            Tuple.Create("Status", "Status", 100),
            Tuple.Create("CreatOfficer", "Create Officer", 170),
            Tuple.Create("CreatTime", "Create Time", 160),
            Tuple.Create("UpdateOficer", "Update Officer", 150),
            Tuple.Create("UpdateTime", "Update Time", 160)
        }

        For Each fieldName As String In {"AllowDecimal", "VatStatus", "CanCollectPoint", "HotProduct", "ShowOnWeb"}
            ReplaceWithCheckBoxColumn(fieldName)
        Next

        For Each column As DataGridViewColumn In dtDetail.Columns
            column.Visible = False
            column.ReadOnly = True
            column.SortMode = DataGridViewColumnSortMode.Automatic
        Next

        For displayIndex As Integer = 0 To settings.Length - 1
            Dim fieldName = settings(displayIndex).Item1
            If Not dtDetail.Columns.Contains(fieldName) Then Continue For

            Dim column = dtDetail.Columns(fieldName)
            column.HeaderText = settings(displayIndex).Item2
            column.Width = settings(displayIndex).Item3
            column.DisplayIndex = displayIndex
            column.Visible = True
        Next

        For Each fieldName As String In {"Stock", "SellPrice1", "SellPrice2", "SellPrice3", "SellPrice4", "SellPrice5", "MinPrice", "StandardCost"}
            If dtDetail.Columns.Contains(fieldName) Then
                dtDetail.Columns(fieldName).DefaultCellStyle.Format = "N2"
                dtDetail.Columns(fieldName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next

        For Each fieldName As String In {"SellPrice2", "SellPrice3", "SellPrice4", "SellPrice5"}
            If dtDetail.Columns.Contains(fieldName) Then
                dtDetail.Columns(fieldName).DefaultCellStyle.ForeColor = Color.DarkGray
                dtDetail.Columns(fieldName).DefaultCellStyle.SelectionForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub ReplaceWithCheckBoxColumn(fieldName As String)
        If Not dtDetail.Columns.Contains(fieldName) Then Return
        If TypeOf dtDetail.Columns(fieldName) Is DataGridViewCheckBoxColumn Then Return

        Dim oldColumn = dtDetail.Columns(fieldName)
        Dim checkColumn As New DataGridViewCheckBoxColumn() With {
            .Name = fieldName,
            .DataPropertyName = fieldName,
            .HeaderText = oldColumn.HeaderText,
            .ReadOnly = True,
            .TrueValue = 1,
            .FalseValue = 0,
            .IndeterminateValue = DBNull.Value,
            .ThreeState = False
        }

        dtDetail.Columns.Remove(oldColumn)
        dtDetail.Columns.Add(checkColumn)
    End Sub

    Private Sub dtDetail_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dtDetail.CellFormatting
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If dtDetail.Columns(e.ColumnIndex).DataPropertyName <> "Stock" Then Return

        Dim stock = GetRowDecimal(dtDetail.Rows(e.RowIndex), "Stock")
        e.CellStyle.BackColor = If(stock > 0D, Color.FromArgb(198, 239, 206), Color.Salmon)
    End Sub

    Private Sub tbAutoInputSet_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            LoadProduct(txtSearch.Text)
        End If
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            LoadProduct(txtSearch.Text)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadProduct(txtSearch.Text)
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadProduct(txtSearch.Text)
    End Sub


    Private Sub FilterCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProductGroup.SelectedIndexChanged, cbCategory.SelectedIndexChanged, cbStatus.SelectedIndexChanged, cbBrand.SelectedIndexChanged
        If isInitializing Then Return
        If Me.IsHandleCreated Then LoadProduct(txtSearch.Text)
    End Sub

    Private Sub SelectCurrentRowAndClose()
        Dim row = dtDetail.CurrentRow
        If row Is Nothing OrElse row.IsNewRow Then Return

        Dim stock As Decimal = GetRowDecimal(row, "Stock")
        If stock <= 0D AndAlso Not IsBeverageProduct(row) AndAlso Not AllowOutOfStockSelection Then
            MessageBox.Show("ของหมด", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SelectedPN = Convert.ToInt64(GetRowValue(row, "PN"))
        SelectedProductCode = Convert.ToString(GetRowValue(row, "ProductCode"))
        SelectedBarcode = Convert.ToString(GetRowValue(row, "Barcode"))
        SelectedProductName = Convert.ToString(GetRowValue(row, "ProductName"))
        SelectedSellPrice = GetRowDecimal(row, "SellPrice1")
        SelectedStock = stock
        SelectedAllowDecimal = CInt(GetRowDecimal(row, "AllowDecimal"))
        SelectedUnit = Convert.ToString(GetRowValue(row, "SUnit"))
        SelectedComment = Convert.ToString(GetRowValue(row, "Comment"))

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Function GetRowValue(row As DataGridViewRow, fieldName As String) As Object
        If row Is Nothing OrElse Not dtDetail.Columns.Contains(fieldName) Then Return Nothing
        Return row.Cells(fieldName).Value
    End Function

    Private Function GetRowDecimal(row As DataGridViewRow, fieldName As String) As Decimal
        Dim value = GetRowValue(row, fieldName)
        Dim result As Decimal = 0D
        If value IsNot Nothing AndAlso Not IsDBNull(value) Then Decimal.TryParse(value.ToString(), result)
        Return result
    End Function

    Private Function IsBeverageProduct(row As DataGridViewRow) As Boolean
        Dim categoryId As Integer = 0
        Dim categoryValue = GetRowValue(row, "CategoryID")
        If categoryValue IsNot Nothing AndAlso Not IsDBNull(categoryValue) Then Integer.TryParse(categoryValue.ToString(), categoryId)
        If categoryId = 5 Then Return True

        Dim typeName As String = Convert.ToString(GetRowValue(row, "TypeName")).Trim()
        Dim groupName As String = Convert.ToString(GetRowValue(row, "GroupName")).Trim()
        Dim unitName As String = Convert.ToString(GetRowValue(row, "SUnit")).Trim()
        Dim productName As String = Convert.ToString(GetRowValue(row, "ProductName")).Trim()

        If typeName.Contains("เครื่องดื่ม") Then Return True
        If groupName.Contains("เครื่องดื่ม") Then Return True
        If unitName = "แก้ว" OrElse unitName.Contains("แก้ว") Then Return True

        Return productName.Contains("กาแฟ") _
            OrElse productName.Contains("ชา") _
            OrElse productName.Contains("โกโก้") _
            OrElse productName.Contains("นม") _
            OrElse productName.Contains("โซดา") _
            OrElse productName.Contains("สมูทตี้") _
            OrElse productName.Contains("อิตาเลี่ยน")
    End Function

    Private Sub dtDetail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtDetail.CellDoubleClick
        If e.RowIndex < 0 Then Return
        If IsSelectMode Then
            'ดับเบิ้ลคลิกในโหมดซื้อสินค้าให้ทำงานเหมือนปุ่มเพิ่มรายการที่เลือก
            'เพื่อให้ frmBuy ได้จำนวนซื้อและกำหนดราคา/จำนวนเงินเป็น 0.00 เหมือนกัน
            SelectMultipleRowsAndClose()
        Else
            'Open the same edit form used by the Edit button for the
            'double-clicked product row.
            btnEditProduct.PerformClick()
        End If
    End Sub

    Private Sub dtDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles dtDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            If IsSelectMode Then
                SelectMultipleRowsAndClose()
            End If
        End If
    End Sub

    Private Class ComboListItem
        Public Property ID As Integer
        Public Property Name As String

        Public Sub New(id As Integer, name As String)
            Me.ID = id
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
    Private Sub LoadWinComboFromSql(combo As ComboBox, sqlText As String, addBlank As Boolean)
        combo.Items.Clear()
        If addBlank Then combo.Items.Add(New ComboListItem(0, ""))

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sqlText, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            combo.Items.Add(New ComboListItem(CInt(reader("ID")), reader("Name").ToString()))
                        End While
                    End Using
                End Using
            End Using
        Catch
        End Try

        If combo.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Function SelectedComboID(combo As ComboBox) As Integer
        Dim item = TryCast(combo.SelectedItem, ComboListItem)
        If item Is Nothing Then Return 0
        Return item.ID
    End Function

    Private Function SelectedComboName(combo As ComboBox) As String
        Dim item = TryCast(combo.SelectedItem, ComboListItem)
        If item Is Nothing Then Return combo.Text.Trim()
        Return item.Name.Trim()
    End Function

    Private Sub SelectComboByName(combo As ComboBox, value As String)
        Dim textValue As String = If(value, "").Trim()
        For i As Integer = 0 To combo.Items.Count - 1
            Dim item = TryCast(combo.Items(i), ComboListItem)
            If item IsNot Nothing AndAlso String.Equals(item.Name.Trim(), textValue, StringComparison.OrdinalIgnoreCase) Then
                combo.SelectedIndex = i
                Exit Sub
            End If
        Next

        If combo.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Function EditDecimal(textBox As TextBox, Optional defaultValue As Decimal = 0D) As Decimal
        If textBox Is Nothing Then Return defaultValue
        Dim textValue As String = textBox.Text.Replace(",", "").Trim()
        If textValue = "" OrElse textValue.Contains("มากกว่า") Then Return defaultValue
        Dim result As Decimal
        If Decimal.TryParse(textValue, result) Then Return result
        Return defaultValue
    End Function

    Private Function EditText(textBox As TextBox) As Object
        If textBox Is Nothing OrElse textBox.Text.Trim() = "" Then Return DBNull.Value
        Return textBox.Text.Trim()
    End Function

    Private Function EditBit(value As Boolean) As Integer
        Return If(value, 1, 0)
    End Function

    Private Function LoginOfficerNumber() As Integer
        If ModSession.LoginOfficerID > 0 Then Return CInt(ModSession.LoginOfficerID)
        Return 1
    End Function

    Private Function NextProductPN(conn As SqlConnection, tr As SqlTransaction) As Long
        Using cmd As New SqlCommand("SELECT ISNULL(MAX(PN), 0) + 1 FROM Product WITH (UPDLOCK, HOLDLOCK)", conn, tr)
            Return CLng(cmd.ExecuteScalar())
        End Using
    End Function

    Private Function IsDuplicateProductValue(conn As SqlConnection, tr As SqlTransaction, fieldName As String, value As String, currentPN As Long) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False
        Dim sql As String = "SELECT COUNT(1) FROM Product WHERE " & fieldName & " = @Value AND PN <> @PN"
        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 200).Value = value.Trim()
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = currentPN
            Return CInt(cmd.ExecuteScalar()) > 0
        End Using
    End Function
    Private Sub btnEditClose_Click(sender As Object, e As EventArgs)
        If IsSelectMode Then
            Me.Close()
        Else
            ShowSearchTab()
        End If
    End Sub

    Private Sub btnAddProduct_Click(sender As Object, e As EventArgs) Handles btnAddProduct.Click
        If IsSelectMode Then
            SelectMultipleRowsAndClose()
            Return
        End If

        Using productForm As New FrmProductAdd()
            If productForm.ShowDialog(Me) = DialogResult.OK Then
                LoadProduct(txtSearch.Text)
            End If
        End Using
    End Sub

    Private Sub SelectMultipleRowsAndClose()
        SelectedProducts.Clear()

        For Each row As DataGridViewRow In dtDetail.SelectedRows
            If row Is Nothing OrElse row.IsNewRow Then Continue For

            Dim stock As Decimal = GetRowDecimal(row, "Stock")
            If OnlyNegativeStockSelection AndAlso stock >= 0D Then Continue For

            SelectedProducts.Add(New ProductSelection With {
                .PN = Convert.ToInt64(GetRowValue(row, "PN")),
                .ProductCode = Convert.ToString(GetRowValue(row, "ProductCode")).Trim(),
                .Barcode = Convert.ToString(GetRowValue(row, "Barcode")).Trim(),
                .ProductName = Convert.ToString(GetRowValue(row, "ProductName")).Trim(),
                .SellPrice = GetRowDecimal(row, "SellPrice1"),
                .Stock = stock,
                .AllowDecimal = CInt(GetRowDecimal(row, "AllowDecimal")),
                .Unit = Convert.ToString(GetRowValue(row, "SUnit")).Trim(),
                .Comment = Convert.ToString(GetRowValue(row, "Comment")).Trim()
            })
        Next

        If SelectedProducts.Count = 0 Then
            MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการเพิ่มอย่างน้อย 1 รายการ", "สินค้า",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        'เก็บรายการแรกไว้ให้โค้ดเดิมที่เลือกสินค้าแบบรายการเดียวทำงานต่อได้
        Dim first = SelectedProducts(0)
        SelectedPN = first.PN
        SelectedProductCode = first.ProductCode
        SelectedBarcode = first.Barcode
        SelectedProductName = first.ProductName
        SelectedSellPrice = first.SellPrice
        SelectedStock = first.Stock
        SelectedAllowDecimal = first.AllowDecimal
        SelectedUnit = first.Unit
        SelectedComment = first.Comment
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnEditProduct_Click(sender As Object, e As EventArgs) Handles btnEditProduct.Click
        If dtDetail.CurrentRow Is Nothing OrElse dtDetail.CurrentRow.IsNewRow Then
            MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการแก้ไข 1 รายการ", "แจ้งเตือน",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim productPN As Long
        If Not Long.TryParse(Convert.ToString(GetRowValue(dtDetail.CurrentRow, "PN")), productPN) OrElse productPN <= 0 Then
            MessageBox.Show("ไม่พบ Product ID ของสินค้าที่เลือก", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using productForm As New FrmProductAdd()
            productForm.EditProductPN = productPN
            productForm.IsBeverageFromProductList = IsBeverageProduct(dtDetail.CurrentRow)
            productForm.BeverageCategoryNameFromProductList = Convert.ToString(GetRowValue(dtDetail.CurrentRow, "TypeName")).Trim()
            productForm.BeverageGroupNameFromProductList = Convert.ToString(GetRowValue(dtDetail.CurrentRow, "GroupName")).Trim()
            If productForm.ShowDialog(Me) = DialogResult.OK Then
                LoadProduct(txtSearch.Text)
            End If
        End Using
    End Sub

    Private Sub btnCloneProduct_Click_1(sender As Object, e As EventArgs) Handles btnCloneProduct.Click
        If dtDetail.CurrentRow Is Nothing OrElse dtDetail.CurrentRow.IsNewRow Then
            MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการ Clone 1 รายการ", "แจ้งเตือน",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim sourcePN As Long
        If Not Long.TryParse(Convert.ToString(GetRowValue(dtDetail.CurrentRow, "PN")), sourcePN) OrElse sourcePN <= 0 Then
            MessageBox.Show("ไม่พบ Product ID ของสินค้าที่เลือก", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using productForm As New FrmProductAdd()
            productForm.CloneSourcePN = sourcePN
            productForm.IsBeverageFromProductList = IsBeverageProduct(dtDetail.CurrentRow)
            productForm.BeverageCategoryNameFromProductList = Convert.ToString(GetRowValue(dtDetail.CurrentRow, "TypeName")).Trim()
            productForm.BeverageGroupNameFromProductList = Convert.ToString(GetRowValue(dtDetail.CurrentRow, "GroupName")).Trim()
            If productForm.ShowDialog(Me) = DialogResult.OK Then
                LoadProduct(txtSearch.Text)
            End If
        End Using
    End Sub
End Class






