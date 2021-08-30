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
        private readonly HttpClient http;

        private readonly ISpecializationReader specializationReader;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public SpecializationService(
            HttpClient http,
            ISpecializationReader specializationReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.specializationReader = specializationReader ?? throw new ArgumentNullException(nameof(specializationReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Specialization>> GetSpecializations(Language? language = default)
        {
            var request = new SpecializationsRequest(language);
            return await http.GetResourcesSet(request, json => specializationReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetSpecializationsIndex()
        {
            var request = new SpecializationsIndexRequest();
            return await http
                .GetResourcesSet(request, json => specializationReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Specialization>> GetSpecializationById(int specializationId, Language? language = default)
        {
            var request = new SpecializationByIdRequest(specializationId, language);
            return await http.GetResource(request, json => specializationReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Specialization>> GetSpecializationsByIds(
            IReadOnlyCollection<int> specializationIds,
            Language? language = default
        )
        {
            var request = new SpecializationsByIdsRequest(specializationIds, language);
            return await http.GetResourcesSet(request, json => specializationReader.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
