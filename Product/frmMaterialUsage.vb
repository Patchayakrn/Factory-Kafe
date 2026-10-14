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

    End Sub

    Private Sub ConfigureGrid()
        AddTextColumn("MaterialID", "รหัสวัตถุดิบ", 125)
        AddTextColumn("MaterialDetail", "รายการวัตถุดิบ", 300)
        AddNumberColumn("BalancePrevious", "คงเหลือต้นงวด", 125)
        AddNumberColumn("RestQty", "ตรวจนับปลายงวด", 145)
        AddNumberColumn("UseFromBOM", "ใช้ตาม BOM", 135)
        AddNumberColumn("RealUse", "ใช้จริง", 105)
        AddNumberColumn("PayQty", "ตัดใช้แล้ว", 105)
        AddNumberColumn("DiffFromBOM", "ผลต่าง BOM %", 135)
        AddNumberColumn("DiffFromPay", "ผลต่างตัดใช้", 115)
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
                " INNER JOIN dbo.Product P ON P.PN=SD.PN INNER JOIN dbo.BOM B ON B.ProductID=RTRIM(P.ProductCode)" &
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
                " ISNULL(X.PayQty,0) AS PayQty, M.Unit_Use AS UnitUse" &
                " FROM dbo.MaterialDrink M LEFT JOIN PreviousStock P ON P.MaterialID=M.MaterialID AND P.RowNo=1" &
                " LEFT JOIN CurrentStock C ON C.MaterialID=M.MaterialID LEFT JOIN BomUse B ON B.MaterialID=M.MaterialID" &
                " LEFT JOIN PayUse X ON X.MaterialID=M.MaterialID)" &
                " SELECT MaterialID, MaterialDetail, BalancePrevious, RestQty, UseFromBOM, RealUse, PayQty," &
                " CASE WHEN UseFromBOM=0 THEN NULL ELSE ROUND((RealUse-UseFromBOM)/UseFromBOM*100,2) END AS DiffFromBOM," &
                " RealUse-PayQty AS DiffFromPay, UnitUse" &
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
                lblDoc.Text = "เอกสารตัดใช้: " & If(value Is Nothing OrElse value Is DBNull.Value, "ยังไม่มี", value.ToString())
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
        Dim docNo = "MAT-" & year.ToString("0000", CultureInfo.InvariantCulture) & month.ToString("00", CultureInfo.InvariantCulture)

        If MessageBox.Show("ระบบจะสร้าง/ปรับปรุงเอกสารตัดใช้ของเดือนนี้จากยอดใช้จริง" & Environment.NewLine & "ต้องการดำเนินการต่อหรือไม่?", "สร้างเอกสารตัดใช้", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr = conn.BeginTransaction(IsolationLevel.Serializable)
                    Dim documentId As Integer
                    Const headerSql As String =
                        "SELECT UsageDocumentID FROM dbo.MaterialUsageDocument WHERE BranchNumber=@Branch AND UsageYear=@Year AND UsageMonth=@Month;"
                    Using find As New SqlCommand(headerSql, conn, tr)
                        find.Parameters.Add("@Branch", SqlDbType.Int).Value = branch.Number
                        find.Parameters.Add("@Year", SqlDbType.Int).Value = year
                        find.Parameters.Add("@Month", SqlDbType.Int).Value = month
                        Dim value = find.ExecuteScalar()
                        If value Is Nothing OrElse value Is DBNull.Value Then
                            Using insertHeader As New SqlCommand("INSERT INTO dbo.MaterialUsageDocument(BranchNumber,UsageYear,UsageMonth,DocNo,UsageDate,SourceType,Status,UTime) VALUES(@Branch,@Year,@Month,@DocNo,@Date,'AUTO',NULL,GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS int);", conn, tr)
                                AddDocumentParameters(insertHeader, branch.Number, year, month, docNo)
                                documentId = Convert.ToInt32(insertHeader.ExecuteScalar(), CultureInfo.InvariantCulture)
                            End Using
                        Else
                            documentId = Convert.ToInt32(value, CultureInfo.InvariantCulture)
                            Using updateHeader As New SqlCommand("UPDATE dbo.MaterialUsageDocument SET DocNo=@DocNo,UsageDate=@Date,SourceType='AUTO',Status=NULL,UTime=GETDATE() WHERE UsageDocumentID=@ID", conn, tr)
                                updateHeader.Parameters.Add("@DocNo", SqlDbType.NVarChar, 50).Value = docNo
                                updateHeader.Parameters.Add("@Date", SqlDbType.DateTime).Value = New DateTime(year, month, DateTime.DaysInMonth(year, month))
                                updateHeader.Parameters.Add("@ID", SqlDbType.Int).Value = documentId
                                updateHeader.ExecuteNonQuery()
                            End Using
                        End If
                    End Using

                    Using deleteDetails As New SqlCommand("DELETE FROM dbo.MaterialUsageDocumentDetail WHERE UsageDocumentID=@ID", conn, tr)
                        deleteDetails.Parameters.Add("@ID", SqlDbType.Int).Value = documentId
                        deleteDetails.ExecuteNonQuery()
                    End Using

                    For Each row As DataRow In materialTable.Rows
                        Dim realUse As Decimal = If(row.IsNull("RealUse"), 0D, Convert.ToDecimal(row("RealUse"), CultureInfo.InvariantCulture))
                        If realUse <= 0D Then Continue For
                        Using insertDetail As New SqlCommand("INSERT INTO dbo.MaterialUsageDocumentDetail(UsageDocumentID,MaterialID,Qty,UnitUse) SELECT @ID,@MaterialID,@Qty,Unit_Use FROM dbo.MaterialDrink WHERE MaterialID=@MaterialID", conn, tr)
                            insertDetail.Parameters.Add("@ID", SqlDbType.Int).Value = documentId
                            insertDetail.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = Convert.ToString(row("MaterialID")).Trim()
                            insertDetail.Parameters.Add("@Qty", SqlDbType.Decimal).Value = realUse
                            insertDetail.ExecuteNonQuery()
                        End Using
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
