using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero;

/// <summary>The attunement of an Elementalist.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(AttunementJsonConverter))]
public enum Attunement
{
    /// <summary>No specific attunement or unknown attunement.</summary>
    None,

    /// <summary>Attunement to Earth.</summary>
    Earth,

    /// <summary>Attunement to Water.</summary>
    Water,

    /// <summary>Attunement to Air.</summary>
    Air,

    /// <summary>Attunement to Fire.</summary>
    Fire
}
