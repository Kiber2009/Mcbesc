using System;
using System.Collections.Generic;
using Mcbesc.Errors;
using Mcbesc.Utils.Extensions;

namespace Mcbesc.Modules
{
    internal class HelpModule : INamedModule
    {
        private readonly Dictionary<string, IModule> _modules;

        internal HelpModule(Dictionary<string, IModule> modules)
        {
            _modules = modules;
        }

        public string Description => "Show help";

        public string[] Help => new[]
        {
            "Show list of modules",
            ">> build",
            "Show help about module",
            ">> build <module>"
        };

        public void Execute(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    foreach (KeyValuePair<string, IModule> moduleI in _modules)
                        Console.WriteLine($"{moduleI.Key} - {moduleI.Value.Description}");
                    break;
                case 1:
                    KeyValuePair<string, IModule> module;
                    try
                    {
                        module = _modules.GetPair(args[0].ToLower());
                    }
                    catch (KeyNotFoundException)
                    {
                        new NotFoundError(nameof(HelpModule), $"Module \"{args[0].ToLower()}\" not registered").Write();
                        return;
                    }

                    Console.WriteLine(module.Key);
                    Console.WriteLine(module.Value.Description);
                    Console.WriteLine();
                    foreach (string help in module.Value.Help) Console.WriteLine(help);
                    break;
                default:
                    new Error(Name, "Too many arguments").Write();
                    break;
            }
        }

        public string Name => "help";
    }
}