namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record PrefixedBuffSkillFact : BuffSkillFact
{
    public required BuffPrefix Prefix { get; init; }
}
