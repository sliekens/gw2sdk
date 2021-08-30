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
        private readonly IAccountRecipeReader accountRecipeReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public AccountRecipesService(
            HttpClient http,
            IAccountRecipeReader accountRecipeReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.accountRecipeReader = accountRecipeReader ?? throw new ArgumentNullException(nameof(accountRecipeReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Unlocks)]
#if NET
        public async Task<IReplica<IReadOnlySet<int>>> GetUnlockedRecipes(string? accessToken = null)
#else
        public async Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedRecipes(string? accessToken = null)
#endif
        {
            var request = new UnlockedRecipesRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => accountRecipeReader.Id.ReadArray(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
