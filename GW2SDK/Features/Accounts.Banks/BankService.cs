using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Http;
using GW2SDK.Accounts.Banks.Json;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankService
    {
        private readonly HttpClient http;

        public BankService(HttpClient http)
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
            var request = new BankRequest(accessToken);
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
            var request = new MaterialCategoriesRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => MaterialCategoryReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetMaterialCategoriesIndex(CancellationToken cancellationToken = default)
        {
            var request = new MaterialCategoriesIndexRequest();
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
            var request = new MaterialCategoryByIdRequest(materialCategoryId, language);
            return await http.GetResource(request,
                    json => MaterialCategoryReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<MaterialCategory>> GetMaterialCategoriesByIds(
#if NET
            IReadOnlySet<int> materialCategoryIds,
#else
            IReadOnlyCollection<int> materialCategoryIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new MaterialCategoriesByIdsRequest(materialCategoryIds, language);
            return await http.GetResourcesSet(request,
                json => json.RootElement.GetArray(item => MaterialCategoryReader.Read(item, missingMemberBehavior)),
                cancellationToken);
        }
    }
}
