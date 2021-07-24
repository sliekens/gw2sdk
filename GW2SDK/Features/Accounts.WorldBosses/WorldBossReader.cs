using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.WorldBosses
{
    [PublicAPI]
    public sealed class WorldBossReader : IWorldBossReader
    {
        public IJsonReader<string> Id => new StringJsonReader();
    }
}
