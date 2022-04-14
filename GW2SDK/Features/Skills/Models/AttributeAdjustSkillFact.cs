using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record AttributeAdjustSkillFact : SkillFact
{
    public int? Value { get; init; }

    public AttributeAdjustTarget Target { get; init; }
}