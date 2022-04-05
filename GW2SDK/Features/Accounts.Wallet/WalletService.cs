using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Accounts.Wallet.Http;
using GW2SDK.Accounts.Wallet.Json;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Wallet
{
    [PublicAPI]
    public sealed class WalletService
    {
        private readonly HttpClient http;

        public WalletService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        [Scope(Permission.Wallet)]
#if NET
        public async Task<IReplica<IReadOnlySet<CurrencyAmount>>> GetWallet(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
#else
        public async Task<IReplica<IReadOnlyCollection<CurrencyAmount>>> GetWallet(
            string? accessToken,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
#endif
        {
            var request = new WalletRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => json.RootElement.GetArray(item => CurrencyAmountReader.Read(item, missingMemberBehavior)),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
