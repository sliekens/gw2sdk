namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Information about an objective to complete in order to progress through a story step.</summary>
[DataTransferObject]
public sealed record Objective
{
    /// <summary>The text displayed when the objective is active, usually contains player instructions.</summary>
    public required string Active { get; init; }

    /// <summary>The text displayed when the objective is complete, usually contains a summary of what happened.</summary>
    public required string Complete { get; init; }
}
