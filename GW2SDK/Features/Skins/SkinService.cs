using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Skins.Http;
using JetBrains.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    public sealed class SkinService
    {
        private readonly HttpClient _http;

        private readonly ISkinReader _skinReader;

        public SkinService(HttpClient http, ISkinReader skinReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _skinReader = skinReader ?? throw new ArgumentNullException(nameof(skinReader));
        }

        public async Task<IDataTransferSet<int>> GetSkinsIndex()
        {
            var request = new SkinsIndexRequest();
            return await _http.GetResourcesSet(request, json => _skinReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<Skin> GetSkinById(int skinId)
        {
            var request = new SkinByIdRequest(skinId);
            return await _http.GetResource(request, json => _skinReader.Read(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferSet<Skin>> GetSkinsByIds(IReadOnlyCollection<int> skinIds)
        {
            if (skinIds is null)
            {
                throw new ArgumentNullException(nameof(skinIds));
            }

            if (skinIds.Count == 0)
            {
                throw new ArgumentException("Skin IDs cannot be an empty collection.", nameof(skinIds));
            }

            var request = new SkinsByIdsRequest(skinIds);
            return await _http.GetResourcesSet(request, json => _skinReader.ReadArray(json))
                .ConfigureAwait(false);
        }

        public async Task<IDataTransferPage<Skin>> GetSkinsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new SkinsByPageRequest(pageIndex, pageSize);
            return await _http.GetResourcesPage(request, json => _skinReader.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
