Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

' Temporary source-compatible adapters backed entirely by Windows Forms.
' They keep existing designer files readable while removing the DevExpress binaries.
Namespace Global.DevExpress.Utils
    Public Enum HorzAlignment
        [Default]
        Near
        Center
        Far
    End Enum

    Public Enum DefaultBoolean
        [Default]
        [False]
        [True]
    End Enum

    Public Enum FormatType
        None
        Numeric
        Custom
        DateTime
    End Enum

    Public Class AppearanceOptions
        Public Property UseBackColor As Boolean
        Public Property UseBorderColor As Boolean
        Public Property UseFont As Boolean
        Public Property UseForeColor As Boolean
        Public Property UseTextOptions As Boolean
    End Class

    Public Class TextOptions
        Public Property HAlignment As HorzAlignment
        Public Property VAlignment As HorzAlignment
        Public Property WordWrap As DefaultBoolean
    End Class

    Public Class AppearanceObject
        Private _empty As AppearanceObject
        Private _row As AppearanceObject
        Private _headerPanel As AppearanceObject
        Private _footerPanel As AppearanceObject
        Private _evenRow As AppearanceObject

        Public Property BackColor As Color
        Public Property BorderColor As Color
        Public Property Font As Font
        Public Property ForeColor As Color
        Public ReadOnly Property Options As New AppearanceOptions()
        Public ReadOnly Property TextOptions As New TextOptions()

        Public ReadOnly Property Empty As AppearanceObject
            Get
                If _empty Is Nothing Then _empty = New AppearanceObject()
                Return _empty
            End Get
        End Property

        Public ReadOnly Property Row As AppearanceObject
            Get
                If _row Is Nothing Then _row = New AppearanceObject()
                Return _row
            End Get
        End Property

        Public ReadOnly Property HeaderPanel As AppearanceObject
            Get
                If _headerPanel Is Nothing Then _headerPanel = New AppearanceObject()
                Return _headerPanel
            End Get
        End Property

        Public ReadOnly Property FooterPanel As AppearanceObject
            Get
                If _footerPanel Is Nothing Then _footerPanel = New AppearanceObject()
                Return _footerPanel
            End Get
        End Property

        Public ReadOnly Property EvenRow As AppearanceObject
            Get
                If _evenRow Is Nothing Then _evenRow = New AppearanceObject()
                Return _evenRow
            End Get
        End Property
    End Class
End Namespace

Namespace Global.DevExpress.XtraEditors.Controls
    Public Enum BorderStyles
        [Default]
        NoBorder
        Simple
    End Enum

    Public Enum ButtonPredefines
        Combo
    End Enum

    Public Enum StyleIndeterminate
        Unchecked
        Checked
    End Enum

    Public Enum TextEditStyles
        Standard
        DisableTextEditor
    End Enum

    Public Class EditorButton
        Public Sub New()
        End Sub

        Public Sub New(kind As ButtonPredefines)
        End Sub
    End Class

    Public Class EditorButtonCollection
        Inherits List(Of EditorButton)

        Public Overloads Sub AddRange(items As EditorButton())
            MyBase.AddRange(items)
        End Sub
    End Class
End Namespace

