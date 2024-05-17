using System.Text.Json;

namespace GuildWars2.Http;

internal static class HttpClientExtensions
{
    public static async Task<(JsonDocument Json, MessageContext Context)> AcceptJsonAsync(
        this HttpClient httpClient,
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);
        try
        {
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsJsonDocumentAsync(cancellationToken)
                    .ConfigureAwait(false);
                return (json, new MessageContext(response));
            }

            // Do not dispose this JsonDocument, transfer ownership to the caller
            var reason = response.ReasonPhrase;
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                using var json = await response.Content.ReadAsJsonDocumentAsync(cancellationToken)
                    .ConfigureAwait(false);
                if (json.RootElement.TryGetProperty("text", out var text))
                {
                    reason = text.GetString();
                }
            }

#if NET
            throw new BadResponseException(reason, null, response.StatusCode);
#else
            throw new BadResponseException(reason);
#endif
        }
        catch (JsonException jsonException)
        {
#if NET
            throw new BadResponseException(
                "Failed to parse the response.",
                jsonException,
                response.StatusCode
            );
#else
            throw new BadResponseException("Failed to parse the response.", jsonException);
#endif
        }
    }
}
