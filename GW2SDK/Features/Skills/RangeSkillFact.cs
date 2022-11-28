using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record RangeSkillFact : SkillFact
{
    public required int Value { get; init; }
}
