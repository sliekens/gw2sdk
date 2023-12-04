namespace GuildWars2.Guilds.Logs;

/// <summary>The guild upgrade actions.</summary>
[PublicAPI]
public enum GuildUpgradeAction
{
    /// <summary>The upgrade was queued for processing.</summary>
    Queued = 1,

    /// <summary>The upgrade was cancelled.</summary>
    Cancelled,

    /// <summary>The upgrade was completed.</summary>
    Completed,

    /// <summary>The upgrade was sped up.</summary>
    SpedUp
}
