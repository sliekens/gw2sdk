using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Impl;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankService
    {
        private readonly HttpClient _http;

        public BankService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Inventories)]
        public async Task<Bank?> GetBank(string? accessToken = null)
        {
            var request = new BankRequest(accessToken);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Bank>(json, Json.DefaultJsonSerializerSettings);
        }
    }
}
