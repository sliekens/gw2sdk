namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a member of a guild's PvP team.</summary>
[DataTransferObject]
public sealed record GuildTeamMember
{
    /// <summary>The user ID of the member.</summary>
    public required string Name { get; init; }

    /// <summary>The member's team role.</summary>
    public required Extensible<GuildTeamRole> Role { get; init; }
}
