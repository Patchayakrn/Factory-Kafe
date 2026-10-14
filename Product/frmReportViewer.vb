Imports Microsoft.Reporting.WinForms

Imports System.Globalization
Imports System.Collections.Generic
Imports System.Xml

Public Class frmReportViewer

    Private printAfterShown As Boolean
    Private reportRendered As Boolean

    Private Shared Function ThaiDateText(value As DateTime) As String
        'ส่งวันที่เป็นข้อความ พ.ศ. ที่คำนวณเอง เพื่อไม่ให้ ReportViewer
        'ตีความปีซ้ำตาม Regional/Calendar ของเครื่องปลายทาง
        Return value.ToString("dd/MM/", CultureInfo.InvariantCulture) &
               (value.Year + 543).ToString("0000", CultureInfo.InvariantCulture)
    End Function

    Public Sub PrintLoadedReportAfterShown()
        printAfterShown = True
    End Sub

    Public Sub LoadSellByProductGroupReport(dt As DataTable, dateFrom As Date, dateTo As Date, branchName As String)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptSellByProductGroup.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsSellGroup", dt))
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {
            New ReportParameter("pDateFrom", ThaiDateText(dateFrom.Date)),
            New ReportParameter("pDateTo", ThaiDateText(dateTo.Date)),
            New ReportParameter("pBranch", branchName),
            New ReportParameter("pPrintDate", Date.Now.ToString("dd MMMM yyyy (hh:mm:ss tt)", CultureInfo.InvariantCulture))
        })
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadDailySalesReport(dt As DataTable, dateFrom As Date, dateTo As Date)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptDailySales.rdlc")

        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsDailySales", dt))

        Dim p1 As New ReportParameter("pDateFrom", ThaiDateText(dateFrom.Date))
        Dim p2 As New ReportParameter("pDateTo", ThaiDateText(dateTo.Date))
        Dim p3 As New ReportParameter("pArrears", PaymentDisplay(dt, "ค้างชำระ", "ค้างชำระ"))
        Dim p4 As New ReportParameter("pTotalLabel", String.Format("TOTAL ( {0} บิล )", CountBills(dt)))
        Dim p5 As New ReportParameter("pTotalAmount", TotalAmount(dt).ToString("N2"))
        Dim p6 As New ReportParameter("pDiscountLabel", "ส่วนลดรวม")
        Dim p7 As New ReportParameter("pDiscountAmount", DistinctBillColumnTotal(dt, "DiscountMoney").ToString("N2"))
        Dim p8 As New ReportParameter("pCash", PaymentDisplay(dt, "เงินสด", "เงินสด"))
        Dim p9 As New ReportParameter("pCredit", PaymentDisplay(dt, "บัตรเครดิต", "บัตรเครดิต"))
        Dim p10 As New ReportParameter("pInstallment", PaymentDisplay(dt, "ผ่อน", "เงินผ่อน"))
        Dim p11 As New ReportParameter("pCheque", PaymentDisplay(dt, "เช็ค", "เช็ค"))
        Dim p12 As New ReportParameter("pTransfer", PaymentDisplay(dt, "โอน", "เงินโอน"))
        Dim p13 As New ReportParameter("pWriteoff", PaymentDisplay(dt, "ชำระโดยลดหนี้", "ชำระโดยลดหนี้"))
        Dim p14 As New ReportParameter("pCashCard", PaymentDisplay(dt, "บัตรเงินสด", "บัตรเงินสด"))
        Dim p15 As New ReportParameter("pPrepaid", PaymentDisplay(dt, "ล่วงหน้า", "หักจากเงินที่ชำระไว้ล่วงหน้า"))
        Dim p16 As New ReportParameter("pFreeCup", FreeCupDisplay(dt))
        Dim p17 As New ReportParameter("pCustomerCertified", PaymentDisplay(dt, "รับรองลูกค้า", "รับรองลูกค้า"))
        Dim p18 As New ReportParameter("pCashAfterDeduction", CashAfterDeductionDisplay(dt))

        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18})

        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadBuyInvoiceReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptBuyInvoice.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsBuyInvoice", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadFrozenShopIssueReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptFrozenShopIssue.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsFrozenShopIssue", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadMaterialIssueReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptMaterialIssue.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsMaterialIssue", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadMaterialReceiveReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptMaterialReceive.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsMaterialReceive", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadEmployeeHolidayMemoReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptEmployeeHolidayMemo.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsEmployeeHolidayMemo", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadMemoPromotionReport(dt As DataTable, Optional signatureOnSecondPage As Boolean = False)
        Dim reportPath As String = IO.Path.Combine(Application.StartupPath, "Report", "rptMemoPromotion.rdlc")
        If signatureOnSecondPage Then
            Dim reportDocument As New XmlDocument()
            reportDocument.Load(reportPath)

            Dim namespaceManager As New XmlNamespaceManager(reportDocument.NameTable)
            namespaceManager.AddNamespace("rd", reportDocument.DocumentElement.NamespaceURI)
            Dim approvalRectangle As XmlNode = reportDocument.SelectSingleNode("//rd:Rectangle[@Name='rectPromotionApproval']", namespaceManager)
            If approvalRectangle IsNot Nothing AndAlso approvalRectangle.SelectSingleNode("rd:PageBreak", namespaceManager) Is Nothing Then
                Dim pageBreak As XmlElement = reportDocument.CreateElement("PageBreak", reportDocument.DocumentElement.NamespaceURI)
                Dim breakLocation As XmlElement = reportDocument.CreateElement("BreakLocation", reportDocument.DocumentElement.NamespaceURI)
                breakLocation.InnerText = "Start"
                pageBreak.AppendChild(breakLocation)
                Dim topNode As XmlNode = approvalRectangle.SelectSingleNode("rd:Top", namespaceManager)
                If topNode Is Nothing Then
                    approvalRectangle.AppendChild(pageBreak)
                Else
                    approvalRectangle.InsertBefore(pageBreak, topNode)
                End If
            End If

            Using reportStream As New IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(reportDocument.OuterXml))
                ReportViewer1.LocalReport.LoadReportDefinition(reportStream)
            End Using
        Else
            ReportViewer1.LocalReport.ReportPath = reportPath
        End If
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsMemoPromotion", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadDrinkPriceApprovalReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptMemoDrinkPriceApproval.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsMemoDrinkPriceApproval", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadProductPriceApprovalReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptMemoProductPriceApproval.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsMemoProductPriceApproval", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadInventoryCountReport(dt As DataTable)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptInventoryCount.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsInventoryCount", dt))
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadStockMovementMonthlyReport(dt As DataTable, dateFrom As Date, dateTo As Date, branchName As String)
        Dim reportFile As String = IO.Path.Combine(Application.StartupPath, "Report", "rptStockMovementMonthly.rdlc")
        If IO.File.Exists(reportFile) Then
            ReportViewer1.LocalReport.ReportPath = reportFile
        Else
            'รองรับเครื่อง User ที่คัดลอกเฉพาะ EXE โดยไม่คัดลอกโฟลเดอร์ Report
            ReportViewer1.LocalReport.ReportEmbeddedResource = "Coffee_SMH2.Report.rptStockMovementMonthly.rdlc"
        End If
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsStockMovement", dt))
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {
            New ReportParameter("pDateFrom", ThaiDateText(dateFrom.Date)),
            New ReportParameter("pDateTo", ThaiDateText(dateTo.Date)),
            New ReportParameter("pBranch", If(String.IsNullOrWhiteSpace(branchName), "ไม่ระบุสาขา", branchName)),
            New ReportParameter("pPrintDate", Date.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture))
        })
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadBuyMonthlyReport(dt As DataTable, dateFrom As Date, dateTo As Date, branchName As String)
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptBuyMonthly.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsBuyMonthly", dt))
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {
            New ReportParameter("pDateFrom", ThaiDateText(dateFrom.Date)),
            New ReportParameter("pDateTo", ThaiDateText(dateTo.Date)),
            New ReportParameter("pBranch", If(String.IsNullOrWhiteSpace(branchName), "ไม่ระบุสาขา", branchName))
        })
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Private Shared Function InventoryCountTotal(dt As DataTable, columnName As String) As Decimal
        Dim total As Decimal = 0D
        If dt Is Nothing OrElse Not dt.Columns.Contains(columnName) Then Return total
        For Each row As DataRow In dt.Rows
            If Not row.IsNull(columnName) Then total += Convert.ToDecimal(row(columnName), CultureInfo.CurrentCulture)
        Next
        Return total
    End Function

    Public Sub LoadVatSellReport(dt As DataTable, monthDate As Date, branchName As String, companyName As String, totalBeforeVat As Decimal, totalVat As Decimal, Optional companyAddress As String = "", Optional companyTel As String = "", Optional taxCode As String = "")
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptVatSell.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsVatSell", BuildVatSellDailySummary(dt)))
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {
            New ReportParameter("pMonth", monthDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture)),
            New ReportParameter("pBranch", If(String.IsNullOrWhiteSpace(branchName), "ไม่ระบุสาขา", branchName)),
            New ReportParameter("pCompany", String.Join(Environment.NewLine, New String() {
                If(String.IsNullOrWhiteSpace(companyName), "-", companyName),
                If(String.IsNullOrWhiteSpace(companyAddress), "", companyAddress),
                If(String.IsNullOrWhiteSpace(companyTel) AndAlso String.IsNullOrWhiteSpace(taxCode), "", "Tel: " & companyTel & "    TAX ID: " & taxCode)
            }).Trim()),
            New ReportParameter("pTotalBeforeVat", totalBeforeVat.ToString("N2", CultureInfo.InvariantCulture)),
            New ReportParameter("pTotalVat", totalVat.ToString("N2", CultureInfo.InvariantCulture)),
            New ReportParameter("pGrandTotal", (totalBeforeVat + totalVat).ToString("N2", CultureInfo.InvariantCulture))
        })
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Public Sub LoadVatSellMonthlyReport(dt As DataTable, monthDate As Date, branchName As String, companyName As String, Optional companyAddress As String = "", Optional companyTel As String = "", Optional taxCode As String = "")
        ReportViewer1.LocalReport.ReportPath = IO.Path.Combine(Application.StartupPath, "Report", "rptVatSellMonthly.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsVatSellMonthly", dt))

        Dim posId As String = ""
        If dt IsNot Nothing AndAlso dt.Columns.Contains("POSID") Then
            For Each row As DataRow In dt.Rows
                If Not row.IsNull("POSID") AndAlso Convert.ToString(row("POSID")).Trim() <> "" Then
                    posId = Convert.ToString(row("POSID")).Trim()
                    Exit For
                End If
            Next
        End If

        Dim titleMonth As String = monthDate.ToString("MMMM", CultureInfo.InvariantCulture)
        Dim companyDisplay As String = If(String.IsNullOrWhiteSpace(companyName) OrElse companyName = "- ทั้งหมด -", "Smile Heart Foods Co.,Ltd.", companyName)
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {
            New ReportParameter("pMonthTitle", titleMonth & " ปี " & monthDate.Year.ToString(CultureInfo.InvariantCulture)),
            New ReportParameter("pDateSuffix", "/" & monthDate.Month.ToString(CultureInfo.InvariantCulture) & "/" & monthDate.Year.ToString(CultureInfo.InvariantCulture)),
            New ReportParameter("pCompanyName", companyDisplay),
            New ReportParameter("pCompanyAddress", If(String.IsNullOrWhiteSpace(companyAddress), "-", companyAddress)),
            New ReportParameter("pCompanyContact", If(String.IsNullOrWhiteSpace(companyTel) AndAlso String.IsNullOrWhiteSpace(taxCode), "-", "Tel: " & companyTel & "    TAX ID: " & taxCode)),
            New ReportParameter("pPosBranch", "Pos ID: " & If(posId = "", "-", posId) & " [" & If(String.IsNullOrWhiteSpace(branchName), "ไม่ระบุสาขา", branchName) & "]")
        })
        ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
        ReportViewer1.ZoomMode = ZoomMode.PageWidth
        ReportViewer1.RefreshReport()
    End Sub

    Private Shared Function BuildVatSellDailySummary(source As DataTable) As DataTable
        Dim result As New DataTable("ReportSell")
        result.Columns.Add("DateTime", GetType(DateTime))
        result.Columns.Add("BillNumber", GetType(String))
        result.Columns.Add("CustomerName", GetType(String))
        result.Columns.Add("PriceBV", GetType(Decimal))
        result.Columns.Add("VAT", GetType(Decimal))
        result.Columns.Add("Exempt", GetType(Decimal))
        result.Columns.Add("TotalSales", GetType(Decimal))

        Dim groups As New SortedDictionary(Of DateTime, List(Of DataRow))()
        For Each row As DataRow In source.Rows
            Dim day As DateTime = Convert.ToDateTime(row("DateTime")).Date
            If Not groups.ContainsKey(day) Then groups.Add(day, New List(Of DataRow)())
            groups(day).Add(row)
        Next

        For Each pair In groups
            Dim sales As New List(Of String)()
            Dim credits As New List(Of String)()
            Dim beforeVat As Decimal = 0D
            Dim vat As Decimal = 0D
            For Each row As DataRow In pair.Value
                Dim docType As Integer = If(IsDBNull(row("DocType")), 0, Convert.ToInt32(row("DocType")))
                Dim bill As String = Convert.ToString(row("BillNumber")).Trim()
                If docType = 1 Then
                    If bill <> "" Then credits.Add(bill)
                ElseIf bill <> "" Then
                    sales.Add(bill)
                End If
                beforeVat += If(IsDBNull(row("PriceBV")), 0D, Convert.ToDecimal(row("PriceBV")))
                vat += If(IsDBNull(row("VAT")), 0D, Convert.ToDecimal(row("VAT")))
            Next

            Dim output As DataRow = result.NewRow()
            output("DateTime") = pair.Key
            output("BillNumber") = FormatDocumentRange(sales)
            output("CustomerName") = FormatDocumentRange(credits)
            output("PriceBV") = beforeVat
            output("VAT") = vat
            output("Exempt") = 0D
            output("TotalSales") = beforeVat + vat
            result.Rows.Add(output)
        Next
        Return result
    End Function

    Private Shared Function FormatDocumentRange(values As List(Of String)) As String
        If values.Count = 0 Then Return ""
        values.Sort(StringComparer.OrdinalIgnoreCase)
        If values.Count = 1 Then Return values(0)
        Return values(0) & " - " & values(values.Count - 1)
    End Function

    Private Function BuildSummary(dt As DataTable) As String
        Dim cash As Decimal = 0D
        Dim transfer As Decimal = 0D
        Dim credit As Decimal = 0D
        Dim installment As Decimal = 0D
        Dim cheque As Decimal = 0D

        If dt IsNot Nothing Then
            For Each row As DataRow In dt.Rows
                Dim lineTotal = DecimalValue(row, "LineTotal")
                Dim payName = Convert.ToString(row("PayName")).Trim()
                If payName = "เงินสด" Then
                    cash += lineTotal
                ElseIf payName.IndexOf("โอน", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    transfer += lineTotal
                ElseIf payName.IndexOf("บัตรเครดิต", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    credit += lineTotal
                ElseIf payName.IndexOf("ผ่อน", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    installment += lineTotal
                ElseIf payName.IndexOf("เช็ค", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    cheque += lineTotal
                End If
            Next
        End If

        Return String.Format("[เงินสด {0:N2}]       [บัตรเครดิต {1:N2}]       [เงินผ่อน {2:N2}]       [เช็ค {3:N2}]{4}[เงินโอน {5:N2}]       [ชำระโดยลดหนี้ 0.00]       [บัตรเงินสด 0.00]       [หักจากเงินที่ชำระไว้ล่วงหน้า 0.00]",
                             cash, credit, installment, cheque, Environment.NewLine, transfer)
    End Function

    Private Function CountBills(dt As DataTable) As Integer
        Dim billNumbers As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        If dt Is Nothing Then Return 0
        For Each row As DataRow In dt.Rows
            Dim billKey = GetBillKey(row)
            If billKey <> String.Empty Then billNumbers.Add(billKey)
        Next
        Return billNumbers.Count
    End Function

    Private Function TotalAmount(dt As DataTable) As Decimal
        Return DistinctBillColumnTotal(dt, "TotalPrice")
    End Function

    Private Function ColumnTotal(dt As DataTable, columnName As String) As Decimal
        Dim total As Decimal = 0D
        If dt Is Nothing Then Return total
        For Each row As DataRow In dt.Rows
            total += DecimalValue(row, columnName)
        Next
        Return total
    End Function

    Private Function PaymentTotal(dt As DataTable, paymentName As String) As Decimal
        Dim total As Decimal = 0D
        If dt Is Nothing OrElse Not dt.Columns.Contains("PayName") Then Return total
        Dim usedBills As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each row As DataRow In dt.Rows
            Dim billKey = GetBillKey(row)
            If billKey <> String.Empty AndAlso usedBills.Contains(billKey) Then Continue For

            Dim payName = Convert.ToString(row("PayName")).Trim()
            If (paymentName = "เงินสด" AndAlso payName = paymentName) OrElse
               (paymentName <> "เงินสด" AndAlso payName.IndexOf(paymentName, StringComparison.OrdinalIgnoreCase) >= 0) Then
                total += DecimalValue(row, "TotalPrice")
            End If

            If billKey <> String.Empty Then usedBills.Add(billKey)
        Next
        Return total
    End Function

    Private Function PaymentDisplay(dt As DataTable, paymentName As String, displayName As String) As String
        Dim amount As Decimal = PaymentTotal(dt, paymentName)

        'ลูกค้ารับรองชำระด้วยเงินสด ดังนั้นยอดเงินสดในรายงานต้องรวมยอดรับรองลูกค้าด้วย
        'แต่ยังคงแสดงบรรทัดรับรองลูกค้าแยกต่างหากตามเดิม
        If paymentName = "เงินสด" Then
            amount += PaymentTotal(dt, "รับรองลูกค้า")
        End If

        Return String.Format("[{0} {1:N2}]", displayName, amount)
    End Function

    Private Function FreeCupDisplay(dt As DataTable) As String
        Dim cupCount As Decimal = DistinctBillColumnTotal(dt, "FreeCupOut")
        Dim cupAmount As Decimal = DistinctBillColumnTotal(dt, "FreeCupAmount")
        Return String.Format("[แก้วฟรี {0:N0} ใบ / {1:N2}]", cupCount, cupAmount)
    End Function

    Private Function CashAfterDeductionDisplay(dt As DataTable) As String
        Dim freeCupAmount As Decimal = DistinctBillColumnTotal(dt, "FreeCupAmount")
        Dim customerCertifiedAmount As Decimal = PaymentTotal(dt, "รับรองลูกค้า")

        'เงินสดต้องรวมยอดรับรองลูกค้าด้วย เช่น 245 + 50 = 295
        'จากนั้นหักมูลค่าแก้วฟรีและยอดรับรองลูกค้าออกตามรายการสรุป
        Dim cashAmount As Decimal = PaymentTotal(dt, "เงินสด") + customerCertifiedAmount
        Dim amountAfterDeduction As Decimal = cashAmount - freeCupAmount - customerCertifiedAmount
        Return String.Format("[เงินสดหลังหัก {0:N2}]", amountAfterDeduction)
    End Function

    Private Function DecimalValue(row As DataRow, columnName As String) As Decimal
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) OrElse IsDBNull(row(columnName)) Then Return 0D
        Return Convert.ToDecimal(row(columnName))
    End Function

    Private Function DistinctBillColumnTotal(dt As DataTable, columnName As String) As Decimal
        Dim total As Decimal = 0D
        If dt Is Nothing Then Return total

        Dim usedBills As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each row As DataRow In dt.Rows
            Dim billKey = GetBillKey(row)
            If billKey <> String.Empty AndAlso usedBills.Contains(billKey) Then Continue For

            total += DecimalValue(row, columnName)
            If billKey <> String.Empty Then usedBills.Add(billKey)
        Next

        Return total
    End Function

    Private Function GetBillKey(row As DataRow) As String
        If row Is Nothing Then Return String.Empty

        If row.Table.Columns.Contains("BranchNumber") AndAlso row.Table.Columns.Contains("SellNumber") Then
            Return Convert.ToString(row("BranchNumber")).Trim() & "|" & Convert.ToString(row("SellNumber")).Trim()
        End If

        If row.Table.Columns.Contains("BillNumber") Then
            Return Convert.ToString(row("BillNumber")).Trim()
        End If

        Return String.Empty
    End Function

    Private Sub frmReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'แต่ละเมธอด Load...Report จะกำหนด RDLC, DataSource และ Parameters
        'ก่อนเรียก RefreshReport เพื่อป้องกันการประมวลผลรายงานก่อนมีค่า pPosId
    End Sub

    Private Sub frmReportViewer_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        SchedulePrintIfReady()
    End Sub

    Private Sub ReportViewer1_RenderingComplete(sender As Object, e As RenderingCompleteEventArgs) Handles ReportViewer1.RenderingComplete
        reportRendered = True
        SchedulePrintIfReady()
    End Sub

    Private Sub SchedulePrintIfReady()
        If Not printAfterShown OrElse Not reportRendered OrElse Not IsHandleCreated Then Return

        printAfterShown = False
        BeginInvoke(New MethodInvoker(AddressOf PrintLoadedReport))
    End Sub

    Private Sub PrintLoadedReport()
        Try
            ReportViewer1.PrintDialog()
        Catch ex As Exception
            'ถ้า ReportViewer ยังไม่พร้อมพิมพ์ ให้คงหน้ารายงานไว้โดยไม่แสดงกล่องแจ้งเตือน
        End Try
    End Sub

End Class
