using System.ComponentModel;

namespace GuildWars2.Chat;

/// <summary>Represents a selected trait.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum SelectedTrait
{
    // THE VALUES OF THIS ENUM ARE USED IN THE BINARY FORMAT OF THE LINK

    /// <summary>No trait selected.</summary>
    None = 0,

    /// <summary>The top trait is selected.</summary>
    Top = 1,

    /// <summary>The middle trait is selected.</summary>
    Middle = 2,

    /// <summary>The bottom trait is selected.</summary>
    Bottom = 3
}
