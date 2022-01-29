using GW2SDK.Accounts.Wallet.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GW2SDK.Accounts.Wallet
{
    [PublicAPI]
    public sealed class WalletService
    {
        private readonly IWalletReader walletReader;

        private readonly HttpClient http;

        private readonly MissingMemberBehavior missingMemberBehavior;

        public WalletService(
            HttpClient http,
            IWalletReader walletReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.walletReader = walletReader ?? throw new ArgumentNullException(nameof(walletReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        [Scope(Permission.Wallet)]
#if NET
        public async Task<IReplica<IReadOnlySet<CurrencyAmount>>> GetWallet(string? accessToken, CancellationToken cancellationToken = default)
#else
        public async Task<IReplica<IReadOnlyCollection<CurrencyAmount>>> GetWallet(
            string? accessToken,
            CancellationToken cancellationToken = default
        )
#endif
        {
            var request = new WalletRequest(accessToken);
            return await http.GetResourcesSetSimple(request,
                    json => walletReader.CurrencyAmount.ReadArray(json, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
