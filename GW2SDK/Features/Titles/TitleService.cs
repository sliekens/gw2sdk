using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Titles.Http;
using JetBrains.Annotations;

namespace GW2SDK.Titles
{
    [PublicAPI]
    public sealed class TitleService
    {
        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        private readonly ITitleReader _titleReader;

        public TitleService(
            HttpClient http,
            ITitleReader titleReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _titleReader = titleReader ?? throw new ArgumentNullException(nameof(titleReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Title>> GetTitles(Language? language = default)
        {
            var request = new TitlesRequest(language);
            return await _http.GetResourcesSet(request, json => _titleReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetTitlesIndex()
        {
            var request = new TitlesIndexRequest();
            return await _http.GetResourcesSet(request, json => _titleReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Title>> GetTitleById(int titleId, Language? language = default)
        {
            var request = new TitleByIdRequest(titleId, language);
            return await _http.GetResource(request, json => _titleReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Title>> GetTitlesByIds(
            IReadOnlyCollection<int> titleIds,
            Language? language = default
        )
        {
            var request = new TitlesByIdsRequest(titleIds, language);
            return await _http.GetResourcesSet(request, json => _titleReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Title>> GetTitlesByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new TitlesByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request, json => _titleReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
