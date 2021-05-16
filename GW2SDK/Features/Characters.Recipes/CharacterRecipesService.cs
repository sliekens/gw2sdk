using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Characters.Recipes.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

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
            return await _http.GetResource(request, json => _characterReader.Read(json))
                .ConfigureAwait(false);
        }
    }
}
