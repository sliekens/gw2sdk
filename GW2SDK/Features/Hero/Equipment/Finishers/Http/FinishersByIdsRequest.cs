using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers.Http;

internal sealed class FinishersByIdsRequest : IHttpRequest<HashSet<Finisher>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/finishers") { AcceptEncoding = "gzip" };

    public FinishersByIdsRequest(IReadOnlyCollection<int> finisherIds)
    {
        Check.Collection(finisherIds);
        FinisherIds = finisherIds;
    }

    public IReadOnlyCollection<int> FinisherIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Finisher> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", FinisherIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetFinisher(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
