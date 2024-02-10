﻿using GuildWars2.Hero.Equipment.MailCarriers.Http;

namespace GuildWars2.Hero.Equipment.MailCarriers;

/// <summary>Provides query methods for mail carrier animations and animations unlocked on the account.</summary>
[PublicAPI]
public sealed class MailCarriersClient
{
    private readonly HttpClient httpClient;

    public MailCarriersClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    public Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriers(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetMailCarriersIndex(
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersIndexRequest request = new();
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(MailCarrier Value, MessageContext Context)> GetMailCarrierById(
        int mailCarrierId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarrierByIdRequest request = new(mailCarrierId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriersByIds(
        IReadOnlyCollection<int> mailCarrierIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersByIdsRequest request = new(mailCarrierIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<MailCarrier> Value, MessageContext Context)> GetMailCarriersByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersByPageRequest request = new(pageIndex)
        {
            PageSize = pageSize,
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }

    public Task<(HashSet<int> Value, MessageContext Context)> GetUnlockedMailCarriers(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        UnlockedMailCarriersRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
