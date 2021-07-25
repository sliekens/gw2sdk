using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupService
    {
        private readonly IAchievementGroupReader _achievementGroupReader;

        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AchievementGroupService(
            HttpClient http,
            IAchievementGroupReader achievementGroupReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementGroupReader =
                achievementGroupReader ?? throw new ArgumentNullException(nameof(achievementGroupReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroups(Language? language = default)
        {
            var request = new AchievementGroupsRequest(language);
            return await _http.GetResourcesSet(request,
                    json => _achievementGroupReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetAchievementGroupsIndex()
        {
            var request = new AchievementGroupsIndexRequest();
            return await _http.GetResourcesSet(request,
                    json => _achievementGroupReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementGroup>> GetAchievementGroupById(
            string achievementGroupId,
            Language? language = default
        )
        {
            var request = new AchievementGroupByIdRequest(achievementGroupId, language);
            return await _http.GetResource(request, json => _achievementGroupReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroupsByIds(
            IReadOnlyCollection<string> achievementGroupIds,
            Language? language = default
        )
        {
            var request = new AchievementGroupsByIdsRequest(achievementGroupIds, language);
            return await _http.GetResourcesSet(request,
                    json => _achievementGroupReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementGroup>> GetAchievementGroupsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new AchievementGroupsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request,
                    json => _achievementGroupReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
