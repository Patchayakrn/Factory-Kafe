Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmProductAdd
    Private Const BranchNumber As Integer = 1
    Private Const BeverageGroupKeyword As String = "เครื่องดื่ม"
    Private selectedPicturePath As String = ""
    Public Property CloneSourcePN As Long
    Public Property EditProductPN As Long
    Public Property IsBeverageFromProductList As Boolean
    Public Property BeverageCategoryNameFromProductList As String = ""
    Public Property BeverageGroupNameFromProductList As String = ""

    Private Sub FitToWorkingArea()
        'The designer layout is larger than a typical user monitor.
        'Restore the design size first because the caller may have maximized this form.
        Const designWidth As Integer = 1814
        Const designHeight As Integer = 1124

        Dim workArea As Rectangle = Screen.FromControl(Me).WorkingArea
        Dim availableWidth As Integer = Math.Max(800, workArea.Width - 24)
        Dim availableHeight As Integer = Math.Max(560, workArea.Height - 48)

        WindowState = FormWindowState.Normal
        AutoScroll = True
        ClientSize = New Size(designWidth, designHeight)
        'Do not scale the child controls: this legacy layout has fixed columns,
        'and scaling them makes labels/textboxes overlap. Keep the layout intact
        'and let the user scroll on smaller screens.
        AutoScrollMinSize = New Size(designWidth, designHeight)
        ClientSize = New Size(availableWidth, availableHeight)
        StartPosition = FormStartPosition.CenterParent
        WindowState = FormWindowState.Maximized
    End Sub

    Private Sub PositionActionButtons()
        If grpInformationSingle Is Nothing OrElse btnSave Is Nothing OrElse btnCancel Is Nothing Then Return

        'Keep the action buttons inside the visible client area. The original
        'designer positions them near the bottom of the 1814x1124 layout.
        'grpInformationSingle keeps the original designer size for its content.
        'Use the actual visible form area for the action buttons instead.
        Dim visibleHeight As Integer = Math.Max(0, Me.ClientSize.Height - pnlHeader.Height)
        Dim visibleWidth As Integer = Math.Max(0, Me.ClientSize.Width - 28)
        Dim bottom As Integer = visibleHeight - 14
        btnCancel.Top = Math.Max(0, bottom - btnCancel.Height)
        btnSave.Top = Math.Max(0, bottom - btnSave.Height)
        btnCancel.Left = Math.Max(0, visibleWidth - btnCancel.Width)
        btnSave.Left = Math.Max(0, btnCancel.Left - btnSave.Width - 18)
    End Sub

    Private Sub FrmProductAdd_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        PositionActionButtons()
    End Sub

    Private Class ComboListItem
        Public ReadOnly Property ID As Integer
        Public ReadOnly Property Name As String

        Public Sub New(id As Integer, name As String)
            Me.ID = id
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Sub FrmProductAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FitToWorkingArea()
            PositionActionButtons()
            LoadLookupData()
            SetInitialValues()
            If EditProductPN > 0 Then
                LoadSourceProduct(EditProductPN, True)
                cbStatus.Visible = True
            ElseIf CloneSourcePN > 0 Then
                LoadSourceProduct(CloneSourcePN, False)
                cbStatus.Visible = False
            Else
                cbStatus.Visible = False
            End If
            tbProductCode.Focus()
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลสินค้าไม่สำเร็จ: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnSave.Enabled = False
        End Try
    End Sub

    Private Sub LoadLookupData()
        LoadComboFromSql(cbProductGroup,
                         "SELECT ID, RTRIM(Name) AS Name FROM ProductGroup WHERE Status IS NULL ORDER BY Name", False)
        LoadComboFromSql(KryptonComboBox1,
                         "SELECT ID, RTRIM(Name) AS Name FROM Brand WHERE Status IS NULL ORDER BY Name", True)
        LoadComboFromSql(KryptonComboBox2,
                         "SELECT ID, RTRIM(Name) AS Name FROM ProductVolume WHERE Status IS NULL ORDER BY Name", True)

        cbSUnit.Items.Clear()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT RTRIM(UnitName) AS UnitName FROM Unit ORDER BY UnitName", conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim unitName As String = reader("UnitName").ToString().Trim()
                        If unitName <> "" Then cbSUnit.Items.Add(unitName)
                    End While
                End Using
            End Using
        End Using
        If cbSUnit.Items.Count = 0 Then cbSUnit.Items.Add("ชิ้น")
        cbSUnit.SelectedIndex = 0
    End Sub

    Private Sub LoadComboFromSql(combo As Krypton.Toolkit.KryptonComboBox, sqlText As String, addBlank As Boolean)
        combo.Items.Clear()
        If addBlank Then combo.Items.Add(New ComboListItem(0, ""))

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sqlText, conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        combo.Items.Add(New ComboListItem(Convert.ToInt32(reader("ID")),
                                                          reader("Name").ToString().Trim()))
                    End While
                End Using
            End Using
        End Using

        If combo.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Sub SetInitialValues()
        txtEditCode.ReadOnly = True
        txtEditCode.Text = ""
        chkEditAutoCode.Checked = True
        cbVatStatus.Checked = True
        cbAllowDecimal.Checked = False
        cbHotProduct.Checked = False
        cbAlert.Checked = False
        cbCanCollectPoint.Checked = False
        cbStatus.Checked = False
        cbStatus.Visible = False
        tbBarcode.Enabled = False
        SetAlertControlsEnabled(False)
        lbPicName.Text = "ยังไม่ได้เลือกรูป"
    End Sub

    Private Function IsBeverageGroup() As Boolean
        Dim groupName As String = SelectedComboName(cbProductGroup)
        Return IsBeverageFromProductList OrElse
               BeverageCategoryNameFromProductList.IndexOf(BeverageGroupKeyword, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               BeverageGroupNameFromProductList.IndexOf(BeverageGroupKeyword, StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               groupName.IndexOf(BeverageGroupKeyword, StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Sub cbProductGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProductGroup.SelectedIndexChanged
        If EditProductPN > 0 Then Return

        Dim isBeverage As Boolean = IsBeverageGroup()
        'บังคับเฉพาะสินค้าใหม่ ส่วน Clone ต้องเปิดให้สร้าง Barcode ใหม่ได้
        chkEditAutoCode.Enabled = Not (isBeverage AndAlso CloneSourcePN <= 0)
        If isBeverage Then chkEditAutoCode.Checked = True
        chkEditAutoCode.Text = If(isBeverage,
                                  "สร้างบาร์โค้ดเครื่องดื่มอัตโนมัติ (EAN-13)",
                                  "สร้างบาร์โค้ดอัตโนมัติ (Same as ID)")
    End Sub

    Private Sub LoadSourceProduct(sourcePN As Long, isEditMode As Boolean)
        Const sql As String =
            "SELECT P.ProductCode,P.Name,P.Name2,P.Barcode,P.Price1,P.Price2,P.Price3,P.Price4,P.Price5," &
            "P.MinPrice,P.StandardCost,RTRIM(P.SUnit) AS SUnit,P.NumPrice2,P.NumPrice3,P.NumPrice4,P.NumPrice5," &
            "P.VolumeID,P.Length,P.Width,P.Height,P.Weight,P.GrossWeight,P.AllowDecimal," &
            "RTRIM(P.MDiscountP1String) AS MDiscountP1String,RTRIM(P.MDiscountP2String) AS MDiscountP2String," &
            "RTRIM(P.MDiscountP3String) AS MDiscountP3String,P.GroupID,P.BrandID,RTRIM(P.MainSupplier) AS MainSupplier," &
            "P.VatStatus,P.CanCollectPoint,P.Point,P.HotProduct,P.Status,RTRIM(P.PicName) AS PicName,RTRIM(P.Comment) AS Comment," &
            "RTRIM(PL.Location) AS Location,PA.NumAlert,PA.NumMustHave " &
            "FROM Product P " &
            "LEFT JOIN ProductLocation PL ON PL.PN=P.PN AND PL.BranchNumber=@BranchNumber " &
            "LEFT JOIN ProductAlert PA ON PA.PN=P.PN AND PA.BranchNumber=@BranchNumber AND PA.Status IS NULL " &
            "WHERE P.PN=@PN"

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = sourcePN
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If Not reader.Read() Then Throw New ApplicationException("ไม่พบสินค้าต้นฉบับสำหรับ Clone")

                    txtEditCode.Text = If(isEditMode, sourcePN.ToString(), "")
                    lblTitle.Text = If(isEditMode, "แก้ไขสินค้า", "Clone สินค้า")
                    lblSubtitle.Text = If(isEditMode,
                                          "แก้ไขข้อมูล Product ID " & sourcePN.ToString(),
                                          "คัดลอกข้อมูลจาก Product ID " & sourcePN.ToString() & " กรุณาเปลี่ยนรหัสหรือบาร์โค้ดก่อนบันทึก")
                    Text = If(isEditMode, "แก้ไขสินค้า", "Clone สินค้า")
                    btnSave.Text = If(isEditMode, "บันทึกการแก้ไข", "บันทึก")
                    tbProductCode.Text = ReaderText(reader, "ProductCode")
                    tbName.Text = ReaderText(reader, "Name")
                    tbName2.Text = ReaderText(reader, "Name2")
                    tbBarcode.Text = ReaderText(reader, "Barcode")
                    chkEditAutoCode.Checked = False
                    tbBarcode.Enabled = True

                    KryptonTextBox1.Text = ReaderDecimal(reader, "Price1").ToString("0.00")
                    KryptonTextBox2.Text = ReaderDecimal(reader, "Price2").ToString("0.00")
                    KryptonTextBox3.Text = ReaderDecimal(reader, "Price3").ToString("0.00")
                    KryptonTextBox4.Text = ReaderDecimal(reader, "Price4").ToString("0.00")
                    KryptonTextBox5.Text = ReaderDecimal(reader, "Price5").ToString("0.00")
                    KryptonTextBox6.Text = ReaderDecimal(reader, "MinPrice").ToString("0.00")
                    KryptonTextBox11.Text = ReaderDecimal(reader, "StandardCost").ToString("0.00")
                    KryptonTextBox7.Text = ReaderDecimal(reader, "NumPrice2").ToString("0.00")
                    KryptonTextBox8.Text = ReaderDecimal(reader, "NumPrice3").ToString("0.00")
                    KryptonTextBox9.Text = ReaderDecimal(reader, "NumPrice4").ToString("0.00")
                    KryptonTextBox10.Text = ReaderDecimal(reader, "NumPrice5").ToString("0.00")

                    KryptonTextBox12.Text = ReaderTextOrDefault(reader, "MDiscountP1String", "0.00")
                    KryptonTextBox13.Text = ReaderTextOrDefault(reader, "MDiscountP2String", "0.00")
                    KryptonTextBox14.Text = ReaderTextOrDefault(reader, "MDiscountP3String", "0.00")
                    KryptonTextBox15.Text = ReaderDecimal(reader, "Width").ToString("0.00")
                    KryptonTextBox16.Text = ReaderDecimal(reader, "Length").ToString("0.00")
                    KryptonTextBox17.Text = ReaderDecimal(reader, "Height").ToString("0.00")
                    KryptonTextBox18.Text = ReaderDecimal(reader, "Weight").ToString("0.00")
                    KryptonTextBox19.Text = ReaderDecimal(reader, "GrossWeight").ToString("0.00")
                    KryptonTextBox23.Text = ReaderDecimal(reader, "Point").ToString("0.00")
                    KryptonTextBox22.Text = ReaderText(reader, "Location")
                    tbMainSupplier.Text = ReaderText(reader, "MainSupplier")
                    tbComment.Text = ReaderText(reader, "Comment")

                    If isEditMode Then
                        selectedPicturePath = ReaderText(reader, "PicName")
                        ShowSelectedPicture()
                        cbStatus.Checked = Not Convert.IsDBNull(reader("Status"))
                    End If

                    cbAllowDecimal.Checked = ReaderBoolean(reader, "AllowDecimal")
                    cbVatStatus.Checked = ReaderBoolean(reader, "VatStatus")
                    cbCanCollectPoint.Checked = ReaderBoolean(reader, "CanCollectPoint")
                    cbHotProduct.Checked = ReaderBoolean(reader, "HotProduct")
                    SelectComboByID(cbProductGroup, ReaderInteger(reader, "GroupID"))
                    SelectComboByID(KryptonComboBox1, ReaderInteger(reader, "BrandID"))
                    SelectComboByID(KryptonComboBox2, ReaderInteger(reader, "VolumeID"))
                    SelectComboByText(cbSUnit, ReaderText(reader, "SUnit"))

                    If Not isEditMode AndAlso IsBeverageGroup() Then
                        chkEditAutoCode.Enabled = True
                        chkEditAutoCode.Checked = True
                        tbBarcode.Text = PreviewNextBeverageBarcode()
                    End If

                    Dim hasAlert As Boolean = Not Convert.IsDBNull(reader("NumAlert"))
                    cbAlert.Checked = hasAlert
                    If hasAlert Then
                        KryptonTextBox20.Text = ReaderDecimal(reader, "NumAlert").ToString("0.00")
                        KryptonTextBox21.Text = ReaderDecimal(reader, "NumMustHave").ToString("0.00")
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub ShowSelectedPicture()
        If pbPicture.Image IsNot Nothing Then
            pbPicture.Image.Dispose()
            pbPicture.Image = Nothing
        End If
        If String.IsNullOrWhiteSpace(selectedPicturePath) OrElse Not File.Exists(selectedPicturePath) Then
            lbPicName.Text = If(String.IsNullOrWhiteSpace(selectedPicturePath), "ยังไม่ได้เลือกรูป", Path.GetFileName(selectedPicturePath))
            Return
        End If

        lbPicName.Text = Path.GetFileName(selectedPicturePath)
        Using source As Image = Image.FromFile(selectedPicturePath)
            pbPicture.Image = New Bitmap(source)
        End Using
    End Sub

    Private Function ReaderText(reader As SqlDataReader, fieldName As String) As String
        If Convert.IsDBNull(reader(fieldName)) Then Return ""
        Return Convert.ToString(reader(fieldName)).Trim()
    End Function

    Private Function ReaderTextOrDefault(reader As SqlDataReader, fieldName As String, defaultValue As String) As String
        Dim value As String = ReaderText(reader, fieldName)
        Return If(value = "", defaultValue, value)
    End Function

    Private Function ReaderDecimal(reader As SqlDataReader, fieldName As String) As Decimal
        If Convert.IsDBNull(reader(fieldName)) Then Return 0D
        Return Convert.ToDecimal(reader(fieldName))
    End Function

    Private Function ReaderInteger(reader As SqlDataReader, fieldName As String) As Integer
        If Convert.IsDBNull(reader(fieldName)) Then Return 0
        Return Convert.ToInt32(reader(fieldName))
    End Function

    Private Function ReaderBoolean(reader As SqlDataReader, fieldName As String) As Boolean
        If Convert.IsDBNull(reader(fieldName)) Then Return False
        Return Convert.ToBoolean(reader(fieldName))
    End Function

    Private Sub SelectComboByID(combo As Krypton.Toolkit.KryptonComboBox, id As Integer)
        For index As Integer = 0 To combo.Items.Count - 1
            Dim item As ComboListItem = TryCast(combo.Items(index), ComboListItem)
            If item IsNot Nothing AndAlso item.ID = id Then
                combo.SelectedIndex = index
                Return
            End If
        Next
        If combo.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Sub SelectComboByText(combo As Krypton.Toolkit.KryptonComboBox, value As String)
        Dim textValue As String = If(value, "").Trim()
        Dim index As Integer = combo.FindStringExact(textValue)
        If index < 0 AndAlso textValue <> "" Then
            combo.Items.Add(textValue)
            index = combo.Items.Count - 1
        End If
        If index >= 0 Then combo.SelectedIndex = index
    End Sub

    Private Function SelectedComboID(combo As Krypton.Toolkit.KryptonComboBox) As Integer
        Dim item As ComboListItem = TryCast(combo.SelectedItem, ComboListItem)
        Return If(item Is Nothing, 0, item.ID)
    End Function

    Private Function SelectedComboName(combo As Krypton.Toolkit.KryptonComboBox) As String
        Dim item As ComboListItem = TryCast(combo.SelectedItem, ComboListItem)
        Return If(item Is Nothing, combo.Text.Trim(), item.Name.Trim())
    End Function

    Private Function ParseDecimal(textValue As String, Optional defaultValue As Decimal = 0D) As Decimal
        Dim value As Decimal
        If Decimal.TryParse(If(textValue, "").Replace(",", "").Trim(), value) Then Return value
        Return defaultValue
    End Function

    Private Function DbText(textValue As String) As Object
        Dim value As String = If(textValue, "").Trim()
        Return If(value = "", CType(DBNull.Value, Object), value)
    End Function

    Private Function LoginOfficerNumber() As Integer
        If ModSession.LoginOfficerID > 0 Then Return Convert.ToInt32(ModSession.LoginOfficerID)
        Return 1
    End Function

    Private Function ValidateInput() As Boolean
        If tbName.Text.Trim() = "" Then Return ValidationError("กรุณาป้อนชื่อสินค้า", tbName)
        If SelectedComboID(cbProductGroup) <= 0 Then Return ValidationError("กรุณาเลือกกลุ่มสินค้า", cbProductGroup)
        If cbSUnit.Text.Trim() = "" Then Return ValidationError("กรุณาเลือกหน่วยที่เล็กที่สุดของสินค้า", cbSUnit)

        If StartsWithReservedCharacter(tbProductCode.Text) Then
            Return ValidationError("Product Code ไม่สามารถขึ้นต้นด้วยเครื่องหมาย * หรือ +", tbProductCode)
        End If
        If StartsWithReservedCharacter(tbBarcode.Text) Then
            Return ValidationError("Barcode ไม่สามารถขึ้นต้นด้วยเครื่องหมาย * หรือ +", tbBarcode)
        End If

        Dim discountError As String = ""
        If Not TryGetDiscount(KryptonTextBox12.Text, Nothing, discountError) Then
            Return ValidationError("ส่วนลดสมาชิก 1 " & discountError, KryptonTextBox12)
        End If
        If Not TryGetDiscount(KryptonTextBox13.Text, Nothing, discountError) Then
            Return ValidationError("ส่วนลดสมาชิก 2 " & discountError, KryptonTextBox13)
        End If
        If Not TryGetDiscount(KryptonTextBox14.Text, Nothing, discountError) Then
            Return ValidationError("ส่วนลดสมาชิก 3 " & discountError, KryptonTextBox14)
        End If

        Dim volumeError As String = ""
        If Not ValidatePriceVolumes(volumeError) Then
            MessageBox.Show(volumeError, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Function StartsWithReservedCharacter(value As String) As Boolean
        Dim textValue As String = If(value, "").Trim()
        Return textValue.StartsWith("*") OrElse textValue.StartsWith("+")
    End Function

    Private Function ValidationError(message As String, control As Control) As Boolean
        MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        control.Focus()
        Return False
    End Function

    Private Function TryGetDiscount(textValue As String, ByRef result As Decimal, ByRef errorMessage As String) As Boolean
        result = 0D
        errorMessage = ""
        Dim normalized As String = If(textValue, "").Replace(" ", "").Trim()
        If normalized = "" Then Return True

        Dim remainingPercent As Decimal = 100D
        For Each part As String In normalized.Split("+"c)
            Dim percent As Decimal
            If part = "" OrElse Not Decimal.TryParse(part, percent) OrElse percent < 0D OrElse percent >= 100D Then
                errorMessage = "มีรูปแบบไม่ถูกต้อง (ตัวอย่างที่ถูกต้อง: 10 หรือ 10+5)"
                Return False
            End If
            remainingPercent *= (100D - percent) / 100D
        Next

        result = Decimal.Round(100D - remainingPercent, 4)
        If result >= 100D Then
            errorMessage = "ต้องมีค่าน้อยกว่า 100%"
            Return False
        End If
        Return True
    End Function

    Private Function ValidatePriceVolumes(ByRef errorMessage As String) As Boolean
        Dim previous As Decimal = 0D
        Dim controls() As Krypton.Toolkit.KryptonTextBox = {
            KryptonTextBox7, KryptonTextBox8, KryptonTextBox9, KryptonTextBox10
        }
        Dim priceNumbers() As Integer = {2, 3, 4, 5}

        For i As Integer = 0 To controls.Length - 1
            Dim value As Decimal = ParseDecimal(controls(i).Text)
            If value > 0D Then
                If previous > 0D AndAlso value <= previous Then
                    errorMessage = "จำนวนที่ได้ราคาช่อง " & priceNumbers(i).ToString() &
                                   " ต้องมากกว่า " & previous.ToString("N2")
                    controls(i).Focus()
                    Return False
                End If
                previous = value
            End If
        Next
        Return True
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValidateInput() Then Return

        btnSave.Enabled = False
        Cursor = Cursors.WaitCursor
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    Dim isEditMode As Boolean = EditProductPN > 0
                    Dim pn As Long = If(isEditMode, EditProductPN, NextProductPN(conn, tr))
                    If chkEditAutoCode.Checked Then
                        If IsBeverageGroup() Then
                            If String.IsNullOrWhiteSpace(tbBarcode.Text) Then tbBarcode.Text = NextBeverageBarcode(conn, tr)
                        Else
                            tbBarcode.Text = pn.ToString("00000")
                        End If
                    End If

                    If IsDuplicateValue(conn, tr, "ProductCode", tbProductCode.Text, pn) Then
                        Throw New ApplicationException("Product Code นี้มีอยู่แล้ว")
                    End If
                    If IsDuplicateValue(conn, tr, "Barcode", tbBarcode.Text, pn) Then
                        Throw New ApplicationException("Barcode นี้มีอยู่แล้ว")
                    End If
                    If HasDuplicateName(conn, tr, tbName.Text, pn) Then
                        Dim answer As DialogResult = MessageBox.Show(
                            "มีชื่อสินค้านี้อยู่แล้ว คุณยังต้องการเพิ่มซ้ำหรือไม่?",
                            "ยืนยันชื่อสินค้าซ้ำ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button2)
                        If answer <> DialogResult.Yes Then Return
                    End If

                    If isEditMode Then
                        UpdateProduct(conn, tr, pn)
                    Else
                        InsertProduct(conn, tr, pn)
                    End If
                    SaveProductLocation(conn, tr, pn)
                    SaveProductAlert(conn, tr, pn)
                    tr.Commit()
                    txtEditCode.Text = pn.ToString()
                End Using
            End Using

            'เก็บสำเนารูปไว้ในโฟลเดอร์กลางของ User เพื่อให้เมนูค้นพบรูปได้แน่นอน
            If File.Exists(selectedPicturePath) Then
                Try
                    ImageAssetStore.SaveMenuImage(selectedPicturePath, SelectedComboName(cbProductGroup))
                Catch imageEx As Exception
                    MessageBox.Show("บันทึกข้อมูลสินค้าแล้ว แต่คัดลอกรูปเมนูไม่สำเร็จ: " & imageEx.Message,
                                    "รูปสินค้า", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If

            MessageBox.Show(If(EditProductPN > 0, "แก้ไขสินค้าเรียบร้อย", "เพิ่มสินค้าเรียบร้อย"), "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show(If(EditProductPN > 0, "แก้ไขสินค้าไม่สำเร็จ: ", "เพิ่มสินค้าไม่สำเร็จ: ") & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            btnSave.Enabled = True
        End Try
    End Sub

    Private Function NextProductPN(conn As SqlConnection, tr As SqlTransaction) As Long
        Using cmd As New SqlCommand("SELECT ISNULL(MAX(PN), 0) + 1 FROM Product WITH (UPDLOCK, HOLDLOCK)", conn, tr)
            Return Convert.ToInt64(cmd.ExecuteScalar())
        End Using
    End Function

    Private Function IsDuplicateValue(conn As SqlConnection, tr As SqlTransaction,
                                      fieldName As String, value As String, pn As Long) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False
        Dim allowedFields() As String = {"ProductCode", "Barcode"}
        If Array.IndexOf(allowedFields, fieldName) < 0 Then Throw New ArgumentException("Invalid product field")

        Using cmd As New SqlCommand("SELECT COUNT(1) FROM Product WHERE " & fieldName & "=@Value AND PN<>@PN", conn, tr)
            cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = value.Trim()
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Function HasDuplicateName(conn As SqlConnection, tr As SqlTransaction,
                                      productName As String, currentPN As Long) As Boolean
        Using cmd As New SqlCommand("SELECT COUNT(1) FROM Product WHERE Name=@Name AND PN<>@PN", conn, tr)
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = productName.Trim()
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = currentPN
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Sub InsertProduct(conn As SqlConnection, tr As SqlTransaction, pn As Long)
        Dim sql As String =
            "INSERT INTO Product(" &
            "PN,ProductCode,Name,Name2,Barcode,Price1,Price2,Price3,Price4,Price5,MinPrice,StandardCost,SUnit," &
            "NumPrice2,NumPrice3,NumPrice4,NumPrice5,VolumeID,ColorID,SizeID,Length,Width,Height,Weight,GrossWeight," &
            "AllowDecimal,MDiscountP1String,MDiscountP1,MDiscountP2String,MDiscountP2,MDiscountP3String,MDiscountP3," &
            "GroupID,BrandID,MainSupplier,VatStatus,CanCollectPoint,Point,Warranty,HotProduct,PicName,PrinterID,Comment," &
            "MedLabel1,MedLabel2,MedLabel3,MedLabel4,ShowOnWeb,PicSmall,WebDetail,CONumber,UONumber,CTime,UTime) VALUES(" &
            "@PN,@ProductCode,@Name,@Name2,@Barcode,@Price1,@Price2,@Price3,@Price4,@Price5,@MinPrice,@StandardCost,@SUnit," &
            "@NumPrice2,@NumPrice3,@NumPrice4,@NumPrice5,@VolumeID,NULL,NULL,@Length,@Width,@Height,@Weight,@GrossWeight," &
            "@AllowDecimal,@Discount1String,@Discount1,@Discount2String,@Discount2,@Discount3String,@Discount3," &
            "@GroupID,@BrandID,@MainSupplier,@VatStatus,@CanCollectPoint,@Point,0,@HotProduct,@PicName,NULL,@Comment," &
            "NULL,NULL,NULL,NULL,0,NULL,NULL,@OfficerNumber,@OfficerNumber,GETDATE(),GETDATE())"

        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
            cmd.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 50).Value = DbText(tbProductCode.Text)
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = tbName.Text.Trim()
            cmd.Parameters.Add("@Name2", SqlDbType.NVarChar, 200).Value = DbText(tbName2.Text)
            cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 100).Value = DbText(tbBarcode.Text)
            AddDecimalParameter(cmd, "@Price1", ParseDecimal(KryptonTextBox1.Text))
            AddDecimalParameter(cmd, "@Price2", ParseDecimal(KryptonTextBox2.Text))
            AddDecimalParameter(cmd, "@Price3", ParseDecimal(KryptonTextBox3.Text))
            AddDecimalParameter(cmd, "@Price4", ParseDecimal(KryptonTextBox4.Text))
            AddDecimalParameter(cmd, "@Price5", ParseDecimal(KryptonTextBox5.Text))
            AddDecimalParameter(cmd, "@MinPrice", ParseDecimal(KryptonTextBox6.Text))
            AddDecimalParameter(cmd, "@StandardCost", ParseDecimal(KryptonTextBox11.Text))
            cmd.Parameters.Add("@SUnit", SqlDbType.NVarChar, 50).Value = cbSUnit.Text.Trim()

            Dim volumeID As Integer = SelectedComboID(KryptonComboBox2)
            AddDecimalParameter(cmd, "@NumPrice2", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox7.Text)))
            AddDecimalParameter(cmd, "@NumPrice3", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox8.Text)))
            AddDecimalParameter(cmd, "@NumPrice4", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox9.Text)))
            AddDecimalParameter(cmd, "@NumPrice5", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox10.Text)))
            cmd.Parameters.Add("@VolumeID", SqlDbType.Int).Value = If(volumeID > 0, CType(volumeID, Object), DBNull.Value)

            AddDecimalParameter(cmd, "@Length", ParseDecimal(KryptonTextBox16.Text))
            AddDecimalParameter(cmd, "@Width", ParseDecimal(KryptonTextBox15.Text))
            AddDecimalParameter(cmd, "@Height", ParseDecimal(KryptonTextBox17.Text))
            AddDecimalParameter(cmd, "@Weight", ParseDecimal(KryptonTextBox18.Text))
            AddDecimalParameter(cmd, "@GrossWeight", ParseDecimal(KryptonTextBox19.Text))
            cmd.Parameters.Add("@AllowDecimal", SqlDbType.Bit).Value = cbAllowDecimal.Checked

            AddDiscountParameters(cmd, 1, KryptonTextBox12.Text)
            AddDiscountParameters(cmd, 2, KryptonTextBox13.Text)
            AddDiscountParameters(cmd, 3, KryptonTextBox14.Text)
            cmd.Parameters.Add("@GroupID", SqlDbType.Int).Value = SelectedComboID(cbProductGroup)
            cmd.Parameters.Add("@BrandID", SqlDbType.Int).Value = SelectedComboID(KryptonComboBox1)
            cmd.Parameters.Add("@MainSupplier", SqlDbType.NVarChar, 200).Value = DbText(tbMainSupplier.Text)
            cmd.Parameters.Add("@VatStatus", SqlDbType.Bit).Value = cbVatStatus.Checked
            cmd.Parameters.Add("@CanCollectPoint", SqlDbType.Bit).Value = cbCanCollectPoint.Checked
            AddDecimalParameter(cmd, "@Point", ParseDecimal(KryptonTextBox23.Text))
            cmd.Parameters.Add("@HotProduct", SqlDbType.Bit).Value = cbHotProduct.Checked
            cmd.Parameters.Add("@PicName", SqlDbType.NVarChar, 255).Value = DbText(If(String.IsNullOrWhiteSpace(selectedPicturePath), "", Path.GetFileName(selectedPicturePath)))
            cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = DbText(tbComment.Text)
            cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = LoginOfficerNumber()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub AddDecimalParameter(cmd As SqlCommand, name As String, value As Decimal)
        Dim parameter As SqlParameter = cmd.Parameters.Add(name, SqlDbType.Decimal)
        parameter.Precision = 18
        parameter.Scale = 4
        parameter.Value = value
    End Sub

    Private Sub UpdateProduct(conn As SqlConnection, tr As SqlTransaction, pn As Long)
        Const sql As String =
            "UPDATE Product SET ProductCode=@ProductCode,Name=@Name,Name2=@Name2,Barcode=@Barcode," &
            "Price1=@Price1,Price2=@Price2,Price3=@Price3,Price4=@Price4,Price5=@Price5,MinPrice=@MinPrice," &
            "StandardCost=@StandardCost,SUnit=@SUnit,NumPrice2=@NumPrice2,NumPrice3=@NumPrice3," &
            "NumPrice4=@NumPrice4,NumPrice5=@NumPrice5,VolumeID=@VolumeID,Length=@Length,Width=@Width," &
            "Height=@Height,Weight=@Weight,GrossWeight=@GrossWeight,AllowDecimal=@AllowDecimal," &
            "MDiscountP1String=@Discount1String,MDiscountP1=@Discount1," &
            "MDiscountP2String=@Discount2String,MDiscountP2=@Discount2," &
            "MDiscountP3String=@Discount3String,MDiscountP3=@Discount3,GroupID=@GroupID,BrandID=@BrandID," &
            "MainSupplier=@MainSupplier,VatStatus=@VatStatus,CanCollectPoint=@CanCollectPoint,Point=@Point," &
            "Status=@Status," &
            "HotProduct=@HotProduct,PicName=@PicName,Comment=@Comment,UONumber=@OfficerNumber,UTime=GETDATE() " &
            "WHERE PN=@PN"

        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
            cmd.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 50).Value = DbText(tbProductCode.Text)
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = tbName.Text.Trim()
            cmd.Parameters.Add("@Name2", SqlDbType.NVarChar, 200).Value = DbText(tbName2.Text)
            cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 100).Value = DbText(tbBarcode.Text)
            AddDecimalParameter(cmd, "@Price1", ParseDecimal(KryptonTextBox1.Text))
            AddDecimalParameter(cmd, "@Price2", ParseDecimal(KryptonTextBox2.Text))
            AddDecimalParameter(cmd, "@Price3", ParseDecimal(KryptonTextBox3.Text))
            AddDecimalParameter(cmd, "@Price4", ParseDecimal(KryptonTextBox4.Text))
            AddDecimalParameter(cmd, "@Price5", ParseDecimal(KryptonTextBox5.Text))
            AddDecimalParameter(cmd, "@MinPrice", ParseDecimal(KryptonTextBox6.Text))
            AddDecimalParameter(cmd, "@StandardCost", ParseDecimal(KryptonTextBox11.Text))
            cmd.Parameters.Add("@SUnit", SqlDbType.NVarChar, 50).Value = cbSUnit.Text.Trim()

            Dim volumeID As Integer = SelectedComboID(KryptonComboBox2)
            AddDecimalParameter(cmd, "@NumPrice2", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox7.Text)))
            AddDecimalParameter(cmd, "@NumPrice3", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox8.Text)))
            AddDecimalParameter(cmd, "@NumPrice4", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox9.Text)))
            AddDecimalParameter(cmd, "@NumPrice5", If(volumeID > 0, 0D, ParseDecimal(KryptonTextBox10.Text)))
            cmd.Parameters.Add("@VolumeID", SqlDbType.Int).Value = If(volumeID > 0, CType(volumeID, Object), DBNull.Value)
            AddDecimalParameter(cmd, "@Length", ParseDecimal(KryptonTextBox16.Text))
            AddDecimalParameter(cmd, "@Width", ParseDecimal(KryptonTextBox15.Text))
            AddDecimalParameter(cmd, "@Height", ParseDecimal(KryptonTextBox17.Text))
            AddDecimalParameter(cmd, "@Weight", ParseDecimal(KryptonTextBox18.Text))
            AddDecimalParameter(cmd, "@GrossWeight", ParseDecimal(KryptonTextBox19.Text))
            cmd.Parameters.Add("@AllowDecimal", SqlDbType.Bit).Value = cbAllowDecimal.Checked
            AddDiscountParameters(cmd, 1, KryptonTextBox12.Text)
            AddDiscountParameters(cmd, 2, KryptonTextBox13.Text)
            AddDiscountParameters(cmd, 3, KryptonTextBox14.Text)
            cmd.Parameters.Add("@GroupID", SqlDbType.Int).Value = SelectedComboID(cbProductGroup)
            cmd.Parameters.Add("@BrandID", SqlDbType.Int).Value = SelectedComboID(KryptonComboBox1)
            cmd.Parameters.Add("@MainSupplier", SqlDbType.NVarChar, 200).Value = DbText(tbMainSupplier.Text)
            cmd.Parameters.Add("@VatStatus", SqlDbType.Bit).Value = cbVatStatus.Checked
            cmd.Parameters.Add("@CanCollectPoint", SqlDbType.Bit).Value = cbCanCollectPoint.Checked
            AddDecimalParameter(cmd, "@Point", ParseDecimal(KryptonTextBox23.Text))
            cmd.Parameters.Add("@HotProduct", SqlDbType.Bit).Value = cbHotProduct.Checked
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(cbStatus.Checked, CType(1, Object), DBNull.Value)
            cmd.Parameters.Add("@PicName", SqlDbType.NVarChar, 255).Value = DbText(If(String.IsNullOrWhiteSpace(selectedPicturePath), "", Path.GetFileName(selectedPicturePath)))
            cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 500).Value = DbText(tbComment.Text)
            cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = LoginOfficerNumber()

            If cmd.ExecuteNonQuery() <> 1 Then
                Throw New ApplicationException("ไม่พบสินค้าที่ต้องการแก้ไข")
            End If
        End Using
    End Sub

    Private Sub AddDiscountParameters(cmd As SqlCommand, level As Integer, textValue As String)
        Dim result As Decimal = 0D
        Dim errorMessage As String = ""
        TryGetDiscount(textValue, result, errorMessage)
        Dim normalized As String = If(String.IsNullOrWhiteSpace(textValue), "0.00", textValue.Trim())
        cmd.Parameters.Add("@Discount" & level.ToString() & "String", SqlDbType.NVarChar, 50).Value = normalized
        AddDecimalParameter(cmd, "@Discount" & level.ToString(), result)
    End Sub

    Private Sub SaveProductLocation(conn As SqlConnection, tr As SqlTransaction, pn As Long)
        Const sql As String =
            "IF EXISTS(SELECT 1 FROM ProductLocation WHERE BranchNumber=@BranchNumber AND PN=@PN) " &
            "UPDATE ProductLocation SET Location=@Location,UONumber=@OfficerNumber,UTime=GETDATE() " &
            "WHERE BranchNumber=@BranchNumber AND PN=@PN " &
            "ELSE IF @Location IS NOT NULL " &
            "INSERT INTO ProductLocation(BranchNumber,PN,Location,CONumber,UONumber,CTime,UTime) " &
            "VALUES(@BranchNumber,@PN,@Location,@OfficerNumber,@OfficerNumber,GETDATE(),GETDATE())"
        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
            cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 200).Value = DbText(KryptonTextBox22.Text)
            cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = LoginOfficerNumber()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub SaveProductAlert(conn As SqlConnection, tr As SqlTransaction, pn As Long)
        Const sql As String =
            "IF EXISTS(SELECT 1 FROM ProductAlert WHERE BranchNumber=@BranchNumber AND PN=@PN) " &
            "UPDATE ProductAlert SET NumAlert=@NumAlert,NumMustHave=@NumMustHave,Status=@Status," &
            "UONumber=@OfficerNumber,UTime=GETDATE() WHERE BranchNumber=@BranchNumber AND PN=@PN " &
            "ELSE IF @Status IS NULL " &
            "INSERT INTO ProductAlert(BranchNumber,PN,NumAlert,NumMustHave,CONumber,UONumber,CTime,UTime) " &
            "VALUES(@BranchNumber,@PN,@NumAlert,@NumMustHave,@OfficerNumber,@OfficerNumber,GETDATE(),GETDATE())"
        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
            cmd.Parameters.Add("@PN", SqlDbType.BigInt).Value = pn
            AddDecimalParameter(cmd, "@NumAlert", If(cbAlert.Checked, ParseDecimal(KryptonTextBox20.Text), 0D))
            AddDecimalParameter(cmd, "@NumMustHave", If(cbAlert.Checked, ParseDecimal(KryptonTextBox21.Text), 0D))
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(cbAlert.Checked, CType(DBNull.Value, Object), 1)
            cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = LoginOfficerNumber()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub btPicName_Click(sender As Object, e As EventArgs) Handles btPicName.Click
        Using dialog As New OpenFileDialog()
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.bmp;*.png|All Files|*.*"
            If dialog.ShowDialog(Me) <> DialogResult.OK Then Return

            selectedPicturePath = dialog.FileName
            lbPicName.Text = Path.GetFileName(dialog.FileName)
            Try
                Using source As Image = Image.FromFile(dialog.FileName)
                    If pbPicture.Image IsNot Nothing Then pbPicture.Image.Dispose()
                    pbPicture.Image = New Bitmap(source)
                End Using
            Catch ex As Exception
                selectedPicturePath = ""
                pbPicture.Image = Nothing
                lbPicName.Text = "ยังไม่ได้เลือกรูป"
                MessageBox.Show("เปิดรูปไม่สำเร็จ: " & ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btRemovePic_Click(sender As Object, e As EventArgs) Handles btRemovePic.Click
        If String.IsNullOrWhiteSpace(selectedPicturePath) AndAlso pbPicture.Image Is Nothing Then Return

        If MessageBox.Show("ต้องการลบรูปสินค้านี้ออกหรือไม่?" & Environment.NewLine &
                           "ระบบจะล้างรูปที่บันทึกไว้เมื่อกดปุ่มบันทึก", "ยืนยันการลบรูป",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        selectedPicturePath = ""
        If pbPicture.Image IsNot Nothing Then
            pbPicture.Image.Dispose()
            pbPicture.Image = Nothing
        End If
        lbPicName.Text = "ยังไม่ได้เลือกรูป"
    End Sub

    Private Sub chkEditAutoCode_CheckedChanged(sender As Object, e As EventArgs) Handles chkEditAutoCode.CheckedChanged
        tbBarcode.Enabled = Not chkEditAutoCode.Checked
        If Not chkEditAutoCode.Checked Then Return

        If IsBeverageGroup() Then
            Try
                If String.IsNullOrWhiteSpace(tbBarcode.Text) OrElse EditProductPN > 0 OrElse CloneSourcePN > 0 Then
                    tbBarcode.Text = PreviewNextBeverageBarcode()
                End If
            Catch ex As Exception
                tbBarcode.Clear()
                MessageBox.Show("สร้าง Barcode เครื่องดื่มไม่สำเร็จ: " & ex.Message,
                                "Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        ElseIf EditProductPN <= 0 Then
            tbBarcode.Clear()
        End If
    End Sub

    Private Function PreviewNextBeverageBarcode() As String
        Const sql As String =
            "SELECT ISNULL(MAX(TRY_CONVERT(bigint, LEFT(RTRIM(Barcode), 12))), 210000000000) " &
            "FROM Product WHERE RTRIM(Barcode) LIKE '21%' AND LEN(RTRIM(Barcode)) = 13 " &
            "AND TRY_CONVERT(bigint, LEFT(RTRIM(Barcode), 12)) IS NOT NULL"

        Dim nextSequence As Long = 1
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sql, conn)
                Dim maxData As Long = Convert.ToInt64(cmd.ExecuteScalar())
                If maxData >= 210000000000L Then nextSequence = maxData - 210000000000L + 1L
            End Using
        End Using

        If nextSequence > 9999999999L Then Throw New ApplicationException("เลข Barcode เครื่องดื่มเต็มช่วงที่กำหนด")
        Dim data12 As String = "21" & nextSequence.ToString("0000000000")
        Return data12 & Ean13CheckDigit(data12)
    End Function

    Private Function NextBeverageBarcode(conn As SqlConnection, tr As SqlTransaction) As String
        Dim nextSequence As Long = 1
        Const sql As String =
            "SELECT ISNULL(MAX(TRY_CONVERT(bigint, LEFT(RTRIM(Barcode), 12))), 210000000000) " &
            "FROM Product WITH (UPDLOCK, HOLDLOCK) " &
            "WHERE RTRIM(Barcode) LIKE '21%' AND LEN(RTRIM(Barcode)) = 13 " &
            "AND TRY_CONVERT(bigint, LEFT(RTRIM(Barcode), 12)) IS NOT NULL"

        Using cmd As New SqlCommand(sql, conn, tr)
            Dim maxData As Long = Convert.ToInt64(cmd.ExecuteScalar())
            If maxData >= 210000000000L Then nextSequence = maxData - 210000000000L + 1L
        End Using

        If nextSequence > 9999999999L Then Throw New ApplicationException("เลข Barcode เครื่องดื่มเต็มช่วงที่กำหนด")
        Dim data12 As String = "21" & nextSequence.ToString("0000000000")
        Return data12 & Ean13CheckDigit(data12)
    End Function

    Private Function Ean13CheckDigit(data12 As String) As String
        If data12 Is Nothing OrElse data12.Length <> 12 OrElse Not data12.All(Function(c) Char.IsDigit(c)) Then
            Throw New ArgumentException("EAN-13 ต้องมีตัวเลขข้อมูล 12 หลัก")
        End If

        Dim total As Integer = 0
        For i As Integer = 0 To data12.Length - 1
            Dim digit As Integer = AscW(data12(i)) - AscW("0"c)
            total += If((i Mod 2) = 0, digit, digit * 3)
        Next
        Return ((10 - (total Mod 10)) Mod 10).ToString()
    End Function

    Private Sub cbAlert_CheckedChanged(sender As Object, e As EventArgs) Handles cbAlert.CheckedChanged
        SetAlertControlsEnabled(cbAlert.Checked)
    End Sub

    Private Sub SetAlertControlsEnabled(enabled As Boolean)
        KryptonTextBox20.Enabled = enabled
        KryptonTextBox21.Enabled = enabled
    End Sub

    Private Sub cbCanCollectPoint_CheckedChanged(sender As Object, e As EventArgs) Handles cbCanCollectPoint.CheckedChanged
        KryptonTextBox23.Enabled = cbCanCollectPoint.Checked
        If Not cbCanCollectPoint.Checked Then KryptonTextBox23.Text = "0.00"
    End Sub

    Private Sub NumericTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
        KryptonTextBox1.KeyPress, KryptonTextBox2.KeyPress, KryptonTextBox3.KeyPress,
        KryptonTextBox4.KeyPress, KryptonTextBox5.KeyPress, KryptonTextBox6.KeyPress,
        KryptonTextBox7.KeyPress, KryptonTextBox8.KeyPress, KryptonTextBox9.KeyPress,
        KryptonTextBox10.KeyPress, KryptonTextBox11.KeyPress, KryptonTextBox15.KeyPress,
        KryptonTextBox16.KeyPress, KryptonTextBox17.KeyPress, KryptonTextBox18.KeyPress,
        KryptonTextBox19.KeyPress, KryptonTextBox20.KeyPress, KryptonTextBox21.KeyPress,
        KryptonTextBox23.KeyPress

        If Char.IsControl(e.KeyChar) OrElse Char.IsDigit(e.KeyChar) Then Return
        If e.KeyChar = "."c Then
            Dim control As Control = DirectCast(sender, Control)
            If Not control.Text.Contains(".") Then Return
        End If
        If e.KeyChar = "-"c Then
            Dim textBox As Krypton.Toolkit.KryptonTextBox = DirectCast(sender, Krypton.Toolkit.KryptonTextBox)
            If textBox.SelectionStart = 0 AndAlso Not textBox.Text.Contains("-") Then Return
        End If
        e.Handled = True
    End Sub

    Private Sub DecimalTextBox_Leave(sender As Object, e As EventArgs) Handles _
        KryptonTextBox1.Leave, KryptonTextBox2.Leave, KryptonTextBox3.Leave,
        KryptonTextBox4.Leave, KryptonTextBox5.Leave, KryptonTextBox6.Leave,
        KryptonTextBox7.Leave, KryptonTextBox8.Leave, KryptonTextBox9.Leave,
        KryptonTextBox10.Leave, KryptonTextBox11.Leave, KryptonTextBox15.Leave,
        KryptonTextBox16.Leave, KryptonTextBox17.Leave, KryptonTextBox18.Leave,
        KryptonTextBox19.Leave, KryptonTextBox20.Leave, KryptonTextBox21.Leave,
        KryptonTextBox23.Leave

        Dim control As Control = DirectCast(sender, Control)
        control.Text = ParseDecimal(control.Text).ToString("0.00")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.S) Then
            btnSave.PerformClick()
            Return True
        End If
        If keyData = Keys.Escape Then
            btnCancel.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub FrmProductAdd_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If pbPicture.Image IsNot Nothing Then
            pbPicture.Image.Dispose()
            pbPicture.Image = Nothing
        End If
    End Sub
End Class
