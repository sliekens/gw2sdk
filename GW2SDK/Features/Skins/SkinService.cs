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

        public SkinService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferCollection<int>> GetSkinsIndex(JsonSerializerSettings? settings = null)
        {
            var request = new SkinsIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Skin?> GetSkinById(int skinId, JsonSerializerSettings? settings = null)
        {
            var request = new SkinByIdRequest(skinId);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Skin>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferCollection<Skin>> GetSkinsByIds(IReadOnlyCollection<int> skinIds, JsonSerializerSettings? settings = null)
        {
            if (skinIds == null)
            {
                throw new ArgumentNullException(nameof(skinIds));
            }

            if (skinIds.Count == 0)
            {
                throw new ArgumentException("Skin IDs cannot be an empty collection.", nameof(skinIds));
            }

            var request = new SkinsByIdsRequest(skinIds);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Skin>(context.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferCollection<Skin>(list, context);
        }

        public async Task<IDataTransferPage<Skin>> GetSkinsByPage(int pageIndex, int? pageSize = null, JsonSerializerSettings? settings = null)
        {
            var request = new SkinsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Skin>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Skin>(list, pageContext);
        }
    }
}
