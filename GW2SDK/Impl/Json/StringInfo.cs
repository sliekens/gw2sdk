using System.Reflection;

namespace GW2SDK.Impl.Json
{
    internal static class StringInfo
    {
        internal static readonly MethodInfo FormatOne = typeof(string).GetMethod(nameof(string.Format), new[] { typeof(string), typeof(object) });

        internal static readonly MethodInfo FormatTwo = typeof(string).GetMethod(
            nameof(string.Format),
            new[] { typeof(string), typeof(object), typeof(object) }
        );

        internal static readonly MethodInfo FormatThree = typeof(string).GetMethod(
            nameof(string.Format),
            new[] { typeof(string), typeof(object), typeof(object), typeof(object) }
        );
    }
}
