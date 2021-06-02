using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.DailyCrafting
{
    [PublicAPI]
    public interface IDailyCraftingReader
    {
        IJsonReader<string> Id { get; }
    }
}
