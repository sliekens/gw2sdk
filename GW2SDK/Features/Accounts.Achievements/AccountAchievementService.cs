using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Impl;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    public sealed class AccountAchievementService
    {
        private readonly HttpClient _http;

        public AccountAchievementService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Progression)]
        public async Task<AccountAchievement?> GetAccountAchievementById(int achievementId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetAccountAchievementByIdRequest.Builder(achievementId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<AccountAchievement>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferList<AccountAchievement>> GetAccountAchievementsByIds(
            IReadOnlyList<int> achievementIds,
            JsonSerializerSettings? settings = null)
        {
            if (achievementIds == null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achiement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            using var request = new GetAccountAchievementsByIdsRequest.Builder(achievementIds).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<AccountAchievement>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<AccountAchievement>(list, listContext);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferList<AccountAchievement>> GetAccountAchievements(JsonSerializerSettings? settings = null)
        {
            using var request = new GetAccountAchievementsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<AccountAchievement>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<AccountAchievement>(list, listContext);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferPage<AccountAchievement>> GetAccountAchievementsByPage(
            int pageIndex,
            int? pageSize,
            JsonSerializerSettings? settings = null)
        {
            using var request = new GetAccountAchievementsByPageRequest.Builder(pageIndex, pageSize).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<AccountAchievement>(pageContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<AccountAchievement>(list, pageContext);
        }
    }
}
