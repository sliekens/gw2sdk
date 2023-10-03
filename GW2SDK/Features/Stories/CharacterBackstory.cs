namespace GuildWars2.Stories;

/// <summary>Represents a character's backstory answers to the questions answered during character creation.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record CharacterBackstory
{
    /// <summary>A list of backstory answer IDs.</summary>
    public required IReadOnlyList<string> Backstory { get; init; }
}
