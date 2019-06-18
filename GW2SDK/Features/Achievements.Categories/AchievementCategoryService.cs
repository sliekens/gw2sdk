using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Achievements.Categories;
using GW2SDK.Infrastructure.Common;
using Newtonsoft.Json;

namespace GW2SDK.Features.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryService
    {
        private readonly HttpClient _http;

        public AchievementCategoryService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<AchievementCategory>> GetAchievementCategories([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementCategoriesRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<AchievementCategory>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<AchievementCategory>(list, listContext);
            }
        }

        public async Task<IDataTransferList<int>> GetAchievementCategoriesIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementCategoriesIndexRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<int>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<int>(list, listContext);
            }
        }
        
        public async Task<AchievementCategory> GetAchievementCategoryById(int achievementCategoryId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementCategoryByIdRequest.Builder(achievementCategoryId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<AchievementCategory>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<AchievementCategory>> GetAchievementCategoriesByIds([NotNull] IReadOnlyList<int> achievementCategoryIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (achievementCategoryIds == null)
            {
                throw new ArgumentNullException(nameof(achievementCategoryIds));
            }

            if (achievementCategoryIds.Count == 0)
            {
                throw new ArgumentException("Achievement category IDs cannot be an empty collection.", nameof(achievementCategoryIds));
            }

            using (var request = new GetAchievementCategoriesByIdsRequest.Builder(achievementCategoryIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<AchievementCategory>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<AchievementCategory>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<AchievementCategory>> GetAchievementCategoriesByPage(int page, int? pageSize = null, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementCategoriesByPageRequest.Builder(page, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<AchievementCategory>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<AchievementCategory>(list, pageContext);
            }
        }
    }
}
