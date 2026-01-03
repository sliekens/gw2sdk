namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Information about a story within a storyline.</summary>
[DataTransferObject]
public sealed record Story
{
    /// <summary>The story ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the storyline to which this story belongs.</summary>
    public required string StorylineId { get; init; }

    /// <summary>The short title of the story.</summary>
    public required string Name { get; init; }

    /// <summary>The long description of the story. Can be empty.</summary>
    public required string Description { get; init; }

    /// <summary>The year in which the story takes place.</summary>
    public required string Timeline { get; init; }

    /// <summary>The required level to start the story.</summary>
    public required int Level { get; init; }

    /// <summary>The races that can play this story.</summary>
    public required IReadOnlyCollection<Extensible<RaceName>> Races { get; init; }

    /// <summary>The display order of the story in the story journal.</summary>
    public required int Order { get; init; }

    /// <summary>The chapters of the story, only applicable to the personal story (My Story in the story journal).</summary>
    public required IReadOnlyList<Chapter> Chapters { get; init; }

    /// <summary>Contains various modifiers for the story.</summary>
    public required StoryFlags Flags { get; init; }
}
