using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Wallet
{
    [PublicAPI]
    public sealed class WalletReader : IWalletReader
    {
        public IJsonReader<CurrencyAmount> CurrencyAmount { get; } = new CurrencyAmountReader();
    }
}