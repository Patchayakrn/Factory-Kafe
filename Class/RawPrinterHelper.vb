Imports System.Runtime.InteropServices
Imports System.Text

Public Class RawPrinterHelper

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Class DOCINFOA
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDataType As String
    End Class

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", CharSet:=CharSet.Ansi, SetLastError:=True)>
    Public Shared Function OpenPrinter(szPrinter As String, ByRef hPrinter As IntPtr, pd As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True)>
    Public Shared Function ClosePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", CharSet:=CharSet.Ansi, SetLastError:=True)>
    Public Shared Function StartDocPrinter(hPrinter As IntPtr, level As Integer, di As DOCINFOA) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True)>
    Public Shared Function EndDocPrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True)>
    Public Shared Function StartPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True)>
    Public Shared Function EndPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True)>
    Public Shared Function WritePrinter(hPrinter As IntPtr, pBytes As Byte(), dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    Public Shared Function SendBytesToPrinter(printerName As String, bytes As Byte()) As Boolean
        Dim hPrinter As IntPtr = IntPtr.Zero
        Dim di As New DOCINFOA()
        di.pDocName = "Receipt"
        di.pDataType = "RAW"

        Dim written As Integer = 0

        If Not OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then
            MessageBox.Show("OpenPrinter error: " & Marshal.GetLastWin32Error().ToString())
            Return False
        End If

        If Not StartDocPrinter(hPrinter, 1, di) Then
            MessageBox.Show("StartDocPrinter error: " & Marshal.GetLastWin32Error().ToString())
            ClosePrinter(hPrinter)
            Return False
        End If

        If Not StartPagePrinter(hPrinter) Then
            MessageBox.Show("StartPagePrinter error: " & Marshal.GetLastWin32Error().ToString())
            EndDocPrinter(hPrinter)
            ClosePrinter(hPrinter)
            Return False
        End If

        Dim ok As Boolean = WritePrinter(hPrinter, bytes, bytes.Length, written)

        If Not ok Then
            MessageBox.Show("WritePrinter error: " & Marshal.GetLastWin32Error().ToString())
        End If

        EndPagePrinter(hPrinter)
        EndDocPrinter(hPrinter)
        ClosePrinter(hPrinter)

        Return ok
    End Function

    Public Shared Function SendStringToPrinter(printerName As String, text As String) As Boolean
        Dim bytes As Byte() = Encoding.GetEncoding(874).GetBytes(text)
        Return SendBytesToPrinter(printerName, bytes)
    End Function
End Class
