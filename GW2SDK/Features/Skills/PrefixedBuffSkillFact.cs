namespace GuildWars2.Skills;

[PublicAPI]
public sealed record PrefixedBuffSkillFact : BuffSkillFact
{
    public required BuffPrefix Prefix { get; init; }
}
