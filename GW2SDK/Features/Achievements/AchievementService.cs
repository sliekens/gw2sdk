using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly IAchievementReader _achievementReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AchievementService(HttpClient http, IAchievementReader achievementReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementReader = achievementReader ?? throw new ArgumentNullException(nameof(achievementReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<int>> GetAchievementsIndex()
        {
            var request = new AchievementsIndexRequest();
            return await _http.GetResourcesSet(request, json => _achievementReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Achievement>> GetAchievementById(int achievementId)
        {
            var request = new AchievementByIdRequest(achievementId);
            return await _http.GetResource(request, json => _achievementReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Achievement>> GetAchievementsByIds(
            IReadOnlyCollection<int> achievementIds
        )
        {
            var request = new AchievementsByIdsRequest(achievementIds);
            return await _http.GetResourcesSet(request, json => _achievementReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Achievement>> GetAchievementsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new AchievementsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _achievementReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
