using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Categories.Impl;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryService
    {
        private readonly HttpClient _http;

        public AchievementCategoryService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<AchievementCategory>> GetAchievementCategories(JsonSerializerSettings? settings = null)
        {
            var request = new AchievementCategoriesRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AchievementCategory>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<AchievementCategory>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetAchievementCategoriesIndex(JsonSerializerSettings? settings = null)
        {
            var request = new AchievementCategoriesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<AchievementCategory?> GetAchievementCategoryById(int achievementCategoryId, JsonSerializerSettings? settings = null)
        {
            var request = new AchievementCategoryByIdRequest(achievementCategoryId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AchievementCategory>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<AchievementCategory>> GetAchievementCategoriesByIds(
            IReadOnlyCollection<int> achievementCategoryIds,
            JsonSerializerSettings? settings = null)
        {
            if (achievementCategoryIds == null)
            {
                throw new ArgumentNullException(nameof(achievementCategoryIds));
            }

            if (achievementCategoryIds.Count == 0)
            {
                throw new ArgumentException("Achievement category IDs cannot be an empty collection.", nameof(achievementCategoryIds));
            }

            var request = new AchievementCategoriesByIdsRequest(achievementCategoryIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AchievementCategory>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<AchievementCategory>(list, context);
        }

        public async Task<IDataTransferPage<AchievementCategory>> GetAchievementCategoriesByPage(
            int pageIndex,
            int? pageSize = null,
            JsonSerializerSettings? settings = null)
        {
            var request = new AchievementCategoriesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<AchievementCategory>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<AchievementCategory>(list, pageContext);
        }
    }
}
