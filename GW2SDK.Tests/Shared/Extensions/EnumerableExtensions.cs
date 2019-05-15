using System.Collections;
using System.Linq;

namespace GW2SDK.Tests.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToCsv(this IEnumerable values) => string.Join(", ", values.Cast<object>());
    }
}
