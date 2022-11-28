using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record RechargeSkillFact : SkillFact
{
    public required double Value { get; init; }
}
