using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Wvw.Objectives;

[PublicAPI]
public sealed class ObjectivesByIdsRequest : IHttpRequest<Replica<HashSet<Objective>>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/wvw/objectives") { AcceptEncoding = "gzip" };

    public ObjectivesByIdsRequest(IReadOnlyCollection<string> objectiveIds)
    {
        Check.Collection(objectiveIds);
        ObjectiveIds = objectiveIds;
    }

    public IReadOnlyCollection<string> ObjectiveIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Objective>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ObjectiveIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<HashSet<Objective>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetObjective(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
