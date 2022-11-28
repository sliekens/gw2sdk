using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record HealingAdjustSkillFact : SkillFact
{
    public required int HitCount { get; init; }
}
