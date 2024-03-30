namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

/// <summary>Information about an answer to a background story question.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record BackgroundStoryAnswer
{
    /// <summary>The answer ID.</summary>
    public required string Id { get; init; }

    /// <summary>The short title of the answer.</summary>
    public required string Title { get; init; }

    /// <summary>The answer text.</summary>
    public required string Description { get; init; }

    /// <summary>The text that is displayed in the character's biography when this answer is chosen.</summary>
    public required string Journal { get; init; }

    /// <summary>The ID of the background story question that this answer belongs to.</summary>
    public required int QuestionId { get; init; }

    /// <summary>The races that can give this answer.</summary>
    public required IReadOnlyCollection<Extensible<RaceName>> Races { get; init; }

    /// <summary>The professions that can give this answer.</summary>
    public required IReadOnlyCollection<Extensible<ProfessionName>> Professions { get; init; }
}
