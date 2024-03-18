namespace GuildWars2.Pvp.Seasons;

/// <summary>Modifiers for divisions.</summary>
[PublicAPI]
public sealed record DivisionFlags : Flags
{
    public required bool CanLosePoints { get; init; }

    public required bool CanLoseTiers { get; init; }

    public required bool Repeatable { get; init; }
}
