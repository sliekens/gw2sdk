using GuildWars2.Professions.Http;

namespace GuildWars2.Professions;

[PublicAPI]
public sealed class ProfessionsQuery
{
    private readonly HttpClient http;

    public ProfessionsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessions(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ProfessionsRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<ProfessionName> Value, MessageContext Context)> GetProfessionNames(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ProfessionNamesRequest request = new() { MissingMemberBehavior = missingMemberBehavior };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(Profession Value, MessageContext Context)> GetProfessionByName(
        ProfessionName professionName,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ProfessionByNameRequest request = new(professionName)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessionsByNames(
        IReadOnlyCollection<ProfessionName> professionNames,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ProfessionsByNamesRequest request = new(professionNames)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<(HashSet<Profession> Value, MessageContext Context)> GetProfessionsByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ProfessionsByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #region v2/characters/:id/training

    public Task<(CharacterTraining Value, MessageContext Context)> GetCharacterTraining(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharacterTrainingRequest request = new(characterName)
        {
            MissingMemberBehavior = missingMemberBehavior,
            AccessToken = accessToken
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
