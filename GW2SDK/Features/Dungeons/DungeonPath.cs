using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Dungeons;

[PublicAPI]
[DataTransferObject]
public sealed record DungeonPath
{
    public required string Id { get; init; }

    public required DungeonKind Kind { get; init; }
}
