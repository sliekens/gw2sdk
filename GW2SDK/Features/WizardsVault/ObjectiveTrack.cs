namespace GuildWars2.WizardsVault;

/// <summary>The Wizard's Vault objective tracks.</summary>
[PublicAPI]
public enum ObjectiveTrack
{
    /// <summary>PvE objectives such as completing events.</summary>
    PvE = 1,

    /// <summary>PvP objectives such as earning a top scoreboard stat.</summary>
    PvP,

    /// <summary>WvW objectives such as defeating a supply caravan.</summary>
    WvW
}
