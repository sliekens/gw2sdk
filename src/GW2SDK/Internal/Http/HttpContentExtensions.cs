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
        ThrowHelper.ThrowIfNull(instance);
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

    /// <summary>Provides an overload of <see cref="ReadAsStreamAsync"/> that accepts a <see cref="CancellationToken"/>.</summary>
    /// <param name="instance">The HTTP content instance to read from.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <remarks>
    /// This overload is provided for compatibility with older versions of .NET where the native method does not accept a <see cref="CancellationToken"/>.
    /// </remarks>
    private static Task<Stream> ReadAsStreamAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<Stream>(cancellationToken)
            : instance.ReadAsStreamAsync();
    }
#endif
}
