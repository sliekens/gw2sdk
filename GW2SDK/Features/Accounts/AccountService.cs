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
        private readonly HttpClient http;
        private readonly IAccountReader accountReader;
        private readonly MissingMemberBehavior missingMemberBehavior;

        public AccountService(HttpClient http, IAccountReader accountReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.accountReader = accountReader ?? throw new ArgumentNullException(nameof(accountReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<Account>> GetAccount(string? accessToken = null)
        {
            var request = new AccountRequest(accessToken);
            return await http.GetResource(request, json => accountReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