Namespace Global.DevExpress.XtraEditors
    Friend Module AdapterStyles
        Public Sub Apply(control As Control, appearance As DevExpress.Utils.AppearanceObject)
            If appearance Is Nothing Then Return
            If appearance.Options.UseBackColor Then
                control.BackColor = If(appearance.BackColor = Color.Transparent, SystemColors.Window, appearance.BackColor)
            End If
            If appearance.Options.UseForeColor Then control.ForeColor = appearance.ForeColor
            If appearance.Options.UseFont AndAlso appearance.Font IsNot Nothing Then control.Font = appearance.Font
        End Sub
    End Module

    Public Enum LabelAutoSizeMode
        [Default]
        None
        Vertical
        Horizontal
    End Enum

    Public Enum ImageAlignToText
        LeftCenter
        RightCenter
        TopCenter
        BottomCenter
    End Enum

    Public Class ImageOptions
        Public Property Image As Image
        Public Property ImageToTextAlignment As ImageAlignToText
    End Class

    Public Class XtraForm
        Inherits Form

        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()

        Protected Overrides Sub OnLoad(e As EventArgs)
            AdapterStyles.Apply(Me, Appearance)
            MyBase.OnLoad(e)
        End Sub
    End Class

    Public Class PanelControl
        Inherits Panel
        Implements ISupportInitialize

        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()
        Public Property BorderStyleEx As DevExpress.XtraEditors.Controls.BorderStyles

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            AdapterStyles.Apply(Me, Appearance)
        End Sub
    End Class

    Public Class SimpleButton
        Inherits Button
        Implements ISupportInitialize

        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()
        Public ReadOnly Property ImageOptions As New ImageOptions()
        Public Property AllowFocus As Boolean = True

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            AdapterStyles.Apply(Me, Appearance)
            If ImageOptions.Image IsNot Nothing Then Image = ImageOptions.Image
            FlatStyle = FlatStyle.Flat
        End Sub
    End Class

    Public Class EditorProperties
        Implements ISupportInitialize

        Public Sub New(owner As Control)
            _owner = owner
        End Sub

        Private ReadOnly _owner As Control
        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()
        Public ReadOnly Property Buttons As New DevExpress.XtraEditors.Controls.EditorButtonCollection()
        Public Property BorderStyle As DevExpress.XtraEditors.Controls.BorderStyles
        Public Property TextEditStyle As DevExpress.XtraEditors.Controls.TextEditStyles
        Public Property PasswordChar As Char
        Public Property UseSystemPasswordChar As Boolean
        Public Property AutoHeight As Boolean

        Public Sub Apply()
            AdapterStyles.Apply(_owner, Appearance)
            Dim textBox = TryCast(_owner, TextBox)
            If textBox IsNot Nothing Then
                textBox.PasswordChar = PasswordChar
                textBox.UseSystemPasswordChar = UseSystemPasswordChar
                textBox.TextAlign = ToHorizontalAlignment(Appearance.TextOptions.HAlignment)
            End If
        End Sub

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            Apply()
        End Sub

        Private Shared Function ToHorizontalAlignment(value As DevExpress.Utils.HorzAlignment) As HorizontalAlignment
            Select Case value
                Case DevExpress.Utils.HorzAlignment.Center
                    Return HorizontalAlignment.Center
                Case DevExpress.Utils.HorzAlignment.Far
                    Return HorizontalAlignment.Right
                Case Else
                    Return HorizontalAlignment.Left
            End Select
        End Function
    End Class

    Public Class TextEdit
        Inherits TextBox
        Implements ISupportInitialize

        Public Sub New()
            Properties = New EditorProperties(Me)
        End Sub

        Public ReadOnly Property Properties As EditorProperties
        Public Property EnterMoveNextControl As Boolean
        Public Event EditValueChanged As EventHandler
        Public Property EditValue As Object
            Get
                Return Text
            End Get
            Set(value As Object)
                Text = Convert.ToString(value)
            End Set
        End Property

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            Properties.Apply()
        End Sub

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            RaiseEvent EditValueChanged(Me, e)
        End Sub
    End Class

    Public Class ComboBoxProperties
        Inherits EditorProperties

        Public Sub New(owner As ComboBox)
            MyBase.New(owner)
            Items = owner.Items
        End Sub

        Public ReadOnly Property Items As ComboBox.ObjectCollection
    End Class

    Public Class ComboBoxEdit
        Inherits ComboBox
        Implements ISupportInitialize

        Public Sub New()
            Properties = New ComboBoxProperties(Me)
            DropDownStyle = ComboBoxStyle.DropDownList
        End Sub

        Public ReadOnly Property Properties As ComboBoxProperties
        Public Event EditValueChanged As EventHandler
        Public Property EditValue As Object
            Get
                Return SelectedItem
            End Get
            Set(value As Object)
                SelectedItem = value
                If SelectedIndex < 0 Then Text = Convert.ToString(value)
            End Set
        End Property

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            Properties.Apply()
        End Sub

        Protected Overrides Sub OnSelectedIndexChanged(e As EventArgs)
            MyBase.OnSelectedIndexChanged(e)
            RaiseEvent EditValueChanged(Me, e)
        End Sub
    End Class

    Public Class LabelControl
        Inherits Label
        Implements ISupportInitialize

        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()
        Public ReadOnly Property Properties As New EditorProperties(Me)
        Public Property AutoSizeMode As LabelAutoSizeMode
        Public Event EditValueChanged As EventHandler

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            AdapterStyles.Apply(Me, Appearance)
            Properties.Apply()
            TextAlign = ToContentAlignment(Appearance.TextOptions.HAlignment)
        End Sub

        Protected Overrides Sub OnTextChanged(e As EventArgs)
            MyBase.OnTextChanged(e)
            RaiseEvent EditValueChanged(Me, e)
        End Sub

        Private Shared Function ToContentAlignment(value As DevExpress.Utils.HorzAlignment) As ContentAlignment
            Select Case value
                Case DevExpress.Utils.HorzAlignment.Center
                    Return ContentAlignment.MiddleCenter
                Case DevExpress.Utils.HorzAlignment.Far
                    Return ContentAlignment.MiddleRight
                Case Else
                    Return ContentAlignment.MiddleLeft
            End Select
        End Function
    End Class
