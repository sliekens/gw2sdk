using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Titles.Http;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleService
    {
        private readonly HttpClient _http;

        private readonly ITitleReader _titleReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public TitleService(HttpClient http, ITitleReader titleReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _titleReader = titleReader ?? throw new ArgumentNullException(nameof(titleReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Title>> GetTitles()
        {
            var request = new TitlesRequest();
            return await _http.GetResourcesSet(request, json => _titleReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetTitlesIndex()
        {
            var request = new TitlesIndexRequest();
            return await _http.GetResourcesSet(request, json => _titleReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Title>> GetTitleById(int titleId)
        {
            var request = new TitleByIdRequest(titleId);
            return await _http.GetResource(request, json => _titleReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Title>> GetTitlesByIds(IReadOnlyCollection<int> titleIds)
        {
            var request = new TitlesByIdsRequest(titleIds);
            return await _http.GetResourcesSet(request, json => _titleReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Title>> GetTitlesByPage(int pageIndex, int? pageSize = null)
        {
            var request = new TitlesByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _titleReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
