using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Annotations;
using GW2SDK.Characters.Http;
using GW2SDK.Characters.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    public sealed class CharacterService
    {
        private readonly HttpClient http;

        public CharacterService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Account, Permission.Characters)]
        public async Task<IReplicaSet<string>> GetCharactersIndex(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CharactersIndexRequest(accessToken);
            return await http.GetResourcesSet(request, json => json.RootElement.GetStringArray(), cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Account, Permission.Characters)]
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
        public async Task<IReplica<Character>> GetCharacterByName(
            string characterName,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CharacterByNameRequest(characterName, accessToken);
            return await http.GetResource(request,
                    json => CharacterReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Account, Permission.Characters)]
        [Scope(ScopeRequirement.Any, Permission.Builds, Permission.Inventories, Permission.Progression)]
        public async Task<IReplicaSet<Character>> GetCharacters(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new CharactersRequest(accessToken);
            return await http.GetResourcesSet(request,
                    json => json.RootElement.GetArray(item => CharacterReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Characters, Permission.Inventories)]
        public async Task<IReplica<UnlockedRecipesView>> GetUnlockedRecipes(
            string characterId,
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new UnlockedRecipesRequest(characterId, accessToken);
            return await http.GetResource(request,
                    json => UnlockedRecipesReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