End Namespace

Namespace Global.DevExpress.XtraEditors.Repository
    Public Class RepositoryItem
        Implements ISupportInitialize

        Public Property Name As String
        Public Property AutoHeight As Boolean

        Public Overridable Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Overridable Sub EndInit() Implements ISupportInitialize.EndInit
        End Sub
    End Class

    Public Class RepositoryItemCollection
        Inherits List(Of RepositoryItem)

        Public Overloads Sub AddRange(items As RepositoryItem())
            MyBase.AddRange(items)
        End Sub
    End Class

    Public Class CheckBoxOptions
        Public Property Style As DevExpress.XtraEditors.Controls.StyleIndeterminate
    End Class

    Public Class RepositoryItemCheckEdit
        Inherits RepositoryItem

        Public Property ValueChecked As Object
        Public Property ValueUnchecked As Object
        Public Property NullStyle As DevExpress.XtraEditors.Controls.StyleIndeterminate
        Public ReadOnly Property CheckBoxOptions As New CheckBoxOptions()
    End Class
End Namespace

Namespace Global.DevExpress.XtraGrid
    Public Class EmbeddedNavigatorOptions
        Public Property Margin As Padding
    End Class

    Public Class ViewCollection
        Inherits List(Of DevExpress.XtraGrid.Views.Base.BaseView)

        Public Overloads Sub AddRange(items As DevExpress.XtraGrid.Views.Base.BaseView())
            For Each viewItem In items
                MyBase.Add(viewItem)
            Next
        End Sub
    End Class

    Public Class GridControl
        Inherits DataGridView
        Implements ISupportInitialize

        Private _mainView As DevExpress.XtraGrid.Views.Base.BaseView

        Public Sub New()
            AutoGenerateColumns = False
            AllowUserToAddRows = False
            RowHeadersVisible = False
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
            MultiSelect = False
            BackgroundColor = SystemColors.Window
            EmbeddedNavigator = New EmbeddedNavigatorOptions()
            RepositoryItems = New DevExpress.XtraEditors.Repository.RepositoryItemCollection()
            ViewCollection = New ViewCollection()
        End Sub

        Public Property MainView As DevExpress.XtraGrid.Views.Base.BaseView
            Get
                Return _mainView
            End Get
            Set(value As DevExpress.XtraGrid.Views.Base.BaseView)
                _mainView = value
                If value IsNot Nothing Then value.Attach(Me)
            End Set
        End Property

        Public ReadOnly Property EmbeddedNavigator As EmbeddedNavigatorOptions
        Public ReadOnly Property RepositoryItems As DevExpress.XtraEditors.Repository.RepositoryItemCollection
        Public ReadOnly Property ViewCollection As ViewCollection

        Public Sub ForceInitialize()
            If _mainView IsNot Nothing Then _mainView.Attach(Me)
        End Sub

        Public Sub ShowPrintPreview()
            Dim document As New Printing.PrintDocument()
            AddHandler document.PrintPage,
                Sub(sender, e)
                    Dim y = e.MarginBounds.Top
                    Dim visibleColumns = Columns.Cast(Of DataGridViewColumn)().Where(Function(column) column.Visible).Take(8).ToArray()
                    If visibleColumns.Length = 0 Then Return
                    Dim cellWidth = Math.Max(70, e.MarginBounds.Width \ visibleColumns.Length)
                    Using font As New Font("Tahoma", 8.0F)
                        For columnIndex = 0 To visibleColumns.Length - 1
                            e.Graphics.DrawString(visibleColumns(columnIndex).HeaderText, font, Brushes.Black, e.MarginBounds.Left + columnIndex * cellWidth, y)
                        Next
                        y += 24
                        For Each row As DataGridViewRow In Rows
                            If row.IsNewRow OrElse y + 22 > e.MarginBounds.Bottom Then Exit For
                            For columnIndex = 0 To visibleColumns.Length - 1
                                Dim value = Convert.ToString(row.Cells(visibleColumns(columnIndex).Index).Value)
                                e.Graphics.DrawString(value, font, Brushes.Black, e.MarginBounds.Left + columnIndex * cellWidth, y)
                            Next
                            y += 22
                        Next
                    End Using
                End Sub
            Using preview As New PrintPreviewDialog()
                preview.Document = document
                preview.WindowState = FormWindowState.Maximized
                preview.ShowDialog()
            End Using
        End Sub

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
            If _mainView IsNot Nothing Then _mainView.Attach(Me)
        End Sub
    End Class

    Public Class GridSummaryItem
        Public Sub New()
        End Sub
    End Class

    Public Class GridColumnSummaryItem
        Inherits GridSummaryItem

        Public Sub New(summaryType As DevExpress.Data.SummaryItemType)
        End Sub

        Public Sub New(summaryType As DevExpress.Data.SummaryItemType, fieldName As String, displayFormat As String)
        End Sub
    End Class
