using System.IO;
using System.Text.Json;

namespace GW2SDK.TestDataHelper;

internal static class JsonDocumentEx
{
    private static readonly JsonWriterOptions WithoutIndented = new() { Indented = false };

    private static readonly JsonWriterOptions WithIndented = new() { Indented = true };

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
