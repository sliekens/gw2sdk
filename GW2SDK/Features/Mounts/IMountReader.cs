using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Mounts
{
    [PublicAPI]
    public interface IMountReader
    {
        IJsonReader<Mount> Mount { get; }

        IJsonReader<MountName> MountId { get; }

        IJsonReader<MountSkin> MountSkin { get; }

        IJsonReader<int> MountSkinId { get; }
    }
}
