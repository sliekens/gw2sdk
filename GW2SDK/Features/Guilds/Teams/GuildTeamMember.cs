namespace GuildWars2.Guilds.Teams;

[PublicAPI]
[DataTransferObject]
public sealed record GuildTeamMember
{
    public required string Name { get; init; }

    public required GuildTeamRole Role { get; init; }
}
