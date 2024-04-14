using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries.Http;

internal sealed class MasteryProgressRequest : IHttpRequest<HashSet<MasteryTrackProgress>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account/masteries")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    
    public async Task<(HashSet<MasteryTrackProgress> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        var request = Template with { BearerToken = AccessToken };
        using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value =
            json.RootElement.GetSet(static entry => entry.GetMasteryTrackProgress());
        return (value, new MessageContext(response));
    }
}
