Imports System.Linq
Imports System.Drawing
Imports System.Windows.Forms

Partial Public Class FrmFreeCupSelect
    Public Property MaxQuantity As Integer
    Public ReadOnly Property SelectedItems As New List(Of FreeCupSelectionItem)()

    Private ReadOnly items As List(Of FreeCupSelectionItem)

    Public Sub New(selectionItems As IEnumerable(Of FreeCupSelectionItem), maxQuantity As Integer)
        InitializeComponent()
        items = If(selectionItems, Enumerable.Empty(Of FreeCupSelectionItem)()).ToList()
        MaxQuantity = Math.Max(1, maxQuantity)

        For Each item As FreeCupSelectionItem In items
            Dim rowIndex As Integer = itemGrid.Rows.Add(False, item.MenuName, item.Price, If(item.IsEligible, "เลือกได้", "มัทฉะ - ใช้ไม่ได้"))
            itemGrid.Rows(rowIndex).Tag = item
            If Not item.IsEligible Then
                itemGrid.Rows(rowIndex).DefaultCellStyle.ForeColor = Color.Gray
                itemGrid.Rows(rowIndex).Cells("Choose").ReadOnly = True
            End If
        Next

        UpdateInstruction()
    End Sub

    Private Sub ItemGrid_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles itemGrid.CurrentCellDirtyStateChanged
        If itemGrid.IsCurrentCellDirty Then itemGrid.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub ItemGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles itemGrid.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 OrElse itemGrid.Columns(e.ColumnIndex).Name <> "Choose" Then Return
        BeginInvoke(New MethodInvoker(AddressOf UpdateInstruction))
    End Sub

    Private Sub UpdateInstruction()
        Dim selectedCount As Integer = itemGrid.Rows.Cast(Of DataGridViewRow)().Count(Function(row) IsRowChecked(row))
        lblInstruction.Text = "เลือกแล้ว " & selectedCount.ToString("N0") & " รายการ"
    End Sub

    Private Function IsRowChecked(row As DataGridViewRow) As Boolean
        If row Is Nothing Then Return False
        Dim cell As DataGridViewCheckBoxCell = TryCast(row.Cells("Choose"), DataGridViewCheckBoxCell)
        If cell Is Nothing Then Return False
        Try
            'ใช้ค่า Value หลังจาก Commit เป็นหลัก เพื่อไม่ให้ค่า EditedFormattedValue
            'ที่ยังไม่ทันเปลี่ยน ทำให้ระบบอ่านการเลือกเป็น 0 รายการ
            If cell.Value IsNot Nothing AndAlso Not IsDBNull(cell.Value) Then
                Return Convert.ToBoolean(cell.Value)
            End If
            Return Convert.ToBoolean(cell.EditedFormattedValue)
        Catch
            Return False
        End Try
    End Function

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If itemGrid.IsCurrentCellDirty Then itemGrid.CommitEdit(DataGridViewDataErrorContexts.Commit)
        itemGrid.EndEdit()

        SelectedItems.Clear()
        For Each row As DataGridViewRow In itemGrid.Rows
            If IsRowChecked(row) Then
                Dim item As FreeCupSelectionItem = TryCast(row.Tag, FreeCupSelectionItem)
                If item IsNot Nothing AndAlso item.IsEligible Then SelectedItems.Add(item)
            End If
        Next

        If SelectedItems.Count = 0 Then
            MessageBox.Show("กรุณาเลือกรายการที่ต้องการใช้สิทธิ์แก้วฟรี", "แก้วฟรี", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class
