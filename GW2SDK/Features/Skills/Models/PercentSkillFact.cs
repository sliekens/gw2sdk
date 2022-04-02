using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed record PercentSkillFact : SkillFact
    {
        public double Percent { get; init; }
    }
}
