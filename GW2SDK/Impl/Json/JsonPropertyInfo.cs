using System.Reflection;
using System.Text.Json;

namespace GW2SDK.Impl.Json
{
    internal static class JsonPropertyInfo
    {
        internal static readonly PropertyInfo Name = typeof(JsonProperty).GetProperty(nameof(JsonProperty.Name));

        internal static readonly PropertyInfo Value = typeof(JsonProperty).GetProperty(nameof(JsonProperty.Value));

        internal static readonly MethodInfo NameEquals = typeof(JsonProperty).GetMethod(nameof(JsonProperty.NameEquals), new[] { typeof(string) });

        internal static readonly PropertyInfo Item = typeof(JsonElement).GetProperty("Item");
    }
}