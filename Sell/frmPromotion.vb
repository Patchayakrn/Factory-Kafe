Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class frmPromotion
    Private Const AllText As String = "ทั้งหมด"
    Private Const ActiveText As String = "ใช้งานอยู่"
    Private Const DraftText As String = "ร่าง (Draft)"
    Private Const CancelledText As String = "ยกเลิกแล้ว"
    Private isLoading As Boolean
    Private memoPrintButton As Krypton.Toolkit.KryptonButton

    Private NotInheritable Class LookupItem
        Public ReadOnly ID As Integer
        Public ReadOnly Text As String
        Public Sub New(idValue As Integer, textValue As String)
            ID = idValue : Text = textValue
        End Sub
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Private Sub frmPromotion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        HidePromotionFilters()
        AddMemoPrintButton()
        isLoading = True
        Try
            Label2.Text = "ประเภทโปรโมชั่น"
            Label3.Text = "กลุ่มสินค้า"
            Label4.Text = "วันที่เริ่ม"
            LoadPromotionTypes()
            LoadProductGroups()
            LoadBrands()
            LoadCategories()
            LoadStatuses()
            dpDate.Value = Date.Today
            KryptonDateTimePicker1.Value = Date.Today
        Finally
            isLoading = False
        End Try
        LoadPromotions()
    End Sub

    Private Sub AddMemoPrintButton()
        If memoPrintButton IsNot Nothing Then Return

        memoPrintButton = New Krypton.Toolkit.KryptonButton With {
            .Name = "btnPrintPromotionMemo",
            .Location = New Point(20, 6),
            .Size = New Size(190, 55),
            .TabIndex = 48
        }
        memoPrintButton.StateCommon.Back.Color1 = Color.White
        memoPrintButton.StateCommon.Back.Color2 = Color.White
        memoPrintButton.StateCommon.Content.ShortText.Color1 = Color.DarkGreen
        memoPrintButton.StateCommon.Content.ShortText.Font = New Font("Cordia New", 16.2!, FontStyle.Bold)
        memoPrintButton.Values.Text = "  พิมพ์ Memo"
        AddHandler memoPrintButton.Click, AddressOf PrintSelectedPromotionMemo
        KryptonPanel2.Controls.Add(memoPrintButton)
    End Sub

    Private Sub HidePromotionFilters()
        'หน้านี้ใช้งานแบบรายการโปรโมชั่นเป็นหลัก ไม่ต้องกรอกเงื่อนไขค้นหาก่อนเพิ่มรายการ
        KryptonPanel1.Visible = False
        KryptonPanel3.Dock = DockStyle.Fill
        KryptonPanel2.Dock = DockStyle.Bottom
        KryptonPanel2.BringToFront()
    End Sub

    Private Sub LoadPromotionTypes()
        cbHaveTax.Items.Clear()
        cbHaveTax.Items.Add(AllText)
        cbHaveTax.DropDownWidth = 720
        cbHaveTax.MaxDropDownItems = 15
        cbHaveTax.Items.AddRange(New Object() {
            "1. ซื้อสินค้าที่กำหนด ครบจำนวน → ลดเป็นเปอร์เซ็นต์ (เช่น ซื้อ 2 แก้ว ลด 10%)",
            "2. ซื้อสินค้าที่กำหนด ครบจำนวน → ลดเป็นเงิน (เช่น ซื้อ 2 แก้ว ลด 20 บาท)",
            "3. ซื้อสินค้าที่กำหนดหลายรายการ → ขายรวมราคาพิเศษ",
            "4. ซื้อสินค้าที่กำหนดหลายรายการ → ลดเป็นเปอร์เซ็นต์ทั้งชุด",
            "5. ซื้อสินค้าที่กำหนดหลายรายการ → ลดเป็นเงินทั้งชุด",
            "6. ซื้อสินค้าที่กำหนด → แถมสินค้าอีกชุด",
            "7. ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมในราคาพิเศษ",
            "8. ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
            "9. ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเงิน",
            "10. ซื้อสินค้าครบยอดที่กำหนด → ซื้อสินค้าแถมในราคาพิเศษ",
            "11. ซื้อสินค้าครบยอดที่กำหนด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
            "12. ซื้อสินค้าครบยอดที่กำหนด → ซื้อสินค้าแถมลดเป็นเงิน",
            "13. ซื้อสินค้าที่กำหนดครบจำนวนขั้นต่ำ → ลดเป็นเปอร์เซ็นต์",
            "14. ซื้อสินค้าอะไรก็ได้ครบจำนวนขั้นต่ำ → ลดเป็นเปอร์เซ็นต์",
            "15. ซื้อสินค้าหลัก → เลือกเครื่องดื่มได้ 1 รายการในราคาเครื่องดื่ม"})
        cbHaveTax.SelectedIndex = 0
    End Sub

    Private Sub LoadProductGroups()
        KryptonComboBox1.Items.Clear()
        KryptonComboBox1.Items.Add(New LookupItem(0, AllText))
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT ID,RTRIM(Name) AS Name FROM ProductGroup WHERE Status IS NULL ORDER BY Name", conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            KryptonComboBox1.Items.Add(New LookupItem(Convert.ToInt32(reader("ID")), reader("Name").ToString().Trim()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดกลุ่มสินค้าไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        KryptonComboBox1.SelectedIndex = 0
    End Sub

    Private Sub LoadBrands()
        KryptonComboBox2.Items.Clear()
        KryptonComboBox2.Items.Add(New LookupItem(0, AllText))
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT ID,RTRIM(Name) AS Name FROM Brand WHERE Status IS NULL ORDER BY Name", conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            KryptonComboBox2.Items.Add(New LookupItem(Convert.ToInt32(reader("ID")), reader("Name").ToString().Trim()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดแบรนด์ไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        KryptonComboBox2.SelectedIndex = 0
    End Sub

    Private Sub LoadCategories()
        KryptonComboBox4.Items.Clear()
        KryptonComboBox4.Items.Add(New LookupItem(0, AllText))
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT ID,RTRIM(Name) AS Name FROM Category WHERE Status IS NULL ORDER BY Name", conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            KryptonComboBox4.Items.Add(New LookupItem(Convert.ToInt32(reader("ID")), reader("Name").ToString().Trim()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดประเภทสินค้าไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        KryptonComboBox4.SelectedIndex = 0
    End Sub

    Private Sub LoadStatuses()
        KryptonComboBox3.Items.Clear()
        KryptonComboBox3.Items.AddRange(New Object() {AllText, DraftText, ActiveText, CancelledText})
        KryptonComboBox3.SelectedIndex = 0
    End Sub

    Private Sub LoadPromotions() Handles Button1.Click
        If isLoading Then Return
        Dim table As New DataTable()
        Dim sql As New StringBuilder()
        sql.AppendLine("SELECT PMT.ID,PMT.Type AS PromotionType,PMT.Priority,RTRIM(PMT.Name) AS PromotionName,")
        sql.AppendLine("PMT.SDate,PMT.EDate,RTRIM(PMT.Comment) AS Comment,")
        sql.AppendLine("CASE WHEN PMT.Status=0 THEN N'ร่าง (Draft)' WHEN PMT.Status IS NULL THEN N'ใช้งานอยู่' ELSE N'ยกเลิกแล้ว' END AS StatusText")
        sql.AppendLine("FROM PMT WHERE 1=1")

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand()
                cmd.Connection = conn

                If Not KryptonPanel1.Visible Then
                    'หน้าหลักแสดงโปรโมชั่นร่างและโปรโมชั่นที่ใช้งานอยู่ในปีปัจจุบัน
                    Dim currentYearStart As New Date(Date.Today.Year, 1, 1)
                    Dim nextYearStart As Date = currentYearStart.AddYears(1)
                    sql.AppendLine("AND (PMT.Status IS NULL OR PMT.Status=0)")
                    sql.AppendLine("AND PMT.SDate < @NextYearStart AND PMT.EDate >= @CurrentYearStart")
                    cmd.Parameters.Add("@CurrentYearStart", SqlDbType.DateTime).Value = currentYearStart
                    cmd.Parameters.Add("@NextYearStart", SqlDbType.DateTime).Value = nextYearStart
                    sql.AppendLine("ORDER BY PMT.ID DESC")
                    cmd.CommandText = sql.ToString()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                    KryptonDataGridView1.DataSource = table
                    FormatPromotionGrid()
                    Return
                End If

                Dim id As Integer
                If Integer.TryParse(TextEdit1.Text.Trim(), id) Then
                    sql.AppendLine("AND PMT.ID=@ID")
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                ElseIf TextEdit1.Text.Trim() <> "" Then
                    sql.AppendLine("AND CONVERT(nvarchar(20),PMT.ID) LIKE @IDText")
                    cmd.Parameters.Add("@IDText", SqlDbType.NVarChar, 30).Value = "%" & TextEdit1.Text.Trim() & "%"
                End If
                If KryptonTextBox1.Text.Trim() <> "" Then
                    sql.AppendLine("AND PMT.Name LIKE @Name")
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = "%" & KryptonTextBox1.Text.Trim() & "%"
                End If
                If cbHaveTax.SelectedIndex > 0 Then
                    sql.AppendLine("AND PMT.Type=@Type")
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = cbHaveTax.SelectedIndex
                End If
                Dim groupItem As LookupItem = TryCast(KryptonComboBox1.SelectedItem, LookupItem)
                If groupItem IsNot Nothing AndAlso groupItem.ID > 0 Then
                    sql.AppendLine("AND EXISTS (SELECT 1 FROM PMTD WHERE PMTD.ID=PMT.ID AND PMTD.ProductGroupID=@GroupID)")
                    cmd.Parameters.Add("@GroupID", SqlDbType.Int).Value = groupItem.ID
                End If
                Dim brandItem As LookupItem = TryCast(KryptonComboBox2.SelectedItem, LookupItem)
                If brandItem IsNot Nothing AndAlso brandItem.ID > 0 Then
                    sql.AppendLine("AND EXISTS (SELECT 1 FROM PMTD WHERE PMTD.ID=PMT.ID AND PMTD.BrandID=@BrandID)")
                    cmd.Parameters.Add("@BrandID", SqlDbType.Int).Value = brandItem.ID
                End If
                Dim categoryItem As LookupItem = TryCast(KryptonComboBox4.SelectedItem, LookupItem)
                If categoryItem IsNot Nothing AndAlso categoryItem.ID > 0 Then
                    sql.AppendLine("AND EXISTS (SELECT 1 FROM PMTD INNER JOIN ProductGroup PG ON PG.ID=PMTD.ProductGroupID WHERE PMTD.ID=PMT.ID AND PG.CategoryID=@CategoryID)")
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryItem.ID
                End If
                sql.AppendLine("AND PMT.SDate < DATEADD(day,1,@DateTo) AND PMT.EDate >= @DateFrom")
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dpDate.Value.Date
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = KryptonDateTimePicker1.Value.Date
                Select Case If(KryptonComboBox3.SelectedItem, AllText).ToString()
                    Case DraftText : sql.AppendLine("AND PMT.Status=0")
                    Case ActiveText : sql.AppendLine("AND PMT.Status IS NULL")
                    Case CancelledText : sql.AppendLine("AND PMT.Status IS NOT NULL")
                End Select
                sql.AppendLine("ORDER BY PMT.ID ASC")
                cmd.CommandText = sql.ToString()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(table)
                End Using
            End Using
        End Using
        KryptonDataGridView1.DataSource = table
        FormatPromotionGrid()
    End Sub

    Private Sub FormatPromotionGrid()
        If KryptonDataGridView1.Columns.Count = 0 Then Return
        SetHeader("ID", "รหัส")
        SetHeader("PromotionType", "ประเภท")
        SetHeader("Priority", "ความสำคัญ")
        SetHeader("PromotionName", "ชื่อโปรโมชั่น")
        SetHeader("SDate", "วันที่เริ่ม")
        SetHeader("EDate", "วันที่สิ้นสุด")
        SetHeader("Comment", "หมายเหตุ")
        SetHeader("StatusText", "สถานะ")
        If KryptonDataGridView1.Columns.Contains("SDate") Then KryptonDataGridView1.Columns("SDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        If KryptonDataGridView1.Columns.Contains("EDate") Then KryptonDataGridView1.Columns("EDate").DefaultCellStyle.Format = "dd/MM/yyyy"
        If KryptonDataGridView1.Columns.Contains("PromotionName") Then KryptonDataGridView1.Columns("PromotionName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        If KryptonDataGridView1.Columns.Contains("Comment") Then KryptonDataGridView1.Columns("Comment").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        For Each row As DataGridViewRow In KryptonDataGridView1.Rows
            If Convert.ToString(row.Cells("StatusText").Value) = CancelledText Then
                row.DefaultCellStyle.ForeColor = Color.Gray
                row.DefaultCellStyle.BackColor = Color.MistyRose
            ElseIf Convert.ToString(row.Cells("StatusText").Value) = DraftText Then
                row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod
                row.DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next
    End Sub

    Private Sub SetHeader(columnName As String, caption As String)
        If KryptonDataGridView1.Columns.Contains(columnName) Then KryptonDataGridView1.Columns(columnName).HeaderText = caption
    End Sub

    Private Function SelectedPromotionID() As Integer
        If KryptonDataGridView1.CurrentRow Is Nothing OrElse Not KryptonDataGridView1.Columns.Contains("ID") Then Return 0
        Return Convert.ToInt32(KryptonDataGridView1.CurrentRow.Cells("ID").Value)
    End Function

    Private Sub ClearFilters() Handles btnAddToBill.Click
        TextEdit1.Clear() : KryptonTextBox1.Clear()
        cbHaveTax.SelectedIndex = 0 : KryptonComboBox1.SelectedIndex = 0 : KryptonComboBox2.SelectedIndex = 0 : KryptonComboBox3.SelectedIndex = 0 : KryptonComboBox4.SelectedIndex = 0
        dpDate.Value = Date.Today : KryptonDateTimePicker1.Value = Date.Today
        LoadPromotions()
    End Sub

    Private Sub CancelPromotion() Handles KryptonButton2.Click
        Dim id As Integer = SelectedPromotionID()
        If id <= 0 Then
            MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการยกเลิกก่อน", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If MessageBox.Show("ต้องการยกเลิกโปรโมชั่นรหัส " & id.ToString() & " หรือไม่?", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("UPDATE PMT SET Status=1,UTime=GETDATE(),UONumber=@UONumber WHERE ID=@ID AND (Status IS NULL OR Status=0)", conn)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@UONumber", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
                    If cmd.ExecuteNonQuery() = 0 Then
                        MessageBox.Show("โปรโมชั่นนี้ถูกยกเลิกไปแล้ว หรือไม่พบข้อมูล", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Else
                        LoadPromotions()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("ยกเลิกโปรโมชั่นไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CopySelectedRows() Handles KryptonButton3.Click
        If KryptonDataGridView1.SelectedRows.Count = 0 Then Return
        Dim output As New StringBuilder()
        For Each column As DataGridViewColumn In KryptonDataGridView1.Columns
            If column.Visible Then output.Append(column.HeaderText).Append(vbTab)
        Next
        output.AppendLine()
        For Each row As DataGridViewRow In KryptonDataGridView1.SelectedRows
            For Each column As DataGridViewColumn In KryptonDataGridView1.Columns
                If column.Visible Then output.Append(Convert.ToString(row.Cells(column.Index).Value)).Append(vbTab)
            Next
            output.AppendLine()
        Next
        Clipboard.SetText(output.ToString())
    End Sub

    Private Sub EditPromotion() Handles KryptonButton1.Click
        Dim id As Integer = SelectedPromotionID()
        If id <= 0 Then
            MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการแก้ไขก่อน", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using detail As New PromotionDetailForm(id)
            If detail.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                UpdatePromotion(detail)
                LoadPromotions()
                MessageBox.Show("แก้ไขโปรโมชั่นเรียบร้อยแล้ว", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("แก้ไขโปรโมชั่นไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub AddPromotion() Handles btnAdd.Click
        Using detail As New PromotionDetailForm(0, 10)
            If detail.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                SavePromotion(detail)
                LoadPromotions()
                MessageBox.Show("บันทึกโปรโมชั่นเรียบร้อยแล้ว", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("บันทึกโปรโมชั่นไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub PrintSelectedPromotionMemo(sender As Object, e As EventArgs)
        Dim promotionId As Integer = SelectedPromotionID()
        If promotionId <= 0 Then
            MessageBox.Show("กรุณาเลือกโปรโมชั่นที่ต้องการพิมพ์ Memo ก่อน", "พิมพ์ Memo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim reportTable As DataTable = BuildPromotionMemoReport(promotionId)
            If reportTable.Rows.Count = 0 Then
                MessageBox.Show("ไม่พบรายละเอียดโปรโมชั่นที่เลือก", "พิมพ์ Memo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim documentNo As String = SavePromotionMemoReport(reportTable)
            If String.IsNullOrWhiteSpace(documentNo) Then Return
            For Each reportRow As DataRow In reportTable.Rows
                reportRow("DocumentNo") = documentNo
            Next

            Using viewer As New frmReportViewer()
                viewer.PrintLoadedReportAfterShown()
                viewer.LoadMemoPromotionReport(reportTable, reportTable.Rows.Count >= 2)
                viewer.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show("สร้างเอกสาร Memo ไม่สำเร็จ: " & ex.Message, "พิมพ์ Memo", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function SavePromotionMemoReport(reportTable As DataTable) As String
        If reportTable Is Nothing OrElse reportTable.Rows.Count = 0 Then Return ""

        Try
            If Not EnsurePromotionMemoTablesForPrint() Then Return ""

            Dim documentDate As Date = Convert.ToDateTime(reportTable.Rows(0)("DocumentDate")).Date
            Dim buddhistYearTwoDigits As Integer = (documentDate.Year + 543) Mod 100
            Dim prefix As String = String.Format(CultureInfo.InvariantCulture,
                                                 "FTR{0:00}{1:00}",
                                                 buddhistYearTwoDigits,
                                                 documentDate.Month)

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Dim nextNumber As Integer = 1
                    Using cmd As New SqlCommand(
                        "SELECT MAX(DocumentNo) FROM dbo.PromotionMemo WITH (UPDLOCK,HOLDLOCK) " &
                        "WHERE DocumentNo LIKE @Prefix + N'%'", conn, tr)
                        cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = prefix
                        Dim value As Object = cmd.ExecuteScalar()
                        If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                            Dim existing As String = Convert.ToString(value).Trim()
                            If existing.Length > prefix.Length Then
                                Dim existingNumber As Integer
                                If Integer.TryParse(existing.Substring(prefix.Length), existingNumber) Then
                                    nextNumber = existingNumber + 1
                                End If
                            End If
                        End If
                    End Using

                    Dim documentNo As String = prefix & nextNumber.ToString("000", CultureInfo.InvariantCulture)
                    Dim memoId As Integer
                    Using cmd As New SqlCommand(
                        "INSERT INTO dbo.PromotionMemo " &
                        "(DocumentNo,DocumentDate,SubjectText,BodyText,CreatedBy) " &
                        "VALUES (@DocumentNo,@DocumentDate,@SubjectText,@BodyText,@CreatedBy); " &
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tr)
                        cmd.Parameters.Add("@DocumentNo", SqlDbType.NVarChar, 30).Value = documentNo
                        cmd.Parameters.Add("@DocumentDate", SqlDbType.Date).Value = documentDate
                        cmd.Parameters.Add("@SubjectText", SqlDbType.NVarChar, 500).Value = Convert.ToString(reportTable.Rows(0)("SubjectText")).Trim()
                        cmd.Parameters.Add("@BodyText", SqlDbType.NVarChar, -1).Value = Convert.ToString(reportTable.Rows(0)("BodyText")).Trim()
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = If(ModSession.LoginOfficerID > 0, CType(ModSession.LoginOfficerID, Object), DBNull.Value)
                        memoId = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                    Const insertDetail As String =
                        "INSERT INTO dbo.PromotionMemoDetail " &
                        "(MemoID,[LineNo],PromotionText,RewardText,ActivityDateText) " &
                        "VALUES (@MemoID,@LineNo,@PromotionText,@RewardText,@ActivityDateText)"
                    For Each reportRow As DataRow In reportTable.Rows
                        Using cmd As New SqlCommand(insertDetail, conn, tr)
                            cmd.Parameters.Add("@MemoID", SqlDbType.Int).Value = memoId
                            cmd.Parameters.Add("@LineNo", SqlDbType.Int).Value = Convert.ToInt32(reportRow("LineNo"))
                            cmd.Parameters.Add("@PromotionText", SqlDbType.NVarChar, 1000).Value = Convert.ToString(reportRow("PromotionText")).Trim()
                            cmd.Parameters.Add("@RewardText", SqlDbType.NVarChar, 1000).Value = Convert.ToString(reportRow("RewardText")).Trim()
                            cmd.Parameters.Add("@ActivityDateText", SqlDbType.NVarChar, 200).Value = Convert.ToString(reportRow("ActivityDateText")).Trim()
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                    Return documentNo
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("สร้างเลขที่เอกสาร Memo ไม่สำเร็จ: " & ex.Message,
                            "พิมพ์ Memo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Private Function EnsurePromotionMemoTablesForPrint() As Boolean
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "IF OBJECT_ID(N'dbo.PromotionMemo', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.PromotionMemo (" &
                    "MemoID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PromotionMemo PRIMARY KEY, " &
                    "DocumentNo NVARCHAR(30) NOT NULL CONSTRAINT UQ_PromotionMemo_DocumentNo UNIQUE, " &
                    "DocumentDate DATE NOT NULL, SubjectText NVARCHAR(500) NULL, BodyText NVARCHAR(MAX) NULL, " &
                    "CreatedBy INT NULL, CreatedAt DATETIME NOT NULL CONSTRAINT DF_PromotionMemo_CreatedAt DEFAULT GETDATE()) END; " &
                    "IF OBJECT_ID(N'dbo.PromotionMemoDetail', N'U') IS NULL " &
                    "BEGIN CREATE TABLE dbo.PromotionMemoDetail (" &
                    "MemoDetailID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PromotionMemoDetail PRIMARY KEY, " &
                    "MemoID INT NOT NULL, [LineNo] INT NOT NULL, PromotionText NVARCHAR(1000) NULL, " &
                    "RewardText NVARCHAR(1000) NULL, ActivityDateText NVARCHAR(200) NULL, " &
                    "CONSTRAINT FK_PromotionMemoDetail_Memo FOREIGN KEY (MemoID) REFERENCES dbo.PromotionMemo(MemoID), " &
                    "CONSTRAINT UQ_PromotionMemoDetail_Memo_Line UNIQUE (MemoID, [LineNo])) END;"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.CommandTimeout = 30
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("สร้างตารางบันทึก Memo ไม่สำเร็จ: " & ex.Message,
                            "พิมพ์ Memo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function BuildPromotionMemoReport(promotionId As Integer) As DataTable
        Dim reportTable As New DataTable("MemoPromotion")
        reportTable.Columns.Add("DocumentDate", GetType(DateTime))
        reportTable.Columns.Add("DocumentNo", GetType(String))
        reportTable.Columns.Add("SubjectText", GetType(String))
        reportTable.Columns.Add("BodyText", GetType(String))
        reportTable.Columns.Add("Recipient1", GetType(String))
        reportTable.Columns.Add("Recipient2", GetType(String))
        reportTable.Columns.Add("CopyRecipient", GetType(String))
        reportTable.Columns.Add("LineNo", GetType(Integer))
        reportTable.Columns.Add("PromotionText", GetType(String))
        reportTable.Columns.Add("RewardText", GetType(String))
        reportTable.Columns.Add("ActivityDateText", GetType(String))

        Dim promotionData As New DataTable()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT H.ID,H.Type,RTRIM(H.Name) AS PromotionName,H.SDate,H.EDate,ISNULL(H.Price,0) AS PromotionPrice," &
                "RTRIM(ISNULL(H.Comment,'')) AS PromotionComment," &
                "D.Part AS DetailPart,D.Item AS DetailItem,D.Number AS DetailNumber,D.Price AS DetailPrice,D.PN AS DetailPN," &
                "RTRIM(ISNULL(P.ProductCode,'')) AS ProductCode,RTRIM(ISNULL(P.Name,'')) AS ProductName " &
                "FROM PMT H LEFT JOIN PMTD D ON D.ID=H.ID LEFT JOIN Product P ON P.PN=D.PN " &
                "WHERE RTRIM(H.Name)=RTRIM((SELECT Name FROM PMT WHERE ID=@ID)) " &
                "AND YEAR(H.SDate)=YEAR((SELECT SDate FROM PMT WHERE ID=@ID)) " &
                "AND MONTH(H.SDate)=MONTH((SELECT SDate FROM PMT WHERE ID=@ID)) " &
                "AND (H.Status IS NULL OR H.Status=0) " &
                "ORDER BY H.ID,D.Part,D.Item", conn)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = promotionId
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(promotionData)
                End Using
            End Using
        End Using

        If promotionData.Rows.Count = 0 Then Return reportTable

        Dim selectedHeader As DataRow = promotionData.Rows(0)
        Dim promotionName As String = Convert.ToString(selectedHeader("PromotionName")).Trim()

        Dim groupedPromotions = promotionData.AsEnumerable().
            GroupBy(Function(row) Convert.ToInt32(row("ID"))).
            OrderBy(Function(group) group.Key).ToList()

        Dim lineNo As Integer = 1
        For Each promotionGroup In groupedPromotions
            Dim header As DataRow = promotionGroup.First()
            Dim promotionType As Integer = If(IsDBNull(header("Type")), 0, Convert.ToInt32(header("Type")))
            Dim promotionPrice As Decimal = If(IsDBNull(header("PromotionPrice")), 0D, Convert.ToDecimal(header("PromotionPrice")))
            Dim startDate As Date = If(IsDBNull(header("SDate")), Date.Today, Convert.ToDateTime(header("SDate")))
            Dim endDate As Date = If(IsDBNull(header("EDate")), startDate, Convert.ToDateTime(header("EDate")))
            Dim comment As String = Convert.ToString(header("PromotionComment")).Trim()

            Dim mainRows = promotionGroup.Where(Function(row) Not row.IsNull("DetailPart") AndAlso Convert.ToInt32(row("DetailPart")) = 1 AndAlso Not row.IsNull("DetailPN") AndAlso Convert.ToInt64(row("DetailPN")) > 0).ToList()
            Dim optionRows = promotionGroup.Where(Function(row) Not row.IsNull("DetailPart") AndAlso Convert.ToInt32(row("DetailPart")) = 2 AndAlso Not row.IsNull("DetailPN") AndAlso Convert.ToInt64(row("DetailPN")) > 0).ToList()

            Dim promotionText As String = Convert.ToString(header("PromotionName")).Trim()
            Dim rewardText As String = ""
            If promotionType = 15 Then
                promotionText = String.Join(" + ", mainRows.Select(Function(row) ProductDisplayText(row, "แพ็ค")))
                rewardText = "เลือก 1 รายการ: " & String.Join(" / ", optionRows.Select(Function(row) ProductDisplayText(row, "แก้ว"))) &
                             " ราคา " & promotionPrice.ToString("N2") & " บาท"
            ElseIf mainRows.Count > 0 Then
                promotionText = String.Join(" / ", mainRows.Select(Function(row) ProductDisplayText(row)))
            End If

            If rewardText.Length = 0 AndAlso optionRows.Count > 0 Then
                rewardText = String.Join(" / ", optionRows.Select(Function(row) ProductDisplayText(row)))
            End If
            If rewardText.Length = 0 AndAlso promotionPrice > 0D Then
                rewardText = "ราคาพิเศษ " & promotionPrice.ToString("N2") & " บาท"
            End If

            Dim promotionKind As String = ExtractPromotionKind(comment)
            Dim kindInParentheses As String = If(promotionKind.Length > 0, " (" & promotionKind & ")", "")
            Dim currentMonthName As String = Date.Today.ToString("MMMM", New CultureInfo("th-TH"))
            Dim bodyText As String =
                "เนื่องจาก Shop The Factory Kafe จัดกิจกรรมโปรโมชั่นเดือน " & currentMonthName & kindInParentheses &
                " เพื่อกระตุ้นยอดขายและระบายสินค้าที่ขายออกช้า จึงขออนุมัติจัดกิจกรรมและโปรโมชั่นเพื่อกระตุ้นยอดขาย ใน Shop The Factory Kafe รายละเอียดการจัดกิจกรรมและโปรโมชั่นมีดังนี้"

            Dim output As DataRow = reportTable.NewRow()
            output("DocumentDate") = Date.Today
            output("DocumentNo") = "PROMO" & promotionId.ToString("000000", CultureInfo.InvariantCulture)
            output("SubjectText") = "ขออนุมัติจัดกิจกรรมและโปรโมชั่น " & promotionName
            output("BodyText") = bodyText
            output("Recipient1") = "คุณโรจนินทร์  พัชรเรืองกิตติ์    กรรมการผู้จัดการ"
            output("Recipient2") = "คุณยงยุทธ์  นุ่มศิริ    ผู้จัดการทั่วไป"
            output("CopyRecipient") = "คุณรุ่งจิตต์  อิ่มนุกูลกิจ    ผู้จัดการฝ่ายบัญชีและการเงิน"
            output("LineNo") = lineNo
            output("PromotionText") = promotionText
            output("RewardText") = rewardText
            output("ActivityDateText") = startDate.ToString("dd/MM/yyyy") & " - " & endDate.ToString("dd/MM/yyyy")
            reportTable.Rows.Add(output)
            lineNo += 1
        Next
        Return reportTable
    End Function

    Private Function ExtractPromotionKind(comment As String) As String
        If String.IsNullOrWhiteSpace(comment) Then Return ""

        Dim value As String = comment.Trim()
        Const prefix As String = "รูปแบบโปรโมชั่น:"
        If value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) Then
            value = value.Substring(prefix.Length).Trim()
        End If

        Dim separatorIndex As Integer = value.IndexOf(";"c)
        If separatorIndex >= 0 Then value = value.Substring(0, separatorIndex)
        Return value.Trim()
    End Function

    Private Function ProductDisplayText(row As DataRow, Optional unitText As String = "") As String
        Dim productName As String = Convert.ToString(row("ProductName")).Trim()
        Dim number As Decimal = If(row.IsNull("DetailNumber"), 1D, Convert.ToDecimal(row("DetailNumber")))
        If number <= 0D Then number = 1D
        If String.IsNullOrWhiteSpace(unitText) Then unitText = "แพ็ค"
        Return productName & " " & number.ToString("N0") & " " & unitText
    End Function

    Private Sub SavePromotion(detail As PromotionDetailForm)
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using tr As SqlTransaction = conn.BeginTransaction()
                Try
                    Dim id As Integer
                    Using cmd As New SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM PMT WITH (UPDLOCK,HOLDLOCK)", conn, tr)
                        id = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using
                    Using cmd As New SqlCommand("INSERT INTO PMT(ID,Type,Name,Price,DiscountP,DiscountM,Priority,SDate,EDate,Comment,Status,CONumber,UONumber,CTime,UTime) VALUES(@ID,@Type,@Name,@Price,@DiscountP,@DiscountM,0,@SDate,@EDate,@Comment,@Status,@User,@User,GETDATE(),GETDATE())", conn, tr)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = detail.PromotionType
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = detail.PromotionName
                        cmd.Parameters.Add("@Price", SqlDbType.Money).Value = If(detail.IsFixedPriceDiscount OrElse detail.IsBundleChoiceType, detail.SpecialPrice, 0D)
                        cmd.Parameters.Add("@DiscountP", SqlDbType.Money).Value = If(detail.IsDirectPercentDiscount, detail.SpecialPrice, 0D)
                        cmd.Parameters.Add("@DiscountM", SqlDbType.Money).Value = If(detail.IsDirectMoneyDiscount, detail.SpecialPrice, 0D)
                        cmd.Parameters.Add("@SDate", SqlDbType.DateTime).Value = detail.StartDate
                        cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = detail.EndDate
                        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 300).Value = detail.BuildComment()
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(detail.IsActivePromotion, CType(DBNull.Value, Object), CType(0, Object))
                        cmd.Parameters.Add("@User", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
                        cmd.ExecuteNonQuery()
                    End Using
                    InsertPromotionDetails(conn, tr, id, detail)
                    Using cmd As New SqlCommand("INSERT INTO PMTBranch(ID,BranchNumber) SELECT @ID,BranchNumber FROM Branch WHERE Status IS NULL", conn, tr)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                        cmd.ExecuteNonQuery()
                    End Using
                    tr.Commit()
                Catch
                    tr.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Sub UpdatePromotion(detail As PromotionDetailForm)
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using tr As SqlTransaction = conn.BeginTransaction()
                Try
                    'การแก้ไขโปรโมชั่นที่ถูกยกเลิกไว้ก่อนหน้า ต้องเปิดใช้งานกลับมาเมื่อกดบันทึก
                    Using cmd As New SqlCommand("UPDATE PMT SET Type=@Type,Name=@Name,Price=@Price,DiscountP=@DiscountP,DiscountM=@DiscountM,SDate=@SDate,EDate=@EDate,Comment=@Comment,Status=@Status,UONumber=@User,UTime=GETDATE() WHERE ID=@ID", conn, tr)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = detail.PromotionID
                        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = detail.PromotionType
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 300).Value = detail.PromotionName
                        cmd.Parameters.Add("@Price", SqlDbType.Money).Value = If(detail.IsFixedPriceDiscount OrElse detail.IsBundleChoiceType, detail.SpecialPrice, 0D)
                        cmd.Parameters.Add("@DiscountP", SqlDbType.Money).Value = If(detail.IsDirectPercentDiscount, detail.SpecialPrice, 0D)
                        cmd.Parameters.Add("@DiscountM", SqlDbType.Money).Value = If(detail.IsDirectMoneyDiscount, detail.SpecialPrice, 0D)
                        cmd.Parameters.Add("@SDate", SqlDbType.DateTime).Value = detail.StartDate
                        cmd.Parameters.Add("@EDate", SqlDbType.DateTime).Value = detail.EndDate
                        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar, 300).Value = detail.BuildComment()
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(detail.IsActivePromotion, CType(DBNull.Value, Object), CType(0, Object))
                        cmd.Parameters.Add("@User", SqlDbType.Int).Value = CInt(Math.Max(0, ModSession.LoginOfficerID))
                        If cmd.ExecuteNonQuery() = 0 Then Throw New ApplicationException("ไม่พบโปรโมชั่นที่ต้องการแก้ไข")
                    End Using
                    Using cmd As New SqlCommand("DELETE FROM PMTD WHERE ID=@ID AND Part IN (1,2)", conn, tr)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = detail.PromotionID
                        cmd.ExecuteNonQuery()
                    End Using
                    InsertPromotionDetails(conn, tr, detail.PromotionID, detail)
                    tr.Commit()
                Catch
                    tr.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Sub InsertPromotionDetails(conn As SqlConnection, tr As SqlTransaction, id As Integer, detail As PromotionDetailForm)
        If detail.IsBundleChoiceType Then
            Dim mainItem As Integer = 1
            For Each pn As Long In detail.BundleMainPNs
                Using cmd As New SqlCommand("INSERT INTO PMTD(ID,Item,Part,PN,ProductGroupID,BrandID,Number,Price,DiscountP,DiscountM) SELECT @ID,@Item,1,P.PN,ISNULL(P.GroupID,0),ISNULL(P.BrandID,0),1,0,0,0 FROM Product P WHERE P.PN=@PN", conn, tr)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@Item", SqlDbType.Int).Value = mainItem
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = CInt(pn)
                    If cmd.ExecuteNonQuery() = 0 Then Throw New ApplicationException("ไม่พบรหัสสินค้าหลัก PN: " & pn.ToString())
                End Using
                mainItem += 1
            Next

            Dim optionItem As Integer = 1
            For Each pn As Long In detail.BundleOptionPNs
                Using cmd As New SqlCommand("INSERT INTO PMTD(ID,Item,Part,PN,ProductGroupID,BrandID,Number,Price,DiscountP,DiscountM) SELECT @ID,@Item,2,P.PN,ISNULL(P.GroupID,0),ISNULL(P.BrandID,0),1,@Price,0,0 FROM Product P WHERE P.PN=@PN", conn, tr)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@Item", SqlDbType.Int).Value = optionItem
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = CInt(pn)
                    cmd.Parameters.Add("@Price", SqlDbType.Money).Value = detail.SpecialPrice
                    If cmd.ExecuteNonQuery() = 0 Then Throw New ApplicationException("ไม่พบรหัสเครื่องดื่ม PN: " & pn.ToString())
                End Using
                optionItem += 1
            Next
        ElseIf detail.IsDirectDiscountType Then
            Dim item As Integer = 1
            For Each pn As Long In detail.RewardPNs
                Using cmd As New SqlCommand("INSERT INTO PMTD(ID,Item,Part,PN,ProductGroupID,BrandID,Number,Price,DiscountP,DiscountM) SELECT @ID,@Item,1,P.PN,ISNULL(P.GroupID,0),ISNULL(P.BrandID,0),@Number,@Price,@DiscountP,@DiscountM FROM Product P WHERE P.PN=@PN", conn, tr)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@Item", SqlDbType.Int).Value = item
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = CInt(pn)
                    cmd.Parameters.Add("@Number", SqlDbType.Money).Value = detail.MinimumAmount
                    cmd.Parameters.Add("@Price", SqlDbType.Money).Value = If(detail.IsFixedPriceDiscount, detail.SpecialPrice, 0D)
                    cmd.Parameters.Add("@DiscountP", SqlDbType.Money).Value = If(detail.IsDirectPercentDiscount, detail.SpecialPrice, 0D)
                    cmd.Parameters.Add("@DiscountM", SqlDbType.Money).Value = If(detail.IsDirectMoneyDiscount, detail.SpecialPrice, 0D)
                    If cmd.ExecuteNonQuery() = 0 Then Throw New ApplicationException("ไม่พบรหัสสินค้า PN: " & pn.ToString())
                End Using
                item += 1
            Next
        Else
            Using cmd As New SqlCommand("INSERT INTO PMTD(ID,Item,Part,PN,ProductGroupID,BrandID,Number,Price,DiscountP,DiscountM) VALUES(@ID,1,1,0,0,0,@Number,0,0,0)", conn, tr)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@Number", SqlDbType.Money).Value = detail.MinimumAmount
                cmd.ExecuteNonQuery()
            End Using
            Dim item As Integer = 1
            For Each pn As Long In detail.RewardPNs
                Using cmd As New SqlCommand("INSERT INTO PMTD(ID,Item,Part,PN,ProductGroupID,BrandID,Number,Price,DiscountP,DiscountM) SELECT @ID,@Item,2,P.PN,ISNULL(P.GroupID,0),ISNULL(P.BrandID,0),1,@Price,0,0 FROM Product P WHERE P.PN=@PN", conn, tr)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@Item", SqlDbType.Int).Value = item
                    cmd.Parameters.Add("@PN", SqlDbType.Int).Value = CInt(pn)
                    cmd.Parameters.Add("@Price", SqlDbType.Money).Value = detail.SpecialPrice
                    If cmd.ExecuteNonQuery() = 0 Then Throw New ApplicationException("ไม่พบรหัสสินค้า PN: " & pn.ToString())
                End Using
                item += 1
            Next
        End If
    End Sub



End Class

Friend Class PromotionDetailForm
    Inherits Form
    Private ReadOnly txtName As New TextBox()
    Private ReadOnly cboCategory As New ComboBox()
    Private ReadOnly txtMinimum As New TextBox()
    Private ReadOnly lstRewards As New ListBox()
    Private ReadOnly btnPickProduct As New Button()
    Private ReadOnly txtPrice As New TextBox()
    Private ReadOnly dtStart As New DateTimePicker()
    Private ReadOnly dtEnd As New DateTimePicker()
    Private ReadOnly cboPromotionStatus As New ComboBox()
    Private ReadOnly btnSave As New Button()
    Private ReadOnly btnCancel As New Button()
    Private ReadOnly lblPromotionType As New Label()
    Private ReadOnly cboPromotionType As New ComboBox()
    Private ReadOnly selectedRewards As New List(Of Tuple(Of Long, String))()
    Private ReadOnly selectedBundleMain As New List(Of Tuple(Of Long, String))()
    Private ReadOnly selectedBundleOptions As New List(Of Tuple(Of Long, String))()
    Private ReadOnly lstBundleMain As New ListBox()
    Private ReadOnly lstBundleOptions As New ListBox()
    Private ReadOnly btnPickBundleMain As New Button()
    Private ReadOnly btnPickBundleOptions As New Button()
    Private ReadOnly normalProductPanel As New Panel()
    Private ReadOnly bundleProductPanel As New TableLayoutPanel()
    Public ReadOnly Property PromotionID As Integer
    Private editID As Integer
    Private _promotionType As Integer

    Public ReadOnly Property PromotionType As Integer
        Get
            Return _promotionType
        End Get
    End Property

    Public ReadOnly Property IsDirectDiscountType As Boolean
        Get
            Return _promotionType = 1 OrElse _promotionType = 2 OrElse _promotionType = 5
        End Get
    End Property

    Public ReadOnly Property IsBundleChoiceType As Boolean
        Get
            Return _promotionType = 15
        End Get
    End Property

    Public ReadOnly Property IsActivePromotion As Boolean
        Get
            Return cboPromotionStatus.SelectedIndex = 1
        End Get
    End Property

    Public ReadOnly Property IsFixedPriceDiscount As Boolean
        Get
            Return _promotionType = 5
        End Get
    End Property

    Public ReadOnly Property IsDirectMoneyDiscount As Boolean
        Get
            Return _promotionType = 2
        End Get
    End Property

    Public ReadOnly Property IsDirectPercentDiscount As Boolean
        Get
            Return _promotionType = 1
        End Get
    End Property

    Public ReadOnly Property PromotionName As String
        Get
            Return txtName.Text.Trim()
        End Get
    End Property
    Public ReadOnly Property IsRafflePromotion As Boolean
        Get
            Dim value As String = PromotionName
            Return value.Contains("จับสลาก") OrElse value.Contains("ลุ้นรางวัล") OrElse value.Contains("ชิงโชค")
        End Get
    End Property
    Public Function BuildComment() As String
        Dim kind As String
        If IsRafflePromotion Then
            kind = "จับสลาก"
        ElseIf _promotionType = 2 Then
            kind = "ลดเป็นเงิน"
        ElseIf _promotionType = 1 Then
            kind = "ลดเป็นเปอร์เซ็นต์"
        ElseIf _promotionType = 3 Then
            kind = "ขายรวมราคาพิเศษ"
        ElseIf _promotionType = 4 Then
            kind = "ลดเป็นเปอร์เซ็นต์ทั้งชุด"
        ElseIf _promotionType = 5 Then
            kind = "ลดราคาเหลือ"
        ElseIf _promotionType = 15 Then
            kind = "ชุดสินค้าเลือกเครื่องดื่ม 1 รายการ"
        Else
            kind = "แลกซื้อสินค้า"
        End If
        Return "รูปแบบโปรโมชั่น: " & kind & "; ประเภทสินค้า: " & CategoryName & "; ยอดขั้นต่ำต่อบิล: " & MinimumAmount.ToString("N2")
    End Function
    Public ReadOnly Property CategoryName As String
        Get
            Return If(cboCategory.SelectedItem, "ทั้งหมด").ToString()
        End Get
    End Property
    Public ReadOnly Property MinimumAmount As Decimal
        Get
            Dim v As Decimal
            Decimal.TryParse(txtMinimum.Text, v)
            Return v
        End Get
    End Property
    Public ReadOnly Property SpecialPrice As Decimal
        Get
            Dim v As Decimal
            Decimal.TryParse(txtPrice.Text, v)
            Return v
        End Get
    End Property
    Public ReadOnly Property StartDate As Date
        Get
            Return dtStart.Value.Date
        End Get
    End Property
    Public ReadOnly Property EndDate As Date
        Get
            Return dtEnd.Value.Date.AddDays(1).AddTicks(-1)
        End Get
    End Property
    Public ReadOnly Property RewardPNs As List(Of Long)
        Get
            Return selectedRewards.Select(Function(x) x.Item1).Distinct().ToList()
        End Get
    End Property

    Public ReadOnly Property BundleMainPNs As List(Of Long)
        Get
            Return selectedBundleMain.Select(Function(x) x.Item1).Distinct().ToList()
        End Get
    End Property

    Public ReadOnly Property BundleOptionPNs As List(Of Long)
        Get
            Return selectedBundleOptions.Select(Function(x) x.Item1).Distinct().ToList()
        End Get
    End Property

    Public Sub New(Optional promotionIDValue As Integer = 0, Optional promotionTypeValue As Integer = 10)
        editID = promotionIDValue
        PromotionID = promotionIDValue
        _promotionType = If(promotionTypeValue > 0, promotionTypeValue, 10)
        Text = If(promotionIDValue > 0, "แก้ไขโปรโมชั่นสินค้า", "เพิ่มโปรโมชั่นสินค้า")
        StartPosition = FormStartPosition.CenterParent
        Size = New Size(900, 700)
        MinimumSize = New Size(820, 650)
        Font = New Font("Cordia New", 15.0F)
        AutoScroll = True
        Dim panel As New TableLayoutPanel With {.Dock = DockStyle.Fill, .Padding = New Padding(24), .ColumnCount = 2, .RowCount = 10, .AutoScroll = True}
        panel.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 230))
        panel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 58))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 125))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 58))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 52))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 52))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 60))
        panel.RowStyles.Add(New RowStyle(SizeType.Absolute, 60))
        AddField(panel, 0, "ชื่อโปรโมชั่น", txtName)
        lblPromotionType.Text = "ซื้อครบยอด → แลกซื้อสินค้าในราคาพิเศษ"
        lblPromotionType.AutoSize = True
        cboPromotionType.DropDownStyle = ComboBoxStyle.DropDownList
        cboPromotionType.Font = New Font("Cordia New", 14.0F)
        cboPromotionType.DropDownWidth = 700
        cboPromotionType.Items.AddRange(New Object() {
            "1. ซื้อสินค้าที่กำหนดครบจำนวน → ลดเป็นเปอร์เซ็นต์",
            "2. ซื้อสินค้าที่กำหนดครบจำนวน → ลดเป็นเงิน",
            "3. ซื้อสินค้าที่กำหนดหลายรายการ → ขายรวมราคาพิเศษ",
            "4. ซื้อสินค้าที่กำหนดหลายรายการ → ลดเป็นเปอร์เซ็นต์ทั้งชุด",
            "5. ซื้อสินค้าที่กำหนด → ลดราคาเหลือ",
            "6. ซื้อสินค้าที่กำหนด → แถมสินค้าอีกชุด",
            "7. ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมในราคาพิเศษ",
            "8. ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
            "9. ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเงิน",
            "10. ซื้อสินค้าครบยอดที่กำหนด → ซื้อสินค้าแถมในราคาพิเศษ",
            "11. ซื้อสินค้าครบยอดที่กำหนด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
            "12. ซื้อสินค้าครบยอดที่กำหนด → ซื้อสินค้าแถมลดเป็นเงิน",
            "13. ซื้อสินค้าที่กำหนดครบจำนวนขั้นต่ำ → ลดเป็นเปอร์เซ็นต์",
            "14. ซื้อสินค้าอะไรก็ได้ครบจำนวนขั้นต่ำ → ลดเป็นเปอร์เซ็นต์",
            "15. ซื้อสินค้าหลัก → เลือกเครื่องดื่มได้ 1 รายการในราคาเครื่องดื่ม"})
        Dim initialTypeIndex As Integer = Math.Max(0, Math.Min(_promotionType - 1, cboPromotionType.Items.Count - 1))
        cboPromotionType.SelectedIndex = initialTypeIndex
        AddField(panel, 1, "ประเภทโปรโมชั่น", cboPromotionType)
        AddField(panel, 2, "ประเภทสินค้า", cboCategory)
        AddField(panel, 3, "ยอดซื้อขั้นต่ำ/บิล", txtMinimum)
        normalProductPanel.Dock = DockStyle.Fill
        lstRewards.Dock = DockStyle.Fill
        lstRewards.Font = New Font("Cordia New", 14.0F)
        btnPickProduct.Text = "เลือกสินค้า"
        btnPickProduct.Dock = DockStyle.Right
        btnPickProduct.Width = 120
        normalProductPanel.Controls.Add(lstRewards)
        normalProductPanel.Controls.Add(btnPickProduct)

        bundleProductPanel.Dock = DockStyle.Fill
        bundleProductPanel.ColumnCount = 2
        bundleProductPanel.RowCount = 2
        bundleProductPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        bundleProductPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        bundleProductPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 28.0!))
        bundleProductPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
        bundleProductPanel.Controls.Add(New Label With {.Text = "สินค้าหลัก (เลือกได้หลายรายการ)", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, 0)
        bundleProductPanel.Controls.Add(New Label With {.Text = "เครื่องดื่มที่ร่วมรายการ (ลูกค้าเลือกได้ 1 รายการ/ชุด)", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 1, 0)
        bundleProductPanel.Controls.Add(CreateBundlePickerPanel(lstBundleMain, btnPickBundleMain, "เลือกสินค้าหลัก"), 0, 1)
        bundleProductPanel.Controls.Add(CreateBundlePickerPanel(lstBundleOptions, btnPickBundleOptions, "เพิ่มเครื่องดื่ม"), 1, 1)
        bundleProductPanel.Visible = False

        Dim productPanel As New Panel With {.Dock = DockStyle.Fill}
        productPanel.Controls.Add(normalProductPanel)
        productPanel.Controls.Add(bundleProductPanel)
        AddField(panel, 4, "สินค้าแลกซื้อ", productPanel)
        AddField(panel, 5, "ราคาแลกซื้อ", txtPrice)
        AddField(panel, 6, "วันที่เริ่ม", dtStart)
        AddField(panel, 7, "วันที่สิ้นสุด", dtEnd)
        cboPromotionStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cboPromotionStatus.Font = New Font("Cordia New", 14.0F)
        cboPromotionStatus.Items.AddRange(New Object() {"ร่าง (Draft)", "ใช้งานอยู่"})
        cboPromotionStatus.SelectedIndex = 0
        AddField(panel, 8, "สถานะโปรโมชั่น", cboPromotionStatus)
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList
        txtMinimum.Text = "299"
        txtPrice.Text = "25"
        btnSave.Text = "บันทึกโปรโมชั่น"
        btnSave.BackColor = Color.RoyalBlue
        btnSave.ForeColor = Color.White
        btnSave.Width = 170
        btnSave.Height = 42
        btnCancel.Text = "ยกเลิก"
        btnCancel.Width = 110
        btnCancel.Height = 42
        btnCancel.DialogResult = DialogResult.Cancel
        Dim buttonPanel As New FlowLayoutPanel With {.Dock = DockStyle.Fill, .FlowDirection = FlowDirection.RightToLeft, .AutoSize = True}
        buttonPanel.Controls.Add(btnSave)
        buttonPanel.Controls.Add(btnCancel)
        Controls.Add(panel)
        dtStart.Format = DateTimePickerFormat.Short
        dtEnd.Format = DateTimePickerFormat.Short
        dtEnd.Value = Date.Today.AddMonths(1)
        LoadCategories()
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler txtName.TextChanged, Sub() UpdatePromotionType()
        AddHandler cboPromotionType.SelectedIndexChanged, AddressOf PromotionTypeChanged
        AddHandler btnCancel.Click, Sub() DialogResult = DialogResult.Cancel
        AddHandler btnPickProduct.Click, AddressOf PickProductClick
        AddHandler lstRewards.DoubleClick, AddressOf RemoveSelectedProduct
        AddHandler btnPickBundleMain.Click, AddressOf PickBundleMainClick
        AddHandler btnPickBundleOptions.Click, AddressOf PickBundleOptionsClick
        AddHandler lstBundleMain.DoubleClick, AddressOf RemoveBundleMainProduct
        AddHandler lstBundleOptions.DoubleClick, AddressOf RemoveBundleOptionProduct
        panel.Controls.Add(buttonPanel, 1, 9)
        AcceptButton = btnSave
        CancelButton = btnCancel
        ConfigurePromotionType()
        If editID > 0 Then LoadExistingPromotion(editID)
    End Sub

    Private Sub PromotionTypeChanged(sender As Object, e As EventArgs)
        If cboPromotionType.SelectedIndex < 0 Then Return
        Dim newType As Integer = cboPromotionType.SelectedIndex + 1
        If newType = _promotionType Then Return

        _promotionType = newType
        selectedRewards.Clear()
        selectedBundleMain.Clear()
        selectedBundleOptions.Clear()
        lstRewards.Items.Clear()
        lstBundleMain.Items.Clear()
        lstBundleOptions.Items.Clear()
        ConfigurePromotionType()
    End Sub

    Private Function CreateBundlePickerPanel(targetList As ListBox, pickButton As Button, buttonText As String) As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill, .Padding = New Padding(3)}
        targetList.Dock = DockStyle.Fill
        targetList.Font = New Font("Cordia New", 13.0F)
        pickButton.Text = buttonText
        pickButton.Dock = DockStyle.Bottom
        pickButton.Height = 32
        panel.Controls.Add(targetList)
        panel.Controls.Add(pickButton)
        Return panel
    End Function

    Private Sub ConfigurePromotionType()
        If _promotionType = 15 Then
            lblPromotionType.Text = "ซื้อสินค้าหลัก → เลือกเครื่องดื่มได้ 1 รายการในราคาเครื่องดื่ม"
            panelLabel("ยอดซื้อขั้นต่ำ/บิล", "จำนวนชุด")
            panelLabel("สินค้าแลกซื้อ", "รายการสินค้าในชุด")
            panelLabel("ราคาแลกซื้อ", "ราคาเครื่องดื่มที่แลกซื้อ")
            txtMinimum.Text = "1"
            txtPrice.Text = "0"
            SetBundleEditorVisible(True)
        ElseIf _promotionType = 2 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อเมนูที่กำหนดครบจำนวน → ลดเป็นเงิน"
            panelLabel("ยอดซื้อขั้นต่ำ/บิล", "จำนวนขั้นต่ำ/เมนู")
            panelLabel("สินค้าแลกซื้อ", "เมนูที่ลดราคา")
            panelLabel("ราคาแลกซื้อ", "ส่วนลด/เมนู")
            txtMinimum.Text = "1"
            txtPrice.Text = "10"
        ElseIf _promotionType = 1 Then
            lblPromotionType.Text = "ซื้อเมนูที่กำหนดครบจำนวน → ลดเป็นเปอร์เซ็นต์"
            panelLabel("ยอดซื้อขั้นต่ำ/บิล", "จำนวนขั้นต่ำ/เมนู")
            panelLabel("สินค้าแลกซื้อ", "เมนูที่ลดราคา")
            panelLabel("ราคาแลกซื้อ", "ส่วนลด %/เมนู")
            txtMinimum.Text = "1"
            txtPrice.Text = "10"
        ElseIf _promotionType = 3 Then
            lblPromotionType.Text = "ซื้อสินค้าที่กำหนดหลายรายการ → ขายรวมราคาพิเศษ"
            panelLabel("ยอดซื้อขั้นต่ำ/บิล", "จำนวนขั้นต่ำ/ชุด")
            panelLabel("สินค้าแลกซื้อ", "สินค้าที่ร่วมโปรโมชั่น")
            panelLabel("ราคาแลกซื้อ", "ราคารวมพิเศษ")
            txtMinimum.Text = "1"
            txtPrice.Text = "0"
        ElseIf _promotionType = 4 Then
            lblPromotionType.Text = "ซื้อสินค้าที่กำหนดหลายรายการ → ลดเป็นเปอร์เซ็นต์ทั้งชุด"
            panelLabel("ยอดซื้อขั้นต่ำ/บิล", "จำนวนขั้นต่ำ/ชุด")
            panelLabel("สินค้าแลกซื้อ", "สินค้าที่ร่วมโปรโมชั่น")
            panelLabel("ราคาแลกซื้อ", "ส่วนลด % ทั้งชุด")
            txtMinimum.Text = "1"
            txtPrice.Text = "10"
        ElseIf _promotionType = 5 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อเมนูที่กำหนด → ลดราคาเหลือ"
            panelLabel("ยอดซื้อขั้นต่ำ/บิล", "จำนวนขั้นต่ำ/เมนู")
            panelLabel("สินค้าแลกซื้อ", "เมนูที่ลดราคา")
            panelLabel("ราคาแลกซื้อ", "ราคาขายหลังลด")
            txtMinimum.Text = "1"
            txtPrice.Text = "0"
        ElseIf _promotionType >= 6 AndAlso _promotionType <= 12 Then
            SetBundleEditorVisible(False)
            Dim typeTexts As String() = {
                "ซื้อสินค้าที่กำหนด → แถมสินค้าอีกชุด",
                "ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมในราคาพิเศษ",
                "ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
                "ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเงิน",
                "ซื้อสินค้าครบยอด → ซื้อสินค้าแถมในราคาพิเศษ",
                "ซื้อสินค้าครบยอด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
                "ซื้อสินค้าครบยอด → ซื้อสินค้าแถมลดเป็นเงิน"}
            lblPromotionType.Text = typeTexts(_promotionType - 6)
        ElseIf IsRafflePromotion Then
            SetBundleEditorVisible(False)
            UpdatePromotionType()
        Else
            SetBundleEditorVisible(False)
        End If
    End Sub

    Private Sub panelLabel(oldText As String, newText As String)
        For Each control As Control In Controls.OfType(Of TableLayoutPanel)().SelectMany(Function(x) x.Controls.Cast(Of Control)())
            Dim label = TryCast(control, Label)
            If label IsNot Nothing AndAlso label.Text = oldText Then label.Text = newText
        Next
    End Sub

    Private Sub UpdatePromotionType()
        If _promotionType = 15 Then
            SetBundleEditorVisible(True)
            lblPromotionType.Text = "ซื้อสินค้าหลัก → เลือกเครื่องดื่มได้ 1 รายการในราคาเครื่องดื่ม"
            btnPickBundleMain.Enabled = True
            btnPickBundleOptions.Enabled = True
            lstBundleMain.Enabled = True
            lstBundleOptions.Enabled = True
            txtPrice.Enabled = True
        ElseIf _promotionType = 2 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อเมนูที่กำหนดครบจำนวน → ลดเป็นเงิน"
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        ElseIf _promotionType = 1 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อเมนูที่กำหนดครบจำนวน → ลดเป็นเปอร์เซ็นต์"
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        ElseIf _promotionType = 3 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อสินค้าที่กำหนดหลายรายการ → ขายรวมราคาพิเศษ"
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        ElseIf _promotionType = 4 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อสินค้าที่กำหนดหลายรายการ → ลดเป็นเปอร์เซ็นต์ทั้งชุด"
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        ElseIf _promotionType = 5 Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อเมนูที่กำหนด → ลดราคาเหลือ"
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        ElseIf _promotionType >= 6 AndAlso _promotionType <= 12 Then
            SetBundleEditorVisible(False)
            Dim typeTexts As String() = {
                "ซื้อสินค้าที่กำหนด → แถมสินค้าอีกชุด",
                "ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมในราคาพิเศษ",
                "ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
                "ซื้อสินค้าที่กำหนด → ซื้อสินค้าแถมลดเป็นเงิน",
                "ซื้อสินค้าครบยอด → ซื้อสินค้าแถมในราคาพิเศษ",
                "ซื้อสินค้าครบยอด → ซื้อสินค้าแถมลดเป็นเปอร์เซ็นต์",
                "ซื้อสินค้าครบยอด → ซื้อสินค้าแถมลดเป็นเงิน"}
            lblPromotionType.Text = typeTexts(_promotionType - 6)
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        ElseIf IsRafflePromotion Then
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อครบยอด → ออกสิทธิ์จับสลาก (ไม่แลกซื้อสินค้า)"
            btnPickProduct.Enabled = False
            lstRewards.Enabled = False
            txtPrice.Enabled = False
            txtPrice.Text = "0"
        Else
            SetBundleEditorVisible(False)
            lblPromotionType.Text = "ซื้อครบยอด → แลกซื้อสินค้าในราคาพิเศษ"
            btnPickProduct.Enabled = True
            lstRewards.Enabled = True
            txtPrice.Enabled = True
        End If
    End Sub

    Private Sub SetBundleEditorVisible(isVisible As Boolean)
        bundleProductPanel.Visible = isVisible
        normalProductPanel.Visible = Not isVisible
    End Sub

    Private Sub AddField(panel As TableLayoutPanel, row As Integer, caption As String, control As Control)
        panel.Controls.Add(New Label With {.Text = caption, .AutoSize = True}, 0, row)
        control.Dock = DockStyle.Fill
        panel.Controls.Add(control, 1, row)
    End Sub

    Private Sub LoadCategories()
        cboCategory.Items.Add("ทั้งหมด")
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT RTRIM(Name) FROM Category WHERE Status IS NULL ORDER BY Name", conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read() : cboCategory.Items.Add(reader.GetString(0).Trim()) : End While
                End Using
            End Using
        End Using
        cboCategory.SelectedIndex = 0
    End Sub

    Private Sub LoadExistingPromotion(id As Integer)
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT Type,RTRIM(Name),SDate,EDate,RTRIM(Comment),Status FROM PMT WHERE ID=@ID", conn)
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            _promotionType = If(reader.IsDBNull(0), 10, Convert.ToInt32(reader(0)))
                            If _promotionType >= 1 AndAlso _promotionType <= cboPromotionType.Items.Count Then
                                cboPromotionType.SelectedIndex = _promotionType - 1
                            End If
                            cboPromotionStatus.SelectedIndex = If(reader.IsDBNull(5) OrElse Convert.ToInt32(reader(5)) <> 0, 1, 0)
                            txtName.Text = reader.GetString(1).Trim()
                            dtStart.Value = reader.GetDateTime(2)
                            dtEnd.Value = reader.GetDateTime(3)
                            Dim comment As String = If(reader.IsDBNull(4), "", reader.GetString(4))
                            Dim amountMatch As Match = Regex.Match(comment, "ยอดขั้นต่ำต่อบิล\s*:\s*([0-9,\.]+)")
                            If amountMatch.Success Then txtMinimum.Text = amountMatch.Groups(1).Value
                            Dim categoryMatch As Match = Regex.Match(comment, "ประเภทสินค้า\s*:\s*([^;]+)")
                            If categoryMatch.Success Then
                                Dim categoryIndex As Integer = cboCategory.FindStringExact(categoryMatch.Groups(1).Value.Trim())
                                If categoryIndex >= 0 Then cboCategory.SelectedIndex = categoryIndex
                            End If
                        End If
                    End Using
                End Using
                'อัปเดตข้อความ/สถานะช่องตาม Type ที่อ่านจากฐานข้อมูล
                'โดยไม่รีเซ็ตค่าจำนวนและส่วนลดที่โหลดมาแล้ว
                UpdatePromotionType()
                If IsBundleChoiceType Then
                    LoadBundleItems(conn, id, 1, selectedBundleMain, lstBundleMain)
                    LoadBundleItems(conn, id, 2, selectedBundleOptions, lstBundleOptions)
                    Dim bundlePrice As Decimal = 0D
                    Using priceCmd As New SqlCommand("SELECT ISNULL(Price,0) FROM PMT WHERE ID=@ID", conn)
                        priceCmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                        Decimal.TryParse(Convert.ToString(priceCmd.ExecuteScalar()), bundlePrice)
                    End Using
                    txtPrice.Text = bundlePrice.ToString("N2")
                Else
                    'Type 1/2 เป็นโปรโมชั่นลดสินค้าตรง ๆ จึงเก็บสินค้าไว้ Part=1
                    'ส่วน Type อื่นใช้ Part=2 เป็นสินค้าแถมหรือสินค้าราคาพิเศษ
                    Dim detailPart As Integer = If(IsDirectDiscountType, 1, 2)
                    Using cmd As New SqlCommand("SELECT D.PN,RTRIM(P.Name),D.Price,D.DiscountP,D.DiscountM FROM PMTD D INNER JOIN Product P ON P.PN=D.PN WHERE D.ID=@ID AND D.Part=@Part ORDER BY D.Item", conn)
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
                        cmd.Parameters.Add("@Part", SqlDbType.Int).Value = detailPart
                        Using reader = cmd.ExecuteReader()
                            While reader.Read()
                                Dim pn As Long = Convert.ToInt64(reader(0))
                                selectedRewards.Add(Tuple.Create(pn, reader.GetString(1).Trim()))
                                If IsDirectPercentDiscount Then
                                    txtPrice.Text = Convert.ToDecimal(reader(3)).ToString("N2")
                                ElseIf IsDirectMoneyDiscount Then
                                    txtPrice.Text = Convert.ToDecimal(reader(4)).ToString("N2")
                                ElseIf IsFixedPriceDiscount Then
                                    Dim fixedPrice As Decimal = If(reader.IsDBNull(2), 0D, Convert.ToDecimal(reader(2)))
                                    If fixedPrice <= 0D Then
                                        Dim priceMatch As Match = Regex.Match(txtName.Text, "เหลือ\s*([0-9,]+(?:\.[0-9]+)?)\s*บาท")
                                        If priceMatch.Success Then Decimal.TryParse(priceMatch.Groups(1).Value.Replace(",", ""), fixedPrice)
                                    End If
                                    txtPrice.Text = fixedPrice.ToString("N2")
                                Else
                                    txtPrice.Text = Convert.ToDecimal(reader(2)).ToString("N2")
                                End If
                                lstRewards.Items.Add(reader.GetString(1).Trim() & "  (PN: " & pn.ToString() & ")")
                            End While
                        End Using
                    End Using
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลโปรโมชั่นไม่สำเร็จ: " & ex.Message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBundleItems(conn As SqlConnection, id As Integer, part As Integer,
                                targetItems As List(Of Tuple(Of Long, String)), targetList As ListBox)
        Using cmd As New SqlCommand("SELECT D.PN,RTRIM(P.Name) FROM PMTD D INNER JOIN Product P ON P.PN=D.PN WHERE D.ID=@ID AND D.Part=@Part ORDER BY D.Item", conn)
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
            cmd.Parameters.Add("@Part", SqlDbType.Int).Value = part
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim pn As Long = Convert.ToInt64(reader(0))
                    Dim name As String = reader.GetString(1).Trim()
                    targetItems.Add(Tuple.Create(pn, name))
                    targetList.Items.Add(name & "  (PN: " & pn.ToString() & ")")
                End While
            End Using
        End Using
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        'โปรโมชั่นลดราคาเมนูโดยตรงไม่ต้องกำหนดยอดขั้นต่ำ/บิล
        'ถ้าไม่กรอก Minimum ระบบขายจะถือเป็นขั้นต่ำ 1 เมนู
        Dim minimumRequired As Boolean = Not IsDirectDiscountType AndAlso Not IsRafflePromotion
        Dim productRequired As Boolean = Not IsRafflePromotion
        Dim missingProducts As Boolean = If(IsBundleChoiceType,
                                            BundleMainPNs.Count = 0 OrElse BundleOptionPNs.Count = 0,
                                            RewardPNs.Count = 0)
        Dim invalidRequiredFields As Boolean = PromotionName = "" OrElse
                                                (minimumRequired AndAlso MinimumAmount <= 0D) OrElse
                                                (productRequired AndAlso (SpecialPrice < 0D OrElse missingProducts))
        If invalidRequiredFields Then
            Dim message As String = If(IsBundleChoiceType,
                                       "กรุณากรอกชื่อ, ราคาเครื่องดื่ม และเลือกสินค้าหลักกับเครื่องดื่มอย่างน้อย 1 รายการ",
                                       "กรุณากรอกชื่อ, ราคาหลังลด และเลือกเมนูอย่างน้อย 1 รายการ")
            MessageBox.Show(message, "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If EndDate < StartDate Then
            MessageBox.Show("วันที่สิ้นสุดต้องไม่น้อยกว่าวันที่เริ่ม", "Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        DialogResult = DialogResult.OK
    End Sub

    Private Sub PickProductClick(sender As Object, e As EventArgs)
        If IsBundleChoiceType Then Return
        PickProductsInto(selectedRewards, lstRewards)
    End Sub

    Private Sub PickBundleMainClick(sender As Object, e As EventArgs)
        PickProductsInto(selectedBundleMain, lstBundleMain)
    End Sub

    Private Sub PickBundleOptionsClick(sender As Object, e As EventArgs)
        PickProductsInto(selectedBundleOptions, lstBundleOptions)
    End Sub

    Private Sub PickProductsInto(targetItems As List(Of Tuple(Of Long, String)), targetList As ListBox)
        Using picker As New fProduct()
            picker.IsSelectMode = True
            If picker.ShowDialog(Me) <> DialogResult.OK Then Return

            'fProduct รองรับการเลือกหลายรายการในโหมดโปรโมชั่นอยู่แล้ว
            'ต้องนำ SelectedProducts มาครบทุกแถว ไม่ใช่ใช้เฉพาะ SelectedPN รายการแรก
            'เช่น PN 261 และ PN 116 ต้องถูกบันทึกเป็นเงื่อนไขโปรโมชั่นทั้งคู่
            Dim selectedItems As IEnumerable(Of fProduct.ProductSelection) = picker.SelectedProducts
            If selectedItems Is Nothing OrElse Not selectedItems.Any() Then
                selectedItems = New List(Of fProduct.ProductSelection) From {
                    New fProduct.ProductSelection With {
                        .PN = picker.SelectedPN,
                        .ProductName = picker.SelectedProductName}}
            End If

            For Each selectedItem As fProduct.ProductSelection In selectedItems
                If selectedItem Is Nothing OrElse selectedItem.PN <= 0 Then Continue For
                If targetItems.Any(Function(x) x.Item1 = selectedItem.PN) Then Continue For

                targetItems.Add(Tuple.Create(selectedItem.PN, selectedItem.ProductName.Trim()))
                targetList.Items.Add(selectedItem.ProductName.Trim() & "  (PN: " & selectedItem.PN.ToString() & ")")
            Next
        End Using
    End Sub

    Private Sub RemoveSelectedProduct(sender As Object, e As EventArgs)
        If IsBundleChoiceType Then Return
        If lstRewards.SelectedIndex < 0 Then Return
        selectedRewards.RemoveAt(lstRewards.SelectedIndex)
        lstRewards.Items.RemoveAt(lstRewards.SelectedIndex)
    End Sub

    Private Sub RemoveBundleMainProduct(sender As Object, e As EventArgs)
        RemoveBundleItem(lstBundleMain, selectedBundleMain)
    End Sub

    Private Sub RemoveBundleOptionProduct(sender As Object, e As EventArgs)
        RemoveBundleItem(lstBundleOptions, selectedBundleOptions)
    End Sub

    Private Sub RemoveBundleItem(targetList As ListBox, targetItems As List(Of Tuple(Of Long, String)))
        If targetList.SelectedIndex < 0 Then Return
        targetItems.RemoveAt(targetList.SelectedIndex)
        targetList.Items.RemoveAt(targetList.SelectedIndex)
    End Sub
End Class