End Namespace

Namespace Global.DevExpress.XtraGrid.Columns
    Public Class GridColumnOptions
        Public Property AllowEdit As Boolean = True
        Public Property FixedWidth As Boolean
        Public Property [ReadOnly] As Boolean
        Public Property ShowInCustomizationForm As Boolean = True
    End Class

    Public Class GridColumnFilterOptions
        Public Property AllowAutoFilter As Boolean = True
        Public Property AllowFilter As Boolean = True
    End Class

    Public Class DisplayFormatInfo
        Public Property FormatString As String
        Public Property FormatType As DevExpress.Utils.FormatType
    End Class

    Public Class SummaryCollection
        Inherits List(Of DevExpress.XtraGrid.GridSummaryItem)

        Public Overloads Sub AddRange(items As DevExpress.XtraGrid.GridSummaryItem())
            MyBase.AddRange(items)
        End Sub

        Public Overloads Function Add(summaryType As DevExpress.Data.SummaryItemType, fieldName As String, displayFormat As String) As DevExpress.XtraGrid.GridSummaryItem
            Dim item = New DevExpress.XtraGrid.GridColumnSummaryItem(summaryType, fieldName, displayFormat)
            MyBase.Add(item)
            Return item
        End Function
    End Class

    Public Class GridColumn
        Public Property Caption As String
        Public Property FieldName As String
        Public Property Name As String
        Public Property Visible As Boolean
        Public Property VisibleIndex As Integer = -1
        Public Property Width As Integer = 100
        Public Property MinWidth As Integer
        Public Property MaxWidth As Integer
        Public Property ColumnEdit As DevExpress.XtraEditors.Repository.RepositoryItem
        Public Property ShowUnboundExpressionMenu As Boolean
        Public ReadOnly Property AppearanceCell As New DevExpress.Utils.AppearanceObject()
        Public ReadOnly Property AppearanceHeader As New DevExpress.Utils.AppearanceObject()
        Public ReadOnly Property DisplayFormat As New DisplayFormatInfo()
        Public ReadOnly Property OptionsColumn As New GridColumnOptions()
        Public ReadOnly Property OptionsFilter As New GridColumnFilterOptions()
        Public ReadOnly Property Summary As New SummaryCollection()
    End Class

    Public Class GridColumnCollection
        Inherits List(Of GridColumn)

        Public Overloads Sub AddRange(items As GridColumn())
            MyBase.AddRange(items)
        End Sub

        Default Public Overloads ReadOnly Property Item(fieldName As String) As GridColumn
            Get
                Return Me.FirstOrDefault(Function(column) column.FieldName = fieldName OrElse column.Name = fieldName)
            End Get
        End Property

        Public Function ColumnByFieldName(fieldName As String) As GridColumn
            Return Item(fieldName)
        End Function
    End Class
End Namespace

