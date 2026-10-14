Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' หน้าตรวจสอบสินค้าในคลังสินค้า (แปลงจาก SabuySoft.fStock)
''' ใช้ DataGridView มาตรฐานแทน Janus GridEX/DevExpress
''' </summary>
Partial Public Class fStock
    Inherits Form

    Private ReadOnly branchIds As New List(Of Integer)()
    Private isLoading As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub StockLoad(sender As Object, e As EventArgs)
        Try
            isLoading = True
            LoadCombo(cboBranch, "SELECT BranchNumber, RTRIM(BranchName) AS BranchName FROM Branch WHERE Status IS NULL ORDER BY BranchName", "BranchName", True)
            LoadCombo(cboCategory, "SELECT ID, RTRIM(Name) AS Name FROM Category WHERE Status IS NULL ORDER BY Name", "Name", False)
            LoadCombo(cboGroup, "SELECT ID, RTRIM(Name) AS Name FROM ProductGroup WHERE Status IS NULL ORDER BY Name", "Name", False)
            LoadCombo(cboBrand, "SELECT ID, RTRIM(Name) AS Name FROM Brand WHERE Status IS NULL ORDER BY Name", "Name", False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "โหลดข้อมูลไม่สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isLoading = False
        End Try
        SearchStock()
    End Sub

    Private Sub LoadCombo(combo As ComboBox, sql As String, displayField As String, isBranch As Boolean)
        combo.Items.Clear()
        combo.DropDownStyle = ComboBoxStyle.DropDownList
        If Not isBranch Then combo.Items.Add(New ComboItem(0, "ทั้งหมด"))
        branchIds.Clear()
        Using conn = modVariable.OpenConnection()
            Using cmd As New SqlCommand(sql, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim id = Convert.ToInt32(reader(If(isBranch, "BranchNumber", "ID")))
                        combo.Items.Add(New ComboItem(id, reader(displayField).ToString().Trim()))
                        If isBranch Then branchIds.Add(id)
                    End While
                End Using
            End Using
        End Using
        If combo.Items.Count > 0 Then combo.SelectedIndex = 0
    End Sub

    Private Function SelectedId(combo As ComboBox) As Integer
        Dim item = TryCast(combo.SelectedItem, ComboItem)
        Return If(item Is Nothing, 0, item.Id)
    End Function

    Private Function SelectedBranch() As Integer
        Dim id = SelectedId(cboBranch)
        Return If(id = 0, 1, id)
    End Function

    Private Sub SearchClick(sender As Object, e As EventArgs)
        SearchStock()
    End Sub

    Private Sub SearchKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchStock()
        End If
    End Sub

    Private Sub SearchStock()
        If isLoading Then Return
        Try
            Cursor = Cursors.WaitCursor
            Dim sql = "SELECT p.PN, RTRIM(p.ProductCode) AS ProductCode, RTRIM(p.Barcode) AS Barcode, " &
                      "RTRIM(p.Name) AS ProductName, RTRIM(c.Name) AS CategoryName, " &
                      "RTRIM(pg.Name) AS GroupName, RTRIM(b.Name) AS BrandName, " &
                      "SUM(s.Number) AS StockNumber, RTRIM(p.SUnit) AS Unit, " &
                      "SUM(s.Number * s.BuyPrice) AS StockValue " &
                      "FROM Stock s LEFT JOIN Product p ON p.PN=s.PN " &
                      "LEFT JOIN ProductGroup pg ON p.GroupID=pg.ID " &
                      "LEFT JOIN Category c ON pg.CategoryID=c.ID " &
                      "LEFT JOIN Brand b ON p.BrandID=b.ID " &
                      "WHERE s.BranchNumber=@Branch AND p.Status IS NULL " &
                      "AND (@Search='' OR p.ProductCode LIKE @Like OR p.Barcode LIKE @Like OR p.Name LIKE @Like) " &
                      "AND (@Category=0 OR c.ID=@Category) AND (@Group=0 OR pg.ID=@Group) AND (@Brand=0 OR b.ID=@Brand) " &
                      "GROUP BY p.PN,p.ProductCode,p.Barcode,p.Name,c.Name,pg.Name,b.Name,p.SUnit " &
                      "ORDER BY p.ProductCode,p.Name"

            Dim dt As New DataTable()
            Using conn = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Dim search = txtSearch.Text.Trim()
                    cmd.Parameters.Add("@Branch", SqlDbType.Int).Value = SelectedBranch()
                    cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200).Value = search
                    cmd.Parameters.Add("@Like", SqlDbType.NVarChar, 210).Value = "%" & search & "%"
                    cmd.Parameters.Add("@Category", SqlDbType.Int).Value = SelectedId(cboCategory)
                    cmd.Parameters.Add("@Group", SqlDbType.Int).Value = SelectedId(cboGroup)
                    cmd.Parameters.Add("@Brand", SqlDbType.Int).Value = SelectedId(cboBrand)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            gridStock.DataSource = dt
            FormatGrid()
            lblCount.Text = dt.Rows.Count.ToString("N0") & " รายการ"
            lblTotal.Text = "รวมคงเหลือ: " & dt.AsEnumerable().Sum(Function(r) If(r.IsNull("StockNumber"), 0D, Convert.ToDecimal(r("StockNumber")))).ToString("N2")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ค้นหาสต๊อกไม่สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub FormatGrid()
        SetHeader("PN", "PN")
        SetHeader("ProductCode", "รหัสสินค้า")
        SetHeader("Barcode", "Barcode")
        SetHeader("ProductName", "ชื่อสินค้า")
        SetHeader("CategoryName", "ประเภท")
        SetHeader("GroupName", "กลุ่มสินค้า")
        SetHeader("BrandName", "ยี่ห้อ")
        SetHeader("StockNumber", "คงเหลือ")
        SetHeader("Unit", "หน่วย")
        SetHeader("StockValue", "มูลค่าทุน")
        If gridStock.Columns.Contains("StockNumber") Then gridStock.Columns("StockNumber").DefaultCellStyle.Format = "N2"
        If gridStock.Columns.Contains("StockValue") Then gridStock.Columns("StockValue").DefaultCellStyle.Format = "N2"
    End Sub

    Private Sub SetHeader(name As String, header As String)
        If gridStock.Columns.Contains(name) Then gridStock.Columns(name).HeaderText = header
    End Sub

    Private Sub GridCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex >= 0 AndAlso gridStock.Columns(e.ColumnIndex).Name = "StockNumber" AndAlso e.Value IsNot Nothing AndAlso Convert.ToDecimal(e.Value) < 0D Then
            e.CellStyle.ForeColor = Color.Red
        End If
    End Sub

    Private Sub CopyClick(sender As Object, e As EventArgs)
        If gridStock.GetCellCount(DataGridViewElementStates.Selected) > 0 Then
            Clipboard.SetDataObject(gridStock.GetClipboardContent())
            MessageBox.Show("คัดลอกข้อมูลแล้ว", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private NotInheritable Class ComboItem
        Public ReadOnly Id As Integer
        Public ReadOnly Caption As String
        Public Sub New(id As Integer, caption As String)
            Me.Id = id : Me.Caption = caption
        End Sub
        Public Overrides Function ToString() As String
            Return Caption
        End Function
    End Class
End Class
