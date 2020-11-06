using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Characters.Recipes.Impl;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Characters.Recipes
{
    [PublicAPI]
    public sealed class CharacterRecipesService
    {
        private readonly HttpClient _http;

        public CharacterRecipesService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Characters, Permission.Inventories)]
        public async Task<Character?> GetUnlockedRecipes(string characterId, string? accessToken = null)
        {
            var request = new UnlockedRecipesRequest(characterId, accessToken);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Character>(json, Json.DefaultJsonSerializerSettings);
        }
    }
}
