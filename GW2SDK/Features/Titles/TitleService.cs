using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Titles.Http;
using GW2SDK.Titles.Json;
using JetBrains.Annotations;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleService
    {
        private readonly HttpClient http;

        public TitleService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplicaSet<Title>> GetTitles(
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TitlesRequest(language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => TitleReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetTitlesIndex(CancellationToken cancellationToken = default)
        {
            var request = new TitlesIndexRequest();
            return await http.GetResourcesSet(request, json => json.RootElement.GetInt32Array(), cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Title>> GetTitleById(
            int titleId,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TitleByIdRequest(titleId, language);
            return await http.GetResource(request,
                    json => TitleReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Title>> GetTitlesByIds(
#if NET
            IReadOnlySet<int> titleIds,
#else
            IReadOnlyCollection<int> titleIds,
#endif
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TitlesByIdsRequest(titleIds, language);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => TitleReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Title>> GetTitlesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new TitlesByPageRequest(pageIndex, pageSize, language);
            return await http.GetResourcesPage(request,
                    json => json.RootElement.GetArray(item => TitleReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
