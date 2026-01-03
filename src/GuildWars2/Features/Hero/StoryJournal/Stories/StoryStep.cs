namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Information about a story step, for example "Victory or Death".</summary>
[DataTransferObject]
public sealed record StoryStep
{
    /// <summary>The story step ID.</summary>
    public required int Id { get; init; }

    /// <summary>The story step name.</summary>
    public required string Name { get; init; }

    /// <summary>The level requirement to start the story step.</summary>
    public required int Level { get; init; }

    /// <summary>The ID of the story that this step belongs to.</summary>
    public required int StoryId { get; init; }

    /// <summary>The objectives to complete in order to progress through the story step.</summary>
    public required IReadOnlyList<Objective> Objectives { get; init; }
}
