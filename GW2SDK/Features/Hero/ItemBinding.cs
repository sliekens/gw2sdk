using System.ComponentModel;

namespace GuildWars2.Hero;

[PublicAPI]
[DefaultValue(None)]
public enum ItemBinding
{
    /// <summary>The item is not bound and can be traded with other players.</summary>
    None,

    /// <summary>The item can only be used on current player's account.</summary>
    Account,

    /// <summary>The item can only be used by a soulbound character on the current player's account.</summary>
    Character
}
