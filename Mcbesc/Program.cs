using Mcbesc.Data;
using System.IO;
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
                new Error(nameof(Program), $"Project file \"{path}\" is not exists").Write();
                return;
            }
            ProjectFile projectFile;
            try
            {
                projectFile = JsonSerializer.Deserialize<ProjectFile>(File.ReadAllText(path),
                    new JsonSerializerOptions
                    {
                        IncludeFields = true
                    });
            }
            catch (JsonException)
            {
                new Error(nameof(Program), $"Project file \"{path}\" has wrong JSON structure").Write();
                return;
            }
            Error[] errors = projectFile.Validate();
            if (errors.Length > 0)
            {
                foreach (Error error in projectFile.Validate()) error.Write();
                return;
            }
            projectFile.FullPath();
            if (Directory.Exists(projectFile.outputDir)) Directory.Delete(projectFile.outputDir, true);
            Directory.CreateDirectory(projectFile.outputDir);
            foreach (Addon addon in projectFile.addons) addon.Build(projectFile.outputDir);
        }
    }
}
