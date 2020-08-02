using System.Reflection;
using System.Text.Json;

namespace GW2SDK.Impl.Json
{
    internal static class JsonElementInfo
    {
        internal static readonly MethodInfo GetRawText = typeof(JsonElement).GetMethod(nameof(JsonElement.GetRawText));

        internal static readonly MethodInfo GetString = typeof(JsonElement).GetMethod(nameof(JsonElement.GetString));

        internal static readonly MethodInfo TryGetDateTime = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetDateTime));

        internal static readonly MethodInfo TryGetDateTimeOffset = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetDateTimeOffset));

        internal static readonly MethodInfo TryGetGuid = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetGuid));

        internal static readonly MethodInfo TryGetDouble = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetDouble));

        internal static readonly MethodInfo TryGetSingle = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetSingle));

        internal static readonly MethodInfo TryGetByte = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetByte));

        internal static readonly MethodInfo TryGetDecimal = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetDecimal));

        internal static readonly MethodInfo TryGetInt16 = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetInt16));

        internal static readonly MethodInfo TryGetInt32 = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetInt32));

        internal static readonly MethodInfo TryGetInt64 = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetInt64));

        internal static readonly MethodInfo TryGetSByte = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetSByte));

        internal static readonly MethodInfo TryGetUInt16 = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetUInt16));

        internal static readonly MethodInfo TryGetUInt32 = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetUInt32));

        internal static readonly MethodInfo TryGetUInt64 = typeof(JsonElement).GetMethod(nameof(JsonElement.TryGetUInt64));

        internal static readonly MethodInfo GetArrayLength = typeof(JsonElement).GetMethod(nameof(JsonElement.GetArrayLength));

        internal static readonly MethodInfo EnumerateObject = typeof(JsonElement).GetMethod(nameof(JsonElement.EnumerateObject));

        internal static readonly PropertyInfo ValueKind = typeof(JsonElement).GetProperty(nameof(JsonElement.ValueKind));

        internal static readonly PropertyInfo Current = typeof(JsonElement.ObjectEnumerator).GetProperty(nameof(JsonElement.ObjectEnumerator.Current));

        internal static readonly MethodInfo MoveNext = typeof(JsonElement.ObjectEnumerator).GetMethod(nameof(JsonElement.ObjectEnumerator.MoveNext));

        internal static readonly PropertyInfo Item = typeof(JsonElement).GetProperty(@"Item");
    }
}
