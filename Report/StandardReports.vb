Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public MustInherit Class StandardTableReport
    Implements DevExpress.XtraReports.UI.IPrintableReport

    Private ReadOnly _title As String
    Private _data As DataTable
    Private _dateFrom As Date
    Private _dateTo As Date
    Private _nextRow As Integer

    Protected Sub New(title As String)
        _title = title
    End Sub

    Public Sub SetData(data As DataTable)
        _data = If(data, New DataTable())
    End Sub

    Public Sub SetHeader(dateFrom As Date, dateTo As Date, data As DataTable)
        _dateFrom = dateFrom
        _dateTo = dateTo
        SetData(data)
    End Sub

    Public Sub ShowPreview() Implements DevExpress.XtraReports.UI.IPrintableReport.ShowPreview
        _nextRow = 0
        Dim document As PrintDocument = CreatePrintDocument()

        Using preview As New PrintPreviewDialog()
            preview.Document = document
            preview.WindowState = FormWindowState.Maximized
            preview.ShowDialog()
        End Using
    End Sub

    Public Function CreatePrintDocument() As PrintDocument
        _nextRow = 0
        Dim document As New PrintDocument()
        document.DocumentName = _title
        document.DefaultPageSettings.Landscape = True
        AddHandler document.PrintPage, AddressOf PrintPage
        Return document
    End Function

    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim bounds = e.MarginBounds
        Dim y = bounds.Top
        Using titleFont As New Font("Tahoma", 16.0F, FontStyle.Bold),
              headerFont As New Font("Tahoma", 9.0F, FontStyle.Bold),
              bodyFont As New Font("Tahoma", 8.0F)

            e.Graphics.DrawString(_title, titleFont, Brushes.Black, bounds.Left, y)
            y += CInt(titleFont.GetHeight(e.Graphics)) + 4
            e.Graphics.DrawString(String.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", _dateFrom, _dateTo), bodyFont, Brushes.Black, bounds.Left, y)
            y += CInt(bodyFont.GetHeight(e.Graphics)) + 10

            If _data Is Nothing OrElse _data.Columns.Count = 0 Then
                e.Graphics.DrawString("No data", bodyFont, Brushes.Black, bounds.Left, y)
                e.HasMorePages = False
                Return
            End If

            Dim columnCount = Math.Min(_data.Columns.Count, 10)
            Dim cellWidth = Math.Max(70, bounds.Width \ columnCount)
            Dim rowHeight = 24

            For columnIndex = 0 To columnCount - 1
                Dim rect As New Rectangle(bounds.Left + (columnIndex * cellWidth), y, cellWidth, rowHeight)
                e.Graphics.FillRectangle(Brushes.Gainsboro, rect)
                e.Graphics.DrawRectangle(Pens.Gray, rect)
                e.Graphics.DrawString(_data.Columns(columnIndex).ColumnName, headerFont, Brushes.Black, rect)
            Next
            y += rowHeight

            While _nextRow < _data.Rows.Count AndAlso y + rowHeight <= bounds.Bottom
                For columnIndex = 0 To columnCount - 1
                    Dim rect As New Rectangle(bounds.Left + (columnIndex * cellWidth), y, cellWidth, rowHeight)
                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    Dim value = Convert.ToString(_data.Rows(_nextRow)(columnIndex))
                    e.Graphics.DrawString(value, bodyFont, Brushes.Black, rect)
                Next
                _nextRow += 1
                y += rowHeight
            End While

            e.HasMorePages = _nextRow < _data.Rows.Count
        End Using
    End Sub
End Class

Public Class rptDailySales
    Inherits StandardTableReport

    Public Sub New()
        MyBase.New("Daily Sales Report")
    End Sub
End Class

Public Class rptSalePromotion
    Inherits StandardTableReport

    Public Sub New()
        MyBase.New("Sales Promotion Report")
    End Sub
End Class
