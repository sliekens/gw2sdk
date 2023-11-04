using System.IO.Compression;
using System.Text.Json;

namespace GuildWars2.Http;

internal static class HttpResponseMessageExtensions
{
    public static async Task<JsonDocument> ReadAsJsonAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
#if NET
        var content = await instance.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#else
        var content = await instance.ReadAsStreamAsync().ConfigureAwait(false);
#endif
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

    /// <summary>Throws an exception if the response does not contain a useful result.</summary>
    /// <param name="instance">The current response.</param>
    /// <param name="cancellationToken">Self-explanatory.</param>
    /// <exception cref="GatewayException">Generic problem at the server (or an intermediary).</exception>
    /// <exception cref="ResourceNotFoundException">Not Found.</exception>
    /// <exception cref="ArgumentException">Bad Request.</exception>
    /// <exception cref="HttpRequestException">Any other unexpected error.</exception>
    public static async Task EnsureResult(
        this HttpResponseMessage instance,
        CancellationToken cancellationToken
    )
    {
        if (instance.IsSuccessStatusCode)
        {
            return;
        }

        var requestUri = instance.RequestMessage?.RequestUri?.ToString();
        var reason = instance.ReasonPhrase;
        if (instance.Content.Headers.ContentType?.MediaType == "application/json")
        {
            using var json = await instance.Content.ReadAsJsonAsync(cancellationToken)
                .ConfigureAwait(false);
            if (json.RootElement.TryGetProperty("text", out var text))
            {
                reason = text.GetString();
            }
        }

        switch (instance.StatusCode)
        {
            case >= InternalServerError:
                throw new GatewayException(instance.StatusCode, reason)
                {
                    Data = { ["RequestUri"] = requestUri }
                };
            case NotFound:
                throw new ResourceNotFoundException(reason)
                {
                    Data = { ["RequestUri"] = requestUri }
                };
            case BadRequest:
                throw new ArgumentException(reason) { Data = { ["RequestUri"] = requestUri } };
            case Unauthorized or Forbidden:
                throw new UnauthorizedOperationException(reason)
                {
                    Data = { ["RequestUri"] = requestUri }
                };
            case TooManyRequests:
                throw new TooManyRequestsException(reason)
                {
                    Data = { ["RequestUri"] = requestUri }
                };
            default:
                // Throw a super generic, super useless error for cases that we missed
                instance.EnsureSuccessStatusCode();
                break;
        }
    }
}
