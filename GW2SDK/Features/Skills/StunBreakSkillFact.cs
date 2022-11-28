using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record StunBreakSkillFact : SkillFact
{
    public required bool Value { get; init; }
}
