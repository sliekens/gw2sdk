namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Information about a storyline, for example Living World Season 1.</summary>
[DataTransferObject]
public sealed record Storyline
{
    /// <summary>The storyline ID.</summary>
    public required string Id { get; init; }

    /// <summary>The storyline name.</summary>
    public required string Name { get; init; }

    /// <summary>The display order of the storyline in the story journal.</summary>
    public required int Order { get; init; }

#pragma warning disable CA1002 // Do not expose generic lists
    // TODO: reconsider collection type
    /// <summary>The IDs of the stories that belong to this storyline.</summary>
    public required List<int> StoryIds { get; init; }
#pragma warning restore CA1002 // Do not expose generic lists
}
