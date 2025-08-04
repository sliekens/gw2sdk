using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The offhands which may be required for a weapon skill.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(OffhandJsonConverter))]
public enum Offhand
{
    /// <summary>The skill is selected when there is no other skill in the same palette for the current offhand weapon.</summary>
    None,

    /// <summary>The skill is selected when the offhand is empty.</summary>
    Nothing,

    /// <summary>The skill is selected when a dagger is equipped in the offhand.</summary>
    Dagger,

    /// <summary>The skill is selected when a pistol is equipped in the offhand.</summary>
    Pistol
}
