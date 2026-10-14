Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmFrozenShopIssueHistory
    Inherits Form

    Public Property SelectedIssueId As Integer

    Public Sub New()
        InitializeComponent()

        AddHandler Load, AddressOf HistoryLoad
        AddHandler btnSearch.Click, AddressOf SearchClick
        AddHandler btnSelect.Click, AddressOf SelectClick
        AddHandler btnClose.Click, Sub() DialogResult = DialogResult.Cancel
        AddHandler txtSearch.KeyDown, AddressOf SearchKeyDown
        AddHandler grid.CellDoubleClick, AddressOf GridDoubleClick
    End Sub

    Private Sub HistoryLoad(sender As Object, e As EventArgs)
        LoadHistory()
    End Sub

    Private Sub SearchClick(sender As Object, e As EventArgs)
        LoadHistory()
    End Sub

    Private Sub SearchKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadHistory()
        End If
    End Sub

    Private Sub LoadHistory()
        Try
            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT IssueID,DocumentNo,OrderDate,ReceiveDate,Destination,Remark " &
                    "FROM dbo.FrozenShopIssue WHERE 1=1"
                If txtSearch.Text.Trim() <> "" Then
                    sql &= " AND (DocumentNo LIKE @Search OR Destination LIKE @Search OR Remark LIKE @Search)"
                End If
                sql &= " ORDER BY OrderDate DESC,IssueID DESC"

                Using cmd As New SqlCommand(sql, conn)
                    If txtSearch.Text.Trim() <> "" Then
                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 300).Value = "%" & txtSearch.Text.Trim() & "%"
                    End If
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                End Using
            End Using

            grid.DataSource = table
            If grid.Columns.Contains("IssueID") Then grid.Columns("IssueID").Visible = False
            If grid.Columns.Contains("DocumentNo") Then grid.Columns("DocumentNo").HeaderText = "เลขที่เอกสาร"
            If grid.Columns.Contains("OrderDate") Then grid.Columns("OrderDate").HeaderText = "วันที่สั่ง"
            If grid.Columns.Contains("ReceiveDate") Then grid.Columns("ReceiveDate").HeaderText = "วันที่รับสินค้า"
            If grid.Columns.Contains("Destination") Then grid.Columns("Destination").HeaderText = "ปลายทาง"
            If grid.Columns.Contains("Remark") Then grid.Columns("Remark").HeaderText = "หมายเหตุ"
        Catch ex As Exception
            MessageBox.Show("โหลดใบย้อนหลังไม่สำเร็จ: " & ex.Message, "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then SelectClick(sender, EventArgs.Empty)
    End Sub

    Private Sub SelectClick(sender As Object, e As EventArgs)
        If grid.CurrentRow Is Nothing OrElse grid.CurrentRow.Cells("IssueID").Value Is Nothing Then
            MessageBox.Show("กรุณาเลือกใบเบิกที่ต้องการเรียก", "Frozen Shop", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        SelectedIssueId = Convert.ToInt32(grid.CurrentRow.Cells("IssueID").Value)
        DialogResult = DialogResult.OK
    End Sub
End Class
