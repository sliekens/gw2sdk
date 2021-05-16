using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Groups.Http;
using JetBrains.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Achievements.Groups
{
    [PublicAPI]
    public sealed class AchievementGroupService
    {
        private readonly HttpClient _http;

        private readonly IAchievementGroupReader _achievementGroupReader;

        public AchievementGroupService(HttpClient http, IAchievementGroupReader achievementGroupReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementGroupReader = achievementGroupReader ?? throw new ArgumentNullException(nameof(achievementGroupReader));
        }

        public async Task<IDataTransferSet<AchievementGroup>> GetAchievementGroups()
        {
            var request = new AchievementGroupsRequest();
            return await _http.GetResourcesSet(request, json => _achievementGroupReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<string>> GetAchievementGroupsIndex()
        {
            var request = new AchievementGroupsIndexRequest();
            return await _http.GetResourcesSet(request, json => _achievementGroupReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<AchievementGroup> GetAchievementGroupById(string achievementGroupId)
        {
            var request = new AchievementGroupByIdRequest(achievementGroupId);
            return await _http.GetResource(request, json => _achievementGroupReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<AchievementGroup>> GetAchievementGroupsByIds(IReadOnlyCollection<string> achievementGroupIds)
        {
            if (achievementGroupIds is null)
            {
                throw new ArgumentNullException(nameof(achievementGroupIds));
            }

            if (achievementGroupIds.Count == 0)
            {
                throw new ArgumentException("Achievement group IDs cannot be an empty collection.", nameof(achievementGroupIds));
            }

            if (achievementGroupIds.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException("Achievement group IDs collection cannot contain empty values.", nameof(achievementGroupIds));
            }

            var request = new AchievementGroupsByIdsRequest(achievementGroupIds);
            return await _http.GetResourcesSet(request, json => _achievementGroupReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<AchievementGroup>> GetAchievementGroupsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new AchievementGroupsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _achievementGroupReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
