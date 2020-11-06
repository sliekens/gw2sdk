using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Recipes.Impl;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Recipes
{
    [PublicAPI]
    public sealed class AccountRecipesService
    {
        private readonly HttpClient _http;

        public AccountRecipesService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Unlocks)]
        public async Task<IReadOnlyCollection<int>> GetUnlockedRecipes(string? accessToken = null)
        {
            var request = new UnlockedRecipesRequest(accessToken);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var list = new List<int>();
            JsonConvert.PopulateObject(json, list, Json.DefaultJsonSerializerSettings);
            return list.AsReadOnly();
        }
    }
}
