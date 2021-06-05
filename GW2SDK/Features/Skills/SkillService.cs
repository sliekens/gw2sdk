using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Skills.Http;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed class SkillService
    {
        private readonly HttpClient _http;

        private readonly ISkillReader _skillReader;

        private MissingMemberBehavior _missingMemberBehavior;

        public SkillService(
            HttpClient http,
            ISkillReader skillReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _skillReader = skillReader ?? throw new ArgumentNullException(nameof(skillReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IDataTransferSet<Skill>> GetSkills()
        {
            var request = new SkillsRequest();
            return await _http.GetResourcesSet(request, json => _skillReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetSkillsIndex()
        {
            var request = new SkillsIndexRequest();
            return await _http.GetResourcesSet(request, json => _skillReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<Skill> GetSkillById(int skillId)
        {
            var request = new SkillByIdRequest(skillId);
            return await _http.GetResource(request, json => _skillReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Skill>> GetSkillsByIds(IReadOnlyCollection<int> skillIds)
        {
            var request = new SkillsByIdsRequest(skillIds);
            return await _http.GetResourcesSet(request, json => _skillReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Skill>> GetSkillsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new SkillsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _skillReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
