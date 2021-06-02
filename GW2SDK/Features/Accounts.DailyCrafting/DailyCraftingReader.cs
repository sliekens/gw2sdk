using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.DailyCrafting
{
    [PublicAPI]
    public sealed class DailyCraftingReader : IDailyCraftingReader
    {
        public IJsonReader<string> Id => new StringJsonReader();
    }
}
