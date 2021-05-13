using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Http;
using GW2SDK.Annotations;
using JetBrains.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankService
    {
        private readonly IBankReader _bankReader;
        private readonly HttpClient _http;

        public BankService(HttpClient http, IBankReader bankReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _bankReader = bankReader ?? throw new ArgumentNullException(nameof(bankReader));
        }

        [Scope(Permission.Inventories)]
        public async Task<Bank> GetBank(string? accessToken = null)
        {
            var request = new BankRequest(accessToken);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _bankReader.Read(json);
        }
    }
}
