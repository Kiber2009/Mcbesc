using System.Collections.Generic;

namespace Mcbesc.Utils.Extensions
{
    internal static class DictionaryExtensions
    {
        internal static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key
        )
        {
            return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
        }
    }
}