namespace GuildWars2.Guilds.Members;

[PublicAPI]
[DataTransferObject]
[Inheritable]
public record GuildMember
{
    public required string Name { get; init; }

    public required string Rank { get; init; }

    /// <summary>When the member joined the guild.</summary>
    /// <remarks>May also be null — the join date was not tracked before around March 19th, 2013.</remarks>
    public required DateTimeOffset? Joined { get; init; }
}
