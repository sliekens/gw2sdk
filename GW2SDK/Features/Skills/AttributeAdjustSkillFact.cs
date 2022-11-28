using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record AttributeAdjustSkillFact : SkillFact
{
    public required int? Value { get; init; }

    public required AttributeAdjustTarget Target { get; init; }
}
