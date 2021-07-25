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

        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly IProfessionReader _professionReader;

        public ProfessionService(
            HttpClient http,
            IProfessionReader professionReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _professionReader = professionReader ?? throw new ArgumentNullException(nameof(professionReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Profession>> GetProfessions(Language? language = default)
        {
            var request = new ProfessionsRequest(language);
            return await _http
                .GetResourcesSet(request, json => _professionReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ProfessionName>> GetProfessionNames()
        {
            var request = new ProfessionNamesRequest();
            return await _http.GetResourcesSet(request,
                    json => _professionReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Profession>> GetProfessionByName(
            ProfessionName professionName,
            Language? language = default
        )
        {
            var request = new ProfessionByNameRequest(professionName, language);
            return await _http.GetResource(request, json => _professionReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Profession>> GetProfessionsByIds(
            IReadOnlyCollection<ProfessionName> professionNames,
            Language? language = default
        )
        {
            var request = new ProfessionsByNamesRequest(professionNames, language);
            return await _http
                .GetResourcesSet(request, json => _professionReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Profession>> GetProfessionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ProfessionsByPageRequest(pageIndex, pageSize, language);
            return await _http
                .GetResourcesPage(request, json => _professionReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
