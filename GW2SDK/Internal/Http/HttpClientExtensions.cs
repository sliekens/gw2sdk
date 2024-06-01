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

            ThrowHelper.ThrowBadResponse(reason, response.StatusCode);
        }
        catch (JsonException jsonException)
        {
            ThrowHelper.ThrowBadResponse(
                "Failed to parse the response.",
                jsonException,
                response.StatusCode
            );
        }

        // Fix CS0161, a return is needed even though this code is unreachable
        return default;
    }
}
