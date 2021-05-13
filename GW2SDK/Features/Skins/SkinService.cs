using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Skins.Http;

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

        public async Task<IDataTransferCollection<int>> GetSkinsIndex()
        {
            var request = new SkinsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_skinReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Skin> GetSkinById(int skinId)
        {
            var request = new SkinByIdRequest(skinId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _skinReader.Read(json);
        }

        public async Task<IDataTransferCollection<Skin>> GetSkinsByIds(IReadOnlyCollection<int> skinIds)
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
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Skin>(context.ResultCount);
            list.AddRange(_skinReader.ReadArray(json));
            return new DataTransferCollection<Skin>(list, context);
        }

        public async Task<IDataTransferPage<Skin>> GetSkinsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new SkinsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Skin>(pageContext.PageSize);
            list.AddRange(_skinReader.ReadArray(json));
            return new DataTransferPage<Skin>(list, pageContext);
        }
    }
}
