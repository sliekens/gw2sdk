using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Characters.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    public sealed class CharacterService
    {
        private readonly ICharacterReader _characterReader;

        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public CharacterService(
            HttpClient http,
            ICharacterReader characterReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _characterReader = characterReader ?? throw new ArgumentNullException(nameof(characterReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Account, Permission.Characters)]
        public async Task<IReplicaSet<string>> GetCharactersIndex(string? accessToken = default)
        {
            var request = new CharactersIndexRequest(accessToken);
            return await _http.GetResourcesSet(request,
                    json => _characterReader.Name.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Account, Permission.Characters)]
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
        public async Task<IReplica<Character>> GetCharacterByName(string characterName, string? accessToken = default)
        {
            var request = new CharacterByNameRequest(characterName, accessToken);
            return await _http.GetResource(request,
                    json => _characterReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Account, Permission.Characters)]
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
        public async Task<IReplicaSet<Character>> GetCharacters(string? accessToken = default)
        {
            var request = new CharactersRequest(accessToken);
            return await _http
                .GetResourcesSet(request, json => _characterReader.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Characters, Permission.Inventories)]
        public async Task<IReplica<UnlockedRecipesView>> GetUnlockedRecipes(
            string characterId,
            string? accessToken = null
        )
        {
            var request = new UnlockedRecipesRequest(characterId, accessToken);
            return await _http.GetResource(request, json => _characterReader.Recipes.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
