using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Professions.Http;
using GW2SDK.Professions.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    public sealed class ProfessionService
    {
        private readonly HttpClient http;

        public ProfessionService(HttpClient http)
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<Profession>> GetProfessions(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionsRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ProfessionReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ProfessionName>> GetProfessionNames(
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionNamesRequest();
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ProfessionNameReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Profession>> GetProfessionByName(
            ProfessionName professionName,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionByNameRequest(professionName, language);
            return await http.GetResource(request,
                    json => ProfessionReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Profession>> GetProfessionsByNames(
            IReadOnlyCollection<ProfessionName> professionNames,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionsByNamesRequest(professionNames, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => ProfessionReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Profession>> GetProfessionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionsByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => ProfessionReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
