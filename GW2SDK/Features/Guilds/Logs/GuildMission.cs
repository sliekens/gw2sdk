namespace GuildWars2.Guilds.Logs;

/// <summary>A log entry about a started or ended guild mission.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildMission : GuildLogEntry
{
    /// <summary>The ID of the user who started the mission.</summary>
    /// <remarks>Can be empty.</remarks>
    public required string User { get; init; }

    /// <summary>The state to which the mission transitioned.</summary>
    public required Extensible<GuildMissionState> State { get; init; }

    /// <summary>The amount of guild influence awarded.</summary>
    public required int Influence { get; init; }
}
