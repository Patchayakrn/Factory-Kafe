Public Class Fmailvb
    Private frozenShopMenuItem As ToolStripMenuItem

    Private Sub Fmailvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lokEmp_name.Renderer = New BlueToolStripRenderer()
        lokEmp_name.RenderMode = ToolStripRenderMode.Professional
        IsMdiContainer = True
        For Each control As Control In Controls
            If TypeOf control Is MdiClient Then control.BackColor = Color.FromArgb(238, 245, 252)
        Next

        DatabaseBackup.StartAutomaticBackup(Me)

    End Sub

    Private Sub frozenShopMenuItem_Click(sender As Object, e As EventArgs)
        OpenChild(New frmFrozenShopIssue())
    End Sub

    Private Sub btnBuyProduct_Click(sender As Object, e As EventArgs) Handles btnBuyProduct.Click
        OpenChild(New frmBuy())
    End Sub
    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        OpenChild(New fInventoryCount())
    End Sub

    Private Sub btnMaterialUsage_Click(sender As Object, e As EventArgs) Handles btnMaterialUsage.Click
        OpenChild(New frmMaterialUsage())
    End Sub

    Private Sub btnMaterialIssue_Click(sender As Object, e As EventArgs) Handles btnMaterialIssue.Click
        OpenChild(New frmMaterialIssue())
    End Sub

    Private Sub btnMaterialReceive_Click(sender As Object, e As EventArgs) Handles btnMaterialReceive.Click
        If Not PermissionService.HasPermission("Stock.In") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์รับวัตถุดิบเข้าสต็อก", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        OpenChild(New frmMaterialReceive())
    End Sub

    Private Sub btnMaterialStock_Click(sender As Object, e As EventArgs) Handles btnMaterialStock.Click
        OpenChild(New frmMaterialStock())
    End Sub

    Private Sub btnProduct_Click(sender As Object, e As EventArgs) Handles btnProduct.Click
        OpenChild(New fProduct())
    End Sub

    Private Sub btnMaterialDrinkAdd_Click(sender As Object, e As EventArgs) Handles btnMaterialDrinkAdd.Click
        If Not PermissionService.HasPermission("MaterialDrink.Add") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์เพิ่มวัตถุดิบเครื่องดื่ม", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using dialog As New frmMaterialDrinkAdd()
            dialog.ShowDialog(Me)
        End Using
    End Sub

    Private Sub OpenChild(child As Form)
        For Each opened As Form In MdiChildren
            If opened.GetType() Is child.GetType() Then
                child.Dispose()
                If Not opened.Visible Then opened.Show()
                opened.WindowState = FormWindowState.Maximized
                opened.Activate()
                Return
            End If
        Next
        child.MdiParent = Me
        child.Show()
        'ต้องสั่ง Maximize หลัง Show เพื่อให้ MDI คำนวณขนาดเต็มพื้นที่จริง
        child.WindowState = FormWindowState.Maximized
        child.Activate()
        'บางครั้ง MDI จะคำนวณขนาดลูกใหม่อีกครั้งหลัง Show จบ จึงย้ำอีกครั้งหลังรอบ Layout
        BeginInvoke(New MethodInvoker(Sub()
                                          If Not child.IsDisposed Then
                                              child.WindowState = FormWindowState.Maximized
                                          End If
                                      End Sub))
    End Sub

    Private Sub btnSell_Click(sender As Object, e As EventArgs) Handles btnSell.Click
        OpenChild(New fsell())
    End Sub

    Private Sub Fmailvb_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DatabaseBackup.StopAutomaticBackup()
        Application.Exit()
    End Sub

    Private Sub Fmailvb_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'เมื่อ Frozen Shop เป็น MDI Child และขยายเต็มจอ
        'กากบาทมุมขวาบนจะเป็นของ Fmailvb ให้ปิดเฉพาะ Child ก่อน
        If ActiveMdiChild IsNot Nothing AndAlso TypeOf ActiveMdiChild Is frmFrozenShopIssue Then
            e.Cancel = True
            ActiveMdiChild.Hide()
        End If
    End Sub

    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        OpenChild(New fCustomer())
    End Sub

    Private Sub btnCategory_Click(sender As Object, e As EventArgs) Handles btnCategory.Click
        OpenChild(New frmCategory())
    End Sub

    Private Sub btnGroupProduct_Click(sender As Object, e As EventArgs) Handles btnGroupProduct.Click
        OpenChild(New frmProductGroup())
    End Sub

    Private Sub btnPromotion_Click(sender As Object, e As EventArgs) Handles btnPromotion.Click
        OpenChild(New frmPromotion())
    End Sub

    Private Sub btnDaiySales_Click(sender As Object, e As EventArgs) Handles btnDaiySales.Click
        OpenChild(New frmDailySales())
    End Sub

    Private Sub btnReportbyProduct_Click(sender As Object, e As EventArgs) Handles btnReportbyProduct.Click
        OpenChild(New fReportSellByProductGroup())
    End Sub

    Private Sub btnStockMovement_Click(sender As Object, e As EventArgs) Handles btnStockMovement.Click
        OpenChild(New fReportStock())
    End Sub

    Private Sub btnVatSell_Click(sender As Object, e As EventArgs) Handles btnVatSell.Click
        OpenChild(New frmVatSell())
    End Sub

    Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
        DatabaseBackup.BackupCurrentDatabase(Me)
    End Sub

    Private Sub btnOfficer_Click(sender As Object, e As EventArgs) Handles btnOfficer.Click
        OpenChild(New fOfficer())
    End Sub

    Private Sub btnFrozenShopIssue_Click(sender As Object, e As EventArgs) Handles btnFrozenShopIssue.Click
        OpenChild(New frmFrozenShopIssue())
    End Sub

    Private Sub btnNotHoliday_Click(sender As Object, e As EventArgs) Handles btnNotHoliday.Click
        OpenChild(New frmMemoEmployeeHoliday())
    End Sub

    Private Sub btnMemoPromotion_Click(sender As Object, e As EventArgs)
        OpenChild(New frmMemoPromotion())
    End Sub

    Private Sub btnMemoDrinkPriceApproval_Click(sender As Object, e As EventArgs) Handles btnMemoDrinkPriceApproval.Click
        OpenChild(New frmMemoDrinkPriceApproval())
    End Sub

    Private Sub btnMemoProductPriceApproval_Click(sender As Object, e As EventArgs) Handles btnMemoProductPriceApproval.Click
        OpenChild(New frmMemoProductPriceApproval())
    End Sub

    Private Sub btnMemoProductFormula_Click(sender As Object, e As EventArgs) Handles btnMemoProductFormula.Click
        If Not PermissionService.HasPermission("ProductDrink.View") Then
            MessageBox.Show("ผู้ใช้ปัจจุบันไม่มีสิทธิ์ดูรายการเครื่องดื่มและสูตร", "สิทธิ์ไม่เพียงพอ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        OpenChild(New frmBeverageProduct())
    End Sub
End Class

Friend NotInheritable Class BlueToolStripRenderer
    Inherits ToolStripProfessionalRenderer
    Public Sub New()
        MyBase.New(New BlueColorTable())
    End Sub
End Class

Friend NotInheritable Class BlueColorTable
    Inherits ProfessionalColorTable
    Public Overrides ReadOnly Property ToolStripGradientBegin As Color
        Get
            Return Color.FromArgb(30, 115, 190)
        End Get
    End Property
    Public Overrides ReadOnly Property ToolStripGradientMiddle As Color
        Get
            Return Color.FromArgb(27, 105, 175)
        End Get
    End Property
    Public Overrides ReadOnly Property ToolStripGradientEnd As Color
        Get
            Return Color.FromArgb(20, 88, 155)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
        Get
            Return Color.FromArgb(92, 170, 235)
        End Get
    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
        Get
            Return Color.FromArgb(45, 125, 205)
        End Get
    End Property
End Class
