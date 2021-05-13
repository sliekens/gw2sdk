using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using JetBrains.Annotations;
using GW2SDK.Characters.Recipes.Http;
using GW2SDK.Http;

namespace GW2SDK.Characters.Recipes
{
    [PublicAPI]
    public sealed class CharacterRecipesService
    {
        private readonly ICharacterReader _characterReader;
        private readonly HttpClient _http;

        public CharacterRecipesService(HttpClient http, ICharacterReader characterReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _characterReader = characterReader ?? throw new ArgumentNullException(nameof(characterReader));
        }

        [Scope(Permission.Characters, Permission.Inventories)]
        public async Task<Character> GetUnlockedRecipes(string characterId, string? accessToken = null)
        {
            var request = new UnlockedRecipesRequest(characterId, accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _characterReader.Read(json);
        }
    }
}
