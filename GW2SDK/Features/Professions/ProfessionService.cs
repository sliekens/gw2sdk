using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Professions.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    public sealed class ProfessionService
    {
        private readonly HttpClient _http;

        private readonly IProfessionReader _professionReader;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ProfessionService(HttpClient http, IProfessionReader professionReader, MissingMemberBehavior missingMemberBehavior)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _professionReader = professionReader ?? throw new ArgumentNullException(nameof(professionReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Profession>> GetProfessions()
        {
            var request = new ProfessionsRequest();
            return await _http.GetResourcesSet(request, json => _professionReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ProfessionName>> GetProfessionNames()
        {
            var request = new ProfessionNamesRequest();
            return await _http.GetResourcesSet(request, json => _professionReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Profession>> GetProfessionByName(ProfessionName professionName)
        {
            var request = new ProfessionByNameRequest(professionName);
            return await _http.GetResource(request, json => _professionReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Profession>> GetProfessionsByIds(IReadOnlyCollection<ProfessionName> professionNames)
        {
            var request = new ProfessionsByNamesRequest(professionNames);
            return await _http.GetResourcesSet(request, json => _professionReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Profession>> GetProfessionsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ProfessionsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _professionReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
