using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups.Impl;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupService
    {
        private readonly HttpClient _http;

        public AchievementGroupService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<AchievementGroup>> GetAchievementGroups(JsonSerializerSettings? settings = null)
        {
            var request = new AchievementGroupsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AchievementGroup>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<AchievementGroup>(list, context);
        }

        public async Task<IDataTransferCollection<string>> GetAchievementGroupsIndex(JsonSerializerSettings? settings = null)
        {
            var request = new AchievementGroupsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<string>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<string>(list, context);
        }

        public async Task<AchievementGroup?> GetAchievementGroupById(string achievementGroupId, JsonSerializerSettings? settings = null)
        {
            var request = new AchievementGroupByIdRequest(achievementGroupId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AchievementGroup>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<AchievementGroup>> GetAchievementGroupsByIds(
            IReadOnlyCollection<string> achievementGroupIds,
            JsonSerializerSettings? settings = null)
        {
            if (achievementGroupIds == null)
            {
                throw new ArgumentNullException(nameof(achievementGroupIds));
            }

            if (achievementGroupIds.Count == 0)
            {
                throw new ArgumentException("Achievement group IDs cannot be an empty collection.", nameof(achievementGroupIds));
            }

            if (achievementGroupIds.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException("Achievement group IDs collection cannot contain empty values.", nameof(achievementGroupIds));
            }

            var request = new AchievementGroupsByIdsRequest(achievementGroupIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AchievementGroup>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<AchievementGroup>(list, context);
        }

        public async Task<IDataTransferPage<AchievementGroup>> GetAchievementGroupsByPage(
            int pageIndex,
            int? pageSize = null,
            JsonSerializerSettings? settings = null)
        {
            var request = new AchievementGroupsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<AchievementGroup>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<AchievementGroup>(list, pageContext);
        }
    }
}
