using Mcbesc.Utils;
using Mcbesc.Utils.Extentions;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace Mcbesc
{
    internal static class Program
    {
        private const string DEFAULT_PATH = @".\mcbesc.json";

        private static void Main(string[] args)
        {
            string path = Path.GetFullPath(args.Length <= 0 ? DEFAULT_PATH : args[0]);
            if (!File.Exists(path))
            {
                new Error($"Project file \"{path}\" is not exists").Write();
                return;
            }
            ProjectFile projectFile;
            try
            {
                projectFile = JsonSerializer.Deserialize<ProjectFile>(File.ReadAllText(path));
            }
            catch (JsonException)
            {
                new Error($"Project file \"{path}\" has wrong JSON structure").Write();
                return;
            }
            Error[] errors = projectFile.Validate();
            if (errors.Length > 0)
            {
                foreach (Error error in projectFile.Validate()) error.Write();
                return;
            }
            projectFile.FullPath();
            bool isAddon = projectFile.Packs.Length > 1;
            if (Directory.Exists(projectFile.BuildDir)) Directory.Delete(projectFile.BuildDir, true);
            Directory.CreateDirectory(projectFile.BuildDir);
            string addonPath = Path.Combine(projectFile.BuildDir,
                $"{projectFile.BuildName}.{(isAddon ? "mcaddon" : "mcpack")}");
            if (isAddon) using (ZipArchive archive = ZipFile.Open(addonPath, ZipArchiveMode.Create))
                    foreach (string pack in projectFile.Packs) archive.AddDirectory(pack, "");
            else ZipFile.CreateFromDirectory(projectFile.Packs[0], addonPath);
            return;
        }
    }
}
