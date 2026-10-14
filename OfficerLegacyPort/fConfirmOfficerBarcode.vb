'  
' Type: NewStock.fConfirmOfficerBarcode
' Assembly: SabuySoft, Version=1.0.7471.10171, Culture=neutral, PublicKeyToken=null
' MVID: A0E28F6D-1A8F-4615-A343-F46533641B26
' Assembly location: \SabuySoft.exe

Imports NewStock.Properties
Imports System
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Namespace NewStock

    Public Class fConfirmOfficerBarcode
        Inherits Form
        Public [myClass] As [MyClass]
        Public lLoginONumber As Long = 0
        Public m_lLoginONumberReturn As Long = 0
        Public m_strOfficerName As String = ""
        Private iShowOfficerBarcodeWhenInput As Integer = 1
        Private components As IContainer = Nothing
        Private btCancel As Button
        Private btOK As Button
        Private tbBarcode As TextBox
        Private lbUseName As Label

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub fConfirmOfficerBarcode_Load(sender As Object, e As EventArgs)
            Dim IntValueOut As Long = 0
            Dim MoneyValueOut = 0D
            Dim StringValueOut = ""
            If [myClass].GetDefaultProperty("OTHER007", IntValueOut, MoneyValueOut, StringValueOut) Then iShowOfficerBarcodeWhenInput = CInt(IntValueOut)
            If iShowOfficerBarcodeWhenInput <> 0 Then Return
            tbBarcode.UseSystemPasswordChar = True
        End Sub

        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
            If msg.WParam.ToInt32() <> 27 Then Return MyBase.ProcessCmdKey(msg, keyData)
            Close()
            Return True
        End Function

        Private Sub tbBarcode_Enter(sender As Object, e As EventArgs)
            tbBarcode.SelectAll()
        End Sub

        Private Sub tbBarcode_KeyPress(sender As Object, e As KeyPressEventArgs)
            If Not [myClass].CheckString(e.KeyChar) Then e.Handled = True
            If e.KeyChar <> ChrW(13) Then Return
            OKClick()
        End Sub

        Private Sub MakeTrueValue()
            tbBarcode.Text = tbBarcode.Text.TrimEnd()
        End Sub

        Private Sub btOK_Click(sender As Object, e As EventArgs)
            OKClick()
        End Sub

        Private Sub OKClick()
            Dim sqlConnection As SqlConnection = New SqlConnection([myClass].ConnectionString)
            Dim command As SqlCommand = sqlConnection.CreateCommand()
            Dim text = ""
            Dim flag = True
            MakeTrueValue()
            If flag AndAlso Equals(tbBarcode.Text, "") Then
                text = "กรุณาป้อน Barocde พนักงานก่อนกดปุ่ม OK !"
                flag = False
            End If
            If flag Then
                Try
                    Cursor = Cursors.WaitCursor
                    sqlConnection.Open()
                    command.CommandText = $"SELECT Officer.OfficerNumber,Officer.Name FROM Officer WHERE Officer.Barcode = N'{tbBarcode.Text}' AND Officer.Status IS NULL"
                    Dim sqlDataReader As SqlDataReader = command.ExecuteReader()
                    If sqlDataReader.Read() Then
                        m_lLoginONumberReturn = CLng(sqlDataReader.GetInt32(0))
                        m_strOfficerName = sqlDataReader.GetString(1).TrimEnd()
                    Else
                        Dim str = "ไม่พบ Barcode พนักงาน"
                        text = If(iShowOfficerBarcodeWhenInput <> 0, $"{str} = '{tbBarcode.Text}' ", str & "ที่ป้อน") & "ในระบบ" & vbLf & "กรุณาลองใหม่อีกครั้ง !"
                        flag = False
                    End If
                    sqlDataReader.Close()
                Catch ex As Exception
                    flag = False
                    text = ex.Message
                End Try
            End If
            sqlConnection.Close()
            Cursor = Cursors.Default
            If flag Then DialogResult = DialogResult.OK
            If flag Then Return
            Dim num As Integer = MessageBox.Show(text, "Warnning message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            tbBarcode.Focus()
            tbBarcode.SelectAll()
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            tbBarcode = New TextBox()
            lbUseName = New Label()
            btCancel = New Button()
            btOK = New Button()
            SuspendLayout()
            tbBarcode.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            tbBarcode.ForeColor = Color.Blue
            tbBarcode.Location = New Point(246, 15)
            tbBarcode.MaxLength = 19
            tbBarcode.Name = "tbBarcode"
            tbBarcode.Size = New Size(256, 22)
            tbBarcode.TabIndex = 1
            AddHandler tbBarcode.Enter, New EventHandler(AddressOf tbBarcode_Enter)
            AddHandler tbBarcode.KeyPress, New KeyPressEventHandler(AddressOf tbBarcode_KeyPress)
            lbUseName.BackColor = Color.Transparent
            lbUseName.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            lbUseName.ForeColor = SystemColors.WindowText
            lbUseName.Location = New Point(12, 15)
            lbUseName.Name = "lbUseName"
            lbUseName.Size = New Size(228, 21)
            lbUseName.TabIndex = 0
            lbUseName.Text = "Officer &Barcode (Barcode พนักงาน)"
            lbUseName.TextAlign = ContentAlignment.MiddleRight
            btCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            btCancel.BackColor = Color.Salmon
            btCancel.DialogResult = DialogResult.Cancel
            btCancel.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btCancel.Image = CType(Resources.Cancelsmall, Image)
            btCancel.Location = New Point(401, 66)
            btCancel.Name = "btCancel"
            btCancel.Size = New Size(101, 32)
            btCancel.TabIndex = 3
            btCancel.Text = "&Cancel"
            btCancel.TextImageRelation = TextImageRelation.ImageBeforeText
            btCancel.UseVisualStyleBackColor = False
            btOK.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
            btOK.BackColor = Color.Yellow
            btOK.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 222)
            btOK.Image = CType(Resources.OKsmall, Image)
            btOK.Location = New Point(289, 66)
            btOK.Name = "btOK"
            btOK.Size = New Size(106, 32)
            btOK.TabIndex = 2
            btOK.Text = "&OK"
            btOK.TextImageRelation = TextImageRelation.ImageBeforeText
            btOK.UseVisualStyleBackColor = False
            AddHandler btOK.Click, New EventHandler(AddressOf btOK_Click)
            AutoScaleDimensions = New SizeF(6F, 13F)
            AutoScaleMode = AutoScaleMode.Font
            BackColor = Color.PaleGoldenrod
            ClientSize = New Size(547, 110)
            Controls.Add(btCancel)
            Controls.Add(btOK)
            Controls.Add(tbBarcode)
            Controls.Add(lbUseName)
            Name = NameOf(fConfirmOfficerBarcode)
            ShowInTaskbar = False
            StartPosition = FormStartPosition.CenterScreen
            Text = "Confirm Officer Barcode"
            AddHandler Load, New EventHandler(AddressOf fConfirmOfficerBarcode_Load)
            ResumeLayout(False)
            PerformLayout()
        End Sub

    End Class
End Namespace
