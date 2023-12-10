namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Modifiers for stories.</summary>
[PublicAPI]
public sealed record StoryFlags
{
    /// <summary>No modifiers.</summary>
    public static StoryFlags None { get; } = new()
    {
        RequiresUnlock = false,
        Other = Empty.ListOfString
    };

    /// <summary>Whether the story is locked by default.</summary>
    public required bool RequiresUnlock { get; init; }

    /// <summary>Other undocumented flags. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyCollection<string> Other { get; init; }
}
