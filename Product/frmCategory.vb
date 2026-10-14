Imports System.Data
Imports System.Data.SqlClient

Public Class frmCategory
    Private Const StatusAll As Integer = 0
    Private Const StatusActive As Integer = 1
    Private Const StatusInactive As Integer = 2

    Private Sub frmCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboStatus.Items.AddRange(New Object() {
            "- ไม่ระบุสถานะ -",
            "ดูเฉพาะที่ใช้อยู่",
            "ดูเฉพาะที่ไม่ได้ใช้แล้ว"
        })
        cboStatus.SelectedIndex = StatusActive
        LoadCategories()
        txtSearch.Focus()
    End Sub

    Private Sub LoadCategories(Optional selectID As Integer = 0)
        Cursor = Cursors.WaitCursor
        Try
            Dim sql As String =
                "SELECT C.ID,RTRIM(C.Name) AS Name,C.Status," &
                "CASE WHEN C.Status IS NULL THEN N'ใช้งาน' ELSE N'ไม่ใช้แล้ว' END AS StatusText," &
                "C.CTime AS CreateTime,C.CONumber,RTRIM(CO.Name) AS CreateOfficer," &
                "C.UTime AS UpdateTime,C.UONumber,RTRIM(UO.Name) AS UpdateOfficer " &
                "FROM Category C " &
                "LEFT JOIN Officer CO ON CO.OfficerNumber=C.CONumber " &
                "LEFT JOIN Officer UO ON UO.OfficerNumber=C.UONumber " &
                "WHERE (@Keyword='' OR C.Name LIKE @KeywordLike) "

            Select Case cboStatus.SelectedIndex
                Case StatusActive
                    sql &= "AND C.Status IS NULL "
                Case StatusInactive
                    sql &= "AND C.Status=1 "
            End Select
            sql &= "ORDER BY C.Name"

            Dim table As New DataTable()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand(sql, conn)
                    Dim keyword As String = txtSearch.Text.Trim()
                    cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = keyword
                    cmd.Parameters.Add("@KeywordLike", SqlDbType.NVarChar, 202).Value = "%" & keyword & "%"
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(table)
                    End Using
                End Using
            End Using

            dgvCategory.DataSource = table
            ConfigureGrid()
            lblCount.Text = table.Rows.Count.ToString("N0") & " รายการ"
            If selectID > 0 Then SelectCategoryRow(selectID)
        Catch ex As Exception
            MessageBox.Show("โหลดประเภทสินค้าไม่สำเร็จ: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ConfigureGrid()
        If dgvCategory.Columns.Count = 0 Then Return

        dgvCategory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(219, 234, 254)
        dgvCategory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59)
        dgvCategory.ColumnHeadersDefaultCellStyle.Font = New Font("Cordia New", 15.0!, FontStyle.Bold)
        dgvCategory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(191, 219, 254)
        dgvCategory.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvCategory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)

        SetColumn("ID", "ID", 80, DataGridViewContentAlignment.MiddleRight)
        SetColumn("Name", "ชื่อประเภทสินค้า", 300, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("StatusText", "สถานะ", 110, DataGridViewContentAlignment.MiddleCenter)
        SetColumn("CreateOfficer", "ผู้สร้าง", 160, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("CreateTime", "วันที่สร้าง", 170, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yyyy HH:mm")
        SetColumn("UpdateOfficer", "ผู้แก้ไข", 160, DataGridViewContentAlignment.MiddleLeft)
        SetColumn("UpdateTime", "วันที่แก้ไข", 170, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yyyy HH:mm")

        For Each columnName As String In {"Status", "CONumber", "UONumber"}
            If dgvCategory.Columns.Contains(columnName) Then dgvCategory.Columns(columnName).Visible = False
        Next
    End Sub

    Private Sub SetColumn(name As String, headerText As String, fillWeight As Integer,
                          alignment As DataGridViewContentAlignment, Optional format As String = "")
        If Not dgvCategory.Columns.Contains(name) Then Return
        Dim column As DataGridViewColumn = dgvCategory.Columns(name)
        column.HeaderText = headerText
        column.FillWeight = fillWeight
        column.DefaultCellStyle.Alignment = alignment
        If format <> "" Then column.DefaultCellStyle.Format = format
    End Sub

    Private Sub SelectCategoryRow(categoryID As Integer)
        For Each row As DataGridViewRow In dgvCategory.Rows
            If Convert.ToInt32(row.Cells("ID").Value) = categoryID Then
                row.Selected = True
                dgvCategory.CurrentCell = row.Cells("Name")
                Return
            End If
        Next
    End Sub

    Private Function CurrentCategoryID() As Integer
        If dgvCategory.CurrentRow Is Nothing OrElse dgvCategory.CurrentRow.IsNewRow Then Return 0
        Return Convert.ToInt32(dgvCategory.CurrentRow.Cells("ID").Value)
    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadCategories()
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            LoadCategories()
        End If
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        If IsHandleCreated Then LoadCategories()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Using editor As New CategoryEditorDialog()
            editor.EditMode = False
            If editor.ShowDialog(Me) = DialogResult.OK Then
                cboStatus.SelectedIndex = StatusActive
                txtSearch.Clear()
                LoadCategories(editor.CategoryID)
            End If
        End Using
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        EditCurrentCategory()
    End Sub

    Private Sub dgvCategory_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategory.CellDoubleClick
        If e.RowIndex >= 0 Then EditCurrentCategory()
    End Sub

    Private Sub EditCurrentCategory()
        Dim categoryID As Integer = CurrentCategoryID()
        If categoryID <= 0 Then
            MessageBox.Show("กรุณาเลือกประเภทสินค้าที่ต้องการแก้ไข 1 รายการ",
                            "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using editor As New CategoryEditorDialog()
            editor.EditMode = True
            editor.CategoryID = categoryID
            editor.CategoryName = Convert.ToString(dgvCategory.CurrentRow.Cells("Name").Value).Trim()
            editor.IsInactive = Not Convert.IsDBNull(dgvCategory.CurrentRow.Cells("Status").Value)
            If editor.ShowDialog(Me) = DialogResult.OK Then
                LoadCategories(categoryID)
            End If
        End Using
    End Sub

    Private Sub dgvCategory_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCategory.CellFormatting
        If e.RowIndex < 0 OrElse Not dgvCategory.Columns.Contains("Status") Then Return
        Dim statusValue As Object = dgvCategory.Rows(e.RowIndex).Cells("Status").Value
        If Not Convert.IsDBNull(statusValue) Then
            dgvCategory.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
            dgvCategory.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.DarkRed
        End If
    End Sub

    Private Sub btnCancelCategory_Click(sender As Object, e As EventArgs) Handles btnCancelCategory.Click
        If dgvCategory.SelectedRows.Count = 0 Then
            MessageBox.Show("กรุณาเลือกประเภทสินค้าที่ต้องการยกเลิกการใช้",
                            "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim answer As DialogResult = MessageBox.Show(
            "ต้องการกำหนดประเภทสินค้าที่เลือกเป็น 'ไม่ใช้แล้ว' ใช่หรือไม่?",
            "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2)
        If answer <> DialogResult.Yes Then Return

        Try
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using transaction As SqlTransaction = conn.BeginTransaction()
                    For Each row As DataGridViewRow In dgvCategory.SelectedRows
                        Using cmd As New SqlCommand(
                            "UPDATE Category SET Status=1,UONumber=@OfficerNumber,UTime=GETDATE() WHERE ID=@ID", conn, transaction)
                            cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = LoginOfficerNumber()
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(row.Cells("ID").Value)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    transaction.Commit()
                End Using
            End Using
            MessageBox.Show("ยกเลิกการใช้ประเภทสินค้าเรียบร้อย", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCategories()
        Catch ex As Exception
            MessageBox.Show("ยกเลิกประเภทสินค้าไม่สำเร็จ: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function LoginOfficerNumber() As Integer
        If ModSession.LoginOfficerID > 0 Then Return Convert.ToInt32(ModSession.LoginOfficerID)
        Return 1
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Close()
            Return True
        End If
        If keyData = (Keys.Control Or Keys.N) Then
            btnAdd.PerformClick()
            Return True
        End If
        If keyData = Keys.Enter AndAlso dgvCategory.Focused Then
            EditCurrentCategory()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Class CategoryEditorDialog
        Inherits Form

        Private ReadOnly txtName As New TextBox()
        Private ReadOnly chkInactive As New CheckBox()
        Private ReadOnly lblIDValue As New Label()
        Private ReadOnly btnSave As New Button()
        Private ReadOnly btnCancel As New Button()

        Public Property EditMode As Boolean
        Public Property CategoryID As Integer
        Public Property CategoryName As String
        Public Property IsInactive As Boolean

        Public Sub New()
            Text = "เพิ่มประเภทสินค้า"
            StartPosition = FormStartPosition.CenterParent
            FormBorderStyle = FormBorderStyle.FixedDialog
            MaximizeBox = False
            MinimizeBox = False
            ClientSize = New Size(590, 285)
            BackColor = Color.White
            Font = New Font("Cordia New", 14.0!)

            Dim header As New Panel() With {
                .Dock = DockStyle.Top,
                .Height = 65,
                .BackColor = Color.FromArgb(37, 99, 235)
            }
            Dim title As New Label() With {
                .AutoSize = True,
                .Location = New Point(18, 10),
                .Font = New Font("Cordia New", 20.0!, FontStyle.Bold),
                .ForeColor = Color.White,
                .Text = "เพิ่มประเภทสินค้า"
            }
            title.Name = "lblEditorTitle"
            header.Controls.Add(title)

            Dim lblID As New Label() With {.AutoSize = True, .Location = New Point(24, 86), .Text = "ID"}
            lblIDValue.AutoSize = True
            lblIDValue.Location = New Point(145, 86)
            lblIDValue.Font = New Font("Cordia New", 14.0!, FontStyle.Bold)

            Dim lblName As New Label() With {.AutoSize = True, .Location = New Point(24, 125), .Text = "ชื่อประเภทสินค้า *"}
            txtName.Location = New Point(145, 118)
            txtName.Size = New Size(415, 38)
            txtName.Font = New Font("Cordia New", 15.0!)

            chkInactive.Location = New Point(145, 166)
            chkInactive.AutoSize = True
            chkInactive.Text = "ไม่ใช้แล้ว"

            btnSave.Location = New Point(330, 220)
            btnSave.Size = New Size(110, 45)
            btnSave.Text = "บันทึก"
            btnSave.Font = New Font("Cordia New", 15.0!, FontStyle.Bold)
            btnSave.BackColor = Color.FromArgb(20, 184, 166)
            btnSave.ForeColor = Color.White
            btnSave.FlatStyle = FlatStyle.Flat

            btnCancel.Location = New Point(450, 220)
            btnCancel.Size = New Size(110, 45)
            btnCancel.Text = "ยกเลิก"
            btnCancel.Font = New Font("Cordia New", 15.0!, FontStyle.Bold)
            btnCancel.BackColor = Color.White
            btnCancel.FlatStyle = FlatStyle.Flat

            Controls.Add(header)
            Controls.Add(lblID)
            Controls.Add(lblIDValue)
            Controls.Add(lblName)
            Controls.Add(txtName)
            Controls.Add(chkInactive)
            Controls.Add(btnSave)
            Controls.Add(btnCancel)
            AcceptButton = btnSave
            CancelButton = btnCancel

            AddHandler Load, AddressOf Editor_Load
            AddHandler btnSave.Click, AddressOf Save_Click
            AddHandler btnCancel.Click, Sub() Close()
        End Sub

        Private Sub Editor_Load(sender As Object, e As EventArgs)
            Dim title As Label = DirectCast(Controls.Find("lblEditorTitle", True)(0), Label)
            If EditMode Then
                Text = "แก้ไขประเภทสินค้า"
                title.Text = "แก้ไขประเภทสินค้า"
                lblIDValue.Text = CategoryID.ToString()
                txtName.Text = If(CategoryName, "")
                chkInactive.Checked = IsInactive
                chkInactive.Visible = True
            Else
                CategoryID = GetNextPreviewID()
                lblIDValue.Text = CategoryID.ToString()
                chkInactive.Visible = False
            End If
            txtName.Focus()
            txtName.SelectAll()
        End Sub

        Private Function GetNextPreviewID() As Integer
            Using conn As SqlConnection = modVariable.OpenConnection()
                Using cmd As New SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM Category", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        Private Sub Save_Click(sender As Object, e As EventArgs)
            Dim nameValue As String = txtName.Text.Trim()
            If nameValue = "" Then
                MessageBox.Show("กรุณาป้อนชื่อประเภทสินค้า", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Return
            End If

            btnSave.Enabled = False
            Cursor = Cursors.WaitCursor
            Try
                Using conn As SqlConnection = modVariable.OpenConnection()
                    Using transaction As SqlTransaction = conn.BeginTransaction()
                        Dim savedID As Integer = CategoryID
                        If Not EditMode Then savedID = NextCategoryID(conn, transaction)
                        If IsDuplicateName(conn, transaction, nameValue, savedID) Then
                            Throw New ApplicationException("ชื่อประเภทสินค้านี้มีอยู่แล้ว")
                        End If

                        If EditMode Then
                            UpdateCategory(conn, transaction, savedID, nameValue)
                        Else
                            InsertCategory(conn, transaction, savedID, nameValue)
                        End If
                        transaction.Commit()
                        CategoryID = savedID
                        CategoryName = nameValue
                    End Using
                End Using
                DialogResult = DialogResult.OK
                Close()
            Catch ex As Exception
                MessageBox.Show("บันทึกประเภทสินค้าไม่สำเร็จ: " & ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor = Cursors.Default
                btnSave.Enabled = True
            End Try
        End Sub

        Private Function NextCategoryID(conn As SqlConnection, transaction As SqlTransaction) As Integer
            Using cmd As New SqlCommand(
                "SELECT ISNULL(MAX(ID),0)+1 FROM Category WITH (UPDLOCK,HOLDLOCK)", conn, transaction)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Function

        Private Function IsDuplicateName(conn As SqlConnection, transaction As SqlTransaction,
                                         nameValue As String, currentID As Integer) As Boolean
            Using cmd As New SqlCommand(
                "SELECT COUNT(1) FROM Category WHERE Name=@Name AND ID<>@ID", conn, transaction)
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = nameValue
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = currentID
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Function

        Private Sub InsertCategory(conn As SqlConnection, transaction As SqlTransaction,
                                   categoryID As Integer, nameValue As String)
            Const sql As String =
                "INSERT INTO Category(ID,Name,CONumber,UONumber,CTime,UTime) " &
                "VALUES(@ID,@Name,@OfficerNumber,@OfficerNumber,GETDATE(),GETDATE())"
            Using cmd As New SqlCommand(sql, conn, transaction)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = categoryID
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = nameValue
                cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = CurrentOfficerNumber()
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Private Sub UpdateCategory(conn As SqlConnection, transaction As SqlTransaction,
                                   categoryID As Integer, nameValue As String)
            Const sql As String =
                "UPDATE Category SET Name=@Name,Status=@Status,UONumber=@OfficerNumber,UTime=GETDATE() WHERE ID=@ID"
            Using cmd As New SqlCommand(sql, conn, transaction)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = categoryID
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = nameValue
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = If(chkInactive.Checked, CType(1, Object), DBNull.Value)
                cmd.Parameters.Add("@OfficerNumber", SqlDbType.Int).Value = CurrentOfficerNumber()
                If cmd.ExecuteNonQuery() <> 1 Then Throw New ApplicationException("ไม่พบประเภทสินค้าที่ต้องการแก้ไข")
            End Using
        End Sub

        Private Function CurrentOfficerNumber() As Integer
            If ModSession.LoginOfficerID > 0 Then Return Convert.ToInt32(ModSession.LoginOfficerID)
            Return 1
        End Function
    End Class
End Class
