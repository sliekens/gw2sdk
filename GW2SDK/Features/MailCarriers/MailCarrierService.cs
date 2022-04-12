using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.MailCarriers.Http;
using GW2SDK.MailCarriers.Json;

using JetBrains.Annotations;

namespace GW2SDK.MailCarriers;

[PublicAPI]
public sealed class MailCarrierService
{
    private readonly HttpClient http;

    public MailCarrierService(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplicaSet<MailCarrier>> GetMailCarriers(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => MailCarrierReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetMailCarriersIndex(CancellationToken cancellationToken = default)
    {
        MailCarriersIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<MailCarrier>> GetMailCarrierById(
        int mailCarrierId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarrierByIdRequest request = new(mailCarrierId, language);
        return await http.GetResource(request,
                json => MailCarrierReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<MailCarrier>> GetMailCarriersByIds(
#if NET
        IReadOnlySet<int> mailCarrierIds,
#else
        IReadOnlyCollection<int> mailCarrierIds,
#endif
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersByIdsRequest request = new(mailCarrierIds, language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => MailCarrierReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaPage<MailCarrier>> GetMailCarriersByPage(
        int pageIndex,
        int? pageSize = default,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MailCarriersByPageRequest request = new(pageIndex, pageSize, language);
        return await http.GetResourcesPage(request,
                json => json.RootElement.GetArray(item => MailCarrierReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }
}