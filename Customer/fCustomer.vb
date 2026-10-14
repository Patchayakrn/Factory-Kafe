Imports System.Data.SqlClient
Imports System.Text
Imports System.Data

Public Class fCustomer
    Public Property SelectedCustomerName As String
    Public Property SelectedCustomerPoint As Decimal
    Public Property CustomerNumber As Long = 0
    Public Property PointReceive As Decimal
    Public Property CustomerCode As String
    Dim ContactName, Barcode As String
    Private Sub SaveCustomerAccumulatedCupBalance(conn As SqlConnection, tr As SqlTransaction,
                                                   customerNumber As Long, requestedAccumulatedCups As Integer)
        If customerNumber <= 0 Then Return
        If requestedAccumulatedCups < 0 Then requestedAccumulatedCups = 0

        Dim requestedFreeCupBalance As Integer = requestedAccumulatedCups \ 10
        Dim requestedCupBalance As Integer = requestedAccumulatedCups Mod 10

        Dim oldFreeCupBalance As Integer = 0
        Dim oldCupBalance As Decimal = 0D
        Using readCmd As New SqlCommand(
            "SELECT TOP 1 ISNULL(CupBalance, 0), ISNULL(FreeCupBalance, 0) " &
            "FROM CustomerFreeCup WHERE CustomerNumber=@CustomerNumber ORDER BY ID DESC", conn, tr)
            readCmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
            Using rd As SqlDataReader = readCmd.ExecuteReader()
                If rd.Read() Then
                    oldCupBalance = Convert.ToDecimal(rd.GetValue(0))
                    oldFreeCupBalance = Convert.ToInt32(rd.GetValue(1))
                End If
            End Using
        End Using

        If oldCupBalance = requestedCupBalance AndAlso oldFreeCupBalance = requestedFreeCupBalance Then Return

        Dim nextId As Integer
        Using idCmd As New SqlCommand("SELECT ISNULL(MAX(ID), 0) + 1 FROM CustomerFreeCup WHERE CustomerNumber=@CustomerNumber", conn, tr)
            idCmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
            nextId = Convert.ToInt32(idCmd.ExecuteScalar())
        End Using

        Using insertCmd As New SqlCommand(
            "INSERT INTO CustomerFreeCup " &
            "(CustomerNumber, ID, SellNumber, SellBranchNumber, CupIn, FreeCupIn, FreeCupOut, CupBalance, FreeCupBalance, Detail, CTime) " &
            "VALUES (@CustomerNumber, @ID, NULL, NULL, @CupIn, @FreeCupIn, 0, @CupBalance, @FreeCupBalance, @Detail, GETDATE())", conn, tr)
            insertCmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
            insertCmd.Parameters.Add("@ID", SqlDbType.Int).Value = nextId
            Dim cupInParam As SqlParameter = insertCmd.Parameters.Add("@CupIn", SqlDbType.Decimal)
            cupInParam.Precision = 18
            cupInParam.Scale = 2
            cupInParam.Value = requestedAccumulatedCups
            insertCmd.Parameters.Add("@FreeCupIn", SqlDbType.Int).Value = requestedFreeCupBalance
            insertCmd.Parameters.Add("@CupBalance", SqlDbType.Decimal).Value = requestedCupBalance
            insertCmd.Parameters.Add("@FreeCupBalance", SqlDbType.Int).Value = requestedFreeCupBalance
            insertCmd.Parameters.Add("@Detail", SqlDbType.NVarChar, 500).Value = "ปรับยอดแก้วสะสมจากกระดาษ: " & requestedAccumulatedCups.ToString() & "/10 แก้ว"
            insertCmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function GetCustomerAccumulatedCupBalance(customerNumber As Long) As Integer
        If customerNumber <= 0 Then Return 0
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT SellNumber, CupIn, FreeCupIn, FreeCupOut, CupBalance, FreeCupBalance, Detail FROM CustomerFreeCup WHERE CustomerNumber=@CustomerNumber ORDER BY ID ASC", conn)
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
                    Dim table As New DataTable()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        table.Load(reader)
                    End Using
                    Dim cupBalance As Decimal = 0D
                    Dim freeCupBalance As Integer = 0
                    CustomerFreeCupBalance.ReadLatestBalance(table, cupBalance, freeCupBalance)
                    Return Convert.ToInt32(Math.Truncate(cupBalance))
                End Using
            End Using
        Catch
        End Try
        Return 0
    End Function


    Private Sub fCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'รองรับฐานข้อมูลเดิมที่ยังไม่มีตารางส่วนขยายของลูกค้า
        Try
            DbSchemaChecker.EnsureSupportTables()
        Catch
            'การค้นหาลูกค้ายังทำงานได้ แม้การตรวจโครงสร้างจะล้มเหลว
        End Try
        cbLimitSearch.SelectedIndex = 0
        cbStatus.SelectedIndex = 0
        UpdateFreeCupSummary(0)
        WireSearchEnterKey()
        Me.ActiveControl = tbMobile
        Me.BeginInvoke(New Action(Sub()
                                      tbMobile.Focus()
                                      tbMobile.SelectAll()
                                  End Sub))
    End Sub

    Private Sub btnFreeCupHistory_Click(sender As Object, e As EventArgs) Handles btnFreeCupHistory.Click
        If CustomerNumber <= 0 Then
            MessageBox.Show("กรุณาเลือกข้อมูลลูกค้าก่อนดูประวัติแต้มและสิทธิ์", "ประวัติแต้ม/สิทธิ์", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using history As New fCustomerFreeCupHistory(Convert.ToInt32(CustomerNumber), tbName.Text.Trim(), tbCustomerCode.Text.Trim())
            history.ShowDialog(Me)
        End Using
    End Sub

    Private Sub UpdateFreeCupSummary(customerNumber As Long)
        If lblFreeCupAvailableValue Is Nothing Then Return

        Dim availableFreeCup As Integer = 0
        If customerNumber > 0 Then
            Try
                Using conn As SqlConnection = modVariable.OpenConnection()
                    Using cmd As New SqlCommand(
                        "SELECT SellNumber, CupIn, FreeCupIn, FreeCupOut, CupBalance, FreeCupBalance, Detail " &
                        "FROM CustomerFreeCup WHERE CustomerNumber=@CustomerNumber ORDER BY ID ASC", conn)
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
                        Dim table As New DataTable()
                        Using rd As SqlDataReader = cmd.ExecuteReader()
                            table.Load(rd)
                        End Using
                        Dim cupBalance As Decimal = 0D
                        CustomerFreeCupBalance.ReadLatestBalance(table, cupBalance, availableFreeCup)
                    End Using
                End Using
            Catch
                availableFreeCup = 0
            End Try
        End If

        lblFreeCupAvailableValue.Text = availableFreeCup.ToString("N0") & " ใบ"
        btnFreeCupHistory.Enabled = customerNumber > 0
    End Sub

    Private Sub WireSearchEnterKey()
        Dim searchControls As Control() = {
            tbName,
            tbMobile,
            tbAddress,
            tbCustomerCode,
            cbLimitSearch,
            cbStatus
        }

        For Each c As Control In searchControls
            AddHandler c.KeyDown, AddressOf SearchControl_KeyDown
        Next
    End Sub

    Private Sub SearchControl_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter Then Exit Sub

        e.SuppressKeyPress = True
        e.Handled = True
        btSearch.PerformClick()
    End Sub
    Private Sub btSearchClick(SearchGeneralCustomer As Boolean)
        Try
            Dim SC As New StringBuilder
            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("Customer.CustomerNumber,")
                sb.Append("  RTRIM(Customer.CustomerCode) AS 'CustomerCode',")
                sb.Append("  RTRIM(Customer.Barcode) AS 'Barcode',")
                sb.Append("  RTRIM(Customer.Name) AS 'Name',")
                sb.Append("  RTRIM(Customer.RDBranchName) AS 'RDBranchName',")
                sb.Append("  RTRIM(Customer.ContactName) AS 'ContactName',")
                sb.Append("  RTRIM(Customer.EMail) AS 'EMail',")
                sb.Append("  Customer.Credit,")
                sb.Append("  Customer.DiscountP,")
                sb.Append("  CAST(Customer.LimitCreditMoney AS bit) AS 'LimitCreditMoney',")
                sb.Append("  Customer.CreditMoney,")
                sb.Append("  CAST(0 AS money) AS 'RemainPay',")
                sb.Append("  Customer.PriceType,")
                sb.Append("  Customer.MemberLevel,")
                sb.Append("  Customer.MemberExpire,")
                sb.Append("  CAST(Customer.CanUsePromotion AS bit) AS 'CanUsePromotion',")
                sb.Append("  Customer.CanUsePMTBill,")
                sb.Append("  CAST(Customer.CanCollectPoint AS bit) AS 'CanCollectPoint',")
                sb.Append("  Customer.Point,")
                sb.Append("  ISNULL((SELECT TOP 1 CFC.CupBalance FROM CustomerFreeCup CFC WHERE CFC.CustomerNumber = Customer.CustomerNumber ORDER BY CFC.ID DESC), 0) AS 'AccumulatedCupBalance',")
                sb.Append("  Customer.AdvanceCN,")
                sb.Append("  RTRIM(Customer.Tel) AS 'Tel',")
                sb.Append("  RTRIM(Customer.Mobile) AS 'Mobile',")
                sb.Append("  RTRIM(Customer.Fax) AS 'Fax',")
                sb.Append("  RTRIM(Customer.TaxCode) AS 'TaxCode',")
                sb.Append("  Customer.Birthday,")
                sb.Append("  RTRIM(Customer.Address) AS 'Address',")
                sb.Append("  RTRIM(Customer.PassportNo) AS 'PassportNo',")
                sb.Append("  RTRIM(Customer.CountryCode) AS 'CountryCode',")
                sb.Append("  RTRIM(Customer.Gender) AS 'Gender',")
                sb.Append("  RTRIM(Customer.StreetAddress) AS 'StreetAddress',")
                sb.Append("  RTRIM(Customer.SubDistrict) AS 'SubDistrict',")
                sb.Append("  RTRIM(Customer.District) AS 'District',")
                sb.Append("  RTRIM(Customer.Province) AS 'Province',")
                sb.Append("  RTRIM(Customer.Country) AS 'Country',")
                sb.Append("  RTRIM(Customer.ZipCode) AS 'ZipCode',")
                sb.Append("  RTRIM(Customer.TermOfPayment) AS 'TermOfPayment',")
                sb.Append("  RTRIM(Customer.Incoterms) AS 'Incoterms',")
                sb.Append("  RTRIM(Customer.ModeOfShipment) AS 'ModeOfShipment',")
                sb.Append("  RTRIM(Customer.PortOfLoading) AS 'PortOfLoading',")
                sb.Append("  RTRIM(Customer.PortOfDischarge) AS 'PortOfDischarge',")
                sb.Append("  Customer.DeliveryID,")
                sb.Append("  RTRIM(Delivery.Name) AS 'DeliveryName',")
                sb.Append("  CAST(Customer.Employee AS bit) AS 'Employee',")
                sb.Append("  RTRIM(Customer.WebEMail) AS 'WebEMail',")
                sb.Append("  RTRIM(Customer.Comment) AS 'Comment',")
                sb.Append("  RTRIM(Customer.PicName) AS 'PicName',")
                sb.Append("  Customer.Status,")
                sb.Append("  RTRIM(COff.Name) AS 'COName',")
                sb.Append("  Customer.CTime,")
                sb.Append("  RTRIM(UOff.Name) AS 'UOName',")
                sb.Append("  Customer.UTime ")
                SC.Append("Customer ")
                SC.Append("LEFT OUTER JOIN Delivery ON Customer.DeliveryID = Delivery.ID ")
                SC.Append("LEFT OUTER JOIN Officer COff ON Customer.CONumber = COff.OfficerNumber ")
                SC.Append("LEFT OUTER JOIN Officer UOff ON Customer.UONumber = UOff.OfficerNumber ")

                Dim whereStr As String = ""

                If SearchGeneralCustomer Then
                    whereStr = "Customer.CustomerNumber = 1"
                Else
                    If tbName.Text <> "" Then
                        If whereStr <> "" Then whereStr &= " AND "
                        whereStr &= "Customer.Name LIKE N'%" & tbName.Text & "%'"
                    End If


                    If tbMobile.Text <> "" Then
                        If whereStr <> "" Then whereStr &= " AND "
                        whereStr &= "Customer.Mobile LIKE N'%" & tbMobile.Text & "%'"
                    End If

                    If tbAddress.Text <> "" Then
                        If whereStr <> "" Then whereStr &= " AND "
                        whereStr &= "Customer.Address LIKE N'%" & tbAddress.Text & "%'"
                    End If


                    If tbCustomerCode.Text <> "" Then
                        If whereStr <> "" Then whereStr &= " AND "
                        whereStr &= "Customer.CustomerCode LIKE N'%" & tbCustomerCode.Text & "%'"
                    End If

                    If cbStatus.SelectedIndex = 1 Then
                        If whereStr <> "" Then whereStr &= " AND "
                        whereStr &= "Customer.Status IS NULL"
                    ElseIf cbStatus.SelectedIndex = 2 Then
                        If whereStr <> "" Then whereStr &= " AND "
                        whereStr &= "Customer.Status = 1"
                    End If

                    If String.IsNullOrWhiteSpace(whereStr) Then whereStr = "1=1"

                End If

                .Selecting(sb.ToString, SC.ToString, whereStr.ToString, "Customer.Name ASC")
                dtGCustomer.DataSource = .DTb
                ConfigureCustomerGridColumns()

            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Function SafeString(val As Object) As String
        If val Is Nothing OrElse IsDBNull(val) Then
            Return ""
        End If
        Return val.ToString()
    End Function

    Private Sub ConfigureCustomerGridColumns()
        Dim settings As Tuple(Of String, Integer)() = {
            Tuple.Create("CustomerNumber|ID", 60),
            Tuple.Create("CustomerCode|รหัสลูกค้า", 120),
            Tuple.Create("Barcode|Barcode", 120),
            Tuple.Create("Name|Name (ชื่อลูกค้า)", 250),
            Tuple.Create("RDBranchName|ชื่อสาขาตามกรมสรรพากร", 220),
            Tuple.Create("ContactName|Default Contract Name (ชื่อผู้ติดต่อ)", 300),
            Tuple.Create("EMail|Email", 180),
            Tuple.Create("Credit|Credit (วัน)", 110),
            Tuple.Create("DiscountP|ส่วนลด (%)", 110),
            Tuple.Create("LimitCreditMoney|จำกัดวงเงิน", 120),
            Tuple.Create("CreditMoney|วงเงินเครดิต", 130),
            Tuple.Create("PriceType|ช่องราคาขาย", 120),
            Tuple.Create("MemberLevel|ระดับสมาชิก", 130),
            Tuple.Create("MemberExpire|วันหมดอายุสมาชิก", 170),
            Tuple.Create("CanUsePromotion|ใช้โปรโมชั่นได้", 140),
            Tuple.Create("CanUsePMTBill|ใช้โปรโมชั่นบิลได้", 150),
            Tuple.Create("CanCollectPoint|สะสมแต้มได้", 120),
            Tuple.Create("Point|Point (แต้มที่มี)", 130),
            Tuple.Create("AccumulatedCupBalance|แก้วสะสม (แก้ว)", 150),
            Tuple.Create("AdvanceCN|เงินที่ชำระล่วงหน้า", 170),
            Tuple.Create("Tel|Tel (เบอร์โทรศัพท์)", 150),
            Tuple.Create("Mobile|Mobile (เบอร์มือถือ)", 170),
            Tuple.Create("Fax|FAX", 130),
            Tuple.Create("TaxCode|Tax Code (เลขผู้เสียภาษี)", 210),
            Tuple.Create("Birthday|Birth Date (วันเกิด)", 160),
            Tuple.Create("Address|Full Address (ที่อยู่แบบเต็ม)", 260),
            Tuple.Create("PassportNo|Passport Number", 180),
            Tuple.Create("CountryCode|Country Code", 140),
            Tuple.Create("Gender|Gender (เพศ)", 120),
            Tuple.Create("StreetAddress|Street Address (เลขที่ถนน)", 220),
            Tuple.Create("SubDistrict|District (ตำบล)", 150),
            Tuple.Create("District|District (อำเภอ)", 150),
            Tuple.Create("Province|Province (จังหวัด)", 180),
            Tuple.Create("Country|Country (ประเทศ)", 180),
            Tuple.Create("ZipCode|Zip Code (รหัสไปรษณีย์)", 180),
            Tuple.Create("TermOfPayment|Term Of Payment (เงื่อนไขการชำระเงิน)", 300),
            Tuple.Create("Incoterms|Incoterms (เงื่อนไขการขนส่ง)", 250),
            Tuple.Create("ModeOfShipment|Mode Of Shipment (วิธีการขนส่ง-ส่งออก)", 320),
            Tuple.Create("PortOfLoading|Port Of Loading (เมืองท่าเส้นทาง)", 300),
            Tuple.Create("DeliveryName|การขนส่งในประเทศ", 220),
            Tuple.Create("Employee|เป็นลูกจ้างของบริษัท", 180),
            Tuple.Create("WebEMail|Web EMail", 180),
            Tuple.Create("Comment|Comment (หมายเหตุ)", 220),
            Tuple.Create("PicName|ชื่อรูป", 180),
            Tuple.Create("Status|Status", 110),
            Tuple.Create("COName|Create Officer", 180),
            Tuple.Create("CTime|Create Time", 180),
            Tuple.Create("UOName|Update Officer", 180),
            Tuple.Create("UTime|Update Time", 180)
        }

        For Each column As DataGridViewColumn In dtGCustomer.Columns
            column.Visible = False
            column.ReadOnly = True
        Next

        For displayIndex As Integer = 0 To settings.Length - 1
            Dim parts = settings(displayIndex).Item1.Split("|"c)
            Dim fieldName = parts(0)
            If Not dtGCustomer.Columns.Contains(fieldName) Then Continue For

            Dim column = dtGCustomer.Columns(fieldName)
            column.HeaderText = parts(1)
            column.Width = settings(displayIndex).Item2
            column.DisplayIndex = displayIndex
            column.Visible = True
        Next
    End Sub

    Private Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        btSearchClick(False)
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        SaveCustomer()
    End Sub

    Private Sub btcancel_Click(sender As Object, e As EventArgs) Handles btcancel.Click
        ClearCustomerInput()

        Dim table = TryCast(dtGCustomer.DataSource, DataTable)
        If table IsNot Nothing Then
            table.Rows.Clear()
        Else
            Dim view = TryCast(dtGCustomer.DataSource, DataView)
            If view IsNot Nothing Then view.Table.Rows.Clear()
        End If

        dtGCustomer.ClearSelection()
        SelectedCustomerName = ""
        CustomerNumber = 0
        PointReceive = 0D
        CustomerCode = ""
        UpdateFreeCupSummary(0)
    End Sub

    Private Sub SaveCustomer()
        Dim customerName As String = tbName.Text.Trim()
        Dim customerCodeText As String = tbCustomerCode.Text.Trim()
        Dim mobileText As String = tbMobile.Text.Trim()
        Dim addressText As String = tbAddress.Text.Trim()
        Dim accumulatedCupQty As Integer = 0

        If Not Integer.TryParse(tbFreeCupOpening.Text.Trim(), accumulatedCupQty) OrElse accumulatedCupQty < 0 Then
            MessageBox.Show("กรุณากรอกจำนวนแก้วสะสมเป็นเลขจำนวนเต็มตั้งแต่ 0 ขึ้นไป", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            tbFreeCupOpening.Focus()
            tbFreeCupOpening.SelectAll()
            Exit Sub
        End If

        If customerName = "" Then
            MessageBox.Show("กรุณากรอกชื่อลูกค้าก่อนบันทึก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            tbName.Focus()
            Exit Sub
        End If

        If CustomerNumber > 0 Then
            UpdateCustomer(CustomerNumber, customerCodeText, customerName, mobileText, addressText, accumulatedCupQty)
            Exit Sub
        End If

        Try
            Cursor.Current = Cursors.WaitCursor

            Using conn As SqlConnection = modVariable.OpenConnection()
                If conn.State <> ConnectionState.Open Then conn.Open()

                Using tr As SqlTransaction = conn.BeginTransaction()
                    Dim duplicateMobileCustomerNumber As Long = 0
                    Dim duplicateMobileCustomerName As String = ""
                    If mobileText <> "" AndAlso FindDuplicateMobile(conn, tr, mobileText, 0, duplicateMobileCustomerNumber, duplicateMobileCustomerName) Then
                        tr.Rollback()
                        ShowDuplicateMobileWarning("เบอร์มือถือซ้ำกับสมาชิก " & duplicateMobileCustomerName &
                                                   " (รหัสสมาชิก " & duplicateMobileCustomerNumber.ToString() & ")" &
                                                   Environment.NewLine & "กรุณาตรวจสอบเบอร์อีกครั้ง")
                        tbMobile.Focus()
                        tbMobile.SelectAll()
                        Exit Sub
                    End If

                    If customerCodeText <> "" AndAlso IsDuplicateCustomerCode(conn, tr, customerCodeText) Then
                        tr.Rollback()
                        MessageBox.Show("Customer Code (รหัสลูกค้า) ซ้ำ กรุณาป้อนใหม่อีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tbCustomerCode.Focus()
                        tbCustomerCode.SelectAll()
                        Exit Sub
                    End If

                    Dim nextCustomerNumber As Integer = GetNextCustomerNumber(conn, tr)

                    Using cmd As New SqlCommand("
INSERT INTO Customer
    (CustomerNumber, CustomerCode, Name, Address, Mobile, Credit, LimitCreditMoney, CreditMoney,
     DiscountP, PriceType, MemberLevel, MemberExpire, CanUsePromotion, CanUsePMTBill,
     CanCollectPoint, Point, AdvanceCN, Employee, Status, CONumber, UONumber, CTime, UTime)
VALUES
    (@CustomerNumber, @CustomerCode, @Name, @Address, @Mobile, 0, 0, 0,
     0, 1, 0, GETDATE(), 1, NULL,
     1, 0, 0, 0, @Status, @CONumber, @UONumber, GETDATE(), GETDATE())", conn, tr)

                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = nextCustomerNumber
                        cmd.Parameters.Add("@CustomerCode", SqlDbType.NVarChar, 50).Value = DbValueOrNull(customerCodeText)
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = customerName
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 300).Value = DbValueOrNull(addressText)
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 50).Value = DbValueOrNull(mobileText)
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(cbStatus.SelectedIndex = 2, CType(1, Object), DBNull.Value)
                        Dim loginOfficerNumber As Integer = GetLoginOfficerNumber()
                        cmd.Parameters.Add("@CONumber", SqlDbType.Int).Value = loginOfficerNumber
                        cmd.Parameters.Add("@UONumber", SqlDbType.Int).Value = loginOfficerNumber
                        cmd.ExecuteNonQuery()
                    End Using

                    SaveCustomerCertification(conn, tr, nextCustomerNumber, chkCustomerCertified.Checked)
                    SaveCustomerAccumulatedCupBalance(conn, tr, nextCustomerNumber, accumulatedCupQty)
                    tr.Commit()
                End Using
            End Using

            MessageBox.Show("บันทึกข้อมูลลูกค้าเรียบร้อยแล้ว", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearCustomerInput()
            btSearchClick(False)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Save Customer Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub UpdateCustomer(customerNumber As Long,
                               customerCodeText As String,
                               customerName As String,
                               mobileText As String,
                               addressText As String,
                               accumulatedCupQty As Integer)
        Try
            Cursor.Current = Cursors.WaitCursor

            Using conn As SqlConnection = modVariable.OpenConnection()
                If conn.State <> ConnectionState.Open Then conn.Open()

                Using tr As SqlTransaction = conn.BeginTransaction()
                    Dim duplicateMobileCustomerNumber As Long = 0
                    Dim duplicateMobileCustomerName As String = ""
                    If mobileText <> "" AndAlso FindDuplicateMobile(conn, tr, mobileText, customerNumber, duplicateMobileCustomerNumber, duplicateMobileCustomerName) Then
                        tr.Rollback()
                        ShowDuplicateMobileWarning("เบอร์มือถือซ้ำกับสมาชิก " & duplicateMobileCustomerName &
                                                   " (รหัสสมาชิก " & duplicateMobileCustomerNumber.ToString() & ")" &
                                                   Environment.NewLine & "กรุณาตรวจสอบเบอร์อีกครั้ง")
                        tbMobile.Focus()
                        tbMobile.SelectAll()
                        Return
                    End If

                    If customerCodeText <> "" Then
                        Using duplicateCmd As New SqlCommand(
                            "SELECT COUNT(1) FROM Customer WHERE CustomerCode=@CustomerCode " &
                            "AND CustomerNumber<>@CustomerNumber", conn, tr)

                            duplicateCmd.Parameters.Add("@CustomerCode", SqlDbType.NVarChar, 50).Value = customerCodeText
                            duplicateCmd.Parameters.Add("@CustomerNumber", SqlDbType.BigInt).Value = customerNumber
                            If Convert.ToInt32(duplicateCmd.ExecuteScalar()) > 0 Then
                                tr.Rollback()
                                MessageBox.Show("Customer Code (รหัสลูกค้า) ซ้ำ กรุณาป้อนใหม่อีกครั้ง",
                                                "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                tbCustomerCode.Focus()
                                tbCustomerCode.SelectAll()
                                Return
                            End If
                        End Using
                    End If

                    Const sql As String =
                        "UPDATE Customer SET CustomerCode=@CustomerCode, Name=@Name, " &
                        "Address=@Address, Mobile=@Mobile, Status=@Status, " &
                        "UONumber=@UONumber, UTime=GETDATE() " &
                        "WHERE CustomerNumber=@CustomerNumber"

                    Using cmd As New SqlCommand(sql, conn, tr)
                        cmd.Parameters.Add("@CustomerCode", SqlDbType.NVarChar, 50).Value = DbValueOrNull(customerCodeText)
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = customerName
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 300).Value = DbValueOrNull(addressText)
                        cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 50).Value = DbValueOrNull(mobileText)
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(cbStatus.SelectedIndex = 2, CType(1, Object), DBNull.Value)
                        cmd.Parameters.Add("@UONumber", SqlDbType.Int).Value = GetLoginOfficerNumber()
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.BigInt).Value = customerNumber

                        If cmd.ExecuteNonQuery() = 0 Then
                            Throw New ApplicationException("ไม่พบข้อมูลลูกค้าที่ต้องการแก้ไข")
                        End If
                    End Using

                    SaveCustomerCertification(conn, tr, customerNumber, chkCustomerCertified.Checked)
                    SaveCustomerAccumulatedCupBalance(conn, tr, customerNumber, accumulatedCupQty)
                    tr.Commit()
                End Using
            End Using

            MessageBox.Show("แก้ไขข้อมูลลูกค้าเรียบร้อยแล้ว", "บันทึก",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearCustomerInput()
            customerNumber = 0
            CustomerCode = ""
            SelectedCustomerName = ""
            PointReceive = 0D
            UpdateFreeCupSummary(0)
            btSearchClick(False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Update Customer Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function GetLoginOfficerNumber() As Integer
        If ModSession.LoginOfficerID > 0 Then
            Return Convert.ToInt32(ModSession.LoginOfficerID)
        End If

        Return 1
    End Function

    Private Sub SaveCustomerCertification(conn As SqlConnection,
                                          tr As SqlTransaction,
                                          customerNumber As Long,
                                          isCertified As Boolean)
        Const sql As String =
            "IF EXISTS (SELECT 1 FROM dbo.CustomerCertification WHERE CustomerNumber=@CustomerNumber) " &
            "BEGIN UPDATE dbo.CustomerCertification SET IsCertified=@IsCertified, UTime=GETDATE() " &
            "WHERE CustomerNumber=@CustomerNumber END " &
            "ELSE IF @IsCertified=1 " &
            "BEGIN INSERT INTO dbo.CustomerCertification (CustomerNumber, IsCertified, CTime) " &
            "VALUES (@CustomerNumber, 1, GETDATE()) END"

        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
            cmd.Parameters.Add("@IsCertified", SqlDbType.Bit).Value = isCertified
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function IsCustomerCertified(customerNumber As Long) As Boolean
        If customerNumber <= 1 Then Return False

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(
                    "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.CustomerCertification " &
                    "WHERE CustomerNumber=@CustomerNumber AND IsCertified=1) THEN 1 ELSE 0 END", conn)
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = Convert.ToInt32(customerNumber)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) = 1
                End Using
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetNextCustomerNumber(conn As SqlConnection, tr As SqlTransaction) As Integer
        Using cmd As New SqlCommand("SELECT ISNULL(MAX(CustomerNumber), 0) + 1 FROM Customer", conn, tr)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function

    Private Function IsDuplicateCustomerCode(conn As SqlConnection, tr As SqlTransaction, customerCodeText As String) As Boolean
        Using cmd As New SqlCommand("SELECT COUNT(1) FROM Customer WHERE CustomerCode = @CustomerCode", conn, tr)
            cmd.Parameters.Add("@CustomerCode", SqlDbType.NVarChar, 50).Value = customerCodeText
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Function FindDuplicateMobile(conn As SqlConnection,
                                         tr As SqlTransaction,
                                         mobileText As String,
                                         excludeCustomerNumber As Long,
                                         ByRef duplicateCustomerNumber As Long,
                                         ByRef duplicateCustomerName As String) As Boolean
        duplicateCustomerNumber = 0
        duplicateCustomerName = ""

        Dim normalizedMobile As String = NormalizeMobileNumber(mobileText)
        If normalizedMobile = "" Then Return False

        Const sql As String =
            "SELECT TOP 1 CustomerNumber, RTRIM(ISNULL(Name, N'')) AS CustomerName " &
            "FROM Customer " &
            "WHERE CustomerNumber <> @ExcludeCustomerNumber " &
            "AND NULLIF(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(Mobile, N''), N' ', N''), N'-', N''), N'(', N''), N')', N''), N'.', N''), N'+', N''), N'') = @Mobile " &
            "ORDER BY CustomerNumber"

        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@ExcludeCustomerNumber", SqlDbType.BigInt).Value = excludeCustomerNumber
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 50).Value = normalizedMobile
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If Not rd.Read() Then Return False
                duplicateCustomerNumber = Convert.ToInt64(rd("CustomerNumber"))
                duplicateCustomerName = Convert.ToString(rd("CustomerName")).Trim()
                Return True
            End Using
        End Using
    End Function

    Private Function NormalizeMobileNumber(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return ""

        Dim digits As New StringBuilder()
        For Each ch As Char In value
            If Char.IsDigit(ch) Then digits.Append(ch)
        Next
        Return digits.ToString()
    End Function

    Private Sub ShowDuplicateMobileWarning(message As String)
        Using warningForm As New Form()
            warningForm.Text = "เบอร์มือถือซ้ำ"
            warningForm.StartPosition = FormStartPosition.CenterParent
            warningForm.FormBorderStyle = FormBorderStyle.FixedDialog
            warningForm.MinimizeBox = False
            warningForm.MaximizeBox = False
            warningForm.ShowInTaskbar = False
            warningForm.ClientSize = New Size(720, 270)
            warningForm.Font = New System.Drawing.Font("Cordia New", 18.0!, FontStyle.Regular)

            Dim warningIcon As New PictureBox()
            warningIcon.Image = SystemIcons.Warning.ToBitmap()
            warningIcon.SizeMode = PictureBoxSizeMode.AutoSize
            warningIcon.Location = New Point(35, 58)

            Dim messageLabel As New Label()
            messageLabel.AutoSize = False
            messageLabel.Location = New Point(120, 38)
            messageLabel.Size = New Size(560, 125)
            messageLabel.Font = New System.Drawing.Font("Cordia New", 22.0!, FontStyle.Bold)
            messageLabel.ForeColor = Color.FromArgb(35, 35, 35)
            messageLabel.Text = message
            messageLabel.TextAlign = ContentAlignment.MiddleLeft

            Dim okButton As New Button()
            okButton.Text = "ตกลง"
            okButton.Size = New Size(130, 52)
            okButton.Location = New Point(295, 190)
            okButton.Font = New System.Drawing.Font("Cordia New", 19.0!, FontStyle.Bold)
            okButton.DialogResult = DialogResult.OK

            warningForm.AcceptButton = okButton
            warningForm.Controls.Add(warningIcon)
            warningForm.Controls.Add(messageLabel)
            warningForm.Controls.Add(okButton)
            warningForm.ShowDialog(Me)
        End Using
    End Sub

    Private Function DbValueOrNull(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then Return DBNull.Value
        Return value
    End Function

    Private Sub ClearCustomerInput()
        tbCustomerCode.Clear()
        tbName.Clear()
        tbMobile.Clear()
        tbAddress.Clear()
        tbFreeCupOpening.Text = "0"
        cbStatus.SelectedIndex = 0
        If chkCustomerCertified IsNot Nothing Then chkCustomerCertified.Checked = False
        tbCustomerCode.Focus()
    End Sub
    Private Sub dtGCustomer_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtGCustomer.CellClick
        If e.RowIndex < 0 Then Return

        LoadCustomerToEditor(dtGCustomer.Rows(e.RowIndex))
    End Sub

    Private Sub LoadCustomerToEditor(row As DataGridViewRow)
        If row Is Nothing Then Return

        SelectedCustomerName = SafeString(row.Cells("Name").Value)
        Long.TryParse(SafeString(row.Cells("CustomerNumber").Value), CustomerNumber)
        Decimal.TryParse(SafeString(row.Cells("Point").Value), PointReceive)
        CustomerCode = SafeString(row.Cells("CustomerCode").Value)

        'One click only loads the selected customer into the editor.
        tbCustomerCode.Text = CustomerCode
        tbName.Text = SelectedCustomerName
        tbMobile.Text = SafeString(row.Cells("Mobile").Value)
        tbAddress.Text = SafeString(row.Cells("Address").Value)
        tbFreeCupOpening.Text = GetCustomerAccumulatedCupBalance(CustomerNumber).ToString()
        UpdateFreeCupSummary(CustomerNumber)
        If chkCustomerCertified IsNot Nothing Then
            chkCustomerCertified.Checked = IsCustomerCertified(CustomerNumber)
        End If
    End Sub

    Private Sub dtGCustomer_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtGCustomer.CellDoubleClick
        If e.RowIndex < 0 Then Return

        'A double-click confirms the customer and returns it to fsell.
        LoadCustomerToEditor(dtGCustomer.Rows(e.RowIndex))
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub


End Class




