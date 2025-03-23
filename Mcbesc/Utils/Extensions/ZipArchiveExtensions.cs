using System.IO;
using System.IO.Compression;

namespace Mcbesc.Utils.Extensions
{
    internal static class ZipArchiveExtensions
    {
        internal static void AddDirectory(this ZipArchive self, string directoryPath, string basePath)
        {
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                string relativePath = Path.Combine(basePath, PathUtils.GetRelativePath(directoryPath, file));
                ZipArchiveEntry entry = self.CreateEntry(relativePath);
                using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                using (Stream entryStream = entry.Open()) fileStream.CopyTo(entryStream);
            }
            foreach (string subDirectory in Directory.GetDirectories(directoryPath))
            {
                string relativePath = Path.Combine(basePath, PathUtils.GetRelativePath(directoryPath, subDirectory));
                self.AddDirectory(subDirectory, relativePath);
            }
        }

        internal static void AddDirectory(this ZipArchive self, string directoryPath)
        {
            self.AddDirectory(directoryPath, "");
        }
    }
}
