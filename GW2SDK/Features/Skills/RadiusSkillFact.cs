using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record RadiusSkillFact : SkillFact
{
    public required int Distance { get; init; }
}
