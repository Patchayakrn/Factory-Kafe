Imports System.Data
Imports System.Data.SqlClient

Public Class frmProductGroup
    Private Const StatusAll As Integer = 0
    Private Const StatusActive As Integer = 1
    Private Const StatusInactive As Integer = 2

    Public Sub New()
        InitializeComponent()
        WireEvents()
    End Sub

    Private Sub WireEvents()
        AddHandler Load, AddressOf Form_Load
        AddHandler btnSearch.Click, Sub() LoadProductGroups()
        AddHandler btnAdd.Click, AddressOf AddGroup
        AddHandler btnEdit.Click, AddressOf EditGroup
        AddHandler btnCancelGroup.Click, AddressOf CancelGroups
        AddHandler btnClose.Click, Sub() Close()
        AddHandler txtSearch.KeyPress, AddressOf Search_KeyPress
        AddHandler cboCategory.SelectedIndexChanged, AddressOf FilterChanged
        AddHandler cboStatus.SelectedIndexChanged, AddressOf FilterChanged
        AddHandler dgvProductGroup.CellDoubleClick, AddressOf Grid_DoubleClick
        AddHandler dgvProductGroup.CellFormatting, AddressOf Grid_CellFormatting
    End Sub

    Private Class LookupItem
        Public ReadOnly Property ID As Integer
        Public ReadOnly Property Name As String

        Public Sub New(id As Integer, name As String)
            Me.ID = id
            Me.Name = name
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    Private Sub BuildInterface()
        Text = "กลุ่มสินค้า"
        ClientSize = New Size(1400, 760)
        WindowState = FormWindowState.Maximized
        MinimumSize = New Size(1380, 650)
        BackColor = Color.White

        Dim header As New Panel() With {.Dock = DockStyle.Top, .Height = 78, .BackColor = Color.FromArgb(37, 99, 235)}
        header.Controls.Add(New Label() With {
            .AutoSize = True, .Location = New Point(22, 5), .Text = "กลุ่มสินค้า",
            .Font = New Font("Cordia New", 22.0!, FontStyle.Bold), .ForeColor = Color.White
        })
        header.Controls.Add(New Label() With {
            .AutoSize = True, .Location = New Point(26, 48), .Text = "ค้นหา เพิ่ม แก้ไข และยกเลิกกลุ่มสินค้า",
            .Font = New Font("Cordia New", 12.0!), .ForeColor = Color.FromArgb(219, 234, 254)
        })

        Dim filter As New Panel() With {.Dock = DockStyle.Top, .Height = 108, .BackColor = Color.White}
        AddCaption(filter, "ชื่อกลุ่มสินค้า", 20)
        SetupTextBox(txtSearch, New Point(22, 42), New Size(270, 41))
        filter.Controls.Add(txtSearch)
        AddCaption(filter, "ประเภทสินค้า", 306)
        SetupCombo(cboCategory, New Point(308, 42), New Size(220, 42))
        filter.Controls.Add(cboCategory)
        AddCaption(filter, "สถานะ", 542)
        SetupCombo(cboStatus, New Point(544, 42), New Size(205, 42))
        filter.Controls.Add(cboStatus)

        SetupButton(btnSearch, "ค้นหา", Color.FromArgb(59, 130, 246), 765, 105)
        SetupButton(btnAdd, "เพิ่ม", Color.FromArgb(20, 184, 166), 878, 100)
        SetupButton(btnEdit, "แก้ไข", Color.FromArgb(14, 165, 233), 986, 100)
        SetupButton(btnCancelGroup, "ยกเลิกการใช้", Color.FromArgb(239, 68, 68), 1094, 130)
        SetupButton(btnClose, "ปิด", Color.White, 1232, 90, Color.FromArgb(71, 85, 105))
        For Each button As Button In {btnSearch, btnAdd, btnEdit, btnCancelGroup, btnClose}
            button.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            filter.Controls.Add(button)
        Next

        dgvProductGroup.Dock = DockStyle.Fill
        dgvProductGroup.BackgroundColor = Color.White
        dgvProductGroup.BorderStyle = BorderStyle.None
        dgvProductGroup.ReadOnly = True
        dgvProductGroup.AllowUserToAddRows = False
        dgvProductGroup.AllowUserToDeleteRows = False
        dgvProductGroup.AllowUserToResizeRows = False
        dgvProductGroup.RowHeadersVisible = False
        dgvProductGroup.RowTemplate.Height = 36
        dgvProductGroup.ColumnHeadersHeight = 46
        dgvProductGroup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvProductGroup.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvProductGroup.MultiSelect = True
        dgvProductGroup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvProductGroup.EnableHeadersVisualStyles = False
        dgvProductGroup.Font = New Font("Cordia New", 14.0!)

        Dim statusPanel As New Panel() With {.Dock = DockStyle.Bottom, .Height = 42, .BackColor = Color.FromArgb(241, 245, 249)}
        lblCount.AutoSize = True
        lblCount.Location = New Point(20, 7)
        lblCount.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)
        lblCount.ForeColor = Color.FromArgb(51, 65, 85)
        lblCount.Text = "0 รายการ"
        statusPanel.Controls.Add(lblCount)

        Controls.Add(dgvProductGroup)
        Controls.Add(statusPanel)
        Controls.Add(filter)
        Controls.Add(header)

        AddHandler Load, AddressOf Form_Load
        AddHandler btnSearch.Click, Sub() LoadProductGroups()
        AddHandler btnAdd.Click, AddressOf AddGroup
        AddHandler btnEdit.Click, AddressOf EditGroup
        AddHandler btnCancelGroup.Click, AddressOf CancelGroups
        AddHandler btnClose.Click, Sub() Close()
        AddHandler txtSearch.KeyPress, AddressOf Search_KeyPress
        AddHandler cboCategory.SelectedIndexChanged, AddressOf FilterChanged
        AddHandler cboStatus.SelectedIndexChanged, AddressOf FilterChanged
        AddHandler dgvProductGroup.CellDoubleClick, AddressOf Grid_DoubleClick
        AddHandler dgvProductGroup.CellFormatting, AddressOf Grid_CellFormatting
    End Sub

    Private Sub AddCaption(parent As Control, textValue As String, x As Integer)
        parent.Controls.Add(New Label() With {
            .AutoSize = True, .Location = New Point(x, 9), .Text = textValue,
            .Font = New Font("Cordia New", 14.0!, FontStyle.Bold)
        })
    End Sub

    Private Sub SetupTextBox(control As TextBox, location As Point, size As Size)
        control.Location = location
        control.Size = size
        control.Font = New Font("Cordia New", 16.0!)
    End Sub

    Private Sub SetupCombo(control As ComboBox, location As Point, size As Size)
        control.Location = location
        control.Size = size
        control.Font = New Font("Cordia New", 15.0!)
        control.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Private Sub SetupButton(button As Button, textValue As String, backColor As Color,
                            x As Integer, width As Integer, Optional foreColor As Color = Nothing)
        button.Text = textValue
        button.Location = New Point(x, 41)
        button.Size = New Size(width, 44)
        button.BackColor = backColor
        button.ForeColor = If(foreColor = Nothing, Color.White, foreColor)
        button.FlatStyle = FlatStyle.Flat
        button.Font = New Font("Cordia New", 15.0!, FontStyle.Bold)
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs)
        cboStatus.Items.AddRange(New Object() {
            "- ไม่ระบุสถานะ -", "ดูเฉพาะที่ใช้อยู่", "ดูเฉพาะที่ไม่ได้ใช้แล้ว"
        })
        LoadCategoryFilter()
        cboStatus.SelectedIndex = StatusActive
        LoadProductGroups()
        txtSearch.Focus()
    End Sub

    Private Sub LoadCategoryFilter()
        cboCategory.Items.Clear()
        cboCategory.Items.Add(New LookupItem(0, "- ไม่ระบุประเภทสินค้า -"))
        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT ID,RTRIM(Name) AS Name FROM Category WHERE Status IS NULL ORDER BY Name", conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        cboCategory.Items.Add(New LookupItem(Convert.ToInt32(reader("ID")),
                                                             reader("Name").ToString().Trim()))
                    End While
                End Using
            End Using
        End Using
        cboCategory.SelectedIndex = 0
    End Sub

    Private Sub LoadProductGroups(Optional selectID As Integer = 0)
        If cboStatus.SelectedIndex < 0 OrElse cboCategory.SelectedIndex < 0 Then Return
        Cursor = Cursors.WaitCursor
        Try
            Dim sql As String =
                "SELECT PG.ID,RTRIM(PG.Name) AS Name,PG.CategoryID,RTRIM(C.Name) AS CategoryName,PG.Status," &
                "CASE WHEN PG.Status IS NULL THEN N'ใช้งาน' ELSE N'ไม่ใช้แล้ว' END AS StatusText," &
                "PG.CTime AS CreateTime,PG.CONumber,RTRIM(CO.Name) AS CreateOfficer," &
                "PG.UTime AS UpdateTime,PG.UONumber,RTRIM(UO.Name) AS UpdateOfficer " &
                "FROM ProductGroup PG LEFT JOIN Category C ON C.ID=PG.CategoryID " &
                "LEFT JOIN Officer CO ON CO.OfficerNumber=PG.CONumber " &
                "LEFT JOIN Officer UO ON UO.OfficerNumber=PG.UONumber " &
                "WHERE (@Keyword='' OR PG.Name LIKE @KeywordLike) "

            Dim categoryID As Integer = SelectedCategoryID(cboCategory)
            If categoryID > 0 Then sql &= "AND PG.CategoryID=@CategoryID "
            If cboStatus.SelectedIndex = StatusActive Then
                sql &= "AND PG.Status IS NULL "
            ElseIf cboStatus.SelectedIndex = StatusInactive Then
                sql &= "AND PG.Status=1 "
            End If
            sql &= "ORDER BY PG.Name"

            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Dim keyword As String = txtSearch.Text.Trim()
                    cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = keyword
                    cmd.Parameters.Add("@KeywordLike", SqlDbType.NVarChar, 202).Value = "%" & keyword & "%"
                    If categoryID > 0 Then cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                End Using
            End Using
            dgvProductGroup.DataSource = table
            ConfigureGrid()
            lblCount.Text = table.Rows.Count.ToString("N0") & " รายการ"
            If selectID > 0 Then SelectGridRow(selectID)
        Catch ex As Exception
            MessageBox.Show("โหลดกลุ่มสินค้าไม่สำเร็จ: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ConfigureGrid()
        If dgvProductGroup.Columns.Count = 0 Then Return
        dgvProductGroup.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(219, 234, 254)
        dgvProductGroup.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 15.0!, FontStyle.Bold)
        dgvProductGroup.DefaultCellStyle.SelectionBackColor = Color.FromArgb(191, 219, 254)
        dgvProductGroup.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvProductGroup.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)
        SetColumn("ID", "ID", 70, DataGridViewContentAlignment.MiddleRight)
        SetColumn("Name", "ชื่อกลุ่มสินค้า", 260, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("CategoryName", "ประเภทสินค้า", 210, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("StatusText", "สถานะ", 100, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("CreateOfficer", "ผู้สร้าง", 130, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("CreateTime", "วันที่สร้าง", 155, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yyyy HH:mm")
        SetColumn("UpdateOfficer", "ผู้แก้ไข", 130, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("UpdateTime", "วันที่แก้ไข", 155, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yyyy HH:mm")
        For Each columnName As String In {"CategoryID", "Status", "CONumber", "UONumber"}
            If dgvProductGroup.Columns.Contains(columnName) Then dgvProductGroup.Columns(columnName).Visible = False
        Next
    End Sub

    Private Sub SetColumn(name As String, caption As String, weight As Integer,
                          alignment As DataGridViewContentAlignment, Optional format As String = "")
        If Not dgvProductGroup.Columns.Contains(name) Then Return
        With dgvProductGroup.Columns(name)
            .HeaderText = caption
            .FillWeight = weight
            .DefaultCellStyle.Alignment = alignment
            If format <> "" Then .DefaultCellStyle.Format = format
        End With
    End Sub

    Private Function SelectedCategoryID(combo As ComboBox) As Integer
        Dim item As LookupItem = TryCast(combo.SelectedItem, LookupItem)
        Return If(item Is Nothing, 0, item.ID)
    End Function

    Private Sub Search_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            LoadProductGroups()
        End If
    End Sub

    Private Sub FilterChanged(sender As Object, e As EventArgs)
        If IsHandleCreated AndAlso cboStatus.SelectedIndex >= 0 AndAlso cboCategory.SelectedIndex >= 0 Then LoadProductGroups()
    End Sub

    Private Sub AddGroup(sender As Object, e As EventArgs)
        Using editor As New ProductGroupEditorDialog(0, "", 0, False)
            If editor.ShowDialog(Me) = DialogResult.OK Then
                txtSearch.Clear()
                cboCategory.SelectedIndex = 0
                cboStatus.SelectedIndex = StatusActive
                LoadProductGroups(editor.ProductGroupID)
            End If
        End Using
    End Sub

    Private Sub EditGroup(sender As Object, e As EventArgs)
        EditCurrentGroup()
    End Sub

    Private Sub Grid_DoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then EditCurrentGroup()
    End Sub

    Private Sub EditCurrentGroup()
        If dgvProductGroup.CurrentRow Is Nothing Then
            MessageBox.Show("กรุณาเลือกกลุ่มสินค้าที่ต้องการแก้ไข 1 รายการ", "แจ้งเตือน",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim row As DataGridViewRow = dgvProductGroup.CurrentRow
        Dim id As Integer = Convert.ToInt32(row.Cells("ID").Value)
        Dim categoryID As Integer = If(Convert.IsDBNull(row.Cells("CategoryID").Value), 0, Convert.ToInt32(row.Cells("CategoryID").Value))
        Dim inactive As Boolean = Not Convert.IsDBNull(row.Cells("Status").Value)
        Using editor As New ProductGroupEditorDialog(id, Convert.ToString(row.Cells("Name").Value), categoryID, inactive)
            If editor.ShowDialog(Me) = DialogResult.OK Then LoadProductGroups(id)
        End Using
    End Sub

    Private Sub Grid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 OrElse Not dgvProductGroup.Columns.Contains("Status") Then Return
        If Not Convert.IsDBNull(dgvProductGroup.Rows(e.RowIndex).Cells("Status").Value) Then
            dgvProductGroup.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
            dgvProductGroup.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.DarkRed
        End If
    End Sub

    Private Sub CancelGroups(sender As Object, e As EventArgs)
        If dgvProductGroup.SelectedRows.Count = 0 Then
            MessageBox.Show("กรุณาเลือกกลุ่มสินค้าที่ต้องการยกเลิกการใช้", "แจ้งเตือน",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If MessageBox.Show("ต้องการกำหนดกลุ่มสินค้าที่เลือกเป็น 'ไม่ใช้แล้ว' ใช่หรือไม่?", "ยืนยัน",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return
        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using transaction As SqlTransaction = conn.BeginTransaction()
                    For Each row As DataGridViewRow In dgvProductGroup.SelectedRows
                        Using cmd As New SqlCommand(
                            "UPDATE ProductGroup SET Status=1,UONumber=@Officer,UTime=GETDATE() WHERE ID=@ID", conn, transaction)
                            cmd.Parameters.Add("@Officer", SqlDbType.Int).Value = OfficerNumber()
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(row.Cells("ID").Value)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    transaction.Commit()
                End Using
            End Using
            MessageBox.Show("ยกเลิกการใช้กลุ่มสินค้าเรียบร้อย", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadProductGroups()
        Catch ex As Exception
            MessageBox.Show("ยกเลิกกลุ่มสินค้าไม่สำเร็จ: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SelectGridRow(id As Integer)
        For Each row As DataGridViewRow In dgvProductGroup.Rows
            If Convert.ToInt32(row.Cells("ID").Value) = id Then
                row.Selected = True
                dgvProductGroup.CurrentCell = row.Cells("Name")
                Return
            End If
        Next
    End Sub

    Private Function OfficerNumber() As Integer
        Return If(ModSession.LoginOfficerID > 0, Convert.ToInt32(ModSession.LoginOfficerID), 1)
    End Function

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then Close() : Return True
        If keyData = (Keys.Control Or Keys.N) Then btnAdd.PerformClick() : Return True
        If keyData = Keys.Enter AndAlso dgvProductGroup.Focused Then EditCurrentGroup() : Return True
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Class ProductGroupEditorDialog
        Inherits Form

        Private ReadOnly txtName As New TextBox()
        Private ReadOnly cboCategory As New ComboBox()
        Private ReadOnly chkInactive As New CheckBox()
        Private ReadOnly lblID As New Label()
        Private ReadOnly btnSave As New Button()
        Private ReadOnly isEdit As Boolean
        Private ReadOnly initialCategoryID As Integer
        Public Property ProductGroupID As Integer

        Public Sub New(id As Integer, groupName As String, categoryID As Integer, inactive As Boolean)
            isEdit = id > 0
            ProductGroupID = id
            initialCategoryID = categoryID
            Text = If(isEdit, "แก้ไขกลุ่มสินค้า", "เพิ่มกลุ่มสินค้า")
            StartPosition = FormStartPosition.CenterParent
            FormBorderStyle = FormBorderStyle.FixedDialog
            ClientSize = New Size(620, 345)
            MaximizeBox = False
            MinimizeBox = False
            BackColor = Color.White
            Font = New Font("Cordia New", 14.0!)

            Dim header As New Panel() With {.Dock = DockStyle.Top, .Height = 66, .BackColor = Color.FromArgb(37, 99, 235)}
            header.Controls.Add(New Label() With {
                .AutoSize = True, .Location = New Point(18, 10), .Text = Text,
                .Font = New Font("Cordia New", 20.0!, FontStyle.Bold), .ForeColor = Color.White
            })
            AddEditorLabel("ID", 25, 88)
            lblID.AutoSize = True : lblID.Location = New Point(175, 88) : lblID.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)
            AddEditorLabel("ชื่อกลุ่มสินค้า *", 25, 130)
            txtName.Location = New Point(175, 123) : txtName.Size = New Size(415, 40) : txtName.Font = New Font("Cordia New", 15.0!)
            txtName.Text = groupName
            AddEditorLabel("ประเภทสินค้า", 25, 180)
            cboCategory.Location = New Point(175, 175) : cboCategory.Size = New Size(415, 42)
            cboCategory.Font = New Font("Cordia New", 15.0!) : cboCategory.DropDownStyle = ComboBoxStyle.DropDownList
            chkInactive.Location = New Point(175, 229) : chkInactive.AutoSize = True : chkInactive.Text = "ไม่ใช้แล้ว"
            chkInactive.Checked = inactive : chkInactive.Visible = isEdit

            btnSave.Location = New Point(360, 282) : btnSave.Size = New Size(110, 45) : btnSave.Text = "บันทึก"
            btnSave.Font = New Font("Cordia New", 15.0!, FontStyle.Bold) : btnSave.BackColor = Color.FromArgb(20, 184, 166)
            btnSave.ForeColor = Color.White : btnSave.FlatStyle = FlatStyle.Flat
            Dim cancel As New Button() With {
                .Location = New Point(480, 282), .Size = New Size(110, 45), .Text = "ยกเลิก",
                .Font = New Font("Cordia New", 15.0!, FontStyle.Bold), .BackColor = Color.White,
                .FlatStyle = FlatStyle.Flat, .DialogResult = DialogResult.Cancel
            }
            Controls.Add(header) : Controls.Add(lblID) : Controls.Add(txtName) : Controls.Add(cboCategory)
            Controls.Add(chkInactive) : Controls.Add(btnSave) : Controls.Add(cancel)
            AcceptButton = btnSave : CancelButton = cancel
            AddHandler Load, AddressOf EditorLoad
            AddHandler btnSave.Click, AddressOf SaveGroup
        End Sub

        Private Sub AddEditorLabel(textValue As String, x As Integer, y As Integer)
            Controls.Add(New Label() With {.AutoSize = True, .Location = New Point(x, y), .Text = textValue})
        End Sub

        Private Sub EditorLoad(sender As Object, e As EventArgs)
            LoadCategories()
            If isEdit Then
                lblID.Text = ProductGroupID.ToString()
            Else
                ProductGroupID = PreviewNextID()
                lblID.Text = ProductGroupID.ToString()
            End If
            SelectCategory(initialCategoryID)
            txtName.Focus() : txtName.SelectAll()
        End Sub

        Private Sub LoadCategories()
            cboCategory.Items.Clear()
            cboCategory.Items.Add(New LookupItem(0, "- ไม่ระบุประเภทสินค้า -"))
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(
                    "SELECT ID,RTRIM(Name) AS Name FROM Category WHERE Status IS NULL OR ID=@CurrentID ORDER BY Name", conn)
                    cmd.Parameters.Add("@CurrentID", SqlDbType.Int).Value = initialCategoryID
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            cboCategory.Items.Add(New LookupItem(Convert.ToInt32(reader("ID")), reader("Name").ToString().Trim()))
                        End While
                    End Using
                End Using
            End Using
            cboCategory.SelectedIndex = 0
        End Sub

        Private Sub SelectCategory(id As Integer)
            For i As Integer = 0 To cboCategory.Items.Count - 1
                Dim item As LookupItem = TryCast(cboCategory.Items(i), LookupItem)
                If item IsNot Nothing AndAlso item.ID = id Then cboCategory.SelectedIndex = i : Return
            Next
        End Sub

        Private Function SelectedID() As Integer
            Dim item As LookupItem = TryCast(cboCategory.SelectedItem, LookupItem)
            Return If(item Is Nothing, 0, item.ID)
        End Function

        Private Function PreviewNextID() As Integer
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM ProductGroup", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        Private Sub SaveGroup(sender As Object, e As EventArgs)
            Dim nameValue As String = txtName.Text.Trim()
            If nameValue = "" Then MessageBox.Show("กรุณาป้อนชื่อกลุ่มสินค้า", "Warning") : txtName.Focus() : Return
            btnSave.Enabled = False : Cursor = Cursors.WaitCursor
            Try
                Using conn As SqlConnection = modVariable.OpenConnection()
                    Using transaction As SqlTransaction = conn.BeginTransaction()
                        Dim id As Integer = If(isEdit, ProductGroupID, NextID(conn, transaction))
                        If DuplicateName(conn, transaction, nameValue, id) Then Throw New ApplicationException("ชื่อกลุ่มสินค้านี้มีอยู่แล้ว")
                        If isEdit Then
                            Using cmd As New SqlCommand(
                                "UPDATE ProductGroup SET Name=@Name,CategoryID=@CategoryID,Status=@Status,UONumber=@Officer,UTime=GETDATE() WHERE ID=@ID", conn, transaction)
                                AddParameters(cmd, id, nameValue)
                                If cmd.ExecuteNonQuery() <> 1 Then Throw New ApplicationException("ไม่พบกลุ่มสินค้าที่ต้องการแก้ไข")
                            End Using
                        Else
                            Using cmd As New SqlCommand(
                                "INSERT INTO ProductGroup(ID,Name,CategoryID,CONumber,UONumber,CTime,UTime) VALUES(@ID,@Name,@CategoryID,@Officer,@Officer,GETDATE(),GETDATE())", conn, transaction)
                                AddParameters(cmd, id, nameValue)
                                cmd.ExecuteNonQuery()
                            End Using
                        End If
                        transaction.Commit() : ProductGroupID = id
                    End Using
                End Using
                DialogResult = DialogResult.OK : Close()
            Catch ex As Exception
                MessageBox.Show("บันทึกกลุ่มสินค้าไม่สำเร็จ: " & ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor = Cursors.Default : btnSave.Enabled = True
            End Try
        End Sub

        Private Sub AddParameters(cmd As SqlCommand, id As Integer, nameValue As String)
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = nameValue
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = SelectedID()
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(chkInactive.Checked, CType(1, Object), DBNull.Value)
            cmd.Parameters.Add("@Officer", SqlDbType.Int).Value = If(ModSession.LoginOfficerID > 0, Convert.ToInt32(ModSession.LoginOfficerID), 1)
        End Sub

        Private Function NextID(conn As SqlConnection, transaction As SqlTransaction) As Integer
            Using cmd As New SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM ProductGroup WITH (UPDLOCK,HOLDLOCK)", conn, transaction)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Function

        Private Function DuplicateName(conn As SqlConnection, transaction As SqlTransaction,
                                       nameValue As String, currentID As Integer) As Boolean
            Using cmd As New SqlCommand("SELECT COUNT(1) FROM ProductGroup WHERE Name=@Name AND ID<>@ID", conn, transaction)
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = nameValue
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = currentID
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Function
    End Class
End Class
