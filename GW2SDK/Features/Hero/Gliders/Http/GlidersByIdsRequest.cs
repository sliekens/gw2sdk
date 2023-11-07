using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Gliders.Http;

internal sealed class GlidersByIdsRequest : IHttpRequest<HashSet<Glider>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/gliders") { AcceptEncoding = "gzip" };

    public GlidersByIdsRequest(IReadOnlyCollection<int> gliderIds)
    {
        Check.Collection(gliderIds);
        GliderIds = gliderIds;
    }

    public IReadOnlyCollection<int> GliderIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Glider> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", GliderIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetGlider(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
