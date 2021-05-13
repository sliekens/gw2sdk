using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Recipes.Search.Http;

// TODO: search should be able to expand details
namespace GW2SDK.Recipes.Search
{
    [PublicAPI]
    public sealed class SearchRecipeService
    {
        private readonly HttpClient _http;

        private readonly IRecipeReader _recipeReader;

        public SearchRecipeService(HttpClient http, IRecipeReader recipeReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _recipeReader = recipeReader ?? throw new ArgumentNullException(nameof(recipeReader));
        }

        public async Task<IDataTransferCollection<int>> GetRecipesIndexByIngredientId(int ingredientId)
        {
            var request = new RecipesIndexByIngredientIdRequest(ingredientId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_recipeReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<IDataTransferCollection<int>> GetRecipesIndexByItemId(int itemId)
        {
            var request = new RecipesIndexByItemIdRequest(itemId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_recipeReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }
    }
}
