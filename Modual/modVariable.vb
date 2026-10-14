
Option Explicit On  'ให้มีการประกาศตัวแปลก่อนใช้งาน
Option Strict On    'ให้เคร่งครัดต่อการเปลี่ยนแปลงชนิดข้อมูล
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration

Module modVariable
    Public Comm As SqlCommand               'ตัวแปลออบเจ็กต์ SqlCommand >>ใช้เก็บคำสั่งในการ เรียกดู เพิ่ม ลบ ปรับปรุง ข้อมูล
    Public clsSQL As clsSQLQuery            'สืบทอด Class clsSQLQuery
    Public sb As StringBuilder              'ตัวแปลออบเจ็กต์ StringBuilder >>ใช้เก็บข้อความที่มีขนาดยาวมาก
    Public dt As DataTable                  'ตัวแปลออบเจ็กต์ DataTable >>ใช้เก็บข้อมูลในรูปตารางข้อมูล
    Public dr As SqlDataReader              'ตัวแปลออบเจ็กต์ SqlDataReader >>ใช้สำหรับอ่านข้อมูลในตารางข้อมูล
    Public conStr As String
    Public sql As String
    Public Conns As SqlConnection            'ติดต่อฐานข้อมูลผ่าน strConn
    Public DbConnectionStringOverride As String = ""
    Public tr As SqlTransaction              'ตัวแปลออบเจ็กต์ SqlTransaction >>ใช้ควมคุมสถานะการบันทึก แก้ไขมูล แบบ Transaction
    'Public Function OpenConnection() As SqlConnection
    '    Dim cs As String =
    '"Server=localhost\SQLEXPRESS;" &
    '"Database=NewStock;" &
    '"User Id=sa;" &
    '"Password=GiftGift;" &
    '"TrustServerCertificate=True;"
    '    Dim conn As New SqlConnection(cs)
    '    conn.Open()
    '    Return conn
    'End Function

    'ไว้ใช้ที่ร้านกาแฟ
    'Public Function OpenConnection() As SqlConnection
    '    Dim cs As String =
    '   "Server=SMH0HMKL-POS01\SQLEXPRESS;" &
    '   "Database=NewStockTest;" &
    '   "Integrated Security =True;" &
    '    "TrustServerCertificate=True;"
    '    ' "User Id=sa;" &
    '    ' "Password=GiftGift;" &

    '    Dim conn As New SqlConnection(cs)
    '    conn.Open()
    '    Return conn
    'End Function
    Public Function OpenConnection() As SqlConnection
        Dim cs As String = If(String.IsNullOrWhiteSpace(DbConnectionStringOverride), ConfigurationManager.ConnectionStrings("DbConn").ConnectionString, DbConnectionStringOverride)

        Dim conn As New SqlConnection(cs)
        conn.Open()
        Return conn
    End Function
End Module

