using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Mcbesc.Errors;
using Mcbesc.Modules;

namespace Mcbesc
{
    internal static class Program
    {
        internal static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true
        };

        private static readonly Dictionary<string, IModule> modules = new Dictionary<string, IModule>();

        private static void Main(string[] args)
        {
            RegisterModules();

            if (args.Length == 0) args = new[] { "help" };


            try
            {
                modules[args[0].ToLower()].Execute(args.Skip(1).ToArray());
            }
            catch (KeyNotFoundException)
            {
                new NotFoundError(nameof(Program), $"Module \"{args[0].ToLower()}\" not registered").Write();
            }
        }

        private static void RegisterModules()
        {
            RegisterModule(new BuildModule());
            RegisterModule(new HelpModule(modules));
        }

        private static void RegisterModule(string name, IModule module)
        {
            modules.Add(name, module);
        }

        private static void RegisterModule(INamedModule module)
        {
            RegisterModule(module.Name.ToLower(), module);
        }
    }
}