using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record AttributeAdjustSkillFact : SkillFact
{
    public required int? Value { get; init; }

    public required AttributeAdjustTarget Target { get; init; }
}
