namespace GuildWars2.Hero.Accounts;

/// <summary>Short summary about a player character.</summary>
[DataTransferObject]
public sealed record CharacterSummary
{
    /// <summary>The name of the current character.</summary>
    /// <remarks>This can be changed later with a Black Lion contract.</remarks>
    public required string Name { get; init; }

    /// <summary>The race selected during creation of the current character.</summary>
    public required Extensible<RaceName> Race { get; init; }

    /// <summary>The character's appearance.</summary>
    /// <remarks>This can be changed later with a Black Lion contract.</remarks>
    public required Extensible<BodyType> BodyType { get; init; }

    /// <summary>The profession name of the current character.</summary>
    public required Extensible<ProfessionName> Profession { get; init; }

    /// <summary>The character's level.</summary>
    public required int Level { get; init; }

    /// <summary>The current guild, or an empty string if the character is not currently representing a guild.</summary>
    public required string GuildId { get; init; }

    /// <summary>The amount of time played as the current character.</summary>
    public required TimeSpan Age { get; init; }

    /// <summary>The date and time when this information was last updated.</summary>
    /// <remarks>This is roughly equal to the last time played as the current character.</remarks>
    public required DateTimeOffset LastModified { get; init; }

    /// <summary>The date and time when the current character was created.</summary>
    public required DateTimeOffset Created { get; init; }

    /// <summary>The number of times the current character was fully defeated.</summary>
    public required int Deaths { get; init; }

    /// <summary>The selected title ID of the current character.</summary>
    public required int? TitleId { get; init; }
}
