using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Authorization;

#pragma warning disable CA1711 // Identifiers should not have incorrect suffix

/// <summary>Represents the permissions available for Guild Wars 2 authorization.</summary>
[JsonConverter(typeof(PermissionJsonConverter))]
[DefaultValue(None)]
public enum Permission
{
    /// <summary>No permissions granted.</summary>
    None,

    /// <summary>Grants access to the account information.</summary>
    Account,

    /// <summary>Grants access to the builds information.</summary>
    Builds,

    /// <summary>Grants access to the characters information.</summary>
    Characters,

    /// <summary>Grants access to the guilds information.</summary>
    Guilds,

    /// <summary>Grants access to the inventories information.</summary>
    Inventories,

    /// <summary>Grants access to the progression information.</summary>
    Progression,

    /// <summary>Grants access to the PvP information.</summary>
    PvP,

    /// <summary>Grants access to the unlocks information.</summary>
    Unlocks,

    /// <summary>Grants access to the wallet information.</summary>
    Wallet,

    /// <summary>Grants access to the trading post information.</summary>
    TradingPost,

    /// <summary>Grants access to the WvW guild information.</summary>
    Wvw
}
