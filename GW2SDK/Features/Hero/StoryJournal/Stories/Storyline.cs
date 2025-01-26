namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Information about a storyline, for example Living World Season 1.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Storyline
{
    /// <summary>The storyline ID.</summary>
    public required string Id { get; init; }

    /// <summary>The storyline name.</summary>
    public required string Name { get; init; }

    /// <summary>The display order of the storyline in the story journal.</summary>
    public required int Order { get; init; }

    // TODO: should have been IReadOnlyList<int>
    /// <summary>The IDs of the stories that belong to this storyline.</summary>
    public required List<int> StoryIds { get; init; }
}
