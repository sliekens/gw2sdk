using GuildWars2.Accounts.Http;

namespace GuildWars2.Accounts;

[PublicAPI]
public sealed class AccountsQuery
{
    private readonly HttpClient http;

    public AccountsQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/account

    public Task<Replica<AccountSummary>> GetSummary(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/account/progression

    public Task<Replica<HashSet<Progression>>> GetProgression(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ProgressionRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/account/luck

    /// <summary>
    /// Retrieves the total amount of luck consumed on an account. This endpoint is only accessible with a valid API key.
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="missingMemberBehavior"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Replica<AccountLuck>> GetLuck(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountLuckRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/characters/:id/core

    public Task<Replica<CharacterSummary>> GetCharacterSummary(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharacterSummaryRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion

    #region v2/characters

    public Task<Replica<HashSet<string>>> GetCharactersIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CharactersIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Character>> GetCharacterByName(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharacterByNameRequest request = new(characterName)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Character>>> GetCharacters(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharactersRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}
