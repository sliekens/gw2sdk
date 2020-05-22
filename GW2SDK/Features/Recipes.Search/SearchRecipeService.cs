using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Recipes.Search.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Recipes.Search
{
    [PublicAPI]
    public sealed class SearchRecipeService
    {
        private readonly HttpClient _http;

        public SearchRecipeService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<int>> GetRecipesIndexByIngredientId(int ingredientId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetRecipesIndexByIngredientIdRequest.Builder(ingredientId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<int>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list, listContext);
        }

        public async Task<IDataTransferList<int>> GetRecipesIndexByItemId(int itemId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetRecipesIndexByItemIdRequest.Builder(itemId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<int>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list, listContext);
        }
    }
}
