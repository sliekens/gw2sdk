using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Recipes.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Recipes
{
    [PublicAPI]
    public sealed class AccountRecipesService
    {
        private readonly IAccountRecipeReader _accountRecipeReader;
        private readonly HttpClient _http;

        public AccountRecipesService(HttpClient http, IAccountRecipeReader accountRecipeReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountRecipeReader = accountRecipeReader ?? throw new ArgumentNullException(nameof(accountRecipeReader));
        }

        [Scope(Permission.Unlocks)]
        public async Task<IReadOnlySet<int>> GetUnlockedRecipes(string? accessToken = null)
        {
            var request = new UnlockedRecipesRequest(accessToken);
            return await _http.GetResourcesSetSimple(request, json => _accountRecipeReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