Namespace Global.DevExpress.XtraGrid.Views.Base
    Public Enum ScrollVisibility
        Never
        Auto
        Always
    End Enum

    Public Class CellValueChangedEventArgs
        Inherits EventArgs

        Public Sub New(rowHandle As Integer, column As DevExpress.XtraGrid.Columns.GridColumn, value As Object)
            Me.RowHandle = rowHandle
            Me.Column = column
            Me.Value = value
        End Sub

        Public ReadOnly Property RowHandle As Integer
        Public ReadOnly Property Column As DevExpress.XtraGrid.Columns.GridColumn
        Public ReadOnly Property Value As Object
    End Class

    Public Class FocusedRowChangedEventArgs
        Inherits EventArgs

        Public Sub New(previousFocusedRowHandle As Integer, focusedRowHandle As Integer)
            Me.PrevFocusedRowHandle = previousFocusedRowHandle
            Me.FocusedRowHandle = focusedRowHandle
        End Sub

        Public ReadOnly Property PrevFocusedRowHandle As Integer
        Public ReadOnly Property FocusedRowHandle As Integer
    End Class

    Public Class BaseView
        Inherits Component
        Implements ISupportInitialize

        Protected Friend Grid As DevExpress.XtraGrid.GridControl
        Public Property Name As String

        Public Overridable Property GridControl As DevExpress.XtraGrid.GridControl
            Get
                Return Grid
            End Get
            Set(value As DevExpress.XtraGrid.GridControl)
                Attach(value)
            End Set
        End Property

        Public Overridable Sub Attach(gridControl As DevExpress.XtraGrid.GridControl)
            Grid = gridControl
        End Sub

        Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        End Sub

        Public Sub EndInit() Implements ISupportInitialize.EndInit
        End Sub
    End Class
End Namespace

