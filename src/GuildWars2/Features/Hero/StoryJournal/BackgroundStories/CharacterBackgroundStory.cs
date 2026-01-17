namespace GuildWars2.Hero.StoryJournal.BackgroundStories;

/// <summary>Information about a character's background story.</summary>
[DataTransferObject]
public sealed record CharacterBackgroundStory
{
    /// <summary>The IDs of background story answers picked during the character's creation.</summary>
    public required IImmutableValueList<string> AnswerIds { get; init; }
}
