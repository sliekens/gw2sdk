using System.IO.Compression;
using System.Text.Json;

namespace GuildWars2.TestDataHelper;

internal static class HttpContentExtensions
{
    public static async Task<JsonDocument> ReadAsJsonDocumentAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(instance);
        if (instance.Headers.ContentType?.MediaType != "application/json")
        {
            throw new JsonException(
                $"Expected a JSON response (application/json) but received '{instance.Headers.ContentType}'."
            );
        }

        Stream? content = await instance.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        if (instance.Headers.ContentEncoding.LastOrDefault() == "gzip")
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            content = new GZipStream(content, CompressionMode.Decompress, false);
#pragma warning restore CA2000 // Dispose objects before losing scope
        }

        await using (content)
        {
            return await JsonDocument.ParseAsync(content, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }

    public static async Task<string> ReadAsDecodedStringAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(instance);

        Stream? content = await instance.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        if (instance.Headers.ContentEncoding.LastOrDefault() == "gzip")
        {
#pragma warning disable CA2000 // Dispose objects before losing scope
            content = new GZipStream(content, CompressionMode.Decompress, false);
#pragma warning restore CA2000 // Dispose objects before losing scope
        }

        await using (content)
        {
            using StreamReader reader = new(content, leaveOpen: false);
            string text = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
            return text;
        }
    }
}
