using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed record ComboFinisherSkillFact : SkillFact
    {
        public int Percent { get; init; }

        public ComboFinisherName FinisherName { get; init; }
    }
}