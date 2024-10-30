using System.Text.Encodings.Web;
using System.Text.Json;

namespace GuildWars2.TestDataHelper;

internal static class JsonDocumentEx
{
    // UnsafeRelaxedJsonEscaping is used to preserve special characters in the output
    // Otherwise, they would be escaped as \uXXXX sequences which is a safety measure for web content but not necessary for test data
    private static readonly JsonWriterOptions WithoutIndented = new() { Indented = false, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    private static readonly JsonWriterOptions WithIndented = new() { Indented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    internal static JsonDocument Indent(this JsonDocument json, bool indent = true)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, indent ? WithIndented : WithoutIndented);
        json.WriteTo(writer);
        writer.Flush();
        stream.Position = 0;
        return JsonDocument.Parse(stream);
    }
}
