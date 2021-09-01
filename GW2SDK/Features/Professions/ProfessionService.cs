using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        private readonly IProfessionReader professionReader;

        public ProfessionService(
            HttpClient http,
            IProfessionReader professionReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.professionReader = professionReader ?? throw new ArgumentNullException(nameof(professionReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Profession>> GetProfessions(
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionsRequest(language);
            return await http
                .GetResourcesSet(request, json => professionReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<ProfessionName>> GetProfessionNames(CancellationToken cancellationToken = default)
        {
            var request = new ProfessionNamesRequest();
            return await http.GetResourcesSet(request,
                    json => professionReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Profession>> GetProfessionByName(
            ProfessionName professionName,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionByNameRequest(professionName, language);
            return await http.GetResource(request, json => professionReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Profession>> GetProfessionsByNames(
            IReadOnlyCollection<ProfessionName> professionNames,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionsByNamesRequest(professionNames, language);
            return await http
                .GetResourcesSet(request, json => professionReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Profession>> GetProfessionsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new ProfessionsByPageRequest(pageIndex, pageSize, language);
            return await http
                .GetResourcesPage(request, json => professionReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
