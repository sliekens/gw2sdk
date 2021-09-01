using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Categories.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryService
    {
        private readonly IAchievementCategoryReader achievementCategoryReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public AchievementCategoryService(
            HttpClient http,
            IAchievementCategoryReader achievementCategoryReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.achievementCategoryReader = achievementCategoryReader ??
                throw new ArgumentNullException(nameof(achievementCategoryReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategories(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoriesRequest(language);
            return await http.GetResourcesSet(request,
                    json => achievementCategoryReader.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetAchievementCategoriesIndex(CancellationToken cancellationToken = default)
        {
            var request = new AchievementCategoriesIndexRequest();
            return await http.GetResourcesSet(request,
                    json => achievementCategoryReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementCategory>> GetAchievementCategoryById(
            int achievementCategoryId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoryByIdRequest(achievementCategoryId, language);
            return await http
                .GetResource(request, json => achievementCategoryReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategoriesByIds(
            IReadOnlyCollection<int> achievementCategoryIds,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoriesByIdsRequest(achievementCategoryIds, language);
            return await http.GetResourcesSet(request,
                    json => achievementCategoryReader.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementCategory>> GetAchievementCategoriesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementCategoriesByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => achievementCategoryReader.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
