using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class BankReader : IBankReader
    {
        public IJsonReader<AccountBank> AccountBank { get; } = new AccountBankReader();

        public IJsonReader<int> MaterialCategoryId { get; } = new Int32JsonReader();

        public IJsonReader<MaterialCategory> MaterialCategory { get; } = new MaterialCategoryReader();
    }
}
