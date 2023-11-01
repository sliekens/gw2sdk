namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record DamageSkillFact : SkillFact
{
    public required int HitCount { get; init; }

    public required double DamageMultiplier { get; init; }
}
