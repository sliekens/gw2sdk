using System.IO.Compression;
using System.Text.Json;

namespace GuildWars2.Http;

internal static class HttpContentExtensions
{
    public static async Task<JsonDocument> ReadAsJsonDocumentAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (instance.Headers.ContentType?.MediaType != "application/json")
        {
            throw new JsonException($"Expected a JSON response (application/json) but received '{instance.Headers.ContentType}'.");
        }

        var content = await instance.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        if (instance.Headers.ContentEncoding.LastOrDefault() == "gzip")
        {
            content = new GZipStream(content, CompressionMode.Decompress, false);
        }

#if NET
        await using (content)
#else
        using (content)
#endif
        {
            return await JsonDocument.ParseAsync(content, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }

#if !NET

    // Overload which takes a CancellationToken is unavailable in older versions of .NET
    private static Task<Stream> ReadAsStreamAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    ) =>
        cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<Stream>(cancellationToken)
            : instance.ReadAsStreamAsync();
#endif
}
