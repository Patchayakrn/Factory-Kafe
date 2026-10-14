Imports System.Data.SqlClient
Imports System.Net
Imports System.Text
Imports DevExpress.XtraGrid.Views.WinExplorer.ViewInfo

Public Class fCustomerBarcode
    Public Property CustomerNumber As Long = 0
    Public Property m_customerMobile As String
    Public Property m_CustomerCode As String
    Public Property SelectedCustomerName As String
    Public m_strCustomerName As String = ""
    Public m_bCanCollectPoint As Boolean = False
    Public m_bCanUsePMTBill As Boolean = True
    Public m_bCanUsePromotion As Boolean = True
    Public m_bLimitCreditMoney As Boolean = False
    Public m_dCreditMoney As Decimal = 0D
    Public m_dDiscountP As Decimal = 0D
    Public m_dPoint As Decimal = 0D
    Public m_dtMemberExpire As DateTime = DateTime.Now
    Public m_lCredit As Long = 0
    Public m_lDeliveryID As Long = 0
    Public m_lMemberLevel As Long = 0
    Public m_lPriceType As Long = 0
    Public m_strComment As String = ""
    Public m_strIncoterms As String = ""
    Public m_strModeOfShipment As String = ""
    Public m_strPortOfLoading As String = ""
    Public m_strPortOfDischarge As String = ""
    Public m_lCustomerNumber As Long = 0
    Private Sub tbBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles tbBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call GetBarcodeData()
            e.Handled = True
        End If
    End Sub
    Private Sub GetBarcodeData()
        Try
            dtG01.DataSource = Nothing
            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("Customer.CustomerNumber, RTrim(Customer.Name) AS 'Name',Customer.CustomerCode, ")
                sb.Append("RTrim(Customer.Mobile) As 'Mobile' ")
                .Selecting(sb.ToString, "Customer", "Customer.Mobile Like '%" & tbBarcode.Text & "%' ", "Customer.Name ASC")
                ' ===== เช็คผลลัพธ์ =====
                If .DTb Is Nothing OrElse .DTb.Rows.Count = 0 Then
                    MessageBox.Show(
                    "ไม่พบเบอร์โทรศัพท์ในระบบ",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                    tbBarcode.Focus()
                    tbBarcode.SelectAll()

                    Exit Sub
                End If

                ' ถ้าพบข้อมูล
                dtG01.DataSource = .DTb
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
    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        SelectedCustomerName = SafeString(gridView.GetRowCellValue(e.RowHandle, "Name"))
        CustomerNumber = SafeString(GridView1.GetRowCellValue(e.RowHandle, "CustomerNumber"))
        m_customerMobile = SafeString(GridView1.GetRowCellValue(e.RowHandle, "Mobile"))
        m_CustomerCode = SafeString(GridView1.GetRowCellValue(e.RowHandle, "CustomerCode"))
        Call ReadCustomerData(CustomerNumber)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Private Sub ReadCustomerData(CustomerNumber As Long)
        Try
            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("RTRIM(Customer.Name) AS Name, ")
                sb.Append("Customer.Credit, Customer.DiscountP, Customer.MemberLevel, Customer.MemberExpire, ")
                sb.Append("Customer.PriceType, Customer.DeliveryID, Customer.LimitCreditMoney, Customer.CreditMoney, ")
                sb.Append("Customer.CanUsePromotion, Customer.CanUsePMTBill, Customer.CanCollectPoint, Customer.Point, ")
                sb.Append("RTRIM(Customer.Incoterms) AS Incoterms, ")
                sb.Append("RTRIM(Customer.ModeOfShipment) AS ModeOfShipment, ")
                sb.Append("RTRIM(Customer.PortOfLoading) AS PortOfLoading, ")
                sb.Append("RTRIM(Customer.PortOfDischarge) AS PortOfDischarge, ")
                sb.Append("RTRIM(Customer.Comment) AS Comment ,Customer.Mobile ")
                .Selecting(sb.ToString, "Customer", "Customer.CustomerNumber = '" & CustomerNumber & "'", "Name")
                m_strCustomerName = .DTb.Rows(0)("Name").ToString().TrimEnd()
                m_lCredit = CLng(.DTb.Rows(0)("Credit"))
                m_dDiscountP = CDec(.DTb.Rows(0)("DiscountP"))
                m_lMemberLevel = CLng(.DTb.Rows(0)("MemberLevel"))
                m_dtMemberExpire = CDate(.DTb.Rows(0)("MemberExpire"))
                m_lPriceType = CLng(.DTb.Rows(0)("PriceType"))
                m_customerMobile = CStr(.DTb.Rows(0)("Mobile"))

                If Not IsDBNull(.DTb.Rows(0)("DeliveryID")) Then
                    m_lDeliveryID = CLng(.DTb.Rows(0)("DeliveryID"))
                End If

                m_bLimitCreditMoney = (CInt(.DTb.Rows(0)("LimitCreditMoney")) <> 0)
                m_dCreditMoney = CDec(.DTb.Rows(0)("CreditMoney"))
                m_bCanUsePromotion = (CInt(.DTb.Rows(0)("CanUsePromotion")) <> 0)
                m_bCanUsePMTBill = IsDBNull(.DTb.Rows(0)("CanUsePMTBill"))
                m_bCanCollectPoint = (CInt(.DTb.Rows(0)("CanCollectPoint")) <> 0)
                m_dPoint = CDec(.DTb.Rows(0)("Point"))
                m_strIncoterms = If(IsDBNull(.DTb.Rows(0)("Incoterms")), "", .DTb.Rows(0)("Incoterms").ToString().TrimEnd())
                m_strModeOfShipment = If(IsDBNull(.DTb.Rows(0)("ModeOfShipment")), "", .DTb.Rows(0)("ModeOfShipment").ToString().TrimEnd())
                m_strPortOfLoading = If(IsDBNull(.DTb.Rows(0)("PortOfLoading")), "", .DTb.Rows(0)("PortOfLoading").ToString().TrimEnd())
                m_strPortOfDischarge = If(IsDBNull(.DTb.Rows(0)("PortOfDischarge")), "", .DTb.Rows(0)("PortOfDischarge").ToString().TrimEnd())
                m_strComment = If(IsDBNull(.DTb.Rows(0)("Comment")), "", .DTb.Rows(0)("Comment").ToString().TrimEnd())

            End With
        Catch ex As Exception

        End Try

    End Sub
End Class