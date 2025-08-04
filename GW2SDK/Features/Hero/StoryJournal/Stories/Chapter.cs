namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Information about a chapter of the personal story (My Story in the story journal).</summary>
[DataTransferObject]
public sealed record Chapter
{
    /// <summary>The name of the chapter.</summary>
    public required string Name { get; init; }
}
