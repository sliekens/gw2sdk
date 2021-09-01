using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementService
    {
        private readonly IAchievementReader achievementReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public AchievementService(
            HttpClient http,
            IAchievementReader achievementReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.achievementReader = achievementReader ?? throw new ArgumentNullException(nameof(achievementReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetAchievementsIndex(CancellationToken cancellationToken = default)
        {
            var request = new AchievementsIndexRequest();
            return await http.GetResourcesSet(request,
                    json => achievementReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Achievement>> GetAchievementById(
            int achievementId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementByIdRequest(achievementId, language);
            return await http.GetResource(request, json => achievementReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Achievement>> GetAchievementsByIds(
            IReadOnlyCollection<int> achievementIds,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementsByIdsRequest(achievementIds, language);
            return await http
                .GetResourcesSet(request, json => achievementReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Achievement>> GetAchievementsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementsByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => achievementReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
