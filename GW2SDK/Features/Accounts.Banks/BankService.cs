﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankService
    {
        private readonly IBankReader bankReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public BankService(
            HttpClient http,
            IBankReader bankReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.bankReader = bankReader ?? throw new ArgumentNullException(nameof(bankReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Inventories)]
        public async Task<IReplica<AccountBank>> GetBank(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
        {
            var request = new BankRequest(accessToken);
            return await http.GetResource(request, json => bankReader.AccountBank.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MaterialCategory>> GetMaterialCategories(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MaterialCategoriesRequest(language);
            return await http.GetResourcesSet(request, json => bankReader.MaterialCategory.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMaterialCategoriesIndex(CancellationToken cancellationToken = default)
        {
            var request = new MaterialCategoriesIndexRequest();
            return await http.GetResourcesSet(request, json => bankReader.MaterialCategoryId.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<MaterialCategory>> GetMaterialCategoryById(
            int materialCategoryId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MaterialCategoryByIdRequest(materialCategoryId, language);
            return await http.GetResource(request, json => bankReader.MaterialCategory.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MaterialCategory>> GetMaterialCategoriesByIds(
#if NET
            IReadOnlySet<int> materialCategoryIds,
#else
            IReadOnlyCollection<int> materialCategoryIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MaterialCategoriesByIdsRequest(materialCategoryIds, language);
            return await http.GetResourcesSet(request,
                json => bankReader.MaterialCategory.ReadArray(json, missingMemberBehavior),
                cancellationToken);
        }
    }
}
