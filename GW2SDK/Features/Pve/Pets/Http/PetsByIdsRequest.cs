using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Pve.Pets.Http;

internal sealed class PetsByIdsRequest : IHttpRequest<HashSet<Pet>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/pets") { AcceptEncoding = "gzip" };

    public PetsByIdsRequest(IReadOnlyCollection<int> petIds)
    {
        Check.Collection(petIds);
        PetIds = petIds;
    }

    public IReadOnlyCollection<int> PetIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Pet> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", PetIds },
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
        var value = json.RootElement.GetSet(entry => entry.GetPet(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
