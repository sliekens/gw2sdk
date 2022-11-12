using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record HealingAdjustSkillFact : SkillFact
{
    public required int HitCount { get; init; }
}
