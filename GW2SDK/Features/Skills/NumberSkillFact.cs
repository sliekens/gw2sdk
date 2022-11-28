using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record NumberSkillFact : SkillFact
{
    public required int Value { get; init; }
}
