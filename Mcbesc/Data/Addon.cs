using Mcbesc.Utils.Extentions;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Mcbesc.Data
{
    internal sealed class Addon
    {
        public string name = null;
        public Pack[] packs = null;

        internal bool IsAddon => packs.Length > 1;
        internal string Filename => $"{name}.{(IsAddon ? "mcaddon" : "mcpack")}";

        internal void FullPath()
        {
            foreach (Pack pack in packs) pack.FullPath();
        }

        internal Error[] Validate()
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add(new Error(nameof(Addon), $"{nameof(name)} is null or empty"));
            else if (name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                errors.Add(new Error(nameof(Addon), $"{nameof(name)} is invalid"));

            if (packs == null)
                errors.Add(new Error(nameof(Addon), $"{nameof(packs)} is null"));
            else if (packs.Length <= 0)
                errors.Add(new Error(nameof(Addon), $"{nameof(packs)} is empty"));
            else if (packs.ContainsNull())
                errors.Add(new Error(nameof(Addon), $"{nameof(packs)} contains null"));
            else foreach (Pack pack in packs)
                    errors.AddRange(pack.Validate());

            return errors.ToArray();
        }

        internal void Build(string output)
        {
            string addonPath = Path.Combine(output, Filename);
            if (IsAddon) using (ZipArchive archive = ZipFile.Open(addonPath, ZipArchiveMode.Create))
                    foreach (Pack pack in packs) archive.AddDirectory(pack.path, "");
            else ZipFile.CreateFromDirectory(packs[0].path, addonPath);
        }
    }
}
