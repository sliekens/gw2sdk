using System.Reflection;
using System.Text.Json;

namespace GW2SDK.Impl.Json
{
    internal static class JsonExceptionInfo
    {
        internal static readonly ConstructorInfo JsonExceptionConstructor = typeof(JsonException).GetConstructor(new[] { typeof(string) });
    }
}