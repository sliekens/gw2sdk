using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Http;
using GW2SDK.Annotations;
using JetBrains.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    public sealed class AccountAchievementService
    {
        private readonly IAccountAchievementReader _accountAchievementReader;
        private readonly HttpClient _http;

        public AccountAchievementService(HttpClient http, IAccountAchievementReader accountAchievementReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountAchievementReader = accountAchievementReader ??
                throw new ArgumentNullException(nameof(accountAchievementReader));
        }

        [Scope(Permission.Progression)]
        public async Task<AccountAchievement> GetAccountAchievementById(int achievementId, string? accessToken = null)
        {
            var request = new AccountAchievementByIdRequest(achievementId, accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _accountAchievementReader.Read(json);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferCollection<AccountAchievement>> GetAccountAchievementsByIds(
            IReadOnlyCollection<int> achievementIds,
            string? accessToken = null
        )
        {
            if (achievementIds is null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achiement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            var request = new AccountAchievementsByIdsRequest(achievementIds, accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AccountAchievement>(context.ResultCount);
            list.AddRange(_accountAchievementReader.ReadArray(json));
            return new DataTransferCollection<AccountAchievement>(list, context);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferCollection<AccountAchievement>> GetAccountAchievements(
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsRequest(accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<AccountAchievement>(context.ResultCount);
            list.AddRange(_accountAchievementReader.ReadArray(json));
            return new DataTransferCollection<AccountAchievement>(list, context);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferPage<AccountAchievement>> GetAccountAchievementsByPage(
            int pageIndex,
            int? pageSize,
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsByPageRequest(pageIndex, pageSize, accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<AccountAchievement>(pageContext.ResultCount);
            list.AddRange(_accountAchievementReader.ReadArray(json));
            return new DataTransferPage<AccountAchievement>(list, pageContext);
        }
    }
}
