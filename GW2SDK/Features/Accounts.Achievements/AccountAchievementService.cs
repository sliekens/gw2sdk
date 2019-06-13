using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Accounts.Achievements;
using GW2SDK.Infrastructure.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Accounts.Achievements
{
    public sealed class AccountAchievementService
    {
        private readonly HttpClient _http;

        public AccountAchievementService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Progression)]
        public async Task<AccountAchievement> GetAccountAchievementById(int achievementId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAccountAchievementByIdRequest.Builder(achievementId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<AccountAchievement>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferList<AccountAchievement>> GetAccountAchievementsByIds([NotNull] IReadOnlyList<int> achievementIds,
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (achievementIds == null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achiement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            using (var request = new GetAccountAchievementsByIdsRequest.Builder(achievementIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                var listContext = response.Headers.GetListContext();
                var list = new List<AccountAchievement>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<AccountAchievement>(list, listContext);
            }
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferList<AccountAchievement>> GetAllAccountAchievements([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAllAccountAchievementsRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                var listContext = response.Headers.GetListContext();
                var list = new List<AccountAchievement>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<AccountAchievement>(list, listContext);
            }
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferPage<AccountAchievement>> GetAccountAchievementsByPage(int page, int? pageSize, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAccountAchievementsByPageRequest.Builder(page, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new ArgumentException(text);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                var pageContext = response.Headers.GetPageContext();
                var list = new List<AccountAchievement>(pageContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<AccountAchievement>(list, pageContext);
            }
        }
    }
}
