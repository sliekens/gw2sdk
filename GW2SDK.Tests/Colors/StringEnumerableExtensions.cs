using System.Collections.Generic;

namespace GW2SDK.Tests.Colors
{
    public static class StringEnumerableExtensions
    {
        public static string ToCsv(this IEnumerable<string> collection)
        {
            return string.Join(", ", collection);
        }
    }
}