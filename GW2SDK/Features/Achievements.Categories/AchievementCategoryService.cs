using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly IAchievementCategoryReader _achievementCategoryReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AchievementCategoryService(HttpClient http, IAchievementCategoryReader achievementCategoryReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementCategoryReader = achievementCategoryReader ??
                throw new ArgumentNullException(nameof(achievementCategoryReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategories()
        {
            var request = new AchievementCategoriesRequest();
            return await _http.GetResourcesSet(request, json => _achievementCategoryReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetAchievementCategoriesIndex()
        {
            var request = new AchievementCategoriesIndexRequest();
            return await _http.GetResourcesSet(request, json => _achievementCategoryReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementCategory>> GetAchievementCategoryById(int achievementCategoryId)
        {
            var request = new AchievementCategoryByIdRequest(achievementCategoryId);
            return await _http.GetResource(request, json => _achievementCategoryReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementCategory>> GetAchievementCategoriesByIds(
            IReadOnlyCollection<int> achievementCategoryIds
        )
        {
            var request = new AchievementCategoriesByIdsRequest(achievementCategoryIds);
            return await _http.GetResourcesSet(request, json => _achievementCategoryReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementCategory>> GetAchievementCategoriesByPage(
            int pageIndex,
            int? pageSize = null
        )
        {
            var request = new AchievementCategoriesByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _achievementCategoryReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
