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
        if (response.IsSuccessStatusCode)
        {
            // Do not dispose this JsonDocument, transfer ownership to the caller
            var json = await response.Content.ReadAsJsonDocumentAsync(cancellationToken)
                .ConfigureAwait(false);
            return (json, new MessageContext(response));
        }

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

        Exception exception = response.StatusCode switch
        {
            >= InternalServerError => new ServerProblemException(response.StatusCode, reason),
            TooManyRequests => new TooManyRequestsException(reason),
            NotFound => new ResourceNotFoundException(reason),
            BadRequest => new ArgumentException(reason),
            Unauthorized or Forbidden => new UnauthorizedOperationException(reason),
            _ => new HttpRequestException(reason)
        };

        exception.Data["RequestUri"] = response.RequestMessage?.RequestUri?.ToString();

        throw exception;
    }
}
