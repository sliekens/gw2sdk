namespace GuildWars2.Pvp.Seasons;

/// <summary>Modifiers for divisions.</summary>
public sealed record DivisionFlags : Flags
{
    /// <summary>Indicates whether you can lose pips in this division.</summary>
    /// <remarks>This was only ever the case in the first 4 seasons, when pips played a role in matchmaking. As of season five,
    /// it's no longer possible to lose pips.</remarks>
    public required bool CanLosePoints { get; init; }

    /// <summary>Indicates whether you can progress backwards to the previous tier in this division if you lose enough pips.</summary>
    /// <remarks>This was only ever the case in the first 4 seasons, when pips played a role in matchmaking. As of season five,
    /// it's no longer possible to lose pips.</remarks>
    public required bool CanLoseTiers { get; init; }

    /// <summary>Indicates whether you can repeatedly complete this division for extra rewards.</summary>
    public required bool Repeatable { get; init; }
}
