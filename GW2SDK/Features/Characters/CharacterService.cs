using System;
using System.Net.Http;
using System.Threading;
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
        private readonly ICharacterReader characterReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public CharacterService(
            HttpClient http,
            ICharacterReader characterReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.characterReader = characterReader ?? throw new ArgumentNullException(nameof(characterReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Account, Permission.Characters)]
        public async Task<IReplicaSet<string>> GetCharactersIndex(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CharactersIndexRequest(accessToken);
            return await http.GetResourcesSet(request,
                    json => characterReader.Name.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Account, Permission.Characters)]
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
        public async Task<IReplica<Character>> GetCharacterByName(
            string characterName,
            string? accessToken,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CharacterByNameRequest(characterName, accessToken);
            return await http.GetResource(request, json => characterReader.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Account, Permission.Characters)]
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
        public async Task<IReplicaSet<Character>> GetCharacters(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CharactersRequest(accessToken);
            return await http.GetResourcesSet(request, json => characterReader.ReadArray(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Characters, Permission.Inventories)]
        public async Task<IReplica<UnlockedRecipesView>> GetUnlockedRecipes(
            string characterId,
            string? accessToken,
            CancellationToken cancellationToken = default
        )
        {
            var request = new UnlockedRecipesRequest(characterId, accessToken);
            return await http.GetResource(request, json => characterReader.Recipes.Read(json, missingMemberBehavior), cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
