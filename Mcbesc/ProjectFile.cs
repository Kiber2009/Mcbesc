using Mcbesc.Utils.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mcbesc
{
    internal struct ProjectFile
    {
        public string[] Packs { get; set; }
        public string BuildDir { get; set; }
        public string BuildName { get; set; }

        internal Error[] Validate()
        {
            List<Error> errors = new List<Error>();

            if (Packs == null)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(Packs)} is null"));
            else if (Packs.ContainsNull())
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(Packs)} contains null"));
            else if (Packs.Length <= 1)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(Packs)} must contains at least one pack"));
            else foreach (string pack in Packs) if (!Directory.Exists(pack))
                        errors.Add(new Error(nameof(ProjectFile),
                            $"{nameof(Packs)} contains \"{pack}\", but it is not exists"));

            if (BuildDir == null)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(BuildDir)} is null"));
            else if (BuildDir.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(BuildDir)} contains invalid characters"));

            if (BuildName == null)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(BuildName)} is null"));
            else if (BuildName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                errors.Add(new Error(nameof(ProjectFile), $"{nameof(BuildName)} contains invalid characters"));

            return errors.ToArray();
        }

        internal void FullPath()
        {
            BuildDir = Path.GetFullPath(BuildDir);
            for (int i = 0; i < Packs.Length; i++) Packs[i] = Path.GetFullPath(Packs[i]);
        }

        [Serializable]
        internal class ProjectFileException : Exception
        {
            internal ProjectFileException() { }
            internal ProjectFileException(string message) : base(message) { }
            internal ProjectFileException(string message, Exception inner) : base(message, inner) { }
            protected ProjectFileException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
