using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record RangeSkillFact : SkillFact
{
    public required int Value { get; init; }
}
