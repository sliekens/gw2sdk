using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Builds.Http;

internal sealed class SpecializationsByIdsRequest : IHttpRequest<HashSet<Specialization>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/specializations")
    {
        AcceptEncoding = "gzip"
    };

    public SpecializationsByIdsRequest(IReadOnlyCollection<int> specializationIds)
    {
        Check.Collection(specializationIds);
        SpecializationIds = specializationIds;
    }

    public IReadOnlyCollection<int> SpecializationIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Specialization> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", SpecializationIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetSpecialization(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
