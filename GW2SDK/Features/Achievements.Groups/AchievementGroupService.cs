using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Achievements.Groups;
using GW2SDK.Infrastructure.Common;
using Newtonsoft.Json;

namespace GW2SDK.Features.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupService
    {
        private readonly HttpClient _http;

        public AchievementGroupService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<AchievementGroup>> GetAchievementGroups([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementGroupsRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<AchievementGroup>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<AchievementGroup>(list, listContext);
            }
        }

        public async Task<IDataTransferList<string>> GetAchievementGroupsIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementGroupsIndexRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<string>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<string>(list, listContext);
            }
        }
        
        public async Task<AchievementGroup> GetAchievementGroupById([NotNull] string achievementGroupId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementGroupByIdRequest.Builder(achievementGroupId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<AchievementGroup>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<AchievementGroup>> GetAchievementGroupsByIds([NotNull] [ItemNotNull] IReadOnlyList<string> achievementGroupIds, [CanBeNull] JsonSerializerSettings settings = null)
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

            using (var request = new GetAchievementGroupsByIdsRequest.Builder(achievementGroupIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<AchievementGroup>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<AchievementGroup>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<AchievementGroup>> GetAchievementGroupsByPage(int page, int? pageSize = null, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementGroupsByPageRequest.Builder(page, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<AchievementGroup>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<AchievementGroup>(list, pageContext);
            }
        }
    }
}
