using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Skins.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Skins
{
    [PublicAPI]
    public sealed class SkinService
    {
        private readonly HttpClient _http;

        public SkinService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<int>> GetSkinsIndex([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetSkinsIndexRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<int>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<int>(list, listContext);
            }
        }

        public async Task<Skin> GetSkinById(int skinId, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetSkinByIdRequest.Builder(skinId).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Skin>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }

        public async Task<IDataTransferList<Skin>> GetSkinsByIds([NotNull] IReadOnlyList<int> skinIds, [CanBeNull] JsonSerializerSettings settings = null)
        {
            if (skinIds == null)
            {
                throw new ArgumentNullException(nameof(skinIds));
            }

            if (skinIds.Count == 0)
            {
                throw new ArgumentException("Skin IDs cannot be an empty collection.", nameof(skinIds));
            }

            using (var request = new GetSkinsByIdsRequest.Builder(skinIds).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var listContext = response.Headers.GetListContext();
                var list = new List<Skin>(listContext.ResultCount);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferList<Skin>(list, listContext);
            }
        }

        public async Task<IDataTransferPage<Skin>> GetSkinsByPage(int page, int? pageSize = null, [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetSkinsByPageRequest.Builder(page, pageSize).GetRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var pageContext = response.Headers.GetPageContext();
                var list = new List<Skin>(pageContext.PageSize);
                JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
                return new DataTransferPage<Skin>(list, pageContext);
            }
        }
    }
}
