﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GuildWars2.Masteries;

[PublicAPI]
public sealed class MasteriesQuery
{
    private readonly HttpClient http;

    public MasteriesQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<Replica<HashSet<Mastery>>> GetMasteries(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteriesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<int>>> GetMasteriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MasteriesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<Mastery>> GetMasteryById(
        int masteryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteryByIdRequest request = new(masteryId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<Mastery>>> GetMasteriesByIds(
        IReadOnlyCollection<int> masteryIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MasteriesByIdsRequest request = new(masteryIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<Replica<HashSet<MasteryProgress>>> GetMasteryProgress(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        MasteryProgressRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = MissingMemberBehavior.Error
        };
        return request.SendAsync(http, cancellationToken);
    }
}
