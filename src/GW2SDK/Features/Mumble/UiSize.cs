using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Mumble;

/// <summary>The user interface size choices provided by the game client.</summary>
[DefaultValue(Small)]
[JsonConverter(typeof(UiSizeJsonConverter))]
public enum UiSize
{
    /// <summary>Smallest user interface size.</summary>
    Small,

    /// <summary>Normal user interface size.</summary>
    Normal,

    /// <summary>Larger user interface size.</summary>
    Large,

    /// <summary>Largest user interface size.</summary>
    Larger
}
