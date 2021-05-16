using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    public sealed class AccountService
    {
        private readonly IAccountReader _accountReader;
        private readonly HttpClient _http;

        public AccountService(HttpClient http, IAccountReader accountReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountReader = accountReader ?? throw new ArgumentNullException(nameof(accountReader));
        }

        public async Task<Account> GetAccount(string? accessToken = null)
        {
            var request = new AccountRequest(accessToken);
            return await _http.GetResource(request, json => _accountReader.Read(json))
                .ConfigureAwait(false);
        }
    }
}
