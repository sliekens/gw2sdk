using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts;

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

    public Task<IReplica<AccountSummary>> GetSummary(
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

    #region v2/characters

    [Scope(Permission.Account, Permission.Characters)]
    public Task<IReplicaSet<string>> GetCharactersIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CharactersIndexRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }

    [Scope(Permission.Account, Permission.Characters)]
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
    public Task<IReplica<Character>> GetCharacterByName(
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

    [Scope(Permission.Account, Permission.Characters)]
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
    public Task<IReplicaSet<Character>> GetCharacters(
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
