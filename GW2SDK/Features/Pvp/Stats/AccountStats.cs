using GuildWars2.Hero;

namespace GuildWars2.Pvp.Stats;

[PublicAPI]
[DataTransferObject]
public sealed record AccountStats
{
    public required int PvpRank { get; init; }

    public required int PvpRankPoints { get; init; }

    public required int PvpRankRollovers { get; init; }

    public required Results Aggregate { get; init; }

    public required IReadOnlyDictionary<ProfessionName, Results> Professions { get; init; }

    public required Ladders Ladders { get; init; }
}
