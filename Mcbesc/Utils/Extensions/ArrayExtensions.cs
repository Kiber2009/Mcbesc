using System.Linq;

namespace Mcbesc.Utils.Extensions
{
    internal static class ArrayExtensions
    {
        internal static bool ContainsNull<T>(this T[] self) where T : class
        {
            return self.Any(item => item == null);
        }
    }
}
