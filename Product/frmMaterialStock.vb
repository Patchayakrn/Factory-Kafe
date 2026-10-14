Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

''' <summary>
''' รายงานสต็อกวัตถุดิบจากข้อมูล StockDrinkCheck งวดล่าสุดที่เลือก
''' </summary>
Partial Public Class frmMaterialStock
    Inherits Form

    Private currentTable As New DataTable("MaterialStock")
    Private printDocument As New PrintDocument()
    Private printRowIndex As Integer

    Public Sub New()
        InitializeComponent()
        'กำหนดลำดับเลเยอร์หลังจากสร้างคอนโทรล เพื่อไม่ให้ตาราง Dock=Fill คลุมแถบค้นหา
        pnlHeader.BringToFront()
        lblSummary.BringToFront()
        gridStock.Dock = DockStyle.None
        AddHandler Resize, AddressOf FormResized
        LayoutGrid()
        AddHandler Load, AddressOf FormLoaded
        AddHandler btnSearch.Click, AddressOf SearchClick
        AddHandler btnRefresh.Click, AddressOf SearchClick
        AddHandler btnCopy.Click, AddressOf CopyClick
        AddHandler btnPrint.Click, AddressOf PrintClick
        AddHandler txtSearch.KeyDown, AddressOf SearchKeyDown
        AddHandler cboPeriod.SelectedIndexChanged, AddressOf PeriodChanged
        AddHandler gridStock.CellFormatting, AddressOf GridCellFormatting
        AddHandler printDocument.PrintPage, AddressOf PrintPage
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        If MdiParent IsNot Nothing Then
            WindowState = FormWindowState.Normal
            Dock = DockStyle.Fill
        Else
            WindowState = FormWindowState.Maximized
        End If
        LayoutGrid()
        LoadPeriods()
        SearchStock()
    End Sub

    Private Sub FormResized(sender As Object, e As EventArgs)
        LayoutGrid()
    End Sub

    Private Sub LayoutGrid()
        If gridStock Is Nothing OrElse pnlHeader Is Nothing OrElse lblSummary Is Nothing Then Return
        Const headerHeight As Integer = 132
        Const summaryHeight As Integer = 40
        pnlHeader.Dock = DockStyle.None
        pnlHeader.SetBounds(0, 0, ClientSize.Width, headerHeight)
        lblSummary.Dock = DockStyle.None
        lblSummary.SetBounds(0, Math.Max(headerHeight, ClientSize.Height - summaryHeight), ClientSize.Width, summaryHeight)
        Dim top As Integer = headerHeight
        Dim height As Integer = Math.Max(0, ClientSize.Height - top - summaryHeight)
        gridStock.SetBounds(0, top, ClientSize.Width, height)
        pnlHeader.BringToFront()
        lblSummary.BringToFront()
    End Sub

    Private Sub LoadPeriods()
        Try
            cboPeriod.Items.Clear()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT DISTINCT [Year], [Month] FROM dbo.StockDrinkCheck " &
                    "ORDER BY [Year] DESC, [Month] DESC"
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cboPeriod.Items.Add(New PeriodItem(Convert.ToInt32(reader("Year")), Convert.ToInt32(reader("Month"))))
                        End While
                    End Using
                End Using
            End Using
            If cboPeriod.Items.Count > 0 Then cboPeriod.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("โหลดงวดสต็อกวัตถุดิบไม่สำเร็จ: " & ex.Message, "สต็อกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PeriodChanged(sender As Object, e As EventArgs)
        If IsHandleCreated AndAlso cboPeriod.SelectedItem IsNot Nothing Then SearchStock()
    End Sub

    Private Sub SearchKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchStock()
        End If
    End Sub

    Private Sub SearchClick(sender As Object, e As EventArgs)
        SearchStock()
    End Sub

    Private Sub SearchStock()
        Dim period As PeriodItem = TryCast(cboPeriod.SelectedItem, PeriodItem)
        If period Is Nothing Then
            currentTable = New DataTable("MaterialStock")
            gridStock.DataSource = currentTable
            lblSummary.Text = "ยังไม่มีข้อมูลงวดสต็อกวัตถุดิบ"
            Return
        End If

        Try
            Cursor = Cursors.WaitCursor
            Dim table As New DataTable("MaterialStock")
            Const sql As String =
                "SELECT M.MaterialID, LTRIM(RTRIM(M.Material_Detail)) AS MaterialDetail, " &
                "       LTRIM(RTRIM(ISNULL(M.Brand,N''))) AS Brand, " &
                "       ISNULL(S.Rest_Qty,0) AS StockQty, LTRIM(RTRIM(ISNULL(M.Unit_Use,N''))) AS UnitUse, " &
                "       ISNULL(M.Unit_per_Package,0) AS UnitPerPackage " &
                "FROM dbo.MaterialDrink AS M " &
                "LEFT JOIN dbo.StockDrinkCheck AS S ON S.MaterialID=M.MaterialID " &
                "    AND S.[Year]=@Year AND S.[Month]=@Month " &
                "WHERE @Search=N'' OR M.MaterialID LIKE N'%' + @Search + N'%' " &
                "   OR M.Material_Detail LIKE N'%' + @Search + N'%' " &
                "   OR ISNULL(M.Brand,N'') LIKE N'%' + @Search + N'%' " &
                "ORDER BY M.MaterialID"

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Year", SqlDbType.SmallInt).Value = period.YearValue
                    cmd.Parameters.Add("@Month", SqlDbType.TinyInt).Value = period.MonthValue
                    cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = txtSearch.Text.Trim()
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                End Using
            End Using

            currentTable = table
            gridStock.DataSource = currentTable
            FormatGrid()
            Dim total As Decimal = table.AsEnumerable().Sum(Function(row) If(row.IsNull("StockQty"), 0D, Convert.ToDecimal(row("StockQty"))))
            lblSummary.Text = "งวด " & period.ToString() & "  |  พบ " & table.Rows.Count.ToString("N0") & " รายการ  |  รวมคงเหลือ " & total.ToString("N2")
        Catch ex As Exception
            MessageBox.Show("ค้นหาสต็อกวัตถุดิบไม่สำเร็จ: " & ex.Message, "สต็อกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub FormatGrid()
        SetHeader("MaterialID", "รหัสวัตถุดิบ")
        SetHeader("MaterialDetail", "รายการวัตถุดิบ")
        SetHeader("Brand", "ยี่ห้อ")
        SetHeader("StockQty", "คงเหลือ")
        SetHeader("UnitUse", "หน่วย")
        SetHeader("UnitPerPackage", "จำนวน/แพ็ค")
        If gridStock.Columns.Contains("StockQty") Then gridStock.Columns("StockQty").DefaultCellStyle.Format = "N2"
        If gridStock.Columns.Contains("UnitPerPackage") Then gridStock.Columns("UnitPerPackage").DefaultCellStyle.Format = "N2"
    End Sub

    Private Sub SetHeader(name As String, header As String)
        If gridStock.Columns.Contains(name) Then gridStock.Columns(name).HeaderText = header
    End Sub

    Private Sub GridCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso gridStock.Columns(e.ColumnIndex).Name = "StockQty" AndAlso e.Value IsNot Nothing Then
            If Convert.ToDecimal(e.Value) < 0D Then e.CellStyle.ForeColor = Color.Red
        End If
    End Sub

    Private Sub CopyClick(sender As Object, e As EventArgs)
        If gridStock.GetCellCount(DataGridViewElementStates.Selected) = 0 Then
            MessageBox.Show("กรุณาเลือกรายการที่ต้องการคัดลอก", "สต็อกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Clipboard.SetDataObject(gridStock.GetClipboardContent())
        MessageBox.Show("คัดลอกข้อมูลแล้ว", "สต็อกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub PrintClick(sender As Object, e As EventArgs)
        If currentTable.Rows.Count = 0 Then
            MessageBox.Show("ไม่มีข้อมูลสำหรับพิมพ์", "สต็อกวัตถุดิบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Using preview As New PrintPreviewDialog()
            printRowIndex = 0
            preview.Document = printDocument
            preview.WindowState = FormWindowState.Maximized
            preview.ShowDialog(Me)
        End Using
    End Sub

    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)
        Using titleFont As New Font("Cordia New", 20.0!, FontStyle.Bold), bodyFont As New Font("Cordia New", 16.0!), headerFont As New Font("Cordia New", 16.0!, FontStyle.Bold)
            Dim left As Integer = e.MarginBounds.Left
            Dim y As Integer = e.MarginBounds.Top
            e.Graphics.DrawString("รายงานสต็อกวัตถุดิบ", titleFont, Brushes.Black, left, y)
            y += 34
            Dim period As PeriodItem = TryCast(cboPeriod.SelectedItem, PeriodItem)
            e.Graphics.DrawString("งวด " & If(period Is Nothing, "", period.ToString()), bodyFont, Brushes.Black, left, y)
            y += 34
            e.Graphics.DrawString("รหัส", headerFont, Brushes.Black, left, y)
            e.Graphics.DrawString("รายการวัตถุดิบ", headerFont, Brushes.Black, left + 100, y)
            e.Graphics.DrawString("คงเหลือ", headerFont, Brushes.Black, left + 470, y)
            e.Graphics.DrawString("หน่วย", headerFont, Brushes.Black, left + 590, y)
            y += 28
            While printRowIndex < currentTable.Rows.Count AndAlso y < e.MarginBounds.Bottom - 30
                Dim row As DataRow = currentTable.Rows(printRowIndex)
                e.Graphics.DrawString(Convert.ToString(row("MaterialID")), bodyFont, Brushes.Black, left, y)
                e.Graphics.DrawString(Convert.ToString(row("MaterialDetail")), bodyFont, Brushes.Black, left + 100, y)
                e.Graphics.DrawString(Convert.ToDecimal(row("StockQty")).ToString("N2"), bodyFont, Brushes.Black, left + 470, y)
                e.Graphics.DrawString(Convert.ToString(row("UnitUse")), bodyFont, Brushes.Black, left + 590, y)
                y += 25
                printRowIndex += 1
            End While
            e.HasMorePages = printRowIndex < currentTable.Rows.Count
            If Not e.HasMorePages Then printRowIndex = 0
        End Using
    End Sub

    Private NotInheritable Class PeriodItem
        Public ReadOnly YearValue As Integer
        Public ReadOnly MonthValue As Integer

        Public Sub New(yearValue As Integer, monthValue As Integer)
            Me.YearValue = yearValue
            Me.MonthValue = monthValue
        End Sub

        Public Overrides Function ToString() As String
            Return MonthValue.ToString("00") & "/" & YearValue.ToString()
        End Function
    End Class
End Class
