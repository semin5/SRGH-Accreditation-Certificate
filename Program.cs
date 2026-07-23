using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

try
{
    string basePath = AppDomain.CurrentDomain.BaseDirectory;

    string source = Path.Combine(basePath, "NPKI");

    string dest1 = @"C:\Program Files\NPKI";
    string dest2 = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        @"..\LocalLow\NPKI");

    DirectoryCopy(source, dest1, true);
    DirectoryCopy(source, Path.GetFullPath(dest2), true);

    MessageBox.Show(
        "공동인증서 갱신이 완료되었습니다.",
        "알림",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
}
catch (Exception ex)
{
    MessageBox.Show(
        ex.Message,
        "오류",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error);
}

static void DirectoryCopy(string sourceDir, string destDir, bool copySubDirs)
{
    DirectoryInfo dir = new DirectoryInfo(sourceDir);

    if (!dir.Exists)
        throw new DirectoryNotFoundException($"원본 폴더를 찾을 수 없습니다: {sourceDir}");

    DirectoryInfo[] dirs = dir.GetDirectories();

    Directory.CreateDirectory(destDir);

    foreach (FileInfo file in dir.GetFiles())
    {
        string targetFilePath = Path.Combine(destDir, file.Name);
        file.CopyTo(targetFilePath, true);
    }

    if (copySubDirs)
    {
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destDir, subDir.Name);
            DirectoryCopy(subDir.FullName, newDestinationDir, true);
        }
    }
}