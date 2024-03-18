﻿namespace GuildWars2.Hero.StoryJournal.Stories;

/// <summary>Modifiers for stories.</summary>
[PublicAPI]
public sealed record StoryFlags : Flags
{
    /// <summary>No modifiers.</summary>
    public static StoryFlags None { get; } = new()
    {
        RequiresUnlock = false,
        Other = Empty.ListOfString
    };

    /// <summary>Whether the story is locked by default.</summary>
    public required bool RequiresUnlock { get; init; }
}
