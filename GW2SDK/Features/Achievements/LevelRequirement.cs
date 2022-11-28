using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
[DataTransferObject]
public sealed record LevelRequirement
{
    public required int Min { get; init; }

    public required int Max { get; init; }
}
