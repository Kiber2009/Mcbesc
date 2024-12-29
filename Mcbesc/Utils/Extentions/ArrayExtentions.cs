namespace Mcbesc.Utils.Extentions
{
    internal static class ArrayExtentions
    {
        internal static bool ContainsNull<T>(this T[] self) where T : class
        {
            bool temp = false;
            foreach (T item in self)
                if (item == null)
                {
                    temp = true;
                    break;
                }
            return temp;
        }
    }
}
