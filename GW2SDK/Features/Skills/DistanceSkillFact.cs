using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record DistanceSkillFact : SkillFact
{
    public required int Distance { get; init; }
}
