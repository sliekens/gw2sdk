namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record HealingAdjustSkillFact : SkillFact
{
    public required int HitCount { get; init; }
}
