using GuildWars2.Guilds.Bank;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Http;

internal sealed class GuildBankRequest : IHttpRequest<List<GuildBankTab>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/guild/:id/stash") { AcceptEncoding = "gzip" };

    public GuildBankRequest(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(List<GuildBankTab> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", Id),
                    Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetList(entry => entry.GetGuildBankTab(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
