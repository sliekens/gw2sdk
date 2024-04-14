using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Dungeons.Http;

internal sealed class DungeonsRequest : IHttpRequest<HashSet<Dungeon>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/dungeons")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    
    public async Task<(HashSet<Dungeon> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetDungeon());
        return (value, new MessageContext(response));
    }
}
