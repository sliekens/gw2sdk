using System.ComponentModel;

namespace GuildWars2.Mumble;

/// <summary>The user interface size choices provided by the game client.</summary>
[PublicAPI]
[DefaultValue(Small)]
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
