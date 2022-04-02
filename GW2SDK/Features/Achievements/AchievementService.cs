using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Http;
using GW2SDK.Achievements.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementService
    {
        private readonly HttpClient http;

        public AchievementService(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<int>> GetAchievementsIndex(CancellationToken cancellationToken = default)
        {
            var request = new AchievementsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Achievement>> GetAchievementById(
            int achievementId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementByIdRequest(achievementId, language);
            return await http.GetResource(request,
                    json => AchievementReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Achievement>> GetAchievementsByIds(
#if NET
            IReadOnlySet<int> achievementIds,
#else
            IReadOnlyCollection<int> achievementIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementsByIdsRequest(achievementIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => AchievementReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Achievement>> GetAchievementsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => AchievementReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
