<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class fProduct
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(fProduct))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnEditProduct = New Krypton.Toolkit.KryptonButton()
        Me.btnCloneProduct = New Krypton.Toolkit.KryptonButton()
        Me.btnAddProduct = New Krypton.Toolkit.KryptonButton()
        Me.btnSearch = New Krypton.Toolkit.KryptonButton()
        Me.cbStatus = New Krypton.Toolkit.KryptonComboBox()
        Me.cbCategory = New Krypton.Toolkit.KryptonComboBox()
        Me.cbBrand = New Krypton.Toolkit.KryptonComboBox()
        Me.cbProductGroup = New Krypton.Toolkit.KryptonComboBox()
        Me.txtSearch = New Krypton.Toolkit.KryptonTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.dtDetail = New Krypton.Toolkit.KryptonDataGridView()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbProductGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dtDetail, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 132.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1910, 999)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.btnEditProduct)
        Me.Panel1.Controls.Add(Me.btnCloneProduct)
        Me.Panel1.Controls.Add(Me.btnAddProduct)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.cbStatus)
        Me.Panel1.Controls.Add(Me.cbCategory)
        Me.Panel1.Controls.Add(Me.cbBrand)
        Me.Panel1.Controls.Add(Me.cbProductGroup)
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblBrand)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1904, 126)
        Me.Panel1.TabIndex = 8
        '
        'btnEditProduct
        '
        Me.btnEditProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEditProduct.Location = New System.Drawing.Point(1688, 64)
        Me.btnEditProduct.Name = "btnEditProduct"
        Me.btnEditProduct.Size = New System.Drawing.Size(175, 53)
        Me.btnEditProduct.StateCommon.Back.Color1 = System.Drawing.Color.DodgerBlue
        Me.btnEditProduct.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White
        Me.btnEditProduct.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditProduct.TabIndex = 127
        Me.btnEditProduct.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btnEditProduct.Values.Text = "Edit (แก้ไข)"
        '
        'btnCloneProduct
        '
        Me.btnCloneProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCloneProduct.Location = New System.Drawing.Point(1779, 5)
        Me.btnCloneProduct.Name = "btnCloneProduct"
        Me.btnCloneProduct.Size = New System.Drawing.Size(114, 53)
        Me.btnCloneProduct.StateCommon.Back.Color1 = System.Drawing.Color.MediumOrchid
        Me.btnCloneProduct.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCloneProduct.TabIndex = 129
        Me.btnCloneProduct.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btnCloneProduct.Values.Text = "Clone"
        Me.btnCloneProduct.Values.UACShieldIconSize = Krypton.Toolkit.IconSize.MediumSmall
        '
        'btnAddProduct
        '
        Me.btnAddProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddProduct.Location = New System.Drawing.Point(1659, 5)
        Me.btnAddProduct.Name = "btnAddProduct"
        Me.btnAddProduct.Size = New System.Drawing.Size(114, 53)
        Me.btnAddProduct.StateCommon.Back.Color1 = System.Drawing.Color.Lime
        Me.btnAddProduct.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddProduct.TabIndex = 128
        Me.btnAddProduct.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btnAddProduct.Values.Text = "Add (เพิ่ม)"
        Me.btnAddProduct.Values.UACShieldIconSize = Krypton.Toolkit.IconSize.MediumSmall
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(1498, 44)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(140, 53)
        Me.btnSearch.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.TabIndex = 127
        Me.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty
        Me.btnSearch.Values.Text = "Search (ค้นหา)"
        Me.btnSearch.Values.UACShieldIconSize = Krypton.Toolkit.IconSize.MediumSmall
        '
        'cbStatus
        '
        Me.cbStatus.DropDownHeight = 300
        Me.cbStatus.IntegralHeight = False
        Me.cbStatus.Location = New System.Drawing.Point(1312, 52)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(147, 44)
        Me.cbStatus.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbStatus.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cbStatus.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.TabIndex = 57
        '
        'cbCategory
        '
        Me.cbCategory.DropDownHeight = 300
        Me.cbCategory.IntegralHeight = False
        Me.cbCategory.Location = New System.Drawing.Point(494, 51)
        Me.cbCategory.Name = "cbCategory"
        Me.cbCategory.Size = New System.Drawing.Size(230, 44)
        Me.cbCategory.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCategory.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbCategory.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cbCategory.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCategory.TabIndex = 56
        '
        'cbBrand
        '
        Me.cbBrand.DropDownHeight = 300
        Me.cbBrand.IntegralHeight = False
        Me.cbBrand.Location = New System.Drawing.Point(1071, 51)
        Me.cbBrand.Name = "cbBrand"
        Me.cbBrand.Size = New System.Drawing.Size(217, 44)
        Me.cbBrand.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBrand.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbBrand.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cbBrand.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBrand.TabIndex = 55
        '
        'cbProductGroup
        '
        Me.cbProductGroup.DropDownHeight = 300
        Me.cbProductGroup.IntegralHeight = False
        Me.cbProductGroup.Location = New System.Drawing.Point(747, 51)
        Me.cbProductGroup.Name = "cbProductGroup"
        Me.cbProductGroup.Size = New System.Drawing.Size(308, 44)
        Me.cbProductGroup.StateCommon.ComboBox.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProductGroup.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near
        Me.cbProductGroup.StateCommon.Item.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cbProductGroup.StateCommon.Item.Content.ShortText.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProductGroup.TabIndex = 54
        '
        'txtSearch
        '
        Me.txtSearch.InputControlStyle = Krypton.Toolkit.InputControlStyle.Ribbon
        Me.txtSearch.Location = New System.Drawing.Point(21, 52)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem
        Me.txtSearch.Size = New System.Drawing.Size(389, 45)
        Me.txtSearch.StateCommon.Border.Color1 = System.Drawing.Color.DarkGoldenrod
        Me.txtSearch.StateCommon.Content.Color1 = System.Drawing.Color.Blue
        Me.txtSearch.StateCommon.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.TabIndex = 52
        Me.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(508, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 29)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "ประเภท (Category)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1307, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 29)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "สถานะ (Status)"
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBrand.Location = New System.Drawing.Point(1076, 18)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(98, 29)
        Me.lblBrand.TabIndex = 51
        Me.lblBrand.Text = "ยี่ห้อ (Brand)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(742, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 29)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "กลุ่มสินค้า (Product Group)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 29)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "ค้นหาเมนู"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Window
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Font = New System.Drawing.Font("Cordia New", 13.8!)
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(411, 52)
        Me.Button2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(59, 45)
        Me.Button2.TabIndex = 13
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = False
        '
        'dtDetail
        '
        Me.dtDetail.AllowUserToAddRows = False
        Me.dtDetail.AllowUserToDeleteRows = False
        Me.dtDetail.AllowUserToResizeRows = False
        Me.dtDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dtDetail.ColumnHeadersHeight = 50
        Me.dtDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dtDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtDetail.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Sheet
        Me.dtDetail.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dtDetail.GridStyles.StyleColumn = Krypton.Toolkit.GridStyle.Sheet
        Me.dtDetail.GridStyles.StyleDataCells = Krypton.Toolkit.GridStyle.Sheet
        Me.dtDetail.GridStyles.StyleRow = Krypton.Toolkit.GridStyle.Sheet
        Me.dtDetail.Location = New System.Drawing.Point(6, 138)
        Me.dtDetail.Margin = New System.Windows.Forms.Padding(6)
        Me.dtDetail.MultiSelect = False
        Me.dtDetail.Name = "dtDetail"
        Me.dtDetail.ReadOnly = True
        Me.dtDetail.RowHeadersVisible = False
        Me.dtDetail.RowHeadersWidth = 51
        Me.dtDetail.RowTemplate.Height = 36
        Me.dtDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtDetail.Size = New System.Drawing.Size(1898, 855)
        Me.dtDetail.StateCommon.Background.Color1 = System.Drawing.Color.White
        Me.dtDetail.StateCommon.Background.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid
        Me.dtDetail.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundSheet
        Me.dtDetail.StateCommon.DataCell.Content.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDetail.StateCommon.HeaderColumn.Content.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDetail.TabIndex = 7
        '
        'DataGridViewTextBoxColumn19
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridViewTextBoxColumn19.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn19.HeaderText = "Extra Volume"
        Me.DataGridViewTextBoxColumn19.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Width = 120
        '
        'DataGridViewTextBoxColumn35
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridViewTextBoxColumn35.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn35.HeaderText = "Location (ที่เก็บสินค้า)"
        Me.DataGridViewTextBoxColumn35.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        Me.DataGridViewTextBoxColumn35.Width = 135
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Cordia New", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1924, 1055)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 46)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1916, 1005)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ค้นหา"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'fProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 1055)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "fProduct"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "สินค้า"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbBrand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbProductGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents dtDetail As Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblBrand As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents txtSearch As Krypton.Toolkit.KryptonTextBox
    Friend WithEvents DataGridViewTextBoxColumn19 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As DataGridViewTextBoxColumn
    Friend WithEvents cbStatus As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents cbCategory As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents cbBrand As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents cbProductGroup As Krypton.Toolkit.KryptonComboBox
    Friend WithEvents btnSearch As Krypton.Toolkit.KryptonButton
    Friend WithEvents btnAddProduct As Krypton.Toolkit.KryptonButton
    Friend WithEvents btnCloneProduct As Krypton.Toolkit.KryptonButton
    Friend WithEvents btnEditProduct As Krypton.Toolkit.KryptonButton
End Class








