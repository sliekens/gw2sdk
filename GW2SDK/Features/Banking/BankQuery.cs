using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Banking;

[PublicAPI]
public sealed class BankQuery
{
    private readonly HttpClient http;

    public BankQuery(HttpClient http)
    {
        this.http = http ?? throw new ArgumentNullException(nameof(http));
        http.BaseAddress ??= BaseAddress.DefaultUri;
    }

    [Scope(Permission.Inventories)]
    public Task<IReplica<Bank>> GetBank(
        string? accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        BankRequest request = new()
        {
            AccessToken = accessToken,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<MaterialCategory>> GetMaterialCategories(
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoriesRequest request = new()
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<int>> GetMaterialCategoriesIndex(
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoriesIndexRequest request = new();
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplica<MaterialCategory>> GetMaterialCategoryById(
        int materialCategoryId,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoryByIdRequest request = new(materialCategoryId)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }

    public Task<IReplicaSet<MaterialCategory>> GetMaterialCategoriesByIds(
        IReadOnlyCollection<int> materialCategoryIds,
        Language? language = default,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        MaterialCategoriesByIdsRequest request = new(materialCategoryIds)
        {
            Language = language,
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(http, cancellationToken);
    }
}
