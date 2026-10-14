Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Module PermissionService
    Private schemaReady As Boolean
    Private ReadOnly schemaLock As New Object()

    Private Class PermissionSeed
        Public ReadOnly Key As String
        Public ReadOnly Name As String
        Public ReadOnly ModuleName As String

        Public Sub New(keyIn As String, nameIn As String, moduleNameIn As String)
            Key = keyIn
            Name = nameIn
            ModuleName = moduleNameIn
        End Sub
    End Class

    Public Sub EnsurePermissionSchema()
        If schemaReady Then Return

        SyncLock schemaLock
            If schemaReady Then Return

            Using conn As SqlConnection = modVariable.OpenConnection()
                CreateTables(conn)
                For Each item As PermissionSeed In PermissionSeeds()
                    Using cmd As New SqlCommand("IF NOT EXISTS (SELECT 1 FROM AppPermission WHERE PermissionKey=@Key) INSERT INTO AppPermission(PermissionKey,PermissionName,ModuleName,IsActive) VALUES(@Key,@Name,@Module,1)", conn)
                        cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 100).Value = item.Key
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = item.Name
                        cmd.Parameters.Add("@Module", SqlDbType.NVarChar, 100).Value = item.ModuleName
                        cmd.ExecuteNonQuery()
                    End Using
                Next

                MigrateLegacyPermissions(conn)
                EnsureDefaultAdminPermissions(conn)
                EnsureDefaultBuyEditor(conn)
                EnsureDefaultSellEditor(conn)
            End Using

            schemaReady = True
        End SyncLock
    End Sub

    Public Function HasPermission(permissionKey As String) As Boolean
        If String.IsNullOrWhiteSpace(permissionKey) OrElse ModSession.LoginOfficerID <= 0 Then Return False

        Try
            EnsurePermissionSchema()
            Using conn As SqlConnection = modVariable.OpenConnection()
                Const sql As String =
                    "SELECT TOP 1 AllowValue FROM (" &
                    "SELECT 1 AS Priority, AllowValue FROM AppOfficerPermission op INNER JOIN AppPermission p ON p.PermissionID=op.PermissionID " &
                    "WHERE op.OfficerNumber=@Officer AND p.PermissionKey=@Key AND p.IsActive=1 " &
                    "UNION ALL " &
                    "SELECT 2 AS Priority, rp.AllowValue FROM AppOfficerRole ore INNER JOIN AppRolePermission rp ON rp.RoleID=ore.RoleID " &
                    "INNER JOIN AppPermission p ON p.PermissionID=rp.PermissionID " &
                    "WHERE ore.OfficerNumber=@Officer AND p.PermissionKey=@Key AND p.IsActive=1" &
                    ") AS PermissionValues ORDER BY Priority"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.Add("@Officer", SqlDbType.Int).Value = CInt(ModSession.LoginOfficerID)
                    cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 100).Value = permissionKey.Trim()
                    Dim result As Object = cmd.ExecuteScalar()
                    Return result IsNot Nothing AndAlso result IsNot DBNull.Value AndAlso Convert.ToInt32(result) <> 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Sub CreateTables(conn As SqlConnection)
        Const sql As String =
            "IF OBJECT_ID(N'dbo.AppPermission',N'U') IS NULL " &
            "BEGIN CREATE TABLE dbo.AppPermission(" &
            "PermissionID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AppPermission PRIMARY KEY," &
            "PermissionKey NVARCHAR(100) NOT NULL CONSTRAINT UQ_AppPermission_Key UNIQUE," &
            "PermissionName NVARCHAR(200) NOT NULL,ModuleName NVARCHAR(100) NOT NULL," &
            "IsActive BIT NOT NULL CONSTRAINT DF_AppPermission_IsActive DEFAULT(1)) END;" &
            "IF OBJECT_ID(N'dbo.AppRole',N'U') IS NULL " &
            "BEGIN CREATE TABLE dbo.AppRole(" &
            "RoleID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AppRole PRIMARY KEY," &
            "RoleCode NVARCHAR(50) NOT NULL CONSTRAINT UQ_AppRole_Code UNIQUE," &
            "RoleName NVARCHAR(200) NOT NULL,IsActive BIT NOT NULL CONSTRAINT DF_AppRole_IsActive DEFAULT(1)) END;" &
            "IF OBJECT_ID(N'dbo.AppRolePermission',N'U') IS NULL " &
            "BEGIN CREATE TABLE dbo.AppRolePermission(" &
            "RoleID INT NOT NULL,PermissionID INT NOT NULL,AllowValue BIT NOT NULL CONSTRAINT DF_AppRolePermission_Allow DEFAULT(1)," &
            "CONSTRAINT PK_AppRolePermission PRIMARY KEY(RoleID,PermissionID)) END;" &
            "IF OBJECT_ID(N'dbo.AppOfficerRole',N'U') IS NULL " &
            "BEGIN CREATE TABLE dbo.AppOfficerRole(" &
            "OfficerNumber INT NOT NULL,RoleID INT NOT NULL,CONSTRAINT PK_AppOfficerRole PRIMARY KEY(OfficerNumber,RoleID)) END;" &
            "IF OBJECT_ID(N'dbo.AppOfficerPermission',N'U') IS NULL " &
            "BEGIN CREATE TABLE dbo.AppOfficerPermission(" &
            "OfficerNumber INT NOT NULL,PermissionID INT NOT NULL,AllowValue BIT NOT NULL," &
            "CONSTRAINT PK_AppOfficerPermission PRIMARY KEY(OfficerNumber,PermissionID)) END;"
        Using cmd As New SqlCommand(sql, conn)
            cmd.CommandTimeout = 30
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function PermissionSeeds() As List(Of PermissionSeed)
        Return New List(Of PermissionSeed) From {
            New PermissionSeed("Admin.ManagePermissions", "กำหนดสิทธิ์การใช้งาน", "Admin"),
            New PermissionSeed("Admin.ManagePassword", "กำหนด User/Password", "Admin"),
            New PermissionSeed("Officer.View", "ดูข้อมูลพนักงาน", "Officer"),
            New PermissionSeed("Officer.Add", "เพิ่มพนักงาน", "Officer"),
            New PermissionSeed("Officer.Edit", "แก้ไขพนักงาน", "Officer"),
            New PermissionSeed("Product.View", "ดูสินค้า", "Product"),
            New PermissionSeed("Product.Add", "เพิ่มสินค้า", "Product"),
            New PermissionSeed("Product.Edit", "แก้ไขสินค้า", "Product"),
            New PermissionSeed("Product.Cancel", "ยกเลิกสินค้า", "Product"),
            New PermissionSeed("ProductDrink.View", "ดูรายการเครื่องดื่มและสูตร", "Product"),
            New PermissionSeed("ProductDrink.Add", "เพิ่มรายการเครื่องดื่ม", "Product"),
            New PermissionSeed("MaterialDrink.Add", "เพิ่มวัตถุดิบเครื่องดื่ม", "Stock"),
            New PermissionSeed("Buy.View", "ดูรายการซื้อ", "Buy"),
            New PermissionSeed("Buy.Add", "เพิ่มใบซื้อ", "Buy"),
            New PermissionSeed("Buy.Edit", "แก้ไขใบซื้อ", "Buy"),
            New PermissionSeed("Buy.Cancel", "ยกเลิกใบซื้อ", "Buy"),
            New PermissionSeed("Buy.Print", "พิมพ์ใบซื้อ", "Buy"),
            New PermissionSeed("Stock.View", "ดูสต๊อก", "Stock"),
            New PermissionSeed("Stock.In", "รับสินค้าเข้าสต๊อก", "Stock"),
            New PermissionSeed("Stock.Out", "เบิกสินค้าออก", "Stock"),
            New PermissionSeed("Sell.View", "ดูรายการขาย", "Sell"),
            New PermissionSeed("Sell.Add", "เพิ่มรายการขาย", "Sell"),
            New PermissionSeed("Sell.Edit", "แก้ไขบิลขาย", "Sell"),
            New PermissionSeed("Sell.Cancel", "ยกเลิกบิลขาย", "Sell"),
            New PermissionSeed("Sell.DeleteItem", "ลบรายการขาย", "Sell"),
            New PermissionSeed("Report.View", "ดูรายงาน", "Report"),
            New PermissionSeed("Report.Print", "พิมพ์รายงาน", "Report")
        }
    End Function

    Private Sub MigrateLegacyPermissions(conn As SqlConnection)
        Dim mappings As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
            {"ManageAccessLevel", "Admin.ManagePermissions"},
            {"ManageUserNamePassword", "Admin.ManagePassword"},
            {"Officer", "Officer.View"},
            {"AddOfficer", "Officer.Add"},
            {"EditCancelOfficer", "Officer.Edit"},
            {"Product", "Product.View"},
            {"AddProduct", "Product.Add"},
            {"EditProduct", "Product.Edit"},
            {"CancelProduct", "Product.Cancel"},
            {"Buy", "Buy.View"},
            {"BuyDetail", "Buy.View"},
            {"EditBuy", "Buy.Edit"},
            {"CancelBuy", "Buy.Cancel"},
            {"Stock", "Stock.View"},
            {"StockIn", "Stock.In"},
            {"StockOut", "Stock.Out"},
            {"Sell", "Sell.View"},
            {"SellDetail", "Sell.View"},
            {"SellUseEditButton", "Sell.Edit"},
            {"SellUseDeleteButton", "Sell.DeleteItem"},
            {"ReceiveMoneyFromSell", "Sell.Add"}
        }

        For Each item As KeyValuePair(Of String, String) In mappings
            Const sql As String =
                "IF OBJECT_ID(N'dbo.AccessLevel',N'U') IS NOT NULL " &
                "BEGIN INSERT INTO AppOfficerPermission(OfficerNumber,PermissionID,AllowValue) " &
                "SELECT DISTINCT al.OfficerNumber,p.PermissionID,CASE WHEN ISNULL(al.AccessValue,0)<>0 THEN 1 ELSE 0 END " &
                "FROM AccessLevel al INNER JOIN AppPermission p ON p.PermissionKey=@NewKey " &
                "WHERE al.AccessKey=@OldKey AND al.Status IS NULL " &
                "AND NOT EXISTS(SELECT 1 FROM AppOfficerPermission x WHERE x.OfficerNumber=al.OfficerNumber AND x.PermissionID=p.PermissionID) END"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.Add("@NewKey", SqlDbType.NVarChar, 100).Value = item.Value
                cmd.Parameters.Add("@OldKey", SqlDbType.NVarChar, 100).Value = item.Key
                cmd.ExecuteNonQuery()
            End Using
        Next
    End Sub

    Private Sub EnsureDefaultBuyEditor(conn As SqlConnection)
        Const sql As String =
            "IF OBJECT_ID(N'dbo.Officer',N'U') IS NOT NULL " &
            "BEGIN UPDATE op SET AllowValue=1 FROM AppOfficerPermission op " &
            "INNER JOIN AppPermission p ON p.PermissionID=op.PermissionID " &
            "INNER JOIN Officer o ON o.OfficerNumber=op.OfficerNumber " &
            "WHERE RTRIM(o.OfficerCode)=N'A0922' AND p.PermissionKey=N'Buy.Edit'; " &
            "INSERT INTO AppOfficerPermission(OfficerNumber,PermissionID,AllowValue) " &
            "SELECT o.OfficerNumber,p.PermissionID,1 FROM Officer o CROSS JOIN AppPermission p " &
            "WHERE RTRIM(o.OfficerCode)=N'A0922' AND p.PermissionKey=N'Buy.Edit' " &
            "AND NOT EXISTS(SELECT 1 FROM AppOfficerPermission x WHERE x.OfficerNumber=o.OfficerNumber AND x.PermissionID=p.PermissionID) END"
        Using cmd As New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub EnsureDefaultAdminPermissions(conn As SqlConnection)
        Const sql As String =
            "IF OBJECT_ID(N'dbo.Officer',N'U') IS NOT NULL " &
            "BEGIN UPDATE op SET AllowValue=1 FROM AppOfficerPermission op " &
            "INNER JOIN AppPermission p ON p.PermissionID=op.PermissionID " &
            "INNER JOIN Officer o ON o.OfficerNumber=op.OfficerNumber " &
            "WHERE (o.OfficerNumber=1 OR RTRIM(o.OfficerCode)=N'A0922'); " &
            "INSERT INTO AppOfficerPermission(OfficerNumber,PermissionID,AllowValue) " &
            "SELECT o.OfficerNumber,p.PermissionID,1 FROM Officer o CROSS JOIN AppPermission p " &
            "WHERE (o.OfficerNumber=1 OR RTRIM(o.OfficerCode)=N'A0922') " &
            "AND NOT EXISTS(SELECT 1 FROM AppOfficerPermission x WHERE x.OfficerNumber=o.OfficerNumber AND x.PermissionID=p.PermissionID) END"
        Using cmd As New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub EnsureDefaultSellEditor(conn As SqlConnection)
        Const sql As String =
            "IF OBJECT_ID(N'dbo.Officer',N'U') IS NOT NULL " &
            "BEGIN UPDATE op SET AllowValue=1 FROM AppOfficerPermission op " &
            "INNER JOIN AppPermission p ON p.PermissionID=op.PermissionID " &
            "INNER JOIN Officer o ON o.OfficerNumber=op.OfficerNumber " &
            "WHERE RTRIM(o.OfficerCode)=N'A0922' AND p.PermissionKey IN (N'Sell.Edit',N'Sell.Cancel'); " &
            "INSERT INTO AppOfficerPermission(OfficerNumber,PermissionID,AllowValue) " &
            "SELECT o.OfficerNumber,p.PermissionID,1 FROM Officer o CROSS JOIN AppPermission p " &
            "WHERE RTRIM(o.OfficerCode)=N'A0922' AND p.PermissionKey IN (N'Sell.Edit',N'Sell.Cancel') " &
            "AND NOT EXISTS(SELECT 1 FROM AppOfficerPermission x WHERE x.OfficerNumber=o.OfficerNumber AND x.PermissionID=p.PermissionID) END"
        Using cmd As New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
End Module
