using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupService
    {
        private readonly HttpClient _http;

        private readonly IAchievementGroupReader _achievementGroupReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AchievementGroupService(HttpClient http, IAchievementGroupReader achievementGroupReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementGroupReader = achievementGroupReader ?? throw new ArgumentNullException(nameof(achievementGroupReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroups()
        {
            var request = new AchievementGroupsRequest();
            return await _http.GetResourcesSet(request, json => _achievementGroupReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetAchievementGroupsIndex()
        {
            var request = new AchievementGroupsIndexRequest();
            return await _http.GetResourcesSet(request, json => _achievementGroupReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementGroup>> GetAchievementGroupById(string achievementGroupId)
        {
            var request = new AchievementGroupByIdRequest(achievementGroupId);
            return await _http.GetResource(request, json => _achievementGroupReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroupsByIds(IReadOnlyCollection<string> achievementGroupIds)
        {
            var request = new AchievementGroupsByIdsRequest(achievementGroupIds);
            return await _http.GetResourcesSet(request, json => _achievementGroupReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementGroup>> GetAchievementGroupsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new AchievementGroupsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _achievementGroupReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
