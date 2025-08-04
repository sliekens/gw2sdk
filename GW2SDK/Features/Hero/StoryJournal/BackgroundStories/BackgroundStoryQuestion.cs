namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

/// <summary>Information about a background story question which is asked during character creation.</summary>
[DataTransferObject]
public sealed record BackgroundStoryQuestion
{
    /// <summary>The question ID.</summary>
    public required int Id { get; init; }

    /// <summary>The short title of the question.</summary>
    public required string Title { get; init; }

    /// <summary>The question text.</summary>
    public required string Description { get; init; }

    /// <summary>The IDs of the possible answers to the question.</summary>
    public required IReadOnlyList<string> AnswerIds { get; init; }

    /// <summary>The order in which the question appears relative to other questions in the character creation process.</summary>
    public required int Order { get; init; }

    /// <summary>The races that can receive this question.</summary>
    public required IReadOnlyCollection<Extensible<RaceName>> Races { get; init; }

    /// <summary>The professions that can receive this question.</summary>
    public required IReadOnlyCollection<Extensible<ProfessionName>> Professions { get; init; }
}
