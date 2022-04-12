using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record DamageSkillFact : SkillFact
{
    public int HitCount { get; init; }

    public double DamageMultiplier { get; init; }
}