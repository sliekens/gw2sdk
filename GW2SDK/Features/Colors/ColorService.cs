using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Colors.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public sealed class ColorService
    {
        private readonly IColorReader _colorReader;

        private readonly HttpClient _http;

        public ColorService(HttpClient http, IColorReader colorReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _colorReader = colorReader ?? throw new ArgumentNullException(nameof(colorReader));
        }

        public async Task<IDataTransferSet<Color>> GetColors()
        {
            var request = new ColorsRequest();
            return await _http.GetResourcesSet(request, json => _colorReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<int>> GetColorsIndex()
        {
            var request = new ColorsIndexRequest();
            return await _http.GetResourcesSet(request, json => _colorReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<Color> GetColorById(int colorId)
        {
            var request = new ColorByIdRequest(colorId);
            return await _http.GetResource(request, json => _colorReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Color>> GetColorsByIds(IReadOnlyCollection<int> colorIds)
        {
            var request = new ColorsByIdsRequest(colorIds);
            return await _http.GetResourcesSet(request, json => _colorReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Color>> GetColorsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new ColorsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _colorReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
