using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record DamageSkillFact : SkillFact
{
    public required int HitCount { get; init; }

    public required double DamageMultiplier { get; init; }
}
