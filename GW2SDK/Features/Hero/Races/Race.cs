namespace GuildWars2.Hero.Races;

/// <summary>Information about one of the playable races.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Race
{
    /// <summary>The race ID.</summary>
    public required RaceName Id { get; init; }

    /// <summary>The list of racial skill IDs. Each race has 6 unique skills which are healing, utility or elite skills. The
    /// racial skills can be used by any profession except Revenant.</summary>
    public required IReadOnlyList<int> SkillIds { get; init; }

    /// <summary>The display name of the race.</summary>
    public required string Name { get; init; }
}
