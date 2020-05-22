﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Extensions;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Recipes.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public sealed class RecipeService
    {
        private readonly HttpClient _http;

        public RecipeService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IDataTransferList<int>> GetRecipesIndex(JsonSerializerSettings? settings = null)
        {
            using var request = new GetRecipesIndexRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<int>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<int>(list, listContext);
        }

        public async Task<Recipe?> GetRecipeById(int recipeId, JsonSerializerSettings? settings = null)
        {
            using var request = new GetRecipeByIdRequest.Builder(recipeId).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Recipe>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }

        public async Task<IDataTransferList<Recipe>> GetRecipesByIds(IReadOnlyList<int> recipeIds, JsonSerializerSettings? settings = null)
        {
            if (recipeIds == null)
            {
                throw new ArgumentNullException(nameof(recipeIds));
            }

            if (recipeIds.Count == 0)
            {
                throw new ArgumentException("Recipe IDs cannot be an empty collection.", nameof(recipeIds));
            }

            using var request = new GetRecipesByIdsRequest.Builder(recipeIds).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listContext = response.Headers.GetListContext();
            var list = new List<Recipe>(listContext.ResultCount);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferList<Recipe>(list, listContext);
        }

        public async Task<IDataTransferPage<Recipe>> GetRecipesByPage(int pageIndex, int? pageSize = null, JsonSerializerSettings? settings = null)
        {
            using var request = new GetRecipesByPageRequest.Builder(pageIndex, pageSize).GetRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Recipe>(pageContext.PageSize);
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return new DataTransferPage<Recipe>(list, pageContext);
        }
    }
}
