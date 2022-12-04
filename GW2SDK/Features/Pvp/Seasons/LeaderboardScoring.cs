using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardScoring
{
    public required string Id { get; init; }

    public required string Type { get; init; }

    public required string Description { get; init; }

    public required string Name { get; init; }

    public required string Ordering { get; init; }
}
