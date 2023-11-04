using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Quaggans.Http;

internal sealed class QuaggansByIdsRequest : IHttpRequest<HashSet<Quaggan>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public QuaggansByIdsRequest(IReadOnlyCollection<string> quagganIds)
    {
        Check.Collection(quagganIds);
        QuagganIds = quagganIds;
    }

    public IReadOnlyCollection<string> QuagganIds { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Quaggan> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", QuagganIds },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetQuaggan(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
