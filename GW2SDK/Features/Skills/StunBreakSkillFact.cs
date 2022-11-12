using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record StunBreakSkillFact : SkillFact
{
    public required bool Value { get; init; }
}
