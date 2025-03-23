using System.IO;
using System.Text.Json;
using Mcbesc.Data;
using Mcbesc.Errors;

namespace Mcbesc.Modules
{
    internal class BuildModule : INamedModule
    {
        private const string DEFAULT_PATH = @".\mcbesc.json";

        public string Description => "Build project";

        public string[] Help => new[]
        {
            "Build project",
            ">> build [path]",
            $"Default path is \"{DEFAULT_PATH}\""
        };

        public void Execute(string[] args)
        {
            string path = Path.GetFullPath(args.Length <= 0 ? DEFAULT_PATH : args[0]);
            if (!File.Exists(path))
            {
                new Error(nameof(Program), $"Project file \"{path}\" not exists").Write();
                return;
            }

            ProjectFile projectFile;
            try
            {
                projectFile = JsonSerializer.Deserialize<ProjectFile>(
                    File.ReadAllText(path),
                    Program.jsonSerializerOptions
                );
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

        public string Name => "build";
    }
}