namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record StunBreakSkillFact : SkillFact
{
    public required bool Value { get; init; }
}
