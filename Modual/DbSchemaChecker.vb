Option Explicit On
Option Strict On

Imports System.Data.SqlClient
Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq

Public NotInheritable Class DbSchemaChecker
    Private Sub New()
    End Sub

    'รายการ Table ที่โปรแกรม Coffee ใช้งาน
    Private Shared ReadOnly RequiredTables As String() = {
        "Branch", "Brand", "Buy", "BuyD", "BuyStock", "Category", "CNCustomer", "CNSupplier",
        "Company", "Customer", "CustomerFreeCup", "Delivery", "Officer", "PMT", "PMTBranch", "PMTD",
        "Product", "ProductAlert", "ProductGroup", "ProductLocation", "ProductVolume", "Sell", "CustomerCertification",
        "SellCustomerCertified", "SellD", "SellPromotion", "SellStock", "Stock", "StockMovementReportOverride", "Supplier",
        "MaterialUsageDocument", "MaterialUsageDocumentDetail", "StockCheck", "Transaction", "TransactionDetail", "Use_Qty",
        "SupplierContact", "Unit"
    }

    'คอลัมน์ของตารางส่วนขยายและคอลัมน์ที่ฟังก์ชันใหม่ใช้งาน
    Private Shared ReadOnly RequiredColumns As New Dictionary(Of String, String()) From {
        {"Officer", New String() {"Salary"}},
        {"SellCustomerCertified", New String() {"BranchNumber", "SellNumber", "CustomerNumber", "CustomerName", "PayName", "Detail", "CONumber", "CTime"}},
        {"CustomerFreeCup", New String() {"CustomerNumber", "ID", "SellNumber", "SellBranchNumber", "CupIn", "FreeCupIn", "FreeCupOut", "FreeCupAmount", "CupBalance", "FreeCupBalance", "Detail", "CTime"}},
        {"CustomerCertification", New String() {"CustomerNumber", "IsCertified", "CTime", "UTime"}},
        {"SellPromotion", New String() {"BranchNumber", "SellNumber", "Detail", "CTime"}},
        {"StockMovementReportOverride", New String() {"AdjustmentID", "BranchNumber", "PN", "AdjustmentDate", "MovementType", "Action", "SourceDocumentNumber", "ReportDocNumber", "ReportBillNumber", "NumIn", "NumOut", "Detail", "IsActive", "CTime"}}
    }

    'ตารางส่วนขยายที่รุ่นเก่าอาจยังไม่มี ให้สร้างโดยใช้โครงสร้างเดียวกับหน้าขาย
    Public Shared Sub EnsureSupportTables()
        Using conn As SqlConnection = modVariable.OpenConnection()
            Dim sql As String =
                "IF OBJECT_ID(N'dbo.SellCustomerCertified', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.SellCustomerCertified (" &
                "BranchNumber INT NOT NULL, SellNumber INT NOT NULL, " &
                "CustomerNumber INT NULL, CustomerName NVARCHAR(200) NULL, " &
                "PayName NVARCHAR(100) NULL, Detail NVARCHAR(500) NULL, " &
                "CONumber INT NULL, CTime DATETIME NOT NULL CONSTRAINT DF_SellCustomerCertified_CTime DEFAULT GETDATE(), " &
                "CONSTRAINT PK_SellCustomerCertified PRIMARY KEY (BranchNumber, SellNumber)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.CustomerFreeCup', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.CustomerFreeCup (" &
                "CustomerNumber INT NOT NULL, ID INT NOT NULL, SellNumber INT NULL, " &
                "SellBranchNumber INT NULL, CupIn DECIMAL(18,2) NOT NULL CONSTRAINT DF_CustomerFreeCup_CupIn DEFAULT 0, " &
                "FreeCupIn INT NOT NULL CONSTRAINT DF_CustomerFreeCup_FreeCupIn DEFAULT 0, " &
                "FreeCupOut INT NOT NULL CONSTRAINT DF_CustomerFreeCup_FreeCupOut DEFAULT 0, " &
                "CupBalance DECIMAL(18,2) NOT NULL CONSTRAINT DF_CustomerFreeCup_CupBalance DEFAULT 0, " &
                "FreeCupBalance INT NOT NULL CONSTRAINT DF_CustomerFreeCup_FreeCupBalance DEFAULT 0, " &
                "Detail NVARCHAR(500) NULL, CTime DATETIME NOT NULL CONSTRAINT DF_CustomerFreeCup_CTime DEFAULT GETDATE(), " &
                "CONSTRAINT PK_CustomerFreeCup PRIMARY KEY (CustomerNumber, ID)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.CustomerCertification', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.CustomerCertification (" &
                "CustomerNumber INT NOT NULL, " &
                "IsCertified BIT NOT NULL CONSTRAINT DF_CustomerCertification_IsCertified DEFAULT 0, " &
                "CTime DATETIME NOT NULL CONSTRAINT DF_CustomerCertification_CTime DEFAULT GETDATE(), " &
                "UTime DATETIME NULL, " &
                "CONSTRAINT PK_CustomerCertification PRIMARY KEY (CustomerNumber)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.Stock', N'U') IS NOT NULL " &
                "BEGIN " &
                "IF COL_LENGTH(N'dbo.Stock', N'NumberNS') IS NOT NULL " &
                "AND NOT EXISTS (SELECT 1 FROM sys.default_constraints dc INNER JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id WHERE dc.parent_object_id = OBJECT_ID(N'dbo.Stock') AND c.name = N'NumberNS') " &
                "ALTER TABLE dbo.Stock ADD CONSTRAINT DF_Stock_NumberNS_Auto DEFAULT (0) FOR NumberNS; " &
                "IF COL_LENGTH(N'dbo.Stock', N'NumberS') IS NOT NULL " &
                "AND NOT EXISTS (SELECT 1 FROM sys.default_constraints dc INNER JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id WHERE dc.parent_object_id = OBJECT_ID(N'dbo.Stock') AND c.name = N'NumberS') " &
                "ALTER TABLE dbo.Stock ADD CONSTRAINT DF_Stock_NumberS_Auto DEFAULT (0) FOR NumberS; " &
                "END; " &
                "IF OBJECT_ID(N'dbo.SellPromotion', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.SellPromotion (" &
                "BranchNumber INT NOT NULL, SellNumber INT NOT NULL, Detail NVARCHAR(500) NULL, " &
                "CTime DATETIME NOT NULL CONSTRAINT DF_SellPromotion_CTime DEFAULT GETDATE(), " &
                "CONSTRAINT PK_SellPromotion PRIMARY KEY (BranchNumber, SellNumber)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.StockMovementReportOverride', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.StockMovementReportOverride (" &
                "AdjustmentID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_StockMovementReportOverride PRIMARY KEY, " &
                "BranchNumber INT NOT NULL, PN BIGINT NOT NULL, AdjustmentDate DATETIME NOT NULL, " &
                "MovementType NVARCHAR(20) NOT NULL, Action NVARCHAR(20) NOT NULL, " &
                "SourceDocumentNumber NVARCHAR(50) NULL, ReportDocNumber NVARCHAR(50) NOT NULL, " &
                "ReportBillNumber NVARCHAR(100) NULL, NumIn DECIMAL(18,4) NOT NULL CONSTRAINT DF_StockMovementReportOverride_NumIn DEFAULT 0, " &
                "NumOut DECIMAL(18,4) NOT NULL CONSTRAINT DF_StockMovementReportOverride_NumOut DEFAULT 0, " &
                "Detail NVARCHAR(500) NULL, IsActive BIT NOT NULL CONSTRAINT DF_StockMovementReportOverride_IsActive DEFAULT 1, " &
                "CTime DATETIME NOT NULL CONSTRAINT DF_StockMovementReportOverride_CTime DEFAULT GETDATE()) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.SellPromotion', N'U') IS NOT NULL " &
                "AND COL_LENGTH(N'dbo.SellPromotion', N'CTime') IS NULL " &
                "ALTER TABLE dbo.SellPromotion ADD CTime DATETIME NOT NULL CONSTRAINT DF_SellPromotion_CTime_Auto DEFAULT GETDATE(); " &
                "IF OBJECT_ID(N'dbo.CustomerFreeCup', N'U') IS NOT NULL " &
                "AND COL_LENGTH(N'dbo.CustomerFreeCup', N'FreeCupAmount') IS NULL " &
                "ALTER TABLE dbo.CustomerFreeCup ADD FreeCupAmount DECIMAL(18,2) NOT NULL CONSTRAINT DF_CustomerFreeCup_FreeCupAmount_Auto DEFAULT 0; " &
                "IF OBJECT_ID(N'dbo.MaterialUsageDocument', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.MaterialUsageDocument (" &
                "UsageDocumentID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_MaterialUsageDocument PRIMARY KEY, " &
                "BranchNumber INT NOT NULL, UsageYear SMALLINT NOT NULL, UsageMonth TINYINT NOT NULL, " &
                "DocNo NVARCHAR(50) NOT NULL, UsageDate DATETIME NOT NULL, SourceType NVARCHAR(20) NOT NULL, " &
                "Status INT NULL, CTime DATETIME NOT NULL CONSTRAINT DF_MaterialUsageDocument_CTime DEFAULT GETDATE(), " &
                "UTime DATETIME NULL, CONSTRAINT UQ_MaterialUsageDocument_Period UNIQUE (BranchNumber, UsageYear, UsageMonth)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.MaterialUsageDocumentDetail', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.MaterialUsageDocumentDetail (" &
                "UsageDocumentID INT NOT NULL, MaterialID NVARCHAR(50) NOT NULL, Qty DECIMAL(18,4) NOT NULL, " &
                "UnitUse NVARCHAR(50) NULL, CTime DATETIME NOT NULL CONSTRAINT DF_MaterialUsageDocumentDetail_CTime DEFAULT GETDATE(), " &
                "CONSTRAINT PK_MaterialUsageDocumentDetail PRIMARY KEY (UsageDocumentID, MaterialID)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.StockCheck', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.StockCheck (" &
                "[Year] SMALLINT NOT NULL, [Month] TINYINT NOT NULL, MaterialID NVARCHAR(50) NOT NULL, " &
                "Rest_Qty DECIMAL(18,4) NOT NULL CONSTRAINT DF_StockCheck_Rest_Qty DEFAULT 0, " &
                "CONSTRAINT PK_StockCheck PRIMARY KEY ([Year], [Month], MaterialID)) " &
                "CREATE INDEX IX_StockCheck_Material_Period ON dbo.StockCheck(MaterialID, [Year], [Month]); " &
                "END; " &
                "IF OBJECT_ID(N'dbo.Transaction', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.[Transaction] (" &
                "DocNo NVARCHAR(50) NOT NULL CONSTRAINT PK_Transaction PRIMARY KEY, " &
                "[Date] DATETIME2(0) NOT NULL, DocType INT NOT NULL, " &
                "Purpose_Receive INT NULL, Purpose_Pay INT NULL, Remark NVARCHAR(500) NULL, " &
                "SupplierID NVARCHAR(50) NULL, PR_No NVARCHAR(50) NULL, PO_No NVARCHAR(50) NULL, " &
                "CONSTRAINT CK_Transaction_DocType CHECK (DocType IN (1,2))) " &
                "CREATE INDEX IX_Transaction_Date ON dbo.[Transaction]([Date]); " &
                "CREATE INDEX IX_Transaction_PurposePay ON dbo.[Transaction](Purpose_Pay, [Date]); " &
                "END; " &
                "IF OBJECT_ID(N'dbo.TransactionDetail', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.TransactionDetail (" &
                "DocNo NVARCHAR(50) NOT NULL, MaterialID NVARCHAR(50) NOT NULL, " &
                "Rec_Qty_Pack DECIMAL(18,4) NOT NULL CONSTRAINT DF_TransactionDetail_Rec_Qty_Pack DEFAULT 0, " &
                "Rec_Qty_Use_Unit DECIMAL(18,4) NOT NULL CONSTRAINT DF_TransactionDetail_Rec_Qty_Use_Unit DEFAULT 0, " &
                "Pay_Qty_Pack DECIMAL(18,4) NOT NULL CONSTRAINT DF_TransactionDetail_Pay_Qty_Pack DEFAULT 0, " &
                "Pay_Qty_Use_Unit DECIMAL(18,4) NOT NULL CONSTRAINT DF_TransactionDetail_Pay_Qty_Use_Unit DEFAULT 0, " &
                "CONSTRAINT PK_TransactionDetail PRIMARY KEY (DocNo, MaterialID), " &
                "CONSTRAINT FK_TransactionDetail_Transaction FOREIGN KEY (DocNo) REFERENCES dbo.[Transaction](DocNo)) " &
                "CREATE INDEX IX_TransactionDetail_Material ON dbo.TransactionDetail(MaterialID, DocNo); " &
                "END; " &
                "IF OBJECT_ID(N'dbo.Use_Qty', N'U') IS NULL " &
                "BEGIN " &
                "CREATE TABLE dbo.Use_Qty (" &
                "[Year] SMALLINT NOT NULL, [Month] TINYINT NOT NULL, MaterialID NVARCHAR(50) NOT NULL, " &
                "Use_Qty DECIMAL(18,4) NOT NULL CONSTRAINT DF_Use_Qty_Use_Qty DEFAULT 0, " &
                "CTime DATETIME NOT NULL CONSTRAINT DF_Use_Qty_CTime DEFAULT GETDATE(), " &
                "UTime DATETIME NULL, CONSTRAINT PK_Use_Qty PRIMARY KEY ([Year], [Month], MaterialID)) " &
                "END; " &
                "IF OBJECT_ID(N'dbo.Officer', N'U') IS NOT NULL " &
                "AND COL_LENGTH(N'dbo.Officer', N'Salary') IS NULL " &
                "ALTER TABLE dbo.Officer ADD Salary MONEY NOT NULL CONSTRAINT DF_Officer_Salary_Auto DEFAULT 0;"

            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Function FindMissingTables() As List(Of String)
        Dim missing As New List(Of String)()

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES " &
                "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'dbo'", conn)

                Dim existing As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        existing.Add(reader.GetString(0))
                    End While
                End Using

                For Each tableName As String In RequiredTables
                    If Not existing.Contains(tableName) Then
                        missing.Add(tableName)
                    End If
                Next
            End Using
        End Using

        Return missing
    End Function

    Public Shared Function FindMissingColumns() As List(Of String)
        Dim missing As New List(Of String)()
        Dim existing As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        Using conn As SqlConnection = modVariable.OpenConnection()
            Using cmd As New SqlCommand(
                "SELECT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'dbo'", conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        existing.Add(reader.GetString(0).Trim() & "." & reader.GetString(1).Trim())
                    End While
                End Using
            End Using
        End Using

        For Each tableEntry In RequiredColumns
            For Each columnName As String In tableEntry.Value
                Dim key As String = tableEntry.Key & "." & columnName
                If Not existing.Contains(key) Then missing.Add(key)
            Next
        Next

        Return missing
    End Function

    Public Shared Function BuildMissingTablesMessage(missingTables As IEnumerable(Of String),
                                                     Optional missingColumns As IEnumerable(Of String) = Nothing) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("ฐานข้อมูลเครื่องนี้มีโครงสร้างไม่ครบตามที่โปรแกรมต้องใช้")
        sb.AppendLine()
        If missingTables IsNot Nothing AndAlso missingTables.Any() Then
            sb.AppendLine("Table ที่ขาด:")
            For Each tableName As String In missingTables
                sb.AppendLine("- " & tableName)
            Next
            sb.AppendLine()
        End If
        If missingColumns IsNot Nothing AndAlso missingColumns.Any() Then
            sb.AppendLine("Column ที่ขาด:")
            For Each columnName As String In missingColumns
                sb.AppendLine("- " & columnName)
            Next
            sb.AppendLine()
        End If
        sb.AppendLine()
        sb.AppendLine("กรุณาอัปเดตโครงสร้างฐานข้อมูลก่อนใช้งาน")
        Return sb.ToString()
    End Function
End Class
