using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Accounts.Http;

internal sealed class ProgressionRequest : IHttpRequest<HashSet<Progression>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/account/progression")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Progression> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template with { BearerToken = AccessToken }, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetProgression(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
