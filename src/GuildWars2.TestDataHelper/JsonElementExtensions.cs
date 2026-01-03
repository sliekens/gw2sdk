using System.Text.Encodings.Web;
using System.Text.Json;

namespace GuildWars2.TestDataHelper;

internal static class JsonElementExtensions
{
    // UnsafeRelaxedJsonEscaping is used to preserve special characters in the output
    // Otherwise, they would be escaped as \uXXXX sequences which is a safety measure for web content but not necessary for test data
    private static readonly JsonSerializerOptions JsonLineOptions = new()
    {
        WriteIndented = false,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    internal static string ToJsonLine(this JsonElement element)
    {
        return JsonSerializer.Serialize(element, JsonLineOptions);
    }
}
