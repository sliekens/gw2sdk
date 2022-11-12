using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record DungeonPath
{
    public required string Id { get; init; }

    public required DungeonKind Kind { get; init; }
}
