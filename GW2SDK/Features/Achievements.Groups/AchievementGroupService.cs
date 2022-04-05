using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups.Http;
using GW2SDK.Achievements.Groups.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupService
    {
        private readonly HttpClient http;

        public AchievementGroupService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroups(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => AchievementGroupReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetAchievementGroupsIndex(CancellationToken cancellationToken = default)
        {
            var request = new AchievementGroupsIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementGroup>> GetAchievementGroupById(
            string achievementGroupId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupByIdRequest(achievementGroupId, language);
            return await http.GetResource(request,
                    json => AchievementGroupReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroupsByIds(
#if NET
            IReadOnlySet<string> achievementGroupIds,
#else
            IReadOnlyCollection<string> achievementGroupIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupsByIdsRequest(achievementGroupIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => AchievementGroupReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementGroup>> GetAchievementGroupsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => AchievementGroupReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
