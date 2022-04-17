using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.Http;
using GW2SDK.Accounts.Models;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
public sealed class AccountQuery
{
    private readonly HttpClient http;

    public AccountQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    #region /v2/account

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

    #region /v2/characters

    [Scope(Permission.Account, Permission.Characters)]
    public Task<IReplicaSet<string>> GetCharactersIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CharactersIndexRequest request = new()
        {
            AccessToken = accessToken
        };
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
