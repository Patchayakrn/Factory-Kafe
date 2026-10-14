Imports System.Drawing
Imports System.Drawing.Printing

Public Class frmReceiptPreview
    Public Property Receipt As ReceiptData

    Public Class ReceiptItem
        Public Property ProductCode As String
        Public Property ProductName As String
        Public Property Barcode As String
        Public Property Qty As Decimal
        Public Property Price As Decimal
        Public Property Amount As Decimal
        Public Property Sunit As String
    End Class

    Public Class ReceiptData
        Public Property CompanyName As String
        Public Property BranchName As String
        Public Property Address As String
        Public Property Tel As String
        Public Property Fax As String
        Public Property TaxId As String
        Public Property PosId As String
        Public Property InvoiceNo As String
        Public Property SaleDate As DateTime
        Public Property CashierName As String
        Public Property PayBy As String

        Public Property CustomerCode As String
        Public Property CustomerName As String
        Public Property CustomerTel As String

        Public Property SubTotal As Decimal
        Public Property GrossTotal As Decimal
        Public Property DiscountPer As Decimal
        Public Property DiscountBath As Decimal
        Public Property VatAmount As Decimal
        Public Property GrandTotal As Decimal
        Public Property ReceiveAmount As Decimal
        Public Property ChangeAmount As Decimal
        Public Property Remark As String

        Public Property Items As New List(Of ReceiptItem)
    End Class

    Private Sub frmReceiptPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        With PrintDocument1.DefaultPageSettings
            .Margins = New Margins(0, 0, 0, 0)
        End With

        PrintDocument1.OriginAtMargins = False
    End Sub

    Private Sub frmReceiptPreview_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub
    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage


        Dim f As New Font("Arial", 20, FontStyle.Bold)
        e.Graphics.DrawString("TEST", f, Brushes.Black, 10, 10)
        e.HasMorePages = False

    End Sub
    Private Function DrawWrappedCenter(g As Graphics, txt As String, f As Font, centerX As Integer, y As Integer, maxWidth As Integer) As Integer
        Dim rect As New RectangleF(centerX - (maxWidth / 2.0F), y, maxWidth, 200)
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Near

        g.DrawString(txt, f, Brushes.Black, rect, sf)

        Dim size As SizeF = g.MeasureString(txt, f, maxWidth)
        Return y + CInt(Math.Ceiling(size.Height))
    End Function
End Class
