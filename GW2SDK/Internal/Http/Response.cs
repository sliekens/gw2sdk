using System.Text.Json;

namespace GuildWars2.Http;

internal static class Response
{
    public static async Task<(JsonDocument Json, MessageContext Context)> Json(
        HttpClient httpClient,
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

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);

        return (json, new MessageContext(response));
    }
}
