Imports System.Globalization
Imports System.Drawing
Imports System.Windows.Forms

Public Class frmMemoProductFormula
    Inherits Form

    Private bomTabs As TabControl
    Private gridMoreBom As DataGridView
    Private tabsInitialized As Boolean

    Public Sub New()
        InitializeComponent()

        AddHandler btnNew.Click, AddressOf NewDocumentClick
        AddHandler btnAddRow.Click, AddressOf AddRowClick
        AddHandler btnRemoveRow.Click, AddressOf RemoveRowClick
        AddHandler btnSave.Click, AddressOf SaveClick
        AddHandler btnPrint.Click, AddressOf PrintClick
        AddHandler btnClose.Click, AddressOf CloseClick

        btnPrint.Enabled = False
    End Sub

    Private Sub NewDocumentClick(sender As Object, e As EventArgs)
        txtDocumentNo.Clear()
        txtSalesManager.Clear()
        txtBody.Clear()
        dtpDocumentDate.Value = Date.Today
        GetActiveGrid().Rows.Clear()
        gridEmployees.Rows.Clear()
        gridMoreBom.Rows.Clear()
        Dim activeTabIndex As Integer = If(bomTabs IsNot Nothing, bomTabs.SelectedIndex, 0)
        AddFormulaRow(gridEmployees, activeTabIndex = 0)
        AddFormulaRow(gridMoreBom, activeTabIndex = 1)
        btnPrint.Enabled = False
    End Sub

    Private Sub AddRowClick(sender As Object, e As EventArgs)
        AddFormulaRow(GetActiveGrid())
    End Sub

    Private Sub AddFormulaRow(targetGrid As DataGridView, Optional beginEdit As Boolean = True)
        Dim lineNo As Integer = targetGrid.Rows.Count + 1
        targetGrid.Rows.Add(lineNo, "", "", "", "", "")
        If beginEdit Then
            targetGrid.CurrentCell = targetGrid.Rows(targetGrid.Rows.Count - 1).Cells(1)
            targetGrid.BeginEdit(True)
        End If
    End Sub

    Private Sub RemoveRowClick(sender As Object, e As EventArgs)
        Dim targetGrid As DataGridView = GetActiveGrid()
        If targetGrid.CurrentRow Is Nothing OrElse targetGrid.CurrentRow.IsNewRow Then Return

        targetGrid.Rows.Remove(targetGrid.CurrentRow)
        For i As Integer = 0 To targetGrid.Rows.Count - 1
            targetGrid.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub

    Private Sub SaveClick(sender As Object, e As EventArgs)
        MessageBox.Show("หน้าฟอร์มพร้อมสำหรับเชื่อมต่อคำสั่งบันทึกข้อมูลแล้วครับ",
                        "บันทึกเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnPrint.Enabled = True
    End Sub

    Private Sub PrintClick(sender As Object, e As EventArgs)
        MessageBox.Show("สามารถเชื่อมต่อรายงาน RDLC ของเอกสารสูตรวัตถุดิบได้ในขั้นตอนถัดไปครับ",
                        "พิมพ์รายงาน", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CloseClick(sender As Object, e As EventArgs)
        Close()
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        InitializeBomTabs()
        dtpDocumentDate.Value = Date.Today
        If gridEmployees.Rows.Count = 0 Then AddFormulaRow(gridEmployees, True)
        If gridMoreBom.Rows.Count = 0 Then AddFormulaRow(gridMoreBom, False)
    End Sub

    Private Function GetActiveGrid() As DataGridView
        If bomTabs IsNot Nothing AndAlso bomTabs.SelectedIndex = 1 Then Return gridMoreBom
        Return gridEmployees
    End Function

    Private Sub InitializeBomTabs()
        If tabsInitialized Then Return
        tabsInitialized = True

        bomTabs = New TabControl With {
            .Location = New Point(20, 220),
            .Size = New Size(1214, 285),
            .Name = "bomTabs",
            .Font = New Font("Cordia New", 15.0!, FontStyle.Regular),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        }

        Dim lessTab As New TabPage("จำนวนใช้น้อยกว่า BOM")
        Dim moreTab As New TabPage("จำนวนใช้มากกว่า BOM")
        lessTab.Padding = New Padding(3)
        moreTab.Padding = New Padding(3)

        gridMoreBom = CreateBomGrid("gridMoreBom")
        pnlMain.Controls.Remove(gridEmployees)
        gridEmployees.Dock = DockStyle.Fill
        gridMoreBom.Dock = DockStyle.Fill
        lessTab.Controls.Add(gridEmployees)
        moreTab.Controls.Add(gridMoreBom)
        bomTabs.TabPages.Add(lessTab)
        bomTabs.TabPages.Add(moreTab)
        pnlMain.Controls.Add(bomTabs)
        bomTabs.BringToFront()
    End Sub

    Private Function CreateBomGrid(gridName As String) As DataGridView
        Dim grid As New DataGridView With {
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.FixedSingle,
            .ColumnHeadersHeight = 42,
            .MultiSelect = False,
            .Name = gridName,
            .RowHeadersVisible = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        }

        grid.RowTemplate.Height = 36
        grid.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle With {
            .Alignment = DataGridViewContentAlignment.MiddleCenter,
            .BackColor = Color.FromArgb(230, 230, 230),
            .Font = New Font("Cordia New", 14.0!),
            .WrapMode = DataGridViewTriState.True
        }

        grid.Columns.Add(CreateGridColumn("line", "ลำดับ", 75, True))
        grid.Columns.Add(CreateGridColumn("case", "กรณี", 110, False))
        grid.Columns.Add(CreateGridColumn("code", "รหัสวัตถุดิบ", 170, False))
        grid.Columns.Add(CreateGridColumn("name", "รายการวัตถุดิบ", 420, False))
        grid.Columns.Add(CreateGridColumn("qty", "จำนวน", 150, False))
        grid.Columns.Add(CreateGridColumn("unit", "หน่วย", 120, False))
        Return grid
    End Function

    Private Function CreateGridColumn(columnName As String, headerText As String, columnWidth As Integer, readOnlyColumn As Boolean) As DataGridViewTextBoxColumn
        Return New DataGridViewTextBoxColumn With {
            .Name = columnName,
            .HeaderText = headerText,
            .Width = columnWidth,
            .MinimumWidth = 6,
            .ReadOnly = readOnlyColumn
        }
    End Function
End Class
