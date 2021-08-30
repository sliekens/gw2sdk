using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Recipes.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Recipes
{
    [PublicAPI]
    public sealed class AccountRecipesService
    {
        private readonly IAccountRecipeReader _accountRecipeReader;

        private readonly HttpClient _http;

        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AccountRecipesService(
            HttpClient http,
            IAccountRecipeReader accountRecipeReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountRecipeReader = accountRecipeReader ?? throw new ArgumentNullException(nameof(accountRecipeReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Unlocks)]
#if NET
        public async Task<IReplica<IReadOnlySet<int>>> GetUnlockedRecipes(string? accessToken = null)
#else
        public async Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedRecipes(string? accessToken = null)
#endif
        {
            var request = new UnlockedRecipesRequest(accessToken);
            return await _http.GetResourcesSetSimple(request,
                    json => _accountRecipeReader.Id.ReadArray(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
