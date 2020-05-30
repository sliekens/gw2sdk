using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Impl;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementService
    {
        private readonly HttpClient _http;

        public AchievementService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<int>> GetAchievementsIndex(JsonSerializerSettings? settings = null)
        {
            using var request = new GetAchievementsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Achievement?> GetAchievementById(int achievementId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetAchievementByIdRequest.Builder(achievementId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Achievement>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Achievement>> GetAchievementsByIds(
            IReadOnlyCollection<int> achievementIds,
            JsonSerializerSettings? settings = null)
        {
            if (achievementIds == null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achievement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            using var request = new GetAchievementsByIdsRequest.Builder(achievementIds).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Achievement>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Achievement>(list, context);
        }

        public async Task<IDataTransferPage<Achievement>> GetAchievementsByPage(
            int pageIndex,
            int? pageSize = null,
            JsonSerializerSettings? settings = null)
        {
            using var request = new GetAchievementsByPageRequest.Builder(pageIndex, pageSize).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Achievement>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Achievement>(list, pageContext);
        }
    }
}
