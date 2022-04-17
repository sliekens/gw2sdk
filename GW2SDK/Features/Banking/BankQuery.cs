using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Banking.Http;
using GW2SDK.Banking.Json;
using GW2SDK.Banking.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Banking;

[PublicAPI]
public sealed class BankQuery
{
    private readonly HttpClient http;

    public BankQuery(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    [Scope(Permission.Inventories)]
    public async Task<IReplica<AccountBank>> GetBank(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BankRequest request = new(accessToken);
        return await http.GetResource(request,
                json => AccountBankReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<MaterialCategory>> GetMaterialCategories(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoriesRequest request = new(language);
        return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => MaterialCategoryReader.Read(item, missingMemberBehavior)),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<int>> GetMaterialCategoriesIndex(CancellationToken cancellationToken = default)
    {
        MaterialCategoriesIndexRequest request = new();
        return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplica<MaterialCategory>> GetMaterialCategoryById(
        int materialCategoryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoryByIdRequest request = new(materialCategoryId, language);
        return await http.GetResource(request,
                json => MaterialCategoryReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReplicaSet<MaterialCategory>> GetMaterialCategoriesByIds(
        IReadOnlyCollection<int> materialCategoryIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoriesByIdsRequest request = new(materialCategoryIds, language);
        return await http.GetResourcesSet(request,
            json => json.RootElement.GetArray(item => MaterialCategoryReader.Read(item, missingMemberBehavior)),
            cancellationToken);
    }
}
