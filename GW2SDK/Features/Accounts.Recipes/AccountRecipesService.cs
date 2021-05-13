using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Recipes.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Accounts.Recipes
{
    [PublicAPI]
    public sealed class AccountRecipesService
    {
        private readonly HttpClient _http;

        private readonly IAccountRecipeReader _accountRecipeReader;

        public AccountRecipesService(HttpClient http, IAccountRecipeReader accountRecipeReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountRecipeReader = accountRecipeReader ?? throw new ArgumentNullException(nameof(accountRecipeReader));
        }

        [Scope(Permission.Unlocks)]
        public async Task<IReadOnlyCollection<int>> GetUnlockedRecipes(string? accessToken = null)
        {
            var request = new UnlockedRecipesRequest(accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var list = new List<int>();
            list.AddRange(_accountRecipeReader.Id.ReadArray(json));
            return list.AsReadOnly();
        }
    }
}
