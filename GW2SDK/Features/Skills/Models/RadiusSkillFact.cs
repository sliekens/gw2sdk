using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record RadiusSkillFact : SkillFact
{
    public int Distance { get; init; }
}