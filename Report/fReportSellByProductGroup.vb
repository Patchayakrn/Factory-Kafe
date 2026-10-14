Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Drawing

Public Class fReportSellByProductGroup
    Private ReadOnly reportTable As New DataTable("Report")
    Private Const AllBranches As String = "ทุกสาขา"
    Private productCodeSortAscending As Boolean = True
    Private lblFrozenSummary As Label
    Private lblBeverageSummary As Label
    Private lblCupSummary As Label
    Private lblToppingSummary As Label
    Private lblLidSummary As Label

    Public Sub New()
        InitializeComponent()
        InitializeSummaryCards()
        reportTable.Columns.Add("RowNumber", GetType(Integer))
        reportTable.Columns.Add("ProductCode", GetType(String))
        reportTable.Columns.Add("Barcode", GetType(String))
        reportTable.Columns.Add("ProductName", GetType(String))
        reportTable.Columns.Add("Number", GetType(Decimal))
        reportTable.Columns.Add("Price1", GetType(Decimal))
        reportTable.Columns.Add("Price1Total", GetType(Decimal))
        reportTable.Columns.Add("CategoryName", GetType(String))
        reportTable.Columns.Add("GroupName", GetType(String))
        reportTable.Columns.Add("CategoryID", GetType(Integer))
        reportTable.Columns.Add("SUnit", GetType(String))
        reportTable.Columns.Add("NormalCupQty", GetType(Decimal))
        reportTable.Columns.Add("HotCupQty", GetType(Decimal))
        reportTable.Columns.Add("DomeLidQty", GetType(Decimal))
        reportTable.Columns.Add("FlatLidQty", GetType(Decimal))
        reportTable.Columns.Add("HotLidQty", GetType(Decimal))
        Column1.DataPropertyName = "RowNumber"
        Column2.DataPropertyName = "ProductCode"
        Column3.DataPropertyName = "Barcode"
        Column4.DataPropertyName = "ProductName"
        Column5.DataPropertyName = "Number"
        Column6.DataPropertyName = "Price1"
        Column7.DataPropertyName = "Price1Total"
        Column5.DefaultCellStyle.Format = "N2"
        Column6.DefaultCellStyle.Format = "N2"
        Column7.DefaultCellStyle.Format = "N2"
        Column2.SortMode = DataGridViewColumnSortMode.Programmatic
        grid.AutoGenerateColumns = False
        grid.DataSource = reportTable
        UpdateSummaryCards(0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D)
        ApplyProductCodeSort()
    End Sub

    Private Sub InitializeSummaryCards()
        'ขยับการ์ดชิดซ้ายและเว้นพื้นที่ด้านขวาไว้สำหรับการ์ดใบถัดไป
        lblFrozenSummary = CreateSummaryCard("สรุปแช่แข็ง", New Point(420, 12), Color.FromArgb(64, 139, 190))
        lblBeverageSummary = CreateSummaryCard("สรุปเครื่องดื่ม", New Point(605, 12), Color.FromArgb(44, 160, 125))
        lblCupSummary = CreateSummaryCard("จำนวนแก้ว", New Point(790, 12), Color.FromArgb(37, 99, 235))
        lblToppingSummary = CreateSummaryCard("จำนวน Topping", New Point(975, 12), Color.FromArgb(234, 88, 12))
        lblLidSummary = CreateSummaryCard("จำนวนฝา", New Point(1160, 12), Color.FromArgb(126, 87, 194))
    End Sub

    Private Function CreateSummaryCard(title As String, location As Point, backColor As Color) As Label
        Dim isLidCard As Boolean = String.Equals(title, "จำนวนฝา", StringComparison.Ordinal)
        Dim cardHeight As Integer = If(isLidCard, 116, 82)
        Dim valueHeight As Integer = If(isLidCard, 82, 48)
        Dim card As New Panel With {.BackColor = backColor, .Location = location, .Size = New Size(175, cardHeight), .Anchor = AnchorStyles.Top}
        Dim titleLabel As New Label With {.Text = title, .ForeColor = Color.White, .Font = New Font("Cordia New", 17.0!, FontStyle.Bold), .Location = New Point(8, 1), .Size = New Size(160, 30)}
        Dim valueFontSize As Single = If(isLidCard, 13.0!,
                                         If(String.Equals(title, "จำนวนแก้ว", StringComparison.Ordinal), 16.0!, 20.0!))
        Dim valueLabel As New Label With {.ForeColor = Color.White, .Font = New Font("Cordia New", valueFontSize, FontStyle.Bold), .Location = New Point(8, 31), .Size = New Size(160, valueHeight), .AutoSize = False}
        card.Controls.Add(titleLabel)
        card.Controls.Add(valueLabel)
        panelFilter.Controls.Add(card)
        card.BringToFront()
        Return valueLabel
    End Function

    Private Sub UpdateSummaryCards(frozenQty As Decimal,
                                   frozenAmount As Decimal,
                                   beverageQty As Decimal,
                                   beverageAmount As Decimal,
                                   normalCupQty As Decimal,
                                   hotCupQty As Decimal,
                                   toppingQty As Decimal,
                                   domeLidQty As Decimal,
                                   flatLidQty As Decimal,
                                   hotLidQty As Decimal)
        If lblFrozenSummary IsNot Nothing Then lblFrozenSummary.Text = frozenAmount.ToString("N2") & " บาท"
        If lblBeverageSummary IsNot Nothing Then lblBeverageSummary.Text = beverageAmount.ToString("N2") & " บาท"
        If lblCupSummary IsNot Nothing Then
            lblCupSummary.Text = "แก้ว " & normalCupQty.ToString("N0") & Environment.NewLine &
                                 "แก้วร้อน " & hotCupQty.ToString("N0")
        End If
        If lblToppingSummary IsNot Nothing Then lblToppingSummary.Text = toppingQty.ToString("N0") & " รายการ"
        If lblLidSummary IsNot Nothing Then
            lblLidSummary.Text = "ฝาโดม " & domeLidQty.ToString("N0") & Environment.NewLine &
                                  "ฝาเรียบ " & flatLidQty.ToString("N0") & Environment.NewLine &
                                  "ฝาร้อน " & hotLidQty.ToString("N0")
        End If
    End Sub

    Private Sub UpdateSummaryCardsFromReportTable()
        Dim frozenQty As Decimal = 0D, frozenAmount As Decimal = 0D
        Dim beverageQty As Decimal = 0D, beverageAmount As Decimal = 0D
        Dim normalCupQty As Decimal = 0D, hotCupQty As Decimal = 0D, toppingQty As Decimal = 0D
        Dim domeLidQty As Decimal = 0D, flatLidQty As Decimal = 0D, hotLidQty As Decimal = 0D
        For Each row As DataRow In reportTable.Rows
            Dim categoryName As String = Convert.ToString(row("CategoryName")).Trim()
            Dim groupName As String = Convert.ToString(row("GroupName")).Trim()
            Dim unitName As String = Convert.ToString(row("SUnit")).Trim()
            Dim categoryId As Integer = Convert.ToInt32(row("CategoryID"), CultureInfo.InvariantCulture)
            'แบ่งประเภทให้ตรงกับตารางสินค้า: แช่แข็งคือทุกรายการ
            'ยกเว้นสินค้าที่อยู่ในประเภทเครื่องดื่มเท่านั้น
            'ไม่ใช้ชื่อกลุ่ม/หน่วย/ชื่อสินค้าเดา เพราะกลุ่ม "อื่นๆ" อาจอยู่ใต้ประเภทเครื่องดื่ม
            Dim isBeverage As Boolean = categoryId = 5 OrElse categoryName.IndexOf("เครื่องดื่ม", StringComparison.OrdinalIgnoreCase) >= 0
            Dim qty As Decimal = Convert.ToDecimal(row("Number"), CultureInfo.InvariantCulture)
            'ใช้ยอดรวมจากคอลัมน์เดียวกับตาราง (รวมส่วนลดโปรโมชั่นแล้ว)
            Dim amount As Decimal = Convert.ToDecimal(row("Price1Total"), CultureInfo.InvariantCulture)
            Dim isPlainIce As Boolean = Convert.ToString(row("ProductName")).IndexOf("น้ำแข็งเปล่า", StringComparison.OrdinalIgnoreCase) >= 0
            Dim isTopping As Boolean = isBeverage AndAlso Not isPlainIce AndAlso
                                       (groupName.IndexOf("อื่น", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                        categoryName.IndexOf("อื่น", StringComparison.OrdinalIgnoreCase) >= 0)
            If isBeverage Then
                beverageQty += qty
                beverageAmount += amount
                'จำนวนแก้วแยกตามแก้วที่ระบุในสูตร BOM ของเมนู
                If Not isTopping Then
                    normalCupQty += Convert.ToDecimal(row("NormalCupQty"), CultureInfo.InvariantCulture)
                    hotCupQty += Convert.ToDecimal(row("HotCupQty"), CultureInfo.InvariantCulture)
                    domeLidQty += Convert.ToDecimal(row("DomeLidQty"), CultureInfo.InvariantCulture)
                    flatLidQty += Convert.ToDecimal(row("FlatLidQty"), CultureInfo.InvariantCulture)
                    hotLidQty += Convert.ToDecimal(row("HotLidQty"), CultureInfo.InvariantCulture)
                End If
                'Topping นับแยกต่างหากจากเครื่องดื่มกลุ่มอื่นๆ
                If isTopping Then toppingQty += qty
            Else
                frozenQty += qty
                frozenAmount += amount
            End If
        Next
        UpdateSummaryCards(frozenQty, frozenAmount, beverageQty, beverageAmount, normalCupQty, hotCupQty, toppingQty,
                           domeLidQty, flatLidQty, hotLidQty)
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFrom.Value = Date.Today
        dtpTo.Value = Date.Today
        cboBranch.Items.Clear()
        cboBranch.Items.Add(AllBranches)
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT BranchNumber, RTRIM(BranchName) FROM Branch ORDER BY BranchNumber", conn)
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            cboBranch.Items.Add(rd.GetInt32(0).ToString(CultureInfo.InvariantCulture) & " - " & rd.GetString(1).Trim())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลสาขาไม่สำเร็จ: " & ex.Message, "Report Sell", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        cboBranch.SelectedIndex = If(cboBranch.Items.Count > 1, 1, 0)
        UpdateDetail()
    End Sub

    Private Function SelectedBranchNumber() As Integer?
        If cboBranch.SelectedIndex <= 0 Then Return Nothing
        Dim value As Integer
        If Integer.TryParse(cboBranch.Text.Split("-"c)(0).Trim(), value) Then Return value
        Return Nothing
    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If dtpFrom.Value.Date > dtpTo.Value.Date Then
            MessageBox.Show("วันที่เริ่มต้นต้องไม่มากกว่าวันที่สิ้นสุด", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim branch As Integer? = SelectedBranchNumber()
        'ใช้ราคาขายของสินค้าเป็นฐาน แล้วค้นหาส่วนลดโปรโมชั่นของสินค้านั้นจากรายการโปรโมชั่นของบิล
        'ส่วนลดของสินค้าตัวหนึ่งจะไม่ถูกเฉลี่ยไปลดสินค้าตัวอื่นในบิลเดียวกัน
        Dim sql As String = "SELECT RTRIM(Product.ProductCode) AS ProductCode, RTRIM(Product.Barcode) AS Barcode, RTRIM(Product.Name) AS ProductName, " &
                            "RTRIM(Category.Name) AS CategoryName, RTRIM(ProductGroup.Name) AS GroupName, ISNULL(ProductGroup.CategoryID, 0) AS CategoryID, RTRIM(Product.SUnit) AS SUnit, " &
                             "SUM(SellD.Number) AS TotalNumber, " &
                             "SUM(SellD.Number) * MAX(ISNULL(CupFormula.NormalCupPerUnit, 0)) AS NormalCupQty, " &
                             "SUM(SellD.Number) * MAX(ISNULL(CupFormula.HotCupPerUnit, 0)) AS HotCupQty, " &
                             "SUM(SellD.Number) * MAX(ISNULL(CupFormula.DomeLidPerUnit, 0)) AS DomeLidQty, " &
                             "SUM(SellD.Number) * MAX(ISNULL(CupFormula.FlatLidPerUnit, 0)) AS FlatLidQty, " &
                             "SUM(SellD.Number) * MAX(ISNULL(CupFormula.HotLidPerUnit, 0)) AS HotLidQty, " &
                            "CASE WHEN SUM(ISNULL(SellD.Number, 0)) = 0 THEN 0 ELSE " &
                            "SUM(CASE WHEN (CASE WHEN Promo.PromotionFound = 1 THEN ISNULL(SellD.Price, Product.Price1) ELSE ISNULL(Product.Price1, SellD.Price) END) - ISNULL(Promo.DiscountAmount, 0) < 0 THEN 0 " &
                            "ELSE (CASE WHEN Promo.PromotionFound = 1 THEN ISNULL(SellD.Price, Product.Price1) ELSE ISNULL(Product.Price1, SellD.Price) END) - ISNULL(Promo.DiscountAmount, 0) END * ISNULL(SellD.Number, 0)) / SUM(ISNULL(SellD.Number, 0)) END AS UnitPrice1, " &
                            "SUM(CASE WHEN (CASE WHEN Promo.PromotionFound = 1 THEN ISNULL(SellD.Price, Product.Price1) ELSE ISNULL(Product.Price1, SellD.Price) END) - ISNULL(Promo.DiscountAmount, 0) < 0 THEN 0 " &
                            "ELSE (CASE WHEN Promo.PromotionFound = 1 THEN ISNULL(SellD.Price, Product.Price1) ELSE ISNULL(Product.Price1, SellD.Price) END) - ISNULL(Promo.DiscountAmount, 0) END * ISNULL(SellD.Number, 0)) AS TotalPrice1 " &
                            "FROM SellD INNER JOIN Sell ON SellD.BranchNumber = Sell.BranchNumber AND SellD.SellNumber = Sell.SellNumber " &
                            "LEFT JOIN Product ON SellD.PN = Product.PN " &
                            "LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID " &
                             "LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID " &
                             "OUTER APPLY (SELECT " &
                             "SUM(CASE WHEN M.MaterialID = N'F50101' OR RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%แก้ว%ร้อน%' THEN ISNULL(BOM.[Use],0) ELSE 0 END) AS HotCupPerUnit, " &
                             "SUM(CASE WHEN RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%แก้ว%' " &
                             "AND M.MaterialID <> N'F50101' " &
                             "AND RTRIM(ISNULL(M.Material_Detail,N'')) NOT LIKE N'%ร้อน%' THEN ISNULL(BOM.[Use],0) ELSE 0 END) AS NormalCupPerUnit, " &
                             "SUM(CASE WHEN M.MaterialID = N'F50105' OR (RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%ฝา%' AND RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%โดม%') THEN ISNULL(BOM.[Use],0) ELSE 0 END) AS DomeLidPerUnit, " &
                             "SUM(CASE WHEN M.MaterialID = N'F50104' OR (RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%ฝา%' AND RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%เรียบ%') THEN ISNULL(BOM.[Use],0) ELSE 0 END) AS FlatLidPerUnit, " &
                             "SUM(CASE WHEN M.MaterialID = N'F50103' OR (RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%ฝา%' AND RTRIM(ISNULL(M.Material_Detail,N'')) LIKE N'%ร้อน%') THEN ISNULL(BOM.[Use],0) ELSE 0 END) AS HotLidPerUnit " &
                             "FROM BOM LEFT JOIN MaterialDrink M ON M.MaterialID=BOM.MaterialID " &
                             "WHERE BOM.ProductID=RTRIM(Product.ProductCode)) CupFormula " &
                            "OUTER APPLY (SELECT TOP 1 1 AS PromotionFound, CASE WHEN ISNULL(D.DiscountM, 0) > 0 THEN D.DiscountM " &
                            "WHEN ISNULL(D.DiscountP, 0) > 0 THEN ISNULL(SellD.Price, ISNULL(Product.Price1, 0)) * D.DiscountP / 100 " &
                            "WHEN ISNULL(H.DiscountM, 0) > 0 THEN H.DiscountM " &
                            "WHEN ISNULL(H.DiscountP, 0) > 0 THEN ISNULL(SellD.Price, ISNULL(Product.Price1, 0)) * H.DiscountP / 100 " &
                            "WHEN H.Type IN (3, 5) AND CHARINDEX(N'เหลือ', H.Name) > 0 AND TRY_CONVERT(decimal(18,2), LEFT(LTRIM(SUBSTRING(H.Name, CHARINDEX(N'เหลือ', H.Name) + LEN(N'เหลือ'), 50)), CHARINDEX(N' ', LTRIM(SUBSTRING(H.Name, CHARINDEX(N'เหลือ', H.Name) + LEN(N'เหลือ'), 50)) + N' ') - 1)) IS NOT NULL " &
                            "THEN ISNULL(SellD.Price, ISNULL(Product.Price1, 0)) - TRY_CONVERT(decimal(18,2), LEFT(LTRIM(SUBSTRING(H.Name, CHARINDEX(N'เหลือ', H.Name) + LEN(N'เหลือ'), 50)), CHARINDEX(N' ', LTRIM(SUBSTRING(H.Name, CHARINDEX(N'เหลือ', H.Name) + LEN(N'เหลือ'), 50)) + N' ') - 1)) " &
                            "ELSE 0 END AS DiscountAmount FROM PMT H INNER JOIN PMTD D ON D.ID = H.ID " &
                            "AND D.Part = 1 AND D.PN = SellD.PN WHERE 1 = 1 " &
                            "AND Sell.CTime >= H.SDate AND Sell.CTime < DATEADD(DAY, 1, H.EDate) " &
                            "ORDER BY H.Priority, H.ID) Promo " &
                            "WHERE Sell.Status IS NULL AND Sell.CTime >= @DateFrom AND Sell.CTime < @DateTo " &
                            If(branch.HasValue, "AND Sell.BranchNumber = @BranchNumber ", "") &
                            "GROUP BY Product.ProductCode, Product.Barcode, Product.Name, Category.Name, ProductGroup.Name, ProductGroup.CategoryID, Product.SUnit ORDER BY Product.Name"
        reportTable.Clear()
        Cursor = Cursors.WaitCursor
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtpFrom.Value.Date
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtpTo.Value.Date.AddDays(1)
                    If branch.HasValue Then cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branch.Value
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            Dim row As DataRow = reportTable.NewRow()
                            row(0) = reportTable.Rows.Count + 1
                            row(1) = If(rd.IsDBNull(rd.GetOrdinal("ProductCode")), "", Convert.ToString(rd("ProductCode")).Trim())
                            row(2) = If(rd.IsDBNull(rd.GetOrdinal("Barcode")), "", Convert.ToString(rd("Barcode")).Trim())
                            row(3) = If(rd.IsDBNull(rd.GetOrdinal("ProductName")), "", Convert.ToString(rd("ProductName")).Trim())
                            row(4) = If(rd.IsDBNull(rd.GetOrdinal("TotalNumber")), 0D, Convert.ToDecimal(rd("TotalNumber"), CultureInfo.InvariantCulture))
                            row(5) = If(rd.IsDBNull(rd.GetOrdinal("UnitPrice1")), 0D, Convert.ToDecimal(rd("UnitPrice1"), CultureInfo.InvariantCulture))
                            row(6) = If(rd.IsDBNull(rd.GetOrdinal("TotalPrice1")), 0D, Convert.ToDecimal(rd("TotalPrice1"), CultureInfo.InvariantCulture))
                            row(7) = If(rd.IsDBNull(rd.GetOrdinal("CategoryName")), "", Convert.ToString(rd("CategoryName")).Trim())
                            row(8) = If(rd.IsDBNull(rd.GetOrdinal("GroupName")), "", Convert.ToString(rd("GroupName")).Trim())
                             row(9) = If(rd.IsDBNull(rd.GetOrdinal("CategoryID")), 0, Convert.ToInt32(rd("CategoryID")))
                             row(10) = If(rd.IsDBNull(rd.GetOrdinal("SUnit")), "", Convert.ToString(rd("SUnit")).Trim())
                             row(11) = If(rd.IsDBNull(rd.GetOrdinal("NormalCupQty")), 0D, Convert.ToDecimal(rd("NormalCupQty"), CultureInfo.InvariantCulture))
                             row(12) = If(rd.IsDBNull(rd.GetOrdinal("HotCupQty")), 0D, Convert.ToDecimal(rd("HotCupQty"), CultureInfo.InvariantCulture))
                             row(13) = If(rd.IsDBNull(rd.GetOrdinal("DomeLidQty")), 0D, Convert.ToDecimal(rd("DomeLidQty"), CultureInfo.InvariantCulture))
                             row(14) = If(rd.IsDBNull(rd.GetOrdinal("FlatLidQty")), 0D, Convert.ToDecimal(rd("FlatLidQty"), CultureInfo.InvariantCulture))
                             row(15) = If(rd.IsDBNull(rd.GetOrdinal("HotLidQty")), 0D, Convert.ToDecimal(rd("HotLidQty"), CultureInfo.InvariantCulture))
                             reportTable.Rows.Add(row)
                        End While
                    End Using
                End Using
            End Using
            ApplyProductCodeSort()
            UpdateSummaryCardsFromReportTable()
            UpdateDetail()
            If reportTable.Rows.Count = 0 Then MessageBox.Show("ไม่พบข้อมูลตามเงื่อนไขที่เลือก", "Report Sell", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("ค้นหารายงานไม่สำเร็จ: " & ex.Message, "Report Sell", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadSummaryCards(branch As Integer?)
        Dim frozenBills As Integer = 0
        Dim beverageBills As Integer = 0
        Dim frozenAmount As Decimal = 0D
        Dim beverageAmount As Decimal = 0D
        Dim sql As String =
            "SELECT CASE WHEN ISNULL(C.ID, 0) = 5 OR ISNULL(C.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG.Name, '') LIKE N'%เครื่องดื่ม%' THEN 1 ELSE 0 END AS IsBeverage, " &
            "COUNT(DISTINCT S.SellNumber) AS BillCount, SUM(ISNULL(SD.SumPrice, 0)) AS Amount " &
            "FROM Sell S INNER JOIN SellD SD ON S.BranchNumber = SD.BranchNumber AND S.SellNumber = SD.SellNumber " &
            "LEFT JOIN Product P ON SD.PN = P.PN LEFT JOIN ProductGroup PG ON PG.ID = P.GroupID " &
            "LEFT JOIN Category C ON C.ID = PG.CategoryID " &
            "WHERE S.Status IS NULL AND S.CTime >= @DateFrom AND S.CTime < @DateTo " &
            If(branch.HasValue, "AND S.BranchNumber = @BranchNumber ", "") &
            "GROUP BY CASE WHEN ISNULL(C.ID, 0) = 5 OR ISNULL(C.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG.Name, '') LIKE N'%เครื่องดื่ม%' THEN 1 ELSE 0 END"
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtpFrom.Value.Date
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtpTo.Value.Date.AddDays(1)
                    If branch.HasValue Then cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branch.Value
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        While rd.Read()
                            Dim isBeverage As Boolean = Convert.ToInt32(rd("IsBeverage")) = 1
                            Dim bills As Integer = Convert.ToInt32(rd("BillCount"))
                            Dim amount As Decimal = Convert.ToDecimal(rd("Amount"), CultureInfo.InvariantCulture)
                            If isBeverage Then
                                beverageBills = bills
                                beverageAmount = amount
                            Else
                                frozenBills = bills
                                frozenAmount = amount
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch
            'ถ้าโหลดสรุปไม่ได้ ให้ตารางรายงานยังใช้งานต่อได้
        End Try
        UpdateSummaryCards(frozenBills, frozenAmount, beverageBills, beverageAmount, 0D, 0D, 0D, 0D, 0D, 0D)
    End Sub

    Private Sub grid_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles grid.ColumnHeaderMouseClick
        If e.ColumnIndex <> Column2.Index Then Return

        productCodeSortAscending = Not productCodeSortAscending
        ApplyProductCodeSort()
    End Sub

    Private Sub ApplyProductCodeSort()
        If reportTable Is Nothing OrElse reportTable.Columns.Count = 0 Then Return

        reportTable.DefaultView.Sort = "ProductCode " & If(productCodeSortAscending, "ASC", "DESC")

        Dim rowNumber As Integer = 1
        For Each rowView As DataRowView In reportTable.DefaultView
            rowView.Row("RowNumber") = rowNumber
            rowNumber += 1
        Next
    End Sub

    Private Function BuildPrintTable() As DataTable
        Dim result As DataTable = reportTable.Clone()
        Dim rowNumber As Integer = 1

        For Each rowView As DataRowView In reportTable.DefaultView
            Dim row As DataRow = result.NewRow()
            row.ItemArray = rowView.Row.ItemArray.Clone()
            row("RowNumber") = rowNumber
            result.Rows.Add(row)
            rowNumber += 1
        Next

        Return result
    End Function

    Private Sub UpdateDetail()
        lblDetail.Text = String.Format("สรุปยอดขายแยกตามประเภทสินค้า  |  {0}  |  {1:dd/MM/yyyy} - {2:dd/MM/yyyy}", If(cboBranch.SelectedIndex <= 0, AllBranches, cboBranch.Text), dtpFrom.Value, dtpTo.Value)
        lblCount.Text = reportTable.Rows.Count.ToString(CultureInfo.InvariantCulture) & " รายการ"
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If reportTable.Rows.Count = 0 Then
            MessageBox.Show("กรุณาค้นหารายงานก่อนพิมพ์", "Report Sell", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using viewer As New frmReportViewer()
            viewer.LoadSellByProductGroupReport(BuildPrintTable(), dtpFrom.Value.Date, dtpTo.Value.Date, If(cboBranch.SelectedIndex <= 0, AllBranches, cboBranch.Text))
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
End Class
