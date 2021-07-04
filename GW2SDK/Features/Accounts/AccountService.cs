using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    public sealed class AccountService
    {
        private readonly HttpClient _http;
        private readonly IAccountReader _accountReader;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public AccountService(HttpClient http, IAccountReader accountReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _accountReader = accountReader ?? throw new ArgumentNullException(nameof(accountReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<Account>> GetAccount(string? accessToken = null)
        {
            var request = new AccountRequest(accessToken);
            return await _http.GetResource(request, json => _accountReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
