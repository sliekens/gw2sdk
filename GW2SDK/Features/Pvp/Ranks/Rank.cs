using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
[DataTransferObject]
public sealed record Rank
{
    public required int Id { get; init; }

    public required int FinisherId { get; init; }

    public required string Name { get; init; }

    public required string Icon { get; init; }

    public required int MinRank { get; init; }

    public required int MaxRank { get; init; }

    public required IReadOnlyCollection<Level> Levels { get; init; }
}
