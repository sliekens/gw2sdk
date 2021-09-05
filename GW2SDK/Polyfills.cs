using System.Collections.Generic;
using System.Text;

namespace GW2SDK
{
    internal static class Polyfills
    {
#if !NET
        internal static void Deconstruct<TKey, TValue>(
            this KeyValuePair<TKey, TValue> pair,
            out TKey key,
            out TValue value
        )
        {
            key = pair.Key;
            value = pair.Value;
        }

        internal static void AppendJoin(
            this StringBuilder instance,
            string separator,
            string first,
            string second
        )
        {
            instance.Append(string.Join(separator, first, second));
        }
#endif
    }
}
