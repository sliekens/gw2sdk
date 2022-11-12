using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record DamageSkillFact : SkillFact
{
    public required int HitCount { get; init; }

    public required double DamageMultiplier { get; init; }
}
