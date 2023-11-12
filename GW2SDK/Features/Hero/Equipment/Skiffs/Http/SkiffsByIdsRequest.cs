using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Skiffs.Http;

internal sealed class SkiffsByIdsRequest : IHttpRequest<HashSet<Skiff>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/skiffs") { AcceptEncoding = "gzip" };

    public SkiffsByIdsRequest(IReadOnlyCollection<int> skiffIds)
    {
        Check.Collection(skiffIds);
        SkiffIds = skiffIds;
    }

    public IReadOnlyCollection<int> SkiffIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Skiff> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SkiffIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetSkiff(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
