using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankService
    {
        private readonly IBankReader _bankReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public BankService(HttpClient http, IBankReader bankReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _bankReader = bankReader ?? throw new ArgumentNullException(nameof(bankReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Inventories)]
        public async Task<Bank> GetBank(string? accessToken = null)
        {
            var request = new BankRequest(accessToken);
            return await _http.GetResource(request, json => _bankReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
