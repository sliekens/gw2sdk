using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Categories.Http;
using JetBrains.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Achievements.Categories
{
    [PublicAPI]
    public sealed class AchievementCategoryService
    {
        private readonly HttpClient _http;

        private readonly IAchievementCategoryReader _achievementCategoryReader;

        public AchievementCategoryService(HttpClient http, IAchievementCategoryReader achievementCategoryReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementCategoryReader = achievementCategoryReader ??
                throw new ArgumentNullException(nameof(achievementCategoryReader));
        }

        public async Task<IDataTransferCollection<AchievementCategory>> GetAchievementCategories()
        {
            var request = new AchievementCategoriesRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AchievementCategory>(context.ResultCount);
            list.AddRange(_achievementCategoryReader.ReadArray(json));
            return new DataTransferCollection<AchievementCategory>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetAchievementCategoriesIndex()
        {
            var request = new AchievementCategoriesIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_achievementCategoryReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<AchievementCategory> GetAchievementCategoryById(int achievementCategoryId)
        {
            var request = new AchievementCategoryByIdRequest(achievementCategoryId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _achievementCategoryReader.Read(json);
        }

        public async Task<IDataTransferCollection<AchievementCategory>> GetAchievementCategoriesByIds(IReadOnlyCollection<int> achievementCategoryIds)
        {
            if (achievementCategoryIds is null)
            {
                throw new ArgumentNullException(nameof(achievementCategoryIds));
            }

            if (achievementCategoryIds.Count == 0)
            {
                throw new ArgumentException("Achievement category IDs cannot be an empty collection.", nameof(achievementCategoryIds));
            }

            var request = new AchievementCategoriesByIdsRequest(achievementCategoryIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AchievementCategory>(context.ResultCount);
            list.AddRange(_achievementCategoryReader.ReadArray(json));
            return new DataTransferCollection<AchievementCategory>(list, context);
        }

        public async Task<IDataTransferPage<AchievementCategory>> GetAchievementCategoriesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new AchievementCategoriesByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<AchievementCategory>(pageContext.PageSize);
            list.AddRange(_achievementCategoryReader.ReadArray(json));
            return new DataTransferPage<AchievementCategory>(list, pageContext);
        }
    }
}
