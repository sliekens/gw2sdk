using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Specializations.Http;
using JetBrains.Annotations;

namespace GW2SDK.Specializations
{
    [PublicAPI]
    public sealed class SpecializationService
    {
        private readonly HttpClient _http;

        private readonly ISpecializationReader _specializationReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public SpecializationService(
            HttpClient http,
            ISpecializationReader specializationReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _specializationReader = specializationReader ?? throw new ArgumentNullException(nameof(specializationReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Specialization>> GetSpecializations(Language? language = default)
        {
            var request = new SpecializationsRequest(language);
            return await _http.GetResourcesSet(request, json => _specializationReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetSpecializationsIndex()
        {
            var request = new SpecializationsIndexRequest();
            return await _http
                .GetResourcesSet(request, json => _specializationReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Specialization>> GetSpecializationById(int specializationId, Language? language = default)
        {
            var request = new SpecializationByIdRequest(specializationId, language);
            return await _http.GetResource(request, json => _specializationReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Specialization>> GetSpecializationsByIds(
            IReadOnlyCollection<int> specializationIds,
            Language? language = default
        )
        {
            var request = new SpecializationsByIdsRequest(specializationIds, language);
            return await _http.GetResourcesSet(request, json => _specializationReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
