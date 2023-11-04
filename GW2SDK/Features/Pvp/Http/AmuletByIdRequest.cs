using GuildWars2.Http;
using GuildWars2.Pvp.Amulets;

namespace GuildWars2.Pvp.Http;

internal sealed class AmuletByIdRequest : IHttpRequest2<Amulet>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pvp/amulets") { AcceptEncoding = "gzip" };

    public AmuletByIdRequest(int amuletId)
    {
        AmuletId = amuletId;
    }

    public int AmuletId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Amulet Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", AmuletId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetAmulet(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
