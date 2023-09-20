using System.IO.Compression;
using System.Net;
using System.Text.Json;

namespace GuildWars2.Http;

[PublicAPI]
public static class HttpResponseMessageExtensions
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
        if ((int)instance.StatusCode >= 500)
        {
            throw new GatewayException(instance.StatusCode, instance.ReasonPhrase)
            {
                Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
            };
        }

        if (instance.StatusCode == NotFound)
        {
            if (instance.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new ResourceNotFoundException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            using var json = await instance.Content.ReadAsJsonAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new ResourceNotFoundException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            throw new ResourceNotFoundException(text.GetString())
            {
                Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
            };
        }

        if (instance.StatusCode == BadRequest)
        {
            if (instance.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new ArgumentException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            using var json = await instance.Content.ReadAsJsonAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new ArgumentException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            var reason = text.GetString();
            if (reason == "ErrTimeout")
            {
                // Sometimes the API responds with 400 Bad Request and message ErrTimeout
                // That's not a user error and should be handled as 504 Gateway Timeout
                throw new GatewayException(GatewayTimeout, reason)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            throw new ArgumentException(reason)
            {
                Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
            };
        }

        if (instance.StatusCode is Unauthorized or Forbidden)
        {
            if (instance.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new UnauthorizedOperationException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            using var json = await instance.Content.ReadAsJsonAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new UnauthorizedOperationException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            throw new UnauthorizedOperationException(text.GetString())
            {
                Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
            };
        }

        if (instance.StatusCode == TooManyRequests)
        {
            if (instance.Content.Headers.ContentType?.MediaType != "application/json")
            {
                throw new TooManyRequestsException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            using var json = await instance.Content.ReadAsJsonAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!json.RootElement.TryGetProperty("text", out var text))
            {
                throw new TooManyRequestsException(instance.ReasonPhrase)
                {
                    Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
                };
            }

            throw new TooManyRequestsException(text.GetString())
            {
                Data = { ["RequestUri"] = instance.RequestMessage?.RequestUri?.ToString() }
            };
        }

        // Throw a super generic, super useless error for cases that we missed
        instance.EnsureSuccessStatusCode();
    }
}
