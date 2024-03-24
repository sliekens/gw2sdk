namespace GuildWars2.Pve.Dungeons;

/// <summary>The kind of paths in a dungeon.</summary>
[PublicAPI]
public enum DungeonKind
{
    /// <summary>A story path, which does not reward dungeon tokens, only some coins.</summary>
    Story = 1,

    /// <summary>An explorable path, which has better rewards but also higher difficulty.</summary>
    Explorable
}
