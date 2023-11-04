using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Finishers.Http;

internal sealed class UnlockedFinishersRequest : IHttpRequest2<HashSet<UnlockedFinisher>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account/finishers")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<UnlockedFinisher> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetUnlockedFinisher(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
