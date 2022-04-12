using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record RangeSkillFact : SkillFact
{
    public int Value { get; init; }
}