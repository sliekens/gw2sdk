namespace GuildWars2.Accounts;

/// <summary>Short summary about a player character.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record CharacterSummary
{
    /// <summary>The name of the current character.</summary>
    /// <remarks>This can be changed later with a Black Lion contract.</remarks>
    public required string Name { get; init; }

    /// <summary>The race selected during creation of the current character.</summary>
    public required RaceName Race { get; init; }

    /// <summary>Whether the character is male or female.</summary>
    /// <remarks>This can be changed later with a Black Lion contract.</remarks>
    public required Gender Gender { get; init; }

    /// <summary>The profession name of the current character.</summary>
    public required ProfessionName Profession { get; init; }

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
