using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record PercentSkillFact : SkillFact
{
    public double Percent { get; init; }
}
