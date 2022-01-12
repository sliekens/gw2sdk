using GW2SDK.Accounts.Wallet;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Features.Accounts.Wallet
{
    [PublicAPI]
    public sealed class WalletReader : IWalletReader
    {
        public IJsonReader<CurrencyAmount> CurrencyAmount { get; } = new CurrencyAmountReader();
    }
}