using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.DailyCrafting.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.DailyCrafting
{
    [PublicAPI]
    public sealed class DailyCraftingService
    {
        private readonly HttpClient http;

        public DailyCraftingService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetDailyRecipes(CancellationToken cancellationToken = default)
#else
        public async Task<IReplica<IReadOnlyCollection<string>>> GetDailyRecipes(
            CancellationToken cancellationToken = default
        )
#endif
        {
            var request = new DailyCraftingRequest();
            return await http.GetResourcesSetSimple(request,
                    json => json.RootElement.GetStringArray(),
                    cancellationToken)
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
#if NET
        public async Task<IReplica<IReadOnlySet<string>>> GetDailyRecipesOnCooldown(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
#else
        public async Task<IReplica<IReadOnlyCollection<string>>> GetDailyRecipesOnCooldown(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
#endif
        {
            var request = new AccountDailyCraftingRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => json.RootElement.GetStringArray(),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
