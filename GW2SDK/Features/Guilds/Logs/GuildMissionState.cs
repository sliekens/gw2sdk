namespace GuildWars2.Guilds.Logs;

/// <summary>The guild mission state transitions.</summary>
[PublicAPI]
public enum GuildMissionState
{
    /// <summary>Logged when the mission starts.</summary>
    Start = 1,

    /// <summary>Logged when the mission ends successfully.</summary>
    Success,

    /// <summary>Logged when the mission ends unsuccessfully.</summary>
    Fail
}
