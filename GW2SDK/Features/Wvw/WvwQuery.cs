using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Wvw.Abilities;
using JetBrains.Annotations;

namespace GuildWars2.Wvw;

[PublicAPI]
public sealed class WvwQuery
{
    private readonly HttpClient http;

    public WvwQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<HashSet<Ability>>> GetAbilities(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetAbilitiesIndex(
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Ability>> GetAbilityById(
        int AbilityId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilityByIdRequest request = new(AbilityId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Ability>>> GetAbilitiesByIds(
        IReadOnlyCollection<int> AbilityIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesByIdsRequest request = new(AbilityIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Ability>>> GetAbilitiesByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        AbilitiesByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
