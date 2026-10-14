Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class fInventoryCount
    Private Const BranchNumber As Integer = 1
    Private ReadOnly table As New DataTable()
    Private WithEvents inventoryPrintDocument As New PrintDocument()
    Private printRowIndex As Integer

    Public Sub New()
        InitializeComponent()
        ConfigureGrid()
    End Sub

    Private Sub fInventoryCount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboGroup.SelectedIndex = 0
        cboBrand.SelectedIndex = 0
        LoadStock()
    End Sub

    Private Sub ConfigureGrid()
        dgvStock.AutoGenerateColumns = False
        dgvStock.AllowUserToAddRows = False
        dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvStock.EnableHeadersVisualStyles = False
        dgvStock.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(218, 235, 249)
        dgvStock.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        dgvStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvStock.DefaultCellStyle.Font = New Font("Cordia New", 16.0!)
        dgvStock.DefaultCellStyle.SelectionBackColor = Color.FromArgb(96, 139, 151)
        dgvStock.DefaultCellStyle.SelectionForeColor = Color.White
        dgvStock.RowTemplate.Height = 40
        AddColumn("PN", "ID", 60, False)
        AddColumn("ProductCode", "รหัสสินค้า", 115, False)
        AddColumn("Barcode", "Barcode", 145, False)
        AddColumn("ProductName", "ชื่อสินค้า", 260, False)
        AddColumn("SUnit", "หน่วย", 80, False)
        AddColumn("NumStock", "คงเหลือในระบบ", 135, True, "N2")
        'ช่องยอดตรวจนับต้องแก้ไขได้ เพื่อให้ผู้ใช้งานคีย์จำนวนที่นับจริง
        AddColumn("NumCount", "ยอดตรวจนับ", 125, False, "N2")
        AddColumn("NumLost", "ขาด", 100, False, "N2")
        AddColumn("NumOver", "เกิน", 100, False, "N2")
    End Sub

    Private Sub AddColumn(name As String, header As String, width As Integer, isReadOnly As Boolean, Optional format As String = "")
        Dim col As New DataGridViewTextBoxColumn With {.Name = name, .HeaderText = header, .DataPropertyName = name, .Width = width, .ReadOnly = isReadOnly}
        If format <> "" Then col.DefaultCellStyle.Format = format
        If name = "NumCount" Then col.DefaultCellStyle.BackColor = Color.LightYellow
        dgvStock.Columns.Add(col)
    End Sub

    Private Sub LoadStock()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT Stock.PN, RTRIM(ISNULL(Product.ProductCode,'')) AS ProductCode, " &
                    "RTRIM(ISNULL(Product.Barcode,'')) AS Barcode, RTRIM(ISNULL(Product.Name,'')) AS ProductName, " &
                    "RTRIM(ISNULL(Product.SUnit,'')) AS SUnit, SUM(ISNULL(Stock.Number,0)) AS NumStock, " &
                    "ISNULL(InventoryCount.Number,0) AS NumCount, " &
                    "CASE WHEN ISNULL(InventoryCount.Number,0)<SUM(ISNULL(Stock.Number,0)) THEN SUM(ISNULL(Stock.Number,0))-ISNULL(InventoryCount.Number,0) ELSE 0 END AS NumLost, " &
                    "CASE WHEN ISNULL(InventoryCount.Number,0)>SUM(ISNULL(Stock.Number,0)) THEN ISNULL(InventoryCount.Number,0)-SUM(ISNULL(Stock.Number,0)) ELSE 0 END AS NumOver " &
                    "FROM Stock LEFT JOIN Product ON Stock.PN=Product.PN " &
                    "LEFT JOIN InventoryCount ON InventoryCount.PN=Stock.PN AND InventoryCount.BranchNumber=Stock.BranchNumber " &
                    "WHERE Stock.BranchNumber=@BranchNumber AND Product.Status IS NULL " &
                    "AND (@Keyword='' OR Product.Name LIKE N'%' + @Keyword + N'%' OR Product.ProductCode LIKE N'%' + @Keyword + N'%' OR Product.Barcode LIKE N'%' + @Keyword + N'%') " &
                    "GROUP BY Stock.PN, Product.ProductCode, Product.Barcode, Product.Name, Product.SUnit, InventoryCount.Number " &
                    "HAVING SUM(ISNULL(Stock.Number,0)) <> 0 " &
                    "ORDER BY Product.ProductCode"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = BranchNumber
                    cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = txtSearch.Text.Trim()
                    table.Clear()
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(table)
                    End Using
                    dgvStock.DataSource = table
                    lblCount.Text = table.Rows.Count.ToString(CultureInfo.InvariantCulture) & " รายการ"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลตรวจนับไม่สำเร็จ: " & ex.Message, "Inventory Count", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub dgvStock_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStock.CellEndEdit
        If e.RowIndex < 0 OrElse dgvStock.Columns(e.ColumnIndex).Name <> "NumCount" Then Return
        Dim row = dgvStock.Rows(e.RowIndex)
        Dim stock = ToDecimal(row.Cells("NumStock").Value)
        Dim count = ToDecimal(row.Cells("NumCount").Value)
        row.Cells("NumLost").Value = Math.Max(0D, stock - count)
        row.Cells("NumOver").Value = Math.Max(0D, count - stock)
    End Sub

    Private Function ToDecimal(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
        Dim result As Decimal
        If Decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, result) Then Return result
        Return 0D
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using tr As SqlTransaction = conn.BeginTransaction()
                    For Each row As DataGridViewRow In dgvStock.Rows
                        If row.IsNewRow Then Continue For
                        Dim pn = Convert.ToInt32(row.Cells("PN").Value, CultureInfo.InvariantCulture)
                        Dim count = ToDecimal(row.Cells("NumCount").Value)
                        Using cmd As New SqlCommand("IF EXISTS (SELECT 1 FROM InventoryCount WHERE BranchNumber=@B AND PN=@PN) UPDATE InventoryCount SET Number=@N WHERE BranchNumber=@B AND PN=@PN ELSE INSERT INTO InventoryCount (BranchNumber,PN,Number) VALUES (@B,@PN,@N)", conn, tr)
                            cmd.Parameters.Add("@B", SqlDbType.Int).Value = BranchNumber
                            cmd.Parameters.Add("@PN", SqlDbType.Int).Value = pn
                            cmd.Parameters.Add("@N", SqlDbType.Decimal).Value = count
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    tr.Commit()
                End Using
            End Using
            MessageBox.Show("บันทึกผลตรวจนับเรียบร้อยแล้ว", "Inventory Count", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStock()
        Catch ex As Exception
            MessageBox.Show("บันทึกผลตรวจนับไม่สำเร็จ: " & ex.Message, "Inventory Count", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadStock()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If dgvStock.Rows.Count = 0 Then
            MessageBox.Show("ไม่สามารถพิมพ์ได้ เนื่องจากไม่มีรายการตรวจนับสินค้า", "Inventory Count", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        dgvStock.EndEdit()
        If table.Rows.Count = 0 Then
            MessageBox.Show("ไม่สามารถพิมพ์ได้ เนื่องจากไม่มีรายการตรวจนับสินค้า", "Inventory Count", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        'รายงานแสดงเฉพาะสินค้าที่มีคงเหลือไม่เท่ากับ 0.00
        Dim reportTable As DataTable = table.Clone()
        For Each sourceRow As DataRow In table.Rows
            If Not sourceRow.IsNull("NumStock") AndAlso ToDecimal(sourceRow("NumStock")) <> 0D Then
                reportTable.ImportRow(sourceRow)
            End If
        Next

        If reportTable.Rows.Count = 0 Then
            MessageBox.Show("ไม่มีรายการที่มีคงเหลือไม่เท่ากับ 0.00", "Inventory Count", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using viewer As New frmReportViewer()
            viewer.LoadInventoryCountReport(reportTable)
            viewer.ShowDialog(Me)
        End Using
    End Sub

    Private Sub inventoryPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs) Handles inventoryPrintDocument.BeginPrint
        printRowIndex = 0
    End Sub

    Private Sub inventoryPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles inventoryPrintDocument.PrintPage
        Dim g As Graphics = e.Graphics
        Dim titleFont As New Font("Cordia New", 16.0!, FontStyle.Bold)
        Dim headerFont As New Font("Cordia New", 16.0!, FontStyle.Bold)
        Dim bodyFont As New Font("Cordia New", 16.0!)
        Dim brush As Brush = Brushes.Black
        Dim left As Integer = e.MarginBounds.Left
        Dim top As Integer = e.MarginBounds.Top
        '31 รายการจะพอดีใน 1 หน้าแนวนอน โดยไม่ตัดรายการท้ายหน้า
        Dim rowHeight As Integer = 20
        Dim widths() As Integer = {45, 100, 220, 80, 95, 95, 70, 70}
        Dim headers() As String = {"ลำดับ", "รหัสสินค้า", "ชื่อสินค้า", "หน่วย", "คงเหลือ", "ตรวจนับ", "ขาด", "เกิน"}

        g.DrawString("รายงานผลตรวจนับสินค้า", titleFont, brush, left, top)
        g.DrawString("วันที่พิมพ์: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), bodyFont, brush, left, top + 30)
        top += 65

        Dim x As Integer = left
        For i As Integer = 0 To headers.Length - 1
            g.FillRectangle(Brushes.LightSteelBlue, x, top, widths(i), rowHeight)
            g.DrawRectangle(Pens.Gray, x, top, widths(i), rowHeight)
            DrawCellText(g, headers(i), headerFont, brush, x, top, widths(i), rowHeight, StringAlignment.Center)
            x += widths(i)
        Next

        top += rowHeight

        While printRowIndex < dgvStock.Rows.Count AndAlso top + rowHeight <= e.MarginBounds.Bottom
            Dim row As DataGridViewRow = dgvStock.Rows(printRowIndex)
            If Not row.IsNewRow Then
                x = left
                Dim values() As String = {
                    (printRowIndex + 1).ToString(CultureInfo.InvariantCulture),
                    CellText(row, "ProductCode"),
                    CellText(row, "ProductName"),
                    CellText(row, "SUnit"),
                    DecimalText(row, "NumStock"),
                    DecimalText(row, "NumCount"),
                    DecimalText(row, "NumLost"),
                    DecimalText(row, "NumOver")
                }

                For i As Integer = 0 To values.Length - 1
                    g.DrawRectangle(Pens.LightGray, x, top, widths(i), rowHeight)
                    Dim alignment As StringAlignment = If(i >= 4, StringAlignment.Far, If(i = 0, StringAlignment.Center, StringAlignment.Near))
                    DrawCellText(g, values(i), bodyFont, brush, x, top, widths(i), rowHeight, alignment)
                    x += widths(i)
                Next
                top += rowHeight
            End If
            printRowIndex += 1
        End While

        e.HasMorePages = printRowIndex < dgvStock.Rows.Count
        If Not e.HasMorePages Then printRowIndex = 0

        titleFont.Dispose()
        headerFont.Dispose()
        bodyFont.Dispose()
    End Sub

    Private Shared Function CellText(row As DataGridViewRow, columnName As String) As String
        Dim value As Object = row.Cells(columnName).Value
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""
        Return Convert.ToString(value, CultureInfo.CurrentCulture).Trim()
    End Function

    Private Shared Function DecimalText(row As DataGridViewRow, columnName As String) As String
        Dim value As Decimal = 0D
        Decimal.TryParse(CellText(row, columnName), NumberStyles.Any, CultureInfo.CurrentCulture, value)
        Return value.ToString("N2", CultureInfo.CurrentCulture)
    End Function

    Private Shared Sub DrawCellText(g As Graphics,
                                    value As String,
                                    font As Font,
                                    brush As Brush,
                                    x As Integer,
                                    y As Integer,
                                    width As Integer,
                                    height As Integer,
                                    alignment As StringAlignment)
        Using format As New StringFormat With {
            .Alignment = alignment,
            .LineAlignment = StringAlignment.Center,
            .Trimming = StringTrimming.EllipsisCharacter
        }
            g.DrawString(value, font, brush, New RectangleF(x + 4, y, width - 8, height), format)
        End Using
    End Sub
End Class
