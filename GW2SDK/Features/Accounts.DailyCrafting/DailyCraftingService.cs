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
        private readonly IDailyCraftingReader dailyCraftingReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public DailyCraftingService(
            HttpClient http,
            IDailyCraftingReader dailyCraftingReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.dailyCraftingReader =
                dailyCraftingReader ?? throw new ArgumentNullException(nameof(dailyCraftingReader));
            this.missingMemberBehavior = missingMemberBehavior;
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
                    json => dailyCraftingReader.Id.ReadArray(json, missingMemberBehavior),
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
                    json => dailyCraftingReader.Id.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
