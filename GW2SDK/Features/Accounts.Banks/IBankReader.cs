using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public interface IBankReader
    {
        IJsonReader<AccountBank> AccountBank { get; }

        IJsonReader<int> MaterialCategoryId { get; }

        IJsonReader<MaterialCategory> MaterialCategory { get; }
    }
}
