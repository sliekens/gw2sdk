using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using JetBrains.Annotations;

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
            return await _http.GetResource(request, json => _accountAchievementReader.Read(json))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferSet<AccountAchievement>> GetAccountAchievementsByIds(
            IReadOnlyCollection<int> achievementIds,
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsByIdsRequest(achievementIds, accessToken);
            return await _http.GetResourcesSet(request, json => _accountAchievementReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferSet<AccountAchievement>> GetAccountAchievements(
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsRequest(accessToken);
            return await _http.GetResourcesSet(request, json => _accountAchievementReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IDataTransferPage<AccountAchievement>> GetAccountAchievementsByPage(
            int pageIndex,
            int? pageSize,
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsByPageRequest(pageIndex, pageSize, accessToken);
            return await _http.GetResourcesPage(request, json => _accountAchievementReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
