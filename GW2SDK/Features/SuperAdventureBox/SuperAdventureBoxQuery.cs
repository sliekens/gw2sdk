﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.SuperAdventureBox;

[PublicAPI]
public sealed class SuperAdventureBoxQuery
{
    private readonly HttpClient http;

    public SuperAdventureBoxQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    #region v2/characters/:id/sab

    [Scope(Permission.Characters, Permission.Progression)]
    public Task<Replica<SuperAdventureBoxProgress>> GetSuperAdventureBoxProgress(
        string characterId,
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        var request = new SuperAdventureBoxProgressRequest(characterId)
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    #endregion
}