using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.Achievements.Http;
using GW2SDK.Accounts.Achievements.Json;
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

        public AccountAchievementService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));;
        }

        [Scope(Permission.Progression)]
        public async Task<IReplica<AccountAchievement>> GetAccountAchievementById(
            int achievementId,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AccountAchievementByIdRequest(achievementId, accessToken);
            return await http.GetResource(request,
                    json => AccountAchievementReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievementsByIds(
#if NET
            IReadOnlySet<int> achievementIds,
#else
            IReadOnlyCollection<int> achievementIds,
#endif
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AccountAchievementsByIdsRequest(achievementIds, accessToken);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item =>
                        AccountAchievementReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaSet<AccountAchievement>> GetAccountAchievements(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AccountAchievementsRequest(accessToken);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item =>
                        AccountAchievementReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReplicaPage<AccountAchievement>> GetAccountAchievementsByPage(
            int pageIndex,
            int? pageSize,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AccountAchievementsByPageRequest(pageIndex, pageSize, accessToken);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item =>
                        AccountAchievementReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
