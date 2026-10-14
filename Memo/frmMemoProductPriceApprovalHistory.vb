Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmMemoProductPriceApprovalHistory
    Inherits Form

    Public Property SelectedMemoId As Integer

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
        If e.KeyCode <> Keys.Enter Then Return
        e.SuppressKeyPress = True
        LoadHistory()
    End Sub

    Private Sub LoadHistory()
        Try
            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Dim sql As String =
                    "SELECT MemoID,DocumentNo,DocumentDate,SubjectText " &
                    "FROM dbo.ProductPriceApprovalMemo WHERE 1=1"

                If txtSearch.Text.Trim().Length > 0 Then
                    sql &= " AND (DocumentNo LIKE @Search OR SubjectText LIKE @Search)"
                End If
                sql &= " ORDER BY DocumentDate DESC,MemoID DESC"

                Using cmd As New SqlCommand(sql, conn)
                    If txtSearch.Text.Trim().Length > 0 Then
                        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 500).Value = "%" & txtSearch.Text.Trim() & "%"
                    End If
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                End Using
            End Using

            grid.DataSource = table
            If grid.Columns.Contains("MemoID") Then grid.Columns("MemoID").Visible = False
            If grid.Columns.Contains("DocumentNo") Then grid.Columns("DocumentNo").HeaderText = "เลขที่เอกสาร"
            If grid.Columns.Contains("DocumentDate") Then
                grid.Columns("DocumentDate").HeaderText = "วันที่เอกสาร"
                grid.Columns("DocumentDate").DefaultCellStyle.Format = "dd/MM/yyyy"
            End If
            If grid.Columns.Contains("SubjectText") Then grid.Columns("SubjectText").HeaderText = "เรื่อง"
        Catch ex As Exception
            MessageBox.Show("โหลดรายการย้อนหลังไม่สำเร็จ: " & ex.Message, "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then SelectClick(sender, EventArgs.Empty)
    End Sub

    Private Sub SelectClick(sender As Object, e As EventArgs)
        If grid.CurrentRow Is Nothing OrElse
           Not grid.Columns.Contains("MemoID") OrElse
           grid.CurrentRow.Cells("MemoID").Value Is Nothing Then
            MessageBox.Show("กรุณาเลือกเอกสารที่ต้องการเรียกดู", "รายการย้อนหลัง", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        SelectedMemoId = Convert.ToInt32(grid.CurrentRow.Cells("MemoID").Value)
        DialogResult = DialogResult.OK
    End Sub
End Class
