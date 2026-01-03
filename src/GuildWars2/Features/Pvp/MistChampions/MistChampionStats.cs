namespace GuildWars2.Pvp.MistChampions;

/// <summary>Information about the stats of a Mist Champion.</summary>
[DataTransferObject]
public sealed record MistChampionStats
{
    /// <summary>The Mist Champion's offense stat.</summary>
    public required int Offense { get; init; }

    /// <summary>The Mist Champion's defense stat.</summary>
    public required int Defense { get; init; }

    /// <summary>The Mist Champion's speed stat.</summary>
    public required int Speed { get; init; }
}
