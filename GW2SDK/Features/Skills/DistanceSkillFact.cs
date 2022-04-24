using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record DistanceSkillFact : SkillFact
{
    public int Distance { get; init; }
}
