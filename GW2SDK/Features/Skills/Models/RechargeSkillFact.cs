using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record RechargeSkillFact : SkillFact
{
    public double Value { get; init; }
}