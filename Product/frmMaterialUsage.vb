Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

''' <summary>
''' คำนวณการใช้วัตถุดิบรายเดือนจากยอดขายและสูตร BOM
''' แทนหน้าจอ Calculate_Usage ของ Access โดยเก็บเอกสารตัดใช้ไว้ใน SQL Server
''' </summary>
Public Class frmMaterialUsage
    Inherits Form

    Private materialTable As New DataTable("MaterialUsage")
    Private loading As Boolean

    Public Sub New()
        InitializeComponent()
        ConfigureGrid()
        For month As Integer = 1 To 12
            cboMonth.Items.Add(New MonthChoice(month, CultureInfo.GetCultureInfo("th-TH").DateTimeFormat.GetMonthName(month)))
        Next
        AddHandler Load, AddressOf FormLoaded
        AddHandler btnSearch.Click, AddressOf SearchClick
        AddHandler btnRefresh.Click, AddressOf SearchClick
        AddHandler btnAutoAdjust.Click, AddressOf AutoAdjustClick
        AddHandler btnPrint.Click, AddressOf PrintReportClick
        AddHandler grid.CellFormatting, AddressOf GridCellFormatting

    End Sub

    Private Sub ConfigureGrid()
        'ใช้เฉพาะคอลัมน์ที่กำหนดเอง เพื่อไม่ให้ DataGridView สร้างคอลัมน์จาก DataTable แทรก/สลับลำดับ
        grid.AutoGenerateColumns = False
        grid.Columns.Clear()
        AddTextColumn("MaterialID", "รหัสวัตถุดิบ", 125)
        AddTextColumn("MaterialDetail", "รายการวัตถุดิบ", 300)
        AddTextColumn("UnitUse", "หน่วย", 90)
        AddNumberColumn("BalancePrevious", "คงเหลือต้นงวด", 125)
        AddNumberColumn("RestQty", "ตรวจนับปลายงวด", 145)
        AddNumberColumn("RealUse", "ใช้จริง", 105)
        AddNumberColumn("UseFromBOM", "ใช้ตาม BOM", 135)
        AddNumberColumn("DiffFromBOM", "ผลต่าง BOM %", 135)
        AddNumberColumn("PayQty", "ตัดใช้แล้ว", 105)
        AddNumberColumn("DiffFromPay", "ผลต่างตัดใช้", 115)
        grid.Columns("MaterialID").HeaderText = "รหัสวัตถุดิบ"
        grid.Columns("MaterialDetail").HeaderText = "รายการวัตถุดิบ"
        grid.Columns("UnitUse").HeaderText = "หน่วย"
        grid.Columns("BalancePrevious").HeaderText = "ยกมา"
        grid.Columns("RestQty").HeaderText = "คงเหลือ"
        grid.Columns("RealUse").HeaderText = "ใช้ไป"
        grid.Columns("UseFromBOM").HeaderText = "คิดจาก BOM ใช้"
        grid.Columns("DiffFromBOM").HeaderText = "คิดจาก BOM % Diff"
        grid.Columns("PayQty").HeaderText = "ใช้"
        grid.Columns("DiffFromPay").HeaderText = "Diff"
        grid.AllowUserToResizeRows = False
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.ColumnHeadersHeight = 54
        grid.CellBorderStyle = DataGridViewCellBorderStyle.Single
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        grid.GridColor = Color.FromArgb(165, 175, 185)
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.RowsDefaultCellStyle.BackColor = Color.White
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 126, 204)
        grid.DefaultCellStyle.SelectionForeColor = Color.White
        grid.Columns("MaterialID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        grid.Columns("MaterialDetail").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        grid.Columns("UnitUse").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    End Sub

    Private Sub GridCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim columnName As String = grid.Columns(e.ColumnIndex).Name
        If columnName <> "DiffFromBOM" AndAlso columnName <> "DiffFromPay" Then Return
        If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then Return

        Dim difference As Decimal
        If Decimal.TryParse(Convert.ToString(e.Value, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, difference) Then
            If columnName = "DiffFromBOM" Then
                e.Value = difference.ToString("N2", CultureInfo.InvariantCulture) & "%"
                e.FormattingApplied = True
            End If
            If difference < 0D Then
                e.CellStyle.ForeColor = Color.FromArgb(190, 45, 45)
            End If
        End If
    End Sub

    Private Sub AddTextColumn(name As String, header As String, width As Integer)
        grid.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = name, .HeaderText = header, .DataPropertyName = name,
            .Width = width, .ReadOnly = True
        })
    End Sub

    Private Sub AddNumberColumn(name As String, header As String, width As Integer)
        Dim column As New DataGridViewTextBoxColumn With {
            .Name = name, .HeaderText = header, .DataPropertyName = name,
            .Width = width, .ReadOnly = True,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2", .Alignment = DataGridViewContentAlignment.MiddleRight}
        }
        grid.Columns.Add(column)
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        If MdiParent IsNot Nothing Then
            WindowState = FormWindowState.Normal
            Dock = DockStyle.Fill
        Else
            WindowState = FormWindowState.Maximized
        End If

        Try
            DbSchemaChecker.EnsureSupportTables()
            LoadBranches()
            cboMonth.SelectedIndex = Date.Today.Month - 1
            nudYear.Value = Date.Today.Year
            LoadUsage()
        Catch ex As Exception
            MessageBox.Show("เปิดหน้าคำนวณการใช้วัตถุดิบไม่สำเร็จ: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBranches()
        cboBranch.Items.Clear()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT BranchNumber, RTRIM(BranchName) FROM Branch ORDER BY BranchNumber", conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        cboBranch.Items.Add(New BranchChoice(Convert.ToInt32(reader.GetValue(0)), Convert.ToString(reader.GetValue(1)).Trim()))
                    End While
                End Using
            End Using
        End Using
        If cboBranch.Items.Count > 0 Then cboBranch.SelectedIndex = 0
    End Sub

    Private Sub SearchClick(sender As Object, e As EventArgs)
        LoadUsage()
    End Sub

    Private Sub PrintReportClick(sender As Object, e As EventArgs)
        If materialTable Is Nothing OrElse materialTable.Rows.Count = 0 Then
            MessageBox.Show("กรุณาค้นหาข้อมูลก่อนพิมพ์รายงาน", "รายงานการใช้วัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If cboBranch.SelectedItem Is Nothing OrElse cboMonth.SelectedItem Is Nothing Then Return

        Dim branch As BranchChoice = DirectCast(cboBranch.SelectedItem, BranchChoice)
        Dim month As MonthChoice = DirectCast(cboMonth.SelectedItem, MonthChoice)
        Using viewer As New frmReportViewer()
            viewer.LoadMaterialUsageReport(materialTable, New DateTime(CInt(nudYear.Value), month.Number, 1), branch.Name, lblDoc.Text)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Sub LoadUsage()
        If loading OrElse cboBranch.SelectedItem Is Nothing OrElse cboMonth.SelectedItem Is Nothing Then Return
        loading = True
        Try
            Dim branch = DirectCast(cboBranch.SelectedItem, BranchChoice)
            Dim month = DirectCast(cboMonth.SelectedItem, MonthChoice).Number
            Dim year = CInt(nudYear.Value)
            Dim fromDate As New DateTime(year, month, 1)
            Dim toDate As DateTime = fromDate.AddMonths(1)

            Const sql As String =
                "WITH PreviousStock AS (" &
                " SELECT MaterialID, Rest_Qty, ROW_NUMBER() OVER(PARTITION BY MaterialID ORDER BY [Year] DESC,[Month] DESC) AS RowNo" &
                " FROM dbo.StockDrinkCheck WHERE [Year] < @Year OR ([Year] = @Year AND [Month] < @Month))," &
                " CurrentStock AS (" &
                " SELECT MaterialID, Rest_Qty FROM dbo.StockDrinkCheck WHERE [Year]=@Year AND [Month]=@Month)," &
                " BomUse AS (" &
                " SELECT B.MaterialID, SUM(CAST(SD.Number AS decimal(18,4))*B.[Use]) AS UseFromBOM" &
                " FROM dbo.SellD SD INNER JOIN dbo.Sell S ON S.BranchNumber=SD.BranchNumber AND S.SellNumber=SD.SellNumber" &
                " INNER JOIN dbo.Product P ON P.PN=SD.PN" &
                " CROSS APPLY (SELECT TOP 1 H.BOMID FROM dbo.BOMHeader H" &
                " WHERE H.ProductID=RTRIM(P.ProductCode) AND S.DateTime>=H.EffectiveFrom" &
                " AND (H.EffectiveTo IS NULL OR S.DateTime<=H.EffectiveTo)" &
                " ORDER BY H.EffectiveFrom DESC, H.VersionNo DESC) ActiveBOM" &
                " INNER JOIN dbo.BOMDetail B ON B.BOMID=ActiveBOM.BOMID" &
                " WHERE S.BranchNumber=@Branch AND S.Status IS NULL AND S.DateTime>=@FromDate AND S.DateTime<@ToDate" &
                " GROUP BY B.MaterialID)," &
                " PayUse AS (" &
                " SELECT D.MaterialID, SUM(D.Qty) AS PayQty" &
                " FROM dbo.MaterialUsageDocument U INNER JOIN dbo.MaterialUsageDocumentDetail D ON D.UsageDocumentID=U.UsageDocumentID" &
                " WHERE U.BranchNumber=@Branch AND U.UsageYear=@Year AND U.UsageMonth=@Month AND U.Status IS NULL" &
                " GROUP BY D.MaterialID)," &
                " Calc AS (" &
                " SELECT M.MaterialID, RTRIM(M.Material_Detail) AS MaterialDetail, " &
                " ISNULL(P.Rest_Qty,0) AS BalancePrevious, ISNULL(C.Rest_Qty,0) AS RestQty, " &
                " ISNULL(B.UseFromBOM,0) AS UseFromBOM, ISNULL(P.Rest_Qty,0)-ISNULL(C.Rest_Qty,0) AS RealUse, " &
                " ISNULL(X.PayQty,0) AS StoredPayQty, ISNULL(P.Rest_Qty,0)-ISNULL(C.Rest_Qty,0) AS PayQty, M.Unit_Use AS UnitUse" &
                " FROM dbo.MaterialDrink M LEFT JOIN PreviousStock P ON P.MaterialID=M.MaterialID AND P.RowNo=1" &
                " LEFT JOIN CurrentStock C ON C.MaterialID=M.MaterialID LEFT JOIN BomUse B ON B.MaterialID=M.MaterialID" &
                " LEFT JOIN PayUse X ON X.MaterialID=M.MaterialID)" &
                " SELECT MaterialID, MaterialDetail, BalancePrevious, RestQty, UseFromBOM, RealUse, StoredPayQty, PayQty," &
                " CASE WHEN UseFromBOM=0 THEN NULL ELSE ROUND((RealUse-UseFromBOM)/UseFromBOM*100,2) END AS DiffFromBOM," &
                " CAST(0 AS decimal(18,4)) AS DiffFromPay, UnitUse" &
                " FROM Calc ORDER BY MaterialID;"

            materialTable = New DataTable("MaterialUsage")
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = branch.Number
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year
                    cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month
                    cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate
                    cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(materialTable)
                    End Using
                End Using
            End Using

            grid.DataSource = materialTable
            UpdateHeader(branch.Number, year, month)
        Catch ex As Exception
            MessageBox.Show("คำนวณข้อมูลไม่สำเร็จ: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            loading = False
        End Try
    End Sub

    Private Sub UpdateHeader(branchNumber As Integer, year As Integer, month As Integer)
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand("SELECT TOP 1 DocNo FROM dbo.MaterialUsageDocument WHERE BranchNumber=@Branch AND UsageYear=@Year AND UsageMonth=@Month AND Status IS NULL", conn)
                cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = branchNumber
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month
                Dim value = cmd.ExecuteScalar()
                Dim documentNumber As String = If(value Is Nothing OrElse value Is DBNull.Value, "ยังไม่มี", value.ToString().Trim())
                lblDoc.Text = "เอกสารตัดใช้: " & documentNumber
                If documentNumber = "ยังไม่มี" Then
                    'แสดงเลขเอกสารที่จะถูกใช้ล่วงหน้าในหัวตาราง แม้ยังไม่ได้กดสร้างเอกสาร
                    grid.PayGroupTitle = GetPreviewMaterialUsageDocumentNo(conn, New DateTime(year, month, DateTime.DaysInMonth(year, month)))
                Else
                    grid.PayGroupTitle = documentNumber
                End If
                grid.Invalidate()
            End Using
        End Using

        Dim totalBom As Decimal = SumColumn("UseFromBOM")
        Dim totalReal As Decimal = SumColumn("RealUse")
        Dim totalPay As Decimal = SumColumn("PayQty")
        lblSummary.Text = String.Format(CultureInfo.InvariantCulture, "ใช้ตาม BOM {0:N2} | ใช้จริง {1:N2} | ตัดใช้แล้ว {2:N2}", totalBom, totalReal, totalPay)
    End Sub

    Private Function SumColumn(columnName As String) As Decimal
        If materialTable Is Nothing OrElse Not materialTable.Columns.Contains(columnName) Then Return 0D
        Return materialTable.AsEnumerable().Sum(Function(row) If(row.IsNull(columnName), 0D, Convert.ToDecimal(row(columnName), CultureInfo.InvariantCulture)))
    End Function

    Private Sub AutoAdjustClick(sender As Object, e As EventArgs)
        If materialTable.Rows.Count = 0 OrElse cboBranch.SelectedItem Is Nothing OrElse cboMonth.SelectedItem Is Nothing Then Return
        Dim branch = DirectCast(cboBranch.SelectedItem, BranchChoice)
        Dim month = DirectCast(cboMonth.SelectedItem, MonthChoice).Number
        Dim year = CInt(nudYear.Value)
        Dim usageDate As New DateTime(year, month, DateTime.DaysInMonth(year, month))
        Dim docNo As String = Nothing

        If MessageBox.Show("ระบบจะสร้าง/ปรับปรุงเอกสารตัดใช้ของเดือนนี้จากยอดใช้จริง" & Environment.NewLine & "ต้องการดำเนินการต่อหรือไม่?", "สร้างเอกสารตัดใช้", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr = conn.BeginTransaction(IsolationLevel.Serializable)
                    Dim documentId As Integer
                    Const headerSql As String =
                        "SELECT TOP 1 UsageDocumentID, DocNo FROM dbo.MaterialUsageDocument WHERE BranchNumber=@Branch AND UsageYear=@Year AND UsageMonth=@Month;"
                    Dim existingDocNo As String = Nothing
                    Using find As New SqlCommand(headerSql, conn, tr)
                        find.Parameters.Add("@Branch", SqlDbType.Int).Value = branch.Number
                        find.Parameters.Add("@Year", SqlDbType.Int).Value = year
                        find.Parameters.Add("@Month", SqlDbType.Int).Value = month
                        Using reader As SqlDataReader = find.ExecuteReader()
                            If reader.Read() Then
                                documentId = reader.GetInt32(0)
                                If Not reader.IsDBNull(1) Then existingDocNo = reader.GetString(1).Trim()
                            End If
                        End Using
                    End Using

                    If documentId <= 0 Then
                        docNo = GetNextMaterialUsageDocumentNo(conn, tr, usageDate)
                        Using insertHeader As New SqlCommand("INSERT INTO dbo.MaterialUsageDocument(BranchNumber,UsageYear,UsageMonth,DocNo,UsageDate,SourceType,Status,UTime) VALUES(@Branch,@Year,@Month,@DocNo,@Date,'AUTO',NULL,GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS int);", conn, tr)
                            AddDocumentParameters(insertHeader, branch.Number, year, month, docNo)
                            documentId = Convert.ToInt32(insertHeader.ExecuteScalar(), CultureInfo.InvariantCulture)
                        End Using
                    Else
                        'เอกสารเดิมที่เป็น MAT- ให้เปลี่ยนเป็นเลข FKP ครั้งแรก
                        docNo = If(IsFkpDocumentNo(existingDocNo), existingDocNo, GetNextMaterialUsageDocumentNo(conn, tr, usageDate))
                        Using updateHeader As New SqlCommand("UPDATE dbo.MaterialUsageDocument SET DocNo=@DocNo,UsageDate=@Date,SourceType='AUTO',Status=NULL,UTime=GETDATE() WHERE UsageDocumentID=@ID", conn, tr)
                            updateHeader.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = docNo
                            updateHeader.Parameters.Add("@Date", SqlDbType.DateTime).Value = usageDate
                            updateHeader.Parameters.Add("@ID", SqlDbType.Int).Value = documentId
                            updateHeader.ExecuteNonQuery()
                        End Using
                    End If

                    For Each row As DataRow In materialTable.Rows
                        Dim realUse As Decimal = If(row.IsNull("RealUse"), 0D, Convert.ToDecimal(row("RealUse"), CultureInfo.InvariantCulture))
                        Dim payQty As Decimal = If(row.IsNull("StoredPayQty"), 0D, Convert.ToDecimal(row("StoredPayQty"), CultureInfo.InvariantCulture))
                        Dim diffQty As Decimal = realUse - payQty
                        If diffQty <= 0D Then Continue For

                        Dim materialId As String = Convert.ToString(row("MaterialID")).Trim()
                        Dim updated As Integer
                        Using updateDetail As New SqlCommand("UPDATE dbo.MaterialUsageDocumentDetail SET Qty=Qty+@Qty WHERE UsageDocumentID=@ID AND MaterialID=@MaterialID", conn, tr)
                            updateDetail.Parameters.Add("@ID", SqlDbType.Int).Value = documentId
                            updateDetail.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = materialId
                            updateDetail.Parameters.Add("@Qty", SqlDbType.Decimal).Value = diffQty
                            updated = updateDetail.ExecuteNonQuery()
                        End Using

                        If updated = 0 Then
                            Using insertDetail As New SqlCommand("INSERT INTO dbo.MaterialUsageDocumentDetail(UsageDocumentID,MaterialID,Qty,UnitUse) SELECT @ID,@MaterialID,@Qty,Unit_Use FROM dbo.MaterialDrink WHERE MaterialID=@MaterialID", conn, tr)
                                insertDetail.Parameters.Add("@ID", SqlDbType.Int).Value = documentId
                                insertDetail.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = materialId
                                insertDetail.Parameters.Add("@Qty", SqlDbType.Decimal).Value = diffQty
                                insertDetail.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                    tr.Commit()
                End Using
            End Using
            MessageBox.Show("สร้าง/ปรับปรุงเอกสารตัดใช้เรียบร้อยแล้ว: " & docNo, "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsage()
        Catch ex As Exception
            MessageBox.Show("สร้างเอกสารตัดใช้ไม่สำเร็จ: " & ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub AddDocumentParameters(cmd As SqlCommand, branch As Integer, year As Integer, month As Integer, docNo As String)
        cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = branch
        cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year
        cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month
        cmd.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = docNo
        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = New DateTime(year, month, DateTime.DaysInMonth(year, month))
    End Sub

    Private Shared Function IsFkpDocumentNo(documentNo As String) As Boolean
        If String.IsNullOrWhiteSpace(documentNo) OrElse documentNo.Length <> 11 OrElse Not documentNo.StartsWith("FKP", StringComparison.OrdinalIgnoreCase) Then Return False
        Dim numberPart As Integer
        Return Integer.TryParse(documentNo.Substring(3), NumberStyles.None, CultureInfo.InvariantCulture, numberPart)
    End Function

    Private Shared Function GetNextMaterialUsageDocumentNo(conn As SqlConnection, tr As SqlTransaction, issueDate As DateTime) As String
        Dim prefix As String = GetMaterialUsageDocumentPrefix(issueDate)
        Const sql As String = "SELECT MAX(TRY_CONVERT(int, RIGHT(DocNo, 2))) FROM dbo.MaterialUsageDocument WITH (UPDLOCK, HOLDLOCK) WHERE DocNo LIKE @Prefix + N'%' AND LEN(DocNo)=11"
        Dim nextNumber As Integer = 1
        Using cmd As New SqlCommand(sql, conn, tr)
            cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = prefix
            Dim value As Object = cmd.ExecuteScalar()
            If value IsNot Nothing AndAlso value IsNot DBNull.Value Then nextNumber = Convert.ToInt32(value, CultureInfo.InvariantCulture) + 1
        End Using
        Return prefix & nextNumber.ToString("00", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function GetPreviewMaterialUsageDocumentNo(conn As SqlConnection, issueDate As DateTime) As String
        Dim prefix As String = GetMaterialUsageDocumentPrefix(issueDate)
        Const sql As String = "SELECT MAX(TRY_CONVERT(int, RIGHT(DocNo, 2))) FROM dbo.MaterialUsageDocument WHERE DocNo LIKE @Prefix + N'%' AND LEN(DocNo)=11"
        Dim nextNumber As Integer = 1
        Using cmd As New SqlCommand(sql, conn)
            cmd.Parameters.Add("@Prefix", SqlDbType.NVarChar, 20).Value = prefix
            Dim value As Object = cmd.ExecuteScalar()
            If value IsNot Nothing AndAlso value IsNot DBNull.Value Then nextNumber = Convert.ToInt32(value, CultureInfo.InvariantCulture) + 1
        End Using
        Return prefix & nextNumber.ToString("00", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function GetMaterialUsageDocumentPrefix(issueDate As DateTime) As String
        Dim buddhistYear2 As String = (issueDate.Year + 543).ToString("0000", CultureInfo.InvariantCulture).Substring(2, 2)
        Return "FKP" & buddhistYear2 & issueDate.ToString("MMdd", CultureInfo.InvariantCulture)
    End Function

    Private NotInheritable Class BranchChoice
        Public ReadOnly Number As Integer
        Public ReadOnly Name As String
        Public Sub New(number As Integer, name As String)
            Me.Number = number
            Me.Name = name
        End Sub
        Public Overrides Function ToString() As String
            Return Number.ToString(CultureInfo.InvariantCulture) & " - " & Name
        End Function
    End Class

    Private NotInheritable Class MonthChoice
        Public ReadOnly Number As Integer
        Private ReadOnly Caption As String
        Public Sub New(number As Integer, caption As String)
            Me.Number = number
            Me.Caption = caption
        End Sub
        Public Overrides Function ToString() As String
            Return Number.ToString("00", CultureInfo.InvariantCulture) & " - " & Caption
        End Function
    End Class

End Class

''' <summary>
''' DataGridView สำหรับหัวตารางแบบ 2 ชั้นของหน้าคำนวณการใช้วัตถุดิบ
''' </summary>
Public Class MaterialUsageGrid
    Inherits DataGridView

    Public Property PayGroupTitle As String = "เอกสารตัดใช้"

    Public Sub New()
        MyBase.New()
        ColumnHeadersHeight = 54
        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        EnableHeadersVisualStyles = False
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If Not ColumnHeadersVisible OrElse Columns.Count = 0 Then Return

        Dim firstHeader As Rectangle = GetCellDisplayRectangle(0, -1, False)
        If firstHeader.Height <= 0 Then Return
        Dim headerTop As Integer = firstHeader.Y
        Dim headerHeight As Integer = ColumnHeadersHeight
        Dim upperHeight As Integer = headerHeight \ 2

        Dim state As Drawing2D.GraphicsState = e.Graphics.Save()
        Try
            e.Graphics.SetClip(New Rectangle(0, headerTop, ClientSize.Width, headerHeight))
            Using blackBrush As New SolidBrush(Color.Black), whitePen As New Pen(Color.White, 1.0F)
                e.Graphics.FillRectangle(blackBrush, New Rectangle(0, headerTop, ClientSize.Width, headerHeight))

                For Each column As DataGridViewColumn In Columns
                    If Not column.Visible Then Continue For
                    Dim cellRect As Rectangle = GetCellDisplayRectangle(column.Index, -1, True)
                    If cellRect.Width <= 0 Then Continue For

                    Dim lowerRect As New Rectangle(cellRect.X, headerTop + upperHeight, cellRect.Width, headerHeight - upperHeight)
                    e.Graphics.DrawRectangle(whitePen, lowerRect)
                    DrawHeaderText(e.Graphics, column.HeaderText, lowerRect)
                Next

                DrawGroupHeader(e.Graphics, whitePen, "MaterialID", "MaterialDetail", "รายการวัตถุดิบ", headerTop, upperHeight)
                DrawStandaloneHeader(e.Graphics, whitePen, "UnitUse", "หน่วย", headerTop, headerHeight)
                DrawGroupHeader(e.Graphics, whitePen, "BalancePrevious", "RealUse", "คิดจากยอดคงเหลือ", headerTop, upperHeight)
                DrawGroupHeader(e.Graphics, whitePen, "UseFromBOM", "DiffFromBOM", "คิดจาก BOM", headerTop, upperHeight)
                DrawGroupHeader(e.Graphics, whitePen, "PayQty", "DiffFromPay", PayGroupTitle, headerTop, upperHeight)
            End Using
        Finally
            e.Graphics.Restore(state)
        End Try
    End Sub

    Private Sub DrawGroupHeader(graphics As Graphics, pen As Pen, firstName As String, lastName As String, caption As String, headerTop As Integer, groupHeight As Integer)
        If Columns(firstName) Is Nothing OrElse Columns(lastName) Is Nothing Then Return
        Dim firstRect As Rectangle = GetCellDisplayRectangle(Columns(firstName).Index, -1, True)
        Dim lastRect As Rectangle = GetCellDisplayRectangle(Columns(lastName).Index, -1, True)
        If firstRect.Width <= 0 OrElse lastRect.Width <= 0 Then Return
        Dim groupRect As New Rectangle(firstRect.X, headerTop, lastRect.Right - firstRect.X, groupHeight)
        Using brush As New SolidBrush(Color.Black)
            graphics.FillRectangle(brush, groupRect)
        End Using
        graphics.DrawRectangle(pen, groupRect)
        DrawHeaderText(graphics, caption, groupRect)
    End Sub

    Private Sub DrawStandaloneHeader(graphics As Graphics, pen As Pen, columnName As String, caption As String, headerTop As Integer, headerHeight As Integer)
        If Columns(columnName) Is Nothing Then Return
        Dim rect As Rectangle = GetCellDisplayRectangle(Columns(columnName).Index, -1, True)
        If rect.Width <= 0 Then Return
        Dim fullRect As New Rectangle(rect.X, headerTop, rect.Width, headerHeight)
        Using brush As New SolidBrush(Color.Black)
            graphics.FillRectangle(brush, fullRect)
        End Using
        graphics.DrawRectangle(pen, fullRect)
        DrawHeaderText(graphics, caption, fullRect)
    End Sub

    Private Sub DrawHeaderText(graphics As Graphics, caption As String, rect As Rectangle)
        Dim format As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center,
            .Trimming = StringTrimming.EllipsisCharacter
        }
        Using font As New Font("Cordia New", 15.0!, FontStyle.Bold), brush As New SolidBrush(Color.White)
            graphics.DrawString(If(caption, ""), font, brush, rect, format)
        End Using
        format.Dispose()
    End Sub
End Class
