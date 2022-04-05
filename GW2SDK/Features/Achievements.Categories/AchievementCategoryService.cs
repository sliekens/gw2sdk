using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Categories.Http;
using GW2SDK.Achievements.Categories.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryService
    {
        private readonly HttpClient http;

        public AchievementCategoryService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategories(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoriesRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item =>
                        AchievementCategoryReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetAchievementCategoriesIndex(CancellationToken cancellationToken = default)
        {
            var request = new AchievementCategoriesIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementCategory>> GetAchievementCategoryById(
            int achievementCategoryId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoryByIdRequest(achievementCategoryId, language);
            return await http.GetResource(request,
                    json => AchievementCategoryReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategoriesByIds(
#if NET
            IReadOnlySet<int> achievementCategoryIds,
#else
            IReadOnlyCollection<int> achievementCategoryIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoriesByIdsRequest(achievementCategoryIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item =>
                        AchievementCategoryReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementCategory>> GetAchievementCategoriesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoriesByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item =>
                        AchievementCategoryReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
