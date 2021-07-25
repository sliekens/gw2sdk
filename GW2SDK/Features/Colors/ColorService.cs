using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Colors.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorService
    {
        private readonly IColorReader _colorReader;

        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ColorService(
            HttpClient http,
            IColorReader colorReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _colorReader = colorReader ?? throw new ArgumentNullException(nameof(colorReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplicaSet<Color>> GetColors(Language? language = default)
        {
            var request = new ColorsRequest(language);
            return await _http.GetResourcesSet(request, json => _colorReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<int>> GetColorsIndex()
        {
            var request = new ColorsIndexRequest();
            return await _http.GetResourcesSet(request, json => _colorReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplica<Color>> GetColorById(int colorId, Language? language = default)
        {
            var request = new ColorByIdRequest(colorId, language);
            return await _http.GetResource(request, json => _colorReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaSet<Color>> GetColorsByIds(
            IReadOnlyCollection<int> colorIds,
            Language? language = default
        )
        {
            var request = new ColorsByIdsRequest(colorIds, language);
            return await _http.GetResourcesSet(request, json => _colorReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        public async Task<IReplicaPage<Color>> GetColorsByPage(
            int pageIndex,
            int? pageSize = default,
            Language? language = default
        )
        {
            var request = new ColorsByPageRequest(pageIndex, pageSize, language);
            return await _http.GetResourcesPage(request, json => _colorReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
