using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GW2SDK.Accounts.Impl;
using GW2SDK.Annotations;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    public sealed class AccountService
    {
        private readonly HttpClient _http;

        public AccountService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<Account?> GetAccount(string? accessToken = null)
        {
            var request = new AccountRequest(accessToken);
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            await using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var jsonDocument = await JsonDocument.ParseAsync(json).ConfigureAwait(false);
            return AccountJsonReader.Instance.Read(jsonDocument.RootElement);
        }
    }
}
