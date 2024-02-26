using GuildWars2.Pve.Pets.Http;

namespace GuildWars2.Pve.Pets;

/// <summary>Provides query methods for Ranger pets.</summary>
[PublicAPI]
public sealed class PetsClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="PetsClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public PetsClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<Pet> Value, MessageContext Context)> GetPets(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PetsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetPetsIndex(
        CancellationToken cancellationToken = default
    )
    {
        PetsIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(Pet Value, MessageContext Context)> GetPetById(
        int petId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PetByIdRequest request = new(petId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Pet> Value, MessageContext Context)> GetPetsByIds(
        IReadOnlyCollection<int> petIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PetsByIdsRequest request = new(petIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<Pet> Value, MessageContext Context)> GetPetsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        PetsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
