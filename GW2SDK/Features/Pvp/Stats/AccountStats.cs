using GuildWars2.Hero;

namespace GuildWars2.Pvp.Stats;

/// <summary>Information about the PvP statistics of the account.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AccountStats
{
    /// <summary>The account's PvP rank, which is a number between 1 and 80.</summary>
    public required int PvpRank { get; init; }

    /// <summary>The amount of PvP rank points the account has.</summary>
    public required int PvpRankPoints { get; init; }

    /// <summary>The amount of times the account has rolled over the maximum PvP rank.</summary>
    public required int PvpRankRollovers { get; init; }

    /// <summary>The aggregated statistics of the account (total wins, total losses etc.)</summary>
    public required Results Aggregate { get; init; }

    /// <summary>The statistics of the account for each profession (wins, losses etc.)</summary>
    public required IReadOnlyDictionary<ProfessionName, Results> Professions { get; init; }

    /// <summary>The statistics of the account, grouped by game rating type (wins, losses etc.)</summary>
    public required Ladders Ladders { get; init; }
}
