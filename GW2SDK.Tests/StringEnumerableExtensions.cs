using System.Collections.Generic;

namespace GW2SDK.Tests
{
    public static class StringEnumerableExtensions
    {
        public static string ToCsv(this IEnumerable<string> collection)
        {
            return string.Join(", ", collection);
        }
    }
}