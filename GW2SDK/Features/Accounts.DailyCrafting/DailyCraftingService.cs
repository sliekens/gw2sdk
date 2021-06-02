using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.DailyCrafting.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.DailyCrafting
{
    [PublicAPI]
    public sealed class DailyCraftingService
    {
        private readonly IDailyCraftingReader _dailyCraftingReader;

        private readonly HttpClient _http;

        public DailyCraftingService(HttpClient http, IDailyCraftingReader dailyCraftingReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _dailyCraftingReader = dailyCraftingReader ?? throw new ArgumentNullException(nameof(dailyCraftingReader));
        }

        public async Task<IReadOnlySet<string>> GetDailyRecipes()
        {
            var request = new DailyCraftingRequest();
            return await _http.GetResourcesSetSimple(request, json => _dailyCraftingReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }

        [Scope(Permission.Progression)]
        public async Task<IReadOnlySet<string>> GetDailyRecipesOnCooldown(string? accessToken)
        {
            var request = new AccountDailyCraftingRequest(accessToken);
            return await _http.GetResourcesSetSimple(request, json => _dailyCraftingReader.Id.ReadArray(json))
                .ConfigureAwait(false);
        }
    }
}
