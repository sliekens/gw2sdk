using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Accounts;

namespace GW2SDK.Infrastructure.Accounts
{
    public sealed class AccountJsonService : IAccountJsonService
    {
        private readonly HttpClient _http;

        public AccountJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<HttpResponseMessage> GetAccount()
        {
            using (var request = new GetAccountRequest())
            {
                return await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            }
        }
    }
}
