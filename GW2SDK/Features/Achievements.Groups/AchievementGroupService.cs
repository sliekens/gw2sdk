using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        private readonly IAchievementGroupReader achievementGroupReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public AchievementGroupService(
            HttpClient http,
            IAchievementGroupReader achievementGroupReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.achievementGroupReader =
                achievementGroupReader ?? throw new ArgumentNullException(nameof(achievementGroupReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroups(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupsRequest(language);
            return await http.GetResourcesSet(request,
                    json => achievementGroupReader.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<string>> GetAchievementGroupsIndex(CancellationToken cancellationToken = default)
        {
            var request = new AchievementGroupsIndexRequest();
            return await http.GetResourcesSet(request,
                    json => achievementGroupReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<AchievementGroup>> GetAchievementGroupById(
            string achievementGroupId,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupByIdRequest(achievementGroupId, language);
            return await http.GetResource(request, json => achievementGroupReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<AchievementGroup>> GetAchievementGroupsByIds(
#if NET
            IReadOnlySet<string> achievementGroupIds,
#else
            IReadOnlyCollection<string> achievementGroupIds,
#endif
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupsByIdsRequest(achievementGroupIds, language);
            return await http.GetResourcesSet(request,
                    json => achievementGroupReader.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<AchievementGroup>> GetAchievementGroupsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new AchievementGroupsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => achievementGroupReader.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
