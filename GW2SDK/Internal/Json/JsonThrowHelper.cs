using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace GuildWars2.Json;

[StackTraceHidden]
internal static class JsonThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowMissingValue(string name)
    {
        throw new InvalidOperationException($"Missing value for '{name}'.");
    }

    [DoesNotReturn]
    internal static void ThrowIncompatibleValue(
        string name,
        Exception innerException,
        JsonProperty member
    )
    {
        throw new InvalidOperationException($"Value for '{name}' is incompatible.", innerException)
        {
            Data =
            {
                ["ValueKind"] = member.Value.ValueKind.ToString(),
                ["Value"] = member.Value.GetRawText()
            }
        };
    }
}
