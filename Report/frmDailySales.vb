Imports System.Drawing
Imports System.Security.Policy
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Coffee_SMH2.frmReceiptPreview

Public Class frmDailySales
    Inherits Form

    Private cardTotalSales As Panel
    Private cardBillCount As Panel
    Private cardCash As Panel
    Private cardTransfer As Panel
    Private cardDiscount As Panel
    Private cardFreeCup As Panel
    Private cardCustomerCertified As Panel
    Private cardVat As Panel
    Private cardCost As Panel
    Private cardProfit As Panel
    Private cardReturn As Panel
    Dim TotalSell As Decimal
    Dim countBill As Integer
    Dim beverageCupCount As Decimal
    Dim totalCash As Decimal
    Dim totalTran As Decimal
    Dim totalDiscount As Decimal
    Dim freeCupCount As Integer
    Dim freeCupAmount As Decimal
    Dim totalCustomerCertified As Decimal
    Private dailySalesDbOptimized As Boolean = False
    Private splitBillDetails As SplitContainer

    Private lblBillItemsSummary As Label
    Private isLoadingBills As Boolean = False
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        BuildUI()
        SetupBillItemPreview()
        BuildSummaryCards()
        'BuildBillGrid()
        '  LoadSampleData()
    End Sub
    Private Sub frmDailySales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FitMdiWorkspace()
        cbBranch.SelectedIndex = 0
        cbPayType.SelectedIndex = 0
        dtpDateFrom.CustomFormat = "dd/MM/yyyy"
        dtpDateTo.CustomFormat = "dd/MM/yyyy"
        'ตรวจสอบ/สร้างตารางที่จำเป็นทำไว้แล้วตอน Login ผ่าน DbSchemaChecker
        'ไม่ควรสร้าง Table/Index ระหว่างเปิดหน้ารายงาน เพราะอาจล็อกตารางและทำให้หน้าจอหมุนค้าง
        Call SumSellTotal()
        Call BuildSummaryCards()
        Call LoadSampleData()
        ConfigureHistoricalReceiptButton()
    End Sub

    Private Sub ConfigureHistoricalReceiptButton()
        'กำหนดซ้ำตอนรัน เพราะ Visual Studio Designer อาจจำตำแหน่งปุ่มเดิมไว้ด้านล่างจนมองไม่เห็น
        If btnPrintReceipt Is Nothing Then Return

        If btnPrintReceipt.Parent IsNot pnlTop Then
            pnlTop.Controls.Add(btnPrintReceipt)
        End If

        btnPrintReceipt.Visible = True
        btnPrintReceipt.Enabled = True
        btnPrintReceipt.Text = "พิมพ์ใบเสร็จย้อนหลัง"
        btnPrintReceipt.Location = New Point(13, 602)
        btnPrintReceipt.Size = New Size(297, 58)
        btnPrintReceipt.BringToFront()
        pnlTop.PerformLayout()
    End Sub
    Private Sub BuildUI()

        Dim leftTitle As New Label With {
            .Text = "รายการบิล",
            .Dock = DockStyle.Top,
            .Height = 30,
            .Font = New Font("Cordia New", 12.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(42, 60, 84)
        }

        pnlLeft.Controls.Add(leftTitle)



        Dim flpButtons As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .WrapContents = False
        }
        AddHandler Me.Resize, AddressOf frmDailySales_Resize


    End Sub
    Private Sub BuildSummaryCards()
        ' pnlSummary.Controls.Clear()
        lblTotalSales.Text = TotalSell.ToString("N2") & " ฿"
        Label7.Text = "จำนวนบิล " & countBill.ToString("N0") & " บิล"
        Label7.AutoSize = False
        Label7.Location = New Point(4, 3)
        Label7.Size = New Size(144, 32)
        Label7.TextAlign = ContentAlignment.MiddleCenter
        Label7.Font = New Font("Cordia New", 16.2!, FontStyle.Bold)
        lblBillCount.Text = "เครื่องดื่ม " & beverageCupCount.ToString("N0") & " แก้ว"
        lblBillCount.AutoSize = False
        lblBillCount.Location = New Point(4, 39)
        lblBillCount.Size = New Size(144, 38)
        lblBillCount.TextAlign = ContentAlignment.MiddleCenter
        lblBillCount.Font = New Font("Cordia New", 18.0!, FontStyle.Bold)
        lblCash.Text = totalCash.ToString("N2") & " ฿"
        lblTransfer.Text = totalTran.ToString("N2") & " ฿"
        lblDiscount.Text = totalDiscount.ToString("N2") & " ฿"
        lblFreeCup.Text = freeCupCount.ToString("N0") & " ใบ / " & freeCupAmount.ToString("N2") & " ฿"
        'การ์ดแก้วฟรีมีพื้นที่แคบกว่าการ์ดอื่น ให้จัดข้อความด้านล่างให้อยู่กึ่งกลางและไม่ถูกตัด
        lblFreeCup.AutoSize = False
        lblFreeCup.Dock = DockStyle.None
        lblFreeCup.Location = New Point(6, 44)
        lblFreeCup.Size = New Size(135, 34)
        lblFreeCup.Margin = New Padding(4, 0, 4, 0)
        lblFreeCup.TextAlign = ContentAlignment.MiddleCenter
        lblFreeCup.Font = New Font("Cordia New", 17.0!, FontStyle.Bold)
        Label11.BringToFront()
        lblFreeCup.BringToFront()
        lblCustomerCertified.Text = totalCustomerCertified.ToString("N2") & " ฿"

    End Sub

    Private Sub LoadSampleData()
        Try
            Dim dateFrom As String = dtpDateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            Dim dateTo As String = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            clsSQL = New clsSQLQuery
            With clsSQL
                Dim sWhere As New StringBuilder
                Dim sFrom As New StringBuilder
                sb = New StringBuilder
                sb.Append("ROW_NUMBER() OVER (ORDER BY SL.SellNumber ) As RowNo, ")
                sb.Append("SL.SellNumber, SL.BranchNumber, billnumber, SL.ctime, CS.name, ISNULL(PaidMoney, 0) AS PaidMoney, ISNULL(DCCash, 0) AS DiscountMoney, ")
                sb.Append("ISNULL(TotalPrice, 0) AS TotalPrice, ")
                sb.Append(DailySalesPayNameSql("SL"))
                sFrom.Append("Sell SL ")
                sFrom.Append("inner Join Customer CS on SL.CustomerNumber = CS.CustomerNumber ")
                sFrom.Append("LEFT JOIN SellCustomerCertified SCC ON SL.BranchNumber = SCC.BranchNumber AND SL.SellNumber = SCC.SellNumber ")
                sWhere.Append("SL.CTime >= '" & dateFrom & "' ")
                sWhere.Append("AND SL.CTime < '" & dateTo & "' ")
                sWhere.Append(DailySalesBranchFilterSql("SL"))
                sWhere.Append("AND SL.Status IS NULL ")
                sWhere.Append(DailySalesPayFilterSql("SL"))
                .Selecting(sb.ToString, sFrom.ToString, sWhere.ToString, "BillNumber")

                dgvBills.AutoGenerateColumns = False

                dgvBills.Columns("colNo").DataPropertyName = "RowNo"
                dgvBills.Columns("colBillNo").DataPropertyName = "BillNumber"
                dgvBills.Columns("colTime").DataPropertyName = "CTime"
                dgvBills.Columns("colTime").DefaultCellStyle.Format = "HH:mm"
                dgvBills.Columns("colCustomer").DataPropertyName = "Name"
                dgvBills.Columns("colTotal").DataPropertyName = "PaidMoney"
                dgvBills.Columns("colDiscount").DataPropertyName = "DiscountMoney"
                dgvBills.Columns("colNet").DataPropertyName = "TotalPrice"
                dgvBills.Columns("colPayType").DataPropertyName = "PayName"

                isLoadingBills = True
                dgvBills.DataSource = Nothing
                dgvBills.DataSource = .DTb
                isLoadingBills = False
                LoadSelectedBillItems()

            End With
            For Each row As DataGridViewRow In dgvBills.Rows
                row.Height = 25
            Next
        Catch ex As Exception
            isLoadingBills = False
            MessageBox.Show("โหลดรายการขายไม่สำเร็จ:" & Environment.NewLine & ex.Message &
                            Environment.NewLine & Environment.NewLine &
                            "กรุณาตรวจสอบวันที่ ฐานข้อมูล และตาราง Sell/SellCustomerCertified",
                            "Daily Sales Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Function DailySalesPayNameSql(sellAlias As String) As String
        'นอกจากรายการที่ถูกบันทึกใน SellCustomerCertified แล้ว รองรับบิลที่ใช้
        'ลูกค้าเริ่มต้นชื่อ "รับรองลูกค้า" ด้วย เพื่อให้ช่องทางและการ์ดยอดสรุปตรงกัน
        Return "Case WHEN SCC.SellNumber IS NOT NULL OR LTRIM(RTRIM(ISNULL(CS.Name, N''))) = N'รับรองลูกค้า' THEN N'รับรองลูกค้า' " &
               "WHEN " & sellAlias & ".PayType = 1 THEN N'เงินสด' " &
               "WHEN " & sellAlias & ".PayType = 4 THEN N'โอน 002203005517' " &
               "ELSE N'-' End As PayName "
    End Function

    Private Function DailySalesBranchFilterSql(sellAlias As String) As String
        If cbBranch.SelectedIndex > 0 Then
            Return "AND " & sellAlias & ".BranchNumber = 1 "
        End If

        Return String.Empty
    End Function

    Private Function DailySalesPayFilterSql(sellAlias As String) As String
        If cbPayType.SelectedIndex = 1 Then
            Return "AND " & sellAlias & ".PayType IN (1) "
        End If

        If cbPayType.SelectedIndex = 2 Then
            Return "AND " & sellAlias & ".PayType IN (4) "
        End If

        Return "AND " & sellAlias & ".PayType IN (1,4) "
    End Function

    Private Function DailySalesDiscountNoteSql(sellAlias As String) As String
        Return "ISNULL(N' {' + STUFF((SELECT N', ' + X.Note FROM (" &
               "SELECT CASE WHEN ISNULL(" & sellAlias & ".DCCash, 0) > 0 AND (ISNULL(FCD.FreeCupOut, 0) <= 0 OR ISNULL(" & sellAlias & ".DCCash, 0) > ISNULL(FCD.FreeCupAmount, 0) OR EXISTS (SELECT 1 FROM SellPromotion SPN WHERE SPN.BranchNumber = " & sellAlias & ".BranchNumber AND SPN.SellNumber = " & sellAlias & ".SellNumber AND NULLIF(LTRIM(RTRIM(SPN.Detail)), N'') IS NOT NULL AND ISNULL(SPN.Detail, N'') NOT LIKE N'%แก้วฟรี%' AND ISNULL(SPN.Detail, N'') NOT LIKE N'%รับรอง%')) THEN N'ส่วนลด' END AS Note " &
               "UNION ALL SELECT CASE WHEN ISNULL(FCD.FreeCupOut, 0) > 0 THEN N'แก้วฟรี' END " &
               ") X WHERE X.Note IS NOT NULL AND X.Note <> N'' FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, N'') + N'}', N'') AS DiscountNote "
    End Function
    Private Sub EditSelectedBill()
        If Not EnsureBillMaintenanceAllowed("Sell.Edit", "แก้ไขบิลขาย") Then Exit Sub

        If dgvBills.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกบิลที่ต้องการแก้ไข", "แก้ไขบิล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim drv As DataRowView = TryCast(dgvBills.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub

        Dim sellNumber As Integer = 0
        Dim branchNumber As Integer = 1
        Integer.TryParse(drv.Row("SellNumber").ToString(), sellNumber)
        Integer.TryParse(drv.Row("BranchNumber").ToString(), branchNumber)
        If sellNumber <= 0 Then Exit Sub

        Using f As New frmSellBillEdit(branchNumber, sellNumber)
            If f.ShowDialog(Me) = DialogResult.OK Then
                RefreshDailySalesView()
            End If
        End Using
    End Sub

    Private Sub CancelSelectedBill()
        If Not EnsureBillMaintenanceAllowed("Sell.Cancel", "ยกเลิกบิลขาย") Then Exit Sub

        If dgvBills.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกบิลที่ต้องการยกเลิก", "ยกเลิกบิล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim drv As DataRowView = TryCast(dgvBills.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub

        Dim sellNumber As Integer = 0
        Dim branchNumber As Integer = 1
        Integer.TryParse(drv.Row("SellNumber").ToString(), sellNumber)
        Integer.TryParse(drv.Row("BranchNumber").ToString(), branchNumber)
        Dim billNumber As String = drv.Row("BillNumber").ToString()

        If sellNumber <= 0 Then Exit Sub

        If MessageBox.Show("ต้องการยกเลิกบิล " & billNumber & " ใช่หรือไม่?" & Environment.NewLine &
                           "ระบบจะคืนสต๊อกและตัดบิลนี้ออกจากยอดขาย/รายงาน", "ยืนยันยกเลิกบิล",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Exit Sub
        End If

        Try
            VoidSellBill(branchNumber, sellNumber)
            MessageBox.Show("ยกเลิกบิลเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RefreshDailySalesView()
        Catch ex As Exception
            MessageBox.Show("ยกเลิกบิลไม่สำเร็จ: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub VoidSellBill(branchNumber As Integer, sellNumber As Integer)
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using tr As SqlTransaction = conn.BeginTransaction()
                Try
                    Using checkCmd As New SqlCommand("SELECT ISNULL(Status, 0) FROM Sell WITH (UPDLOCK, HOLDLOCK) WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber", conn, tr)
                        checkCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        checkCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                        Dim currentStatus = checkCmd.ExecuteScalar()

                        If currentStatus Is Nothing OrElse currentStatus Is DBNull.Value Then
                            Throw New Exception("ไม่พบบิลนี้")
                        End If
                        If Convert.ToInt32(currentStatus) <> 0 Then
                            Throw New Exception("บิลนี้ถูกยกเลิกไปแล้ว")
                        End If
                    End Using

                    RestoreStockFromSellStock(conn, tr, branchNumber, sellNumber)
                    'สินค้าเครื่องดื่มจะบันทึกยอดติดลบไว้ใน Stock โดยตรง (BillType=5)
                    'จึงต้องลบยอดติดลบของบิลนี้ตอนยกเลิกด้วย ไม่เช่นนั้นสต็อกจะค้าง -1
                    RemoveNegativeStockForVoidedBill(conn, tr, branchNumber, sellNumber)

                    Using updateCmd As New SqlCommand("UPDATE Sell SET Status = 1, UTime = GETDATE() WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber", conn, tr)
                        updateCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        updateCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                        updateCmd.ExecuteNonQuery()
                    End Using

                    'ยกเลิกบิลแล้วต้องคืนสิทธิ์แก้วฟรีของบิลนั้นด้วย
                    'เพื่อไม่ให้ยอด/จำนวนสิทธิ์ค้าง และสามารถขายบิลใหม่ได้ถูกต้อง
                    Using deleteFreeCupCmd As New SqlCommand("DELETE FROM CustomerFreeCup WHERE SellBranchNumber = @BranchNumber AND SellNumber = @SellNumber", conn, tr)
                        deleteFreeCupCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        deleteFreeCupCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                        deleteFreeCupCmd.ExecuteNonQuery()
                    End Using

                    Using deletePromotionCmd As New SqlCommand("DELETE FROM SellPromotion WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber", conn, tr)
                        deletePromotionCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        deletePromotionCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                        deletePromotionCmd.ExecuteNonQuery()
                    End Using

                    tr.Commit()
                Catch
                    Try : tr.Rollback() : Catch : End Try
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Sub RestoreStockFromSellStock(conn As SqlConnection, tr As SqlTransaction, branchNumber As Integer, sellNumber As Integer)
        Dim dt As New DataTable()
        Using cmd As New SqlCommand("SELECT BranchNumber, BillType, BillNumber, PN, Number, BuyPrice, BuyPercentVat, CanUseTime FROM SellStock WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            Using da As New SqlDataAdapter(cmd)
                da.SelectCommand.Transaction = tr
                da.Fill(dt)
            End Using
        End Using

        For Each row As DataRow In dt.Rows
            Dim stockBranch As Integer = Convert.ToInt32(row("BranchNumber"))
            Dim billType As Integer = Convert.ToInt32(row("BillType"))
            Dim stockBillNumber As Integer = Convert.ToInt32(row("BillNumber"))
            Dim pn As Integer = Convert.ToInt32(row("PN"))
            Dim qty As Decimal = Convert.ToDecimal(row("Number"))
            Dim buyPrice As Decimal = Convert.ToDecimal(row("BuyPrice"))
            Dim buyVat As Decimal = Convert.ToDecimal(row("BuyPercentVat"))
            Dim canUseTime As DateTime = Convert.ToDateTime(row("CanUseTime"))

            Using updateStock As New SqlCommand("UPDATE Stock SET NumberNS = ISNULL(NumberNS, 0) + @Qty, Number = ISNULL(Number, 0) + @Qty WHERE BranchNumber = @BranchNumber AND BillType = @BillType AND BillNumber = @BillNumber AND PN = @PN", conn, tr)
                updateStock.Parameters.Add("@Qty", SqlDbType.Decimal).Value = qty
                updateStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = stockBranch
                updateStock.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                updateStock.Parameters.Add("@BillNumber", SqlDbType.Int).Value = stockBillNumber
                updateStock.Parameters.Add("@PN", SqlDbType.Int).Value = pn

                If updateStock.ExecuteNonQuery() = 0 Then
                    Using insertStock As New SqlCommand("INSERT INTO Stock (BranchNumber, BillType, BillNumber, PN, NumberNS, NumberS, Number, BuyPrice, PercentVat, CanUseTime) VALUES (@BranchNumber, @BillType, @BillNumber, @PN, @Qty, 0, @Qty, @BuyPrice, @BuyVat, @CanUseTime)", conn, tr)
                        insertStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = stockBranch
                        insertStock.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                        insertStock.Parameters.Add("@BillNumber", SqlDbType.Int).Value = stockBillNumber
                        insertStock.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                        insertStock.Parameters.Add("@Qty", SqlDbType.Decimal).Value = qty
                        insertStock.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = buyPrice
                        insertStock.Parameters.Add("@BuyVat", SqlDbType.Decimal).Value = buyVat
                        insertStock.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = canUseTime
                        insertStock.ExecuteNonQuery()
                    End Using
                End If
            End Using
        Next
    End Sub

    Private Sub RemoveNegativeStockForVoidedBill(conn As SqlConnection, tr As SqlTransaction, branchNumber As Integer, sellNumber As Integer)
        Using cmd As New SqlCommand("DELETE FROM Stock WHERE BranchNumber = @BranchNumber AND BillType = 5 AND BillNumber = @SellNumber AND Number < 0", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub RefreshDailySalesView()
        SumSellTotal()
        BuildSummaryCards()
        LoadSampleData()
        ApplyBillDetailsSplitter()
    End Sub
    Private Sub SetupBillItemPreview()
        If splitBillDetails IsNot Nothing Then Exit Sub

        pnlLeft.SuspendLayout()
        pnlLeft.Controls.Clear()

        splitBillDetails = New SplitContainer With {
            .Dock = DockStyle.Fill,
            .Orientation = Orientation.Horizontal,
            .SplitterWidth = 6,
            .Panel1MinSize = 25,
            .Panel2MinSize = 25
        }

        Dim pnlBills As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.White}
        Dim lblBillsTitle As New Label With {
            .Text = "รายการบิล",
            .Dock = DockStyle.Top,
            .Height = 30,
            .Font = New Font("Cordia New", 14.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(42, 60, 84)
        }

        dgvBills.Dock = DockStyle.Fill
        dgvBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBills.MultiSelect = False
        ApplyComfortGridStyle(dgvBills)
        pnlBills.Controls.Add(dgvBills)
        pnlBills.Controls.Add(lblBillsTitle)

        Dim pnlItems As New Panel With {.Dock = DockStyle.Fill, .BackColor = Color.White}
        Dim lblItemsTitle As New Label With {
            .Text = "รายการสินค้าในบิล",
            .Dock = DockStyle.Top,
            .Height = 30,
            .Font = New Font("Cordia New", 14.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(42, 60, 84)
        }

        lblBillItemsSummary = New Label With {
            .Text = "เลือกบิลเพื่อดูรายการสินค้า",
            .Dock = DockStyle.Bottom,
            .Height = 30,
            .Font = New Font("Cordia New", 14.0!, FontStyle.Bold),
            .ForeColor = Color.FromArgb(42, 60, 84),
            .TextAlign = ContentAlignment.MiddleRight
        }

        dgvBillItems = New DataGridView With {
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .AutoGenerateColumns = False,
            .BackgroundColor = Color.White,
            .Dock = DockStyle.Fill,
            .EnableHeadersVisualStyles = False,
            .ReadOnly = True,
            .RowHeadersWidth = 40,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .MultiSelect = False
        }
        ApplyComfortGridStyle(dgvBillItems)
        dgvBillItems.ColumnHeadersHeight = 36
        AddBillItemColumns()

        pnlItems.Controls.Add(dgvBillItems)
        pnlItems.Controls.Add(lblBillItemsSummary)
        pnlItems.Controls.Add(lblItemsTitle)

        splitBillDetails.Panel1.Controls.Add(pnlBills)
        splitBillDetails.Panel2.Controls.Add(pnlItems)
        pnlLeft.Controls.Add(splitBillDetails)
        pnlLeft.ResumeLayout()

        AddHandler dgvBills.SelectionChanged, AddressOf dgvBills_SelectionChanged
        AddHandler dgvBills.CellClick, AddressOf dgvBills_CellClick
        AddHandler dgvBillItems.SelectionChanged, AddressOf dgvBillItems_SelectionChanged
        AddHandler dgvBillItems.CellClick, AddressOf dgvBillItems_CellClick
        SetBillActionButtons(False)
    End Sub

    Private Sub SetBillActionButtons(enabled As Boolean)
        Dim canEditBills As Boolean = PermissionService.HasPermission("Sell.Edit")
        Dim canCancelBills As Boolean = PermissionService.HasPermission("Sell.Cancel")
        btnCancelBill.Visible = True
        btnEditBill.Visible = True
        btnCancelBill.Enabled = enabled AndAlso canCancelBills
        btnEditBill.Enabled = enabled AndAlso canEditBills

        If canCancelBills Then
            btnCancelBill.BackColor = Color.FromArgb(148, 82, 78)
            btnCancelBill.ForeColor = Color.Black
        Else
            btnCancelBill.BackColor = Color.FromArgb(220, 220, 220)
            btnCancelBill.ForeColor = Color.FromArgb(140, 140, 140)
        End If

        If canEditBills Then
            btnEditBill.BackColor = Color.Gold
            btnEditBill.ForeColor = Color.Black
        Else
            btnEditBill.BackColor = Color.FromArgb(220, 220, 220)
            btnEditBill.ForeColor = Color.FromArgb(140, 140, 140)
        End If
        btnCancelBill.Location = New Point(13, 535)
        btnEditBill.Location = New Point(190, 535)
        btnCancelBill.BringToFront()
        btnEditBill.BringToFront()
    End Sub

    Private Function EnsureBillMaintenanceAllowed(permissionKey As String, actionName As String) As Boolean
        If PermissionService.HasPermission(permissionKey) Then Return True

        MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์" & actionName, "สิทธิ์ไม่เพียงพอ",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Return False
    End Function


    Private Sub ApplyComfortGridStyle(grid As DataGridView)
        Dim headerColor As Color = Color.FromArgb(91, 120, 137)
        Dim selectedColor As Color = Color.FromArgb(96, 139, 151)
        Dim selectedTextColor As Color = Color.White
        Dim textColor As Color = Color.FromArgb(35, 45, 52)
        Dim gridLineColor As Color = Color.FromArgb(218, 226, 229)

        grid.BackgroundColor = Color.FromArgb(250, 252, 252)
        grid.BorderStyle = BorderStyle.FixedSingle
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        grid.GridColor = gridLineColor
        grid.EnableHeadersVisualStyles = False
        grid.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 248)
        grid.RowHeadersDefaultCellStyle.SelectionBackColor = selectedColor
        grid.RowHeadersDefaultCellStyle.SelectionForeColor = selectedTextColor

        grid.ColumnHeadersDefaultCellStyle.BackColor = headerColor
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerColor
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)

        grid.DefaultCellStyle.BackColor = Color.White
        grid.DefaultCellStyle.ForeColor = textColor
        grid.DefaultCellStyle.SelectionBackColor = selectedColor
        grid.DefaultCellStyle.SelectionForeColor = selectedTextColor
        grid.DefaultCellStyle.Font = New Font("Cordia New", 14.0!, FontStyle.Regular)

        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 248, 247)
        grid.AlternatingRowsDefaultCellStyle.ForeColor = textColor
        grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = selectedColor
        grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = selectedTextColor
    End Sub
    Private Sub AddBillItemColumns()
        dgvBillItems.Columns.Clear()
        dgvBillItems.Columns.Add(CreateTextColumn("colItemNo", "ลำดับ", "Item", 70, DataGridViewContentAlignment.MiddleCenter, ""))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemCode", "รหัสสินค้า", "ProductCode", 120, DataGridViewContentAlignment.MiddleLeft, ""))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemBarcode", "Barcode", "Barcode", 150, DataGridViewContentAlignment.MiddleLeft, ""))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemName", "ชื่อสินค้า", "ProductName", 280, DataGridViewContentAlignment.MiddleLeft, ""))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemQty", "จำนวน", "Qty", 90, DataGridViewContentAlignment.MiddleRight, "N2"))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemUnit", "หน่วย", "SUnit", 80, DataGridViewContentAlignment.MiddleCenter, ""))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemPrice", "ราคา", "Price", 100, DataGridViewContentAlignment.MiddleRight, "N2"))
        dgvBillItems.Columns.Add(CreateTextColumn("colItemTotal", "รวมเงิน", "LineTotal", 110, DataGridViewContentAlignment.MiddleRight, "N2"))
    End Sub

    Private Function CreateTextColumn(name As String, headerText As String, propertyName As String, width As Integer, alignment As DataGridViewContentAlignment, format As String) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn With {
            .Name = name,
            .HeaderText = headerText,
            .DataPropertyName = propertyName,
            .ReadOnly = True,
            .Width = width
        }
        col.DefaultCellStyle.Alignment = alignment
        If format <> "" Then col.DefaultCellStyle.Format = format
        Return col
    End Function

    Private Sub dgvBills_SelectionChanged(sender As Object, e As EventArgs)
        If isLoadingBills Then Exit Sub
        LoadSelectedBillItems()
    End Sub

    Private Sub dgvBills_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then LoadSelectedBillItems()
    End Sub

    Private Sub dgvBillItems_SelectionChanged(sender As Object, e As EventArgs)
        Dim hasSelectedItem As Boolean = dgvBillItems IsNot Nothing AndAlso
                                         dgvBillItems.CurrentRow IsNot Nothing AndAlso
                                         Not dgvBillItems.CurrentRow.IsNewRow
        SetBillActionButtons(hasSelectedItem AndAlso dgvBills IsNot Nothing AndAlso dgvBills.CurrentRow IsNot Nothing)
    End Sub

    Private Sub dgvBillItems_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            SetBillActionButtons(True)
        End If
    End Sub

    Private Sub LoadSelectedBillItems()
        If dgvBillItems Is Nothing Then Exit Sub

        dgvBillItems.DataSource = Nothing
        lblBillItemsSummary.Text = "เลือกบิลเพื่อดูรายการสินค้า"

        If dgvBills.CurrentRow Is Nothing Then Exit Sub

        Dim drv As DataRowView = TryCast(dgvBills.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub

        Dim sellNumber As Integer = 0
        Dim branchNumber As Integer = 1
        Integer.TryParse(drv.Row("SellNumber").ToString(), sellNumber)
        Integer.TryParse(drv.Row("BranchNumber").ToString(), branchNumber)
        If sellNumber <= 0 Then Exit Sub

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT SD.Item, RTRIM(PD.ProductCode) AS ProductCode, RTRIM(PD.Barcode) AS Barcode, " &
                    "RTRIM(PD.Name) AS ProductName, ISNULL(SD.Number, 0) AS Qty, RTRIM(PD.SUnit) AS SUnit, " &
                    "CASE WHEN ISNULL(SD.Price, 0) <> 0 THEN SD.Price ELSE ISNULL(SD.PriceBD, 0) END AS Price, " &
                    "ISNULL(SD.SumPrice, 0) AS LineTotal " &
                    "FROM SellD SD " &
                    "INNER JOIN Product PD ON SD.PN = PD.PN " &
                    "INNER JOIN Sell SL ON SD.BranchNumber = SL.BranchNumber AND SD.SellNumber = SL.SellNumber " &
                    "WHERE SD.SellNumber = @SellNumber AND SD.BranchNumber = @BranchNumber " &
                    "ORDER BY SD.Item"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber

                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        dgvBillItems.DataSource = dt
                        UpdateBillItemsSummary(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            lblBillItemsSummary.Text = "โหลดรายการสินค้าไม่สำเร็จ"
        End Try
    End Sub

    Private Sub UpdateBillItemsSummary(dt As DataTable)
        Dim qtyTotal As Decimal = 0D
        Dim amountTotal As Decimal = 0D

        For Each row As DataRow In dt.Rows
            Dim qty As Decimal = 0D
            Dim amount As Decimal = 0D
            Decimal.TryParse(row("Qty").ToString(), qty)
            Decimal.TryParse(row("LineTotal").ToString(), amount)
            qtyTotal += qty
            amountTotal += amount
        Next

        lblBillItemsSummary.Text = "รวม " & dt.Rows.Count.ToString("N0") & " รายการ | จำนวน " & qtyTotal.ToString("N2") & " | ยอดสินค้า " & amountTotal.ToString("N2") & " บาท"
    End Sub


    Private Sub ApplyBillDetailsSplitter()
        If splitBillDetails Is Nothing Then Exit Sub
        If splitBillDetails.Height <= 80 Then Exit Sub

        Dim minPanelSize As Integer = Math.Min(170, Math.Max(25, (splitBillDetails.Height - splitBillDetails.SplitterWidth) \ 3))
        splitBillDetails.Panel1MinSize = minPanelSize
        splitBillDetails.Panel2MinSize = minPanelSize

        Dim minDistance As Integer = splitBillDetails.Panel1MinSize
        Dim maxDistance As Integer = splitBillDetails.Height - splitBillDetails.Panel2MinSize - splitBillDetails.SplitterWidth
        If maxDistance < minDistance Then Exit Sub

        'ให้ตารางรายละเอียดด้านล่างมีขนาดกะทัดรัดกว่าตารางรายการบิลด้านบน
        Dim preferredDistance As Integer = CInt(splitBillDetails.Height * 0.65)
        splitBillDetails.SplitterDistance = Math.Max(minDistance, Math.Min(preferredDistance, maxDistance))
    End Sub
    Private Sub frmDailySales_Resize(sender As Object, e As EventArgs)
        FitMdiWorkspace()
        ApplyBillDetailsSplitter()
    End Sub

    Private Sub FitMdiWorkspace()
        If Me.IsDisposed OrElse Me.Disposing OrElse Not Me.IsHandleCreated Then Return
        If Me.MdiParent Is Nothing OrElse Me.MdiParent.IsDisposed Then Return

        If Me.Dock <> DockStyle.None Then
            Me.Dock = DockStyle.None
        End If
        If Me.WindowState <> FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub



    Private Sub EnsureDailySalesDatabaseOptimized()
        If dailySalesDbOptimized Then Exit Sub

        Try
            EnsureSellCustomerCertifiedTable()
            EnsureSellPromotionTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Sell_DailySales' AND object_id = OBJECT_ID('Sell')) " &
                    "BEGIN CREATE INDEX IX_Sell_DailySales ON Sell (BranchNumber, CTime, PayType) " &
                    "INCLUDE (SellNumber, BillNumber, CustomerNumber, PaidMoney, DCCash, TotalPrice, Cash) END; " &
                    "IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Customer_CustomerNumber' AND object_id = OBJECT_ID('Customer')) " &
                    "BEGIN CREATE INDEX IX_Customer_CustomerNumber ON Customer (CustomerNumber) INCLUDE (Name) END; " &
                    "IF OBJECT_ID('CustomerFreeCup', 'U') IS NOT NULL " &
                    "AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_CustomerFreeCup_Sell' AND object_id = OBJECT_ID('CustomerFreeCup')) " &
                    "BEGIN CREATE INDEX IX_CustomerFreeCup_Sell ON CustomerFreeCup (SellBranchNumber, SellNumber) INCLUDE (FreeCupOut) END; " &
                    "IF OBJECT_ID('SellCustomerCertified', 'U') IS NOT NULL " &
                    "AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SellCustomerCertified_Sell' AND object_id = OBJECT_ID('SellCustomerCertified')) " &
                    "BEGIN CREATE INDEX IX_SellCustomerCertified_Sell ON SellCustomerCertified (BranchNumber, SellNumber) END; " &
                    "IF OBJECT_ID('SellPromotion', 'U') IS NOT NULL " &
                    "AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SellPromotion_Sell' AND object_id = OBJECT_ID('SellPromotion')) " &
                    "BEGIN CREATE INDEX IX_SellPromotion_Sell ON SellPromotion (BranchNumber, SellNumber) INCLUDE (Detail) END; "

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 120
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            dailySalesDbOptimized = True
        Catch ex As Exception
            dailySalesDbOptimized = True
        End Try
    End Sub

    Private Sub EnsureSellPromotionTable()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID('SellPromotion', 'U') IS NULL " &
                    "BEGIN " &
                    "CREATE TABLE SellPromotion (" &
                    "BranchNumber INT NOT NULL, " &
                    "SellNumber INT NOT NULL, " &
                    "Detail NVARCHAR(500) NULL, " &
                    "CTime DATETIME NOT NULL DEFAULT GETDATE(), " &
                    "CONSTRAINT PK_SellPromotion PRIMARY KEY (BranchNumber, SellNumber)" &
                    ") END " &
                    "ELSE IF COL_LENGTH('SellPromotion', 'Detail') IS NULL " &
                    "BEGIN ALTER TABLE SellPromotion ADD Detail NVARCHAR(500) NULL END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub
    Private Sub EnsureSellCustomerCertifiedTable()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID('SellCustomerCertified', 'U') IS NULL " &
                    "BEGIN " &
                    "CREATE TABLE SellCustomerCertified (" &
                    "BranchNumber INT NOT NULL, " &
                    "SellNumber INT NOT NULL, " &
                    "CustomerNumber INT NULL, " &
                    "CustomerName NVARCHAR(200) NULL, " &
                    "PayName NVARCHAR(100) NULL, " &
                    "Detail NVARCHAR(500) NULL, " &
                    "CONumber INT NULL, " &
                    "CTime DATETIME NOT NULL DEFAULT GETDATE(), " &
                    "CONSTRAINT PK_SellCustomerCertified PRIMARY KEY (BranchNumber, SellNumber)" &
                    ") END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SumSellTotal()
        Try
            Dim dateFrom As String = dtpDateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            Dim dateTo As String = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)

            clsSQL = New clsSQLQuery
            With clsSQL
                Dim sWhere As New StringBuilder
                Dim sFrom As New StringBuilder
                sb = New StringBuilder
                'รวมมูลค่าแก้วฟรีในยอดขายด้วย แต่ป้องกันการบวกซ้ำสำหรับบิลใหม่ที่คิดเงินแก้วฟรีอยู่แล้ว
                sb.Append("ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL THEN CASE WHEN SL.TotalPrice < ISNULL(FC.FreeCupAmount, 0) THEN SL.TotalPrice + ISNULL(FC.FreeCupAmount, 0) ELSE SL.TotalPrice END ELSE 0 END), 0) as Paid, ")
                sb.Append("COUNT(CASE WHEN SCC.SellNumber IS NULL THEN 1 END) as Sellnumber, ")
                sb.Append("ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL AND SL.PayType = 1 THEN CASE WHEN SL.Cash < ISNULL(FC.FreeCupAmount, 0) THEN SL.Cash + ISNULL(FC.FreeCupAmount, 0) ELSE SL.Cash END ELSE 0 END), 0) As Cash, ")
                sb.Append("ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL AND SL.PayType = 4 THEN CASE WHEN SL.TotalPrice < ISNULL(FC.FreeCupAmount, 0) THEN SL.TotalPrice + ISNULL(FC.FreeCupAmount, 0) ELSE SL.TotalPrice END ELSE 0 END), 0) As Transfer, ")
                sb.Append("ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL THEN SL.DCCash ELSE 0 END), 0) As Discount, ")
                'ยอดรับรองลูกค้าใช้ทั้งตัวบันทึก SellCustomerCertified และชื่อลูกค้า
                '"รับรองลูกค้า" เพื่อรองรับบิลเก่าที่ไม่ได้มีแถวในตาราง marker นี้
                sb.Append("ISNULL(SUM(CASE WHEN SCC.SellNumber IS NOT NULL OR LTRIM(RTRIM(ISNULL(CS.Name, N''))) = N'รับรองลูกค้า' THEN ISNULL(SL.TotalPrice, 0) ELSE 0 END), 0) As CustomerCertified ")
                sFrom.Append("Sell SL ")
                sFrom.Append("LEFT JOIN SellCustomerCertified SCC ON SL.BranchNumber = SCC.BranchNumber AND SL.SellNumber = SCC.SellNumber ")
                sFrom.Append("LEFT JOIN Customer CS ON SL.CustomerNumber = CS.CustomerNumber ")
                sFrom.Append("LEFT JOIN (SELECT CFC.SellBranchNumber AS BranchNumber, CFC.SellNumber, SUM(CFC.FreeCupOut * ISNULL(BV.FreeCupPrice, 0)) AS FreeCupAmount FROM CustomerFreeCup CFC OUTER APPLY (SELECT MAX(ISNULL(SD.Price, 0)) AS FreeCupPrice FROM SellD SD INNER JOIN Product PD ON SD.PN = PD.PN LEFT JOIN ProductGroup PG ON PG.ID = PD.GroupID LEFT JOIN Category CT ON CT.ID = PG.CategoryID WHERE SD.BranchNumber = CFC.SellBranchNumber AND SD.SellNumber = CFC.SellNumber AND (ISNULL(CT.ID, -1) = 5 OR ISNULL(CT.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PD.SUnit, '') LIKE N'%แก้ว%' OR ISNULL(PD.Name, '') LIKE N'%กาแฟ%' OR ISNULL(PD.Name, '') LIKE N'%ชา%' OR ISNULL(PD.Name, '') LIKE N'%โกโก้%' OR ISNULL(PD.Name, '') LIKE N'%นม%' OR ISNULL(PD.Name, '') LIKE N'%โซดา%' OR ISNULL(PD.Name, '') LIKE N'%สมูทตี้%' OR ISNULL(PD.Name, '') LIKE N'%อิตาเลี่ยน%')) BV WHERE CFC.FreeCupOut > 0 GROUP BY CFC.SellBranchNumber, CFC.SellNumber) FC ON SL.BranchNumber = FC.BranchNumber AND SL.SellNumber = FC.SellNumber ")
                sWhere.Append("SL.CTime >= '" & dateFrom & "' ")
                sWhere.Append("AND SL.CTime < '" & dateTo & "' ")
                sWhere.Append(DailySalesBranchFilterSql("SL"))
                sWhere.Append("AND SL.Status IS NULL ")
                sWhere.Append(DailySalesPayFilterSql("SL"))


                .Selecting(sb.ToString, sFrom.ToString, sWhere.ToString, "Paid")
                TotalSell = CDec(.DTb.Rows(0)("Paid"))
                countBill = CInt(.DTb.Rows(0)("Sellnumber"))
                totalCash = CDec(.DTb.Rows(0)("Cash"))
                totalTran = CDec(.DTb.Rows(0)("Transfer"))
                totalDiscount = CDec(.DTb.Rows(0)("Discount"))
                totalCustomerCertified = CDec(.DTb.Rows(0)("CustomerCertified"))
            End With

            LoadFreeCupSummary(dateFrom, dateTo)
            LoadBeverageSummary(dateFrom, dateTo)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadBeverageSummary(dateFrom As String, dateTo As String)
        beverageCupCount = 0D

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT " &
                    "ISNULL(SUM(CASE WHEN " &
                    "(ISNULL(CT.ID, -1) = 5 OR ISNULL(CT.Name, N'') LIKE N'%เครื่องดื่ม%' " &
                    "OR ISNULL(PG.Name, N'') LIKE N'%เครื่องดื่ม%' " &
                    "OR ISNULL(PD.SUnit, N'') LIKE N'%แก้ว%') " &
                    "AND ISNULL(PD.SUnit, N'') LIKE N'%แก้ว%' " &
                    "AND ISNULL(PD.Name, N'') NOT LIKE N'%ช็อต%' " &
                    "AND ISNULL(PD.SUnit, N'') NOT LIKE N'%ช็อต%' " &
                    "AND ((ISNULL(PG.Name, N'') NOT LIKE N'อื่น%' " &
                    "AND ISNULL(CT.Name, N'') NOT LIKE N'อื่น%') " &
                    "OR ISNULL(PD.Name, N'') LIKE N'%น้ำแข็งเปล่า%') " &
                    "THEN ISNULL(SD.Number, 0) ELSE 0 END), 0) AS BeverageCupCount " &
                    "FROM Sell SL " &
                    "INNER JOIN SellD SD ON SD.BranchNumber = SL.BranchNumber " &
                    "AND SD.SellNumber = SL.SellNumber " &
                    "INNER JOIN Product PD ON SD.PN = PD.PN " &
                    "LEFT JOIN ProductGroup PG ON PG.ID = PD.GroupID " &
                    "LEFT JOIN Category CT ON CT.ID = PG.CategoryID " &
                    "WHERE SL.CTime >= @DateFrom " &
                    "AND SL.CTime < @DateTo " &
                    DailySalesBranchFilterSql("SL") &
                    "AND SL.Status IS NULL " &
                    DailySalesPayFilterSql("SL")

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value =
                        DateTime.ParseExact(dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value =
                        DateTime.ParseExact(dateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            beverageCupCount = Convert.ToDecimal(reader("BeverageCupCount"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            beverageCupCount = 0D
        End Try
    End Sub

    Private Sub EnsureCustomerFreeCupTable()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID('CustomerFreeCup', 'U') IS NULL " &
                    "BEGIN " &
                    "CREATE TABLE CustomerFreeCup (" &
                    "CustomerNumber INT NOT NULL, " &
                    "ID INT NOT NULL, " &
                    "SellNumber INT NULL, " &
                    "SellBranchNumber INT NULL, " &
                    "CupIn DECIMAL(18,2) NOT NULL DEFAULT 0, " &
                    "FreeCupIn INT NOT NULL DEFAULT 0, " &
                    "FreeCupOut INT NOT NULL DEFAULT 0, " &
                    "FreeCupAmount DECIMAL(18,2) NOT NULL DEFAULT 0, " &
                    "CupBalance DECIMAL(18,2) NOT NULL DEFAULT 0, " &
                    "FreeCupBalance INT NOT NULL DEFAULT 0, " &
                    "Detail NVARCHAR(500) NULL, " &
                    "CTime DATETIME NOT NULL DEFAULT GETDATE(), " &
                    "CONSTRAINT PK_CustomerFreeCup PRIMARY KEY (CustomerNumber, ID)" &
                    ") END; " &
                    "IF COL_LENGTH('CustomerFreeCup', 'FreeCupAmount') IS NULL " &
                    "ALTER TABLE CustomerFreeCup ADD FreeCupAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_CustomerFreeCup_FreeCupAmount DEFAULT 0"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadFreeCupSummary(dateFrom As String, dateTo As String)
        freeCupCount = 0
        freeCupAmount = 0D

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim payFilter As String = ""
                payFilter = " " & DailySalesPayFilterSql("SL")

                Dim usedSql As String =
                    "SELECT ISNULL(SUM(CFC.FreeCupOut), 0) AS FreeCupCount, " &
                    "ISNULL(SUM(CASE WHEN ISNULL(CFC.FreeCupAmount, 0) > 0 THEN CFC.FreeCupAmount ELSE CFC.FreeCupOut * ISNULL(BV.FreeCupPrice, 0) END), 0) AS FreeCupAmount " &
                    "FROM CustomerFreeCup CFC " &
                    "INNER JOIN Sell SL ON CFC.SellNumber = SL.SellNumber " &
                    "AND (CFC.SellBranchNumber = SL.BranchNumber OR ISNULL(CFC.SellBranchNumber, 0) = 0) " &
                    "OUTER APPLY (SELECT MAX(ISNULL(SD.Price, 0)) AS FreeCupPrice FROM SellD SD " &
                    "INNER JOIN Product PD ON SD.PN = PD.PN " &
                    "LEFT JOIN ProductGroup PG ON PG.ID = PD.GroupID " &
                    "LEFT JOIN Category CT ON CT.ID = PG.CategoryID " &
                    "WHERE SD.BranchNumber = CFC.SellBranchNumber AND SD.SellNumber = CFC.SellNumber " &
                    "AND (ISNULL(CT.ID, -1) = 5 OR ISNULL(CT.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG.Name, '') LIKE N'%เครื่องดื่ม%' " &
                    "OR ISNULL(PD.SUnit, '') LIKE N'%แก้ว%' OR ISNULL(PD.Name, '') LIKE N'%กาแฟ%' OR ISNULL(PD.Name, '') LIKE N'%ชา%' " &
                    "OR ISNULL(PD.Name, '') LIKE N'%โกโก้%' OR ISNULL(PD.Name, '') LIKE N'%นม%' OR ISNULL(PD.Name, '') LIKE N'%โซดา%' " &
                    "OR ISNULL(PD.Name, '') LIKE N'%สมูทตี้%' OR ISNULL(PD.Name, '') LIKE N'%อิตาเลี่ยน%')) BV " &
                    "WHERE CFC.FreeCupOut > 0 AND SL.CTime >= @DateFrom AND SL.CTime < @DateTo " & DailySalesBranchFilterSql("SL") & "AND SL.Status IS NULL " & payFilter

                Using cmd As New SqlCommand(usedSql, conn)
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = DateTime.ParseExact(dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = DateTime.ParseExact(dateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            freeCupCount = Convert.ToInt32(reader("FreeCupCount"))
                            freeCupAmount = Convert.ToDecimal(reader("FreeCupAmount"))
                        End If
                    End Using
                End Using

                'รวมสิทธิ์จากบัตรกระดาษใน SellPromotion ด้วย
                'โดยฟังก์ชันจะตัดบิลสมาชิกที่มี CustomerFreeCup ออกเพื่อไม่ให้นับซ้ำ
                LoadLegacyFreeCupSummary(conn, dateFrom, dateTo)
            End Using
        Catch ex As Exception
            freeCupCount = 0
            freeCupAmount = 0D
        End Try
    End Sub

    Private Sub LoadLegacyFreeCupSummary(conn As SqlConnection, dateFrom As String, dateTo As String)
        Try
            Dim legacySql As String =
                "SELECT SP.Detail, ISNULL(BV.FreeCupPrice, 0) AS FreeCupPrice " &
                "FROM SellPromotion SP " &
                "INNER JOIN Sell SL ON SP.SellNumber = SL.SellNumber " &
                "AND (SP.BranchNumber = SL.BranchNumber OR ISNULL(SP.BranchNumber, 0) = 0) " &
                "OUTER APPLY (SELECT MAX(ISNULL(SD.Price, 0)) AS FreeCupPrice FROM SellD SD " &
                "WHERE SD.SellNumber = SL.SellNumber AND SD.BranchNumber = SL.BranchNumber) BV " &
                "WHERE ISNULL(SP.Detail, N'') LIKE N'%แก้วฟรี%' " &
                "AND NOT EXISTS (SELECT 1 FROM CustomerFreeCup CFC WHERE CFC.SellBranchNumber = SL.BranchNumber AND CFC.SellNumber = SL.SellNumber AND CFC.FreeCupOut > 0) " &
                "AND SL.CTime >= @DateFrom AND SL.CTime < @DateTo " &
                DailySalesBranchFilterSql("SL") & "AND SL.Status IS NULL " &
                DailySalesPayFilterSql("SL")

            Using cmd As New SqlCommand(legacySql, conn)
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = DateTime.ParseExact(dateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = DateTime.ParseExact(dateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim detail As String = Convert.ToString(reader("Detail"))
                        Dim qty As Integer = 1
                        Dim match As Match = Regex.Match(detail, "ใช้สิทธิ(?:แก้วฟรี)?\s*(\d+)\s*ใบ")
                        If match.Success Then Integer.TryParse(match.Groups(1).Value, qty)

                        qty = Math.Max(1, qty)
                        freeCupCount += qty
                        freeCupAmount += qty * Convert.ToDecimal(reader("FreeCupPrice"))
                    End While
                End Using
            End Using
        Catch ex As Exception
            'ถ้าฐานข้อมูลรุ่นเก่าไม่มี SellPromotion ให้คงค่า 0 โดยไม่ทำให้รายงานเปิดไม่ได้
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call SumSellTotal()
        Call BuildSummaryCards()
        Call LoadSampleData()
        ApplyBillDetailsSplitter()
    End Sub

    Private Sub frmDailySales_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' ปิดเฉพาะหน้ารายงานยอดขาย ไม่ปิดโปรแกรมหลัก
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim dt As DataTable = GetDailySalesReportData()

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("ไม่มีข้อมูล")
            Exit Sub
        End If

        Using viewer As New frmReportViewer()
            viewer.LoadDailySalesReport(dt, dtpDateFrom.Value, dtpDateTo.Value)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        Dim receipt As ReceiptData = LoadHistoricalReceipt()
        If receipt Is Nothing Then Return

        Try
            'ใช้ตัวพิมพ์เดียวกับหน้าขาย เพื่อให้รูปแบบใบเสร็จและเครื่องพิมพ์เหมือนกัน
            Using printer As New fsell()
                printer.PrintReceiptData(receipt)
            End Using
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "พิมพ์ใบเสร็จย้อนหลังไม่สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function LoadHistoricalReceipt() As ReceiptData
        If dgvBills.CurrentRow Is Nothing Then
            MessageBox.Show(Me, "กรุณาเลือกบิลที่ต้องการพิมพ์", "พิมพ์ใบเสร็จย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Dim selected As DataRowView = TryCast(dgvBills.CurrentRow.DataBoundItem, DataRowView)
        If selected Is Nothing Then Return Nothing

        Dim sellNumber As Integer = 0
        Dim branchNumber As Integer = 0
        Integer.TryParse(Convert.ToString(selected.Row("SellNumber")), sellNumber)
        Integer.TryParse(Convert.ToString(selected.Row("BranchNumber")), branchNumber)
        If sellNumber <= 0 OrElse branchNumber <= 0 Then
            MessageBox.Show(Me, "ไม่พบเลขที่บิลหรือสาขาของรายการที่เลือก", "พิมพ์ใบเสร็จย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return Nothing
        End If

        Try
            Dim receipt As ReceiptData = Nothing
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim headerSql As String =
                    "SELECT TOP 1 SL.BillNumber, SL.CTime, ISNULL(SL.PaidMoney, 0) AS PaidMoney, " &
                    "ISNULL(SL.DCCash, 0) AS DiscountMoney, ISNULL(SL.TotalPrice, 0) AS TotalPrice, " &
                    "ISNULL(SL.PriceBV, 0) AS PriceBV, ISNULL(SL.Cash, 0) AS Cash, SL.PayType, " &
                    "ISNULL(SL.OfficerNumber, 0) AS OfficerNumber, RTRIM(ISNULL(O.Name, N'')) AS CashierName, " &
                    "RTRIM(ISNULL(CS.CustomerCode, N'')) AS CustomerCode, RTRIM(ISNULL(CS.Name, N'')) AS CustomerName, " &
                    "RTRIM(ISNULL(CS.Mobile, N'')) AS CustomerMobile, RTRIM(ISNULL(BR.BranchName, N'')) AS BranchName, " &
                    "RTRIM(ISNULL(BR.RDBranchName, N'')) AS RDBranchName, " &
                    "CASE WHEN EXISTS (SELECT 1 FROM SellCustomerCertified SCC WHERE SCC.BranchNumber=SL.BranchNumber AND SCC.SellNumber=SL.SellNumber) THEN N'รับรองลูกค้า' " &
                    "WHEN SL.PayType=1 THEN N'เงินสด' WHEN SL.PayType=4 THEN N'โอน 002203005517' ELSE N'-' END AS PayName " &
                    "FROM Sell SL " &
                    "LEFT JOIN Customer CS ON SL.CustomerNumber=CS.CustomerNumber " &
                    "LEFT JOIN Officer O ON SL.OfficerNumber=O.OfficerNumber " &
                    "LEFT JOIN Branch BR ON SL.BranchNumber=BR.BranchNumber " &
                    "WHERE SL.BranchNumber=@BranchNumber AND SL.SellNumber=@SellNumber"

                Using cmd As New SqlCommand(headerSql, conn)
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If Not reader.Read() Then
                            MessageBox.Show(Me, "ไม่พบข้อมูลบิลที่เลือก", "พิมพ์ใบเสร็จย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Return Nothing
                        End If

                        receipt = New ReceiptData With {
                            .CompanyName = "Smile Heart Foods Co.,Ltd.",
                            .BranchName = FirstNotEmpty(reader("RDBranchName"), reader("BranchName"), "สำนักงานใหญ่"),
                            .Address = "19/8 ม.8 ต.บางกระเจ้า อ.เมือง จ.สมุทรสาคร",
                            .Tel = "034869080",
                            .Fax = "034869075",
                            .TaxId = "0745543001049",
                            .PosId = "E067400002A1007",
                            .InvoiceNo = Convert.ToString(reader("BillNumber")),
                            .SaleDate = Convert.ToDateTime(reader("CTime")),
                            .CashierName = Convert.ToString(reader("CashierName")),
                            .PayBy = Convert.ToString(reader("PayName")),
                            .CustomerCode = Convert.ToString(reader("CustomerCode")),
                            .CustomerName = Convert.ToString(reader("CustomerName")),
                            .CustomerTel = Convert.ToString(reader("CustomerMobile")),
                            .GrossTotal = Convert.ToDecimal(reader("PaidMoney")),
                            .SubTotal = Convert.ToDecimal(reader("PriceBV")),
                            .DiscountBath = Convert.ToDecimal(reader("DiscountMoney")),
                            .GrandTotal = Convert.ToDecimal(reader("TotalPrice")),
                            .VatAmount = Convert.ToDecimal(reader("TotalPrice")) - Convert.ToDecimal(reader("PriceBV")),
                            .ReceiveAmount = Convert.ToDecimal(reader("Cash")),
                            .ChangeAmount = Math.Max(0D, Convert.ToDecimal(reader("Cash")) - Convert.ToDecimal(reader("TotalPrice")))
                        }
                    End Using
                End Using

                Dim itemSql As String =
                    "SELECT RTRIM(PD.ProductCode) AS ProductCode, RTRIM(PD.Barcode) AS Barcode, " &
                    "RTRIM(PD.Name) AS ProductName, ISNULL(SD.Number, 0) AS Qty, RTRIM(PD.SUnit) AS SUnit, " &
                    "CASE WHEN ISNULL(SD.Price, 0) <> 0 THEN SD.Price ELSE ISNULL(SD.PriceBD, 0) END AS UnitPrice, " &
                    "ISNULL(SD.SumPrice, 0) AS LineTotal " &
                    "FROM SellD SD INNER JOIN Product PD ON SD.PN=PD.PN " &
                    "INNER JOIN Sell SL ON SD.BranchNumber=SL.BranchNumber AND SD.SellNumber=SL.SellNumber " &
                    "WHERE SD.BranchNumber=@BranchNumber AND SD.SellNumber=@SellNumber ORDER BY SD.Item"

                Using cmd As New SqlCommand(itemSql, conn)
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New ReceiptItem With {
                                .ProductCode = Convert.ToString(reader("ProductCode")),
                                .ProductName = Convert.ToString(reader("ProductName")),
                                .Barcode = Convert.ToString(reader("Barcode")),
                                .Sunit = Convert.ToString(reader("SUnit")),
                                .Qty = Convert.ToDecimal(reader("Qty")),
                                .Price = Convert.ToDecimal(reader("UnitPrice")),
                                .Amount = Convert.ToDecimal(reader("LineTotal"))
                            }
                            receipt.Items.Add(item)
                        End While
                    End Using
                End Using
            End Using

            If receipt Is Nothing OrElse receipt.Items.Count = 0 Then
                MessageBox.Show(Me, "บิลนี้ไม่มีรายการสินค้า", "พิมพ์ใบเสร็จย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return Nothing
            End If
            Return receipt
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "โหลดข้อมูลใบเสร็จไม่สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Shared Function FirstNotEmpty(ParamArray values() As Object) As String
        For Each value As Object In values
            If value IsNot Nothing AndAlso Not Convert.IsDBNull(value) Then
                Dim text As String = Convert.ToString(value).Trim()
                If text.Length > 0 Then Return text
            End If
        Next
        Return ""
    End Function

    Private Function GetDailySalesReportData() As DataTable
        Dim dateFrom As String = dtpDateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        Dim dateTo As String = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)

        Dim clsSQL As New clsSQLQuery

        With clsSQL
            Dim sWhere As New StringBuilder
            Dim sFrom As New StringBuilder
            Dim sb As New StringBuilder

            sb.Append("ROW_NUMBER() OVER (ORDER BY SL.SellNumber ASC, SD.Item ASC) As RowNo, ")
            sb.Append("SL.SellNumber, SL.BranchNumber, BillNumber, SL.CTime, CS.Name, ISNULL(CS.Mobile, N'') AS CustomerMobile, CASE WHEN CS.Name = N'ลูกค้าทั่วไป' OR ISNULL(TRIM(CS.Mobile), N'') = N'' THEN TRIM(CS.Name) ELSE TRIM(CS.Name) + N' [' + TRIM(CS.Mobile) + N']' END AS CustomerDisplay, ISNULL(PaidMoney, 0) AS PaidMoney, ISNULL(DCCash, 0) AS DiscountMoney, ISNULL(TotalPrice, 0) AS TotalPrice, ")
            sb.Append(DailySalesPayNameSql("SL") & ",BR.RDBranchName, ")
            sb.Append("PD.ProductCode, PD.Barcode, pd.Name as PdName, SD.Item, SD.Number, SD.Price, ")
            sb.Append("SD.SumPrice,PD.SUnit,SD.SumPrice AS LineTotal, ISNULL(SPD.Detail, N'') AS SpecialDetail, ISNULL(FCD.FreeCupOut, 0) AS FreeCupOut, ISNULL(FCD.FreeCupAmount, 0) AS FreeCupAmount, ")
            sb.Append(DailySalesDiscountNoteSql("SL"))

            sFrom.Append("Sell SL ")
            sFrom.Append("INNER JOIN Customer CS ON SL.CustomerNumber = CS.CustomerNumber ")
            sFrom.Append("INNER JOIN Branch BR on SL.BranchNumber = BR.BranchNumber ")
            sFrom.Append("LEFT JOIN SellCustomerCertified SCC ON SL.BranchNumber = SCC.BranchNumber AND SL.SellNumber = SCC.SellNumber ")
            sFrom.Append("Inner Join SellD SD on SL.BranchNumber = SD.BranchNumber AND SL.SellNumber = SD.SellNumber ")
            sFrom.Append("Inner join Product PD on SD.PN = PD.PN ")
            sFrom.Append("OUTER APPLY (SELECT STUFF((SELECT N' / ' + X.Detail FROM (SELECT CASE WHEN EXISTS (SELECT 1 FROM CustomerFreeCup CFC WHERE CFC.SellBranchNumber = SL.BranchNumber AND CFC.SellNumber = SL.SellNumber AND CFC.FreeCupOut > 0) THEN N'แก้วฟรี' END AS Detail UNION ALL SELECT NULLIF(LTRIM(RTRIM(SP.Detail)), N'') FROM SellPromotion SP WHERE SP.BranchNumber = SL.BranchNumber AND SP.SellNumber = SL.SellNumber AND (ISNULL(SP.Detail, N'') NOT LIKE N'%แก้วฟรี%' OR EXISTS (SELECT 1 FROM CustomerFreeCup CFC2 WHERE CFC2.SellBranchNumber = SL.BranchNumber AND CFC2.SellNumber = SL.SellNumber AND CFC2.FreeCupOut > 0)) UNION ALL SELECT N'รับรองลูกค้า' WHERE EXISTS (SELECT 1 FROM SellCustomerCertified SCC WHERE SCC.BranchNumber = SL.BranchNumber AND SCC.SellNumber = SL.SellNumber)) X WHERE X.Detail IS NOT NULL AND X.Detail <> N'' FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 3, N'') AS Detail) SPD ")
            sFrom.Append("OUTER APPLY (SELECT ISNULL(SUM(CFC.FreeCupOut), 0) AS FreeCupOut, ISNULL(SUM(CFC.FreeCupOut * ISNULL(BV.FreeCupPrice, 0)), 0) AS FreeCupAmount FROM CustomerFreeCup CFC OUTER APPLY (SELECT MAX(ISNULL(SD2.Price, 0)) AS FreeCupPrice FROM SellD SD2 INNER JOIN Product PD2 ON SD2.PN = PD2.PN LEFT JOIN ProductGroup PG2 ON PG2.ID = PD2.GroupID LEFT JOIN Category CT2 ON CT2.ID = PG2.CategoryID WHERE SD2.BranchNumber = CFC.SellBranchNumber AND SD2.SellNumber = CFC.SellNumber AND (ISNULL(CT2.ID, -1) = 5 OR ISNULL(CT2.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG2.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PD2.SUnit, '') LIKE N'%แก้ว%' OR ISNULL(PD2.Name, '') LIKE N'%กาแฟ%' OR ISNULL(PD2.Name, '') LIKE N'%ชา%' OR ISNULL(PD2.Name, '') LIKE N'%โกโก้%' OR ISNULL(PD2.Name, '') LIKE N'%นม%' OR ISNULL(PD2.Name, '') LIKE N'%โซดา%' OR ISNULL(PD2.Name, '') LIKE N'%สมูทตี้%' OR ISNULL(PD2.Name, '') LIKE N'%อิตาเลี่ยน%')) BV WHERE CFC.SellNumber = SL.SellNumber AND (CFC.SellBranchNumber = SL.BranchNumber OR ISNULL(CFC.SellBranchNumber, 0) = 0) AND CFC.FreeCupOut > 0) FCD ")

            sWhere.Append("SL.CTime >= '" & dateFrom & "' ")
            sWhere.Append("AND SL.CTime < '" & dateTo & "' ")
            sWhere.Append(DailySalesBranchFilterSql("SL"))
            sWhere.Append("AND SL.Status IS NULL ")
            sWhere.Append(DailySalesPayFilterSql("SL"))

            .Selecting(sb.ToString(), sFrom.ToString(), sWhere.ToString(), "SL.SellNumber ASC, SD.Item ASC")

            Return .DTb
        End With

    End Function

    Private Sub btnPromotion_Click(sender As Object, e As EventArgs) Handles btnPromotion.Click
        Dim dt As DataTable = GetSalesReportPromotion()

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("ไม่มีข้อมูล")
            Exit Sub
        End If

        Using viewer As New frmReportViewer()
            viewer.LoadDailySalesReport(dt, dtpDateFrom.Value, dtpDateTo.Value)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Function GetSalesReportPromotion() As DataTable
        Dim dateFrom As String = dtpDateFrom.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        Dim dateTo As String = dtpDateTo.Value.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)

        Dim clsSQL As New clsSQLQuery

        With clsSQL
            Dim sWhere As New StringBuilder
            Dim sFrom As New StringBuilder
            Dim sb As New StringBuilder

            sb.Append("ROW_NUMBER() OVER (ORDER BY SL.SellNumber ASC, SD.Item ASC) As RowNo, ")
            sb.Append("SL.SellNumber, SL.BranchNumber, BillNumber, SL.CTime, CS.Name, ISNULL(CS.Mobile, N'') AS CustomerMobile, CASE WHEN CS.Name = N'ลูกค้าทั่วไป' OR ISNULL(TRIM(CS.Mobile), N'') = N'' THEN TRIM(CS.Name) ELSE TRIM(CS.Name) + N' [' + TRIM(CS.Mobile) + N']' END AS CustomerDisplay, ISNULL(PaidMoney, 0) AS PaidMoney, ISNULL(DCCash, 0) AS DiscountMoney, ISNULL(TotalPrice, 0) AS TotalPrice, ")
            sb.Append(DailySalesPayNameSql("SL") & ",BR.RDBranchName, ")
            sb.Append("PD.ProductCode, PD.Barcode, pd.Name as PdName, SD.Item, SD.Number, SD.Price, ")
            sb.Append("SD.SumPrice,PD.SUnit,SD.SumPrice AS LineTotal, ISNULL(SPD.Detail, N'') AS SpecialDetail, ISNULL(FCD.FreeCupOut, 0) AS FreeCupOut, ISNULL(FCD.FreeCupAmount, 0) AS FreeCupAmount, ")
            sb.Append(DailySalesDiscountNoteSql("SL"))
            sFrom.Append("Sell SL ")
            sFrom.Append("INNER JOIN Customer CS ON SL.CustomerNumber = CS.CustomerNumber ")
            sFrom.Append("INNER JOIN Branch BR on SL.BranchNumber = BR.BranchNumber ")
            sFrom.Append("LEFT JOIN SellCustomerCertified SCC ON SL.BranchNumber = SCC.BranchNumber AND SL.SellNumber = SCC.SellNumber ")
            sFrom.Append("Inner Join SellD SD on SL.BranchNumber = SD.BranchNumber AND SL.SellNumber = SD.SellNumber ")
            sFrom.Append("Inner join Product PD on SD.PN = PD.PN ")
            sFrom.Append("OUTER APPLY (SELECT STUFF((SELECT N' / ' + X.Detail FROM (SELECT CASE WHEN EXISTS (SELECT 1 FROM CustomerFreeCup CFC WHERE CFC.SellBranchNumber = SL.BranchNumber AND CFC.SellNumber = SL.SellNumber AND CFC.FreeCupOut > 0) THEN N'แก้วฟรี' END AS Detail UNION ALL SELECT NULLIF(LTRIM(RTRIM(SP.Detail)), N'') FROM SellPromotion SP WHERE SP.BranchNumber = SL.BranchNumber AND SP.SellNumber = SL.SellNumber AND (ISNULL(SP.Detail, N'') NOT LIKE N'%แก้วฟรี%' OR EXISTS (SELECT 1 FROM CustomerFreeCup CFC2 WHERE CFC2.SellBranchNumber = SL.BranchNumber AND CFC2.SellNumber = SL.SellNumber AND CFC2.FreeCupOut > 0)) UNION ALL SELECT N'รับรองลูกค้า' WHERE EXISTS (SELECT 1 FROM SellCustomerCertified SCC WHERE SCC.BranchNumber = SL.BranchNumber AND SCC.SellNumber = SL.SellNumber)) X WHERE X.Detail IS NOT NULL AND X.Detail <> N'' FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 3, N'') AS Detail) SPD ")
            sFrom.Append("OUTER APPLY (SELECT ISNULL(SUM(CFC.FreeCupOut), 0) AS FreeCupOut, ISNULL(SUM(CFC.FreeCupOut * ISNULL(BV.FreeCupPrice, 0)), 0) AS FreeCupAmount FROM CustomerFreeCup CFC OUTER APPLY (SELECT MAX(ISNULL(SD2.Price, 0)) AS FreeCupPrice FROM SellD SD2 INNER JOIN Product PD2 ON SD2.PN = PD2.PN LEFT JOIN ProductGroup PG2 ON PG2.ID = PD2.GroupID LEFT JOIN Category CT2 ON CT2.ID = PG2.CategoryID WHERE SD2.BranchNumber = CFC.SellBranchNumber AND SD2.SellNumber = CFC.SellNumber AND (ISNULL(CT2.ID, -1) = 5 OR ISNULL(CT2.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG2.Name, '') LIKE N'%เครื่องดื่ม%' OR ISNULL(PD2.SUnit, '') LIKE N'%แก้ว%' OR ISNULL(PD2.Name, '') LIKE N'%กาแฟ%' OR ISNULL(PD2.Name, '') LIKE N'%ชา%' OR ISNULL(PD2.Name, '') LIKE N'%โกโก้%' OR ISNULL(PD2.Name, '') LIKE N'%นม%' OR ISNULL(PD2.Name, '') LIKE N'%โซดา%' OR ISNULL(PD2.Name, '') LIKE N'%สมูทตี้%' OR ISNULL(PD2.Name, '') LIKE N'%อิตาเลี่ยน%')) BV WHERE CFC.SellNumber = SL.SellNumber AND (CFC.SellBranchNumber = SL.BranchNumber OR ISNULL(CFC.SellBranchNumber, 0) = 0) AND CFC.FreeCupOut > 0) FCD ")

            sWhere.Append("SL.CTime >= '" & dateFrom & "' ")
            sWhere.Append("AND SL.CTime < '" & dateTo & "' ")
            sWhere.Append(DailySalesBranchFilterSql("SL"))
            sWhere.Append("AND SL.Status IS NULL ")
            sWhere.Append(DailySalesPayFilterSql("SL"))

            .Selecting(sb.ToString(), sFrom.ToString(), sWhere.ToString(), "SL.SellNumber ASC, SD.Item ASC")

            Return .DTb
        End With

    End Function

    Private Sub btnCancelBill_Click_1(sender As Object, e As EventArgs) Handles btnCancelBill.Click
        CancelSelectedBill()
    End Sub

    Private Sub btnEditBill_Click_1(sender As Object, e As EventArgs) Handles btnEditBill.Click
        EditSelectedBill()
    End Sub
End Class







