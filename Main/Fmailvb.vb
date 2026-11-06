Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text

Public Class Fmailvb
    Private frozenShopMenuItem As ToolStripMenuItem
    Private closingSummaryShown As Boolean
    Private Const ClosingBranchNumber As Integer = 1

    Private Sub Fmailvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lokEmp_name.Renderer = New BlueToolStripRenderer()
        lokEmp_name.RenderMode = ToolStripRenderMode.Professional
        IsMdiContainer = True
        For Each control As Control In Controls
            If TypeOf control Is MdiClient Then control.BackColor = Color.FromArgb(238, 245, 252)
        Next

        DatabaseBackup.StartAutomaticBackup(Me)

    End Sub

    Private Sub frozenShopMenuItem_Click(sender As Object, e As EventArgs)
        OpenChild(New frmFrozenShopIssue())
    End Sub

    Private Sub btnBuyProduct_Click(sender As Object, e As EventArgs) Handles btnBuyProduct.Click
        OpenChild(New frmBuy())
    End Sub
    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        OpenChild(New fInventoryCount())
    End Sub

    Private Sub btnMaterialUsage_Click(sender As Object, e As EventArgs) Handles btnMaterialUsage.Click
        OpenChild(New frmMaterialUsage())
    End Sub

    Private Sub btnMaterialIssue_Click(sender As Object, e As EventArgs) Handles btnMaterialIssue.Click
        OpenChild(New frmMaterialIssue())
    End Sub

    Private Sub btnMaterialReceive_Click(sender As Object, e As EventArgs) Handles btnMaterialReceive.Click
        If Not PermissionService.HasPermission("Stock.In") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์รับวัตถุดิบเข้าสต็อก", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        OpenChild(New frmMaterialReceive())
    End Sub

    Private Sub btnMaterialStock_Click(sender As Object, e As EventArgs) Handles btnMaterialStock.Click
        OpenChild(New frmMaterialStock())
    End Sub

    Private Sub btnStockCheck_Click(sender As Object, e As EventArgs) Handles btnStockCheck.Click
        OpenChild(New frmStockCheck())
    End Sub

    Private Sub btnProduct_Click(sender As Object, e As EventArgs) Handles btnProduct.Click
        OpenChild(New fProduct())
    End Sub

    Private Sub btnMaterialDrinkAdd_Click(sender As Object, e As EventArgs) Handles btnMaterialDrinkAdd.Click
        If Not PermissionService.HasPermission("MaterialDrink.Add") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์เพิ่มวัตถุดิบเครื่องดื่ม", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using dialog As New frmMaterialDrinkAdd()
            dialog.ShowDialog(Me)
        End Using
    End Sub

    Private Sub OpenChild(child As Form)
        For Each opened As Form In MdiChildren
            If opened.GetType() Is child.GetType() Then
                child.Dispose()
                If Not opened.Visible Then opened.Show()
                opened.WindowState = FormWindowState.Maximized
                opened.Activate()
                Return
            End If
        Next
        child.MdiParent = Me
        child.Show()
        'ต้องสั่ง Maximize หลัง Show เพื่อให้ MDI คำนวณขนาดเต็มพื้นที่จริง
        child.WindowState = FormWindowState.Maximized
        child.Activate()
        'บางครั้ง MDI จะคำนวณขนาดลูกใหม่อีกครั้งหลัง Show จบ จึงย้ำอีกครั้งหลังรอบ Layout
        BeginInvoke(New MethodInvoker(Sub()
                                          If Not child.IsDisposed Then
                                              child.WindowState = FormWindowState.Maximized
                                          End If
                                      End Sub))
    End Sub

    Private Sub btnSell_Click(sender As Object, e As EventArgs) Handles btnSell.Click
        OpenChild(New fsell())
    End Sub

    Private Sub Fmailvb_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DatabaseBackup.StopAutomaticBackup()
        Application.Exit()
    End Sub

    Private Sub Fmailvb_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'เมื่อ Frozen Shop เป็น MDI Child และขยายเต็มจอ
        'กากบาทมุมขวาบนจะเป็นของ Fmailvb ให้ปิดเฉพาะ Child ก่อน
        If ActiveMdiChild IsNot Nothing AndAlso TypeOf ActiveMdiChild Is frmFrozenShopIssue Then
            e.Cancel = True
            ActiveMdiChild.Hide()
            Return
        End If

        If closingSummaryShown Then Return

        Using summaryDialog As New frmClosingSummary(BuildClosingSummary(Date.Today))
            Dim result As DialogResult = summaryDialog.ShowDialog(Me)

            If result = DialogResult.Cancel Then
                e.Cancel = True
            Else
                closingSummaryShown = True
            End If
        End Using
    End Sub

    Private Function BuildClosingSummary(summaryDate As DateTime) As String
        Dim billCount As Integer = 0
        Dim totalSales As Decimal = 0D
        Dim cashAmount As Decimal = 0D
        Dim transferAmount As Decimal = 0D
        Dim discountAmount As Decimal = 0D
        Dim certifiedAmount As Decimal = 0D
        Dim beverageCount As Decimal = 0D
        Dim beverageGross As Decimal = 0D
        Dim freeCupCount As Decimal = 0D
        Dim freeCupAmount As Decimal = 0D
        Dim newMemberCount As Integer = 0

        Dim dateFrom As DateTime = summaryDate.Date.AddHours(7)
        Dim dateTo As DateTime = summaryDate.Date.AddHours(18).AddMinutes(30)

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT COUNT(1) AS BillCount, " &
                    "ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL THEN CASE WHEN ISNULL(SL.TotalPrice,0) < ISNULL(FC.FreeCupAmount,0) THEN ISNULL(SL.TotalPrice,0)+ISNULL(FC.FreeCupAmount,0) ELSE ISNULL(SL.TotalPrice,0) END ELSE 0 END),0) AS TotalSales, " &
                    "ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL AND SL.PayType=1 THEN CASE WHEN ISNULL(SL.TotalPrice,0) < ISNULL(FC.FreeCupAmount,0) THEN ISNULL(SL.TotalPrice,0)+ISNULL(FC.FreeCupAmount,0) ELSE ISNULL(SL.TotalPrice,0) END ELSE 0 END),0) AS CashAmount, " &
                    "ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL AND SL.PayType=4 THEN CASE WHEN ISNULL(SL.TotalPrice,0) < ISNULL(FC.FreeCupAmount,0) THEN ISNULL(SL.TotalPrice,0)+ISNULL(FC.FreeCupAmount,0) ELSE ISNULL(SL.TotalPrice,0) END ELSE 0 END),0) AS TransferAmount, " &
                    "ISNULL(SUM(CASE WHEN SCC.SellNumber IS NULL THEN ISNULL(SL.DCCash,0) ELSE 0 END),0) AS DiscountAmount, " &
                    "ISNULL(SUM(CASE WHEN SCC.SellNumber IS NOT NULL OR LTRIM(RTRIM(ISNULL(CS.Name,N'')))=N'รับรองลูกค้า' THEN ISNULL(SL.TotalPrice,0) ELSE 0 END),0) AS CertifiedAmount, " &
                    "ISNULL(SUM(ISNULL(BV.BeverageCount,0)),0) AS BeverageCount, " &
                    "ISNULL(SUM(ISNULL(BV.BeverageGross,0)),0) AS BeverageGross, " &
                    "ISNULL(SUM(ISNULL(FC.FreeCupCount,0)),0) AS FreeCupCount, " &
                    "ISNULL(SUM(ISNULL(FC.FreeCupAmount,0)),0) AS FreeCupAmount " &
                    "FROM Sell SL " &
                    "LEFT JOIN Customer CS ON CS.CustomerNumber=SL.CustomerNumber " &
                    "LEFT JOIN SellCustomerCertified SCC ON SCC.BranchNumber=SL.BranchNumber AND SCC.SellNumber=SL.SellNumber " &
                    "OUTER APPLY (SELECT SUM(CFC.FreeCupOut) AS FreeCupCount, " &
                    "SUM(CASE WHEN ISNULL(CFC.FreeCupAmount,0)>0 THEN CFC.FreeCupAmount ELSE CFC.FreeCupOut*ISNULL(BP.FreeCupPrice,0) END) AS FreeCupAmount " &
                    "FROM CustomerFreeCup CFC " &
                    "OUTER APPLY (SELECT MAX(ISNULL(SD.Price,0)) AS FreeCupPrice FROM SellD SD WHERE SD.BranchNumber=SL.BranchNumber AND SD.SellNumber=SL.SellNumber) BP " &
                    "WHERE CFC.SellBranchNumber=SL.BranchNumber AND CFC.SellNumber=SL.SellNumber AND CFC.FreeCupOut>0) FC " &
                    "OUTER APPLY (SELECT SUM(CASE WHEN (ISNULL(CT.ID,-1)=5 OR ISNULL(CT.Name,N'') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG.Name,N'') LIKE N'%เครื่องดื่ม%' OR ISNULL(PD.SUnit,N'') LIKE N'%แก้ว%') AND ISNULL(PD.SUnit,N'') LIKE N'%แก้ว%' AND ISNULL(PD.Name,N'') NOT LIKE N'%ช็อต%' AND ISNULL(PD.SUnit,N'') NOT LIKE N'%ช็อต%' THEN ISNULL(SD.Number,0) ELSE 0 END) AS BeverageCount, " &
                    "SUM(CASE WHEN (ISNULL(CT.ID,-1)=5 OR ISNULL(CT.Name,N'') LIKE N'%เครื่องดื่ม%' OR ISNULL(PG.Name,N'') LIKE N'%เครื่องดื่ม%' OR ISNULL(PD.SUnit,N'') LIKE N'%แก้ว%') AND ISNULL(PD.SUnit,N'') LIKE N'%แก้ว%' AND ISNULL(PD.Name,N'') NOT LIKE N'%ช็อต%' AND ISNULL(PD.SUnit,N'') NOT LIKE N'%ช็อต%' THEN ISNULL(SD.SumPrice,0) ELSE 0 END) AS BeverageGross " &
                    "FROM SellD SD INNER JOIN Product PD ON PD.PN=SD.PN LEFT JOIN ProductGroup PG ON PG.ID=PD.GroupID LEFT JOIN Category CT ON CT.ID=PG.CategoryID " &
                    "WHERE SD.BranchNumber=SL.BranchNumber AND SD.SellNumber=SL.SellNumber) BV " &
                    "WHERE SL.BranchNumber=@Branch AND SL.CTime>=@DateFrom AND SL.CTime<@DateTo AND SL.Status IS NULL"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = ClosingBranchNumber
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateFrom
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTo
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            billCount = Convert.ToInt32(reader("BillCount"), CultureInfo.InvariantCulture)
                            totalSales = Convert.ToDecimal(reader("TotalSales"), CultureInfo.InvariantCulture)
                            cashAmount = Convert.ToDecimal(reader("CashAmount"), CultureInfo.InvariantCulture)
                            transferAmount = Convert.ToDecimal(reader("TransferAmount"), CultureInfo.InvariantCulture)
                            discountAmount = Convert.ToDecimal(reader("DiscountAmount"), CultureInfo.InvariantCulture)
                            certifiedAmount = Convert.ToDecimal(reader("CertifiedAmount"), CultureInfo.InvariantCulture)
                            beverageCount = Convert.ToDecimal(reader("BeverageCount"), CultureInfo.InvariantCulture)
                            beverageGross = Convert.ToDecimal(reader("BeverageGross"), CultureInfo.InvariantCulture)
                            freeCupCount = Convert.ToDecimal(reader("FreeCupCount"), CultureInfo.InvariantCulture)
                            freeCupAmount = Convert.ToDecimal(reader("FreeCupAmount"), CultureInfo.InvariantCulture)
                        End If
                    End Using
                End Using

                'นับสมาชิกที่สมัครใหม่ภายในช่วงเวลาของกะ
                Const newMemberSql As String =
                    "SELECT COUNT(1) FROM Customer " &
                    "WHERE CustomerNumber > 1 " &
                    "AND Status IS NULL " &
                    "AND CTime >= @DateFrom " &
                    "AND CTime < @DateTo"

                Using memberCmd As New SqlCommand(newMemberSql, conn)
                    memberCmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateFrom
                    memberCmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTo
                    newMemberCount = Convert.ToInt32(memberCmd.ExecuteScalar(), CultureInfo.InvariantCulture)
                End Using
            End Using
        Catch ex As Exception
            Return "ไม่สามารถอ่านยอดปิดกะได้" & Environment.NewLine & ex.Message
        End Try

        Dim beveragePaid As Decimal = Math.Max(0D, beverageGross - freeCupAmount)
        Dim frozenAmount As Decimal = Math.Max(0D, totalSales - beverageGross)
        Dim buddhistDate As String = summaryDate.ToString("dd/MM/", CultureInfo.InvariantCulture) & (summaryDate.Year + 543).ToString("0000", CultureInfo.InvariantCulture)
        Dim discountText As String = If(discountAmount = 0D, "-", discountAmount.ToString("N0", CultureInfo.InvariantCulture))
        Dim certifiedText As String = If(certifiedAmount = 0D, "-", certifiedAmount.ToString("N0", CultureInfo.InvariantCulture))
        Dim result As New StringBuilder()
        result.AppendLine("รอบ 07.00 - 18.30 น.")
        result.AppendLine(buddhistDate)
        result.AppendLine("ยอดขายทั้งหมด " & totalSales.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine()
        result.AppendLine("ยอดแช่แข็งทั้งหมด =" & frozenAmount.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("มีส่วนลด " & discountText & " บาท")
        result.AppendLine("ลูกค้าสมัครสมาชิกใหม่ = " & newMemberCount.ToString("N0", CultureInfo.InvariantCulture) & " คน")
        result.AppendLine()
        result.AppendLine("ยอดเครื่องดื่มทั้งหมด  " & beverageCount.ToString("N0", CultureInfo.InvariantCulture) & "แก้ว  = " & beverageGross.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("ขาย " & Math.Max(0D, beverageCount - freeCupCount).ToString("N0", CultureInfo.InvariantCulture) & "แก้ว = " & beveragePaid.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("ฟรี  " & freeCupCount.ToString("N0", CultureInfo.InvariantCulture) & "แก้ว     =" & freeCupAmount.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("รับรองลูกค้า -แก้ว = --บาท")
        result.AppendLine()
        result.AppendLine("เงินสด = " & cashAmount.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("โอน   = " & transferAmount.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("ฟรี    = " & freeCupAmount.ToString("N0", CultureInfo.InvariantCulture) & "บาท")
        result.AppendLine("แถม   =  - บาท")
        result.AppendLine("รับรองลูกค้า " & certifiedText & "บาท")
        result.AppendLine()
        result.AppendLine("-เมื่อสมัครสมาชิกใหม่")
        result.AppendLine(" รับฟรีปูอัดแผ่นทอด 1 แพ็ค 150 กรัม (หรือจนกว่าสินค้าจะหมด)")
        Return result.ToString()
    End Function

    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        OpenChild(New fCustomer())
    End Sub

    Private Sub btnCategory_Click(sender As Object, e As EventArgs) Handles btnCategory.Click
        OpenChild(New frmCategory())
    End Sub

    Private Sub btnGroupProduct_Click(sender As Object, e As EventArgs) Handles btnGroupProduct.Click
        OpenChild(New frmProductGroup())
    End Sub

    Private Sub btnPromotion_Click(sender As Object, e As EventArgs) Handles btnPromotion.Click
        OpenChild(New frmPromotion())
    End Sub

    Private Sub btnDaiySales_Click(sender As Object, e As EventArgs) Handles btnDaiySales.Click
        OpenChild(New frmDailySales())
    End Sub

    Private Sub btnReportbyProduct_Click(sender As Object, e As EventArgs) Handles btnReportbyProduct.Click
        OpenChild(New fReportSellByProductGroup())
    End Sub

    Private Sub btnStockMovement_Click(sender As Object, e As EventArgs) Handles btnStockMovement.Click
        OpenChild(New fReportStock())
    End Sub

    Private Sub btnVatSell_Click(sender As Object, e As EventArgs) Handles btnVatSell.Click
        OpenChild(New frmVatSell())
    End Sub

    Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
        DatabaseBackup.BackupCurrentDatabase(Me)
    End Sub

    Private Sub btnOfficer_Click(sender As Object, e As EventArgs) Handles btnOfficer.Click
        OpenChild(New fOfficer())
    End Sub

    Private Sub btnFrozenShopIssue_Click(sender As Object, e As EventArgs) Handles btnFrozenShopIssue.Click
        OpenChild(New frmFrozenShopIssue())
    End Sub

    Private Sub btnNotHoliday_Click(sender As Object, e As EventArgs) Handles btnNotHoliday.Click
        OpenChild(New frmMemoEmployeeHoliday())
    End Sub

    Private Sub btnMemoPromotion_Click(sender As Object, e As EventArgs)
        OpenChild(New frmMemoPromotion())
    End Sub

    Private Sub btnMemoDrinkPriceApproval_Click(sender As Object, e As EventArgs) Handles btnMemoDrinkPriceApproval.Click
        OpenChild(New frmMemoDrinkPriceApproval())
    End Sub

    Private Sub btnMemoProductPriceApproval_Click(sender As Object, e As EventArgs) Handles btnMemoProductPriceApproval.Click
        OpenChild(New frmMemoProductPriceApproval())
    End Sub

    Private Sub btnMemoProductFormula_Click(sender As Object, e As EventArgs) Handles btnMemoProductFormula.Click
        If Not PermissionService.HasPermission("ProductDrink.View") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์ดูรายการเครื่องดื่มและสูตร", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        OpenChild(New frmBeverageProduct())
    End Sub
End Class

Friend NotInheritable Class BlueToolStripRenderer
    Inherits ToolStripProfessionalRenderer
    Public Sub New()
        MyBase.New(New BlueColorTable())
    End Sub
End Class

Friend NotInheritable Class BlueColorTable
    Inherits ProfessionalColorTable
    Public Overrides ReadOnly Property ToolStripGradientBegin As Color
        Get
            Return Color.FromArgb(30, 115, 190)
        End Get
    End Property
    Public Overrides ReadOnly Property ToolStripGradientMiddle As Color
        Get
            Return Color.FromArgb(27, 105, 175)
        End Get
    End Property
    Public Overrides ReadOnly Property ToolStripGradientEnd As Color
        Get
            Return Color.FromArgb(20, 88, 155)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
        Get
            Return Color.FromArgb(92, 170, 235)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
        Get
            Return Color.FromArgb(45, 125, 205)
        End Get
    End Property
End Class
