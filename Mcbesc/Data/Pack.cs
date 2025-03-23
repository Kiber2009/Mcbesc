using System.Collections.Generic;
using System.IO;
using Mcbesc.Errors;

namespace Mcbesc.Data
{
    internal sealed class Pack
    {
        public string path;
        public JsBuild jsBuild = new JsBuild();

        internal void FullPath()
        {
            path = Path.GetFullPath(path);
        }

        internal Error[] Validate()
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(path))
                errors.Add(new Error(nameof(Pack), $"{nameof(path)} is null or empty"));
            else if (!Directory.Exists(path))
                errors.Add(new Error(nameof(Pack), $"Pack \"{path}\" is not exists"));

            return errors.ToArray();
        }
    }
}