Namespace Global.DevExpress.XtraGrid.Views.Grid
    Public Class GridHitInfo
        Public Property InRow As Boolean
        Public Property RowHandle As Integer
    End Class

    Public Class RowCellClickEventArgs
        Inherits EventArgs

        Public Sub New(rowHandle As Integer, column As DevExpress.XtraGrid.Columns.GridColumn)
            Me.RowHandle = rowHandle
            Me.Column = column
        End Sub

        Public ReadOnly Property RowHandle As Integer
        Public ReadOnly Property Column As DevExpress.XtraGrid.Columns.GridColumn
    End Class

    Public Class RowCellStyleEventArgs
        Inherits EventArgs

        Public Sub New(rowHandle As Integer, column As DevExpress.XtraGrid.Columns.GridColumn)
            Me.RowHandle = rowHandle
            Me.Column = column
        End Sub

        Public ReadOnly Property RowHandle As Integer
        Public ReadOnly Property Column As DevExpress.XtraGrid.Columns.GridColumn
        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()
    End Class

    Public Class GridOptionsView
        Public Property ShowGroupPanel As Boolean
        Public Property ShowHorizontalLines As DevExpress.Utils.DefaultBoolean
        Public Property ShowFooter As Boolean
        Public Property ColumnAutoWidth As Boolean
        Public Property EnableAppearanceEvenRow As Boolean
    End Class

    Public Class GridOptionsEditForm
        Public Property PopupEditFormWidth As Integer
    End Class

    Public Class GridOptionsBehavior
        Public Property Editable As Boolean = True
        Public Property [ReadOnly] As Boolean
    End Class

    Public Class GridView
        Inherits DevExpress.XtraGrid.Views.Base.BaseView

        Private _lastFocusedRow As Integer = -1

        Public Sub New()
            Columns = New DevExpress.XtraGrid.Columns.GridColumnCollection()
            OptionsView = New GridOptionsView()
            OptionsEditForm = New GridOptionsEditForm()
            OptionsBehavior = New GridOptionsBehavior()
        End Sub

        Public ReadOnly Property Columns As DevExpress.XtraGrid.Columns.GridColumnCollection
        Public ReadOnly Property OptionsView As GridOptionsView
        Public ReadOnly Property OptionsEditForm As GridOptionsEditForm
        Public ReadOnly Property OptionsBehavior As GridOptionsBehavior
        Public ReadOnly Property Appearance As New DevExpress.Utils.AppearanceObject()
        Public Property ColumnPanelRowHeight As Integer
        Public Property DetailHeight As Integer
        Public Property RowHeight As Integer
        Public Property HorzScrollVisibility As DevExpress.XtraGrid.Views.Base.ScrollVisibility

        Public Event RowCellClick As EventHandler(Of RowCellClickEventArgs)
        Public Event RowCellStyle As EventHandler(Of RowCellStyleEventArgs)
        Public Event FocusedRowChanged As EventHandler(Of DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        Public Event CellValueChanged As EventHandler(Of DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        Public Event DoubleClick As EventHandler
        Public Event KeyDown As KeyEventHandler

        Public Overrides Sub Attach(gridControl As DevExpress.XtraGrid.GridControl)
            If Grid Is gridControl Then Return
            MyBase.Attach(gridControl)
            If gridControl Is Nothing Then Return

            gridControl.AutoGenerateColumns = False
            gridControl.ReadOnly = OptionsBehavior.ReadOnly OrElse Not OptionsBehavior.Editable
            If ColumnPanelRowHeight > 0 Then gridControl.ColumnHeadersHeight = ColumnPanelRowHeight
            If RowHeight > 0 Then gridControl.RowTemplate.Height = RowHeight
            BuildColumns()

            AddHandler gridControl.CellClick, AddressOf HandleCellClick
            AddHandler gridControl.CellFormatting, AddressOf HandleCellFormatting
            AddHandler gridControl.SelectionChanged, AddressOf HandleSelectionChanged
            AddHandler gridControl.CellValueChanged, AddressOf HandleCellValueChanged
            AddHandler gridControl.DoubleClick, Sub(sender, e) RaiseEvent DoubleClick(Me, e)
            AddHandler gridControl.KeyDown, Sub(sender, e) RaiseEvent KeyDown(Me, e)
        End Sub

        Private Sub BuildColumns()
            If Grid Is Nothing Then Return
            Grid.Columns.Clear()
            For Each source In Columns.OrderBy(Function(column) If(column.VisibleIndex < 0, Integer.MaxValue, column.VisibleIndex))
                If Not source.Visible Then Continue For

                Dim target As DataGridViewColumn
                If TypeOf source.ColumnEdit Is DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit Then
                    target = New DataGridViewCheckBoxColumn()
                Else
                    target = New DataGridViewTextBoxColumn()
                End If

                target.Name = If(String.IsNullOrWhiteSpace(source.Name), source.FieldName, source.Name)
                target.DataPropertyName = source.FieldName
                target.HeaderText = source.Caption
                target.Width = Math.Max(20, source.Width)
                target.ReadOnly = source.OptionsColumn.ReadOnly OrElse Not source.OptionsColumn.AllowEdit
                target.DefaultCellStyle.Alignment = ToAlignment(source.AppearanceCell.TextOptions.HAlignment)
                If source.AppearanceCell.Options.UseBackColor Then target.DefaultCellStyle.BackColor = source.AppearanceCell.BackColor
                If source.AppearanceCell.Options.UseForeColor Then target.DefaultCellStyle.ForeColor = source.AppearanceCell.ForeColor
                If source.AppearanceCell.Options.UseFont AndAlso source.AppearanceCell.Font IsNot Nothing Then target.DefaultCellStyle.Font = source.AppearanceCell.Font
                If Not String.IsNullOrWhiteSpace(source.DisplayFormat.FormatString) Then target.DefaultCellStyle.Format = source.DisplayFormat.FormatString
                Grid.Columns.Add(target)
            Next
        End Sub

        Private Shared Function ToAlignment(value As DevExpress.Utils.HorzAlignment) As DataGridViewContentAlignment
            Select Case value
                Case DevExpress.Utils.HorzAlignment.Center
                    Return DataGridViewContentAlignment.MiddleCenter
                Case DevExpress.Utils.HorzAlignment.Far
                    Return DataGridViewContentAlignment.MiddleRight
                Case Else
                    Return DataGridViewContentAlignment.MiddleLeft
            End Select
        End Function

        Private Sub HandleCellClick(sender As Object, e As DataGridViewCellEventArgs)
            If e.RowIndex < 0 Then Return
            Dim column As DevExpress.XtraGrid.Columns.GridColumn = Nothing
            If e.ColumnIndex >= 0 AndAlso e.ColumnIndex < Grid.Columns.Count Then
                Dim fieldName = Grid.Columns(e.ColumnIndex).DataPropertyName
                column = Columns(fieldName)
            End If
            RaiseEvent RowCellClick(Me, New RowCellClickEventArgs(e.RowIndex, column))
        End Sub

        Private Sub HandleCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If e.RowIndex < 0 Then Return
            Dim sourceColumn As DevExpress.XtraGrid.Columns.GridColumn = Nothing
            If e.ColumnIndex >= 0 AndAlso e.ColumnIndex < Grid.Columns.Count Then
                sourceColumn = Columns(Grid.Columns(e.ColumnIndex).DataPropertyName)
            End If
            Dim args = New RowCellStyleEventArgs(e.RowIndex, sourceColumn)
            RaiseEvent RowCellStyle(Me, args)
            If args.Appearance.Options.UseBackColor Then e.CellStyle.BackColor = args.Appearance.BackColor
            If args.Appearance.Options.UseForeColor Then e.CellStyle.ForeColor = args.Appearance.ForeColor
            If args.Appearance.Options.UseFont AndAlso args.Appearance.Font IsNot Nothing Then e.CellStyle.Font = args.Appearance.Font
        End Sub

        Private Sub HandleSelectionChanged(sender As Object, e As EventArgs)
            Dim current = FocusedRowHandle
            If current = _lastFocusedRow Then Return
            Dim previous = _lastFocusedRow
            _lastFocusedRow = current
            RaiseEvent FocusedRowChanged(Me, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(previous, current))
        End Sub

        Private Sub HandleCellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
            Dim fieldName = Grid.Columns(e.ColumnIndex).DataPropertyName
            Dim sourceColumn = Columns(fieldName)
            Dim value = Grid.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            RaiseEvent CellValueChanged(Me, New DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs(e.RowIndex, sourceColumn, value))
        End Sub

        Public Property FocusedRowHandle As Integer
            Get
                If Grid Is Nothing OrElse Grid.CurrentCell Is Nothing Then Return -1
                Return Grid.CurrentCell.RowIndex
            End Get
            Set(value As Integer)
                If Grid Is Nothing OrElse value < 0 OrElse value >= Grid.Rows.Count OrElse Grid.Columns.Count = 0 Then Return
                Grid.CurrentCell = Grid.Rows(value).Cells(0)
            End Set
        End Property

        Public ReadOnly Property RowCount As Integer
            Get
                Return If(Grid Is Nothing, 0, Grid.Rows.Count)
            End Get
        End Property

        Public ReadOnly Property DataRowCount As Integer
            Get
                Return RowCount
            End Get
        End Property

        Public Function GetVisibleRowHandle(visibleIndex As Integer) As Integer
            Return visibleIndex
        End Function

        Public Function IsGroupRow(rowHandle As Integer) As Boolean
            Return False
        End Function

        Public Function IsDataRow(rowHandle As Integer) As Boolean
            Return rowHandle >= 0 AndAlso rowHandle < RowCount
        End Function

        Public Function IsRowSelected(rowHandle As Integer) As Boolean
            Return Grid IsNot Nothing AndAlso rowHandle >= 0 AndAlso rowHandle < Grid.Rows.Count AndAlso Grid.Rows(rowHandle).Selected
        End Function

        Public Function GetFocusedRow() As Object
            Return GetRow(FocusedRowHandle)
        End Function

        Public Function GetFocusedDataRow() As DataRow
            Dim item = GetRow(FocusedRowHandle)
            Dim view = TryCast(item, DataRowView)
            Return If(view Is Nothing, Nothing, view.Row)
        End Function

        Public Function GetRow(rowHandle As Integer) As Object
            If Grid Is Nothing OrElse rowHandle < 0 OrElse rowHandle >= Grid.Rows.Count Then Return Nothing
            Return Grid.Rows(rowHandle).DataBoundItem
        End Function

        Public Function GetRowCellValue(rowHandle As Integer, fieldName As String) As Object
            If Grid Is Nothing OrElse rowHandle < 0 OrElse rowHandle >= Grid.Rows.Count Then Return Nothing
            Dim item = Grid.Rows(rowHandle).DataBoundItem
            Dim dataRowView = TryCast(item, DataRowView)
            If dataRowView IsNot Nothing AndAlso dataRowView.DataView.Table.Columns.Contains(fieldName) Then Return dataRowView(fieldName)
            Dim dataRow = TryCast(item, DataRow)
            If dataRow IsNot Nothing AndAlso dataRow.Table.Columns.Contains(fieldName) Then Return dataRow(fieldName)
            Dim prop = If(item Is Nothing, Nothing, TypeDescriptor.GetProperties(item).Find(fieldName, True))
            If prop IsNot Nothing Then Return prop.GetValue(item)
            For Each column As DataGridViewColumn In Grid.Columns
                If String.Equals(column.DataPropertyName, fieldName, StringComparison.OrdinalIgnoreCase) Then
                    Return Grid.Rows(rowHandle).Cells(column.Index).Value
                End If
            Next
            Return Nothing
        End Function

        Public Function GetFocusedRowCellValue(fieldName As String) As Object
            Return GetRowCellValue(FocusedRowHandle, fieldName)
        End Function

        Public Sub SetRowCellValue(rowHandle As Integer, fieldName As String, value As Object)
            If Grid Is Nothing OrElse rowHandle < 0 OrElse rowHandle >= Grid.Rows.Count Then Return
            Dim item = Grid.Rows(rowHandle).DataBoundItem
            Dim dataRowView = TryCast(item, DataRowView)
            If dataRowView IsNot Nothing AndAlso dataRowView.DataView.Table.Columns.Contains(fieldName) Then
                dataRowView(fieldName) = value
                Return
            End If
            For Each column As DataGridViewColumn In Grid.Columns
                If String.Equals(column.DataPropertyName, fieldName, StringComparison.OrdinalIgnoreCase) Then
                    Grid.Rows(rowHandle).Cells(column.Index).Value = value
                    Return
                End If
            Next
        End Sub

        Public Sub ClearSelection()
            If Grid IsNot Nothing Then Grid.ClearSelection()
        End Sub

        Public Sub SelectRow(rowHandle As Integer)
            If Grid IsNot Nothing AndAlso rowHandle >= 0 AndAlso rowHandle < Grid.Rows.Count Then Grid.Rows(rowHandle).Selected = True
        End Sub

        Public Sub DeleteRow(rowHandle As Integer)
            If Grid Is Nothing OrElse rowHandle < 0 OrElse rowHandle >= Grid.Rows.Count Then Return
            If Not Grid.Rows(rowHandle).IsNewRow Then Grid.Rows.RemoveAt(rowHandle)
        End Sub

        Public Sub PopulateColumns()
            If Grid Is Nothing Then Return
            Columns.Clear()
            Dim table As DataTable = Nothing
            Dim view = TryCast(Grid.DataSource, DataView)
            If view IsNot Nothing Then table = view.Table
            If table Is Nothing Then table = TryCast(Grid.DataSource, DataTable)
            If table Is Nothing Then Return
            Dim index = 0
            For Each dataColumn As DataColumn In table.Columns
                Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn With {
                    .Name = dataColumn.ColumnName,
                    .FieldName = dataColumn.ColumnName,
                    .Caption = dataColumn.ColumnName,
                    .Visible = True,
                    .VisibleIndex = index
                })
                index += 1
            Next
            BuildColumns()
        End Sub

        Public Sub BestFitColumns()
            If Grid IsNot Nothing Then Grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)
        End Sub

        Public Function CalcHitInfo(location As Point) As GridHitInfo
            If Grid Is Nothing Then Return New GridHitInfo()
            Dim hit = Grid.HitTest(location.X, location.Y)
            Return New GridHitInfo With {.InRow = hit.RowIndex >= 0, .RowHandle = hit.RowIndex}
        End Function

        Public Sub RefreshData()
            If Grid IsNot Nothing Then Grid.Refresh()
        End Sub
    End Class
End Namespace

Namespace Global.DevExpress.Data
    Public Enum SummaryItemType
        Count
        Sum
        Min
        Max
        Average
    End Enum
End Namespace

Namespace Global.DevExpress.PivotGrid.QueryMode
End Namespace

Namespace Global.DevExpress.XtraEditors.ColorPick
End Namespace

Namespace Global.DevExpress.XtraGrid.Views.WinExplorer.ViewInfo
End Namespace

Namespace Global.DevExpress.XtraReports.UI
    Public Interface IPrintableReport
        Sub ShowPreview()
    End Interface

    Public Class ReportPrintTool
        Private ReadOnly _report As IPrintableReport

        Public Sub New(report As IPrintableReport)
            _report = report
        End Sub

        Public Sub ShowPreview()
            _report.ShowPreview()
        End Sub
    End Class
End Namespace
