using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pve.Dungeons;

/// <summary>The kind of paths in a dungeon.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(DungeonKindJsonConverter))]
public enum DungeonKind
{
    /// <summary>No specific path or unknown path.</summary>
    None,

    /// <summary>A story path, which does not reward dungeon tokens, only some coins.</summary>
    Story,

    /// <summary>An explorable path, which has better rewards but also higher difficulty.</summary>
    Explorable
}
