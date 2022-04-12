using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Accounts.Http;
using GW2SDK.Accounts.Json;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;

using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
public sealed class Account
{
    private readonly HttpClient http;

    public Account(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    #region /v2/account

    public async Task<IReplica<AccountSummary>> GetSummary(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AccountRequest request = new(accessToken);
        return await http.GetResource(request,
                json => AccountReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion

    #region /v2/characters

    [Scope(Permission.Account, Permission.Characters)]
    public async Task<IReplicaSet<string>> GetCharactersIndex(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        CharactersIndexRequest request = new(accessToken);
        return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Account, Permission.Characters)]
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
    public async Task<IReplica<Character>> GetCharacterByName(
        string characterName,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharacterByNameRequest request = new(characterName, accessToken);
        return await http.GetResource(request,
                json => CharacterReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    [Scope(Permission.Account, Permission.Characters)]
    [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
    public async Task<IReplicaSet<Character>> GetCharacters(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CharactersRequest request = new(accessToken);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => CharacterReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    #endregion
}