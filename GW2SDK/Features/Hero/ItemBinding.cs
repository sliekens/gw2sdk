using System.ComponentModel;

namespace GuildWars2.Hero;

/// <summary>Represents the types of item binding in Guild Wars 2.</summary>
[PublicAPI]
[DefaultValue(None)]
public enum ItemBinding
{
    /// <summary>The item is not bound and can be traded with other players.</summary>
    None,

    /// <summary>The item can only be used on the current player's account.</summary>
    Account,

    /// <summary>The item can only be used by a soulbound character on the current player's account.</summary>
    Character
}
