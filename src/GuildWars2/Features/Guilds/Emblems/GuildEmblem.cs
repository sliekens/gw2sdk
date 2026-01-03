namespace GuildWars2.Guilds.Emblems;

/// <summary>Information about the guild's current emblem.</summary>
[DataTransferObject]
public sealed record GuildEmblem
{
    /// <summary>The background image of the guild emblem.</summary>
    public required GuildEmblemBackground Background { get; init; }

    /// <summary>The foreground image of the guild emblem.</summary>
    public required GuildEmblemForeground Foreground { get; init; }

    /// <summary>Contains various modifiers that affect how the emblem is displayed.</summary>
    public required GuildEmblemFlags Flags { get; init; }
}
