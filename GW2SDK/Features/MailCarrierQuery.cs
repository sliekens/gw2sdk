using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.MailCarriers;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.MailCarriers;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public sealed class MailCarrierQuery
{
    private readonly HttpClient http;

    public MailCarrierQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public Task<IReplicaSet<MailCarrier>> GetMailCarriers(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetMailCarriersIndex(
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<MailCarrier>> GetMailCarrierById(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<MailCarrier>> GetMailCarriersByIds(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaPage<MailCarrier>> GetMailCarriersByPage(
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
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<IReadOnlyCollection<int>>> GetOwnedMailCarriers(
        string? accessToken,
        CancellationToken cancellationToken = default
    )
    {
        OwnedMailCarriersRequest request = new() { AccessToken = accessToken };
        return request.SendAsync(http, cancellationToken);
    }
}
