using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
        private readonly HttpClient http;

        public AccountRecipesService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Unlocks)]
#if NET
        public async Task<IReplica<IReadOnlySet<int>>> GetUnlockedRecipes(string? accessToken, CancellationToken cancellationToken
 = default)
#else
        public async Task<IReplica<IReadOnlyCollection<int>>> GetUnlockedRecipes(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
#endif
        {
            var request = new UnlockedRecipesRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => json.RootElement.GetInt32Array(),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
