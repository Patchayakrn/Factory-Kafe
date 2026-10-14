Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms

Partial Public Class fReportStock
    Inherits Form

    Private productTable As New DataTable("Product")
    Private movementTable As New DataTable("Movement")
    Private printDoc As New PrintDocument()
    Private printRow As Integer
    Private ReadOnly chkAllProducts As New CheckBox()

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        chkAllProducts.Text = "สินค้าทั้งหมด"
        'ไม่เลือกสินค้าทั้งหมดเป็นค่าเริ่มต้น ให้ผู้ใช้เป็นคนเลือกเอง
        chkAllProducts.Checked = False
        chkAllProducts.AutoSize = True
        chkAllProducts.Dock = DockStyle.Fill
        chkAllProducts.Font = New Font("Cordia New", 16.2!, FontStyle.Regular, GraphicsUnit.Point)
        chkAllProducts.TextAlign = ContentAlignment.MiddleLeft
        filterPanel.Controls.Add(chkAllProducts, 6, 0)
        AddHandler Load, AddressOf Form_Loaded
        ' AddHandler btnFindProduct.Click, AddressOf FindProduct_Click
        AddHandler btnSearch.Click, AddressOf Search_Click
        AddHandler btnPrint.Click, AddressOf Print_Click
        AddHandler printDoc.PrintPage, AddressOf PrintPage
    End Sub

    Private Sub Form_Loaded(sender As Object, e As EventArgs)
        'ฟอร์มนี้ถูกเปิดเป็น MDI Child จากเมนูหลัก
        'ให้ขยายเต็มพื้นที่ MDI เพื่อไม่ให้เหลือพื้นที่ว่างด้านขวา
        If MdiParent IsNot Nothing Then
            WindowState = FormWindowState.Normal
            Dock = DockStyle.Fill
        Else
            WindowState = FormWindowState.Maximized
        End If

        'รายงานความเคลื่อนไหวเปิดมาเป็นช่วงเดือนปัจจุบันโดยอัตโนมัติ
        Dim monthStart As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
        dtpFrom.Value = monthStart
        dtpTo.Value = monthStart.AddMonths(1).AddDays(-1)
        LoadBranches()
        FindProduct_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub LoadBranches()
        Try
            DbSchemaChecker.EnsureSupportTables()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT BranchNumber, RTRIM(BranchName) AS BranchName FROM Branch ORDER BY BranchNumber", conn)
                    Using rd = cmd.ExecuteReader()
                        cboBranch.Items.Clear()
                        While rd.Read()
                            cboBranch.Items.Add(New BranchItem(CInt(rd("BranchNumber")), rd("BranchName").ToString()))
                        End While
                    End Using
                End Using
            End Using
            If cboBranch.Items.Count > 0 Then cboBranch.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("โหลดสาขาไม่สำเร็จ: " & ex.Message, "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FindProduct_Click(sender As Object, e As EventArgs)
        Try
            productTable.Clear()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT P.PN, RTRIM(P.Name) AS ProductName, RTRIM(P.ProductCode) AS ProductCode, RTRIM(P.Barcode) AS Barcode, RTRIM(ISNULL(PG.Name,N'')) AS ProductGroup FROM Product P LEFT JOIN ProductGroup PG ON PG.ID=P.GroupID WHERE P.Status IS NULL AND (P.Name LIKE @Search OR P.ProductCode LIKE @Search OR P.Barcode LIKE @Search) ORDER BY RTRIM(P.ProductCode), P.PN", conn)
                    'หน้าจอ Designer ปัจจุบันไม่มีช่อง txtProduct แล้ว จึงโหลดสินค้าที่ใช้งานทั้งหมด
                    cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = "%"
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(productTable)
                    End Using
                End Using
            End Using
            dgvProducts.DataSource = productTable
            'ให้รหัสสินค้าอยู่คอลัมน์แรก และเรียงรายการตามรหัสสินค้า
            If dgvProducts.Columns.Contains("Column2") Then dgvProducts.Columns("Column2").DisplayIndex = 0
            If dgvProducts.Columns.Contains("Column1") Then dgvProducts.Columns("Column1").DisplayIndex = 1
            'DataGridView มักเลือกแถวแรกให้อัตโนมัติ แต่ไม่ควรถือว่าเป็นการเลือกสินค้าของผู้ใช้
            'เมื่อไม่มีการเลือกจริง Search_Click จะโหลดสินค้าทั้งหมด
            dgvProducts.ClearSelection()
            If dgvProducts.Rows.Count > 0 Then dgvProducts.CurrentCell = Nothing
            lblStatus.Text = "พบสินค้า " & productTable.Rows.Count.ToString("N0") & " รายการ — เลือกสินค้าได้หลายรายการด้วย Ctrl/Shift"
        Catch ex As Exception
            MessageBox.Show("ค้นหาสินค้าไม่สำเร็จ: " & ex.Message, "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Search_Click(sender As Object, e As EventArgs)
        If cboBranch.SelectedItem Is Nothing Then
            MessageBox.Show("กรุณาเลือกสาขา", "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Cursor = Cursors.WaitCursor
            movementTable = CreateMovementTable()
            Dim branchNo As Integer = DirectCast(cboBranch.SelectedItem, BranchItem).Number
            'ต้องติ๊กสินค้าทั้งหมดเอง หรือเลือกสินค้าในตารางเองเท่านั้น
            Dim loadAllProducts As Boolean = chkAllProducts.Checked

            If Not loadAllProducts AndAlso dgvProducts.SelectedRows.Count = 0 Then
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการดูรายงาน หรือเลือก 'สินค้าทั้งหมด'", "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If loadAllProducts AndAlso dgvProducts.Rows.Count = 0 Then
                MessageBox.Show("ไม่พบสินค้าให้แสดงรายงาน", "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            'ถ้าไม่ได้เลือกสินค้า ให้โหลดสินค้าทั้งหมดในรายการสินค้า
            For Each selected As DataGridViewRow In dgvProducts.Rows
                If Not loadAllProducts AndAlso Not selected.Selected Then Continue For
                Dim productView As DataRowView = TryCast(selected.DataBoundItem, DataRowView)
                If productView Is Nothing Then Continue For

                'อ่านค่าจาก DataTable โดยตรง ไม่ผูกกับชื่อคอลัมน์ที่กำหนดใน Designer
                Dim productNo As Long = Convert.ToInt64(productView.Row("PN"))
                Dim productCode As String = Convert.ToString(productView.Row("ProductCode")).Trim()
                Dim productName As String = Convert.ToString(productView.Row("ProductName")).Trim()
                LoadProductMovement(branchNo, productNo, productCode, productName)
            Next

            'จัดลำดับให้สินค้าอยู่ติดกันและเรียงตามวันที่ เพื่อให้ RDLC จัดกลุ่มเป็นรายสินค้าได้ถูกต้อง
            Dim sortedView As DataView = movementTable.DefaultView
            sortedView.Sort = "ProductCode ASC, DocDate ASC, DocNumber ASC"
            movementTable = sortedView.ToTable("StockMovement")
            dgvMovement.DataSource = movementTable
            If dgvMovement.Columns.Contains("DocDate") Then
                dgvMovement.Columns("DocDate").DefaultCellStyle.Format = "dd/MM/yyyy"
            End If
            lblStatus.Text = "รายการเคลื่อนไหว " & movementTable.Rows.Count.ToString("N0") & " รายการ" & If(loadAllProducts, " จากสินค้าทั้งหมด", "")
        Catch ex As Exception
            MessageBox.Show("ค้นหารายงานไม่สำเร็จ: " & ex.Message, "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function CreateMovementTable() As DataTable
        Dim dt As New DataTable("StockMovement")
        dt.Columns.Add("DocDate", GetType(DateTime))
        dt.Columns.Add("ProductCode", GetType(String))
        dt.Columns.Add("ProductName", GetType(String))
        dt.Columns.Add("BillType", GetType(String))
        dt.Columns.Add("DocNumber", GetType(String))
        dt.Columns.Add("BillNumber", GetType(String))
        dt.Columns.Add("Detail", GetType(String))
        dt.Columns.Add("NumIn", GetType(Decimal))
        dt.Columns.Add("NumOut", GetType(Decimal))
        dt.Columns.Add("NumRemain", GetType(Decimal))
        Return dt
    End Function

    Private Sub LoadProductMovement(branchNo As Integer, productNo As Long, productCode As String, productName As String)
        Dim rows As New DataTable()
        Dim sql As String = "SELECT X.DocDate,X.BillType,X.DocNumber,X.BillNumber,X.Detail,X.NumIn,X.NumOut,X.ShowRow FROM (" &
            "SELECT B.DateTime AS DocDate,N'ซื้อเข้า' AS BillType,CONVERT(nvarchar(50),B.DocNumber) AS DocNumber,CONVERT(nvarchar(100),RTRIM(B.BillNumber)) AS BillNumber,CAST(N'' AS nvarchar(500)) AS Detail,SUM(BD.Number) AS NumIn,CAST(0 AS decimal(18,4)) AS NumOut,CAST(1 AS int) AS ShowRow FROM BuyD BD INNER JOIN Buy B ON B.BranchNumber=BD.BranchNumber AND B.BuyNumber=BD.BuyNumber WHERE B.BranchNumber=@Branch AND BD.PN=@PN AND B.DateTime>=@From AND B.DateTime<@To AND B.Status IS NULL AND NOT EXISTS (SELECT 1 FROM StockMovementReportOverride O WHERE O.BranchNumber=B.BranchNumber AND O.PN=BD.PN AND O.MovementType=N'BUY' AND O.Action=N'REPLACE' AND O.IsActive=1 AND O.SourceDocumentNumber=CONVERT(nvarchar(50),B.DocNumber)) GROUP BY B.DateTime,B.DocNumber,B.BillNumber,B.Status " &
            "UNION ALL SELECT S.DateTime,N'ขายออก',CONVERT(nvarchar(50),S.DocNumber),CONVERT(nvarchar(100),RTRIM(S.BillNumber)),CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),SUM(SS.Number),CAST(1 AS int) FROM SellStock SS INNER JOIN Sell S ON S.BranchNumber=SS.BranchNumber AND S.SellNumber=SS.SellNumber WHERE S.BranchNumber=@Branch AND SS.PN=@PN AND S.DateTime>=@From AND S.DateTime<@To AND S.Status IS NULL AND NOT EXISTS (SELECT 1 FROM StockMovementReportOverride O WHERE O.BranchNumber=S.BranchNumber AND O.PN=SS.PN AND O.MovementType=N'SELL' AND O.Action=N'REPLACE' AND O.IsActive=1 AND O.SourceDocumentNumber=CONVERT(nvarchar(50),S.DocNumber)) GROUP BY S.DateTime,S.DocNumber,S.BillNumber,S.Status " &
            "UNION ALL SELECT S.DateTime,N'ขายออก',CONVERT(nvarchar(50),S.DocNumber),CONVERT(nvarchar(100),RTRIM(S.BillNumber)),CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),SUM(SD.Number)-ISNULL(StockSale.StockNumber,0),CAST(1 AS int) FROM SellD SD INNER JOIN Sell S ON S.BranchNumber=SD.BranchNumber AND S.SellNumber=SD.SellNumber OUTER APPLY (SELECT SUM(SS2.Number) AS StockNumber FROM SellStock SS2 WHERE SS2.BranchNumber=S.BranchNumber AND SS2.SellNumber=S.SellNumber AND SS2.PN=SD.PN) StockSale WHERE S.BranchNumber=@Branch AND SD.PN=@PN AND S.DateTime>=@From AND S.DateTime<@To AND S.Status IS NULL AND NOT EXISTS (SELECT 1 FROM StockMovementReportOverride O WHERE O.BranchNumber=S.BranchNumber AND O.PN=SD.PN AND O.MovementType=N'SELL' AND O.Action=N'REPLACE' AND O.IsActive=1 AND O.SourceDocumentNumber=CONVERT(nvarchar(50),S.DocNumber)) GROUP BY S.DateTime,S.DocNumber,S.BillNumber,S.BranchNumber,S.SellNumber,StockSale.StockNumber HAVING SUM(SD.Number)-ISNULL(StockSale.StockNumber,0)>0 " &
            "UNION ALL SELECT SO.ReceiveTime,N'รับโอนเข้า',CONVERT(nvarchar(50),SO.ID),CONVERT(nvarchar(100),RTRIM(SO.BillNumber)),CAST(N'' AS nvarchar(500)),SUM(SOS.Number),CAST(0 AS decimal(18,4)),CAST(1 AS int) FROM SOutStock SOS INNER JOIN SOut SO ON SO.BranchNumber=SOS.BranchNumber AND SO.ID=SOS.ID WHERE SOS.PN=@PN AND SO.BranchTo=@Branch AND SO.Receive=1 AND SO.ReceiveTime>=@From AND SO.ReceiveTime<@To AND SO.Status IS NULL GROUP BY SO.ReceiveTime,SO.ID,SO.BillNumber,SO.Status " &
            "UNION ALL SELECT SO.DateTime,N'โอนออก',CONVERT(nvarchar(50),SO.ID),CONVERT(nvarchar(100),RTRIM(SO.BillNumber)),CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),SUM(SOS.Number),CAST(1 AS int) FROM SOutStock SOS INNER JOIN SOut SO ON SO.BranchNumber=SOS.BranchNumber AND SO.ID=SOS.ID WHERE SOS.PN=@PN AND SO.BranchNumber=@Branch AND SO.DateTime>=@From AND SO.DateTime<@To AND SO.Status IS NULL GROUP BY SO.DateTime,SO.ID,SO.BillNumber,SO.Status " &
            "UNION ALL SELECT SA.DateTime,CASE WHEN SAD.NumAdjust>=0 THEN N'ปรับสต๊อกเข้า' ELSE N'ปรับสต๊อกออก' END,CONVERT(nvarchar(50),SA.DocNumber),N'',CAST(N'' AS nvarchar(500)),CASE WHEN SAD.NumAdjust>0 THEN SAD.NumAdjust ELSE 0 END,CASE WHEN SAD.NumAdjust<0 THEN ABS(SAD.NumAdjust) ELSE 0 END,CAST(1 AS int) FROM StockAdjustD SAD INNER JOIN StockAdjust SA ON SA.BranchNumber=SAD.BranchNumber AND SA.ID=SAD.ID WHERE SAD.PN=@PN AND SA.BranchNumber=@Branch AND SA.DateTime>=@From AND SA.DateTime<@To AND SA.Status IS NULL " &
            "UNION ALL SELECT C.DateTime,N'คืนจากลูกค้า',CONVERT(nvarchar(50),C.DocNumber),CONVERT(nvarchar(100),RTRIM(C.BillNumber)),CAST(N'' AS nvarchar(500)),SUM(CS.Number),CAST(0 AS decimal(18,4)),CAST(1 AS int) FROM CNCustomerStock CS INNER JOIN CNCustomer C ON C.BranchNumber=CS.BranchNumber AND C.ID=CS.ID WHERE CS.PN=@PN AND C.BranchNumber=@Branch AND C.CNType=0 AND C.DateTime>=@From AND C.DateTime<@To AND C.Status IS NULL GROUP BY C.DateTime,C.DocNumber,C.BillNumber,C.Status " &
            "UNION ALL SELECT C.DateTime,N'คืนให้ผู้ขาย',CONVERT(nvarchar(50),C.DocNumber),CONVERT(nvarchar(100),RTRIM(C.BillNumber)),CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),SUM(CS.Number),CAST(1 AS int) FROM CNSupplierStock CS INNER JOIN CNSupplier C ON C.BranchNumber=CS.BranchNumber AND C.ID=CS.ID WHERE CS.PN=@PN AND C.BranchNumber=@Branch AND C.CNType=0 AND C.DateTime>=@From AND C.DateTime<@To AND C.Status IS NULL GROUP BY C.DateTime,C.DocNumber,C.BillNumber,C.Status " &
            "UNION ALL SELECT P.DocDate,N'ผลิตสินค้าสำเร็จ',CONVERT(nvarchar(50),P.DocNumber),N'',CAST(N'' AS nvarchar(500)),SUM(PS.Number),CAST(0 AS decimal(18,4)),CAST(1 AS int) FROM ProduceFinishProductStock PS INNER JOIN Produce P ON P.BranchNumber=PS.BranchNumber AND P.ID=PS.ID WHERE PS.PN=@PN AND P.BranchNumber=@Branch AND P.DocDate>=@From AND P.DocDate<@To AND P.Status IS NULL GROUP BY P.DocDate,P.DocNumber,P.Status " &
            "UNION ALL SELECT P.DocDate,N'ใช้วัตถุดิบผลิต',CONVERT(nvarchar(50),P.DocNumber),N'',CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),SUM(PS.Number),CAST(1 AS int) FROM ProduceRawMaterialStock PS INNER JOIN Produce P ON P.BranchNumber=PS.BranchNumber AND P.ID=PS.ID WHERE PS.PN=@PN AND P.BranchNumber=@Branch AND P.DocDate>=@From AND P.DocDate<@To AND P.Status IS NULL GROUP BY P.DocDate,P.DocNumber,P.Status " &
            "UNION ALL SELECT R.DateStart,N'ส่งซ่อม',CONVERT(nvarchar(50),R.ID),N'',CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),CAST(1 AS decimal(18,4)),CAST(1 AS int) FROM Repair R WHERE R.PN=@PN AND R.Branch=@Branch AND R.Type=1 AND R.DateStart>=@From AND R.DateStart<@To AND R.Status IS NULL " &
            "UNION ALL SELECT R.DateReturn,N'รับคืนจากซ่อม',CONVERT(nvarchar(50),R.ID),N'',CAST(N'' AS nvarchar(500)),CAST(1 AS decimal(18,4)),CAST(0 AS decimal(18,4)),CAST(1 AS int) FROM Repair R WHERE R.PNReturn=@PN AND R.Branch=@Branch AND R.Type=1 AND R.RepairStatus=4 AND R.DateReturn>=@From AND R.DateReturn<@To AND R.Status IS NULL " &
            "UNION ALL SELECT R.DateSwap,N'เปลี่ยนสินค้าออก',CONVERT(nvarchar(50),R.ID),N'',CAST(N'' AS nvarchar(500)),CAST(0 AS decimal(18,4)),CAST(1 AS decimal(18,4)),CAST(1 AS int) FROM Repair R WHERE R.PNSwap=@PN AND R.Branch=@Branch AND R.Type=0 AND R.RepairStatus>=3 AND R.DateSwap>=@From AND R.DateSwap<@To AND R.Status IS NULL " &
            "UNION ALL SELECT R.DateSwap,N'เปลี่ยนสินค้าเข้า',CONVERT(nvarchar(50),R.ID),N'',CAST(N'' AS nvarchar(500)),CAST(1 AS decimal(18,4)),CAST(0 AS decimal(18,4)),CAST(1 AS int) FROM Repair R WHERE R.PN=@PN AND R.Branch=@Branch AND R.Type=0 AND R.RepairStatus>=3 AND R.PNReceive=0 AND R.DateSwap>=@From AND R.DateSwap<@To AND R.Status IS NULL " &
            "UNION ALL SELECT O.AdjustmentDate,CASE WHEN O.MovementType=N'BUY' THEN N'ซื้อเข้า' ELSE N'ขายออก' END,O.ReportDocNumber,ISNULL(O.ReportBillNumber,N''),ISNULL(O.Detail,N''),O.NumIn,O.NumOut,CAST(1 AS int) FROM StockMovementReportOverride O WHERE O.BranchNumber=@Branch AND O.PN=@PN AND O.IsActive=1 AND O.AdjustmentDate>=@From AND O.AdjustmentDate<@To AND (O.NumIn<>0 OR O.NumOut<>0) " &
            ") X ORDER BY X.DocDate"

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sql, conn)
                'รายงานนี้รวมข้อมูลจากหลายตาราง และอาจค้นหาหลายสินค้าในครั้งเดียว
                cmd.CommandTimeout = 120
                cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = branchNo
                cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = productNo
                cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = dtpFrom.Value.Date
                'รายงานนี้เริ่มยอดที่ 0 ตามวิธีทำงานของร้าน จึงดึงเฉพาะรายการในช่วงวันที่เลือก
                cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = dtpTo.Value.Date.AddDays(1)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(rows)
                End Using
            End Using
        End Using

        'เครื่องดื่มไม่มียอดยกมา แต่สินค้าอื่นต้องเริ่มจากยอดคงเหลือก่อนวันเริ่มรายงาน
        'เพื่อให้รายการแรกของ LY4004 เป็นต้น แสดง 16 - จำนวนขาย ไม่ใช่ 0 - จำนวนขาย
        Dim remain As Decimal = If(IsBeverageProductForMovement(productNo),
                                   0D,
                                   GetOpeningBalance(branchNo, productNo, dtpFrom.Value.Date))
        Dim periodEnd As DateTime = dtpTo.Value.Date.AddDays(1)
        For Each source As DataRow In rows.Rows
            If Convert.ToDateTime(source("DocDate")) >= periodEnd Then Continue For
            'รายการยกเลิกถูกตัดออกตั้งแต่ SQL แล้ว ตามเงื่อนไข Status IS NULL
            If Convert.ToInt32(source("ShowRow")) <> 1 Then Continue For
            Dim row = movementTable.NewRow()
            row("DocDate") = source("DocDate")
            row("ProductCode") = productCode
            row("ProductName") = productName
            row("BillType") = source("BillType")
            row("DocNumber") = source("DocNumber")
            row("BillNumber") = If(source.IsNull("BillNumber"), "", source("BillNumber").ToString().Trim())
            row("Detail") = If(source.IsNull("Detail"), "", source("Detail").ToString().Trim())
            row("NumIn") = Convert.ToDecimal(source("NumIn"))
            row("NumOut") = Convert.ToDecimal(source("NumOut"))
            remain += Convert.ToDecimal(row("NumIn")) - Convert.ToDecimal(row("NumOut"))
            row("NumRemain") = remain
            movementTable.Rows.Add(row)
        Next
    End Sub

    Private Function IsBeverageProductForMovement(productNo As Long) As Boolean
        Using conn As SqlConnection = modVariable.OpenConnection()
            Const sql As String =
                "SELECT TOP 1 CASE WHEN ISNULL(PG.CategoryID, -1) = 5 " &
                "OR RTRIM(ISNULL(C.Name, N'')) = N'เครื่องดื่ม' THEN 1 ELSE 0 END " &
                "FROM Product P " &
                "LEFT JOIN ProductGroup PG ON PG.ID = P.GroupID " &
                "LEFT JOIN Category C ON C.ID = PG.CategoryID " &
                "WHERE P.PN = @PN"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = productNo
                Return Convert.ToInt32(cmd.ExecuteScalar()) = 1
            End Using
        End Using
    End Function

    Private Function GetOpeningBalance(branchNo As Integer, productNo As Long, openingDate As DateTime) As Decimal
        Dim result As Decimal = 0D
        Using conn As SqlConnection = modVariable.OpenConnection()
            Const sql As String =
                "SELECT " &
                "ISNULL((SELECT SUM(BD.Number) FROM Buy B " &
                "INNER JOIN BuyD BD ON BD.BranchNumber=B.BranchNumber AND BD.BuyNumber=B.BuyNumber " &
                "WHERE B.BranchNumber=@Branch AND BD.PN=@PN AND B.DateTime<@OpeningDate AND B.Status IS NULL),0) - " &
                "ISNULL((SELECT SUM(SD.Number) FROM Sell S " &
                "INNER JOIN SellD SD ON SD.BranchNumber=S.BranchNumber AND SD.SellNumber=S.SellNumber " &
                "WHERE S.BranchNumber=@Branch AND SD.PN=@PN AND S.DateTime<@OpeningDate AND S.Status IS NULL),0) + " &
                "ISNULL((SELECT SUM(SAD.NumAdjust) FROM StockAdjust SA " &
                "INNER JOIN StockAdjustD SAD ON SAD.BranchNumber=SA.BranchNumber AND SAD.ID=SA.ID " &
                "WHERE SA.BranchNumber=@Branch AND SAD.PN=@PN AND SA.DateTime<@OpeningDate AND SA.Status IS NULL),0)"
            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandTimeout = 120
                cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = branchNo
                cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = productNo
                cmd.Parameters.Add("@OpeningDate", SqlDbType.DateTime).Value = openingDate
                result = Convert.ToDecimal(cmd.ExecuteScalar())
            End Using
        End Using
        Return result
    End Function
    Private Sub Print_Click(sender As Object, e As EventArgs)
        If movementTable.Rows.Count = 0 Then
            MessageBox.Show("กรุณาค้นหารายงานก่อนพิมพ์", "รายงานความเคลื่อนไหว", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim branchName As String = "ไม่ระบุสาขา"
        If cboBranch.SelectedItem IsNot Nothing Then
            branchName = DirectCast(cboBranch.SelectedItem, BranchItem).Name
        End If

        Using viewer As New frmReportViewer()
            viewer.LoadStockMovementMonthlyReport(movementTable, dtpFrom.Value.Date, dtpTo.Value.Date, branchName)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)
        Using titleFont As New Font("Arial", 14, FontStyle.Bold), bodyFont As New Font("Arial", 9)
            Dim y As Integer = 40
            e.Graphics.DrawString("รายงานความเคลื่อนไหวของสินค้าและวัตถุดิบ", titleFont, Brushes.Black, 250, y)
            y += 35
            e.Graphics.DrawString("วันที่ " & dtpFrom.Value.ToString("dd/MM/yyyy") & " ถึง " & dtpTo.Value.ToString("dd/MM/yyyy"), bodyFont, Brushes.Black, 300, y)
            y += 35
            e.Graphics.DrawString("วันที่", bodyFont, Brushes.Black, 30, y)
            e.Graphics.DrawString("สินค้า", bodyFont, Brushes.Black, 120, y)
            e.Graphics.DrawString("ประเภทบิล", bodyFont, Brushes.Black, 300, y)
            e.Graphics.DrawString("เข้า", bodyFont, Brushes.Black, 480, y)
            e.Graphics.DrawString("ออก", bodyFont, Brushes.Black, 550, y)
            e.Graphics.DrawString("คงเหลือ", bodyFont, Brushes.Black, 630, y)
            y += 22
            While printRow < movementTable.Rows.Count AndAlso y < e.MarginBounds.Bottom - 25
                Dim row = movementTable.Rows(printRow)
                e.Graphics.DrawString(Convert.ToDateTime(row("DocDate")).ToString("dd/MM/yyyy"), bodyFont, Brushes.Black, 20, y)
                e.Graphics.DrawString(row("ProductCode").ToString(), bodyFont, Brushes.Black, 120, y)
                e.Graphics.DrawString(row("BillType").ToString(), bodyFont, Brushes.Black, 300, y)
                e.Graphics.DrawString(Convert.ToDecimal(row("NumIn")).ToString("N2"), bodyFont, Brushes.Black, 470, y)
                e.Graphics.DrawString(Convert.ToDecimal(row("NumOut")).ToString("N2"), bodyFont, Brushes.Black, 540, y)
                e.Graphics.DrawString(Convert.ToDecimal(row("NumRemain")).ToString("N2"), bodyFont, Brushes.Black, 620, y)
                y += 20
                printRow += 1
            End While
            e.HasMorePages = printRow < movementTable.Rows.Count
            If Not e.HasMorePages Then printRow = 0
        End Using
    End Sub

    Private NotInheritable Class BranchItem
        Public ReadOnly Number As Integer
        Public ReadOnly Name As String
        Public Sub New(numberValue As Integer, nameValue As String)
            Number = numberValue
            Name = nameValue
        End Sub
        Public Overrides Function ToString() As String
            Return Number.ToString() & " - " & Name
        End Function
    End Class


End Class
