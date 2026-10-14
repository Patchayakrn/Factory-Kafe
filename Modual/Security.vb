Module Security
    Public Function CalPassword(OfficerID As Long, UserNameString As String, PasswordString As String) As Decimal
        Dim num1 As Decimal = 0D
        Dim num2 As Long = OfficerID

        UserNameString = If(UserNameString, "").Trim()
        PasswordString = If(PasswordString, "").Trim()

        For index As Integer = 0 To PasswordString.Length - 1
            Dim num3 As Long = OfficerID + CLng(AscW(PasswordString(index)))

            Select Case index Mod 4
                Case 0 : num1 += CDec(num3)
                Case 1 : num1 -= CDec(num3)
                Case 2 : num1 *= CDec(num3)
                Case 3
                    If num3 = 0L Then num3 = 1L
                    num1 /= CDec(num3)
            End Select

            OfficerID += 1
        Next

        OfficerID = num2

        For index As Integer = 0 To UserNameString.Length - 1
            Dim num4 As Long = OfficerID + CLng(AscW(UserNameString(index)))

            Select Case index Mod 4
                Case 0 : num1 += CDec(num4)
                Case 1 : num1 *= CDec(num4)
                Case 2 : num1 -= CDec(num4)
                Case 3 : num1 += CDec(num4)
            End Select

            OfficerID += 2
        Next

        num1 = Convert.ToDecimal(num1.ToString("#0.0000"))
        Return num1
    End Function
End Module
