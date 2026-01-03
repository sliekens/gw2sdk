namespace GuildWars2.Pvp.Stats;

/// <summary>Information about a player's results.</summary>
[DataTransferObject]
public sealed record Results
{
    /// <summary>The number of wins the player has.</summary>
    public required int Wins { get; init; }

    /// <summary>The number of losses the player has.</summary>
    public required int Losses { get; init; }

    /// <summary>The number of times the player deserted.</summary>
    public required int Desertions { get; init; }

    /// <summary>The number of times the player has received a Bye.</summary>
    public required int Byes { get; init; }

    /// <summary>The number of times the player has forfeited.</summary>
    public required int Forfeits { get; init; }
}
