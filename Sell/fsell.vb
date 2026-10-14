Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.ComponentModel
Imports Coffee_SMH2.frmReceiptPreview
Public Class fsell

    Private Const CocoaSignaturePrice As Decimal = 45D
    Private customerId As Integer = 0
    Dim ordinal5 As Integer
    Dim ordinal10 As Decimal
    Private dtSell As DataTable
    Dim OfficerID As Integer
    Dim OfficerName As String
    '===== เก็บเลขบิลก่อนแก้ไข =====
    Public m_strBillNumberBeforeEdit As String = ""
    '===== ตั้งค่าการเช็คเลขซ้ำ (1 = เช็ค, 0 = ไม่เช็ค) =====
    Public iSellCheckDuplicateInvoice As Integer = 1
    Public lCustomerNumber As Long = 0
    Dim cbDelivery As String
    Public m_bLimitCreditMoney As Boolean = False
    Public m_dCreditMoney As Decimal = 0D
    Public m_dCustomerPoint As Decimal = 0D
    Public m_dCustomerDiscountP As Decimal = 0D
    Public m_dBranchDiscountP As Decimal = 0D
    Public m_lMemberLevel As Long = 0
    Public m_dPointReceive As Decimal = 0D
    Public m_dtServerDateTime As DateTime = DateTime.Now
    Public m_bCanCollectPoint As Boolean = False
    Public m_bCanUsePMTBill As Boolean = True
    Public m_bCanUsePromotion As Boolean = True
    Public m_bOffLineMode As Boolean = False
    Public iDefaultPriceType As Integer = 0
    Private m_bCanUseProductPrice2 As Boolean = False
    Private m_bCanUseProductPrice3 As Boolean = False
    Private m_bCanUseProductPrice4 As Boolean = False
    Private m_bCanUseProductPrice5 As Boolean = False
    Private m_bCheckCanUsePriceType As Boolean = True
    Private m_bAskForChangePriceType As Boolean = True
    Public lSalesOrderID As Long = 0
    Public m_dDiscountMoney As Decimal = 0D
    Public iMode As Integer = 0
    Dim tbIncoterms As String
    Dim tbModeOfShipment As String
    Dim tbPortOfLoading As String
    Dim tbPortOfDischarge As String
    Dim tbDCPercent As String
    Dim payType As Integer
    Public m_dAdjustMoney As Decimal = 0D
    Dim num12 As Decimal
    Public lLoginONumber As Long = 0
    Public m_lCreateONumber As Long = 0
    Dim flag1 As Boolean = True
    Dim num10 As Decimal = 0
    Dim num1 As Long = 0
    Dim num9 As Long = 0
    Dim ErrorMessage1 As String = ""
    Dim profit As Decimal = 0
    Dim profitVat As Decimal = 0
    Public customerMobile As String
    Public customerCode As String
    Dim newBalance As Decimal
    Dim lUnit As String
    Private customerDisplay As frmCustomerDisplay
    Dim discountbath As Decimal
    Private selectedPromotionName As String = ""
    Private customerSelectionConfirmed As Boolean = False

    Private Class BeverageMenuItem
        Public Property PN As Long
        Public Property ProductCode As String
        Public Property Barcode As String
        Public Property ProductName As String
        Public Property SUnit As String
        Public Property SellPrice As Decimal
        Public Property ProductComment As String
        Public Property PicName As String
        Public Property ImageGroup As String
        Public Property ProductGroupName As String
        Public Property HasHot As Boolean
        Public Property HasIced As Boolean
        Public Property HasBlended As Boolean
        Public Property HotPrice As Decimal
        Public Property IcedPrice As Decimal
        Public Property BlendedPrice As Decimal
        Public Property SignaturePrice As Decimal
        Public Property HotProductNumber As Long
        Public Property IcedProductNumber As Long
        Public Property BlendedProductNumber As Long
        Public Property SignatureProductNumber As Long
        Public Property HotProductCode As String
        Public Property IcedProductCode As String
        Public Property BlendedProductCode As String
        Public Property SignatureProductCode As String
        Public Property HotBarcode As String
        Public Property IcedBarcode As String
        Public Property BlendedBarcode As String
        Public Property SignatureBarcode As String
    End Class

    Private Class ToppingProductInfo
        Public Property PN As Long
        Public Property ProductCode As String
        Public Property Barcode As String
        Public Property ProductName As String
        Public Property SUnit As String
        Public Property Price As Decimal
    End Class

    'สถานะรับรองลูกค้าอ่านจากตาราง CustomerCertification
    Private customerIsCertified As Boolean = False
    Private miFreeCupCustomerSummary As ToolStripMenuItem
    Private qtyValidationInProgress As Boolean
    Private Const FreeCupTargetQty As Decimal = 10D
    Private Const FreeCupMinimumPrice As Decimal = 30D
    Private pnlFreeCup As Panel
    Private lblFreeCupTitle As Label
    Private lblFreeCupSubTitle As Label
    Private btnChooseFreeCup As Button
    Private useFreeCupInCurrentBill As Boolean
    Private freeCupDiscountApplied As Decimal = 0D
    Private freeCupUseQty As Integer = 0
    Private freeCupCheckboxUpdating As Boolean
    Public Property CustomerDisplayForm As frmCustomerDisplay

    Private Sub fsell_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        SetSummaryNumericAlignment()
        If txtTotalMoney IsNot Nothing Then
            txtTotalMoney.TextAlign = HorizontalAlignment.Center
            txtTotalMoney.ForeColor = Color.Purple
        End If
    End Sub

    Private Sub fsell_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        CloseCustomerDisplay()
    End Sub

    Private Sub fsell_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If LicenseManager.UsageMode = LicenseUsageMode.Designtime OrElse Me.DesignMode Then Return

        SetupResponsiveBillingPanels()
        tbAutoInput.CueHint.CueHintText = "สแกนบาร์โค๊ด / เพิ่มสินค้าด้วยรหัส"
        cbCompany.SelectedIndex = 0
        cbVatType.SelectedIndex = 1
        cbHaveTax.SelectedIndex = 1
        cbPriceType.SelectedIndex = 0

        EnsureSellPromotionDetailColumn()
        EnsureSellCustomerCertifiedTable()
        EnsureCustomerFreeCupTable()
        AddHandler pnlHeader2.Paint, AddressOf pnlHeader_Paint
        KryptonDataGridView1.ScrollBars = ScrollBars.Both
        customerId = 0
        lCustomerNumber = 0
        lbCustomerName.Text = ""
        lbCustomerHead.Values.Text = "กรุณาเลือกประเภทลูกค้า"
        SetCustomerSelectionState(False)

        Call ShowdatainGrid()
        KryptonDataGridView1.ContextMenuStrip = cmsGrid
        LoadBeverageMenu()

        If Not String.IsNullOrEmpty(ModSession.LoginUserName) Then
            Me.Text = "Welcome  " & ModSession.LoginName
            OfficerID = ModSession.LoginOfficerID
            OfficerName = ModSession.LoginName
        End If

        iMode = 1

        OpenCustomerDisplay()

    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        'Wait until WinForms finishes changing SelectedTab, then reload the
        'panel belonging to the newly selected tab.
        If IsHandleCreated Then
            BeginInvoke(New MethodInvoker(AddressOf LoadBeverageMenu))
        Else
            LoadBeverageMenu()
        End If
    End Sub

    Private ReadOnly beverageMenuPanels As New Dictionary(Of TabPage, FlowLayoutPanel)()

    Private Function GetBeverageMenuPanel(tabPage As TabPage) As FlowLayoutPanel
        If tabPage Is TabPage9 Then Return FlowLayoutPanel1
        If beverageMenuPanels.ContainsKey(tabPage) Then Return beverageMenuPanels(tabPage)

        Dim panel As New FlowLayoutPanel With {
            .Name = "flpMenu_" & tabPage.Name,
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .WrapContents = True,
            .FlowDirection = FlowDirection.LeftToRight,
            .Padding = New Padding(6),
            .BackColor = Color.White
        }
        tabPage.Controls.Add(panel)
        panel.BringToFront()
        beverageMenuPanels(tabPage) = panel
        Return panel
    End Function

    Private Sub LoadBeverageMenu()
        If FlowLayoutPanel1 Is Nothing OrElse TabControl2 Is Nothing OrElse TabControl2.SelectedTab Is Nothing Then Return

        Dim menuPanel As FlowLayoutPanel = GetBeverageMenuPanel(TabControl2.SelectedTab)

        menuPanel.AutoScroll = True
        menuPanel.WrapContents = True
        menuPanel.FlowDirection = FlowDirection.LeftToRight

        Dim selectedText As String = GetBeverageTabName(TabControl2.SelectedTab)
        Dim filterText As String = If(selectedText = "ทั้งหมด", "", selectedText)

        menuPanel.SuspendLayout()
        Try
            For Each oldCard As Control In menuPanel.Controls
                Dim card = TryCast(oldCard, DrinkMenuCard)
                If card IsNot Nothing AndAlso card.PictureBox1.Image IsNot Nothing Then card.PictureBox1.Image.Dispose()
            Next
            menuPanel.Controls.Clear()

            Dim groupImageIndexes As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            Dim groupedItems As New Dictionary(Of String, BeverageMenuItem)(StringComparer.OrdinalIgnoreCase)

            Dim sql As String =
                "SELECT Product.PN, RTRIM(Product.ProductCode) AS ProductCode, " &
                "RTRIM(Product.Barcode) AS Barcode, RTRIM(Product.Name) AS ProductName, " &
                "Product.Price1 AS SellPrice, RTRIM(Product.SUnit) AS SUnit, " &
                "RTRIM(Product.PicName) AS PicName, RTRIM(Product.Comment) AS ProductComment, " &
                "RTRIM(Category.Name) AS CategoryName, RTRIM(ProductGroup.Name) AS GroupName " &
                "FROM Product LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID " &
                "LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID " &
                "WHERE Product.Status IS NULL AND (Category.ID = 5 OR Category.Name LIKE N'%เครื่องดื่ม%' " &
                "OR ProductGroup.Name LIKE N'%เครื่องดื่ม%' OR Product.SUnit LIKE N'%แก้ว%') " &
                "ORDER BY CASE RTRIM(ProductGroup.Name) " &
                "WHEN N'กาแฟ' THEN 1 WHEN N'ชา' THEN 2 WHEN N'นม' THEN 3 " &
                "WHEN N'โซดา' THEN 4 WHEN N'สมูทตี้' THEN 5 WHEN N'มัทฉะ' THEN 6 " &
                "WHEN N'อื่นๆ' THEN 7 ELSE 99 END, " &
                "RTRIM(Category.Name), Product.ProductCode"

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim categoryName As String = If(reader.IsDBNull(reader.GetOrdinal("CategoryName")), "", reader("CategoryName").ToString().Trim())
                            Dim groupName As String = If(reader.IsDBNull(reader.GetOrdinal("GroupName")), "", reader("GroupName").ToString().Trim())
                            Dim productNameValue As String = Convert.ToString(reader("ProductName")).Trim()
                            Dim imageGroup As String = GetMenuFolderName(selectedText, categoryName, groupName, productNameValue)
                            'For category tabs, filter by the actual Product Group
                            'only.  Do not let Category or product-name guessing
                            'put coffee items into the tea tab (or vice versa).
                            If filterText <> "" AndAlso Not groupName.Equals(filterText, StringComparison.OrdinalIgnoreCase) Then Continue While
                            If filterText = "ชา" AndAlso IsCoffeeMenuName(productNameValue) Then Continue While

                            Dim menuName As String = NormalizeBeverageMenuName(productNameValue)
                            Dim sellPriceValue As Decimal = If(reader.IsDBNull(reader.GetOrdinal("SellPrice")), 0D, Convert.ToDecimal(reader("SellPrice")))
                            Dim hasHot As Boolean = productNameValue.IndexOf("ร้อน", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                    productNameValue.IndexOf("hot", StringComparison.OrdinalIgnoreCase) >= 0
                            Dim hasExplicitIced As Boolean = productNameValue.IndexOf("เย็น", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                              productNameValue.IndexOf("iced", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                              productNameValue.IndexOf("ice", StringComparison.OrdinalIgnoreCase) >= 0
                            Dim hasBlended As Boolean = productNameValue.IndexOf("ปั่น", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                        productNameValue.IndexOf("blend", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                        productNameValue.IndexOf("frappe", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                                        groupName.Equals("สมูทตี้", StringComparison.OrdinalIgnoreCase)
                            'รองรับทั้งชื่อภาษาไทยและชื่อภาษาอังกฤษที่มีการสะกดผิดในข้อมูลเดิม
                            Dim compactProductName As String = System.Text.RegularExpressions.Regex.Replace(productNameValue, "\s+", "")
                            Dim hasSignature As Boolean = IsSignatureProductName(compactProductName)
                            'ถ้าไม่มีคำบอกประเภท ให้ถือเป็นเมนูเย็นเป็นค่าเริ่มต้น
                            Dim hasIced As Boolean = Not groupName.Equals("สมูทตี้", StringComparison.OrdinalIgnoreCase) AndAlso
                                                     (hasExplicitIced OrElse (Not hasHot AndAlso Not hasBlended AndAlso Not hasSignature))
                            'Use the normalized menu name as the key in every
                            'tab.  The All tab must not show duplicate cards
                            'when the same menu exists in multiple groups.
                            Dim groupKey As String = menuName
                            Dim existingItem As BeverageMenuItem = Nothing
                            If Not groupedItems.TryGetValue(groupKey, existingItem) Then
                                groupedItems(groupKey) = New BeverageMenuItem With {
                                    .PN = Convert.ToInt64(reader("PN")),
                                    .ProductCode = Convert.ToString(reader("ProductCode")).Trim(),
                                    .Barcode = Convert.ToString(reader("Barcode")).Trim(),
                                    .ProductName = menuName,
                                    .SUnit = Convert.ToString(reader("SUnit")).Trim(),
                                    .SellPrice = sellPriceValue,
                                    .ProductComment = Convert.ToString(reader("ProductComment")),
                                    .PicName = Convert.ToString(reader("PicName")).Trim(),
                                    .ImageGroup = imageGroup,
                                    .ProductGroupName = groupName,
                                    .HasHot = hasHot,
                                    .HasIced = hasIced,
                                    .HasBlended = hasBlended,
                                    .HotPrice = If(hasHot, sellPriceValue, 0D),
                                    .IcedPrice = If(hasIced, sellPriceValue, 0D),
                                    .BlendedPrice = If(hasBlended, sellPriceValue, 0D),
                                    .SignaturePrice = If(hasSignature, sellPriceValue, 0D)
                                }
                            ElseIf String.IsNullOrWhiteSpace(existingItem.PicName) AndAlso
                                   Not String.IsNullOrWhiteSpace(Convert.ToString(reader("PicName")).Trim()) Then
                                existingItem.PicName = Convert.ToString(reader("PicName")).Trim()
                                existingItem.PN = Convert.ToInt64(reader("PN"))
                                existingItem.ProductCode = Convert.ToString(reader("ProductCode")).Trim()
                                existingItem.Barcode = Convert.ToString(reader("Barcode")).Trim()
                            End If

                            If existingItem IsNot Nothing Then
                                'เลือกสินค้าประเภทเย็นเป็นรายการหลักของการ์ด
                                If hasIced AndAlso existingItem.IcedPrice <= 0D Then
                                    existingItem.PN = Convert.ToInt64(reader("PN"))
                                    existingItem.ProductCode = Convert.ToString(reader("ProductCode")).Trim()
                                    existingItem.Barcode = Convert.ToString(reader("Barcode")).Trim()
                                    existingItem.SellPrice = sellPriceValue
                                End If
                                existingItem.HasHot = existingItem.HasHot OrElse hasHot
                                existingItem.HasIced = existingItem.HasIced OrElse hasIced
                                existingItem.HasBlended = existingItem.HasBlended OrElse hasBlended
                                If hasSignature Then existingItem.SignaturePrice = sellPriceValue
                                If hasHot Then existingItem.HotPrice = sellPriceValue
                                If hasIced Then existingItem.IcedPrice = sellPriceValue
                                If hasBlended Then existingItem.BlendedPrice = sellPriceValue
                            End If

                            'เก็บ PN/รหัส/Barcode ของแต่ละชนิดไว้กับการ์ด
                            'เพื่อไม่ให้การเลือก "ปั่น" ย้อนกลับไปใช้ PN ของเมนูเย็น
                            If existingItem Is Nothing Then existingItem = groupedItems(groupKey)
                            Dim rowPn As Long = Convert.ToInt64(reader("PN"))
                            Dim rowCode As String = Convert.ToString(reader("ProductCode")).Trim()
                            Dim rowBarcode As String = Convert.ToString(reader("Barcode")).Trim()
                            If hasHot Then
                                existingItem.HotProductNumber = rowPn
                                existingItem.HotProductCode = rowCode
                                existingItem.HotBarcode = rowBarcode
                            End If
                            If hasIced Then
                                existingItem.IcedProductNumber = rowPn
                                existingItem.IcedProductCode = rowCode
                                existingItem.IcedBarcode = rowBarcode
                            End If
                            If hasBlended Then
                                existingItem.BlendedProductNumber = rowPn
                                existingItem.BlendedProductCode = rowCode
                                existingItem.BlendedBarcode = rowBarcode
                            End If
                            If hasSignature Then
                                existingItem.SignatureProductNumber = rowPn
                                existingItem.SignatureProductCode = rowCode
                                existingItem.SignatureBarcode = rowBarcode
                            End If
                        End While
                    End Using
                End Using
            End Using

            For Each item As BeverageMenuItem In groupedItems.Values
                If Not groupImageIndexes.ContainsKey(item.ImageGroup) Then groupImageIndexes(item.ImageGroup) = 0
                Dim imageIndex As Integer = groupImageIndexes(item.ImageGroup)
                groupImageIndexes(item.ImageGroup) += 1

                Dim card As New DrinkMenuCard With {
                    .AutoSize = False,
                    .Size = New Size(155, 110),
                    .MinimumSize = New Size(155, 110),
                    .MaximumSize = New Size(155, 110),
                    .Margin = New Padding(4)
                }
                card.SetProduct(item.PN, item.ProductCode, item.Barcode, item.ProductName,
                                item.SUnit, item.SellPrice, item.ProductComment,
                                LoadProductImage(item.PicName, item.ImageGroup, imageIndex))
                card.HasHot = item.HasHot
                card.HasIced = item.HasIced
                card.HasBlended = item.HasBlended
                card.HotPrice = item.HotPrice
                card.IcedPrice = item.IcedPrice
                card.BlendedPrice = item.BlendedPrice
                card.SignaturePrice = item.SignaturePrice
                card.HotProductNumber = item.HotProductNumber
                card.IcedProductNumber = item.IcedProductNumber
                card.BlendedProductNumber = item.BlendedProductNumber
                card.SignatureProductNumber = item.SignatureProductNumber
                card.HotProductCode = item.HotProductCode
                card.IcedProductCode = item.IcedProductCode
                card.BlendedProductCode = item.BlendedProductCode
                card.SignatureProductCode = item.SignatureProductCode
                card.HotBarcode = item.HotBarcode
                card.IcedBarcode = item.IcedBarcode
                card.BlendedBarcode = item.BlendedBarcode
                 card.SignatureBarcode = item.SignatureBarcode
                 card.ProductGroupName = item.ProductGroupName
                 card.SkipBeverageOption = String.Equals(item.ProductGroupName.Trim(), "อื่นๆ", StringComparison.OrdinalIgnoreCase)
                 card.SetSelectionVisualState(customerSelectionConfirmed)
                 AddHandler card.CardClicked, AddressOf BeverageCard_CardClicked
                 menuPanel.Controls.Add(card)
            Next
        Catch ex As Exception
            MessageBox.Show("โหลดเมนูเครื่องดื่มไม่สำเร็จ: " & ex.Message, "เมนูเครื่องดื่ม", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            menuPanel.ResumeLayout(True)
        End Try
    End Sub

    Private Function GetBeverageTabName(tabPage As TabPage) As String
        If tabPage Is Nothing Then Return "ทั้งหมด"

        Select Case tabPage.Name
            Case "TabPage9" : Return "ทั้งหมด"
            Case "TabPage3" : Return "กาแฟ"
            Case "TabPage4" : Return "ชา"
            Case "TabPage5" : Return "นม"
            Case "TabPage6" : Return "โซดา"
            Case "TabPage7" : Return "สมูทตี้"
            Case "TabPage8" : Return "มัทฉะ"
            Case "TabPage10" : Return "อื่นๆ"
        End Select

        Return tabPage.Text.Trim()
    End Function

    Private Function NormalizeBeverageMenuName(productName As String) As String
        Dim result As String = If(productName, "").Trim()
        If result = "" Then Return productName.Trim()

        'Remove recipe suffixes such as "สูตร1" before grouping.
        result = System.Text.RegularExpressions.Regex.Replace(result, "\s*สูตร\s*\d+\s*$", "").Trim()

        'Treat every cocoa variant as one base menu name, including names
        'such as "โกโก้ช็อกแลต สูตร1".
        Dim compactResult As String = System.Text.RegularExpressions.Regex.Replace(result, "\s+", "")
        If compactResult.StartsWith("โกโก้", StringComparison.OrdinalIgnoreCase) Then
            Return "โกโก้"
        End If

        'Remove hot/iced/blended suffixes, including optional spaces or a
        'dash before the suffix (for example: "โกโก้ - เย็น").
        Do
            Dim updated As String = System.Text.RegularExpressions.Regex.Replace(
                result, "\s*[-–—]?\s*(ร้อน|เย็น|ปั่น)\s*$", "")
            If updated = result Then Exit Do
            result = updated.Trim()
        Loop

        Return If(result = "", productName.Trim(), result)
    End Function

    Private Function IsSignatureProductName(productName As String) As Boolean
        Dim name As String = If(productName, "").Trim()
        Return name.IndexOf("ซิกเนเจอร์", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               name.IndexOf("ชิกเนเจอร์", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               name.IndexOf("signature", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               name.IndexOf("singnature", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               name.IndexOf("signnature", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Function IsCoffeeMenuName(productName As String) As Boolean
        Dim name As String = If(productName, "").Trim()
        Dim coffeeWords As String() = {"เอสเพรสโซ", "เอสเปรสโซ", "อเมริกาโน", "ลาเต้", "มอคค่า", "คาปูชิโน", "กาแฟ"}

        For Each word As String In coffeeWords
            If name.Contains(word) Then Return True
        Next

        Return False
    End Function

    Private Function GetMenuFolderName(selectedText As String, categoryName As String, groupName As String, productName As String) As String
        'Use the product group as the menu tab.  Category may be a parent or
        'different classification, so it must not override Group Product.
        'For example, a product named "เพิ่มช็อตกาแฟ" can still belong to
        '"อื่นๆ", while a product whose Group Product is "กาแฟ" must appear
        'in the coffee tab even when its Category is different.
        If Not String.IsNullOrWhiteSpace(groupName) Then Return groupName.Trim()
        If Not String.IsNullOrWhiteSpace(categoryName) Then Return categoryName.Trim()

        Dim knownFolders As String() = {"กาแฟ", "ชา", "นม", "มัทฉะ", "สมูทตี้", "โซดา"}

        For Each folder As String In knownFolders
            If categoryName.Contains(folder) OrElse groupName.Contains(folder) OrElse productName.Contains(folder) Then Return folder
        Next

        If productName.Contains("เอสเพรสโซ") OrElse productName.Contains("อเมริกาโน") OrElse productName.Contains("ลาเต้") OrElse productName.Contains("คาปูชิโน") OrElse productName.Contains("มอคค่า") Then Return "กาแฟ"
        If productName.Contains("ชานม") OrElse productName.Contains("ชามะนาว") Then Return "ชา"
        If productName.Contains("นม") Then Return "นม"
        If productName.Contains("โซดา") Then Return "โซดา"
        Return groupName
    End Function

    Private Function LoadProductImage(imagePath As String, groupName As String, imageIndex As Integer) As Image
        Dim resolvedPath As String = ResolveProductImagePath(imagePath, groupName, imageIndex)
        If String.IsNullOrWhiteSpace(resolvedPath) OrElse Not IO.File.Exists(resolvedPath) Then Return Nothing
        Try
            Using source As Image = Image.FromFile(resolvedPath)
                Return New Bitmap(source)
            End Using
        Catch
            Return Nothing
        End Try
    End Function

    Private Function ResolveProductImagePath(imagePath As String, groupName As String, imageIndex As Integer) As String
        Dim rawPath As String = If(imagePath, "").Trim().Trim(ChrW(34))
        If IO.File.Exists(rawPath) Then Return rawPath

        Dim fileName As String = If(String.IsNullOrWhiteSpace(rawPath), "", IO.Path.GetFileName(rawPath))
        If String.IsNullOrWhiteSpace(fileName) Then Return ""

        Dim menuRoot As String = GetMenuRootPath()
        Dim groupFolder As String = IO.Path.Combine(menuRoot, If(groupName, "").Trim())
        Dim desktopMenuRoot As String = IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Menu")
        Dim desktopGroupFolder As String = IO.Path.Combine(desktopMenuRoot, If(groupName, "").Trim())

        Dim menuCandidates As String() = {
            IO.Path.Combine(groupFolder, fileName),
            IO.Path.Combine(menuRoot, fileName),
            IO.Path.Combine(desktopGroupFolder, fileName),
            IO.Path.Combine(desktopMenuRoot, fileName)
        }
        For Each candidate As String In menuCandidates
            If IO.File.Exists(candidate) Then Return candidate
        Next

        'ค้นหาในโฟลเดอร์หมวดอื่นด้วย กรณีชื่อไฟล์ตรงแต่หมวดสินค้าไม่ตรงกัน
        If IO.Directory.Exists(menuRoot) Then
            Try
                For Each candidate As String In IO.Directory.GetFiles(menuRoot, fileName, IO.SearchOption.AllDirectories)
                    If String.Equals(IO.Path.GetFileName(candidate), fileName, StringComparison.OrdinalIgnoreCase) Then
                        Return candidate
                    End If
                Next
            Catch ex As UnauthorizedAccessException
                'ไม่มีสิทธิ์อ่านบางโฟลเดอร์ ให้ตรวจ path อื่นต่อ
            Catch ex As IO.IOException
                'ข้ามกรณีไฟล์หรือโฟลเดอร์ใช้งานอยู่
            End Try
        End If

        If IO.Directory.Exists(desktopMenuRoot) Then
            Try
                For Each candidate As String In IO.Directory.GetFiles(desktopMenuRoot, fileName, IO.SearchOption.AllDirectories)
                    If String.Equals(IO.Path.GetFileName(candidate), fileName, StringComparison.OrdinalIgnoreCase) Then
                        Return candidate
                    End If
                Next
            Catch ex As UnauthorizedAccessException
                'ไม่มีสิทธิ์อ่านบางโฟลเดอร์ ให้ตรวจ path อื่นต่อ
            Catch ex As IO.IOException
                'ข้ามกรณีไฟล์หรือโฟลเดอร์ใช้งานอยู่
            End Try
        End If

        Dim candidates As String() = {
            IO.Path.Combine(Application.StartupPath, fileName),
            IO.Path.Combine(Application.StartupPath, "Image", fileName),
            IO.Path.Combine(Application.StartupPath, "Image", "Products", fileName),
            IO.Path.Combine(Application.StartupPath, "Images", fileName),
            IO.Path.Combine(Application.StartupPath, "Images", "Products", fileName),
            IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName),
            IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "Products", fileName)
        }

        For Each candidate As String In candidates
            If IO.File.Exists(candidate) Then Return candidate
        Next

        Return ""
    End Function

    Private Function GetMenuRootPath() As String
        Return ImageAssetStore.GetMenuRootPath()
    End Function

    Private Sub BeverageCard_CardClicked(sender As Object, e As EventArgs)
        If Not EnsureCustomerSelected() Then Return
        Dim card As DrinkMenuCard = TryCast(sender, DrinkMenuCard)
        If card Is Nothing Then Return

        If card.SkipBeverageOption Then
            AddOrIncreaseRow(card.ProductNumber,
                             card.ProductCode,
                             card.Barcode,
                             card.MenuProductName,
                             1D,
                             card.UnitName,
                             card.SellPrice,
                             0D,
                             0D,
                             0D,
                             0D,
                             "")
            Return
        End If

        Using optionForm As New FrmBeverageOption()
            optionForm.txtNameMenu.Text = card.MenuProductName
            optionForm.txtPrice.Text = card.SellPrice.ToString("N2")
            optionForm.txtNum.Text = "x 1 แก้ว"
            optionForm.HotPrice = card.HotPrice
            optionForm.IcedPrice = card.IcedPrice
            optionForm.BlendedPrice = card.BlendedPrice
            optionForm.SignaturePrice = If(card.MenuProductName.IndexOf("โกโก้", StringComparison.OrdinalIgnoreCase) >= 0,
                                           CocoaSignaturePrice,
                                           If(card.SignaturePrice > 0D,
                                              card.SignaturePrice,
                                              FindCocoaSignaturePrice()))
            optionForm.EnableSignature = card.MenuProductName.IndexOf("โกโก้", StringComparison.OrdinalIgnoreCase) >= 0
            optionForm.ForceBlended = String.Equals(card.ProductGroupName.Trim(), "สมูทตี้", StringComparison.OrdinalIgnoreCase)
            If optionForm.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim displayName As String = card.MenuProductName & " (" & optionForm.SelectedType & ", " & optionForm.SelectedSweetness & ")"

            Dim selectedPn As Long = card.ProductNumber
            Dim selectedCode As String = card.ProductCode
            Dim selectedBarcode As String = card.Barcode
            Select Case optionForm.SelectedType
                Case "ร้อน"
                    If card.HotProductNumber > 0 Then selectedPn = card.HotProductNumber : selectedCode = card.HotProductCode : selectedBarcode = card.HotBarcode
                Case "เย็น"
                    If card.IcedProductNumber > 0 Then selectedPn = card.IcedProductNumber : selectedCode = card.IcedProductCode : selectedBarcode = card.IcedBarcode
                Case "ปั่น"
                    If card.BlendedProductNumber > 0 Then selectedPn = card.BlendedProductNumber : selectedCode = card.BlendedProductCode : selectedBarcode = card.BlendedBarcode
                Case "ซิกเนเจอร์"
                    If card.SignatureProductNumber > 0 Then selectedPn = card.SignatureProductNumber : selectedCode = card.SignatureProductCode : selectedBarcode = card.SignatureBarcode
            End Select

            AddOrIncreaseRow(selectedPn,
                             selectedCode,
                             selectedBarcode,
                             displayName,
                             1D,
                             card.UnitName,
                             optionForm.SelectedPrice,
                             0D,
                             0D,
                             0D,
                             0D,
                             optionForm.SelectedType)

            'ท็อปปิ้งเป็นสินค้าแยกบรรทัด ต้องใช้ PN ของท็อปปิ้งแต่ละรายการ
            For Each toppingName As String In optionForm.SelectedToppings
                Dim toppingLookupName As String = NormalizeToppingLookupName(toppingName)
                Dim toppingPn As Long = ResolveProductNumber("", "", toppingLookupName)
                If toppingPn <= 0 Then
                    MessageBox.Show("ไม่พบรหัสสินค้า (PN) ของท็อปปิ้ง: " & toppingName,
                                    "เพิ่มท็อปปิ้งไม่สำเร็จ",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)
                    Continue For
                End If

                Dim topping As ToppingProductInfo = GetToppingProductInfo(toppingPn, toppingName)
                If topping Is Nothing Then Continue For

                AddOrIncreaseRow(topping.PN,
                                 topping.ProductCode,
                                 topping.Barcode,
                                 topping.ProductName,
                                 1D,
                                 topping.SUnit,
                                 topping.Price,
                                 0D,
                                 0D,
                                 0D,
                                 0D,
                                 "ท็อปปิ้ง")
            Next
        End Using
    End Sub

    Private Function GetToppingProductInfo(pn As Long, fallbackName As String) As ToppingProductInfo
        Try
            Dim sql As String =
                "SELECT TOP 1 PN, RTRIM(ProductCode) AS ProductCode, " &
                "RTRIM(Barcode) AS Barcode, RTRIM(Name) AS ProductName, " &
                "RTRIM(SUnit) AS SUnit, ISNULL(Price1, 0) AS Price " &
                "FROM Product WHERE PN = @PN"

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New ToppingProductInfo With {
                                .PN = Convert.ToInt64(reader("PN")),
                                .ProductCode = Convert.ToString(reader("ProductCode")).Trim(),
                                .Barcode = Convert.ToString(reader("Barcode")).Trim(),
                                .ProductName = Convert.ToString(reader("ProductName")).Trim(),
                                .SUnit = Convert.ToString(reader("SUnit")).Trim(),
                                .Price = If(reader.IsDBNull(reader.GetOrdinal("Price")), 0D, Convert.ToDecimal(reader("Price")))
                            }
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลท็อปปิ้งไม่สำเร็จ: " & ex.Message,
                            "ท็อปปิ้ง",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
        End Try

        Return Nothing
    End Function

    Private Function FindCocoaSignaturePrice() As Decimal
        Try
            Dim sql As String =
                "SELECT TOP 1 ISNULL(Price1, 0) " &
                "FROM Product " &
                "WHERE Name LIKE N'%โกโก้%' " &
                "AND (Name LIKE N'%ซิกเนเจอร์%' OR Name LIKE N'%ชิกเนเจอร์%' " &
                "OR Name LIKE N'%signature%' OR Name LIKE N'%singnature%' " &
                "OR Name LIKE N'%signnature%') " &
                "ORDER BY PN"

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Return Convert.ToDecimal(value)
                    End If
                End Using
            End Using
        Catch
            'ถ้าค้นหาไม่สำเร็จ ให้ปุ่มถูกปิดไว้เพื่อไม่ให้เพิ่มรายการราคาผิด
        End Try

        Return 0D
    End Function

    Private Sub SetupResponsiveBillingPanels()
        Panel1.SuspendLayout()
        KryptonPanel1.SuspendLayout()
        chkFreecup.SuspendLayout()

        'แบ่งพื้นที่รายการสินค้าและสรุปยอดด้วย Layout เพื่อไม่ให้ Panel ซ้อนกัน
        Dim billingSections As New TableLayoutPanel With {
            .ColumnCount = 1,
            .RowCount = 3,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .Padding = New Padding(0),
            .BackColor = Color.White
        }
        billingSections.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        billingSections.RowStyles.Add(New RowStyle(SizeType.Percent, 36.0!))
        billingSections.RowStyles.Add(New RowStyle(SizeType.Absolute, 58.0!))
        billingSections.RowStyles.Add(New RowStyle(SizeType.Percent, 64.0!))

        Panel1.Controls.Clear()
        รายการสินค้าในบิล.Dock = DockStyle.Fill
        รายการสินค้าในบิล.Margin = New Padding(0, 0, 0, 4)
        KryptonPanel1.Dock = DockStyle.Fill
        KryptonPanel1.Margin = New Padding(0)
        billingSections.Controls.Add(รายการสินค้าในบิล, 0, 0)
        billingSections.Controls.Add(CreateFreeCupPanel(), 0, 1)
        billingSections.Controls.Add(KryptonPanel1, 0, 2)
        Panel1.Controls.Add(billingSections)
        UpdateFreeCupPanel()

        'Leave a safe bottom margin so the last "รวมเป็นเงิน" row and
        'the grand-total section do not touch the panel border.
        Dim summaryRoot As New TableLayoutPanel With {
            .ColumnCount = 1,
            .RowCount = 4,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .Padding = New Padding(12, 4, 12, 20),
            .BackColor = Color.FromArgb(241, 247, 253)
        }
        summaryRoot.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        summaryRoot.RowStyles.Add(New RowStyle(SizeType.Absolute, 42.0!))
        summaryRoot.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        summaryRoot.RowStyles.Add(New RowStyle(SizeType.Absolute, 70.0!))
        summaryRoot.RowStyles.Add(New RowStyle(SizeType.Absolute, 64.0!))

        chkFreecup.Controls.Clear()
        chkFreecup.AutoScroll = False
        chkFreecup.BorderStyle = BorderStyle.None

        lblSummaryCaption.Dock = DockStyle.Fill
        lblSummaryCaption.Margin = New Padding(4, 0, 4, 2)
        lblSummaryCaption.TextAlign = ContentAlignment.MiddleLeft
        summaryRoot.Controls.Add(lblSummaryCaption, 0, 0)

        Dim details As New TableLayoutPanel With {
            .ColumnCount = 4,
            .RowCount = 9,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .Padding = New Padding(0),
            .BackColor = Color.Transparent
        }
        details.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 39.0!))
        details.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 17.0!))
        details.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 9.0!))
        details.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 35.0!))
        For rowIndex As Integer = 0 To 8
            details.RowStyles.Add(New RowStyle(SizeType.Percent, 11.1111!))
        Next

        SetSummaryNumericAlignment()

        AddSummaryControl(details, Button1, 0, 0)
        AddSummaryControl(details, lbPointUse, 1, 0)
        Dim pointUnit As New Label With {
            .Text = "P",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Cordia New", 14.0!, FontStyle.Regular)
        }
        AddSummaryControl(details, pointUnit, 2, 0, ContentAlignment.MiddleCenter)
        AddSummaryControl(details, lbDCPoint, 3, 0)

        AddSummaryLabelAndValue(details, Label28, lbDCPMT, 1)
        AddSummaryLabelAndValue(details, Label27, lbMoney, 2)
        AddSummaryLabelAndValue(details, Label26, lbDCCoupon, 3)

        AddSummaryControl(details, Label25, 0, 4)
        AddSummaryControl(details, txtDiscountPercent, 1, 4)
        AddSummaryControl(details, Label24, 2, 4, ContentAlignment.MiddleCenter)
        AddSummaryControl(details, tbDCPercen, 3, 4)

        AddSummaryLabelAndValue(details, Label23, tbDCCash, 5)

        AddSummaryControl(details, Label22, 0, 6)
        AddSummaryControl(details, KryptonTextBox1, 1, 6)
        AddSummaryControl(details, Label21, 2, 6, ContentAlignment.MiddleCenter)
        AddSummaryControl(details, tbChargePercent, 3, 6)

        AddSummaryControl(details, Label20, 0, 7)
        AddSummaryControl(details, KryptonTextBox2, 1, 7)
        AddSummaryControl(details, Label19, 2, 7, ContentAlignment.MiddleCenter)
        AddSummaryControl(details, txtVat, 3, 7)

        AddSummaryLabelAndValue(details, Label11, lbTotalPrice, 8)
        summaryRoot.Controls.Add(details, 0, 1)

        Dim grandTotal As New TableLayoutPanel With {
            .ColumnCount = 3,
            .RowCount = 1,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(4, 10, 4, 2),
            .Padding = New Padding(8, 4, 8, 4),
            .BackColor = Color.FromArgb(236, 253, 245)
        }
        grandTotal.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 34.0!))
        grandTotal.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 51.0!))
        grandTotal.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 15.0!))

        lblGrandTotalCaption.Dock = DockStyle.Fill
        lblGrandTotalCaption.Margin = New Padding(0)
        lblGrandTotalCaption.TextAlign = ContentAlignment.MiddleLeft
        txtTotalMoney.Dock = DockStyle.Fill
        txtTotalMoney.Margin = New Padding(4, 4, 4, 4)
        txtTotalMoney.Font = New Font("Cordia New", 24.0!, FontStyle.Bold)
        lblGrandTotalUnit.Dock = DockStyle.Fill
        lblGrandTotalUnit.Margin = New Padding(0)
        lblGrandTotalUnit.TextAlign = ContentAlignment.MiddleLeft
        grandTotal.Controls.Add(lblGrandTotalCaption, 0, 0)
        grandTotal.Controls.Add(txtTotalMoney, 1, 0)
        grandTotal.Controls.Add(lblGrandTotalUnit, 2, 0)
        summaryRoot.Controls.Add(grandTotal, 0, 2)

        Dim paymentButtons As New TableLayoutPanel With {
            .ColumnCount = 2,
            .RowCount = 1,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .Padding = New Padding(4, 4, 4, 0),
            .BackColor = Color.Transparent
        }
        paymentButtons.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        paymentButtons.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        PreparePaymentButton(btCash)
        PreparePaymentButton(btnTransfer)
        paymentButtons.Controls.Add(btCash, 0, 0)
        paymentButtons.Controls.Add(btnTransfer, 1, 0)
        summaryRoot.Controls.Add(paymentButtons, 0, 3)

        chkFreecup.Controls.Add(summaryRoot)

        chkFreecup.ResumeLayout(True)
        KryptonPanel1.ResumeLayout(True)
        Panel1.ResumeLayout(True)
    End Sub

    Private Function CreateFreeCupPanel() As Panel
        If pnlFreeCup IsNot Nothing Then Return pnlFreeCup

        pnlFreeCup = New Panel With {
            .Name = "pnlFreeCup",
            .Dock = DockStyle.Fill,
            .Margin = New Padding(8, 0, 8, 4),
            .Padding = New Padding(10, 6, 10, 6),
            .BackColor = Color.FromArgb(255, 252, 232),
            .Visible = False
        }
        AddHandler pnlFreeCup.Paint, AddressOf pnlFreeCup_Paint

        Dim freeCupLayout As New TableLayoutPanel With {
            .ColumnCount = 3,
            .RowCount = 1,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .Padding = New Padding(0),
            .BackColor = Color.Transparent
        }
        freeCupLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 38.0!))
        freeCupLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
        freeCupLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120.0!))
        freeCupLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))

        Dim lblGift As New Label With {
            .Text = "🎁",
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Segoe UI Emoji", 20.0!, FontStyle.Regular),
            .BackColor = Color.Transparent
        }

        Dim textPanel As New TableLayoutPanel With {
            .ColumnCount = 1,
            .RowCount = 2,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .Padding = New Padding(0),
            .BackColor = Color.Transparent
        }
        textPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 56.0!))
        textPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 44.0!))

        lblFreeCupTitle = New Label With {
            .Text = "คุณได้รับสิทธิ์ แก้วฟรี 1 ใบ",
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .TextAlign = ContentAlignment.BottomLeft,
            .ForeColor = Color.FromArgb(5, 102, 141),
            .Font = New Font("Cordia New", 14.0!, FontStyle.Bold),
            .BackColor = Color.Transparent
        }

        lblFreeCupSubTitle = New Label With {
            .Text = "(เมื่อซื้อเครื่องดื่มครบ 10 แก้ว)",
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .TextAlign = ContentAlignment.TopLeft,
            .ForeColor = Color.FromArgb(75, 85, 99),
            .Font = New Font("Cordia New", 10.5!, FontStyle.Regular),
            .BackColor = Color.Transparent
        }

        textPanel.Controls.Add(lblFreeCupTitle, 0, 0)
        textPanel.Controls.Add(lblFreeCupSubTitle, 0, 1)

        btnChooseFreeCup = New Button With {
            .Text = "เลือกแก้วฟรี",
            .Dock = DockStyle.Fill,
            .Margin = New Padding(8, 4, 0, 4),
            .FlatStyle = FlatStyle.Flat,
            .BackColor = Color.FromArgb(22, 163, 74),
            .ForeColor = Color.White,
            .Font = New Font("Cordia New", 12.5!, FontStyle.Bold),
            .Cursor = Cursors.Hand
        }
        btnChooseFreeCup.FlatAppearance.BorderSize = 0
        AddHandler btnChooseFreeCup.Click, AddressOf btnChooseFreeCup_Click

        freeCupLayout.Controls.Add(lblGift, 0, 0)
        freeCupLayout.Controls.Add(textPanel, 1, 0)
        freeCupLayout.Controls.Add(btnChooseFreeCup, 2, 0)
        pnlFreeCup.Controls.Add(freeCupLayout)

        Return pnlFreeCup
    End Function

    Private Sub pnlFreeCup_Paint(sender As Object, e As PaintEventArgs)
        Dim panel = TryCast(sender, Panel)
        If panel Is Nothing Then Return

        Using pen As New Pen(Color.FromArgb(34, 197, 94), 1.5!)
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            Dim rect As New Rectangle(1, 1, panel.Width - 3, panel.Height - 3)
            e.Graphics.DrawRectangle(pen, rect)
        End Using
    End Sub

    Private Sub btnChooseFreeCup_Click(sender As Object, e As EventArgs)
        If btnChooseFreeCup IsNot Nothing AndAlso Not btnChooseFreeCup.Enabled Then Return

        If useFreeCupInCurrentBill Then
            'ลูกค้าเปลี่ยนใจ: ยกเลิกการใช้สิทธิและคืนส่วนลดแก้วฟรี
            CancelFreeCupSelection()
            Return
        End If

        Dim availableFreeCup As Integer = 0
        Dim titleMatch = System.Text.RegularExpressions.Regex.Match(lblFreeCupTitle.Text, "\d+")
        If titleMatch.Success Then Integer.TryParse(titleMatch.Value, availableFreeCup)

        Dim selectionItems As List(Of FreeCupSelectionItem) = BuildFreeCupSelectionItems()
        Dim eligibleQty As Integer = selectionItems.Where(Function(item) item.IsEligible).Count()
        Dim maxUseQty As Integer = Math.Min(availableFreeCup, eligibleQty)
        If maxUseQty <= 0 Then
            If eligibleQty <= 0 Then
                ShowFreeCupWarning("เมนูในบิลนี้ไม่เข้าเงื่อนไขใช้สิทธิ์แก้วฟรี\n(เมนูมัทฉะไม่สามารถใช้สิทธิ์ได้)")
            Else
                ShowFreeCupWarning("ลูกค้ายังไม่มีสิทธิ์แก้วฟรีคงเหลือ หรือสิทธิ์ถูกใช้หมดแล้ว")
            End If
            Return
        End If

        Using selectForm As New FrmFreeCupSelect(selectionItems, maxUseQty)
            If selectForm.ShowDialog(Me) <> DialogResult.OK Then Return
            Dim selectedItems As List(Of FreeCupSelectionItem) = selectForm.SelectedItems.ToList()
            If selectedItems.Count > maxUseQty Then
                MessageBox.Show("เลือกเมนูได้ไม่เกิน " & maxUseQty.ToString("N0") & " รายการ ตามจำนวนสิทธิ์ที่มี",
                                "แก้วฟรี", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Dim selectedQty As Integer = selectedItems.Count
            'สิทธิ์แก้วฟรีใช้สำหรับตัด/บันทึกสิทธิ์เท่านั้น
            'ราคาเครื่องดื่มยังต้องคิดเงินตามปกติ ห้ามหักจนเป็น 0 บาท
            freeCupDiscountApplied = 0D
            freeCupUseQty = selectedQty
            useFreeCupInCurrentBill = True
            SetFreeCupCheckbox(True)
            AppendPromotionToComment("ใช้สิทธิแก้วฟรี " & selectedQty.ToString("N0") & " ใบ")
            UpdateFreeCupPanel()
            MessageBox.Show("เลือกใช้สิทธิ์แก้วฟรี " & selectedQty.ToString("N0") & " ใบไว้ในบิลนี้แล้ว", "แก้วฟรี",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Using
    End Sub

    Private Sub CancelFreeCupSelection()
        'การยกเลิกเป็นเพียงการคืนสถานะของบิลปัจจุบัน
        'ยังไม่บันทึก FreeCupOut จนกว่าจะชำระเงินและบันทึกบิลสำเร็จ
        useFreeCupInCurrentBill = False
        freeCupUseQty = 0
        SetFreeCupCheckbox(False)
        RemoveAppliedFreeCupDiscount()
        RemoveFreeCupComment()
        UpdateFreeCupPanel()
    End Sub

    Private Sub ShowFreeCupWarning(Optional message As String = "เมนูในบิลนี้ไม่เข้าเงื่อนไขใช้สิทธิ์แก้วฟรี")
        Using warningForm As New Form()
            warningForm.Text = "แก้วฟรี"
            warningForm.StartPosition = FormStartPosition.CenterParent
            warningForm.FormBorderStyle = FormBorderStyle.FixedDialog
            warningForm.MinimizeBox = False
            warningForm.MaximizeBox = False
            warningForm.ShowInTaskbar = False
            warningForm.ClientSize = New Size(470, 190)

            Dim messageLabel As New Label With {
                .Text = message,
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Cordia New", 18.0!, FontStyle.Bold),
                .Padding = New Padding(15, 10, 15, 5)
            }

            Dim okButton As New Button With {
                .Text = "ตกลง",
                .DialogResult = DialogResult.OK,
                .Anchor = AnchorStyles.None,
                .AutoSize = True,
                .Font = New Font("Cordia New", 15.0!, FontStyle.Bold),
                .Padding = New Padding(18, 3, 18, 3)
            }

            Dim layout As New TableLayoutPanel With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 1,
                .RowCount = 2,
                .Padding = New Padding(10)
            }
            layout.RowStyles.Add(New RowStyle(SizeType.Percent, 72.0!))
            layout.RowStyles.Add(New RowStyle(SizeType.Percent, 28.0!))
            layout.Controls.Add(messageLabel, 0, 0)
            layout.Controls.Add(okButton, 0, 1)
            warningForm.Controls.Add(layout)
            warningForm.AcceptButton = okButton
            warningForm.CancelButton = okButton
            warningForm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub UpdateFreeCupPanel()
        If pnlFreeCup Is Nothing Then Return

        Dim hasCustomer As Boolean =
            customerId > 1 AndAlso
            lbCustomerName IsNot Nothing AndAlso
            Not String.IsNullOrWhiteSpace(lbCustomerName.Text) AndAlso
            lbCustomerName.Text.Trim() <> "ลูกค้าทั่วไป"

        Dim cupQty As Decimal = GetBeverageCupQtyInCurrentBill()
        Dim canShow As Boolean = hasCustomer
        pnlFreeCup.Visible = canShow

        Dim parentLayout = TryCast(pnlFreeCup.Parent, TableLayoutPanel)
        If parentLayout IsNot Nothing AndAlso parentLayout.RowStyles.Count > 1 Then
            parentLayout.RowStyles(1).Height = If(canShow, 58.0!, 0.0!)
        End If

        If Not canShow Then
            'โหมดบัตรกระดาษไม่มีชื่อลูกค้า: ต้องคงสถานะ checkbox และส่วนลดไว้
            'ไม่ให้การรีเฟรชหน้าขายล้างเครื่องหมายถูกหลังจากใช้สิทธิ์แล้ว
            If Not useFreeCupInCurrentBill Then
                freeCupUseQty = 0
                SetFreeCupCheckbox(False)
                RemoveAppliedFreeCupDiscount()
                RemoveFreeCupComment()
            End If
            Return
        End If

        Dim oldCupBalance As Decimal = 0D
        Dim oldFreeCupBalance As Integer = 0
        GetLastFreeCupBalance(customerId, oldCupBalance, oldFreeCupBalance)

        Dim selectedFreeCupQty As Decimal = If(useFreeCupInCurrentBill, Math.Max(0, freeCupUseQty), 0)
        Dim paidCupQty As Decimal = Math.Max(0D, cupQty - selectedFreeCupQty)
        Dim totalCup As Decimal = oldCupBalance + paidCupQty
        Dim earnedFreeCup As Integer = CInt(Math.Floor(totalCup / FreeCupTargetQty))
        Dim currentCupBalance As Decimal = totalCup - (earnedFreeCup * FreeCupTargetQty)
        'แยกสิทธิ์คงเหลือทั้งหมดออกจากจำนวนที่เลือกใช้ได้ในบิลนี้
        'สิทธิ์ 2 ใบต้องยังแสดงเป็น 2 ใบ แม้บิลนี้จะมีสินค้าให้ใช้ได้เพียง 1 ใบ
        Dim totalFreeCupBalance As Integer = Math.Max(0, oldFreeCupBalance + earnedFreeCup)
        Dim availableFreeCup As Integer = GetMaxUsableFreeCupQty(cupQty, oldCupBalance, oldFreeCupBalance)

        If availableFreeCup <= 0 Then
            useFreeCupInCurrentBill = False
            freeCupUseQty = 0
            SetFreeCupCheckbox(False)
            RemoveAppliedFreeCupDiscount()
            RemoveFreeCupComment()
        End If

        Dim displayFreeCupBalance As Integer = totalFreeCupBalance
        If useFreeCupInCurrentBill Then
            displayFreeCupBalance = Math.Max(0, displayFreeCupBalance - freeCupUseQty)
        End If

        If totalFreeCupBalance > 0 Then
            lblFreeCupTitle.Text = "คุณมีสิทธิ์แก้วฟรี " & displayFreeCupBalance.ToString("N0") & " ใบ"
            lblFreeCupSubTitle.Text = "สะสมครบทุก " & FreeCupTargetQty.ToString("N0") & " แก้ว รับฟรี 1 ใบ | สะสมต่อ " & currentCupBalance.ToString("N0") & "/" & FreeCupTargetQty.ToString("N0") & " แก้ว"
        Else
            lblFreeCupTitle.Text = "สะสมแก้วฟรี " & currentCupBalance.ToString("N0") & "/" & FreeCupTargetQty.ToString("N0") & " แก้ว"
            lblFreeCupSubTitle.Text = "ซื้อเครื่องดื่มราคา " & FreeCupMinimumPrice.ToString("N0") & " บาทขึ้นไปครบ " & FreeCupTargetQty.ToString("N0") & " แก้ว รับฟรี 1 ใบ"
        End If

        btnChooseFreeCup.Text = If(useFreeCupInCurrentBill, "ยกเลิกแก้วฟรี", If(totalFreeCupBalance > 0, "เลือกแก้วฟรี " & totalFreeCupBalance.ToString("N0") & " ใบ", "เลือกแก้วฟรี"))
        btnChooseFreeCup.Enabled = availableFreeCup > 0
        btnChooseFreeCup.BackColor = If(useFreeCupInCurrentBill,
                                        Color.FromArgb(220, 38, 38),
                                        If(btnChooseFreeCup.Enabled,
                                           Color.FromArgb(22, 163, 74),
                                           Color.FromArgb(148, 163, 184)))
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If freeCupCheckboxUpdating Then Return

        If CheckBox1.Checked Then
            Dim hasCustomer As Boolean = customerId > 1 AndAlso
                                         lbCustomerName IsNot Nothing AndAlso
                                         lbCustomerName.Text.Trim() <> "ลูกค้าทั่วไป"

            'ช่วงเปลี่ยนจากบัตรกระดาษ: ใช้ checkbox เป็นสิทธิ์แก้วฟรีชั่วคราว
            'โดยไม่ต้องมีข้อมูลลูกค้า และไม่บันทึกยอดสะสมลงฐานข้อมูล
            If Not hasCustomer Then
                ApplyManualFreeCupWithoutCustomer()
                Return
            End If

            If pnlFreeCup Is Nothing OrElse Not pnlFreeCup.Visible OrElse
               btnChooseFreeCup Is Nothing OrElse Not btnChooseFreeCup.Enabled Then
                SetFreeCupCheckbox(False)
                ShowFreeCupWarning("ลูกค้ายังไม่มีสิทธิ์แก้วฟรีคงเหลือ หรือยังไม่มีเมนูที่เข้าเงื่อนไข")
                Return
            End If

            If Not useFreeCupInCurrentBill Then btnChooseFreeCup.PerformClick()
        ElseIf useFreeCupInCurrentBill Then
            CancelFreeCupSelection()
        End If
    End Sub

    Private Sub ApplyManualFreeCupWithoutCustomer()
        Dim eligibleItem As FreeCupSelectionItem = BuildFreeCupSelectionItems().FirstOrDefault(Function(item) item.IsEligible)
        If eligibleItem Is Nothing OrElse eligibleItem.Price <= 0D Then
            SetFreeCupCheckbox(False)
            Return
        End If

        'บัตรกระดาษใช้ยืนยันการใช้สิทธิ์ แต่ไม่ลดราคาเครื่องดื่ม
        freeCupDiscountApplied = 0D
        freeCupUseQty = 1
        useFreeCupInCurrentBill = True
        AppendPromotionToComment("ใช้สิทธิแก้วฟรี 1 ใบ (บัตรกระดาษ)")
    End Sub

    Private Sub SetFreeCupCheckbox(value As Boolean)
        If CheckBox1 Is Nothing OrElse CheckBox1.Checked = value Then Return
        freeCupCheckboxUpdating = True
        Try
            CheckBox1.Checked = value
        Finally
            freeCupCheckboxUpdating = False
        End Try
    End Sub

    Private Sub RemoveFreeCupComment()
        Const marker As String = "ใช้สิทธิแก้วฟรี"
        If tbComment Is Nothing OrElse Not tbComment.Text.Contains(marker) Then Return

        Dim remainingParts As New List(Of String)()
        For Each part As String In tbComment.Text.Split(New String() {"/"}, StringSplitOptions.RemoveEmptyEntries)
            Dim trimmedPart As String = part.Trim()
            If trimmedPart <> "" AndAlso Not trimmedPart.StartsWith(marker, StringComparison.Ordinal) Then
                remainingParts.Add(trimmedPart)
            End If
        Next

        tbComment.Text = String.Join(" / ", remainingParts)
    End Sub

    Private Sub RemoveAppliedFreeCupDiscount()
        If freeCupDiscountApplied <= 0D Then Return

        Dim appliedAmount As Decimal = freeCupDiscountApplied
        freeCupDiscountApplied = 0D

        Dim currentDiscount As Decimal = 0D
        Decimal.TryParse(tbDCCash.Text, currentDiscount)
        tbDCCash.Text = Math.Max(0D, currentDiscount - appliedAmount).ToString("N2")
    End Sub

    Private Sub SetSummaryNumericAlignment()
        Dim numericControls As Control() = {
            lbPointUse, lbDCPoint, lbDCPMT, lbMoney, lbDCCoupon,
            txtDiscountPercent, tbDCPercen, tbDCCash,
            KryptonTextBox1, tbChargePercent, KryptonTextBox2,
            txtVat, lbTotalPrice
        }

        For Each numericControl In numericControls
            If numericControl Is Nothing Then Continue For
            Dim kryptonTextBox = TryCast(numericControl, Krypton.Toolkit.KryptonTextBox)
            If kryptonTextBox Is Nothing Then Continue For

            kryptonTextBox.TextAlign = HorizontalAlignment.Right
            kryptonTextBox.StateCommon.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
            kryptonTextBox.StateNormal.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far
        Next

        If txtTotalMoney IsNot Nothing Then
            txtTotalMoney.TextAlign = HorizontalAlignment.Center
            txtTotalMoney.ForeColor = Color.Purple
        End If
    End Sub

    Private Sub AddSummaryLabelAndValue(layout As TableLayoutPanel,
                                        caption As Control,
                                        valueControl As Control,
                                        rowIndex As Integer)
        AddSummaryControl(layout, caption, 0, rowIndex)
        layout.SetColumnSpan(caption, 3)
        AddSummaryControl(layout, valueControl, 3, rowIndex)
    End Sub

    Private Sub AddSummaryControl(layout As TableLayoutPanel,
                                  control As Control,
                                  columnIndex As Integer,
                                  rowIndex As Integer,
                                  Optional alignment As ContentAlignment = ContentAlignment.MiddleLeft)
        control.Dock = DockStyle.Fill
        control.Margin = New Padding(4, 3, 4, 3)
        Dim caption = TryCast(control, Label)
        If caption IsNot Nothing Then caption.TextAlign = alignment
        layout.Controls.Add(control, columnIndex, rowIndex)
    End Sub

    Private Sub PreparePaymentButton(button As Button)
        button.Dock = DockStyle.Fill
        button.Margin = New Padding(4, 2, 4, 2)
        button.Anchor = AnchorStyles.None
        button.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If CustomerDisplayForm Is Nothing OrElse CustomerDisplayForm.IsDisposed OrElse Not CustomerDisplayForm.CanEnterPhone Then
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If

        If keyData >= Keys.D0 AndAlso keyData <= Keys.D9 Then
            CustomerDisplayForm.AppendPhoneDigit((keyData - Keys.D0).ToString())
            Return True
        End If

        If keyData >= Keys.NumPad0 AndAlso keyData <= Keys.NumPad9 Then
            CustomerDisplayForm.AppendPhoneDigit((keyData - Keys.NumPad0).ToString())
            Return True
        End If

        If keyData = Keys.Back Then
            CustomerDisplayForm.BackspacePhone()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub CustomerDisplay_CustomerSelected(customerNumber As Integer, customerName As String, customerCodeValue As String, customerPoint As Decimal, memberLevel As Integer)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New Action(Of Integer, String, String, Decimal, Integer)(AddressOf CustomerDisplay_CustomerSelected), customerNumber, customerName, customerCodeValue, customerPoint, memberLevel)
            Return
        End If

        customerId = customerNumber
        lCustomerNumber = customerNumber
        lbCustomerName.Text = customerName
        customerCode = If(customerCodeValue, "").Trim()
        SetCustomerPhoneWaitingStatus(False)
        SetCustomerSelectionState(True)

        GetCustomerData(customerId)
        lbCustomerHead.Text = "ลูกค้า [สมาชิก Level " & ordinal5 & "]"
        lbCustomerPoint.Text = Format(ordinal10, "0.00") & " Point "
        m_dCustomerPoint = ordinal10
        UpdateFreeCupPanel()

        Dim textValue As String = lbPointUse.Text.Replace("Point", "").Trim()
        Decimal.TryParse(textValue, num12)
    End Sub

    Private Sub CustomerDisplay_CustomerLookupFailed(message As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New Action(Of String)(AddressOf CustomerDisplay_CustomerLookupFailed), message)
            Return
        End If

        'Let the customer display paint its message before showing the cashier
        'dialog. Only after OK is pressed do we clear the customer display.
        If CustomerDisplayForm IsNot Nothing AndAlso Not CustomerDisplayForm.IsDisposed Then
            CustomerDisplayForm.ShowPhoneError("ไม่พบเบอร์โทรศัพท์ในระบบ กรุณาติดต่อพนักงาน")
        End If

        Me.BeginInvoke(New Action(Sub()
                                      Using alert As New LargeAlertForm(message)
                                          alert.ShowDialog(Me)
                                      End Using
                                      If CustomerDisplayForm IsNot Nothing AndAlso Not CustomerDisplayForm.IsDisposed Then
                                          CustomerDisplayForm.ShowPhoneScreen()
                                      End If
                                      SetCustomerPhoneWaitingStatus(False)
                                  End Sub))
    End Sub

    Private Sub SetCustomerPhoneWaitingStatus(waiting As Boolean)
        If bCustomerBarcod Is Nothing Then Return

        If waiting Then
            bCustomerBarcod.Values.Text = "กำลังรอลูกค้ากรอกเบอร์..."
            bCustomerBarcod.StateCommon.Back.Color1 = Color.FromArgb(255, 193, 7)
            bCustomerBarcod.StateCommon.Back.Color2 = Color.FromArgb(255, 193, 7)
            lbCustomerHead.Values.Text = "กำลังรอลูกค้ากรอกเบอร์โทรศัพท์..."
            lbCustomerHead.StateCommon.ShortText.Color1 = Color.DarkOrange
        Else
            bCustomerBarcod.Values.Text = "ลูกค้ากดเบอร์"
            bCustomerBarcod.StateCommon.Back.Color1 = Color.FromArgb(255, 255, 192)
            bCustomerBarcod.StateCommon.Back.Color2 = Color.FromArgb(255, 255, 192)
            If customerId <= 1 Then
                lbCustomerHead.Values.Text = "ลูกค้าทั่วไป"
                lbCustomerHead.StateCommon.ShortText.Color1 = Color.Black
            End If
        End If
    End Sub

    Private Sub SetCustomerSelectionState(confirmed As Boolean)
        'ล็อกทุกช่องทางเพิ่มสินค้าไว้จนกว่าจะเลือกลูกค้าเรียบร้อย
        customerSelectionConfirmed = confirmed AndAlso
                                     customerId > 0 AndAlso
                                     lbCustomerName IsNot Nothing AndAlso
                                     Not String.IsNullOrWhiteSpace(lbCustomerName.Text)

        If tbAutoInput IsNot Nothing Then tbAutoInput.Enabled = customerSelectionConfirmed
        If TabControl1 IsNot Nothing Then TabControl1.Enabled = customerSelectionConfirmed
        If TabControl2 IsNot Nothing Then TabControl2.Enabled = customerSelectionConfirmed
        ApplyMenuCardVisualState(customerSelectionConfirmed)
        If customerSelectionConfirmed Then
            If tbAutoInput IsNot Nothing Then tbAutoInput.Focus()
        Else
            If lbCustomerHead IsNot Nothing Then
                lbCustomerHead.Values.Text = "กรุณาเลือกประเภทลูกค้า"
                lbCustomerHead.StateCommon.ShortText.Color1 = Color.DarkOrange
            End If
        End If
    End Sub

    Private Sub ApplyMenuCardVisualState(canSelect As Boolean)
        If TabControl2 Is Nothing Then Return
        ApplyMenuCardVisualState(TabControl2, canSelect)
    End Sub

    Private Sub ApplyMenuCardVisualState(parent As Control, canSelect As Boolean)
        For Each child As Control In parent.Controls
            Dim card As DrinkMenuCard = TryCast(child, DrinkMenuCard)
            If card IsNot Nothing Then
                card.SetSelectionVisualState(canSelect)
            ElseIf child.HasChildren Then
                ApplyMenuCardVisualState(child, canSelect)
            End If
        Next
    End Sub

    Private Function EnsureCustomerSelected() As Boolean
        If customerSelectionConfirmed AndAlso
           customerId > 0 AndAlso
           lbCustomerName IsNot Nothing AndAlso
           Not String.IsNullOrWhiteSpace(lbCustomerName.Text) Then
            Return True
        End If

        customerSelectionConfirmed = False
        If tbAutoInput IsNot Nothing Then tbAutoInput.Enabled = False
        If TabControl1 IsNot Nothing Then TabControl1.Enabled = False
        If TabControl2 IsNot Nothing Then TabControl2.Enabled = False
        MessageBox.Show("กรุณากดเบอร์สมาชิก หรือกดปุ่ม ลูกค้าทั่วไป ก่อนเลือกรายการขาย", "เลือกลูกค้าก่อนเริ่มขาย", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False
    End Function

    Private Sub SelectWalkInCustomer()
        customerId = 1
        lCustomerNumber = 1
        customerCode = ""
        customerIsCertified = False
        m_dCustomerPoint = 0D
        ordinal5 = 0
        ordinal10 = 0D
        lbCustomerName.Text = "ลูกค้าทั่วไป"
        lbCustomerHead.Values.Text = "ลูกค้าทั่วไป"
        lbCustomerHead.StateCommon.ShortText.Color1 = Color.Black
        lbCustomerPoint.Text = "0.00 Point"
        SetCustomerPhoneWaitingStatus(False)
        SetCustomerSelectionState(True)
        UpdateFreeCupPanel()
        If customerDisplay IsNot Nothing AndAlso Not customerDisplay.IsDisposed Then
            customerDisplay.SetCustomerInfo(customerId, "ลูกค้าทั่วไป", 0D)
        End If
    End Sub
    Private Sub GetCustomerData(ByVal CustomerNumberIn As Integer)
        customerIsCertified = False
        Try
            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("Customer.Name,Customer.Credit,Customer.DiscountP,Customer.PriceType,Customer.MemberLevel,Customer.MemberExpire, ")
                sb.Append("Customer.DeliveryID,Customer.LimitCreditMoney,Customer.CreditMoney,Customer.CanUsePromotion,Customer.CanUsePMTBill, ")
                sb.Append("Customer.CanCollectPoint,Customer.Point, ")
                sb.Append("RTRIM(Customer.TermOfPayment) AS 'TermOfPayment', ")
                sb.Append("RTRIM(Customer.Incoterms) AS 'Incoterms', ")
                sb.Append("RTRIM(Customer.ModeOfShipment) AS 'ModeOfShipment', ")
                sb.Append("RTRIM(Customer.PortOfLoading) AS 'PortOfLoading', ")
                sb.Append("RTRIM(Customer.PortOfDischarge) AS 'PortOfDischarge',Customer.mobile ")
                .Selecting(sb.ToString, "Customer", "Customer.CustomerNumber = '" & CustomerNumberIn & "'", "Customer.Name ASC")
                ordinal5 = .DTb.Rows(0)("MemberLevel")
                ordinal10 = .DTb.Rows(0)("Point")
                customerMobile = CStr(.DTb.Rows(0)("mobile"))
                customerIsCertified = IsCustomerCertifiedCustomer(CustomerNumberIn, customerCode)
                ApplyCustomerCertifiedPaymentRules()

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ApplyCustomerCertifiedPaymentRules()
        If btnTransfer Is Nothing Then Return

        'ลูกค้ารับรองชำระได้เฉพาะเงินสดเท่านั้น
        btnTransfer.Enabled = Not customerIsCertified
        btnTransfer.Text = If(customerIsCertified, "โอนจ่าย (ปิด)", "โอนจ่าย")
    End Sub

    Private Sub RefreshCustomerCertifiedStatus()
        customerIsCertified = IsCustomerCertifiedCustomer(customerId, customerCode)
        ApplyCustomerCertifiedPaymentRules()
    End Sub

    Private Function IsCustomerCertifiedCustomer(customerNumber As Integer, customerCodeValue As String) As Boolean
        If customerNumber <= 1 AndAlso String.IsNullOrWhiteSpace(customerCodeValue) Then Return False

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT CASE WHEN EXISTS (" &
                    "SELECT 1 FROM dbo.CustomerCertification CC " &
                    "INNER JOIN dbo.Customer C ON C.CustomerNumber = CC.CustomerNumber " &
                    "WHERE CC.IsCertified = 1 AND (CC.CustomerNumber = @CustomerNumber " &
                    "OR RTRIM(ISNULL(C.CustomerCode, N'')) = @CustomerCode)" &
                    ") THEN 1 ELSE 0 END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                    cmd.Parameters.Add("@CustomerCode", SqlDbType.NVarChar, 50).Value = If(customerCodeValue, "").Trim()
                    Return Convert.ToInt32(cmd.ExecuteScalar()) = 1
                End Using
            End Using
        Catch ex As Exception
            'ถ้าตารางยังไม่มีหรือเชื่อมต่อไม่ได้ ให้ถือว่าไม่ใช่ลูกค้ารับรอง
            Return False
        End Try
    End Function

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs)
        Dim p = DirectCast(sender, Panel)
        Using pen As New Pen(Color.FromArgb(220, 220, 220))
            Dim r As Rectangle = p.ClientRectangle
            r.Width -= 1
            r.Height -= 1
            e.Graphics.DrawRectangle(pen, r)
        End Using
    End Sub

    Private Sub tbComment_Paint(sender As Object, e As PaintEventArgs)
        Using pen As New Pen(Color.FromArgb(220, 220, 220))
            Dim r As Rectangle = sender.ClientRectangle
            r.Width -= 1
            r.Height -= 1
            e.Graphics.DrawRectangle(pen, r)
        End Using
    End Sub
    Private Sub EnsureSellPromotionDetailColumn()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF COL_LENGTH('SellPromotion', 'Detail') IS NULL " &
                    "BEGIN ALTER TABLE SellPromotion ADD Detail NVARCHAR(500) NULL END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ตรวจสอบคอลัมน์ Detail ใน SellPromotion ไม่สำเร็จ: " & ex.Message,
                            "Promotion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show("ตรวจสอบตารางรับรองลูกค้าไม่สำเร็จ: " & ex.Message,
                            "Customer Certified Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub InsertSellCustomerCertified(branchNumber As Integer, sellNumber As Integer, payName As String)
        If Not customerIsCertified Then Exit Sub

        Try
            EnsureSellCustomerCertifiedTable()

            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF EXISTS (SELECT 1 FROM SellCustomerCertified WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber) " &
                    "BEGIN " &
                    "UPDATE SellCustomerCertified SET CustomerNumber = @CustomerNumber, CustomerName = @CustomerName, " &
                    "PayName = @PayName, Detail = @Detail, CONumber = @CONumber " &
                    "WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber " &
                    "END " &
                    "ELSE " &
                    "BEGIN " &
                    "INSERT INTO SellCustomerCertified (BranchNumber, SellNumber, CustomerNumber, CustomerName, PayName, Detail, CONumber) " &
                    "VALUES (@BranchNumber, @SellNumber, @CustomerNumber, @CustomerName, @PayName, @Detail, @CONumber) " &
                    "END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerId
                    cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 200).Value = If(lbCustomerName.Text, "")
                    cmd.Parameters.Add("@PayName", SqlDbType.NVarChar, 100).Value = If(payName, "")
                    cmd.Parameters.Add("@Detail", SqlDbType.NVarChar, 500).Value = "รับรองลูกค้า"
                    cmd.Parameters.Add("@CONumber", SqlDbType.Int).Value = OfficerID
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(Me, "บันทึกรับรองลูกค้าไม่สำเร็จ: " & ex.Message,
                            "Customer Certified Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show("ตรวจสอบตารางสะสมแก้วฟรีไม่สำเร็จ: " & ex.Message,
                            "Free Cup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub tbAutoInput_Enter(sender As Object, e As EventArgs)
        tbAutoInput.CueHint.CueHintText = ""
        tbAutoInput.StateCommon.Content.Color1 = Color.Black
        tbAutoInput.BackColor = Color.Yellow
        tbAutoInput.SelectAll()
    End Sub

    Private Sub tbAutoInput_Leave(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(tbAutoInput.Text) Then
            tbAutoInput.CueHint.CueHintText = "สแกนบาร์โค๊ด / เพิ่มสินค้าด้วยรหัส"
        End If
    End Sub
    Private Function IsSafeChar(ch As Char) As Boolean
        Return ch <> "'"c
    End Function
    Private Function IsEnter(ch As Char) As Boolean
        Return ch = vbCr
    End Function
    Private Sub CleanupAutoInput()
        Me.Cursor = Cursors.Default
    End Sub

    Private _busyAutoInput As Boolean = False
    'สแกนบาร์โค้ดจากช่องหน้าขาย: ถ้าตรงกับ Barcode ให้เพิ่มเข้ากริดทันที
    'ไม่ต้องเปิดหน้าต่าง fProduct ซึ่งทำให้การยิงบาร์โค้ดช้าและดูเหมือนค้าง
    Private Function TryAddProductByBarcode(barcode As String) As Boolean
        Dim lookupBarcode As String = If(barcode, "").Trim()
        If lookupBarcode = "" Then Return False

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 P.PN, RTRIM(P.ProductCode) AS ProductCode, " &
                    "RTRIM(P.Barcode) AS Barcode, RTRIM(P.Name) AS ProductName, " &
                    "ISNULL(P.Price1, 0) AS SellPrice, RTRIM(P.SUnit) AS SUnit, " &
                    "RTRIM(ISNULL(P.Comment, '')) AS ProductComment, " &
                    "ISNULL(StockSum.TotalStock, 0) AS TotalStock " &
                    "FROM Product P " &
                    "OUTER APPLY (SELECT SUM(S.Number) AS TotalStock FROM Stock S " &
                    "WHERE S.PN = P.PN AND S.BranchNumber = 1) StockSum " &
                    "WHERE P.Status IS NULL AND RTRIM(P.Barcode) = @Barcode"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 100).Value = lookupBarcode
                    Using rd As SqlDataReader = cmd.ExecuteReader()
                        If Not rd.Read() Then Return False

                        AddOrIncreaseRow(
                            Convert.ToInt64(rd("PN")),
                            Convert.ToString(rd("ProductCode")).Trim(),
                            Convert.ToString(rd("Barcode")).Trim(),
                            Convert.ToString(rd("ProductName")).Trim(),
                            ReadQtyFromNumberBox(),
                            Convert.ToString(rd("SUnit")).Trim(),
                            Convert.ToDecimal(rd("SellPrice")),
                            0D,
                            0D,
                            0D,
                            Convert.ToDecimal(rd("TotalStock")),
                            Convert.ToString(rd("ProductComment")))
                        Return True
                    End Using
                End Using
            End Using
        Catch
            'ถ้าค้นหาบาร์โค้ดโดยตรงไม่สำเร็จ ให้กระบวนการเดิมเป็นตัวสำรอง
            Return False
        End Try
    End Function

    Private Sub OpenProductByName(keyword As String)
        If TryAddProductByBarcode(keyword) Then Return

        Using f As New fProduct()
            f.IsSelectMode = True
            f.SearchText = keyword
            'โหมดแช่แข็งยังต้องค้นหาเมนูเครื่องดื่มได้ด้วย
            f.HideBeverageProducts = False

            If f.ShowDialog(Me) = DialogResult.OK Then
                AddOrIncreaseRow(
                f.SelectedPN,
                f.SelectedProductCode,
                f.SelectedBarcode,
                f.SelectedProductName,
                ReadQtyFromNumberBox(),
                f.SelectedUnit,
                f.SelectedSellPrice,
                0D,
                0D,
                0D,
                f.SelectedStock,
                f.SelectedComment)
            End If
        End Using
    End Sub

    Private Function IsFrozenItemsMode() As Boolean
        Return TabControl1 IsNot Nothing AndAlso TabControl1.SelectedTab Is TabPage11
    End Function

    Private Function ReadQtyFromNumberBox() As Decimal
        Return 1D
    End Function

    Private Sub EnsureSellTableSchema(dtSell As DataTable)

        If Not dtSell.Columns.Contains("discount") Then
            dtSell.Columns.Add("discount", GetType(Decimal))
        End If

        If Not dtSell.Columns.Contains("DiscountPer") Then
            dtSell.Columns.Add("DiscountPer", GetType(Decimal))
        End If

        dtSell.Columns("discount").DefaultValue = 0D
        dtSell.Columns("DiscountPer").DefaultValue = 0D

    End Sub

    Private Sub ShowdatainGrid()
        useFreeCupInCurrentBill = False
        freeCupUseQty = 0
        dtSell = New DataTable()
        dtSell.Columns.Add("No", GetType(Integer))
        dtSell.Columns.Add("PN", GetType(Long))
        dtSell.Columns.Add("ProductCode", GetType(String))
        dtSell.Columns.Add("Barcode", GetType(String))
        dtSell.Columns.Add("ProductName", GetType(String))
        dtSell.Columns.Add("Qty", GetType(Decimal))
        dtSell.Columns.Add("SUnit", GetType(String))
        dtSell.Columns.Add("Price", GetType(Decimal))
        dtSell.Columns.Add("Discount", GetType(Decimal))
        dtSell.Columns.Add("DiscountPer", GetType(Decimal))
        dtSell.Columns.Add("Pricediscount", GetType(Decimal))
        dtSell.Columns.Add("Amount", GetType(Decimal))
        dtSell.Columns.Add("Stock", GetType(Decimal))
        dtSell.Columns.Add("Vat", GetType(Boolean))
        dtSell.Columns("Vat").DefaultValue = True
        dtSell.Columns.Add("Comment", GetType(String))

        KryptonDataGridView1.DataSource = dtSell
        With KryptonDataGridView1.Columns
            'อยู่ตรงกลาง
            .Item("No").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleCenter

            .Item("PN").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleCenter

            .Item("ProductCode").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleCenter

            .Item("Barcode").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleCenter

            .Item("SUnit").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleCenter

            'ตัวเลขชิดขวา
            .Item("Qty").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleRight

            .Item("Price").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleRight

            .Item("Discount").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleRight

            .Item("DiscountPer").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleRight

            .Item("Pricediscount").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleRight

            .Item("Amount").DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleRight
        End With
        KryptonDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        With KryptonDataGridView1.Columns
            .Item("Qty").DefaultCellStyle.Format = "N2"
            .Item("Price").DefaultCellStyle.Format = "N2"
            .Item("Discount").DefaultCellStyle.Format = "N2"
            .Item("DiscountPer").DefaultCellStyle.Format = "N2"
            .Item("Pricediscount").DefaultCellStyle.Format = "N2"
            .Item("Amount").DefaultCellStyle.Format = "N2"
            .Item("Stock").DefaultCellStyle.Format = "N2"
        End With
        With KryptonDataGridView1.Columns
            .Item("No").Width = 50
            .Item("PN").Width = 60
            .Item("ProductCode").Width = 100
            .Item("Barcode").Width = 120
            .Item("ProductName").Width = 220
            .Item("Qty").Width = 60
            .Item("SUnit").Width = 80
            .Item("Price").Width = 90
            .Item("Discount").Width = 90
            .Item("DiscountPer").Width = 100
            .Item("Pricediscount").Width = 100
            .Item("Amount").Width = 110
            .Item("Stock").Width = 90
            .Item("Vat").Width = 60
            .Item("Comment").Width = 200
        End With

        SetupBillItemsGrid()

    End Sub

    Private Sub SetupBillItemsGrid()
        With KryptonDataGridView2
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            .DataSource = Nothing
            .Columns.Clear()

            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "BillNo",
                .DataPropertyName = "No",
                .HeaderText = "ลำดับ",
                .Width = 52,
                .ReadOnly = True
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "BillProductName",
                .DataPropertyName = "ProductName",
                .HeaderText = "สินค้า",
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                .MinimumWidth = 260,
                .ReadOnly = True
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "BillQty",
                .DataPropertyName = "Qty",
                .HeaderText = "จำนวน",
                .Width = 56,
                .ReadOnly = True
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "BillUnitPrice",
                .DataPropertyName = "Price",
                .HeaderText = "ราคา/หน่วย",
                .Width = 76,
                .ReadOnly = True
            })
            .Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "BillAmount",
                .DataPropertyName = "Amount",
                .HeaderText = "รวมเงิน",
                .Width = 76,
                .ReadOnly = True
            })
            Dim deleteIcon = TryCast(
                My.Resources.Resources.ResourceManager.GetObject("DeleteIcon"),
                Image)

            .Columns.Add(New DataGridViewImageColumn With {
                .Name = "BillDelete",
                .HeaderText = "",
                .Image = deleteIcon,
                .ImageLayout = DataGridViewImageCellLayout.Normal,
                .Width = 44,
                .MinimumWidth = 44,
                .ReadOnly = True
            })

            For Each column As DataGridViewColumn In .Columns
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .Columns("BillNo").DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter
            .Columns("BillProductName").DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft
            .Columns("BillQty").DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight
            .Columns("BillUnitPrice").DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight
            .Columns("BillAmount").DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight
            .Columns("BillDelete").DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter

            .Columns("BillQty").DefaultCellStyle.Format = "N2"
            .Columns("BillUnitPrice").DefaultCellStyle.Format = "N2"
            .Columns("BillAmount").DefaultCellStyle.Format = "N2"

            .DataSource = dtSell
        End With

        UpdateBillItemCount()
    End Sub

    Private Sub UpdateBillItemCount()
        If KryptonLabel1 Is Nothing Then Return

        Dim itemCount As Integer = 0
        If dtSell IsNot Nothing Then
            For Each row As DataRow In dtSell.Rows
                If row.RowState <> DataRowState.Deleted Then itemCount += 1
            Next
        End If

        KryptonLabel1.Values.Text = "รายการในบิล (" & itemCount.ToString() & ")"
    End Sub

    Private Sub KryptonDataGridView2_CellContentClick(sender As Object,
                                                       e As DataGridViewCellEventArgs) Handles KryptonDataGridView2.CellContentClick

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If KryptonDataGridView2.Columns(e.ColumnIndex).Name <> "BillDelete" Then Return

        Dim selectedRow = KryptonDataGridView2.Rows(e.RowIndex)
        Dim rowView = TryCast(selectedRow.DataBoundItem, DataRowView)
        If rowView Is Nothing Then Return

        If MessageBox.Show("ต้องการลบรายการนี้หรือไม่?", "ยืนยันการลบ",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        rowView.Row.Delete()
        KryptonDataGridView2.ClearSelection()
        KryptonDataGridView2.Refresh()
        RenumberSellRows()
        ClearDiscountAfterItemDelete()
        CalculateSummary()
        UpdateBillItemCount()
        UpdateCustomerDisplay()
    End Sub

    Private Sub AddOrIncreaseRow(pn As Long,
                             productCode As String,
                             barcode As String,
                             name As String,
                             qty As Decimal,
                             unit As String,
                             price As Decimal,
                             discount As Decimal,
                             discountPer As Decimal,
                             pricediscount As Decimal,
                             stock As Decimal,
                             comment As String)

        'กันทุกเส้นทางเพิ่มสินค้าอีกชั้น แม้ถูกเรียกจาก event หรือโหมดอื่น
        If Not EnsureCustomerSelected() Then Return

        'เมนูที่มาจากการ์ดต้องมี PN จริงก่อนบันทึกลง dtSell
        'ถ้า PN หาย ให้ค้นจากรหัสสินค้า/Barcode ใหม่ ป้องกัน PN = 0 ไปถึงขั้นตัด Stock
        If pn <= 0 Then
            'ชื่อที่แสดงใน Grid อาจมีรายละเอียดประเภท/ความหวานต่อท้าย เช่น
            'ลาเต้ (เย็น, 120% หวานจี๊ด) แต่ชื่อจริงใน Product ไม่มีส่วนนี้
            Dim lookupName As String = If(name, "")
            Dim optionStart As Integer = lookupName.IndexOf(" (", StringComparison.Ordinal)
            If optionStart >= 0 Then lookupName = lookupName.Substring(0, optionStart).Trim()

            pn = ResolveProductNumber(productCode, barcode, lookupName)
            If pn <= 0 Then
                MessageBox.Show("ไม่พบรหัสสินค้า (PN) สำหรับรายการ: " & If(name, ""),
                                "เพิ่มสินค้าไม่สำเร็จ",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If qty <= 0D Then qty = 1D


        If discount < 0D Then discount = 0D

        If dtSell Is Nothing Then
            MessageBox.Show("dtSell ยังไม่ได้ถูกสร้าง/Bind", "Error")
            Exit Sub
        End If

        Dim existingRow As DataRow = Nothing
        For Each candidate As DataRow In dtSell.Rows
            If candidate.RowState = DataRowState.Deleted Then Continue For

            Dim samePN As Boolean = ReadLong(candidate, "PN") = pn
            Dim sameCode As Boolean = String.Equals(
                Convert.ToString(candidate("ProductCode")).Trim(),
                If(productCode, "").Trim(),
                StringComparison.OrdinalIgnoreCase)
            Dim sameBarcode As Boolean = String.Equals(
                Convert.ToString(candidate("Barcode")).Trim(),
                If(barcode, "").Trim(),
                StringComparison.OrdinalIgnoreCase)
            Dim sameName As Boolean = String.Equals(
                Convert.ToString(candidate("ProductName")).Trim(),
                If(name, "").Trim(),
                StringComparison.OrdinalIgnoreCase)

            If samePN AndAlso sameCode AndAlso sameBarcode AndAlso sameName Then
                existingRow = candidate
                Exit For
            End If
        Next

        If existingRow IsNot Nothing Then
            Dim r = existingRow

            Dim newQty As Decimal = CDec(r("Qty")) + qty


            r("Qty") = newQty
            r("Price") = price
            r("discount") = discount
            r("Amount") = (newQty * price) - discount
            r("Stock") = stock
        Else
            Dim r = dtSell.NewRow()

            r("No") = dtSell.Rows.Count + 1
            r("PN") = pn
            r("ProductCode") = productCode
            r("Barcode") = barcode
            r("ProductName") = name
            r("Qty") = qty                  ' ✅ เก็บเป็น Decimal
            r("SUnit") = unit
            r("Discount") = discount
            r("DiscountPer") = discountPer
            r("Price") = price
            r("Pricediscount") = price - discount
            r("Amount") = (qty * price) - discount
            r("Stock") = stock
            r("Vat") = True
            r("Comment") = comment


            dtSell.Rows.Add(r)
        End If

        If KryptonDataGridView2 IsNot Nothing Then
            KryptonDataGridView2.DataSource = Nothing
            KryptonDataGridView2.DataSource = dtSell
            KryptonDataGridView2.Refresh()
        End If
        Call CalculateSummary()
        UpdateCustomerDisplay()

    End Sub

    Private Function ReadLong(row As DataRow, columnName As String) As Long
        If row Is Nothing OrElse row.IsNull(columnName) Then Return 0L
        Dim value As Long = 0L
        Long.TryParse(row(columnName).ToString(), value)
        Return value
    End Function

    Private Sub KryptonDataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles KryptonDataGridView1.CellValueChanged
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If KryptonDataGridView1.Columns(e.ColumnIndex).DataPropertyName <> "Qty" Then Return

        RecalculateSellGridRow(e.RowIndex)
    End Sub

    Private Sub KryptonDataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles KryptonDataGridView1.CellEndEdit
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If KryptonDataGridView1.Columns(e.ColumnIndex).DataPropertyName <> "Qty" Then Return

        RecalculateSellGridRow(e.RowIndex)
    End Sub

    Private Sub KryptonDataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles KryptonDataGridView2.CellValueChanged
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If KryptonDataGridView2.Columns(e.ColumnIndex).DataPropertyName <> "Qty" Then Return

        RecalculateSellGridRow(e.RowIndex)
    End Sub

    Private Sub KryptonDataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles KryptonDataGridView2.CellEndEdit
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If KryptonDataGridView2.Columns(e.ColumnIndex).DataPropertyName <> "Qty" Then Return

        RecalculateSellGridRow(e.RowIndex)
    End Sub

    Private Sub RecalculateSellGridRow(rowIndex As Integer)
        If rowIndex < 0 OrElse rowIndex >= KryptonDataGridView1.Rows.Count Then Return
        If qtyValidationInProgress Then Return

        Dim rowView = TryCast(KryptonDataGridView1.Rows(rowIndex).DataBoundItem, DataRowView)
        If rowView Is Nothing Then Return

        qtyValidationInProgress = True
        Try
            Dim qty As Decimal = ReadDecimal(rowView.Row, "Qty")
            Dim price As Decimal = ReadDecimal(rowView.Row, "Price")
            Dim discount As Decimal = ReadDecimal(rowView.Row, "Discount")
            Dim stock As Decimal = ReadDecimal(rowView.Row, "Stock")
            Dim pn As Integer = CInt(ReadDecimal(rowView.Row, "PN"))

            If qty < 0D Then qty = 0D
            If discount < 0D Then discount = 0D

            'เครื่องดื่มขายได้แม้ไม่มี Stock และยังนับสะสมแก้วฟรี
            'สินค้าที่ไม่ใช่เครื่องดื่มต้องมี Stock ก่อนขาย
            If qty > stock AndAlso Not IsNonStockProduct(pn) Then
                ShowLargeStockWarning(
                    "จำนวนสินค้าที่ระบุเกิน stock ที่มีอยู่" & Environment.NewLine &
                    "คงเหลือสูงสุด " & stock.ToString("N2") & " " &
                    Convert.ToString(rowView.Row("SUnit")))
                qty = Math.Max(0D, stock)
            End If

            rowView("Qty") = qty
            rowView("Amount") = (qty * price) - discount
            rowView.EndEdit()

            KryptonDataGridView1.Refresh()
            KryptonDataGridView2.Refresh()
            If rowIndex < KryptonDataGridView2.Rows.Count Then
                KryptonDataGridView2.InvalidateRow(rowIndex)
            End If
            CalculateSummary()
            UpdateCustomerDisplay()
        Finally
            qtyValidationInProgress = False
        End Try
    End Sub

    Private Function ReadDecimal(row As DataRow, columnName As String) As Decimal
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) OrElse row.IsNull(columnName) Then Return 0D

        Dim result As Decimal = 0D
        Decimal.TryParse(row(columnName).ToString(), result)
        Return result
    End Function

    Private Sub ShowLargeStockWarning(message As String)
        Using warningForm As New Form()
            warningForm.Text = "สินค้าไม่พอ"
            warningForm.StartPosition = FormStartPosition.CenterParent
            warningForm.FormBorderStyle = FormBorderStyle.FixedDialog
            warningForm.MinimizeBox = False
            warningForm.MaximizeBox = False
            warningForm.ShowInTaskbar = False
            warningForm.ClientSize = New Size(500, 220)

            Dim messageLabel As New Label With {
                .Dock = DockStyle.Fill,
                .Font = New Font("Cordia New", 20.0!, FontStyle.Bold),
                .Text = message,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Padding = New Padding(12),
                .AutoSize = False
            }

            Dim okButton As New Button With {
                .Text = "ตกลง",
                .Font = New Font("Cordia New", 16.0!, FontStyle.Regular),
                .Dock = DockStyle.Fill,
                .DialogResult = DialogResult.OK
            }

            Dim buttonPanel As New Panel With {
                .Dock = DockStyle.Bottom,
                .Height = 54,
                .Padding = New Padding(190, 4, 190, 4)
            }
            buttonPanel.Controls.Add(okButton)

            warningForm.Controls.Add(messageLabel)
            warningForm.Controls.Add(buttonPanel)
            warningForm.AcceptButton = okButton
            warningForm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub KryptonDataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles KryptonDataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            CommitCurrentSellGridEdit()
            Return
        End If

        If e.KeyCode <> Keys.Delete Then Return
        e.Handled = True
        DeleteCurrentSellRow()
    End Sub

    Private Sub KryptonDataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles KryptonDataGridView1.EditingControlShowing
        Dim textBox = TryCast(e.Control, TextBox)
        If textBox Is Nothing Then Return

        RemoveHandler textBox.KeyDown, AddressOf SellGridEditingTextBox_KeyDown
        AddHandler textBox.KeyDown, AddressOf SellGridEditingTextBox_KeyDown
    End Sub

    Private Sub SellGridEditingTextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter Then Return

        e.Handled = True
        e.SuppressKeyPress = True
        CommitCurrentSellGridEdit()
    End Sub

    Private Sub CommitCurrentSellGridEdit()
        If KryptonDataGridView1.CurrentCell Is Nothing Then Return

        If KryptonDataGridView1.IsCurrentCellInEditMode Then
            KryptonDataGridView1.EndEdit()
        End If

        Dim rowView = TryCast(KryptonDataGridView1.CurrentRow?.DataBoundItem, DataRowView)
        If rowView IsNot Nothing Then rowView.EndEdit()

        RecalculateSellGridRow(KryptonDataGridView1.CurrentCell.RowIndex)
    End Sub

    Private Sub miDelete_Click(sender As Object, e As EventArgs) Handles miDelete.Click
        DeleteCurrentSellRow()
    End Sub

    Private Sub DeleteCurrentSellRow()
        If KryptonDataGridView1.CurrentRow Is Nothing OrElse KryptonDataGridView1.CurrentRow.IsNewRow Then Return

        If MessageBox.Show("ต้องการลบรายการนี้หรือไม่?", "Confirm",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim rowView = TryCast(KryptonDataGridView1.CurrentRow.DataBoundItem, DataRowView)
        If rowView IsNot Nothing Then
            rowView.Row.Delete()
        Else
            KryptonDataGridView1.Rows.Remove(KryptonDataGridView1.CurrentRow)
        End If

        RenumberSellRows()
        ClearDiscountAfterItemDelete()
        CalculateSummary()
        UpdateCustomerDisplay()
    End Sub

    Private Function ResolveProductNumber(productCode As String, barcode As String, productName As String) As Long
        Try
            'ชื่อที่แสดงบนตัวเลือกท็อปปิ้งอาจมี suffix เช่น "(Topping)"
            'แต่ชื่อในตาราง Product อาจเก็บเฉพาะชื่อสินค้า ทำให้ค้นหาแบบตรงตัวไม่เจอ
            Dim lookupName As String = If(productName, "").Trim()
            lookupName = System.Text.RegularExpressions.Regex.Replace(
                lookupName, "\s*\([^)]*\)\s*$", "").Trim()

            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 PN FROM Product " &
                    "WHERE Status IS NULL AND " &
                    "((@ProductCode <> N'' AND RTRIM(ProductCode) = @ProductCode) OR " &
                    " (@Barcode <> N'' AND RTRIM(Barcode) = @Barcode) OR " &
                    " (@ProductName <> N'' AND RTRIM(Name) = @ProductName) OR " &
                    " (@ProductName <> N'' AND RTRIM(Name) LIKE @ProductNameLike)) " &
                    "ORDER BY CASE " &
                    "WHEN @ProductCode <> N'' AND RTRIM(ProductCode) = @ProductCode THEN 1 " &
                    "WHEN @Barcode <> N'' AND RTRIM(Barcode) = @Barcode THEN 2 " &
                    "WHEN @ProductName <> N'' AND RTRIM(Name) = @ProductName THEN 3 " &
                    "WHEN @ProductName <> N'' AND RTRIM(Name) LIKE @ProductNameLike THEN 4 " &
                    "ELSE 9 END, PN"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 50).Value = If(productCode, "").Trim()
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 100).Value = If(barcode, "").Trim()
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 200).Value = lookupName
                    cmd.Parameters.Add("@ProductNameLike", SqlDbType.NVarChar, 202).Value = "%" & lookupName & "%"

                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                        Return Convert.ToInt64(value)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorMessage1 = ex.Message
        End Try

        Return 0L
    End Function

    Private Function NormalizeToppingLookupName(toppingName As String) As String
        Dim lookupName As String = If(toppingName, "").Trim()
        lookupName = System.Text.RegularExpressions.Regex.Replace(
            lookupName, "\s*\([^)]*\)\s*$", "").Trim()

        'ชื่อบนปุ่มกับชื่อใน Product บางรายการเรียงคำไม่เหมือนกัน
        If lookupName.Equals("ช็อตกาแฟ", StringComparison.OrdinalIgnoreCase) Then
            Return "กาแฟช็อต"
        End If
        If lookupName.Equals("ช็อตมัทฉะ", StringComparison.OrdinalIgnoreCase) Then
            Return "มัทฉะช็อต"
        End If

        Return lookupName
    End Function

    Private Sub RenumberSellRows()
        If dtSell Is Nothing Then Return

        Dim number As Integer = 1
        For Each row As DataRow In dtSell.Rows
            If row.RowState = DataRowState.Deleted Then Continue For
            row("No") = number
            number += 1
        Next
    End Sub

    Private Sub ClearDiscountAfterItemDelete()
        'เมื่อลบสินค้า ส่วนลดโปรโมชั่นเดิมอาจไม่ตรงกับรายการที่เหลือแล้ว
        selectedPromotionName = ""
        tbDCPercen.Text = "0.00"
        tbDCCash.Text = "0.00"
    End Sub


    Private Sub CalculateSummary()

        UpdateBillItemCount()

        If dtSell Is Nothing OrElse dtSell.Rows.Count = 0 Then
            lbMoney.Text = "0.00"
            UpdateSummaryTotals(0D)
            Exit Sub
        End If

        Dim total As Decimal = 0D

        For Each row As DataRow In dtSell.Rows
            If Not IsDBNull(row("Amount")) Then
                total += Convert.ToDecimal(row("Amount"))
            End If
        Next

        lbMoney.Text = total.ToString("n2")
        UpdateSummaryTotals(total)

    End Sub

    Private Sub UpdateSummaryTotals(total As Decimal)
        Dim discount As Decimal = 0D
        Decimal.TryParse(tbDCCash.Text, discount)

        Dim vatRate As Decimal = 0.07D
        Dim netTotal As Decimal = Math.Max(0D, total - discount)
        Dim beforeVat As Decimal = Math.Round(netTotal / (1D + vatRate), 2)
        Dim vatAmount As Decimal = Math.Round(netTotal - beforeVat, 2)

        lbTotalPrice.Text = beforeVat.ToString("N2")
        txtVat.Text = vatAmount.ToString("N2")
        txtTotalMoney.Text = netTotal.ToString("N2")

        'ไม่คำนวณเปอร์เซ็นต์ย้อนกลับจากส่วนลดเงินสด
        txtDiscountPercent.Text = "0.00"
        UpdateFreeCupPanel()
        UpdateCustomerDisplay()
    End Sub

    Private Sub KryptonDataGridView1_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then Return

        Dim hit = KryptonDataGridView1.HitTest(e.X, e.Y)
        If hit.RowIndex < 0 Then Return

        KryptonDataGridView1.ClearSelection()
        KryptonDataGridView1.Rows(hit.RowIndex).Selected = True
        If hit.ColumnIndex >= 0 Then
            KryptonDataGridView1.CurrentCell = KryptonDataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
        End If
    End Sub

    Private Sub txtTotalAmount_EditValueChanged(sender As Object, e As EventArgs)
        Dim total As Decimal
        Dim discount As Decimal

        If Not Decimal.TryParse(lbMoney.Text, total) Then Exit Sub
        If Not Decimal.TryParse(tbDCCash.Text, discount) Then discount = 0D

        Dim vatRate As Decimal = 0.07D

        ' ยอดหลังหักส่วนลด
        Dim netTotal As Decimal = total - discount
        If netTotal < 0 Then netTotal = 0D

        ' แยก VAT จากยอดสุทธิ
        Dim beforeVat As Decimal = Math.Round(netTotal / (1 + vatRate), 2)
        Dim vatAmount As Decimal = Math.Round(netTotal - beforeVat, 2)

        lbTotalPrice.Text = beforeVat.ToString("N2")     ' ยอดก่อน VAT
        txtVat.Text = vatAmount.ToString("N2")           ' VAT
        txtTotalMoney.Text = netTotal.ToString("N2")     ' ยอดสุทธิที่ต้องจ่าย

        'โปรโมชั่นแบบลดเป็นเงินไม่ต้องคำนวณเป็นเปอร์เซ็นต์
        txtDiscountPercent.Text = "0.00"

    End Sub

    Private Sub tbDCCash_TextChanged(sender As Object, e As EventArgs) Handles tbDCCash.TextChanged
        CalculateSummary()
    End Sub

    Private Sub tbDCCash_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Dim total As Decimal
            Dim discount As Decimal
            discountbath = tbDCCash.Text
            If Not Decimal.TryParse(lbMoney.Text, total) Then Exit Sub
            If Not Decimal.TryParse(tbDCCash.Text, discount) Then discount = 0D

            Dim vatRate As Decimal = 0.07D

            ' ยอดหลังหักส่วนลด
            Dim netTotal As Decimal = total - discount
            If netTotal < 0 Then netTotal = 0D

            ' แยก VAT จากยอดสุทธิ
            Dim beforeVat As Decimal = Math.Round(netTotal / (1 + vatRate), 2)
            Dim vatAmount As Decimal = Math.Round(netTotal - beforeVat, 2)

            lbTotalPrice.Text = beforeVat.ToString("N2")     ' ยอดก่อน VAT
            txtVat.Text = vatAmount.ToString("N2")           ' VAT
            txtTotalMoney.Text = netTotal.ToString("N2")     ' ยอดสุทธิที่ต้องจ่าย

            'โปรโมชั่นแบบลดเป็นเงินไม่ต้องคำนวณเป็นเปอร์เซ็นต์
            txtDiscountPercent.Text = "0.00"
            Call UpdateCustomerDisplay()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub SaveSellBill()
        '===== ตัวแปรสถานะ =====
        Dim companyId As Integer = 1

        Dim num9 As Long = 0
        Dim num10 As Decimal = 0D

        'หาเลขที่ bill ก่อน
        Call GetNextBillNumberRun_VB(1, 1, Format(Date.Now, "dd/MM/yy"))

        Dim billNo As String = tbBillNumber.Text.Trim()

        If lbCustomerName.Text = "ลูกค้าทั่วไป" AndAlso customerId <= 0 Then
            customerId = 1
        End If

        If flag1 AndAlso lbCustomerName.Text <> "" AndAlso lbCustomerName.Text <> "ลูกค้าทั่วไป" AndAlso customerId <= 1 Then
            flag1 = False
            MessageBox.Show("ข้อมูลลูกค้าไม่ถูกต้อง กรุณาเลือกลูกค้าใหม่อีกครั้ง", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            btCustomer.Focus()
        End If

        If flag1 AndAlso lbCustomerName.Text = "" Then
            flag1 = False
            MessageBox.Show("กรุณาระบุลูกค้าก่อนที่จะกดปุ่ม เงินสด หรือ โอนเงิน !", "Error")
            btCustomer.Focus()
        End If

        If flag1 AndAlso (dtSell Is Nothing OrElse dtSell.Rows.Count = 0) Then
            flag1 = False
            MessageBox.Show("กรุณาใส่รายการสินค้าที่จะขายอย่างน้อย 1 รายการ ก่อนที่จะกดปุ่ม OK !", "Error")
        End If

        If flag1 AndAlso Convert.ToDecimal((lbTotalPrice.Text)) < 0D Then
            flag1 = False
            MessageBox.Show("ราคา รวมป็นเงิน หลังจากหักส่วนลดทั้งหมดแล้วต้องไม่น้อยกว่า 0 ถึงจะทำรายการต่อได้ !", "Imformation")
        End If

        If flag1 AndAlso billNo <> "" AndAlso Not cbAutoInvoice.Checked Then
            Dim needCheck As Boolean = True
            If m_strBillNumberBeforeEdit <> "" AndAlso
               m_strBillNumberBeforeEdit = billNo AndAlso
               m_strBillNumberBeforeEdit.Length > 2 AndAlso
               m_strBillNumberBeforeEdit.Substring(0, 2).ToUpper() = "OL" Then
                needCheck = False
            End If

            If iSellCheckDuplicateInvoice = 0 Then
                needCheck = False
            End If

            If needCheck Then

                Dim y As Integer = dpDate.Value.Year
                Dim startDate As DateTime = New DateTime(y, 1, 1, 0, 0, 0)
                Dim endDate As DateTime = New DateTime(y, 12, 31, 23, 59, 59)

                Dim sWhere As New StringBuilder
                clsSQL = New clsSQLQuery
                With clsSQL
                    If companyId = 0 Then
                        sWhere.Append("Status IS NULL AND BranchNumber = '" & 1 & "' ")
                        sWhere.Append("AND BillNumber = '" & tbBillNumber.Text & "' ")
                        sWhere.Append("AND DateTime >= '" & startDate & "' AND DateTime <= '" & endDate & "' ")
                        sWhere.Append("AND CompanyID IS NULL")
                    Else
                        sWhere.Append("Status IS NULL AND BranchNumber = '" & 1 & "' ")
                        sWhere.Append("AND BillNumber = '" & tbBillNumber.Text & "' ")
                        sWhere.Append("AND DateTime >= '" & startDate & "' AND DateTime <= '" & endDate & "' ")
                        sWhere.Append("AND CompanyID = '" & companyId & "'")
                    End If
                    .Selecting("TOP 1 1", "Sell", sWhere.ToString, " 1 ")

                    .IsHasRow = False
                    flag1 = False

                    If companyId = 0 Then
                        ErrorMessage1 = $"เลขที่บิล {billNo} ซ้ำกับที่เคยมีอยู่แล้วในสาขานี้"
                    Else
                        ErrorMessage1 = $"เลขที่บิล {billNo} ซ้ำกับที่เคยมีอยู่แล้ว{vbCrLf}ใน Company = {cbCompany.Text}"
                    End If
                    tbBillNumber.Focus()
                    Exit Sub
                End With

            End If
        End If
        ' A) เช็คแต้มพอไหม (เหมือนเดิม)
        If flag1 AndAlso num12 > 0D AndAlso num12 > m_dCustomerPoint Then
            flag1 = False
            ErrorMessage1 =
        "Point ที่ต้องการใช้ในบิลนี้ = " & lbPointUse.Text & vbCrLf &
        "Point ของลูกค้าที่มีอยู่ = " & m_dCustomerPoint.ToString() & vbCrLf &
        "Point ของลูกค้ามีไม่เพียงพอที่จะใช้ !" & vbCrLf
        End If

        ' B) เช็คสถานะ SalesOrder (Parameter)
        '========================
        If flag1 AndAlso lSalesOrderID > 0 Then

            Dim openSellStatus As Long = 0
            Dim soStatus As Long = 0
            clsSQL = New clsSQLQuery
            With clsSQL
                Dim sWher As New StringBuilder
                sWher.Append("BranchNumber = '" & 1 & "' AND SalesOrderID = '" & lSalesOrderID & "'")
                .Selecting("OpenSellStatus,Status", "SalesOrder", sWher.ToString, "OpenSellStatus")

                openSellStatus = CLng(.DTb.Rows(0)("OpenSellStatus"))
                soStatus = CLng(.DTb.Rows(0)("Status"))

                ' 1) ถูกยกเลิกแล้ว (Status != 0)
                If flag1 AndAlso soStatus <> 0 Then
                    flag1 = False
                    ErrorMessage1 =
                        "ใบสั่งขาย ID = " & lSalesOrderID.ToString() & vbCrLf &
                        "ได้ถูกยกเลิกไปแล้ว !"
                End If

                ' 2) เปิดบิลขายครบแล้ว (OpenSellStatus = 2)
                If flag1 AndAlso openSellStatus = 2 Then
                    flag1 = False
                    ErrorMessage1 =
                        "ใบสั่งขาย ID = " & lSalesOrderID.ToString() & vbCrLf &
                        "มีการเปิดบิลขายครบตามจำนวนแล้ว !"
                End If

                ' 3) ยกเลิกสถานะค้างเปิดใบขาย (OpenSellStatus = 3)
                If flag1 AndAlso openSellStatus = 3 Then
                    flag1 = False
                    ErrorMessage1 =
                            "ใบสั่งขาย ID = " & lSalesOrderID.ToString() & vbCrLf &
                            "ได้มีการ ยกเลิกสถานะค้างเปิดใบขายไปแล้ว !"
                End If

            End With
        End If

        Dim nextSellNo As Integer = 1
        clsSQL = New clsSQLQuery
        With clsSQL
            .Selecting("MAX(SellNumber)as SellNumber ", "Sell", "BranchNumber =1 ", "SellNumber DESC")
            nextSellNo = Convert.ToInt32(.DTb.Rows(0)("SellNumber")) + 1
            lbSellNumber.Text = nextSellNo
            lbDocNumber.Text = nextSellNo
        End With

        If flag1 AndAlso (num12 > 0D OrElse m_dPointReceive > 0D) Then
            num9 = GetNextCustomerPointID(customerId)
            num10 = GetLastCustomerPointBalance(customerId)
            m_dCustomerPoint = num10
        End If

        Using conn As SqlConnection = OpenConnection()
            Using tr As SqlTransaction = conn.BeginTransaction()
                Try
                    If flag1 = True Then
                        InsertSell(1, lbSellNumber.Text)                            ' 1) Insert Sell (หัวบิล)
                        InsertSellD(1, lbSellNumber.Text)                           ' 2) Insert SellD (รายละเอียดบิล)
                        ProcessSellStock(conn, tr, 1, lbSellNumber.Text, profit, profitVat)   ' 3) ตัดสต๊อก + Insert SellStock + Serial + Expire
                        ProcessCustomerPoint(1, CInt(lbSellNumber.Text))
                        ' โปรโมชั่นจะถูกบันทึกหลังเลือกตอนชำระเงิน เพื่อไม่ให้บันทึกซ้ำ
                        tr.Commit()
                    End If
                Catch ex As Exception
                    Try : tr.Rollback() : Catch : End Try
                    flag1 = False
                    ErrorMessage1 = ex.Message
                    MessageBox.Show("บันทึกการขายไม่สำเร็จ: " & ex.Message,
                                    "Save Sell Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
    Public Sub InsertSellPromotion(branchNumber As Integer, sellNumber As Integer, detail As String)
        Try
            EnsureSellPromotionDetailColumn()

            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF EXISTS (SELECT 1 FROM SellPromotion WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber) " &
                    "BEGIN " &
                    "UPDATE SellPromotion " &
                    "SET Detail = CASE " &
                    "WHEN ISNULL(Detail, '') = '' THEN @Detail " &
                    "WHEN @Detail = '' THEN Detail " &
                    "ELSE Detail + N' / ' + @Detail END " &
                    "WHERE BranchNumber = @BranchNumber AND SellNumber = @SellNumber " &
                    "END " &
                    "ELSE " &
                    "BEGIN " &
                    "INSERT INTO SellPromotion (BranchNumber, SellNumber, Detail) VALUES (@BranchNumber, @SellNumber, @Detail) " &
                    "END"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                    cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                    cmd.Parameters.Add("@Detail", SqlDbType.NVarChar, 500).Value = If(detail, "")
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(Me, "บันทึกโปรโมชั่นไม่สำเร็จ: " & ex.Message,
                            "Promotion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Sub InsertSell(branchNumber As Integer, sellNumber As Integer)
        Try
            Dim vatType As Integer = If(cbVatType.SelectedIndex = 1, 1, 0)
            clsSQL = New clsSQLQuery
            With clsSQL
                Dim fields As New List(Of String) From {
                "BranchNumber", "ClientNumber", "SellNumber", "DocNumber", "EditCount",
                "CustomerNumber", "OfficerNumber", "BillNumber", "Credit", "VatType",
                "PayType", "Cash", "DCPMT", "Money", "DCPercent",
                "DCCash", "DiscountMoney", "ChargePercent", "ChargeMoney", "AdjustMoney",
                "PercentVat", "DateTime", "Paid", "PaidMoney", "Profit",
                "ProfitVat", "PriceNV", "PriceV", "PriceBV", "PriceVat",
                "TotalPrice", "PointReceive", "PointUse", "DCPoint", "DCCoupon",
                "CountryOfOrigin", "Comment", "CONumber", "UONumber", "CTime", "UTime"
            }

                Dim parms As List(Of String) = fields.Select(Function(f) "@" & f).ToList()

                .m1_Fild = String.Join(", ", fields)
                .m2_item = String.Join(", ", parms)
                .m3_Value = parms.ToArray()

                ' ----- 2) ใส่ข้อมูลให้ครบตามลำดับ fields (สำคัญมาก) -----
                Dim data As New List(Of String)

                ' NOT NULL หลัก ๆ
                data.Add(branchNumber.ToString())          ' BranchNumber
                data.Add("1")                              ' ClientNumber  <-- แก้เป็นค่าจริงของคุณ
                data.Add(sellNumber.ToString())            ' SellNumber
                data.Add(lbDocNumber.Text.Trim())          ' DocNumber
                data.Add("0")                              ' EditCount

                data.Add(customerId.ToString())            ' CustomerNumber
                data.Add(OfficerID.ToString())             ' OfficerNumber
                data.Add(tbBillNumber.Text.Trim())         ' BillNumber
                data.Add(tbCredit.Text.Trim())             ' Credit
                data.Add(vatType.ToString())               ' VatType

                data.Add(payType.ToString())               ' PayType
                data.Add(dPayCash.ToString())              ' Cash
                data.Add(0.00)                             ' DCPMT  (คุณใส่ซ้ำอยู่ เดี๋ยวปรับเองได้)
                data.Add(dPayCash.ToString())              ' Money (เช็คให้ตรงความหมาย)
                data.Add(tbDCPercen.Text.Trim())           ' DCPercent

                data.Add(tbDCCash.Text.Trim())             ' DCCash
                data.Add(m_dDiscountMoney.ToString())      ' DiscountMoney
                data.Add(tbChargePercent.Text.Trim())      ' ChargePercent
                data.Add(tbChargePercent.Text.Trim())      ' ChargeMoney  <-- เดิมคุณใส่ tbChargePercent ซ้ำ
                data.Add(m_dAdjustMoney.ToString())        ' AdjustMoney  <-- เดิมคั่น comma หาย

                data.Add(KryptonTextBox2.Text.Trim())      ' PercentVat
                data.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)) ' DateTime
                data.Add("1")                              ' Paid
                data.Add(lbMoney.Text.Trim())       ' PaidMoney
                data.Add(lbTotalPrice.Text.Trim())         ' Profit (เช็คให้ตรงความหมาย)

                data.Add(txtTotalMoney.Text.Trim())        ' ProfitVat (เดิมสลับมั่วมาก)
                data.Add(0.00)                             ' PriceNV
                data.Add(lbTotalPrice.Text.Trim())        ' PriceV
                data.Add(lbTotalPrice.Text.Trim())        ' PriceBV
                data.Add(txtVat.Text.Trim())               ' PriceVat

                data.Add(txtTotalMoney.Text.Trim())        ' TotalPrice

                ' พวก point/coupon: ถ้าไม่มีให้ส่ง "" (ถ้าคอลัมน์อนุญาต NULL)
                data.Add(If(m_dPointReceive > 0D, m_dPointReceive.ToString(), "")) ' PointReceive
                data.Add(If(num12 > 0D, num12.ToString(), ""))                     ' PointUse
                data.Add(If(Val(lbDCPoint.Text) > 0D, lbDCPoint.Text.Trim(), ""))  ' DCPoint
                data.Add(If(Val(lbDCCoupon.Text) > 0D, lbDCCoupon.Text.Trim(), "")) ' DCCoupon

                data.Add("THAILAND")                       ' CountryOfOrigin
                data.Add(tbComment.Text.Trim())            ' Comment
                data.Add(OfficerID.ToString())              ' CONumber
                data.Add(OfficerID.ToString())              ' UONumber

                data.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)) ' CTime
                data.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)) ' UTime

                ' ----- 3) ตรวจจำนวนให้ตรงก่อน Insert (กันตาลาย) -----
                If .m3_Value.Length <> data.Count Then
                    Throw New Exception($"จำนวนไม่เท่ากัน: Params={ .m3_Value.Length}, Data={data.Count}")
                End If

                .m4_Data = data.ToArray()
                .InsertData("Sell", .m1_Fild, .m2_item, .m3_Value, .m4_Data)
            End With


            If flag1 Then
                If lbPointUse.Text > 0 Then
                    If Me.m_dPointReceive > 0D AndAlso Not m_bOffLineMode Then
                        Try
                            num10 += Me.m_dPointReceive
                            clsSQL = New clsSQLQuery
                            With clsSQL
                                Dim fields As New List(Of String) From {
                                "CustomerNumber", "ID", "Type",
                                "SellNumber", "SellBranchNumber",
                                "PointIn", "PointOut", "Balance",
                                "CONumber", "UONumber",
                                "CTime", "UTime"}

                                Dim parms As List(Of String) = fields.Select(Function(f) "@" & f).ToList()

                                ' (ถ้าคุณใช้ clsSQLQuery แบบเดิม)
                                .m1_Fild = String.Join(", ", fields)
                                .m2_item = String.Join(", ", parms)
                                .m3_Value = parms.ToArray()

                                ' 3) ข้อมูลตามลำดับ fields (data)
                                Dim data As New List(Of String)

                                data.Add(lCustomerNumber.ToString())    ' CustomerNumber
                                data.Add(num9.ToString())               ' ID
                                data.Add("1")                           ' Type (ตามโค้ดเดิม fix = 1)
                                data.Add(num1.ToString())               ' SellNumber
                                data.Add("1")                           ' SellBranchNumber
                                data.Add(m_dPointReceive.ToString)      'PointIn
                                data.Add("0")                           ' PointOut
                                data.Add(num10.ToString)                ' Balance
                                data.Add(lLoginONumber.ToString())      ' CONumber
                                data.Add(lLoginONumber.ToString())      ' UONumber
                                data.Add(Date.Now.ToString("yyyy-MM-dd HH:mm:ss")) ' CTime
                                data.Add(Date.Now.ToString("yyyy-MM-dd HH:mm:ss")) ' UTime

                                If .m3_Value.Length <> data.Count Then
                                    Throw New Exception($"CustomerPoint: Params={ .m3_Value.Length}, Data={data.Count}")
                                End If

                                .m4_Data = data.ToArray()
                                .InsertData("CustomerPoint", .m1_Fild, .m2_item, .m3_Value, .m4_Data)
                            End With
                        Catch ex As Exception
                            flag1 = False
                            ErrorMessage1 = ex.Message
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            flag1 = False
            ErrorMessage1 = ex.Message
            Throw New Exception("InsertSell Error: " & ex.Message, ex)
        End Try
    End Sub
    Private Sub InsertSellD(branchNumber As Integer, sellNumber As Integer)
        Try
            Dim percentVat As Decimal = CDec(KryptonTextBox2.Text)
            Dim moneyBase As Decimal = CDec(lbMoney.Text)
            Dim dcPercent As Decimal = CDec(tbDCPercen.Text)
            Dim dcCash As Decimal = CDec(tbDCCash.Text)
            Dim profit As Decimal
            Dim profitVat As Decimal

            Dim dcAll As Decimal = moneyBase * (dcPercent / 100D) + dcCash
            Dim num107 As Decimal = If(moneyBase > 0D, 100D * dcAll / moneyBase, 100D)

            For Each sellRow As DataRow In dtSell.Rows
                If sellRow.RowState = DataRowState.Deleted Then Continue For
                Dim typ As Integer = 0
                Dim item As Integer = CInt(sellRow("No"))
                Dim sItem As Integer = 0
                Dim pn As Integer = CInt(sellRow("PN"))
                Dim numberQty As Decimal = CDec(sellRow("Qty"))
                lUnit = CStr(sellRow("SUnit"))
                Dim numSPerLUnit As Decimal = 1
                Dim priceBD As Decimal = CDec(sellRow("Price"))
                Dim discountCash As Decimal = CDec(sellRow("Discount"))
                Dim discountPString As String = "0.00"
                Dim discountP As Decimal = CDec(sellRow("DiscountPer"))
                Dim price As Decimal = CDec(sellRow("Price"))
                Dim sumPrice As Decimal = CDec(sellRow("Amount"))

                If typ = 2 Then
                    Dim priceNV As Decimal
                    Dim priceV As Decimal
                    If cbVatType.SelectedIndex = 0 Then
                        priceNV = price * ((100D - num107) / 100D)
                        priceV = priceNV * ((100D + percentVat) / 100D)
                    Else
                        priceV = price * ((100D - num107) / 100D)
                        priceNV = price / ((100D + percentVat) / 100D) * ((100D - num107) / 100D)
                    End If

                    Dim buyNV As Decimal = 0D ' C# num122
                    Dim buyV As Decimal = 0D  ' C# num123

                    profit += numberQty * (priceNV - buyNV)
                    profitVat += numberQty * (priceV - buyV)
                End If

                clsSQL = New clsSQLQuery
                With clsSQL
                    Dim fields As New List(Of String) From {
                        "branchNumber", "sellNumber", "Type", "item", "sItem", "pn", "Number",
                        "lUnit", "numSPerLUnit", "priceBD", "discountCash", "discountPString",
                        "discountP", "price", "sumPrice", "VatStatus"
                       }
                    Dim parms As List(Of String) = fields.Select(Function(f) "@" & f).ToList()

                    .m1_Fild = String.Join(", ", fields)
                    .m2_item = String.Join(", ", parms)
                    .m3_Value = parms.ToArray()

                    Dim data As New List(Of String)
                    data.Add(branchNumber.ToString())           'BranchNumber
                    data.Add(sellNumber.ToString())             'SellNumber
                    data.Add(typ.ToString())                    'Type
                    data.Add(item.ToString())                   'item
                    data.Add(sItem.ToString())                  'sItem
                    data.Add(pn.ToString())                     'pn
                    data.Add(numberQty.ToString())              'Number
                    data.Add(lUnit.ToString())                  'lUnit
                    data.Add(numSPerLUnit.ToString())           'numSPerLUnit
                    data.Add(price.ToString())                  'price
                    data.Add(discountCash.ToString())           'discountCash
                    data.Add(discountPString.ToString())        'discountPString
                    data.Add(discountP.ToString())              'discountP
                    data.Add(price.ToString())                  'price
                    data.Add(sumPrice.ToString())               'sumPrice
                    data.Add("1")                               'VatStatus
                    If .m3_Value.Length <> data.Count Then
                        Throw New Exception($"จำนวนไม่เท่ากัน: Params ={ .m3_Value.Length}, Data={data.Count}")
                    End If
                    .m4_Data = data.ToArray()
                    .InsertData("SellD", .m1_Fild, .m2_item, .m3_Value, .m4_Data)
                End With
            Next
        Catch ex As Exception
            flag1 = False
            ErrorMessage1 = ex.Message
            Throw
        End Try

    End Sub
    Private Sub ProcessSellStock(conn As SqlConnection, tr As SqlTransaction,
                                 branchNumber As Integer, sellNumber As Integer,
                                 ByRef profit As Decimal, ByRef profitVat As Decimal)
        Try

            For Each sellRow As DataRow In dtSell.Rows
                If sellRow.RowState = DataRowState.Deleted Then Continue For

                Dim pn As Integer = CInt(sellRow("PN"))
                Dim qtyToSell As Decimal = CDec(sellRow("Qty"))
                Dim sellPrice As Decimal = CDec(sellRow("Price"))

                'เครื่องดื่มไม่มี Stock ยกมา: บันทึกยอดติดลบตามยอดขายทันที
                'ไม่ดึงล็อตคงเหลือจากวันก่อนมาตัดแบบ FIFO
                If IsNonStockProduct(conn, tr, pn) Then
                    InsertNegativeStock(conn, tr, branchNumber, sellNumber, pn, qtyToSell)
                    Continue For
                End If

                While qtyToSell > 0

                    Dim billType As Integer
                    Dim billNumber As Integer
                    Dim stockQty As Decimal
                    Dim buyPrice As Decimal
                    Dim buyVat As Decimal = 0D
                    Dim usetime As DateTime

                    '========================
                    ' 1) หา stock ล็อตแรกสุด (FIFO) ใน transaction เดียวกับการตัด stock
                    '========================
                    Using stockCmd As New SqlCommand(
                        "SELECT TOP 1 BillType, BillNumber, Number, BuyPrice, PercentVat, CanUseTime " &
                        "FROM Stock WITH (UPDLOCK, HOLDLOCK) " &
                        "WHERE BranchNumber=@BranchNumber AND PN=@PN AND Number > 0 " &
                        "ORDER BY CanUseTime ASC, BillType ASC, BillNumber ASC", conn, tr)
                        stockCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        stockCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                        Using reader As SqlDataReader = stockCmd.ExecuteReader()
                            If reader.Read() Then
                                billType = Convert.ToInt32(reader("BillType"))
                                billNumber = Convert.ToInt32(reader("BillNumber"))
                                stockQty = Convert.ToDecimal(reader("Number"))
                                buyPrice = Convert.ToDecimal(reader("BuyPrice"))
                                buyVat = Convert.ToDecimal(reader("PercentVat"))
                                usetime = Convert.ToDateTime(reader("CanUseTime"))
                            Else
                                Throw New Exception("Stock ไม่พอ สำหรับสินค้า PN = " & pn)
                            End If
                        End Using
                    End Using

                    '========================
                    ' 2) คำนวณจำนวนที่จะตัดจากล็อตนี้
                    '========================
                    Dim useQty As Decimal = If(qtyToSell >= stockQty, stockQty, qtyToSell)

                    '========================
                    ' 3) บันทึกลง SellStock ใน transaction เดียวกัน
                    '========================
                    Using sellStockCmd As New SqlCommand(
                        "INSERT INTO SellStock " &
                        "(BranchNumber, SellNumber, BillType, BillNumber, PN, Number, BuyPrice, BuyPercentVat, SellPrice, SellPercentVat, CanUseTime) " &
                        "VALUES (@BranchNumber, @SellNumber, @BillType, @BillNumber, @PN, @Number, @BuyPrice, @BuyPercentVat, @SellPrice, @SellPercentVat, @CanUseTime)", conn, tr)
                        sellStockCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        sellStockCmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                        sellStockCmd.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                        sellStockCmd.Parameters.Add("@BillNumber", SqlDbType.Int).Value = billNumber
                        sellStockCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                        sellStockCmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = useQty
                        sellStockCmd.Parameters.Add("@BuyPrice", SqlDbType.Decimal).Value = buyPrice
                        sellStockCmd.Parameters.Add("@BuyPercentVat", SqlDbType.Decimal).Value = buyVat
                        sellStockCmd.Parameters.Add("@SellPrice", SqlDbType.Decimal).Value = sellPrice
                        sellStockCmd.Parameters.Add("@SellPercentVat", SqlDbType.Decimal).Value = 7D
                        sellStockCmd.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = usetime
                        If sellStockCmd.ExecuteNonQuery() <> 1 Then
                            Throw New InvalidOperationException("บันทึกรายการตัดสต็อกไม่สำเร็จ")
                        End If
                    End Using

                    '========================
                    ' 4) ตัด stock ออกจากตาราง Stock
                    '========================
                    If stockQty <= useQty Then
                        ' ใช้หมดล็อต -> ลบแถวออก
                        Using deleteStockCmd As New SqlCommand(
                            "DELETE FROM Stock WHERE BranchNumber=@BranchNumber AND BillType=@BillType AND BillNumber=@BillNumber AND PN=@PN", conn, tr)
                            deleteStockCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                            deleteStockCmd.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                            deleteStockCmd.Parameters.Add("@BillNumber", SqlDbType.Int).Value = billNumber
                            deleteStockCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                            If deleteStockCmd.ExecuteNonQuery() <> 1 Then
                                Throw New InvalidOperationException("ไม่พบล็อตสต็อกที่ต้องการตัด")
                            End If
                        End Using
                    Else
                        ' ใช้ไม่หมดล็อต -> ลดจำนวนคงเหลือ
                        Using updateStockCmd As New SqlCommand(
                            "UPDATE Stock SET NumberNS=ISNULL(NumberNS,0)-@Qty, Number=ISNULL(Number,0)-@Qty " &
                            "WHERE BranchNumber=@BranchNumber AND BillType=@BillType AND BillNumber=@BillNumber AND PN=@PN AND Number >= @Qty", conn, tr)
                            updateStockCmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = useQty
                            updateStockCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                            updateStockCmd.Parameters.Add("@BillType", SqlDbType.Int).Value = billType
                            updateStockCmd.Parameters.Add("@BillNumber", SqlDbType.Int).Value = billNumber
                            updateStockCmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                            If updateStockCmd.ExecuteNonQuery() <> 1 Then
                                Throw New InvalidOperationException("ตัดจำนวนคงเหลือในล็อตสต็อกไม่สำเร็จ")
                            End If
                        End Using
                    End If

                    '========================
                    ' 5) คำนวณกำไร
                    '========================
                    Dim lineProfit As Decimal = useQty * (sellPrice - buyPrice)
                    profit += lineProfit

                    '========================
                    ' 6) หักจำนวนที่ขายไปแล้ว
                    '========================
                    qtyToSell -= useQty

                End While

            Next
        Catch ex As Exception
            flag1 = False
            ErrorMessage1 = ex.Message
            Throw
        End Try
    End Sub

    Private Sub InsertNegativeStock(conn As SqlConnection, tr As SqlTransaction,
                                    branchNumber As Integer, sellNumber As Integer, pn As Integer, qty As Decimal)
        If qty <= 0D Then Return

        Dim negativeQty As Decimal = -qty
        Using cmd As New SqlCommand(
            "INSERT INTO Stock (BranchNumber, BillType, BillNumber, PN, NumberNS, NumberS, Number, BuyPrice, PercentVat, CanUseTime) " &
            "VALUES (@BranchNumber, 5, @SellNumber, @PN, @NumberNS, 0, @Number, 0, 0, @CanUseTime)", conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
            cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            cmd.Parameters.Add("@NumberNS", SqlDbType.Decimal).Value = negativeQty
            cmd.Parameters.Add("@Number", SqlDbType.Decimal).Value = negativeQty
            cmd.Parameters.Add("@CanUseTime", SqlDbType.DateTime).Value = DateTime.Now
            If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("บันทึกสต็อกติดลบไม่สำเร็จ")
        End Using
    End Sub

    Private Function IsNonStockProduct(conn As SqlConnection, tr As SqlTransaction, pn As Integer) As Boolean
        Using cmd As New SqlCommand(
            "SELECT TOP 1 ISNULL(ProductGroup.CategoryID, -1) AS CategoryID, " &
            "ISNULL(RTRIM(Product.ProductCode), '') AS ProductCode, " &
            "ISNULL(RTRIM(Category.Name), '') AS CategoryName, " &
            "ISNULL(RTRIM(ProductGroup.Name), '') AS GroupName, " &
            "ISNULL(RTRIM(Product.SUnit), '') AS SUnit, " &
            "ISNULL(RTRIM(Product.Name), '') AS ProductName " &
            "FROM Product LEFT JOIN ProductGroup ON ProductGroup.ID=Product.GroupID " &
            "LEFT JOIN Category ON Category.ID=ProductGroup.CategoryID WHERE Product.PN=@PN", conn, tr)
            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If Not reader.Read() Then Return False
                Dim categoryId As Integer = Convert.ToInt32(reader("CategoryID"))
                Dim productCode As String = Convert.ToString(reader("ProductCode")).Trim()
                Dim categoryName As String = Convert.ToString(reader("CategoryName")).Trim()
                Dim groupName As String = Convert.ToString(reader("GroupName")).Trim()
                Dim unitName As String = Convert.ToString(reader("SUnit")).Trim()
                Dim productName As String = Convert.ToString(reader("ProductName")).Trim()
                'นับเฉพาะหมวดเครื่องดื่มจริง ไม่ใช้ชื่อหรือหน่วยเป็นตัวเดา
                Return categoryId = 5 OrElse categoryName = "เครื่องดื่ม" OrElse productCode = "C021"
            End Using
        End Using
    End Function

    Private Function IsNonStockProduct(pn As Integer) As Boolean
        clsSQL = New clsSQLQuery
        With clsSQL
            Dim selectFields As String =
                "TOP 1 ISNULL(ProductGroup.CategoryID, -1) AS CategoryID, " &
                "ISNULL(RTRIM(Product.ProductCode), '') AS ProductCode, " &
                "ISNULL(RTRIM(Category.Name), '') AS CategoryName, " &
                "ISNULL(RTRIM(ProductGroup.Name), '') AS GroupName, " &
                "ISNULL(RTRIM(Product.SUnit), '') AS SUnit, " &
                "ISNULL(RTRIM(Product.Name), '') AS ProductName"
            Dim fromText As String = "Product LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID"
            Dim whereText As String = "Product.PN = '" & pn & "'"

            .Selecting(selectFields, fromText, whereText, "Product.PN")

            If Not .IsHasRow Then Return False

            Dim categoryId As Integer = CInt(.DTb.Rows(0)("CategoryID"))
            Dim productCode As String = .DTb.Rows(0)("ProductCode").ToString().Trim()
            Dim categoryName As String = .DTb.Rows(0)("CategoryName").ToString().Trim()
            Dim groupName As String = .DTb.Rows(0)("GroupName").ToString().Trim()
            Dim unitName As String = .DTb.Rows(0)("SUnit").ToString().Trim()
            Dim productName As String = .DTb.Rows(0)("ProductName").ToString().Trim()

            'นับเฉพาะหมวดเครื่องดื่มจริง ไม่ใช้ชื่อหรือหน่วยเป็นตัวเดา
            Return categoryId = 5 OrElse categoryName = "เครื่องดื่ม" OrElse productCode = "C021"
        End With
    End Function
    'ใช้เฉพาะระบบสะสมแก้วฟรี: นับเฉพาะสินค้าที่อยู่ในหมวด/กลุ่มเครื่องดื่ม
    'สินค้าอื่น เช่น อาหาร ขนม และท็อปปิ้ง จะไม่ถูกนับสะสม
    Private Function IsBeverageProduct(pn As Integer) As Boolean
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 CASE WHEN " &
                    "(ISNULL(ProductGroup.CategoryID, -1) = 5 " &
                    "OR ISNULL(Category.Name, '') LIKE N'%เครื่องดื่ม%' " &
                    "OR ISNULL(ProductGroup.Name, '') LIKE N'%เครื่องดื่ม%' " &
                    "OR ISNULL(Product.SUnit, '') LIKE N'%แก้ว%') " &
                    "AND ISNULL(Category.Name, '') NOT LIKE N'อื่น%' " &
                    "AND ISNULL(ProductGroup.Name, '') NOT LIKE N'อื่น%' " &
                    "THEN 1 ELSE 0 END " &
                    "FROM Product " &
                    "LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID " &
                    "LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID " &
                    "WHERE Product.Status IS NULL AND Product.PN = @PN"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                    Dim result As Object = cmd.ExecuteScalar()
                    Return result IsNot Nothing AndAlso result IsNot DBNull.Value AndAlso Convert.ToInt32(result) = 1
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function GetPointReceive(netAmount As Decimal, saleDate As DateTime) As Decimal

        Dim pointReceive As Decimal = 0D
        Dim bestPriority As Integer = Integer.MaxValue

        clsSQL = New clsSQLQuery
        With clsSQL
            .Selecting("*",
                   "CPoint",
                   "'" & saleDate.ToString("yyyy-MM-dd HH:mm:ss") & "' BETWEEN SDate AND EDate",
                   "Priority ASC")

            If .IsHasRow Then
                For Each r As DataRow In .DTb.Rows

                    Dim priceRule As Decimal = CDec(r("Price"))
                    Dim pointRule As Decimal = CDec(r("Point"))
                    Dim priorityRule As Integer = CInt(r("Priority"))

                    If priceRule > 0D Then
                        Dim p As Decimal = Math.Floor(netAmount / priceRule) * pointRule

                        If priorityRule < bestPriority Then
                            bestPriority = priorityRule
                            pointReceive = p
                        End If
                    End If

                Next
            End If
        End With

        Return pointReceive

    End Function
    Private Function GetLastCustomerPointBalance(customerNumber As Integer) As Decimal

        Dim balance As Decimal = 0D
        Try
            clsSQL = New clsSQLQuery
            With clsSQL
                .Selecting("TOP 1 Balance", "CustomerPoint",
                       "CustomerNumber = " & customerNumber,
                       "ID DESC")

                If .IsHasRow Then
                    balance = Convert.ToDecimal(.DTb.Rows(0)("Balance"))
                End If
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "GetLastCustomerPointBalance Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return balance
    End Function
    Private Function GetNextCustomerPointID(customerNumber As Integer) As Integer

        Dim nextID As Integer = 1

        Try
            clsSQL = New clsSQLQuery
            With clsSQL
                .Selecting("ISNULL(MAX(ID),0) AS MaxID", "CustomerPoint", "CustomerNumber = " & customerNumber, "MaxID desc")

                If .IsHasRow Then
                    nextID = Convert.ToInt32(.DTb.Rows(0)("MaxID")) + 1
                End If
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "GetNextCustomerPointID Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return nextID

    End Function
    Private Sub InsertCustomerPoint(branchNumber As Integer, sellNumber As Integer, customerid As Integer, pointReceive As Decimal)

        Try
            If pointReceive <= 0D Then Exit Sub
            If m_bOffLineMode Then Exit Sub

            Dim nextID As Integer = GetNextCustomerPointID(customerid)
            Dim lastBalance As Decimal = GetLastCustomerPointBalance(customerid)
            newBalance = lastBalance + pointReceive

            clsSQL = New clsSQLQuery
            With clsSQL

                Dim fields As New List(Of String) From {
                "CustomerNumber", "ID", "Type",
                "SellNumber", "SellBranchNumber",
                "PointIn", "PointOut", "Balance",
                "CONumber", "UONumber",
                "CTime", "UTime"
            }

                Dim parms As List(Of String) = fields.Select(Function(f) "@" & f).ToList()

                .m1_Fild = String.Join(", ", fields)
                .m2_item = String.Join(", ", parms)
                .m3_Value = parms.ToArray()

                Dim data As New List(Of String)

                data.Add(customerid.ToString())                             ' CustomerNumber
                data.Add(nextID.ToString())                                 ' ID
                data.Add("1")                                               ' Type = 1 รับแต้ม
                data.Add(sellNumber.ToString())                             ' SellNumber
                data.Add(branchNumber.ToString())                           ' SellBranchNumber
                data.Add(pointReceive.ToString())                           ' PointIn
                data.Add("0")                                               ' PointOut
                data.Add(newBalance.ToString())                             ' Balance
                data.Add(lLoginONumber.ToString())                          ' CONumber
                data.Add(lLoginONumber.ToString())                          ' UONumber
                data.Add(Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))          ' CTime
                data.Add(Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))          ' UTime

                If .m3_Value.Length <> data.Count Then
                    Throw New Exception($"CustomerPoint count mismatch : Params={ .m3_Value.Length}, Data={data.Count}")
                End If

                .m4_Data = data.ToArray()
                .InsertData("CustomerPoint", .m1_Fild, .m2_item, .m3_Value, .m4_Data)

                If .IsError Then
                    Throw New Exception("Insert CustomerPoint Failed : " & .m_Error)
                End If

            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "InsertCustomerPoint Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try

    End Sub
    Private Sub InsertSellCPoint(branchNumber As Integer, sellNumber As Integer, pointReceive As Decimal)
        Try
            If pointReceive <= 0 Then Exit Sub

            clsSQL = New clsSQLQuery

            With clsSQL

                Dim fields As New List(Of String) From {
                "BranchNumber",
                "SellNumber",
                "PointReceive"
            }

                Dim parms As List(Of String) =
                fields.Select(Function(f) "@" & f).ToList()

                .m1_Fild = String.Join(", ", fields)
                .m2_item = String.Join(", ", parms)
                .m3_Value = parms.ToArray()

                Dim data As New List(Of String)

                data.Add(branchNumber.ToString())
                data.Add(sellNumber.ToString())
                data.Add(pointReceive.ToString())

                .m4_Data = data.ToArray()

                .InsertData("SellCPoint",
                        .m1_Fild,
                        .m2_item,
                        .m3_Value,
                        .m4_Data)

            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString(),
                        "InsertSellCPoint Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub UpdateSellPointReceive(branchNumber As Integer, sellNumber As Integer, pointReceive As Decimal)
        Try
            If pointReceive <= 0D Then Exit Sub
            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("BranchNumber = '" & 1 & "'And sellNumber = '" & sellNumber & "' ")
                .UpdateData("Sell", "PointReceive = '" & pointReceive.ToString & "' ", sb.ToString)
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "UpdateSellPointReceive Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try
    End Sub
    Private Sub UpdateCustomer(branchNumber As Integer, sellNumber As Integer, pointReceive As Decimal)
        Try
            If pointReceive <= 0D Then Exit Sub
            clsSQL = New clsSQLQuery
            With clsSQL
                sb = New StringBuilder
                sb.Append("CustomerNumber = '" & customerId & "'")
                .UpdateData("Customer", "Point = '" & newBalance.ToString & "' ", sb.ToString)
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "UpdateSellPointReceive Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try


    End Sub
    Private Sub ProcessCustomerPoint(branchNumber As Integer, sellNumber As Integer)

        Try
            If customerId = 0 Then Exit Sub

            Dim netAmount As Decimal = CDec(lbTotalPrice.Text)
            Dim saleDate As DateTime = dpDate.Value

            Dim pointReceive As Decimal =
            GetPointReceive(netAmount, saleDate)

            If pointReceive <= 0 Then Exit Sub

            'Update Sell
            UpdateSellPointReceive(branchNumber, sellNumber, pointReceive)

            'Insert CustomerPoint
            InsertCustomerPoint(branchNumber, sellNumber, customerId, pointReceive)

            'Update Customer
            UpdateCustomer(branchNumber, sellNumber, pointReceive)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub
    Private Function GetCPointResult(netAmount As Decimal, saleDate As DateTime) As Decimal
        Dim pointReceive As Decimal = 0D

        clsSQL = New clsSQLQuery
        With clsSQL

            Dim whereText As String =
            "'" & saleDate.ToString("yyyy-MM-dd HH:mm:ss") & "' BETWEEN SDate AND EDate"

            .Selecting("*", "CPoint", whereText, "Priority ASC")
            If .IsHasRow Then
                For Each r As DataRow In .DTb.Rows

                    Dim rulePrice As Decimal = CDec(r("Price"))
                    Dim rulePoint As Decimal = CDec(r("Point"))

                    If rulePrice > 0D Then

                        Dim stepCount As Integer = Math.Floor(netAmount / rulePrice)

                        pointReceive = stepCount * rulePoint

                        Exit For
                    End If
                Next
            End If
        End With
        Return pointReceive
    End Function
    Private Function GetNextBillNumberRun_VB(ByVal billType As Long, ByVal branchNumber As Long, ByVal docDate As DateTime) As Boolean
        Dim ok As Boolean = True
        ' 1) อ่าน row พร้อม lock กันชน (UPDLOCK + HOLDLOCK)
        Dim resetMode As Integer = 0
        Dim billYear As DateTime = DateTime.Now
        Dim billMonth As Integer = 0
        Dim billDay As Integer = 0
        Dim prefix As String = ""
        Dim includeBranch As Integer = 0
        Dim includeClient As Integer = 0
        Dim includeYear As Integer = 0
        Dim includeMonth As Integer = 0
        Dim includeDay As Integer = 0
        Dim numDigit As Integer = 0
        Dim dbNextNumber As Long = 1
        Dim serverNow As DateTime = DateTime.Now
        Dim errorMessage As String
        Dim SWher As New StringBuilder
        Dim clientNumber As Long
        Dim nextBillNumberString As String
        Dim nextNumber As Long

        errorMessage = ""
        clsSQL = New clsSQLQuery
        With clsSQL
            sb = New StringBuilder
            sb.Append("ResetMode,BillYear,BillMonth,BillDay,RTRIM(Prefix) AS Prefix,IncludeBranch, ")
            sb.Append("IncludeClientID,IncludeYear,IncludeMonth,IncludeDay,NumDigit, ")
            sb.Append("NextNumber,GETDATE() AS ServerDateTime ")
            SWher.Append("BillType =" & 1 & " AND BranchNumber = '" & 1 & "'")
            .Selecting(sb.ToString, "BillNumberRun WITH (UPDLOCK, HOLDLOCK)", SWher.ToString, "BillType")
            If .IsHasRow = False Then
                errorMessage = ("ไม่พบข้อมูลการตั้งค่า BillNumberRun สำหรับ BillType/Branch ที่ระบุ")
            Else
                resetMode = Convert.ToInt32(.DTb.Rows(0)("ResetMode"))
                billYear = Convert.ToDateTime(.DTb.Rows(0)("BillYear"))
                billMonth = Convert.ToInt32(.DTb.Rows(0)("BillMonth"))
                billDay = Convert.ToInt32(.DTb.Rows(0)("BillDay"))
                If .DTb.Rows(0)("Prefix") IsNot DBNull.Value Then
                    prefix = Convert.ToString(.DTb.Rows(0)("Prefix")).TrimEnd()
                End If

                includeBranch = Convert.ToInt32(.DTb.Rows(0)("IncludeBranch"))
                includeClient = Convert.ToInt32(.DTb.Rows(0)("IncludeClientID"))
                includeYear = Convert.ToInt32(.DTb.Rows(0)("IncludeYear"))
                includeMonth = Convert.ToInt32(.DTb.Rows(0)("IncludeMonth"))
                includeDay = Convert.ToInt32(.DTb.Rows(0)("IncludeDay"))
                numDigit = Convert.ToInt32(.DTb.Rows(0)("NumDigit"))
                dbNextNumber = Convert.ToInt64(.DTb.Rows(0)("NextNumber"))
                serverNow = Convert.ToDateTime(.DTb.Rows(0)("ServerDateTime"))
            End If

        End With

        ' 2) ตรวจ ResetMode เหมือนของเดิม
        Select Case resetMode
            Case 1 ' reset รายปี
                If docDate.Year <> serverNow.Year Then
                    ok = False
                    MessageBox.Show("")
                    errorMessage = $"ปีของเอกสาร = {docDate.Year}{vbCrLf}ปีปัจจุบัน = {serverNow.Year}{vbCrLf}ปีเอกสาร ไม่เท่ากับ ปีปัจจุบัน ไม่สามารถใช้ระบบสร้างเลขที่บิลแบบอัตโนมัติได้ !"
                Else
                    If billYear.Year < serverNow.Year Then
                        dbNextNumber = 1
                    ElseIf billYear.Year > serverNow.Year Then
                        ok = False
                        errorMessage = $"ปีของเลขที่บิลที่ตั้งไว้ในระบบบิลแบบอัตโนมัติ = {billYear.Year}{vbCrLf}ปีปัจจุบัน = {serverNow.Year}{vbCrLf}ปีของเลขที่บิลที่ตั้งไว้ มากกว่า ปีปัจจุบัน{vbCrLf}กรุณาตั้งค่าใหม่"
                    End If
                End If

            Case 2 ' reset รายเดือน
                If docDate.Year <> serverNow.Year OrElse docDate.Month <> serverNow.Month Then
                    ok = False
                    errorMessage = $"เดือนของเอกสาร = {docDate.Month}/{docDate.Year}{vbCrLf}เดือนปัจจุบัน = {serverNow.Month}/{serverNow.Year}{vbCrLf}เดือนเอกสาร ไม่เท่ากับ เดือนปัจจุบัน ไม่สามารถใช้ระบบสร้างเลขที่บิลแบบอัตโนมัติได้ !"
                Else
                    Dim lastMonthDate As New DateTime(billYear.Year, billMonth, 1)
                    Dim curMonthDate As New DateTime(serverNow.Year, serverNow.Month, 1)

                    If lastMonthDate < curMonthDate Then
                        dbNextNumber = 1
                    ElseIf lastMonthDate > curMonthDate Then
                        ok = False
                        errorMessage = $"เดือนของเลขที่บิลที่ตั้งไว้ในระบบ = {billMonth}/{billYear.Year}{vbCrLf}เดือนปัจจุบัน = {serverNow.Month}/{serverNow.Year}{vbCrLf}เดือนในระบบ มากกว่า เดือนปัจจุบัน{vbCrLf}กรุณาตั้งค่าใหม่"
                    End If
                End If

            Case 3 ' reset รายวัน
                If docDate.Year <> serverNow.Year OrElse docDate.Month <> serverNow.Month OrElse docDate.Day <> serverNow.Day Then
                    ok = False
                    errorMessage = $"วันที่เอกสาร = {docDate.Day}/{docDate.Month}/{docDate.Year}{vbCrLf}วันที่ปัจจุบัน = {serverNow.Day}/{serverNow.Month}/{serverNow.Year}{vbCrLf}วันที่เอกสาร ไม่เท่ากับ วันที่ปัจจุบัน ไม่สามารถใช้ระบบสร้างเลขที่บิลแบบอัตโนมัติได้ !"
                Else
                    Dim lastDayDate As New DateTime(billYear.Year, billMonth, billDay)
                    Dim curDayDate As New DateTime(serverNow.Year, serverNow.Month, serverNow.Day)

                    If lastDayDate < curDayDate Then
                        dbNextNumber = 1
                    ElseIf lastDayDate > curDayDate Then
                        ok = False
                        errorMessage = $"วันที่ของเลขที่บิลที่ตั้งไว้ในระบบ = {billDay}/{billMonth}/{billYear.Year}{vbCrLf}วันที่ปัจจุบัน = {serverNow.Day}/{serverNow.Month}/{serverNow.Year}{vbCrLf}วันที่ในระบบ มากกว่า วันที่ปัจจุบัน{vbCrLf}กรุณาตั้งค่าใหม่"
                    End If
                End If

            Case Else
                ok = False
                errorMessage = $"ResetMode ไม่ถูกต้อง: {resetMode}"
        End Select

        If Not ok Then
            Return False
        End If

        ' 3) ประกอบเลขบิล
        Dim bill As String = prefix

        If includeBranch = 1 Then bill &= branchNumber.ToString("00")
        If includeClient = 1 Then bill &= clientNumber.ToString("00")
        If includeYear = 1 Then bill &= (serverNow.Year Mod 100).ToString("00")
        If includeMonth = 1 Then bill &= serverNow.Month.ToString("00")
        If includeDay = 1 Then bill &= serverNow.Day.ToString("00")

        Dim numberPart As String = dbNextNumber.ToString(New String("0"c, Math.Max(1, numDigit)))
        bill &= numberPart

        nextBillNumberString = bill
        nextNumber = dbNextNumber

        Dim result As String = prefix & dbNextNumber.ToString("0000000")

        tbBillNumber.Text = result

        ' 4) อัปเดตกลับ DB: NextNumber + วันที่ล่าสุด (BillYear/BillMonth/BillDay)
        clsSQL = New clsSQLQuery
        With clsSQL
            sb = New StringBuilder
            sb.Append("NextNumber = '" & dbNextNumber + 1 & "', BillYear = '" & serverNow.Year & "', ")
            sb.Append("BillMonth = '" & serverNow.Month & "',BillDay = '" & serverNow.Day & "' ")
            .UpdateData("BillNumberRun", sb.ToString, "BillType = " & 1 & " AND BranchNumber = '" & 1 & "'")
        End With

        If Not ok Then

            Return False
        End If

        Return True
        Return False

    End Function
    Private Function GenerateTempRefNo() As String
        Return "TMP" & DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function
    Private Function GetMonthlyPromotionChoices() As List(Of String)
        Dim result As New List(Of String)()
        Dim billTotal As Decimal = 0D
        Decimal.TryParse(txtTotalMoney.Text.Replace(",", ""), billTotal)

        Try
            Using conn As SqlConnection = OpenConnection()
                Using cmd As New SqlCommand("SELECT ID,RTRIM(Name) AS Name,RTRIM(Comment) AS Comment FROM PMT WHERE Status IS NULL AND SDate <= GETDATE() AND EDate >= GETDATE() ORDER BY ID", conn)
                    Dim promotionTable As New DataTable()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(promotionTable)
                    End Using

                    'อ่านข้อมูลจาก DataTable ก่อน เพื่อไม่ให้มี DataReader ค้างอยู่
                    'ระหว่างเรียก GetCategoryTotal ซึ่งมีการ Query ฐานข้อมูลซ้ำ
                    For Each promotionRow As DataRow In promotionTable.Rows
                        Dim name As String = promotionRow("Name").ToString().Trim()
                        Dim comment As String = If(promotionRow("Comment") Is DBNull.Value, "", promotionRow("Comment").ToString().Trim())
                        Dim minimum As Decimal = 0D
                        Dim match As Match = Regex.Match(comment, "ยอดขั้นต่ำต่อบิล\s*:\s*([0-9,\.]+)", RegexOptions.IgnoreCase)
                        If match.Success Then Decimal.TryParse(match.Groups(1).Value.Replace(",", ""), minimum)

                        Dim categoryName As String = ""
                        Dim categoryMatch As Match = Regex.Match(comment, "ประเภทสินค้า\s*:\s*([^;]+)", RegexOptions.IgnoreCase)
                        If categoryMatch.Success Then categoryName = categoryMatch.Groups(1).Value.Trim()

                        Dim eligibleAmount As Decimal = If(String.IsNullOrWhiteSpace(categoryName) OrElse categoryName = "ทั้งหมด",
                                                               billTotal, GetCategoryTotal(categoryName))
                        If eligibleAmount >= minimum Then
                            Dim isRaffle As Boolean = name.Contains("จับสลาก") OrElse name.Contains("ลุ้นรางวัล") OrElse name.Contains("ชิงโชค") OrElse comment.Contains("รูปแบบโปรโมชั่น: จับสลาก")
                            Dim detail As String = If(minimum > 0D, "ซื้อครบ " & minimum.ToString("N2") & " บาท", "เข้าเงื่อนไข")
                            If categoryName <> "" AndAlso categoryName <> "ทั้งหมด" Then detail &= " ในประเภท " & categoryName
                            If isRaffle Then detail &= " → รับสิทธิ์นำใบเสร็จไปจับสลาก"
                            result.Add(name & "  (" & detail & ")")
                        End If
                    Next
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ตรวจสอบโปรโมชั่นไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        Return result
    End Function

    Private Function GetCategoryTotal(categoryName As String) As Decimal
        Dim total As Decimal = 0D
        If dtSell Is Nothing OrElse String.IsNullOrWhiteSpace(categoryName) Then Return total

        Using conn As SqlConnection = OpenConnection()
            For Each row As DataRow In dtSell.Rows
                Dim pn As Long = 0
                If row.Table.Columns.Contains("PN") AndAlso Not IsDBNull(row("PN")) Then pn = Convert.ToInt64(row("PN"))
                If pn <= 0 Then Continue For

                Using cmd As New SqlCommand("SELECT TOP 1 RTRIM(C.Name) FROM Product P LEFT JOIN ProductGroup PG ON PG.ID=P.GroupID LEFT JOIN Category C ON C.ID=PG.CategoryID WHERE P.PN=@PN", conn)
                    cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
                    Dim value As Object = cmd.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value AndAlso String.Equals(value.ToString().Trim(), categoryName.Trim(), StringComparison.OrdinalIgnoreCase) Then
                        Dim amount As Decimal = 0D
                        If row.Table.Columns.Contains("Amount") AndAlso Not IsDBNull(row("Amount")) Then amount = Convert.ToDecimal(row("Amount"))
                        total += amount
                    End If
                End Using
            Next
        End Using
        Return total
    End Function

    Private Function TrySelectPromotionForPayment(ByRef usePromotion As Boolean, ByRef promotionName As String) As Boolean
        usePromotion = False
        promotionName = ""

        RefreshCustomerCertifiedStatus()

        If customerIsCertified Then
            selectedPromotionName = ""
            Return True
        End If

        'ใช้โปรโมชั่นที่เข้าเงื่อนไขรายการแรกอัตโนมัติ ไม่ต้องเปิดหน้าต่างให้เลือก
        Dim availablePromotions As List(Of String) = GetMonthlyPromotionChoices()
        If availablePromotions.Count = 0 Then
            selectedPromotionName = ""
            Return True
        End If
        Dim promotionFailureReason As String = ""
        Dim appliedDiscount As Decimal = 0D
        Dim selectedNames As New List(Of String)()
        For Each candidate As String In availablePromotions
            Dim candidateReason As String = ""
            Dim candidateDiscount As Decimal = ApplyPromotionToBill(candidate, candidateReason, False)
            If candidateDiscount > 0D Then
                'ใช้โปรโมชั่นที่ตรงกับสินค้าเป็นตัวแรกเพียงรายการเดียว
                'ไม่สะสมโปรโมชั่นหลายรายการกับบิลเดียว เพราะจะทำให้ส่วนลดของสินค้า
                'รายการเดียวถูกหักซ้ำ เช่น 6 บาทกลายเป็น 18 บาท
                appliedDiscount = candidateDiscount
                selectedNames.Add(candidate)
                Exit For
            End If
            If String.IsNullOrWhiteSpace(promotionFailureReason) Then promotionFailureReason = candidateReason
        Next

        If selectedNames.Count = 0 OrElse appliedDiscount <= 0D Then
            'สำรองกรณีโปรโมชั่นลดตรงตามสินค้า (Type 1/2/5) ถูกคัดออกจากรายการ
            'เพราะข้อความเงื่อนไขประเภทสินค้าหรือยอดขั้นต่ำใน Comment ไม่ตรงรูปแบบเดิม
            'แต่ PMTD ยังผูก PN สินค้าในบิลไว้ชัดเจน เช่น Type 5 ราคา 139 จากราคาปกติ 145
            Dim exactPromotionName As String = ""
            Dim exactDiscount As Decimal = ApplyExactDirectProductPromotion(exactPromotionName)
            If exactDiscount > 0D Then
                usePromotion = True
                promotionName = exactPromotionName
                selectedPromotionName = exactPromotionName
                Return True
            End If

            'โปรโมชั่นลดราคาเมนูโดยตรงที่ไม่กำหนดยอดขั้นต่ำจะถูกตรวจพบกับทุกบิล
            'ถ้าเมนูในบิลไม่ใช่สินค้าที่ร่วมโปรโมชั่น ให้ขายต่อราคาปกติ ไม่ต้องแจ้งเตือน
            usePromotion = False
            promotionName = ""
            selectedPromotionName = ""
            Return True
        End If
        usePromotion = True
        promotionName = String.Join(" / ", selectedNames)
        selectedPromotionName = promotionName
        Return True
    End Function

    Private Function ApplyExactDirectProductPromotion(ByRef appliedPromotionName As String) As Decimal
        appliedPromotionName = ""
        If dtSell Is Nothing OrElse dtSell.Rows.Count = 0 Then Return 0D

        Dim promotionTable As New DataTable()
        Using conn As SqlConnection = OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT H.Name,H.Type,H.Price,H.DiscountP,H.DiscountM," &
                "D.PN,D.Number,D.Price AS DetailPrice,D.DiscountP AS DetailDiscountP,D.DiscountM AS DetailDiscountM " &
                "FROM PMT H INNER JOIN PMTD D ON D.ID=H.ID " &
                "WHERE H.Status IS NULL AND H.SDate <= GETDATE() AND H.EDate >= GETDATE() " &
                "AND H.Type IN (1,2,5) AND D.Part=1 AND D.PN>0 " &
                "ORDER BY ISNULL(H.Priority,0),H.ID,D.Item", conn)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(promotionTable)
                End Using
            End Using
        End Using

        For Each promotionRow As DataRow In promotionTable.Rows
            Dim promotionPN As Long = If(IsDBNull(promotionRow("PN")), 0L, Convert.ToInt64(promotionRow("PN")))
            Dim allowedQty As Decimal = If(IsDBNull(promotionRow("Number")), 1D, Convert.ToDecimal(promotionRow("Number")))
            If promotionPN <= 0L OrElse allowedQty <= 0D Then allowedQty = 1D

            Dim promotionType As Integer = If(IsDBNull(promotionRow("Type")), 0, Convert.ToInt32(promotionRow("Type")))
            Dim fixedPrice As Decimal = If(IsDBNull(promotionRow("Price")), 0D, Convert.ToDecimal(promotionRow("Price")))
            Dim discountMoney As Decimal = If(IsDBNull(promotionRow("DiscountM")), 0D, Convert.ToDecimal(promotionRow("DiscountM")))
            Dim discountPercent As Decimal = If(IsDBNull(promotionRow("DiscountP")), 0D, Convert.ToDecimal(promotionRow("DiscountP")))
            Dim detailDiscountMoney As Decimal = If(IsDBNull(promotionRow("DetailDiscountM")), 0D, Convert.ToDecimal(promotionRow("DetailDiscountM")))
            Dim detailDiscountPercent As Decimal = If(IsDBNull(promotionRow("DetailDiscountP")), 0D, Convert.ToDecimal(promotionRow("DetailDiscountP")))

            If detailDiscountMoney > 0D Then
                discountMoney = detailDiscountMoney
                discountPercent = 0D
                fixedPrice = 0D
            ElseIf detailDiscountPercent > 0D Then
                discountPercent = detailDiscountPercent
                discountMoney = 0D
                fixedPrice = 0D
            ElseIf promotionType = 2 Then
                discountPercent = 0D
                fixedPrice = 0D
            ElseIf discountPercent <= 0D Then
                discountPercent = 0D
            End If

            For Each saleRow As DataRow In dtSell.Rows
                If saleRow.RowState = DataRowState.Deleted Then Continue For
                If ReadLong(saleRow, "PN") <> promotionPN Then Continue For

                Dim qty As Decimal = Convert.ToDecimal(saleRow("Qty"))
                If qty < allowedQty Then Continue For
                Dim unitPrice As Decimal = Convert.ToDecimal(saleRow("Price"))
                Dim promoQty As Decimal = Math.Min(qty, allowedQty)
                Dim lineDiscount As Decimal

                If discountPercent > 0D Then
                    lineDiscount = Math.Max(0D, unitPrice * discountPercent / 100D * promoQty)
                ElseIf fixedPrice > 0D Then
                    lineDiscount = Math.Max(0D, (unitPrice - fixedPrice) * promoQty)
                Else
                    lineDiscount = Math.Max(0D, Math.Min(unitPrice, discountMoney) * promoQty)
                End If

                If lineDiscount > 0D Then
                    Dim currentCashDiscount As Decimal = 0D
                    Decimal.TryParse(tbDCCash.Text.Replace(",", ""), currentCashDiscount)
                    tbDCCash.Text = (currentCashDiscount + lineDiscount).ToString("N2")
                    appliedPromotionName = Convert.ToString(promotionRow("Name")).Trim()
                    Return lineDiscount
                End If
            Next
        Next

        Return 0D
    End Function

    Private Function ApplyPromotionToBill(selectedPromotion As String,
                                          ByRef failureReason As String,
                                          Optional showMessage As Boolean = True) As Decimal
        failureReason = ""
        If dtSell Is Nothing OrElse dtSell.Rows.Count = 0 Then
            failureReason = "ยังไม่มีสินค้าในรายการขาย"
            Return 0D
        End If

        Dim promotionName As String = selectedPromotion
        Dim nameMarker As Integer = promotionName.IndexOf("  (", StringComparison.Ordinal)
        If nameMarker >= 0 Then promotionName = promotionName.Substring(0, nameMarker).Trim()

        'โปรโมชั่นแลกซื้อ: ซื้อสินค้าหลัก 1 รายการ + เลือกเครื่องดื่มได้ 1 รายการ
        'ราคาใน PMT.Price คือราคาเครื่องดื่มที่แลกซื้อ สินค้าหลักยังคิดราคาปกติ
        Dim bundleDiscount As Decimal = ApplyBundleChoicePromotion(promotionName, failureReason)
        If bundleDiscount > 0D Then Return bundleDiscount

        'ถ้าเป็นโปรโมชั่นแลกซื้อ แต่ในบิลไม่มีสินค้าฝั่งเครื่องดื่ม
        'ห้ามไหลต่อไปใช้กติกาลดสินค้าหลักโดยตรง เพราะจะทำให้ซื้อเฉพาะเต้าหู้ปลา
        'แล้วได้รับส่วนลดเหมือนซื้อครบชุด
        If IsBundleChoicePromotion(promotionName) Then
            If String.IsNullOrWhiteSpace(failureReason) Then
                failureReason = "ยังไม่มีสินค้าหลักและเครื่องดื่มที่ร่วมรายการครบชุดในบิลเดียวกัน"
            End If
            Return 0D
        End If

        'โปรโมชั่นลดราคาสินค้าโดยตรง เช่น PN 116 ลด 10 บาท เหลือ 30 บาท
        'รูปแบบนี้เก็บสินค้าไว้ใน PMTD.Part=1 และไม่มีรายการรางวัล Part=2
        Dim directDiscount As Decimal = ApplyDirectProductPromotion(promotionName, showMessage)
        If directDiscount > 0D Then Return directDiscount

        Dim discountTotal As Decimal = 0D
        Using conn As SqlConnection = OpenConnection()
            Using cmd As New SqlCommand("SELECT D.PN,D.Price,D.Number,ISNULL(P.ProductCode,'') AS RewardProductCode,ISNULL(P.Name,'') AS RewardProductName,ISNULL(H.Comment,'') AS PromotionComment FROM PMTD D INNER JOIN PMT H ON H.ID=D.ID LEFT JOIN Product P ON P.PN=D.PN WHERE H.Name=@Name AND H.Status IS NULL AND D.Part=2 ORDER BY D.Item", conn)
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = promotionName
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    Dim hasPromotionItems As Boolean = reader.HasRows
                    If Not hasPromotionItems Then
                        reader.Close()
                        Using raffleCmd As New SqlCommand("SELECT TOP 1 ISNULL(Comment,'') FROM PMT WHERE Name=@Name AND Status IS NULL", conn)
                            raffleCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = promotionName
                            Dim promotionComment As String = Convert.ToString(raffleCmd.ExecuteScalar())
                            If promotionComment.Contains("รูปแบบโปรโมชั่น: จับสลาก") OrElse promotionName.Contains("จับสลาก") OrElse promotionName.Contains("ลุ้นรางวัล") OrElse promotionName.Contains("ชิงโชค") Then
                                ShowLargePromotionMessage("ได้รับสิทธิ์จับสลากแล้ว\nกรุณานำใบเสร็จไปจับฉลากลุ้นรางวัล")
                                Return 1D
                            End If
                        End Using
                    End If
                    'ถ้าไม่มีรายการ PMTD Reader ถูกปิดไปแล้ว ต้องไม่เรียก Read ซ้ำ
                    If hasPromotionItems Then
                        While reader.Read()
                            Dim pn As Long = Convert.ToInt64(reader("PN"))
                            Dim specialPrice As Decimal = Convert.ToDecimal(reader("Price"))
                            Dim allowedQty As Decimal = Convert.ToDecimal(reader("Number"))
                            Dim rewardProductCode As String = Convert.ToString(reader("RewardProductCode")).Trim()
                            Dim rewardProductName As String = Convert.ToString(reader("RewardProductName")).Trim()
                            If allowedQty <= 0D Then allowedQty = 1D

                            For Each row As DataRow In dtSell.Rows
                                If row.RowState = DataRowState.Deleted Then Continue For

                                Dim rowPN As Long = ReadLong(row, "PN")
                                Dim rowProductCode As String = Convert.ToString(row("ProductCode")).Trim()
                                Dim rowProductName As String = Convert.ToString(row("ProductName")).Trim()
                                Dim sameProduct As Boolean = (rowPN = pn)

                                'รองรับสินค้าที่ PN ในโปรโมชั่นเก่าคลาดเคลื่อน แต่รหัสสินค้าเดียวกัน
                                If Not sameProduct AndAlso rewardProductCode <> "" Then
                                    sameProduct = String.Equals(rowProductCode, rewardProductCode, StringComparison.OrdinalIgnoreCase)
                                End If

                                'ชื่อในตะกร้าอาจมีรายละเอียดเมนูต่อท้าย เช่น (เย็น, 100% หวาน)
                                If Not sameProduct AndAlso rewardProductName <> "" Then
                                    sameProduct = PromotionProductNamesMatch(rowProductName, rewardProductName)
                                End If

                                If Not sameProduct Then Continue For
                                Dim qty As Decimal = Convert.ToDecimal(row("Qty"))
                                Dim unitPrice As Decimal = Convert.ToDecimal(row("Price"))
                                Dim promoQty As Decimal = Math.Min(qty, allowedQty)
                                Dim lineDiscount As Decimal = Math.Max(0D, (unitPrice - specialPrice) * promoQty)
                                If lineDiscount <= 0D Then Continue For

                                discountTotal += lineDiscount
                                Exit For
                            Next
                        End While
                    End If
                End Using
            End Using
        End Using

        If discountTotal > 0D Then
            Dim currentCashDiscount As Decimal = 0D
            Decimal.TryParse(tbDCCash.Text.Replace(",", ""), currentCashDiscount)
            tbDCCash.Text = (currentCashDiscount + discountTotal).ToString("N2")
        Else
            failureReason = "ตรวจพบยอดซื้อครบเงื่อนไขแล้ว แต่ไม่พบสินค้าแลกซื้อที่ตรงกับรายการในบิล หรือราคาพิเศษไม่ต่ำกว่าราคาขาย"
        End If
        Return discountTotal
    End Function

    Private Function IsBundleChoicePromotion(promotionName As String) As Boolean
        If String.IsNullOrWhiteSpace(promotionName) Then Return False

        Try
            Using conn As SqlConnection = OpenConnection()
                Using cmd As New SqlCommand(
                    "SELECT TOP 1 1 FROM PMT WHERE Name=@Name AND Status IS NULL AND Type=15", conn)
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = promotionName.Trim()
                    Return cmd.ExecuteScalar() IsNot Nothing
                End Using
            End Using
        Catch
            'ถ้าตรวจชนิดโปรโมชั่นไม่ได้ ให้ปล่อยให้กติกาเดิมจัดการต่อ
            Return False
        End Try
    End Function

    Private Function ApplyBundleChoicePromotion(promotionName As String,
                                                 ByRef failureReason As String) As Decimal
        Dim promotionItems As New DataTable()
        Using conn As SqlConnection = OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT H.Type,H.Price,D.Part,D.PN,D.Number " &
                "FROM PMT H INNER JOIN PMTD D ON D.ID=H.ID " &
                "WHERE H.Name=@Name AND H.Status IS NULL AND H.Type=15 AND D.Part IN (1,2) " &
                "ORDER BY D.Part,D.Item", conn)
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = promotionName
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(promotionItems)
                End Using
            End Using
        End Using

        If promotionItems.Rows.Count = 0 Then Return 0D

        Dim bundlePrice As Decimal = 0D
        If Not IsDBNull(promotionItems.Rows(0)("Price")) Then Decimal.TryParse(Convert.ToString(promotionItems.Rows(0)("Price")), bundlePrice)
        If bundlePrice < 0D Then bundlePrice = 0D

        Dim mainPNs As New HashSet(Of Long)()
        Dim optionPNs As New HashSet(Of Long)()
        For Each item As DataRow In promotionItems.Rows
            Dim pn As Long = Convert.ToInt64(item("PN"))
            If pn <= 0 Then Continue For
            If Convert.ToInt32(item("Part")) = 1 Then
                mainPNs.Add(pn)
            Else
                optionPNs.Add(pn)
            End If
        Next

        If mainPNs.Count = 0 OrElse optionPNs.Count = 0 Then Return 0D

        'เก็บจำนวนคงเหลือของแต่ละบรรทัด เพื่อให้ 2 ชุดในบิลเดียวกันถูกคิดได้ถูกต้อง
        Dim mainRows As New Dictionary(Of DataRow, Decimal)()
        Dim optionRows As New Dictionary(Of DataRow, Decimal)()
        For Each saleRow As DataRow In dtSell.Rows
            If saleRow.RowState = DataRowState.Deleted Then Continue For
            Dim pn As Long = ReadLong(saleRow, "PN")
            Dim qty As Decimal = 0D
            If saleRow.Table.Columns.Contains("Qty") AndAlso Not IsDBNull(saleRow("Qty")) Then qty = Convert.ToDecimal(saleRow("Qty"))
            If qty <= 0D Then Continue For
            If mainPNs.Contains(pn) Then
                mainRows(saleRow) = qty
            ElseIf optionPNs.Contains(pn) Then
                optionRows(saleRow) = qty
            End If
        Next

        If mainRows.Count = 0 OrElse optionRows.Count = 0 Then
            failureReason = "ต้องมีสินค้าหลักและเครื่องดื่มที่ร่วมรายการอยู่ในบิลเดียวกัน"
            Return 0D
        End If

        Dim discountTotal As Decimal = 0D
        For Each mainEntry In mainRows.Keys.ToList()
            Dim mainRemaining As Decimal = mainRows(mainEntry)
            For Each optionEntry In optionRows.Keys.ToList()
                If mainRemaining <= 0D Then Exit For
                Dim optionRemaining As Decimal = optionRows(optionEntry)
                If optionRemaining <= 0D OrElse Object.ReferenceEquals(mainEntry, optionEntry) Then Continue For

                Dim bundleQty As Decimal = Math.Min(mainRemaining, optionRemaining)
                Dim optionPrice As Decimal = If(IsDBNull(optionEntry("Price")), 0D, Convert.ToDecimal(optionEntry("Price")))
                Dim lineDiscount As Decimal = Math.Max(0D, (optionPrice - bundlePrice) * bundleQty)
                discountTotal += lineDiscount
                mainRemaining -= bundleQty
                optionRows(optionEntry) = optionRemaining - bundleQty
            Next
        Next

        If discountTotal <= 0D Then
            failureReason = "พบสินค้าในชุดแล้ว แต่ราคาปกติรวมไม่สูงกว่าราคาชุด"
            Return 0D
        End If

        Dim currentCashDiscount As Decimal = 0D
        Decimal.TryParse(tbDCCash.Text.Replace(",", ""), currentCashDiscount)
        tbDCCash.Text = (currentCashDiscount + discountTotal).ToString("N2")
        Return discountTotal
    End Function

    Private Function ApplyDirectProductPromotion(promotionName As String,
                                                 Optional showMessage As Boolean = True) As Decimal
        Dim discountTotal As Decimal = 0D
        Dim promotionTable As New DataTable()
        'หนึ่งบรรทัดสินค้าในบิลต้องได้รับส่วนลดตรงเพียงครั้งเดียว
        'ป้องกันข้อมูล PMTD ที่มีสินค้าเดียวกันซ้ำ ทำให้ส่วนลดถูกบวกซ้ำ
        Dim appliedSaleRows As New HashSet(Of Integer)()

        Using conn As SqlConnection = OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT D.PN,D.Number,H.Type,H.Price,H.DiscountP,H.DiscountM,D.DiscountP AS DetailDiscountP,D.DiscountM AS DetailDiscountM," &
                "ISNULL(P.ProductCode,'') AS ProductCode,ISNULL(P.Name,'') AS ProductName " &
                "FROM PMTD D INNER JOIN PMT H ON H.ID=D.ID " &
                "LEFT JOIN Product P ON P.PN=D.PN " &
                "WHERE H.Name=@Name AND H.Status IS NULL AND D.Part=1 AND D.PN>0", conn)
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = promotionName
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(promotionTable)
                End Using
            End Using
        End Using

        For Each promotionRow As DataRow In promotionTable.Rows
            Dim pn As Long = Convert.ToInt64(promotionRow("PN"))
            Dim promotionType As Integer = If(IsDBNull(promotionRow("Type")), 0, Convert.ToInt32(promotionRow("Type")))
            Dim allowedQty As Decimal = If(IsDBNull(promotionRow("Number")), 1D, Convert.ToDecimal(promotionRow("Number")))
            If allowedQty <= 0D Then allowedQty = 1D

            Dim fixedPrice As Decimal = If(IsDBNull(promotionRow("Price")), 0D, Convert.ToDecimal(promotionRow("Price")))
            Dim discountMoney As Decimal = If(IsDBNull(promotionRow("DiscountM")), 0D, Convert.ToDecimal(promotionRow("DiscountM")))
            Dim discountPercent As Decimal = If(IsDBNull(promotionRow("DetailDiscountP")), 0D, Convert.ToDecimal(promotionRow("DetailDiscountP")))
            Dim detailDiscountMoney As Decimal = If(IsDBNull(promotionRow("DetailDiscountM")), 0D, Convert.ToDecimal(promotionRow("DetailDiscountM")))
            'ถ้ามีส่วนลดเป็นเงินในรายละเอียด ให้ใช้ค่านี้ก่อนเสมอ
            'เพื่อไม่ให้ค่าเปอร์เซ็นต์เก่าของโปรโมชั่นทำให้ลดเกินจริง
            If detailDiscountMoney > 0D Then
                discountMoney = detailDiscountMoney
                discountPercent = 0D
                fixedPrice = 0D
            ElseIf promotionType = 2 Then
                discountPercent = 0D
                fixedPrice = 0D
            ElseIf discountPercent <= 0D AndAlso Not IsDBNull(promotionRow("DiscountP")) Then
                discountPercent = Convert.ToDecimal(promotionRow("DiscountP"))
            End If
            Dim promotionCode As String = Convert.ToString(promotionRow("ProductCode")).Trim()
            Dim promotionProductName As String = Convert.ToString(promotionRow("ProductName")).Trim()

            'โปรโมชั่นเดิมแบบ Type 5 บางรายการเก็บราคาพิเศษไว้ในชื่อโปรโมชั่น
            'เช่น "ลดราคาเหลือ 30 บาท" แต่ค่า PMT/PMTD.Price เป็น 0
            'ต้องใช้ราคาที่ระบุหลังคำว่า "เหลือ" เป็นราคาขายจริง
            If fixedPrice <= 0D AndAlso (promotionType = 3 OrElse promotionType = 5) Then
                Dim priceMatch As Match = Regex.Match(promotionName, "เหลือ\s*([0-9,]+(?:\.[0-9]+)?)\s*บาท", RegexOptions.IgnoreCase)
                If priceMatch.Success Then
                    Decimal.TryParse(priceMatch.Groups(1).Value.Replace(",", ""), fixedPrice)
                End If
            End If

            For Each row As DataRow In dtSell.Rows
                If row.RowState = DataRowState.Deleted Then Continue For

                Dim saleRowIndex As Integer = dtSell.Rows.IndexOf(row)
                If appliedSaleRows.Contains(saleRowIndex) Then Continue For

                Dim sameProduct As Boolean = (ReadLong(row, "PN") = pn)
                If Not sameProduct AndAlso promotionCode <> "" Then
                    sameProduct = String.Equals(Convert.ToString(row("ProductCode")).Trim(), promotionCode, StringComparison.OrdinalIgnoreCase)
                End If
                If Not sameProduct AndAlso promotionProductName <> "" Then
                    sameProduct = PromotionProductNamesMatch(Convert.ToString(row("ProductName")), promotionProductName)
                End If
                If Not sameProduct Then Continue For

                Dim qty As Decimal = Convert.ToDecimal(row("Qty"))
                Dim unitPrice As Decimal = Convert.ToDecimal(row("Price"))
                If qty < allowedQty Then Continue For
                Dim promoQty As Decimal = Math.Min(qty, allowedQty)
                Dim lineDiscount As Decimal

                If discountPercent > 0D Then
                    lineDiscount = Math.Max(0D, unitPrice * (discountPercent / 100D) * promoQty)
                ElseIf fixedPrice > 0D Then
                    lineDiscount = Math.Max(0D, (unitPrice - fixedPrice) * promoQty)
                Else
                    lineDiscount = Math.Max(0D, Math.Min(unitPrice, discountMoney) * promoQty)
                End If

                If lineDiscount > 0D Then
                    discountTotal += lineDiscount
                    appliedSaleRows.Add(saleRowIndex)
                End If
                Exit For
            Next
        Next

        If discountTotal > 0D Then
            Dim currentCashDiscount As Decimal = 0D
            Decimal.TryParse(tbDCCash.Text.Replace(",", ""), currentCashDiscount)
            tbDCCash.Text = (currentCashDiscount + discountTotal).ToString("N2")
            If showMessage Then
                ShowLargePromotionMessage("ใช้โปรโมชั่น " & promotionName & " แล้ว\nส่วนลด " & discountTotal.ToString("N2") & " บาท")
            End If
        End If

        Return discountTotal
    End Function

    'เมนูเครื่องดื่มแบบ ร้อน/เย็น/ปั่น ใช้ Product คนละ PN
    'แต่โปรโมชั่นของเมนูเดียวกันต้องใช้ร่วมกันได้ เช่น PN 261/116 กับชาเขียวปั่น
    Private Function PromotionProductNamesMatch(saleName As String, promotionName As String) As Boolean
        Dim normalizedSaleName As String = NormalizePromotionProductName(saleName)
        Dim normalizedPromotionName As String = NormalizePromotionProductName(promotionName)
        Return normalizedSaleName <> "" AndAlso
               normalizedPromotionName <> "" AndAlso
               String.Equals(normalizedSaleName, normalizedPromotionName, StringComparison.OrdinalIgnoreCase)
    End Function

    Private Function NormalizePromotionProductName(productName As String) As String
        Dim result As String = If(productName, "").Trim()
        If result = "" Then Return ""

        'ชื่อในบิลอาจมีรายละเอียด เช่น (ปั่น, 100% หวาน) ต่อท้าย
        Dim optionStart As Integer = result.IndexOf(" (", StringComparison.Ordinal)
        If optionStart >= 0 Then result = result.Substring(0, optionStart).Trim()

        result = Regex.Replace(result, "\s*[-–—]?\s*(ร้อน|เย็น|ปั่น|hot|iced|ice|blend|blended|frappe)\s*$", "", RegexOptions.IgnoreCase).Trim()
        Return Regex.Replace(result, "\s+", " ").Trim()
    End Function

    Private Sub ShowLargePromotionMessage(message As String)
        Using frm As New Form()
            frm.Text = "Promotion"
            frm.StartPosition = FormStartPosition.CenterParent
            frm.FormBorderStyle = FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.ClientSize = New Size(720, 280)
            Dim lbl As New Label With {
                .Text = message,
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Segoe UI", 20.0F, FontStyle.Bold),
                .ForeColor = Color.DarkGreen,
                .Padding = New Padding(20)}
            Dim btn As New Button With {
                .Text = "ตกลง",
                .Dock = DockStyle.Bottom,
                .Height = 58,
                .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
                .DialogResult = DialogResult.OK}
            frm.Controls.Add(lbl)
            frm.Controls.Add(btn)
            frm.AcceptButton = btn
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Function ShowPromotionChoiceDialog() As String
        Dim promotions As List(Of String) = GetMonthlyPromotionChoices()
        If promotions.Count = 0 Then Return ""

        Using frm As New Form()
            frm.Text = "เลือกโปรโมชั่น"
            frm.StartPosition = FormStartPosition.CenterParent
            frm.FormBorderStyle = FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.ClientSize = New Size(560, 390)
            frm.Font = New Font("Segoe UI", 11.0!, FontStyle.Regular)

            Dim lblTitle As New Label()
            lblTitle.Text = "โปรโมชั่นที่เข้าเงื่อนไขของรายการในบิลนี้"
            lblTitle.Font = New Font("Segoe UI Semibold", 13.0!, FontStyle.Bold)
            lblTitle.AutoSize = False
            lblTitle.Location = New Point(18, 16)
            lblTitle.Size = New Size(520, 34)
            frm.Controls.Add(lblTitle)

            Dim panel As New FlowLayoutPanel()
            panel.Location = New Point(18, 58)
            panel.Size = New Size(520, 250)
            panel.FlowDirection = FlowDirection.TopDown
            panel.WrapContents = False
            panel.AutoScroll = True
            frm.Controls.Add(panel)
            For Each promoName As String In promotions
                Dim chk As New CheckBox()
                chk.Text = promoName
                chk.Tag = promoName
                chk.AutoSize = False
                chk.Size = New Size(490, 36)
                chk.Margin = New Padding(4, 4, 4, 4)
                panel.Controls.Add(chk)
            Next

            Dim btnOK As New Button()
            btnOK.Text = "ตกลง"
            btnOK.DialogResult = DialogResult.OK
            btnOK.Size = New Size(120, 42)
            btnOK.Location = New Point(286, 326)
            btnOK.BackColor = Color.FromArgb(38, 166, 154)
            btnOK.ForeColor = Color.White
            btnOK.FlatStyle = FlatStyle.Flat
            frm.Controls.Add(btnOK)

            Dim btnCancel As New Button()
            btnCancel.Text = "ยกเลิก"
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.Size = New Size(120, 42)
            btnCancel.Location = New Point(418, 326)
            frm.Controls.Add(btnCancel)

            frm.AcceptButton = btnOK
            frm.CancelButton = btnCancel

            If frm.ShowDialog(Me) <> DialogResult.OK Then Return ""
            Dim selectedPromotions As New List(Of String)()
            For Each ctrl As Control In panel.Controls
                Dim chk = TryCast(ctrl, CheckBox)
                If chk IsNot Nothing AndAlso chk.Checked Then
                    selectedPromotions.Add(Convert.ToString(chk.Tag))
                End If
            Next

            If selectedPromotions.Count > 0 Then
                Return String.Join(" / ", selectedPromotions)
            End If
        End Using

        Return ""
    End Function

    Private Sub AppendPromotionToComment(promotionName As String)
        If String.IsNullOrWhiteSpace(promotionName) Then Exit Sub

        'ชื่อที่แสดงในหน้าต่างเลือกโปรโมชั่นอาจมีรายละเอียดเงื่อนไขต่อท้ายในวงเล็บ
        'ไม่ต้องพิมพ์รายละเอียดส่วนนี้ซ้ำบนใบเสร็จ ให้แสดงเฉพาะชื่อโปรโมชั่นหลัก
        Dim displayName As String = promotionName.Trim()
        Dim detailStart As Integer = displayName.IndexOf("  (", StringComparison.Ordinal)
        If detailStart >= 0 Then displayName = displayName.Substring(0, detailStart).Trim()
        If String.IsNullOrWhiteSpace(displayName) Then Exit Sub

        If tbComment.Text.Trim().Contains(displayName) Then Exit Sub

        If String.IsNullOrWhiteSpace(tbComment.Text) Then
            tbComment.Text = displayName
        Else
            tbComment.Text = tbComment.Text.Trim() & " / " & displayName
        End If
    End Sub

    Private Function BuildPromotionDetail(promotionName As String) As String
        Dim detail As String = If(promotionName, "").Trim()
        Dim discountCash As Decimal = 0D
        Decimal.TryParse(tbDCCash.Text, discountCash)

        If discountCash > 0D Then
            If detail <> "" Then detail &= " | "
            detail &= "ส่วนลด " & discountCash.ToString("N2") & " บาท"
        End If

        Return detail
    End Function

    Private Function GetBeverageCupQtyInCurrentBill() As Decimal
        Dim cupQty As Decimal = 0D

        If dtSell Is Nothing Then Return cupQty

        For Each sellRow As DataRow In dtSell.Rows
            If sellRow.RowState = DataRowState.Deleted Then Continue For

            Dim pn As Integer = CInt(sellRow("PN"))
            If Not IsBeverageProduct(pn) Then Continue For

            Dim price As Decimal = 0D
            Decimal.TryParse(Convert.ToString(sellRow("Price")), price)
            If price < FreeCupMinimumPrice Then Continue For

            Dim qty As Decimal = 0D
            Decimal.TryParse(Convert.ToString(sellRow("Qty")), qty)
            cupQty += qty
        Next

        Return cupQty
    End Function

    Private Function GetFreeCupDiscountAmount(quantity As Integer) As Decimal
        If quantity <= 0 Then Return 0D
        If dtSell Is Nothing Then Return 0D

        Dim beveragePrices As New List(Of Decimal)()
        For Each sellRow As DataRow In dtSell.Rows
            If sellRow.RowState = DataRowState.Deleted Then Continue For

            Dim pn As Integer = CInt(sellRow("PN"))
            'มัทฉะนับสะสมได้ แต่ห้ามใช้เป็นสินค้าแลกสิทธิ์แก้วฟรี
            If Not IsFreeCupRedeemableProduct(pn) Then Continue For

            Dim price As Decimal = 0D
            Decimal.TryParse(Convert.ToString(sellRow("Price")), price)
            If price >= FreeCupMinimumPrice Then
                Dim rowQty As Integer = Math.Max(1, CInt(Math.Floor(Convert.ToDecimal(sellRow("Qty")))))
                For i As Integer = 1 To rowQty
                    beveragePrices.Add(price)
                Next
            End If
        Next

        beveragePrices.Sort()
        Dim useQty As Integer = Math.Min(quantity, beveragePrices.Count)
        Dim freeCupAmount As Decimal = 0D
        For i As Integer = 0 To useQty - 1
            freeCupAmount += beveragePrices(i)
        Next
        Return freeCupAmount
    End Function

    Private Function BuildFreeCupSelectionItems() As List(Of FreeCupSelectionItem)
        Dim result As New List(Of FreeCupSelectionItem)()
        If dtSell Is Nothing Then Return result

        For Each sellRow As DataRow In dtSell.Rows
            If sellRow.RowState = DataRowState.Deleted Then Continue For

            Dim pn As Integer = CInt(sellRow("PN"))
            If Not IsBeverageProduct(pn) Then Continue For

            Dim price As Decimal = 0D
            Decimal.TryParse(Convert.ToString(sellRow("Price")), price)
            If price < FreeCupMinimumPrice Then Continue For

            Dim rowQty As Integer = Math.Max(1, CInt(Math.Floor(Convert.ToDecimal(sellRow("Qty")))))
            Dim isEligible As Boolean = IsFreeCupRedeemableProduct(pn)
            For i As Integer = 1 To rowQty
                result.Add(New FreeCupSelectionItem With {
                    .MenuName = Convert.ToString(sellRow("ProductName")),
                    .Price = price,
                    .IsEligible = isEligible
                })
            Next
        Next

        Return result
    End Function

    Private Function GetFreeCupDiscountAmount(selectedItems As IEnumerable(Of FreeCupSelectionItem)) As Decimal
        If selectedItems Is Nothing Then Return 0D
        Return selectedItems.Where(Function(item) item IsNot Nothing AndAlso item.IsEligible).
            Sum(Function(item) item.Price)
    End Function

    Private Function IsFreeCupRedeemableProduct(pn As Integer) As Boolean
        If Not IsBeverageProduct(pn) Then Return False

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 CASE WHEN " &
                    "ISNULL(Category.Name, '') NOT LIKE N'%มัทฉะ%' " &
                    "AND ISNULL(ProductGroup.Name, '') NOT LIKE N'%มัทฉะ%' " &
                    "THEN 1 ELSE 0 END " &
                    "FROM Product " &
                    "LEFT JOIN ProductGroup ON ProductGroup.ID = Product.GroupID " &
                    "LEFT JOIN Category ON Category.ID = ProductGroup.CategoryID " &
                    "WHERE Product.Status IS NULL AND Product.PN = @PN"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                    Dim result As Object = cmd.ExecuteScalar()
                    Return result IsNot Nothing AndAlso result IsNot DBNull.Value AndAlso Convert.ToInt32(result) = 1
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function GetMaxUsableFreeCupQty(cupQty As Decimal,
                                             oldCupBalance As Decimal,
                                             oldFreeCupBalance As Integer) As Integer
        Dim totalCupQty As Integer = Math.Max(0, CInt(Math.Floor(cupQty)))

        'สิทธิ์แก้วฟรีเดิมต้องแสดงได้ทันที แม้ยังไม่มีสินค้าในบิลปัจจุบัน
        If totalCupQty = 0 Then Return Math.Max(0, oldFreeCupBalance)

        Dim maxCandidate As Integer = Math.Min(totalCupQty,
                                               oldFreeCupBalance + CInt(Math.Floor((oldCupBalance + cupQty) / FreeCupTargetQty)))

        For useQty As Integer = maxCandidate To 1 Step -1
            Dim paidCupQty As Decimal = Math.Max(0D, cupQty - useQty)
            Dim earnedFreeCup As Integer = CInt(Math.Floor((oldCupBalance + paidCupQty) / FreeCupTargetQty))
            If useQty <= oldFreeCupBalance + earnedFreeCup Then Return useQty
        Next

        Return 0
    End Function

    Private Sub GetLastFreeCupBalance(customerNumber As Integer, ByRef cupBalance As Decimal, ByRef freeCupBalance As Integer)
        cupBalance = 0D
        freeCupBalance = 0

        Try
            EnsureCustomerFreeCupTable()

            Using conn As SqlConnection = modVariable.OpenConnection()
                'ใช้ยอดคงเหลือจากรายการล่าสุดเป็นหลัก เพราะข้อมูลเดิมอาจเก็บ
                'CupIn/FreeCupIn คนละรูปแบบ การ SUM ทุกแถวจะทำให้สิทธิ์คลาดเคลื่อน
                Dim sql As String =
                    "SELECT SellNumber, CupIn, FreeCupIn, FreeCupOut, " &
                    "ISNULL(CupBalance, 0) AS CupBalance, " &
                    "ISNULL(FreeCupBalance, 0) AS FreeCupBalance, " &
                    "ISNULL(Detail, '') AS Detail " &
                    "FROM CustomerFreeCup WHERE CustomerNumber = @CustomerNumber " &
                    "ORDER BY ID ASC"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                    Dim table As New DataTable()
                    Using r As SqlDataReader = cmd.ExecuteReader()
                        table.Load(r)
                    End Using
                    CustomerFreeCupBalance.ReadLatestBalance(table, cupBalance, freeCupBalance)
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetNextFreeCupID(customerNumber As Integer) As Integer
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String = "SELECT ISNULL(MAX(ID), 0) + 1 FROM CustomerFreeCup WHERE CustomerNumber = @CustomerNumber"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 5
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Return 1
        End Try
    End Function

    Private Sub InsertCustomerFreeCup(customerNumber As Integer,
                                      sellNumber As Integer,
                                      branchNumber As Integer,
                                      cupIn As Decimal,
                                      freeCupIn As Integer,
                                      freeCupOut As Integer,
                                      freeCupAmount As Decimal,
                                      cupBalance As Decimal,
                                      freeCupBalance As Integer,
                                      detail As String)
        EnsureCustomerFreeCupTable()

        Dim nextId As Integer = GetNextFreeCupID(customerNumber)

        Using conn As SqlConnection = modVariable.OpenConnection()
            Dim sql As String =
                "INSERT INTO CustomerFreeCup " &
                "(CustomerNumber, ID, SellNumber, SellBranchNumber, CupIn, FreeCupIn, FreeCupOut, FreeCupAmount, CupBalance, FreeCupBalance, Detail, CTime) " &
                "VALUES (@CustomerNumber, @ID, @SellNumber, @SellBranchNumber, @CupIn, @FreeCupIn, @FreeCupOut, @FreeCupAmount, @CupBalance, @FreeCupBalance, @Detail, GETDATE())"

            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandTimeout = 5
                cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = nextId
                cmd.Parameters.Add("@SellNumber", SqlDbType.Int).Value = sellNumber
                cmd.Parameters.Add("@SellBranchNumber", SqlDbType.Int).Value = branchNumber
                Dim cupInParam As SqlParameter = cmd.Parameters.Add("@CupIn", SqlDbType.Decimal)
                cupInParam.Precision = 18
                cupInParam.Scale = 2
                cupInParam.Value = cupIn
                cmd.Parameters.Add("@FreeCupIn", SqlDbType.Int).Value = freeCupIn
                cmd.Parameters.Add("@FreeCupOut", SqlDbType.Int).Value = freeCupOut
                Dim freeCupAmountParam As SqlParameter = cmd.Parameters.Add("@FreeCupAmount", SqlDbType.Decimal)
                freeCupAmountParam.Precision = 18
                freeCupAmountParam.Scale = 2
                freeCupAmountParam.Value = freeCupAmount
                Dim cupBalanceParam As SqlParameter = cmd.Parameters.Add("@CupBalance", SqlDbType.Decimal)
                cupBalanceParam.Precision = 18
                cupBalanceParam.Scale = 2
                cupBalanceParam.Value = cupBalance
                cmd.Parameters.Add("@FreeCupBalance", SqlDbType.Int).Value = freeCupBalance
                cmd.Parameters.Add("@Detail", SqlDbType.NVarChar, 500).Value = detail
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub ProcessCustomerFreeCup(branchNumber As Integer, sellNumber As Integer)
        Try
            If customerId <= 1 Then
                'บัตรกระดาษไม่มีชื่อลูกค้า แต่ยังต้องบันทึกการใช้สิทธิ์ลงตาราง
                'ใช้ CustomerNumber=0 เป็นรายการสิทธิ์แบบไม่ผูกสมาชิก
                If useFreeCupInCurrentBill AndAlso freeCupUseQty > 0 Then
                    InsertCustomerFreeCup(0, sellNumber, branchNumber, 0D, 0, freeCupUseQty, GetFreeCupDiscountAmount(freeCupUseQty), 0D, 0,
                                          "ใช้สิทธิแก้วฟรี " & freeCupUseQty.ToString("N0") & " ใบ (บัตรกระดาษ)")
                End If
                Exit Sub
            End If

            Dim cupQty As Decimal = GetBeverageCupQtyInCurrentBill()

            Dim oldCupBalance As Decimal = 0D
            Dim oldFreeCupBalance As Integer = 0
            GetLastFreeCupBalance(customerId, oldCupBalance, oldFreeCupBalance)

            If cupQty <= 0D AndAlso oldFreeCupBalance <= 0 Then Exit Sub

            'แก้วที่ใช้สิทธิ์ฟรีไม่ถือเป็นแก้วที่ซื้อสะสม
            Dim freeCupQtyInBill As Integer = If(useFreeCupInCurrentBill, Math.Max(0, freeCupUseQty), 0)
            Dim paidCupQty As Decimal = Math.Max(0D, cupQty - freeCupQtyInBill)

            Dim totalCup As Decimal = oldCupBalance + paidCupQty
            Dim earnedFreeCup As Integer = CInt(Math.Floor(totalCup / FreeCupTargetQty))
            Dim newCupBalance As Decimal = totalCup - (earnedFreeCup * FreeCupTargetQty)
            Dim newFreeCupBalance As Integer = oldFreeCupBalance + earnedFreeCup
            Dim freeCupOut As Integer = 0

            If newFreeCupBalance > 0 Then
                If useFreeCupInCurrentBill Then
                    freeCupOut = Math.Min(Math.Max(1, freeCupUseQty), newFreeCupBalance)
                    newFreeCupBalance -= freeCupOut
                    AppendPromotionToComment("ใช้สิทธิแก้วฟรี " & freeCupOut.ToString("N0") & " ใบ")
                End If
            End If

            Dim detail As String =
                "ซื้อเครื่องดื่มสะสม " & paidCupQty.ToString("N2") & " แก้ว" &
                " | ใช้แก้วฟรีในบิล " & freeCupOut.ToString("N0") & " ใบ" &
                " | สะสมคงเหลือ " & newCupBalance.ToString("N2") & "/" & FreeCupTargetQty.ToString("N0") & " แก้ว" &
                " | ได้สิทธิใหม่ " & earnedFreeCup.ToString("N0") & " ใบ" &
                " | ใช้สิทธิ " & freeCupOut.ToString("N0") & " ใบ" &
                " | สิทธิคงเหลือ " & newFreeCupBalance.ToString("N0") & " ใบ"

            InsertCustomerFreeCup(customerId, sellNumber, branchNumber, paidCupQty, earnedFreeCup, freeCupOut, GetFreeCupDiscountAmount(freeCupOut), newCupBalance, newFreeCupBalance, detail)

            If freeCupOut > 0 Then
                InsertSellPromotion(branchNumber, sellNumber, "แก้วฟรี | " & detail)
            End If
        Catch ex As Exception
            Throw New Exception("บันทึกสิทธิแก้วฟรีไม่สำเร็จ: " & ex.Message, ex)
        End Try
    End Sub

    Private Sub HandlePaymentAndPrint(payName As String, usePromotion As Boolean, promotionName As String)
        AppendPromotionToComment(promotionName)

        ' บันทึกบิลก่อน
        SaveSellBill()

        If flag1 = False Then Exit Sub

        Dim sellNo As Integer
        If Not Integer.TryParse(lbSellNumber.Text.Trim(), sellNo) Then
            MessageBox.Show("ไม่พบเลขที่บิลหลังบันทึกขาย", "แจ้งเตือน",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        InsertSellCustomerCertified(1, sellNo, payName)

        If usePromotion Then
            InsertSellPromotion(1, sellNo, BuildPromotionDetail(promotionName))
        End If

        'สิทธิ์จากบัตรกระดาษไม่มี CustomerFreeCup จึงบันทึกไว้ใน SellPromotion
        'เพื่อให้รายงานสรุปจำนวน/ยอดเงินแก้วฟรีมองเห็นรายการนี้ได้
        If useFreeCupInCurrentBill AndAlso customerId <= 1 Then
            InsertSellPromotion(1, sellNo, "ใช้สิทธิแก้วฟรี 1 ใบ (บัตรกระดาษ)")
        End If

        Dim freeCupErrorMessage As String = ""
        Try
            ProcessCustomerFreeCup(1, sellNo)
        Catch ex As Exception
            freeCupErrorMessage = ex.Message
        End Try

        ' สร้างข้อมูลใบเสร็จ
        Dim rp As ReceiptData = BuildReceiptData(payName)

        ' พิมพ์
        PrintReceipt(rp)

        ClearSaleScreen()

        If freeCupErrorMessage <> "" Then
            MessageBox.Show(Me, freeCupErrorMessage & vbCrLf & "บิลถูกบันทึกและพิมพ์ใบเสร็จแล้ว",
                            "Free Cup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private qrForm As fQRPayment
    Private Sub qrForm_FormClosed(sender As Object, e As FormClosedEventArgs)
        Dim ff As fQRPayment = DirectCast(sender, fQRPayment)

        If ff.DialogResult = DialogResult.OK Then
            dCashReceiveMoney = 0D
            dCashChangeMoney = 0D
            dCashPayMoney = 0D
            dPayCash = 0D

            HandlePaymentAndPrint("โอน [002203005517]", ff.UsePromotion, ff.PromotionName)
        End If

        If customerDisplay IsNot Nothing AndAlso Not customerDisplay.IsDisposed Then
            ' customerDisplay.ShowSalePanel()
        End If

        RemoveHandler ff.FormClosed, AddressOf qrForm_FormClosed
        qrForm = Nothing
    End Sub
    Private Function BuildReceiptData(payByText As String) As ReceiptData
        Dim rp As New ReceiptData()

        rp.CompanyName = "Smile Heart Foods Co.,Ltd."
        rp.BranchName = "สำนักงานใหญ่"
        rp.Address = "19/8 ม.8 ต.บางกระเจ้า อ.เมือง จ.สมุทรสาคร "
        rp.Tel = "034869080"
        rp.Fax = "034869075"
        rp.TaxId = "0745543001049"
        rp.PosId = "E067400002A1007"

        rp.InvoiceNo = tbBillNumber.Text.Trim()
        rp.SaleDate = DateTime.Now
        rp.CashierName = OfficerName
        rp.PayBy = payByText
        rp.Remark = tbComment.Text.Trim()

        rp.CustomerCode = customerCode
        rp.CustomerName = lbCustomerName.Text
        rp.CustomerTel = customerMobile

        'ยอดรวมสินค้าจริงก่อนส่วนลด (รวม VAT) เช่น 360 + 30 = 390
        rp.GrossTotal = Decimal.Parse(lbMoney.Text)
        'SubTotal เป็นยอดก่อน VAT ที่ใช้คำนวณภาษี จึงไม่ควรนำไปแสดงเป็นยอดรวมสินค้า
        rp.SubTotal = Decimal.Parse(lbTotalPrice.Text)
        rp.DiscountPer = Decimal.Parse(txtDiscountPercent.Text)
        rp.DiscountBath = Decimal.Parse(tbDCCash.Text)
        rp.VatAmount = Decimal.Parse(txtVat.Text)
        rp.GrandTotal = Decimal.Parse(txtTotalMoney.Text)
        rp.ReceiveAmount = dCashReceiveMoney
        rp.ChangeAmount = dCashChangeMoney

        ' ดึงรายการสินค้าจากตารางขายที่ผูกกับ KryptonDataGridView
        If dtSell Is Nothing Then Return rp

        For Each sellRow As DataRow In dtSell.Rows
            If sellRow.RowState = DataRowState.Deleted Then Continue For

            Dim item As New ReceiptItem()

            item.ProductCode = Convert.ToString(sellRow("ProductCode"))
            item.ProductName = Convert.ToString(sellRow("ProductName"))
            item.Barcode = Convert.ToString(sellRow("Barcode"))
            item.Sunit = Convert.ToString(sellRow("SUnit"))

            Decimal.TryParse(Convert.ToString(sellRow("Qty")), item.Qty)
            Decimal.TryParse(Convert.ToString(sellRow("Price")), item.Price)
            Decimal.TryParse(Convert.ToString(sellRow("Amount")), item.Amount)

            rp.Items.Add(item)
        Next

        Return rp
    End Function
    Private Sub PrintReceipt(rp As ReceiptData)
        currentReceipt = rp

        Dim pd As New Printing.PrintDocument()
        Dim preferredPrinter As String = "EPSON TM-U220II Receipt"

        ' Dim preferredPrinter As String = "Snagit 11"
        Dim selectedPrinter As String = ""

        'ใช้ชื่อเครื่องพิมพ์ที่ตั้งค่าไว้ ถ้ามีอยู่ในเครื่องนี้จริง
        For Each installedPrinter As String In Printing.PrinterSettings.InstalledPrinters
            If String.Equals(installedPrinter.Trim(), preferredPrinter,
                             StringComparison.OrdinalIgnoreCase) Then
                selectedPrinter = installedPrinter
                Exit For
            End If
        Next

        'ถ้าไม่พบ ให้ใช้เครื่องพิมพ์ Default ของ Windows แทน
        If String.IsNullOrWhiteSpace(selectedPrinter) Then
            selectedPrinter = pd.PrinterSettings.PrinterName
        End If

        If String.IsNullOrWhiteSpace(selectedPrinter) Then
            MessageBox.Show(Me,
                            "ไม่พบเครื่องพิมพ์ที่ใช้งานได้ กรุณาติดตั้งหรือเลือกเครื่องพิมพ์ก่อนพิมพ์ใบเสร็จ",
                            "เครื่องพิมพ์",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End If

        Try
            pd.PrinterSettings.PrinterName = selectedPrinter
        Catch ex As Printing.InvalidPrinterException
            MessageBox.Show(Me,
                            "ไม่สามารถใช้งานเครื่องพิมพ์ " & selectedPrinter & " ได้: " & ex.Message,
                            "เครื่องพิมพ์",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End Try

        If Not pd.PrinterSettings.IsValid Then
            MessageBox.Show(Me,
                            "เครื่องพิมพ์ " & selectedPrinter & " ไม่พร้อมใช้งาน",
                            "เครื่องพิมพ์",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End If

        pd.DefaultPageSettings.Margins = New Printing.Margins(0, 0, 0, 0)

        AddHandler pd.PrintPage, AddressOf PrintPageHandler
        pd.Print()
    End Sub

    'เปิดให้หน้า Daily Sales ใช้รูปแบบการพิมพ์ใบเสร็จเดียวกับหน้าขายปัจจุบัน
    Public Sub PrintReceiptData(rp As ReceiptData)
        If rp Is Nothing Then Return
        PrintReceipt(rp)
    End Sub

    Private dCashReceiveMoney As Decimal = 0D
    Private dCashChangeMoney As Decimal = 0D
    Private dCashPayMoney As Decimal = 0D
    Private dPayCash As Decimal = 0D
    Private currentReceipt As frmReceiptPreview.ReceiptData

    Private Sub PrintPageHandler(sender As Object, e As Printing.PrintPageEventArgs)
        If currentReceipt Is Nothing Then Exit Sub

        Dim rp = currentReceipt

        Dim fHead As New Font("Tahoma", 11, FontStyle.Bold)
        Dim fBody As New Font("Tahoma", 8.5, FontStyle.Regular)
        Dim fBold As New Font("Tahoma", 8.5, FontStyle.Bold)

        Dim sfCenter As New StringFormat()
        sfCenter.Alignment = StringAlignment.Center

        Dim sfRight As New StringFormat()
        sfRight.Alignment = StringAlignment.Far

        Dim leftX As Integer = 0
        Dim rightX As Integer = 250
        Dim centerX As Integer = 125
        Dim y As Integer = 0
        Dim lh As Integer = 18
        Dim totalUnit As Decimal = 0D

        ' ===== Header =====
        e.Graphics.DrawString("ใบเสร็จรับเงิน/ใบกำกับภาษีอย่างย่อ", fHead, Brushes.Black, centerX, y, sfCenter)
        y += lh
        y += lh

        e.Graphics.DrawString(rp.CompanyName, fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        e.Graphics.DrawString(rp.BranchName, fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        If Not String.IsNullOrWhiteSpace(rp.BranchName) Then
            e.Graphics.DrawString("สาขา " & rp.BranchName, fBody, Brushes.Black, centerX, y, sfCenter)
            y += lh
        End If

        e.Graphics.DrawString(rp.Address, fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        e.Graphics.DrawString("74000", fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        e.Graphics.DrawString("Tel: " & rp.Tel & "   Fax: " & rp.Fax, fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        e.Graphics.DrawString("Tax ID : " & rp.TaxId, fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        e.Graphics.DrawString("พนักงานขาย : " & rp.CashierName, fBody, Brushes.Black, centerX, y, sfCenter)
        y += lh

        e.Graphics.DrawLine(Pens.Black, leftX, y, rightX, y)
        y += 6

        ' ===== Sale Info =====
        e.Graphics.DrawString("Invoice No.  " & "1-" & rp.InvoiceNo, fBody, Brushes.Black, leftX, y)
        y += lh

        e.Graphics.DrawString("วันที่ (เวลา)     " & rp.SaleDate.ToString("dd/MM/yyyy") & " (" & rp.SaleDate.ToString("HH:mm") & ")", fBody, Brushes.Black, leftX, y)
        y += lh

        e.Graphics.DrawString("ชำระโดย        " & rp.PayBy, fBody, Brushes.Black, leftX, y)
        y += lh

        e.Graphics.DrawLine(Pens.Black, leftX, y, rightX, y)
        y += 8

        ' ===== Customer =====
        If Not String.IsNullOrWhiteSpace(rp.CustomerName) AndAlso rp.CustomerName <> "ลูกค้าทั่วไป" Then
            e.Graphics.DrawString("รหัสลูกค้า : " & rp.CustomerCode, fBody, Brushes.Black, leftX, y)
            y += lh

            e.Graphics.DrawString("ชื่อลูกค้า : " & rp.CustomerName, fBody, Brushes.Black, leftX, y)
            y += lh

            e.Graphics.DrawString("Tel : " & rp.CustomerTel, fBody, Brushes.Black, leftX, y)
            y += lh

            e.Graphics.DrawLine(Pens.Black, leftX, y, rightX, y)
            y += 6
        End If

        ' ===== Items =====
        e.Graphics.DrawString("รายการสินค้า (Product Description)", fBold, Brushes.Black, leftX, y)
        y += lh

        Dim i As Integer = 1
        For Each item In rp.Items
            Dim qtyText As String = item.Qty.ToString("N0")
            Dim priceText As String = item.Price.ToString("N2")
            Dim amountText As String = (item.Qty * item.Price).ToString("N2")
            Dim displayName As String = If(item.ProductName, "").Trim()
            Dim unitText As String = If(String.IsNullOrWhiteSpace(item.Sunit), "แพ็ค", item.Sunit.Trim())
            Dim optionStart As Integer = displayName.IndexOf(" (", StringComparison.Ordinal)
            Dim mainName As String = displayName
            Dim optionName As String = ""
            If optionStart >= 0 Then
                mainName = displayName.Substring(0, optionStart).Trim()
                optionName = displayName.Substring(optionStart).Trim()
            End If

            If Not String.IsNullOrWhiteSpace(item.Barcode) Then
                'บรรทัดแรกแสดงจำนวนและบาร์โค้ดเท่านั้น ไม่แสดงราคาใน [ ]
                e.Graphics.DrawString(qtyText & " (" & item.Barcode & ")", fBody, Brushes.Black, leftX + 10, y)
                y += lh

                'ย้ายราคาไปไว้บรรทัดชื่อสินค้า และระบุหน่วยเป็นแพ็ค
                e.Graphics.DrawString("      " & mainName & " (" & priceText & " / " & unitText & ")", fBody, Brushes.Black, leftX, y)
                e.Graphics.DrawString(amountText, fBody, Brushes.Black, rightX, y, sfRight)
                If optionName <> "" Then
                    y += lh
                    e.Graphics.DrawString("         " & optionName, fBody, Brushes.Black, leftX, y)
                End If
            Else
                'สินค้าที่ไม่มี Barcode แสดงราคาในวงเล็บท้ายชื่อสินค้า
                e.Graphics.DrawString(qtyText & " x " & mainName & " (" & priceText & " / " & unitText & ")", fBody, Brushes.Black, leftX, y)
                e.Graphics.DrawString(amountText, fBody, Brushes.Black, rightX, y, sfRight)
                If optionName <> "" Then
                    y += lh
                    e.Graphics.DrawString("      " & optionName, fBody, Brushes.Black, leftX, y)
                End If
            End If

            y += lh


            totalUnit += item.Qty
            i += 1
        Next

        If freeCupDiscountApplied > 0D Then
            e.Graphics.DrawString("ส่วนลดแก้วฟรี", fBody, Brushes.Black, leftX, y)
            e.Graphics.DrawString("-" & freeCupDiscountApplied.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
            y += lh
        End If

        e.Graphics.DrawLine(Pens.Black, leftX, y, rightX, y)
        y += 8

        ' ===== Summary =====
        e.Graphics.DrawString("[Total Unit " & totalUnit.ToString("#,##0") & "]", fBody, Brushes.Black, leftX, y)
        y += lh

        Dim discount As Decimal = Math.Max(0D, rp.DiscountBath)
        Dim grossTotal As Decimal = Math.Max(0D, rp.GrossTotal)
        Dim netTotal As Decimal = Math.Max(0D, rp.GrandTotal)

        e.Graphics.DrawString("ยอดสินค้า (รวม VAT)", fBody, Brushes.Black, centerX, y, sfCenter)
        e.Graphics.DrawString(grossTotal.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
        y += lh

        If discount > 0D Then
            e.Graphics.DrawString("ส่วนลด (Discount) " & rp.DiscountPer.ToString("N2") & "%", fBody, Brushes.Black, centerX, y, sfCenter)
            e.Graphics.DrawString("-" & discount.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
            y += lh
        End If

        e.Graphics.DrawString("ยอดสุทธิ (รวม VAT)", fBody, Brushes.Black, centerX, y, sfCenter)
        e.Graphics.DrawString(netTotal.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
        y += lh

        e.Graphics.DrawString("VAT 7.00% (รวมอยู่ในยอด)", fBody, Brushes.Black, centerX, y, sfCenter)
        e.Graphics.DrawString(rp.VatAmount.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
        y += lh

        e.Graphics.DrawString("รวมทั้งสิ้น (Grand total)", fBold, Brushes.Black, centerX, y, sfCenter)
        e.Graphics.DrawString(rp.GrandTotal.ToString("N2"), fBold, Brushes.Black, rightX, y, sfRight)
        y += lh

        'แสดงเงินรับและเงินทอนเฉพาะกรณีชำระด้วยเงินสด
        If String.Equals(If(rp.PayBy, "").Trim(), "เงินสด", StringComparison.OrdinalIgnoreCase) Then
            e.Graphics.DrawString("รับเงินสดมา (Cash receive)", fBody, Brushes.Black, centerX, y, sfCenter)
            e.Graphics.DrawString(rp.ReceiveAmount.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
            y += lh

            e.Graphics.DrawString("เงินทอน (Change)", fBody, Brushes.Black, centerX, y, sfCenter)
            e.Graphics.DrawString(rp.ChangeAmount.ToString("N2"), fBody, Brushes.Black, rightX, y, sfRight)
            y += lh
        End If

        e.Graphics.DrawLine(Pens.Black, leftX, y, rightX, y)
        y += 6

        If Not String.IsNullOrWhiteSpace(rp.Remark) Then
            y = DrawWrappedReceiptText(e.Graphics, rp.Remark, fBody, Brushes.Black, leftX, y, rightX - leftX, lh)
            y += 4
        End If

        If discount > 0D Then
            y = DrawWrappedReceiptText(e.Graphics, "วันนี้คุณประหยัดไป " & discount.ToString("N2") & " บาท", fBody, Brushes.Black, leftX, y, rightX - leftX, lh)
            y += 4
        End If

        e.Graphics.DrawString("*** ขอบคุณที่ใช้บริการ ***", fBold, Brushes.Black, centerX, y, sfCenter)
        e.HasMorePages = False

    End Sub

    Private Function DrawWrappedReceiptText(g As Graphics, text As String, font As Font, brush As Brush, x As Integer, y As Integer, width As Integer, lineHeight As Integer) As Integer
        If String.IsNullOrWhiteSpace(text) Then Return y

        Dim format As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Near,
            .Trimming = StringTrimming.Word,
            .FormatFlags = StringFormatFlags.LineLimit
        }

        Dim normalizedText As String = text.Replace(" / ", Environment.NewLine)
        Dim measuredHeight As Integer = CInt(Math.Ceiling(g.MeasureString(normalizedText, font, width).Height))
        Dim drawHeight As Integer = Math.Max(lineHeight, measuredHeight + 4)
        g.DrawString(normalizedText, font, brush, New RectangleF(x, y, width, drawHeight), format)

        Return y + drawHeight
    End Function

    Private Sub ClearSaleScreen()
        Try
            ' เคลียร์ customer
            lbCustomerName.Text = ""
            lbCustomerHead.Values.Text = "กรุณาเลือกประเภทลูกค้า"
            lbCustomerHead.StateCommon.ShortText.Color1 = Color.DarkOrange
            lbCustomerPoint.Text = "0.00 Point"
            customerId = 0
            lCustomerNumber = 0
            customerCode = ""
            customerMobile = ""
            customerSelectionConfirmed = False
            SetCustomerSelectionState(False)


            ' เคลียร์เลขบิล ถ้าคุณให้ระบบรันใหม่เอง
            tbBillNumber.Text = ""
            tbComment.Text = ""
            selectedPromotionName = ""

            customerIsCertified = False
            ApplyCustomerCertifiedPaymentRules()

            ' เคลียร์ยอดต่าง ๆ
            tbDCPercen.Text = "0.00"
            tbDCCash.Text = "0.00"
            txtDiscountPercent.Text = "0.00"
            lbPointUse.Text = "0.00"
            lbDCPoint.Text = "0.00"
            lbDCCoupon.Text = "0.00"
            tbChargePercent.Text = "0.00"
            lbMoney.Text = "0.00"
            lbTotalPrice.Text = "0.00"
            txtVat.Text = "0.00"
            txtTotalMoney.Text = "0.00"
            lbSellNumber.Text = ""
            lbDocNumber.Text = ""

            ' เคลียร์ค่ารับเงิน
            dCashReceiveMoney = 0D
            dCashChangeMoney = 0D
            dCashPayMoney = 0D
            dPayCash = 0D

            ' เคลียร์ point / ส่วนลด ถ้ามี
            num12 = 0D
            m_dPointReceive = 0D
            m_dCustomerPoint = 0D
            m_dDiscountMoney = 0D
            m_dAdjustMoney = 0D
            discountbath = 0D
            useFreeCupInCurrentBill = False
            freeCupDiscountApplied = 0D
            freeCupUseQty = 0

            ' เคลียร์ grid และ bind ตารางขายใหม่ เพื่อให้คีย์รายการต่อแล้วโชว์ทันที
            ShowdatainGrid()
            KryptonDataGridView1.Refresh()

            ' reset ตัวแปรสถานะ
            flag1 = True
            payType = 0
            currentReceipt = Nothing

            If customerDisplay IsNot Nothing AndAlso Not customerDisplay.IsDisposed Then
                customerDisplay.ClearItems()
                customerDisplay.ClearCustomerInfo()
                customerDisplay.ShowDefaultScreen()
            End If

            UpdateFreeCupPanel()

            ' บังคับให้เลือกสมาชิกหรือลูกค้าทั่วไปก่อนเริ่มบิลใหม่
            If btnWalkInCustomer IsNot Nothing Then btnWalkInCustomer.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ClearSaleScreen Error")
        End Try
    End Sub

    Private Sub btnDaiySales_Click(sender As Object, e As EventArgs)
        frmDailySales.Show()
    End Sub
    Private Sub OpenCustomerDisplay()
        If Screen.AllScreens.Length > 1 Then
            If customerDisplay Is Nothing OrElse customerDisplay.IsDisposed Then
                'ล้างจอลูกค้าที่ค้างจาก fsell ตัวเก่าก่อนสร้างตัวใหม่
                Dim oldDisplays As New List(Of frmCustomerDisplay)()
                For Each opened As Form In Application.OpenForms
                    Dim oldDisplay As frmCustomerDisplay = TryCast(opened, frmCustomerDisplay)
                    If oldDisplay IsNot Nothing AndAlso Not oldDisplay.IsDisposed Then oldDisplays.Add(oldDisplay)
                Next
                For Each oldDisplay As frmCustomerDisplay In oldDisplays
                    Try
                        oldDisplay.Close()
                        oldDisplay.Dispose()
                    Catch
                    End Try
                Next

                customerDisplay = New frmCustomerDisplay()
                AddHandler customerDisplay.CustomerSelected, AddressOf CustomerDisplay_CustomerSelected
                AddHandler customerDisplay.CustomerLookupFailed, AddressOf CustomerDisplay_CustomerLookupFailed
            End If
            CustomerDisplayForm = customerDisplay

            Dim screen2 As Screen = Screen.AllScreens(1)
            customerDisplay.StartPosition = FormStartPosition.Manual
            customerDisplay.Location = screen2.Bounds.Location
            customerDisplay.Size = screen2.Bounds.Size

            If Not customerDisplay.Visible Then
                customerDisplay.Show()
            End If
        End If
    End Sub

    Private Sub CloseCustomerDisplay()
        If customerDisplay Is Nothing Then Return

        Try
            RemoveHandler customerDisplay.CustomerSelected, AddressOf CustomerDisplay_CustomerSelected
            RemoveHandler customerDisplay.CustomerLookupFailed, AddressOf CustomerDisplay_CustomerLookupFailed
            If Not customerDisplay.IsDisposed Then customerDisplay.Close()
            customerDisplay.Dispose()
        Catch
        Finally
            customerDisplay = Nothing
            CustomerDisplayForm = Nothing
        End Try
    End Sub
    Private Sub UpdateCustomerDisplay()
        If Screen.AllScreens.Length <= 1 Then Exit Sub
        If customerDisplay Is Nothing OrElse customerDisplay.IsDisposed Then
            OpenCustomerDisplay()
        End If
        If customerDisplay Is Nothing OrElse customerDisplay.IsDisposed Then Exit Sub

        customerDisplay.ClearItems()

        If dtSell Is Nothing Then
            customerDisplay.SetTotal(0D, 0D, 0D)
            Return
        End If

        Dim total As Decimal = 0D

        For Each r As DataRow In dtSell.Rows

            Dim name As String = r("ProductName").ToString()
            Dim qty As Decimal = Convert.ToDecimal(r("Qty"))
            Dim price As Decimal = Convert.ToDecimal(r("Price"))
            Dim amount As Decimal = Convert.ToDecimal(r("Amount"))

            customerDisplay.AddItem(name, qty, price, amount)

            total += amount
        Next

        Dim discount As Decimal = 0D
        Decimal.TryParse(tbDCCash.Text, discount)

        Dim totalM As Decimal = 0D
        Decimal.TryParse(txtTotalMoney.Text, totalM)

        customerDisplay.SetTotal(total, discount, totalM)

    End Sub
    Private Sub ShowQRToCustomerDisplay(qrImage As Image, amount As Decimal)
        If customerDisplay Is Nothing OrElse customerDisplay.IsDisposed Then Exit Sub

        customerDisplay.ShowQRPanel(qrImage, amount)
    End Sub

    Private Sub btnFreecup_Click(sender As Object, e As EventArgs)
        MessageBox.Show("ยังไม่พบฟอร์มสรุป Free Cup ในโปรเจกต์นี้", "Free Cup", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnVatbuy_Click(sender As Object, e As EventArgs)
        frmVatBuy.Show()
    End Sub

    Private Sub btnVatsell_Click(sender As Object, e As EventArgs)
        frmVatSell.Show()
    End Sub
    Private Sub btnPromotionManage_Click_1(sender As Object, e As EventArgs)
        Using frm As New fPromotionManage()
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub bCustomerBarcod_Click(sender As Object, e As EventArgs) Handles bCustomerBarcod.Click
        Try
            SetCustomerSelectionState(False)
            If customerDisplay Is Nothing OrElse customerDisplay.IsDisposed Then
                customerDisplay = New frmCustomerDisplay()
                AddHandler customerDisplay.CustomerSelected, AddressOf CustomerDisplay_CustomerSelected
                AddHandler customerDisplay.CustomerLookupFailed, AddressOf CustomerDisplay_CustomerLookupFailed
                CustomerDisplayForm = customerDisplay

                customerDisplay.StartPosition = FormStartPosition.Manual
                customerDisplay.Location = Screen.AllScreens(1).WorkingArea.Location
                customerDisplay.Show()
            End If
            SetCustomerPhoneWaitingStatus(True)
            customerDisplay.ShowPhoneScreen()

        Catch ex As Exception
            SetCustomerPhoneWaitingStatus(False)
        End Try
    End Sub

    Private Sub btCustomer_Click(sender As Object, e As EventArgs) Handles btCustomer.Click

        Using f As New fCustomer()
            If f.ShowDialog() = DialogResult.OK Then
                customerId = f.CustomerNumber
                lbCustomerName.Text = f.SelectedCustomerName
                customerCode = f.CustomerCode.Trim
                Call GetCustomerData(customerId)
                lbCustomerHead.Text = "ลูกค้า [สมาชิก Level " & ordinal5 & "]"
                lbCustomerPoint.Text = Format(ordinal10, "0.00") & " Point "
                m_dCustomerPoint = ordinal10
                lCustomerNumber = customerId
                SetCustomerSelectionState(True)
                UpdateFreeCupPanel()
                If customerDisplay Is Nothing OrElse customerDisplay.IsDisposed Then OpenCustomerDisplay()
                If customerDisplay IsNot Nothing AndAlso Not customerDisplay.IsDisposed Then
                    customerDisplay.SetCustomerInfo(customerId, f.SelectedCustomerName, ordinal10)
                End If
            End If

            Dim textValue As String = lbPointUse.Text.Replace("Point", "").Trim()
            Decimal.TryParse(textValue, num12)

        End Using
    End Sub

    Private Sub btnWalkInCustomer_Click(sender As Object, e As EventArgs) Handles btnWalkInCustomer.Click
        SelectWalkInCustomer()
    End Sub

    Private Sub tbAutoInput_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbAutoInput.KeyDown
        If e.KeyCode <> Keys.Enter Then Exit Sub

        If Not EnsureCustomerSelected() Then Exit Sub

        If _busyAutoInput Then Exit Sub
        _busyAutoInput = True

        Try
            Dim kw As String = tbAutoInput.Text.Trim()
            If kw = "" Then Exit Sub

            OpenProductByName(kw)

            tbAutoInput.Clear()
            tbAutoInput.Focus()
        Finally
            _busyAutoInput = False
        End Try
    End Sub

    Private Sub btCash_Click_1(sender As Object, e As EventArgs) Handles btCash.Click
        payType = 1

        Dim usePromotion As Boolean = False
        Dim promotionName As String = ""
        If Not TrySelectPromotionForPayment(usePromotion, promotionName) Then Exit Sub

        'คำนวณยอดที่ต้องชำระโดยตรงหลังใช้โปรโมชั่นแล้ว
        'เช่น ยอดสินค้า 570 - ส่วนลด 5 = ยอดที่ต้องชำระ 565 บาท
        Dim billTotal As Decimal = 0D
        Dim discountAfterPromotion As Decimal = 0D
        If Not Decimal.TryParse(lbMoney.Text.Replace(",", ""), billTotal) OrElse
           Not Decimal.TryParse(tbDCCash.Text.Replace(",", ""), discountAfterPromotion) Then
            MessageBox.Show(Me, "ไม่สามารถอ่านยอดสุทธิที่ต้องชำระได้", "ชำระเงินสด", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim total As Decimal = Math.Max(0D, billTotal - discountAfterPromotion)

        Using f As New fReceiveMoney()
            f.TotalDue = total
            f.CustomerDisplayForm = customerDisplay   ' ⭐ ต้องมีบรรทัดนี้
            If f.ShowDialog() = DialogResult.OK Then
                dCashReceiveMoney = f.ReceiveMoney
                dCashChangeMoney = f.ChangeMoney
                dCashPayMoney = dCashReceiveMoney - dCashChangeMoney
                dPayCash = dCashPayMoney

                HandlePaymentAndPrint("เงินสด", usePromotion, promotionName)
            End If
        End Using
    End Sub

    Private Sub btnTransfer_Click_1(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Try
            RefreshCustomerCertifiedStatus()
            If customerIsCertified Then
                MessageBox.Show(Me, "ลูกค้ารับรองต้องชำระด้วยเงินสดเท่านั้น", "รับรองลูกค้า",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            payType = 4 ' โอนเงิน
            Dim usePromotion As Boolean = False
            Dim promotionName As String = ""
            Dim tempRef As String = GenerateTempRefNo()

            If Not TrySelectPromotionForPayment(usePromotion, promotionName) Then Exit Sub

            qrForm = New fQRPayment()

            qrForm.Amount = Decimal.Parse(txtTotalMoney.Text)
            qrForm.BillNo = tempRef

            qrForm.BankName = "ธนาคารกรุงเทพ"
            qrForm.AccountNo = "923-300-3871"
            qrForm.AccountName = "SMILE HEART FOODS CO.,LTD."
            'ค้นหา QR ตามโครงสร้างไฟล์จริงของโปรแกรม และรองรับโครงสร้างเดิมด้วย
            qrForm.QrImagePath = ResolveQrImagePath()

            ' Path เดิม เก็บไว้ดูอ้างอิง
            ' qrForm.QrImagePath = "D:\SMH_Program\Coffee_SMH2\Coffee_SMH2\bin\Debug\Image\bank_qr.png"
            ' qrForm.QrImagePath = "C:\Users\L\Desktop\Coffee_SMH2\Coffee_SMH2\bin\Debug\Image\bank_qr.png"

            qrForm.CustomerDisplayForm = customerDisplay   ' ส่งจอลูกค้าเข้าไป
            qrForm.UsePromotion = usePromotion             ' ถ้าคุณอยากเก็บไว้ใช้ต่อ
            qrForm.PromotionName = promotionName
            qrForm.Owner = Me

            AddHandler qrForm.FormClosed, AddressOf qrForm_FormClosed

            qrForm.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub

    Private Function ResolveQrImagePath() As String
        Dim candidates As String() = {
            IO.Path.Combine(Application.StartupPath, "CustomerAds", "Image", "QRCode_pay.png"),
            IO.Path.Combine(Application.StartupPath, "Image", "QRCode_pay.png"),
            IO.Path.Combine(Application.StartupPath, "CustomerAds", "Image", "bank_qr.png"),
            IO.Path.Combine(Application.StartupPath, "Image", "bank_qr.png")
        }

        For Each candidate As String In candidates
            If IO.File.Exists(candidate) Then Return candidate
        Next

        'คืนตำแหน่งแรกไว้ให้ฟอร์มแสดงข้อความแจ้งเตือนที่ตรงกับไฟล์ที่ควรมี
        Return candidates(0)
    End Function

End Class

Friend NotInheritable Class PromotionStore
    Private Sub New()
    End Sub

    Private Shared ReadOnly Property PromotionFilePath As String
        Get
            Return IO.Path.Combine(Application.StartupPath, "promotions.txt")
        End Get
    End Property

    Private Shared Function GetDefaultPromotions() As List(Of String)
        Return New List(Of String) From {
            "ครบ 499 บาท รับฟรีกระเป๋าเก็บความเย็น",
            "ครบ 399 บาท รับฟรีผ้ากันเปื้อน",
            "ซื้อแข็งครบ 299 บาท รับฟรีข้าวไรซ์เบอร์รี่ 1 กก.",
            "ซื้อมาม่ามัทฉะ รับส่วนลด 5 บาท",
            "ครบ 250 บาท ลุ้นรับรางวัล",
            "อื่น ๆ / โปรโมชั่นหน้าร้าน"
        }
    End Function

    Public Shared Function LoadPromotions() As List(Of String)
        Try
            If Not IO.File.Exists(PromotionFilePath) Then
                '  SavePromotions(GetDefaultPromotions())
            End If

            Return IO.File.ReadAllLines(PromotionFilePath, System.Text.Encoding.UTF8).
                Select(Function(line) line.Trim()).
                Where(Function(line) line <> "").
                Distinct().
                ToList()
        Catch ex As Exception
            MessageBox.Show("อ่านไฟล์โปรโมชั่นไม่สำเร็จ: " & ex.Message, "Promotion Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return GetDefaultPromotions()
        End Try
    End Function

    Public Shared Sub SavePromotions(promotions As IEnumerable(Of String))
        Dim lines = promotions.
            Select(Function(line) If(line, "").Trim()).
            Where(Function(line) line <> "").
            Distinct().
            ToArray()

        IO.File.WriteAllLines(PromotionFilePath, lines, System.Text.Encoding.UTF8)
    End Sub

    Public Shared ReadOnly Property DisplayPath As String
        Get
            Return PromotionFilePath
        End Get
    End Property
End Class

Friend Class fPromotionManage
    Inherits Form

    Private ReadOnly txtPromotions As TextBox
    Private ReadOnly lblPath As Label

    Public Sub New()
        Me.Text = "จัดการโปรโมชั่น"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ClientSize = New Size(720, 520)
        Me.Font = New Font("Segoe UI", 11.0!, FontStyle.Regular)

        Dim lblTitle As New Label()
        lblTitle.Text = "เพิ่ม/แก้ไขโปรโมชั่น"
        lblTitle.Font = New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold)
        lblTitle.Location = New Point(18, 16)
        lblTitle.Size = New Size(680, 34)
        Me.Controls.Add(lblTitle)

        Dim lblHelp As New Label()
        lblHelp.Text = "ใส่โปรโมชั่น 1 รายการต่อ 1 บรรทัด แล้วกดบันทึก"
        lblHelp.Location = New Point(20, 54)
        lblHelp.Size = New Size(680, 28)
        Me.Controls.Add(lblHelp)

        txtPromotions = New TextBox()
        txtPromotions.Multiline = True
        txtPromotions.ScrollBars = ScrollBars.Vertical
        txtPromotions.AcceptsReturn = True
        txtPromotions.AcceptsTab = True
        txtPromotions.Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
        txtPromotions.Location = New Point(22, 92)
        txtPromotions.Size = New Size(676, 320)
        Me.Controls.Add(txtPromotions)

        lblPath = New Label()
        lblPath.Text = "ไฟล์: " & PromotionStore.DisplayPath
        lblPath.Font = New Font("Segoe UI", 8.5!, FontStyle.Regular)
        lblPath.ForeColor = Color.DimGray
        lblPath.Location = New Point(22, 420)
        lblPath.Size = New Size(676, 28)
        Me.Controls.Add(lblPath)

        Dim btnSave As New Button()
        btnSave.Text = "บันทึก"
        btnSave.Size = New Size(120, 44)
        btnSave.Location = New Point(448, 458)
        btnSave.BackColor = Color.FromArgb(38, 166, 154)
        btnSave.ForeColor = Color.White
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.FlatAppearance.BorderSize = 0
        AddHandler btnSave.Click, AddressOf btnSave_Click
        Me.Controls.Add(btnSave)

        Dim btnClose As New Button()
        btnClose.Text = "ปิด"
        btnClose.Size = New Size(120, 44)
        btnClose.Location = New Point(578, 458)
        btnClose.DialogResult = DialogResult.Cancel
        Me.Controls.Add(btnClose)

        Me.AcceptButton = btnSave
        Me.CancelButton = btnClose
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        txtPromotions.Text = String.Join(Environment.NewLine, PromotionStore.LoadPromotions())
        txtPromotions.Select()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim promotions = txtPromotions.Lines.
            Select(Function(line) line.Trim()).
            Where(Function(line) line <> "").
            ToList()

        If promotions.Count = 0 Then
            MessageBox.Show("กรุณาใส่โปรโมชั่นอย่างน้อย 1 รายการ", "แจ้งเตือน",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        PromotionStore.SavePromotions(promotions)
        MessageBox.Show("บันทึกโปรโมชั่นเรียบร้อย", "สำเร็จ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub


End Class

















