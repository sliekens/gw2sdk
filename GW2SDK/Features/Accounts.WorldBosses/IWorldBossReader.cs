using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.WorldBosses
{
    [PublicAPI]
    public interface IWorldBossReader
    {
        IJsonReader<string> Id { get; }
    }
}
