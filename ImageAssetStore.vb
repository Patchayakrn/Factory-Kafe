Imports System.IO
Imports System.IO.Compression
Imports System.Reflection

Public NotInheritable Class ImageAssetStore
    Private Sub New()
    End Sub

    Private Shared ReadOnly AssetFolder As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "The Factory Kafe", "Assets")

    Public Shared Function GetMenuRootPath() As String
        EnsureAssets()
        Return Path.Combine(AssetFolder, "Menu")
    End Function

    Public Shared Function GetCustomerAdsPath() As String
        EnsureAssets()
        Return Path.Combine(AssetFolder, "CustomerAds")
    End Function

    Public Shared Function SaveMenuImage(sourcePath As String, groupName As String) As String
        If String.IsNullOrWhiteSpace(sourcePath) OrElse Not File.Exists(sourcePath) Then Return ""

        Dim fileName As String = Path.GetFileName(sourcePath)
        If String.IsNullOrWhiteSpace(fileName) Then Return ""

        EnsureAssets()
        Dim safeGroupName As String = If(groupName, "").Trim()
        If String.IsNullOrWhiteSpace(safeGroupName) Then safeGroupName = "อื่นๆ"
        safeGroupName = safeGroupName.Replace(Path.DirectorySeparatorChar, "_"c).Replace(Path.AltDirectorySeparatorChar, "_"c)

        Dim targetFolder As String = Path.Combine(AssetFolder, "Menu", safeGroupName)
        Directory.CreateDirectory(targetFolder)
        Dim targetPath As String = Path.Combine(targetFolder, fileName)

        If Not String.Equals(Path.GetFullPath(sourcePath), Path.GetFullPath(targetPath), StringComparison.OrdinalIgnoreCase) Then
            File.Copy(sourcePath, targetPath, True)
        End If

        Return targetPath
    End Function

    Public Shared Sub EnsureAssets()
        Dim menuPath As String = Path.Combine(AssetFolder, "Menu")
        Dim adsPath As String = Path.Combine(AssetFolder, "CustomerAds")
        If Directory.Exists(menuPath) AndAlso Directory.Exists(adsPath) Then Return

        Try
            Directory.CreateDirectory(AssetFolder)
            Dim zipPath As String = Path.Combine(Application.StartupPath, "Coffee_SMH2_Assets.zip")
            If File.Exists(zipPath) Then
                Using archive As ZipArchive = ZipFile.OpenRead(zipPath)
                    ExtractArchive(archive)
                End Using
            Else
                Dim resourceName As String = Assembly.GetExecutingAssembly().GetManifestResourceNames().
                    FirstOrDefault(Function(n) n.EndsWith("Coffee_SMH2_Assets.zip", StringComparison.OrdinalIgnoreCase))
                If String.IsNullOrWhiteSpace(resourceName) Then Return
                Using resourceStream As Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
                    Using archive As New ZipArchive(resourceStream, ZipArchiveMode.Read)
                        ExtractArchive(archive)
                    End Using
                End Using
            End If
        Catch
        End Try
    End Sub

    Private Shared Sub ExtractArchive(archive As ZipArchive)
        For Each entry As ZipArchiveEntry In archive.Entries
            Dim destination As String = Path.GetFullPath(Path.Combine(AssetFolder, entry.FullName))
            If Not destination.StartsWith(Path.GetFullPath(AssetFolder), StringComparison.OrdinalIgnoreCase) Then Continue For
            If String.IsNullOrWhiteSpace(entry.Name) Then
                Directory.CreateDirectory(destination)
            Else
                Directory.CreateDirectory(Path.GetDirectoryName(destination))
                Using input As Stream = entry.Open()
                    Using output As New FileStream(destination, FileMode.Create, FileAccess.Write)
                        input.CopyTo(output)
                    End Using
                End Using
            End If
        Next
    End Sub
End Class
