Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Public Class frmBuyHistory
    Inherits Form

    Private ReadOnly dtFrom As New DateTimePicker()
    Private ReadOnly dtTo As New DateTimePicker()
    Private ReadOnly btnSearch As New Button()
    Private ReadOnly btnPreview As New Button()
    Private ReadOnly btnEdit As New Button()
    Private ReadOnly btnMonthlyReport As New Button()
    Private ReadOnly btnCancelBuy As New Button()
    Private ReadOnly btnClose As New Button()
    Private ReadOnly grid As New DataGridView()
    Private ReadOnly table As New DataTable("BuyHistory")
    Private ReadOnly lblTitle As New Label()

    Public Sub New()
        Text = "รายการซื้อย้อนหลัง"
        StartPosition = FormStartPosition.CenterParent
        WindowState = FormWindowState.Maximized
        MinimumSize = New Size(900, 550)
        Font = New Font("Cordia New", 16.0!, FontStyle.Regular)
        BackColor = Color.White

        table.Columns.Add("BranchNumber", GetType(Integer))
        table.Columns.Add("BuyNumber", GetType(Integer))
        table.Columns.Add("DocNumber", GetType(Integer))
        table.Columns.Add("BillNumber", GetType(String))
        table.Columns.Add("DateTime", GetType(DateTime))
        table.Columns.Add("SupplierName", GetType(String))
        table.Columns.Add("TotalPrice", GetType(Decimal))

        Dim top As New Panel With {.Dock = DockStyle.Top, .Height = 126, .BackColor = Color.FromArgb(235, 243, 250), .Padding = New Padding(18, 0, 18, 0)}
        lblTitle.Text = "รายการซื้อย้อนหลัง"
        lblTitle.AutoSize = False
        lblTitle.Font = New Font("Cordia New", 22.0!, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(31, 78, 121)
        lblTitle.Location = New Point(18, 8)
        lblTitle.Size = New Size(320, 38)
        top.Controls.Add(lblTitle)
        top.Controls.Add(New Label With {.Text = "ตั้งแต่วันที่", .AutoSize = True, .Location = New Point(18, 51)})
        dtFrom.Format = DateTimePickerFormat.Short
        dtFrom.Value = DateTime.Today
        dtFrom.Location = New Point(18, 77)
        dtFrom.Width = 150
        top.Controls.Add(dtFrom)
        top.Controls.Add(New Label With {.Text = "ถึงวันที่", .AutoSize = True, .Location = New Point(190, 51)})
        dtTo.Format = DateTimePickerFormat.Short
        dtTo.Value = DateTime.Today
        dtTo.Location = New Point(190, 77)
        dtTo.Width = 150
        top.Controls.Add(dtTo)

        ConfigureButton(btnSearch, "ค้นหา", 365, 77, Color.FromArgb(37, 99, 235))
        ConfigureButton(btnPreview, "ดูใบซื้อ", 485, 77, Color.FromArgb(20, 184, 166))
        ConfigureButton(btnEdit, "แก้ไขใบซื้อ", 605, 77, Color.FromArgb(245, 158, 11))
        ConfigureButton(btnMonthlyReport, "รายงานประจำเดือน", 725, 77, Color.FromArgb(124, 58, 237))
        ConfigureButton(btnCancelBuy, "ยกเลิกใบซื้อ", 880, 77, Color.FromArgb(180, 65, 65))
        ConfigureButton(btnClose, "ปิด", 1000, 77, Color.FromArgb(110, 110, 110))
        If Not CanEditBuy() Then
            btnEdit.Enabled = False
            btnEdit.BackColor = Color.LightGray
            btnEdit.ForeColor = Color.DimGray
        End If
        If Not CanCancelBuy() Then
            btnCancelBuy.Enabled = False
            btnCancelBuy.BackColor = Color.LightGray
            btnCancelBuy.ForeColor = Color.DimGray
        End If
        top.Controls.AddRange(New Control() {btnSearch, btnPreview, btnEdit, btnMonthlyReport, btnCancelBuy, btnClose})

        grid.Dock = DockStyle.Fill
        grid.ReadOnly = True
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
        grid.AutoGenerateColumns = True
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.RowHeadersVisible = False
        grid.ColumnHeadersHeight = 42
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(214, 231, 245)
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 55, 75)
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.DefaultCellStyle.Font = New Font("Cordia New", 15.0!, FontStyle.Regular)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 126, 202)
        grid.DefaultCellStyle.SelectionForeColor = Color.White
        grid.RowTemplate.Height = 34
        grid.GridColor = Color.FromArgb(190, 205, 218)
        grid.DataSource = table

        AddHandler btnSearch.Click, AddressOf SearchHistory
        AddHandler btnPreview.Click, AddressOf PreviewSelected
        AddHandler btnEdit.Click, AddressOf EditSelected
        AddHandler btnMonthlyReport.Click, AddressOf PreviewMonthlyReport
        AddHandler btnCancelBuy.Click, AddressOf CancelSelected
        AddHandler btnClose.Click, Sub() Close()
        AddHandler grid.CellDoubleClick,
            Sub(sender, e)
                If e.RowIndex >= 0 Then PreviewSelected(sender, e)
            End Sub
        AddHandler Load, Sub() SearchHistory(Nothing, EventArgs.Empty)

        Controls.Add(grid)
        Controls.Add(top)
    End Sub

    Private Sub ConfigureButton(button As Button, caption As String, left As Integer, top As Integer, color As Color)
        button.Text = caption
        button.Location = New Point(left, top)
        button.Size = New Size(105, 38)
        button.BackColor = color
        button.ForeColor = Color.White
        button.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub SearchHistory(sender As Object, e As EventArgs)
        table.Clear()
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT Buy.BranchNumber, Buy.BuyNumber, Buy.DocNumber, " &
                    "ISNULL(RTRIM(Buy.BillNumber), '') AS BillNumber, Buy.DateTime, " &
                    "ISNULL(RTRIM(Supplier.Name), '') AS SupplierName, " &
                    "ISNULL(Buy.TotalPrice, 0) AS TotalPrice " &
                    "FROM Buy LEFT JOIN Supplier ON Buy.SupplierNumber = Supplier.SupplierNumber " &
                    "WHERE Buy.Status IS NULL AND Buy.DateTime >= @DateFrom " &
                    "AND Buy.DateTime < @DateTo ORDER BY Buy.DateTime DESC, Buy.BuyNumber DESC"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtFrom.Value.Date
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtTo.Value.Date.AddDays(1)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            table.Rows.Add(reader("BranchNumber"), reader("BuyNumber"), reader("DocNumber"),
                                           Convert.ToString(reader("BillNumber")).Trim(), reader("DateTime"),
                                           Convert.ToString(reader("SupplierName")).Trim(), reader("TotalPrice"))
                        End While
                    End Using
                End Using
            End Using
            ConfigureColumns()
        Catch ex As Exception
            MessageBox.Show("ค้นหารายการซื้อย้อนหลังไม่สำเร็จ: " & ex.Message, "รายการซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureColumns()
        SetColumn("BranchNumber", "สาขา", 8, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("BuyNumber", "เลขที่ซื้อ", 12, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("DocNumber", "เลขที่เอกสาร", 12, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("BillNumber", "เลขที่บิลผู้ขาย", 15, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("DateTime", "วันที่ซื้อ", 14, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("SupplierName", "ผู้ขาย", 28, DataGridViewContentAlignment.MiddleLeft)
        If grid.Columns("TotalPrice") IsNot Nothing Then
            grid.Columns("TotalPrice").HeaderText = "ยอดรวม"
            grid.Columns("TotalPrice").FillWeight = 11
            grid.Columns("TotalPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grid.Columns("TotalPrice").DefaultCellStyle.Format = "N2"
        End If
    End Sub

    Private Sub SetColumn(name As String, caption As String, fillWeight As Single, alignment As DataGridViewContentAlignment)
        If grid.Columns(name) Is Nothing Then Return
        grid.Columns(name).HeaderText = caption
        grid.Columns(name).FillWeight = fillWeight
        grid.Columns(name).DefaultCellStyle.Alignment = alignment
    End Sub

    Private Sub PreviewSelected(sender As Object, e As EventArgs)
        If grid.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกใบซื้อก่อน", "รายการซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim buyNumber As Integer = Convert.ToInt32(grid.CurrentRow.Cells("BuyNumber").Value, CultureInfo.InvariantCulture)
        Dim branchNumber As Integer = Convert.ToInt32(grid.CurrentRow.Cells("BranchNumber").Value, CultureInfo.InvariantCulture)
        frmBuy.PreviewSavedBuy(buyNumber, branchNumber, Me)
    End Sub

    Private Sub PreviewMonthlyReport(sender As Object, e As EventArgs)
        Try
            Cursor = Cursors.WaitCursor
            Dim reportTable As New DataTable("BuyMonthly")
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT B.DateTime AS BuyDate, CONVERT(date,B.DateTime) AS BuyDay, B.BuyNumber, B.BranchNumber, B.DocNumber, " &
                    "ISNULL(RTRIM(B.BillNumber),'') AS BillNumber, " &
                    "ISNULL(RTRIM(S.Name),'') AS SupplierName, " &
                    "ISNULL(RTRIM(P.ProductCode),'') AS ProductCode, " &
                    "ISNULL(RTRIM(P.Barcode),'') AS Barcode, " &
                    "ISNULL(RTRIM(P.Name),'') AS ProductName, " &
                    "ISNULL(BD.Number,0) AS Quantity, ISNULL(RTRIM(BD.LUnit),'') AS UnitName, " &
                    "ISNULL(BD.PriceBD,0) AS UnitPrice, ISNULL(BD.Price,0) AS NetPrice, " &
                    "ISNULL(BD.SumPrice,0) AS LineTotal " &
                    "FROM Buy AS B " &
                    "INNER JOIN BuyD AS BD ON BD.BranchNumber=B.BranchNumber AND BD.BuyNumber=B.BuyNumber " &
                    "LEFT JOIN Supplier AS S ON S.SupplierNumber=B.SupplierNumber " &
                    "LEFT JOIN Product AS P ON P.PN=BD.PN " &
                    "WHERE B.BranchNumber=1 AND B.Status IS NULL " &
                    "AND B.DateTime>=@DateFrom AND B.DateTime<@DateTo " &
                    "ORDER BY B.DateTime, B.BuyNumber, P.Name, P.ProductCode, BD.Item"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtFrom.Value.Date
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtTo.Value.Date.AddDays(1)
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(reportTable)
                    End Using
                End Using
            End Using

            If reportTable.Rows.Count = 0 Then
                MessageBox.Show("ไม่พบรายการซื้อในช่วงวันที่ที่เลือก", "รายงานซื้อประจำเดือน", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using viewer As New frmReportViewer()
                viewer.LoadBuyMonthlyReport(reportTable, dtFrom.Value.Date, dtTo.Value.Date, "สาขา 1 (สำนักงานใหญ่)")
                viewer.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show("เปิดรายงานซื้อประจำเดือนไม่สำเร็จ: " & ex.Message, "รายงานซื้อประจำเดือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub EditSelected(sender As Object, e As EventArgs)
        If Not CanEditBuy() Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์แก้ไขรายการใบซื้อ", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If grid.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกใบซื้อก่อน", "รายการซื้อย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim buyNumber As Integer = Convert.ToInt32(grid.CurrentRow.Cells("BuyNumber").Value, CultureInfo.InvariantCulture)
        Dim branchNumber As Integer = Convert.ToInt32(grid.CurrentRow.Cells("BranchNumber").Value, CultureInfo.InvariantCulture)
        Using editor As New frmBuy()
            If editor.LoadExistingBuy(buyNumber, branchNumber) Then
                editor.ShowDialog(Me)
                SearchHistory(Nothing, EventArgs.Empty)
            End If
        End Using
    End Sub

    Private Sub CancelSelected(sender As Object, e As EventArgs)
        If Not CanCancelBuy() Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์ยกเลิกใบซื้อ", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If grid.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกใบซื้อก่อน", "ยกเลิกใบซื้อ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim buyNumber As Integer = Convert.ToInt32(grid.CurrentRow.Cells("BuyNumber").Value, CultureInfo.InvariantCulture)
        Dim branchNumber As Integer = Convert.ToInt32(grid.CurrentRow.Cells("BranchNumber").Value, CultureInfo.InvariantCulture)

        If MessageBox.Show("ต้องการยกเลิกใบซื้อเลขที่ " & buyNumber.ToString(CultureInfo.InvariantCulture) & " ใช่หรือไม่?" & Environment.NewLine &
                           "ระบบจะนำรายการนี้ออกจากสต๊อกและรายงานซื้อ แต่จะเก็บประวัติเอกสารไว้", "ยืนยันยกเลิกใบซื้อ",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            VoidBuy(branchNumber, buyNumber)
            MessageBox.Show("ยกเลิกใบซื้อเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SearchHistory(Nothing, EventArgs.Empty)
        Catch ex As Exception
            MessageBox.Show("ยกเลิกใบซื้อไม่สำเร็จ: " & ex.Message, "ยกเลิกใบซื้อ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub VoidBuy(branchNumber As Integer, buyNumber As Integer)
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using tr As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim status As Object
                    Using checkCmd As New SqlCommand("SELECT TOP 1 ISNULL(Status,0) FROM Buy WITH (UPDLOCK,HOLDLOCK) WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber", conn, tr)
                        checkCmd.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        checkCmd.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
                        status = checkCmd.ExecuteScalar()
                    End Using

                    If status Is Nothing OrElse status Is DBNull.Value Then Throw New InvalidOperationException("ไม่พบใบซื้อที่ต้องการยกเลิก")
                    If Convert.ToInt32(status, CultureInfo.InvariantCulture) <> 0 Then Throw New InvalidOperationException("ใบซื้อนี้ถูกยกเลิกไปแล้ว")

                    Const stockCheckSql As String =
                        "SELECT bs.PN, ISNULL(bs.Number,0) AS BuyQuantity, s.PN AS StockPN, ISNULL(s.Number,0) AS CurrentQuantity " &
                        "FROM BuyStock bs WITH (UPDLOCK,HOLDLOCK) " &
                        "LEFT JOIN Stock s WITH (UPDLOCK,HOLDLOCK) ON s.BranchNumber=bs.BranchNumber AND s.BillType=0 " &
                        "AND s.BillNumber=bs.BuyNumber AND s.PN=bs.PN " &
                        "WHERE bs.BranchNumber=@BranchNumber AND bs.BuyNumber=@BuyNumber"
                    Using checkStock As New SqlCommand(stockCheckSql, conn, tr)
                        checkStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        checkStock.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
                        Using reader As SqlDataReader = checkStock.ExecuteReader()
                            While reader.Read()
                                If reader("StockPN") Is DBNull.Value OrElse
                                   Convert.ToDecimal(reader("CurrentQuantity"), CultureInfo.InvariantCulture) < Convert.ToDecimal(reader("BuyQuantity"), CultureInfo.InvariantCulture) Then
                                    Throw New InvalidOperationException("ใบซื้อนี้มีสินค้าที่ถูกขายหรือตัดสต๊อกไปแล้ว จึงยกเลิกทั้งใบไม่ได้")
                                End If
                            End While
                        End Using
                    End Using

                    Using deleteStock As New SqlCommand("DELETE FROM Stock WHERE BranchNumber=@BranchNumber AND BillType=0 AND BillNumber=@BuyNumber", conn, tr)
                        deleteStock.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        deleteStock.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
                        deleteStock.ExecuteNonQuery()
                    End Using

                    Using updateBuy As New SqlCommand("UPDATE Buy SET Status=1, UONumber=@Officer, UTime=GETDATE(), EditCount=ISNULL(EditCount,0)+1 WHERE BranchNumber=@BranchNumber AND BuyNumber=@BuyNumber AND (Status IS NULL OR Status=0)", conn, tr)
                        updateBuy.Parameters.Add("@Officer", SqlDbType.Int).Value = Math.Max(0, ModSession.LoginOfficerID)
                        updateBuy.Parameters.Add("@BranchNumber", SqlDbType.Int).Value = branchNumber
                        updateBuy.Parameters.Add("@BuyNumber", SqlDbType.Int).Value = buyNumber
                        If updateBuy.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("ไม่สามารถเปลี่ยนสถานะใบซื้อได้")
                    End Using

                    tr.Commit()
                Catch
                    Try : tr.Rollback() : Catch : End Try
                    Throw
                End Try
            End Using
        End Using
    End Sub

    Private Function CanEditBuy() As Boolean
        Return PermissionService.HasPermission("Buy.Edit")
    End Function

    Private Function CanCancelBuy() As Boolean
        Return PermissionService.HasPermission("Buy.Cancel")
    End Function
End Class
