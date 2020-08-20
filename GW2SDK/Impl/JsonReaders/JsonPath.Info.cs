using System.Reflection;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class JsonPathInfo
    {
        internal static readonly PropertyInfo Root = typeof(JsonPath).GetProperty(nameof(JsonPath.Root));

        internal static readonly MethodInfo AccessProperty = typeof(JsonPath).GetMethod(nameof(JsonPath.AccessProperty));

        internal static readonly MethodInfo AccessArrayIndex = typeof(JsonPath).GetMethod(nameof(JsonPath.AccessArrayIndex));
    }
}
