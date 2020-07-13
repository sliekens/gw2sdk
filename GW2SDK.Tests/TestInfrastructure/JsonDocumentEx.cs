using System.IO;
using System.Text.Json;

namespace GW2SDK.Tests.TestInfrastructure
{
    internal static class JsonDocumentEx
    {
        private static readonly JsonWriterOptions JsonWriterOptions = new JsonWriterOptions { Indented = true };

        internal static JsonDocument Indent(this JsonDocument json)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, JsonWriterOptions);
            json.WriteTo(writer);
            writer.Flush();
            stream.Position = 0;
            return JsonDocument.Parse(stream);
        }
    }
}