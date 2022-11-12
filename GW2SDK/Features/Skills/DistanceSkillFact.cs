using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record DistanceSkillFact : SkillFact
{
    public required int Distance { get; init; }
}
