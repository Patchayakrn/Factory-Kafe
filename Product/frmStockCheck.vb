Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>
''' ฟอร์มบันทึกยอดตรวจสต็อกวัตถุดิบลง dbo.StockCheck
''' </summary>
Public Class frmStockCheck
    Inherits Form

    Private ReadOnly stockTable As New DataTable("StockCheck")
    Private ReadOnly stockView As DataView
    Private loading As Boolean

    Public Sub New()
        InitializeComponent()
        stockView = stockTable.DefaultView
        ConfigureGrid()
        AddHandler Load, AddressOf FormLoaded
        AddHandler btnLoad.Click, AddressOf LoadClick
        AddHandler btnAdd.Click, AddressOf AddClick
        AddHandler btnAddAll.Click, AddressOf AddAllClick
        AddHandler btnDelete.Click, AddressOf DeleteClick
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnClose.Click, Sub(sender, e) Close()
        AddHandler txtMaterialID.KeyDown, AddressOf MaterialKeyDown
        AddHandler txtFilter.KeyDown, AddressOf FilterKeyDown
        AddHandler dgvStock.CellValidating, AddressOf CellValidating
        AddHandler dgvStock.DataError, AddressOf GridDataError
    End Sub

    Private Sub FormLoaded(sender As Object, e As EventArgs)
        If MdiParent IsNot Nothing Then
            WindowState = FormWindowState.Normal
            Dock = DockStyle.Fill
        End If

        Try
            DbSchemaChecker.EnsureSupportTables()
            cboMonth.Items.Clear()
            For month As Integer = 1 To 12
                cboMonth.Items.Add(New MonthItem(month, CultureInfo.GetCultureInfo("th-TH").DateTimeFormat.GetMonthName(month)))
            Next
            nudYear.Value = Math.Max(nudYear.Minimum, Math.Min(nudYear.Maximum, Date.Today.Year + 543))
            cboMonth.SelectedIndex = Date.Today.Month - 1
            LoadExisting()
        Catch ex As Exception
            MessageBox.Show("เปิดหน้าบันทึกตรวจสต็อกไม่สำเร็จ: " & ex.Message, "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureGrid()
        dgvStock.AutoGenerateColumns = False
        dgvStock.AllowUserToAddRows = False
        dgvStock.AllowUserToDeleteRows = False
        dgvStock.MultiSelect = True
        dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvStock.EnableHeadersVisualStyles = False
        dgvStock.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 86, 105)
        dgvStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvStock.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 17.0!, FontStyle.Bold)
        dgvStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvStock.DefaultCellStyle.Font = New Font("Cordia New", 16.0!)
        dgvStock.DefaultCellStyle.SelectionBackColor = Color.FromArgb(42, 126, 189)
        dgvStock.DefaultCellStyle.SelectionForeColor = Color.White
        dgvStock.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 248, 252)
        dgvStock.RowTemplate.Height = 38

        AddTextColumn("MaterialID", "รหัสวัตถุดิบ", 150, True)
        AddTextColumn("MaterialDetail", "รายการวัตถุดิบ", 390, True)
        AddTextColumn("Brand", "ยี่ห้อ", 180, True)
        AddTextColumn("UnitUse", "หน่วยใช้", 120, True)
        Dim qtyColumn As New DataGridViewTextBoxColumn With {
            .Name = "RestQty", .HeaderText = "ยอดตรวจนับ", .DataPropertyName = "RestQty",
            .Width = 170, .ReadOnly = False,
            .DefaultCellStyle = New DataGridViewCellStyle With {
                .Format = "N2", .Alignment = DataGridViewContentAlignment.MiddleRight,
                .BackColor = Color.LightYellow
            }
        }
        dgvStock.Columns.Add(qtyColumn)
    End Sub

    Private Sub AddTextColumn(name As String, header As String, width As Integer, readOnlyValue As Boolean)
        dgvStock.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = name, .HeaderText = header, .DataPropertyName = name,
            .Width = width, .ReadOnly = readOnlyValue
        })
    End Sub

    Private Sub LoadClick(sender As Object, e As EventArgs)
        LoadExisting()
    End Sub

    Private Sub LoadExisting()
        If loading OrElse cboMonth.SelectedItem Is Nothing Then Return
        loading = True
        Try
            Cursor = Cursors.WaitCursor
            stockTable.Clear()
            Dim period = SelectedPeriod()
            Const sql As String =
                "SELECT RTRIM(S.MaterialID) AS MaterialID, " &
                "       RTRIM(ISNULL(M.Material_Detail,N'')) AS MaterialDetail, " &
                "       RTRIM(ISNULL(M.Brand,N'')) AS Brand, " &
                "       RTRIM(ISNULL(M.Unit_Use,N'')) AS UnitUse, " &
                "       ISNULL(S.Rest_Qty,0) AS RestQty " &
                "FROM dbo.StockCheck AS S " &
                "LEFT JOIN dbo.MaterialDrink AS M ON LTRIM(RTRIM(M.MaterialID))=LTRIM(RTRIM(S.MaterialID)) " &
                "WHERE S.[Year]=@Year AND S.[Month]=@Month " &
                "ORDER BY S.MaterialID"

            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Year", SqlDbType.SmallInt).Value = period.YearValue
                    cmd.Parameters.Add("@Month", SqlDbType.TinyInt).Value = period.MonthValue
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(stockTable)
                    End Using
                End Using
            End Using
            ApplyFilter()
            UpdateSummary()
        Catch ex As Exception
            MessageBox.Show("โหลดข้อมูลตรวจสต็อกไม่สำเร็จ: " & ex.Message, "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            Cursor = Cursors.Default
            loading = False
        End Try
    End Sub

    Private Sub AddClick(sender As Object, e As EventArgs)
        Dim materialID As String = txtMaterialID.Text.Trim()
        If materialID = "" Then
            MessageBox.Show("กรุณากรอกรหัสวัตถุดิบก่อนเพิ่มรายการ", "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtMaterialID.Focus()
            Return
        End If

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 RTRIM(MaterialID) AS MaterialID, RTRIM(ISNULL(Material_Detail,N'')) AS MaterialDetail, " &
                    "RTRIM(ISNULL(Brand,N'')) AS Brand, RTRIM(ISNULL(Unit_Use,N'')) AS UnitUse " &
                    "FROM dbo.MaterialDrink WHERE LTRIM(RTRIM(MaterialID))=@MaterialID OR Material_Detail LIKE N'%' + @MaterialID + N'%' " &
                    "ORDER BY CASE WHEN LTRIM(RTRIM(MaterialID))=@MaterialID THEN 0 ELSE 1 END, MaterialID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 200).Value = materialID
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If Not reader.Read() Then
                            MessageBox.Show("ไม่พบรหัสหรือรายการวัตถุดิบนี้ใน MaterialDrink", "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                        AddMaterialRow(Convert.ToString(reader("MaterialID"), CultureInfo.CurrentCulture).Trim(),
                                       Convert.ToString(reader("MaterialDetail"), CultureInfo.CurrentCulture).Trim(),
                                       Convert.ToString(reader("Brand"), CultureInfo.CurrentCulture).Trim(),
                                       Convert.ToString(reader("UnitUse"), CultureInfo.CurrentCulture).Trim())
                    End Using
                End Using
            End Using
            txtMaterialID.Clear()
            txtMaterialID.Focus()
        Catch ex As Exception
            MessageBox.Show("เพิ่มรายการตรวจสต็อกไม่สำเร็จ: " & ex.Message, "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub AddAllClick(sender As Object, e As EventArgs)
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT RTRIM(MaterialID) AS MaterialID, RTRIM(ISNULL(Material_Detail,N'')) AS MaterialDetail, " &
                    "RTRIM(ISNULL(Brand,N'')) AS Brand, RTRIM(ISNULL(Unit_Use,N'')) AS UnitUse " &
                    "FROM dbo.MaterialDrink ORDER BY MaterialID"
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            AddMaterialRow(Convert.ToString(reader("MaterialID"), CultureInfo.CurrentCulture).Trim(),
                                           Convert.ToString(reader("MaterialDetail"), CultureInfo.CurrentCulture).Trim(),
                                           Convert.ToString(reader("Brand"), CultureInfo.CurrentCulture).Trim(),
                                           Convert.ToString(reader("UnitUse"), CultureInfo.CurrentCulture).Trim())
                        End While
                    End Using
                End Using
            End Using
            UpdateSummary()
        Catch ex As Exception
            MessageBox.Show("เพิ่มรายการวัตถุดิบทั้งหมดไม่สำเร็จ: " & ex.Message, "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub AddMaterialRow(materialID As String, detail As String, brand As String, unitUse As String)
        For Each row As DataRow In stockTable.Rows
            If String.Equals(Convert.ToString(row("MaterialID"), CultureInfo.CurrentCulture).Trim(), materialID, StringComparison.OrdinalIgnoreCase) Then
                dgvStock.ClearSelection()
                Dim existingIndex As Integer = stockTable.Rows.IndexOf(row)
                If existingIndex >= 0 AndAlso existingIndex < dgvStock.Rows.Count Then dgvStock.Rows(existingIndex).Selected = True
                Return
            End If
        Next
        stockTable.Rows.Add(materialID, detail, brand, unitUse, 0D)
        ApplyFilter()
        For Each gridRow As DataGridViewRow In dgvStock.Rows
            If String.Equals(Convert.ToString(gridRow.Cells("MaterialID").Value, CultureInfo.CurrentCulture).Trim(), materialID, StringComparison.OrdinalIgnoreCase) Then
                dgvStock.ClearSelection()
                gridRow.Selected = True
                dgvStock.CurrentCell = gridRow.Cells("RestQty")
                Exit For
            End If
        Next
        UpdateSummary()
    End Sub

    Private Sub ApplyFilter()
        Dim search As String = txtFilter.Text.Trim().Replace("'", "''")
        If search = "" Then
            stockView.RowFilter = ""
        Else
            stockView.RowFilter = "MaterialID LIKE '%" & search & "%' OR MaterialDetail LIKE '%" & search & "%' OR Brand LIKE '%" & search & "%'"
        End If
        dgvStock.DataSource = stockView
    End Sub

    Private Sub DeleteClick(sender As Object, e As EventArgs)
        If dgvStock.SelectedRows.Count = 0 Then
            MessageBox.Show("กรุณาเลือกแถวที่ต้องการลบ", "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        For index As Integer = dgvStock.SelectedRows.Count - 1 To 0 Step -1
            Dim row As DataGridViewRow = dgvStock.SelectedRows(index)
            If Not row.IsNewRow AndAlso row.DataBoundItem IsNot Nothing Then
                Dim dataRow As DataRowView = TryCast(row.DataBoundItem, DataRowView)
                If dataRow IsNot Nothing Then dataRow.Row.Delete()
            End If
        Next
        stockTable.AcceptChanges()
        ApplyFilter()
        UpdateSummary()
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        dgvStock.EndEdit()
        If stockTable.Rows.Count = 0 Then
            MessageBox.Show("ยังไม่มีรายการตรวจสต็อกให้บันทึก", "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim period = SelectedPeriod()
        Try
            Cursor = Cursors.WaitCursor
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using transaction As SqlTransaction = conn.BeginTransaction(IsolationLevel.Serializable)
                    Using deleteCommand As New SqlCommand("DELETE FROM dbo.StockCheck WHERE [Year]=@Year AND [Month]=@Month", conn, transaction)
                        deleteCommand.Parameters.Add("@Year", SqlDbType.SmallInt).Value = period.YearValue
                        deleteCommand.Parameters.Add("@Month", SqlDbType.TinyInt).Value = period.MonthValue
                        deleteCommand.ExecuteNonQuery()
                    End Using

                    For Each row As DataRow In stockTable.Rows
                        If row.RowState = DataRowState.Deleted Then Continue For
                        Dim materialID As String = Convert.ToString(row("MaterialID"), CultureInfo.CurrentCulture).Trim()
                        Dim restQty As Decimal
                        If Not Decimal.TryParse(Convert.ToString(row("RestQty"), CultureInfo.CurrentCulture), NumberStyles.Any, CultureInfo.CurrentCulture, restQty) OrElse restQty < 0D Then
                            Throw New ApplicationException("ยอดตรวจนับของ " & materialID & " ต้องเป็นตัวเลขตั้งแต่ 0 ขึ้นไป")
                        End If
                        Using insertCommand As New SqlCommand("INSERT INTO dbo.StockCheck([Year],[Month],MaterialID,Rest_Qty) VALUES(@Year,@Month,@MaterialID,@RestQty)", conn, transaction)
                            insertCommand.Parameters.Add("@Year", SqlDbType.SmallInt).Value = period.YearValue
                            insertCommand.Parameters.Add("@Month", SqlDbType.TinyInt).Value = period.MonthValue
                            insertCommand.Parameters.Add("@MaterialID", SqlDbType.NVarChar, 50).Value = materialID
                            Dim qtyParameter As SqlParameter = insertCommand.Parameters.Add("@RestQty", SqlDbType.Decimal)
                            qtyParameter.Precision = 18
                            qtyParameter.Scale = 4
                            qtyParameter.Value = restQty
                            insertCommand.ExecuteNonQuery()
                        End Using
                    Next
                    transaction.Commit()
                End Using
            End Using
            MessageBox.Show("บันทึกผลตรวจสต็อกงวด " & period.ToString() & " เรียบร้อยแล้ว", "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadExisting()
        Catch ex As Exception
            MessageBox.Show("บันทึกผลตรวจสต็อกไม่สำเร็จ: " & ex.Message, "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)
        If e.RowIndex < 0 OrElse dgvStock.Columns(e.ColumnIndex).Name <> "RestQty" Then Return
        Dim value As Decimal
        If Not Decimal.TryParse(Convert.ToString(e.FormattedValue), NumberStyles.Any, CultureInfo.CurrentCulture, value) OrElse value < 0D Then
            e.Cancel = True
            MessageBox.Show("กรุณากรอกยอดตรวจนับเป็นตัวเลขตั้งแต่ 0 ขึ้นไป", "ตรวจสต็อก", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub GridDataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        e.ThrowException = False
    End Sub

    Private Sub MaterialKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            AddClick(btnAdd, EventArgs.Empty)
        End If
    End Sub

    Private Sub FilterKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadExisting()
        End If
    End Sub

    Private Function SelectedPeriod() As PeriodItem
        Return New PeriodItem(CInt(nudYear.Value), DirectCast(cboMonth.SelectedItem, MonthItem).Number)
    End Function

    Private Sub UpdateSummary()
        Dim total As Decimal = 0D
        For Each row As DataRow In stockTable.Rows
            If row.RowState <> DataRowState.Deleted Then
                Dim value As Decimal
                If Decimal.TryParse(Convert.ToString(row("RestQty"), CultureInfo.CurrentCulture), NumberStyles.Any, CultureInfo.CurrentCulture, value) Then total += value
            End If
        Next
        lblSummary.Text = "รายการ " & stockTable.Rows.Count.ToString("N0", CultureInfo.InvariantCulture) & " รายการ | ยอดตรวจนับรวม " & total.ToString("N2", CultureInfo.CurrentCulture)
    End Sub

    Private NotInheritable Class MonthItem
        Public ReadOnly Number As Integer
        Private ReadOnly MonthName As String

        Public Sub New(numberValue As Integer, monthNameValue As String)
            Number = numberValue
            MonthName = monthNameValue
        End Sub

        Public Overrides Function ToString() As String
            Return Number.ToString("00", CultureInfo.InvariantCulture) & " - " & MonthName
        End Function
    End Class

    Private NotInheritable Class PeriodItem
        Public ReadOnly YearValue As Integer
        Public ReadOnly MonthValue As Integer

        Public Sub New(yearValue As Integer, monthValue As Integer)
            YearValue = yearValue
            MonthValue = monthValue
        End Sub

        Public Overrides Function ToString() As String
            Return YearValue.ToString(CultureInfo.InvariantCulture) & "/" & MonthValue.ToString("00", CultureInfo.InvariantCulture)
        End Function
    End Class
End Class
