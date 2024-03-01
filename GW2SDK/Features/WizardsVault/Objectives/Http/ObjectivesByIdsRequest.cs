using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.WizardsVault.Objectives.Http;

internal sealed class ObjectivesByIdsRequest : IHttpRequest<HashSet<Objective>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wizardsvault/objectives") { AcceptEncoding = "gzip" };

    public ObjectivesByIdsRequest(IReadOnlyCollection<int> objectiveIds)
    {
        Check.Collection(objectiveIds);
        ObjectiveIds = objectiveIds;
    }

    public IReadOnlyCollection<int> ObjectiveIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Objective> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => entry.GetObjective(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
