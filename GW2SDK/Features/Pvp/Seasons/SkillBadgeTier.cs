namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record SkillBadgeTier
{
    public required int Rating { get; init; }
}
