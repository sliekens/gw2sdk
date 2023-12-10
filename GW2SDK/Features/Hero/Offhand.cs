using System.ComponentModel;

namespace GuildWars2.Hero;

/// <summary>The offhands which may be required for a weapon skill.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum Offhand
{
    /// <summary>Nothing in the offhand (empty).</summary>
    None,

    /// <summary>Nothing in the offhand (empty).</summary>
    Nothing = None,

    /// <summary>A dagger in the offhand.</summary>
    Dagger,

    /// <summary>A pistol in the offhand.</summary>
    Pistol
}
