Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Partial Public Class fCustomerFreeCupHistory
    Inherits Form

    Private ReadOnly customerNumber As Integer
    Private ReadOnly customerName As String
    Private ReadOnly customerCode As String
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(customerNumber As Integer, customerName As String, customerCode As String)
        Me.New()
        Me.customerNumber = customerNumber
        Me.customerName = If(customerName, "").Trim()
        Me.customerCode = If(customerCode, "").Trim()
        titleLabel.Text = "ประวัติแต้มและสิทธิ์แก้วฟรี  " & Me.customerCode & " - " & Me.customerName
    End Sub

    Private Sub History_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LoadHistory()
    End Sub

    Private Sub LoadHistory()
        Try
            DbSchemaChecker.EnsureSupportTables()

            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(
                    "SELECT CTime AS EventTime, SellNumber, " &
                    "ISNULL(CupIn, 0) AS CupIn, ISNULL(FreeCupIn, 0) AS FreeCupIn, " &
                    "ISNULL(FreeCupOut, 0) AS FreeCupOut, ISNULL(CupBalance, 0) AS CupBalance, " &
                    "ISNULL(FreeCupBalance, 0) AS FreeCupBalance, ISNULL(Detail, '') AS Detail " &
                    "FROM CustomerFreeCup WHERE CustomerNumber=@CustomerNumber " &
                    "ORDER BY CTime ASC, ID ASC", conn)
                    cmd.CommandTimeout = 10
                    cmd.Parameters.Add("@CustomerNumber", SqlDbType.Int).Value = customerNumber
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        table.Load(reader)
                    End Using
                End Using
            End Using

            CustomerFreeCupBalance.NormalizeHistory(table)
            grid.DataSource = table
            ConfigureGrid()

            Dim cupBalance As Decimal = 0D
            Dim freeCupBalance As Integer = 0
            CustomerFreeCupBalance.ReadLatestBalance(table, cupBalance, freeCupBalance)

            summaryLabel.Text = "แต้มสะสมรอบปัจจุบัน: " & cupBalance.ToString("N0") & "/10 แก้ว    |    สิทธิ์แก้วฟรีที่ใช้ได้: " & freeCupBalance.ToString("N0") & " ใบ"
            If table.Rows.Count = 0 Then summaryLabel.Text &= "    |    ยังไม่มีประวัติ"
        Catch ex As Exception
            summaryLabel.Text = "ไม่สามารถโหลดประวัติแต้มและสิทธิ์ได้"
            MessageBox.Show(ex.Message, "ประวัติแต้ม/สิทธิ์", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureGrid()
        'กำหนดซ้ำหลังผูก DataSource เพราะบางเครื่องมีค่า DataGridView เดิมที่ซ่อนหัวคอลัมน์
        grid.ColumnHeadersVisible = True
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        grid.ColumnHeadersHeight = 48
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 235, 248)
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30)
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 16.0!, FontStyle.Bold)

        If grid.Columns.Contains("EventTime") Then
            grid.Columns("EventTime").HeaderText = "วันที่/เวลา"
            grid.Columns("EventTime").Width = 145
            grid.Columns("EventTime").DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
            grid.Columns("EventTime").DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("th-TH")
        End If
        If grid.Columns.Contains("SellNumber") Then
            grid.Columns("SellNumber").HeaderText = "เลขที่บิล"
            grid.Columns("SellNumber").Width = 90
        End If
        SetNumberColumn("CupIn", "ซื้อสะสม", 90, "N2")
        SetNumberColumn("FreeCupIn", "ได้รับสิทธิ์", 90, "N0")
        SetNumberColumn("FreeCupOut", "ใช้สิทธิ์", 80, "N0")
        SetNumberColumn("CupBalance", "แต้มคงเหลือ", 100, "N2")
        SetNumberColumn("FreeCupBalance", "สิทธิ์คงเหลือ", 100, "N0")
        If grid.Columns.Contains("Detail") Then
            grid.Columns("Detail").HeaderText = "รายละเอียด"
            grid.Columns("Detail").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            grid.Columns("Detail").MinimumWidth = 300
        End If

        grid.Invalidate()
        grid.Refresh()
    End Sub

    Private Sub SetNumberColumn(columnName As String, headerText As String, width As Integer, format As String)
        If Not grid.Columns.Contains(columnName) Then Return
        grid.Columns(columnName).HeaderText = headerText
        grid.Columns(columnName).Width = width
        grid.Columns(columnName).DefaultCellStyle.Format = format
        grid.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub


End Class
