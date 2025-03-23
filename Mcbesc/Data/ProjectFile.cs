using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mcbesc.Errors;
using Mcbesc.Utils.Extensions;

namespace Mcbesc.Data
{
    internal sealed class ProjectFile
    {
        public string outputDir;
        public Addon[] addons = null;

        internal Error[] Validate()
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(outputDir))
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(outputDir)} is null or empty"));
            else if (outputDir.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(outputDir)} is invalid"));

            if (addons == null)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(addons)} is null"));
            else if (addons.Length <= 0)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(addons)} is empty"));
            else if (addons.ContainsNull())
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(addons)} contains null"));
            else
            {
                List<Error> temp = new List<Error>();
                foreach (Addon addon in addons) temp.AddRange(addon.Validate());
                if (temp.Count <= 0) foreach (IGrouping<string, Addon> group in
                        addons.GroupBy(addon => addon.Filename).Where(group => group.Count() > 1))
                        temp.Add(new Error(nameof(ProjectFile),
                            $"{nameof(addons)} contains many addons with same name: \"{group.Key}\""));
                errors.AddRange(temp);
            }

            return errors.ToArray();
        }

        internal void FullPath()
        {
            outputDir = Path.GetFullPath(outputDir);
            foreach (Addon addon in addons) addon.FullPath();
        }
    }
}
