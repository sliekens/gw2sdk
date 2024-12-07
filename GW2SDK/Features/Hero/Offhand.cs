using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The offhands which may be required for a weapon skill.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(OffhandJsonConverter))]
public enum Offhand
{
    /// <summary>Nothing in the offhand (empty).</summary>
    None,

    /// <summary>Nothing in the offhand (empty).</summary>
    Nothing,

    /// <summary>A dagger in the offhand.</summary>
    Dagger,

    /// <summary>A pistol in the offhand.</summary>
    Pistol
}
