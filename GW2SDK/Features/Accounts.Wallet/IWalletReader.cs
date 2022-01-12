using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts.Wallet
{
    [PublicAPI]
    public interface IWalletReader
    {
        IJsonReader<CurrencyAmount> CurrencyAmount { get; }
    }
}