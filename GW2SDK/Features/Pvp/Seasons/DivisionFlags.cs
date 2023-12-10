namespace GuildWars2.Pvp.Seasons;

/// <summary>Modifiers for divisions.</summary>
[PublicAPI]
public sealed record DivisionFlags
{
    public required bool CanLosePoints { get; init; }

    public required bool CanLoseTiers { get; init; }

    public required bool Repeatable { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
