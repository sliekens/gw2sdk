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
        private readonly HttpClient http;
        private readonly IAccountAchievementReader accountAchievementReader;
        private readonly MissingMemberBehavior missingMemberBehavior;

        public AccountAchievementService(
            HttpClient http,
            IAccountAchievementReader accountAchievementReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.accountAchievementReader = accountAchievementReader ??
                throw new ArgumentNullException(nameof(accountAchievementReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Progression)]
        public async Task<IReplica<AccountAchievement>> GetAccountAchievementById(int achievementId, string? accessToken = null)
        {
            var request = new AccountAchievementByIdRequest(achievementId, accessToken);
            return await http.GetResource(request, json => accountAchievementReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievementsByIds(
            IReadOnlyCollection<int> achievementIds,
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsByIdsRequest(achievementIds, accessToken);
            return await http.GetResourcesSet(request, json => accountAchievementReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievements(
            string? accessToken = null
        )
        {
            var request = new AccountAchievementsRequest(accessToken);
            return await http.GetResourcesSet(request, json => accountAchievementReader.ReadArray(json, missingMemberBehavior))
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
            return await http.GetResourcesPage(request, json => accountAchievementReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
