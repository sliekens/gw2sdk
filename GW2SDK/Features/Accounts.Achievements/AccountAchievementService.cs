using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Achievements
{
    [PublicAPI]
    public sealed class AccountAchievementService
    {
        private readonly HttpClient _http;
        private readonly IAccountAchievementReader _accountAchievementReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AccountAchievementService(
            HttpClient http,
            IAccountAchievementReader accountAchievementReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountAchievementReader = accountAchievementReader ??
                throw new ArgumentNullException(nameof(accountAchievementReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Progression)]
        public async Task<IReplica<AccountAchievement>> GetAccountAchievementById(int achievementId, string? accessToken = null)
        {
            var request = new AccountAchievementByIdRequest(achievementId, accessToken);
            return await _http.GetResource(request, json => _accountAchievementReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievementsByIds(
            IReadOnlyCollection<int> achievementIds,
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsByIdsRequest(achievementIds, accessToken);
            return await _http.GetResourcesSet(request, json => _accountAchievementReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievements(
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsRequest(accessToken);
            return await _http.GetResourcesSet(request, json => _accountAchievementReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaPage<AccountAchievement>> GetAccountAchievementsByPage(
            int pageIndex,
            int? pageSize,
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsByPageRequest(pageIndex, pageSize, accessToken);
            return await _http.GetResourcesPage(request, json => _accountAchievementReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
