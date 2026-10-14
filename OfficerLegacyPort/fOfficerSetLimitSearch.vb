'  
' Type: NewStock.fOfficerSetLimitSearch
' Assembly: SabuySoft, Version=1.0.7471.10171, Culture=neutral, PublicKeyToken=null
' MVID: A0E28F6D-1A8F-4615-A343-F46533641B26
' Assembly location: \SabuySoft.exe

Imports NewStock.Properties
Imports System
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace NewStock

    Public Class fOfficerSetLimitSearch
        Inherits Form
        Public [myClass] As [MyClass]
        Public lLoginONumber As Long = 0
        Public m_lEditOficerNumber As Long = 0
        Private components As IContainer = Nothing
        Private label10 As Label
        Public tbNumDaysLimitSearchSell As TextBox
        Private lbMode As Label
        Private btCancel As Button
        Private btOK As Button
        Public cbLimitSearchSell As CheckBox
        Public lbOfficerName As Label
        Public lbOfficerNumber As Label
        Private label1 As Label
        Private label2 As Label
        Private label3 As Label
        Public cbLimitSearchDrawer As CheckBox
        Private label4 As Label
        Public tbNumDaysLimitSearchDrawer As TextBox
        Public cbLimitSearchExpense As CheckBox
        Private label5 As Label
        Public tbNumDaysLimitSearchExpense As TextBox

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub fOfficerSetLimitSearch_Load(sender As Object, e As EventArgs)
            LoadOfficerLimitSearch()
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If msg.WParam.ToInt32() <> 27 Then Return MyBase.ProcessCmdKey(msg, keyData)
            Close()
            Return True
        End Function

        Private Sub cbLimitSearchSell_CheckedChanged(sender As Object, e As EventArgs)
            If cbLimitSearchSell.Checked Then
                tbNumDaysLimitSearchSell.Enabled = True
            Else
                tbNumDaysLimitSearchSell.Enabled = False
            End If
        End Sub

        Private Sub cbLimitSearchSell_KeyPress(sender As Object, e As KeyPressEventArgs)
            If e.KeyChar <> ChrW(13) Then Return
            tbNumDaysLimitSearchSell.Focus()
        End Sub

        Private Sub tbNumDaysLimitSearchSell_Enter(sender As Object, e As EventArgs)
            tbNumDaysLimitSearchSell.SelectAll()
        End Sub

        Private Sub tbNumDaysLimitSearchSell_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckNumber(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            btOK.Focus()
        End Sub

        Private Sub tbNumDaysLimitSearchSell_Leave(sender As Object, e As EventArgs)
            tbNumDaysLimitSearchSellLeave()
        End Sub

        Private Sub tbNumDaysLimitSearchSellLeave()
            If Equals(tbNumDaysLimitSearchSell.Text, "") Then
                tbNumDaysLimitSearchSell.Text = "0"
            Else
                tbNumDaysLimitSearchSell.Text = Convert.ToInt32(tbNumDaysLimitSearchSell.Text).ToString()
            End If
        End Sub

        Private Sub MakeTrueValue()
            tbNumDaysLimitSearchSellLeave()
            tbNumDaysLimitSearchDrawerLeave()
            tbNumDaysLimitSearchExpenseLeave()
        End Sub

        Private Sub btCancel_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub btOK_Click(sender As Object, e As EventArgs)
            btOKClick()
        End Sub

        Private Sub btOKClick()
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim flag = True
            Dim text = ""
            Cursor = Cursors.WaitCursor
            Try
                sqlConnection.Open()
            Catch ex As Exception
                flag = False
                text = ex.Message
            End Try
            Try
                Me.SetLimitSearch(command, 1L, cbLimitSearchSell.Checked, tbNumDaysLimitSearchSell.Text)
                Me.SetLimitSearch(command, 2L, cbLimitSearchDrawer.Checked, tbNumDaysLimitSearchDrawer.Text)
                Me.SetLimitSearch(command, 3L, cbLimitSearchExpense.Checked, tbNumDaysLimitSearchExpense.Text)
            Catch ex As Exception
                flag = False
                text = ex.Message
            End Try
            sqlConnection.Close()
            Cursor = Cursors.Default
            If flag Then
                Dim num1 As Integer = MessageBox.Show("โปรแกรมทำการบันทึก การจำกัดการค้นหาของพนักงาน เสร็จเรียบร้อยแล้ว.", "Done message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Else
                Dim num2 As Integer = MessageBox.Show(text, "Warnning message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Close()
        End Sub

        Private Sub SetLimitSearch(myCommand As SqlCommand, DocTypeIn As Long, CheckedIn As Boolean, NumDaysLimitSearchTextIn As String)
            myCommand.CommandText = $"SELECT OfficerLimitSearch.OfficerNumber FROM OfficerLimitSearch WHERE OfficerLimitSearch.OfficerNumber = {m_lEditOficerNumber.ToString()} AND OfficerLimitSearch.DocType = {DocTypeIn.ToString()}"
            Dim sqlDataReader As SqlDataReader = myCommand.ExecuteReader()
            Dim flag = False
            If sqlDataReader.HasRows Then flag = True
            sqlDataReader.Close()
            If flag Then
                myCommand.CommandText = "UPDATE OfficerLimitSearch SET "
                If CheckedIn Then
                    Dim sqlCommand As SqlCommand = myCommand
                    sqlCommand.CommandText = $"{sqlCommand.CommandText}NumDays = {NumDaysLimitSearchTextIn}"
                Else
                    myCommand.CommandText += "NumDays = NULL"
                End If
                Dim sqlCommand1 As SqlCommand = myCommand
                sqlCommand1.CommandText = $"{sqlCommand1.CommandText} WHERE OfficerNumber = {m_lEditOficerNumber.ToString()} AND DocType = {DocTypeIn.ToString()}"
                myCommand.ExecuteNonQuery()
            Else
                myCommand.CommandText = $"INSERT INTO OfficerLimitSearch(OfficerNumber,DocType,NumDays) Values({m_lEditOficerNumber.ToString()},{DocTypeIn.ToString()},"
                If CheckedIn Then
                    myCommand.CommandText += NumDaysLimitSearchTextIn
                Else
                    myCommand.CommandText += "NULL"
                End If
                myCommand.CommandText += ")"
                myCommand.ExecuteNonQuery()
            End If
        End Sub

        Private Sub LoadOfficerLimitSearch()
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim flag = True
            Dim text = ""
            Dim CheckIn = False
            Dim NumDaysLimitSearchTextIn = ""
            Cursor = Cursors.WaitCursor
            Try
                sqlConnection.Open()
            Catch ex As Exception
                flag = False
                text = ex.Message
            End Try
            Try
                Me.LoadLimiSearch(command, 1L, CheckIn, NumDaysLimitSearchTextIn)
                cbLimitSearchSell.Checked = CheckIn
                tbNumDaysLimitSearchSell.Text = NumDaysLimitSearchTextIn
                Me.LoadLimiSearch(command, 2L, CheckIn, NumDaysLimitSearchTextIn)
                cbLimitSearchDrawer.Checked = CheckIn
                tbNumDaysLimitSearchDrawer.Text = NumDaysLimitSearchTextIn
                Me.LoadLimiSearch(command, 3L, CheckIn, NumDaysLimitSearchTextIn)
                cbLimitSearchExpense.Checked = CheckIn
                tbNumDaysLimitSearchExpense.Text = NumDaysLimitSearchTextIn
            Catch ex As Exception
                flag = False
                text = ex.Message
            End Try
            sqlConnection.Close()
            Cursor = Cursors.Default
            If flag Then Return
            Dim num As Integer = MessageBox.Show(text, "Warnning message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
        End Sub

        Private Sub LoadLimiSearch(myCommand As SqlCommand, DocTypeIn As Long, <Out> ByRef CheckIn As Boolean, <Out> ByRef NumDaysLimitSearchTextIn As String)
            Dim num As Long = -1
            CheckIn = False
            NumDaysLimitSearchTextIn = ""
            myCommand.CommandText = $"SELECT OfficerLimitSearch.NumDays FROM OfficerLimitSearch WHERE OfficerLimitSearch.OfficerNumber = {m_lEditOficerNumber.ToString()} AND OfficerLimitSearch.DocType = {DocTypeIn.ToString()}"
            Dim sqlDataReader As SqlDataReader = myCommand.ExecuteReader()
            If sqlDataReader.Read() Then num = If(Not sqlDataReader.IsDBNull(0), CLng(sqlDataReader.GetInt32(0)), -1L)
            sqlDataReader.Close()
            If num = -1L Then
                CheckIn = False
                NumDaysLimitSearchTextIn = "0"
            Else
                CheckIn = True
                NumDaysLimitSearchTextIn = num.ToString()
            End If
        End Sub

        Private Sub cbLimitSearchDrawer_CheckedChanged(sender As Object, e As EventArgs)
            If cbLimitSearchDrawer.Checked Then
                tbNumDaysLimitSearchDrawer.Enabled = True
            Else
                tbNumDaysLimitSearchDrawer.Enabled = False
            End If
        End Sub

        Private Sub tbNumDaysLimitSearchDrawer_Enter(sender As Object, e As EventArgs)
            tbNumDaysLimitSearchDrawer.SelectAll()
        End Sub

        Private Sub tbNumDaysLimitSearchDrawer_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckNumber(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            btOK.Focus()
        End Sub

        Private Sub tbNumDaysLimitSearchDrawer_Leave(sender As Object, e As EventArgs)
            tbNumDaysLimitSearchDrawerLeave()
        End Sub

        Private Sub tbNumDaysLimitSearchDrawerLeave()
            If Equals(tbNumDaysLimitSearchDrawer.Text, "") Then
                tbNumDaysLimitSearchDrawer.Text = "0"
            Else
                tbNumDaysLimitSearchDrawer.Text = Convert.ToInt32(tbNumDaysLimitSearchDrawer.Text).ToString()
            End If
        End Sub

        Private Sub cbLimitSearchExpense_CheckedChanged(sender As Object, e As EventArgs)
            If cbLimitSearchExpense.Checked Then
                tbNumDaysLimitSearchExpense.Enabled = True
            Else
                tbNumDaysLimitSearchExpense.Enabled = False
            End If
        End Sub

        Private Sub tbNumDaysLimitSearchExpense_Enter(sender As Object, e As EventArgs)
            tbNumDaysLimitSearchExpense.SelectAll()
        End Sub

        Private Sub tbNumDaysLimitSearchExpense_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckNumber(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            btOK.Focus()
        End Sub

        Private Sub tbNumDaysLimitSearchExpense_Leave(sender As Object, e As EventArgs)
            tbNumDaysLimitSearchExpenseLeave()
        End Sub

        Private Sub tbNumDaysLimitSearchExpenseLeave()
            If Equals(tbNumDaysLimitSearchExpense.Text, "") Then
                tbNumDaysLimitSearchExpense.Text = "0"
            Else
                tbNumDaysLimitSearchExpense.Text = Convert.ToInt32(tbNumDaysLimitSearchExpense.Text).ToString()
            End If
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            label10 = New Label()
            tbNumDaysLimitSearchSell = New TextBox()
            lbMode = New Label()
            btCancel = New Button()
            btOK = New Button()
            cbLimitSearchSell = New CheckBox()
            lbOfficerName = New Label()
            lbOfficerNumber = New Label()
            label1 = New Label()
            label2 = New Label()
            label3 = New Label()
            cbLimitSearchDrawer = New CheckBox()
            label4 = New Label()
            tbNumDaysLimitSearchDrawer = New TextBox()
            cbLimitSearchExpense = New CheckBox()
            label5 = New Label()
            tbNumDaysLimitSearchExpense = New TextBox()
            SuspendLayout()
            label10.BackColor = Color.Transparent
            label10.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label10.ForeColor = SystemColors.ControlText
            label10.Location = New Point(495, 126)
            label10.Name = "label10"
            label10.Size = New Size(65, 22)
            label10.TabIndex = 7
            label10.Text = "วัน"
            label10.TextAlign = ContentAlignment.MiddleLeft
            tbNumDaysLimitSearchSell.Enabled = False
            tbNumDaysLimitSearchSell.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbNumDaysLimitSearchSell.ForeColor = Color.Blue
            tbNumDaysLimitSearchSell.Location = New Point(411, 126)
            tbNumDaysLimitSearchSell.MaxLength = 9
            tbNumDaysLimitSearchSell.Name = "tbNumDaysLimitSearchSell"
            tbNumDaysLimitSearchSell.Size = New Size(78, 22)
            tbNumDaysLimitSearchSell.TabIndex = 6
            tbNumDaysLimitSearchSell.Text = "0"
            tbNumDaysLimitSearchSell.TextAlign = HorizontalAlignment.Right
            AddHandler tbNumDaysLimitSearchSell.Enter, New EventHandler(AddressOf tbNumDaysLimitSearchSell_Enter)
            AddHandler tbNumDaysLimitSearchSell.KeyPress, New KeyPressEventHandler(AddressOf tbNumDaysLimitSearchSell_KeyPress)
            AddHandler tbNumDaysLimitSearchSell.Leave, New EventHandler(AddressOf tbNumDaysLimitSearchSell_Leave)
            lbMode.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            lbMode.BackColor = Color.Bisque
            lbMode.BorderStyle = BorderStyle.FixedSingle
            lbMode.Font = New Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbMode.ForeColor = SystemColors.WindowText
            lbMode.Location = New Point(11, 76)
            lbMode.Name = "lbMode"
            lbMode.Size = New Size(713, 31)
            lbMode.TabIndex = 4
            lbMode.Text = "ตั้งค่าการจำกัดการค้นหา เอกสาร ของพนักงาน"
            lbMode.TextAlign = ContentAlignment.MiddleCenter
            btCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            btCancel.BackColor = Color.Salmon
            btCancel.DialogResult = DialogResult.Cancel
            btCancel.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btCancel.Image = CType(Resources.Cancelsmall, Image)
            btCancel.Location = New Point(623, 231)
            btCancel.Name = "btCancel"
            btCancel.Size = New Size(102, 32)
            btCancel.TabIndex = 9
            btCancel.Text = "&Cancel"
            btCancel.TextImageRelation = TextImageRelation.ImageBeforeText
            btCancel.UseVisualStyleBackColor = False
            AddHandler btCancel.Click, New EventHandler(AddressOf btCancel_Click)
            btOK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            btOK.BackColor = Color.Yellow
            btOK.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btOK.Image = CType(Resources.OKsmall, Image)
            btOK.Location = New Point(504, 231)
            btOK.Name = "btOK"
            btOK.Size = New Size(113, 32)
            btOK.TabIndex = 8
            btOK.Text = "&OK (Save)"
            btOK.TextImageRelation = TextImageRelation.ImageBeforeText
            btOK.UseVisualStyleBackColor = False
            AddHandler btOK.Click, New EventHandler(AddressOf btOK_Click)
            cbLimitSearchSell.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbLimitSearchSell.Location = New Point(100, 126)
            cbLimitSearchSell.Name = "cbLimitSearchSell"
            cbLimitSearchSell.Size = New Size(305, 22)
            cbLimitSearchSell.TabIndex = 5
            cbLimitSearchSell.Text = "จำกัดการค้นหา รายการขาย ได้ย้อนหลังมากที่สุด"
            cbLimitSearchSell.TextAlign = ContentAlignment.MiddleRight
            AddHandler cbLimitSearchSell.CheckedChanged, New EventHandler(AddressOf cbLimitSearchSell_CheckedChanged)
            AddHandler cbLimitSearchSell.KeyPress, New KeyPressEventHandler(AddressOf cbLimitSearchSell_KeyPress)
            lbOfficerName.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            lbOfficerName.BackColor = Color.Transparent
            lbOfficerName.BorderStyle = BorderStyle.FixedSingle
            lbOfficerName.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbOfficerName.ForeColor = Color.Blue
            lbOfficerName.Location = New Point(210, 34)
            lbOfficerName.Name = "lbOfficerName"
            lbOfficerName.Size = New Size(514, 23)
            lbOfficerName.TabIndex = 3
            lbOfficerName.TextAlign = ContentAlignment.MiddleLeft
            lbOfficerNumber.BackColor = Color.Transparent
            lbOfficerNumber.BorderStyle = BorderStyle.FixedSingle
            lbOfficerNumber.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbOfficerNumber.ForeColor = Color.Green
            lbOfficerNumber.Location = New Point(210, 9)
            lbOfficerNumber.Name = "lbOfficerNumber"
            lbOfficerNumber.Size = New Size(98, 23)
            lbOfficerNumber.TabIndex = 1
            lbOfficerNumber.Text = "0"
            lbOfficerNumber.TextAlign = ContentAlignment.MiddleRight
            label1.BackColor = Color.Transparent
            label1.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label1.ForeColor = SystemColors.WindowText
            label1.Location = New Point(11, 34)
            label1.Name = "label1"
            label1.Size = New Size(193, 23)
            label1.TabIndex = 2
            label1.Text = "Officer Name (ชื่อพนักงาน)"
            label1.TextAlign = ContentAlignment.MiddleRight
            label2.BackColor = Color.Transparent
            label2.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label2.ForeColor = SystemColors.WindowText
            label2.Location = New Point(11, 9)
            label2.Name = "label2"
            label2.Size = New Size(193, 23)
            label2.TabIndex = 0
            label2.Text = "Officer Number (รหัสพนักงาน)"
            label2.TextAlign = ContentAlignment.MiddleRight
            label3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            label3.BackColor = Color.Transparent
            label3.BorderStyle = BorderStyle.FixedSingle
            label3.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label3.ForeColor = SystemColors.WindowText
            label3.Location = New Point(11, 231)
            label3.Name = "label3"
            label3.Size = New Size(478, 31)
            label3.TabIndex = 10
            label3.Text = "การจำกัดสิทธิ์การค้นหาย้อนหลัง จะไม่มีผลในส่วนของกลุ่มเมนู ""Report (รายงาน)"""
            label3.TextAlign = ContentAlignment.MiddleCenter
            cbLimitSearchDrawer.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbLimitSearchDrawer.Location = New Point(100, 154)
            cbLimitSearchDrawer.Name = "cbLimitSearchDrawer"
            cbLimitSearchDrawer.Size = New Size(305, 23)
            cbLimitSearchDrawer.TabIndex = 11
            cbLimitSearchDrawer.Text = "จำกัดการค้นหา รายการลิ้นชักเก็บเงิน ได้ย้อนหลังมากที่สุด"
            cbLimitSearchDrawer.TextAlign = ContentAlignment.MiddleRight
            AddHandler cbLimitSearchDrawer.CheckedChanged, New EventHandler(AddressOf cbLimitSearchDrawer_CheckedChanged)
            label4.BackColor = Color.Transparent
            label4.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label4.ForeColor = SystemColors.ControlText
            label4.Location = New Point(495, 154)
            label4.Name = "label4"
            label4.Size = New Size(65, 22)
            label4.TabIndex = 13
            label4.Text = "วัน"
            label4.TextAlign = ContentAlignment.MiddleLeft
            tbNumDaysLimitSearchDrawer.Enabled = False
            tbNumDaysLimitSearchDrawer.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbNumDaysLimitSearchDrawer.ForeColor = Color.Blue
            tbNumDaysLimitSearchDrawer.Location = New Point(411, 154)
            tbNumDaysLimitSearchDrawer.MaxLength = 9
            tbNumDaysLimitSearchDrawer.Name = "tbNumDaysLimitSearchDrawer"
            tbNumDaysLimitSearchDrawer.Size = New Size(78, 22)
            tbNumDaysLimitSearchDrawer.TabIndex = 12
            tbNumDaysLimitSearchDrawer.Text = "0"
            tbNumDaysLimitSearchDrawer.TextAlign = HorizontalAlignment.Right
            AddHandler tbNumDaysLimitSearchDrawer.Enter, New EventHandler(AddressOf tbNumDaysLimitSearchDrawer_Enter)
            AddHandler tbNumDaysLimitSearchDrawer.KeyPress, New KeyPressEventHandler(AddressOf tbNumDaysLimitSearchDrawer_KeyPress)
            AddHandler tbNumDaysLimitSearchDrawer.Leave, New EventHandler(AddressOf tbNumDaysLimitSearchDrawer_Leave)
            cbLimitSearchExpense.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            cbLimitSearchExpense.Location = New Point(100, 182)
            cbLimitSearchExpense.Name = "cbLimitSearchExpense"
            cbLimitSearchExpense.Size = New Size(305, 23)
            cbLimitSearchExpense.TabIndex = 14
            cbLimitSearchExpense.Text = "จำกัดการค้นหา รายการค่าใช้จ่าย ได้ย้อนหลังมากที่สุด"
            cbLimitSearchExpense.TextAlign = ContentAlignment.MiddleRight
            AddHandler cbLimitSearchExpense.CheckedChanged, New EventHandler(AddressOf cbLimitSearchExpense_CheckedChanged)
            label5.BackColor = Color.Transparent
            label5.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            label5.ForeColor = SystemColors.ControlText
            label5.Location = New Point(495, 182)
            label5.Name = "label5"
            label5.Size = New Size(65, 22)
            label5.TabIndex = 16
            label5.Text = "วัน"
            label5.TextAlign = ContentAlignment.MiddleLeft
            tbNumDaysLimitSearchExpense.Enabled = False
            tbNumDaysLimitSearchExpense.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbNumDaysLimitSearchExpense.ForeColor = Color.Blue
            tbNumDaysLimitSearchExpense.Location = New Point(411, 182)
            tbNumDaysLimitSearchExpense.MaxLength = 9
            tbNumDaysLimitSearchExpense.Name = "tbNumDaysLimitSearchExpense"
            tbNumDaysLimitSearchExpense.Size = New Size(78, 22)
            tbNumDaysLimitSearchExpense.TabIndex = 15
            tbNumDaysLimitSearchExpense.Text = "0"
            tbNumDaysLimitSearchExpense.TextAlign = HorizontalAlignment.Right
            AddHandler tbNumDaysLimitSearchExpense.Enter, New EventHandler(AddressOf tbNumDaysLimitSearchExpense_Enter)
            AddHandler tbNumDaysLimitSearchExpense.KeyPress, New KeyPressEventHandler(AddressOf tbNumDaysLimitSearchExpense_KeyPress)
            AddHandler tbNumDaysLimitSearchExpense.Leave, New EventHandler(AddressOf tbNumDaysLimitSearchExpense_Leave)
            AutoScaleDimensions = New SizeF(6F, 13F)
            AutoScaleMode = AutoScaleMode.Font
            BackColor = Color.LightCyan
            ClientSize = New Size(737, 274)
            Controls.Add(cbLimitSearchExpense)
            Controls.Add(label5)
            Controls.Add(tbNumDaysLimitSearchExpense)
            Controls.Add(cbLimitSearchDrawer)
            Controls.Add(label4)
            Controls.Add(tbNumDaysLimitSearchDrawer)
            Controls.Add(label3)
            Controls.Add(lbOfficerName)
            Controls.Add(lbOfficerNumber)
            Controls.Add(label1)
            Controls.Add(label2)
            Controls.Add(cbLimitSearchSell)
            Controls.Add(btCancel)
            Controls.Add(btOK)
            Controls.Add(lbMode)
            Controls.Add(label10)
            Controls.Add(tbNumDaysLimitSearchSell)
            MaximizeBox = False
            MinimizeBox = False
            Name = NameOf(fOfficerSetLimitSearch)
            ShowInTaskbar = False
            StartPosition = FormStartPosition.CenterScreen
            Text = "Set Limit Search Officer (ตั้งค่าการจำกัดการค้นหา ของพนักงาน)"
            AddHandler Load, New EventHandler(AddressOf fOfficerSetLimitSearch_Load)
            ResumeLayout(False)
            PerformLayout()
        End Sub
    End Class
End Namespace
