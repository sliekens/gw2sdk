using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record HealingAdjustSkillFact : SkillFact
{
    public int HitCount { get; init; }
}
