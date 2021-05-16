using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Categories.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryService
    {
        private readonly IAchievementCategoryReader _achievementCategoryReader;
        private readonly HttpClient _http;

        public AchievementCategoryService(HttpClient http, IAchievementCategoryReader achievementCategoryReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementCategoryReader = achievementCategoryReader ??
                throw new ArgumentNullException(nameof(achievementCategoryReader));
        }

        public async Task<IDataTransferSet<AchievementCategory>> GetAchievementCategories()
        {
            var request = new AchievementCategoriesRequest();
            return await _http.GetResourcesSet(request, json => _achievementCategoryReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetAchievementCategoriesIndex()
        {
            var request = new AchievementCategoriesIndexRequest();
            return await _http.GetResourcesSet(request, json => _achievementCategoryReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<AchievementCategory> GetAchievementCategoryById(int achievementCategoryId)
        {
            var request = new AchievementCategoryByIdRequest(achievementCategoryId);
            return await _http.GetResource(request, json => _achievementCategoryReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<AchievementCategory>> GetAchievementCategoriesByIds(
            IReadOnlyCollection<int> achievementCategoryIds
        )
        {
            if (achievementCategoryIds is null)
            {
                throw new ArgumentNullException(nameof(achievementCategoryIds));
            }

            if (achievementCategoryIds.Count == 0)
            {
                throw new ArgumentException("Achievement category IDs cannot be an empty collection.",
                    nameof(achievementCategoryIds));
            }

            var request = new AchievementCategoriesByIdsRequest(achievementCategoryIds);
            return await _http.GetResourcesSet(request, json => _achievementCategoryReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<AchievementCategory>> GetAchievementCategoriesByPage(
            int pageIndex,
            int? pageSize = null
        )
        {
            var request = new AchievementCategoriesByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _achievementCategoryReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
