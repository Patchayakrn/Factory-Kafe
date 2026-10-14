Imports System.Data
Imports System.Globalization
Imports System.Text.RegularExpressions

''' <summary>
''' Normalizes the customer free-cup ledger. Older builds stored the opening
''' accumulated-cup value in FreeCupBalance, so the ledger must be replayed
''' from the opening row before it is displayed or used for a new sale.
''' </summary>
Public Module CustomerFreeCupBalance
    Public Const TargetCups As Decimal = 10D

    Public Sub NormalizeHistory(table As DataTable)
        If table Is Nothing Then Return

        Dim cupBalance As Decimal = 0D
        Dim freeCupBalance As Integer = 0

        For Each row As DataRow In table.Rows
            Dim detail As String = ReadString(row, "Detail")
            Dim isOpeningRow As Boolean = IsDBNullValue(row, "SellNumber") OrElse
                                          detail.IndexOf("ปรับยอด", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                          detail.IndexOf("กระดาษ", StringComparison.OrdinalIgnoreCase) >= 0

            If isOpeningRow Then
                Dim openingCups As Decimal = GetOpeningCupTotal(row, detail)
                Dim earnedFreeCups As Integer = CInt(Math.Floor(openingCups / TargetCups))
                cupBalance = openingCups - (earnedFreeCups * TargetCups)
                freeCupBalance = earnedFreeCups

                SetValue(row, "CupIn", openingCups)
                SetValue(row, "FreeCupIn", earnedFreeCups)
                SetValue(row, "FreeCupOut", 0)
                SetValue(row, "CupBalance", cupBalance)
                SetValue(row, "FreeCupBalance", freeCupBalance)
                SetValue(row, "Detail", "ยอดยกมา: ซื้อสะสม " & openingCups.ToString("N2") &
                         " แก้ว | ได้สิทธิ์ฟรี " & earnedFreeCups.ToString("N0") & " ใบ")
                Continue For
            End If

            Dim boughtCups As Decimal = Math.Max(0D, ReadDecimal(row, "CupIn"))
            Dim usedFreeCups As Integer = Math.Max(0, ReadInteger(row, "FreeCupOut"))
            Dim totalCups As Decimal = cupBalance + boughtCups
            Dim earnedOnThisRow As Integer = CInt(Math.Floor(totalCups / TargetCups))

            cupBalance = totalCups - (earnedOnThisRow * TargetCups)
            freeCupBalance = Math.Max(0, freeCupBalance + earnedOnThisRow - usedFreeCups)

            SetValue(row, "FreeCupIn", earnedOnThisRow)
            SetValue(row, "CupBalance", cupBalance)
            SetValue(row, "FreeCupBalance", freeCupBalance)
        Next
    End Sub

    Public Sub ReadLatestBalance(table As DataTable, ByRef cupBalance As Decimal, ByRef freeCupBalance As Integer)
        cupBalance = 0D
        freeCupBalance = 0
        NormalizeHistory(table)
        If table Is Nothing OrElse table.Rows.Count = 0 Then Return

        Dim lastRow As DataRow = table.Rows(table.Rows.Count - 1)
        cupBalance = ReadDecimal(lastRow, "CupBalance")
        freeCupBalance = Math.Max(0, ReadInteger(lastRow, "FreeCupBalance"))
    End Sub

    Private Function GetOpeningCupTotal(row As DataRow, detail As String) As Decimal
        Dim cupIn As Decimal = Math.Max(0D, ReadDecimal(row, "CupIn"))
        If cupIn > 0D Then Return cupIn

        'The current editor writes the total before "/10 แก้ว" into the detail.
        Dim match As Match = Regex.Match(detail, "([0-9]+(?:[.,][0-9]+)?)\s*/\s*10\s*แก้ว")
        If match.Success Then
            Dim parsed As Decimal
            If Decimal.TryParse(match.Groups(1).Value.Replace(",", ""), NumberStyles.Number,
                                CultureInfo.InvariantCulture, parsed) Then
                Return Math.Max(0D, parsed)
            End If
        End If

        Dim cupBalance As Decimal = Math.Max(0D, ReadDecimal(row, "CupBalance"))
        Dim rawFreeBalance As Integer = Math.Max(0, ReadInteger(row, "FreeCupBalance"))

        'Legacy opening rows labelled as an adjustment actually held the
        'accumulated-cup input in FreeCupBalance, including values below 10.
        If detail.IndexOf("ปรับยอด", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
           detail.IndexOf("กระดาษ", StringComparison.OrdinalIgnoreCase) >= 0 Then
            If cupBalance <= 0D Then Return rawFreeBalance
        End If

        Return cupBalance + (rawFreeBalance * TargetCups)
    End Function

    Private Function IsDBNullValue(row As DataRow, columnName As String) As Boolean
        Return row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) OrElse row.IsNull(columnName)
    End Function

    Private Function ReadString(row As DataRow, columnName As String) As String
        If IsDBNullValue(row, columnName) Then Return ""
        Return Convert.ToString(row(columnName), CultureInfo.InvariantCulture)
    End Function

    Private Function ReadDecimal(row As DataRow, columnName As String) As Decimal
        If IsDBNullValue(row, columnName) Then Return 0D
        Try
            Return Convert.ToDecimal(row(columnName), CultureInfo.InvariantCulture)
        Catch
            Return 0D
        End Try
    End Function

    Private Function ReadInteger(row As DataRow, columnName As String) As Integer
        If IsDBNullValue(row, columnName) Then Return 0
        Try
            Return Convert.ToInt32(row(columnName), CultureInfo.InvariantCulture)
        Catch
            Return 0
        End Try
    End Function

    Private Sub SetValue(row As DataRow, columnName As String, value As Object)
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) Then Return
        If row.Table.Columns(columnName).ReadOnly Then row.Table.Columns(columnName).ReadOnly = False
        row(columnName) = value
    End Sub
End Module
