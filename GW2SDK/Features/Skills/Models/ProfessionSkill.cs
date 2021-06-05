using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    public sealed record ProfessionSkill : Skill
    {
        public int? Specialization { get; init; }

        public Attunement? Attunement { get; init; }

        public int? Cost { get; init; }

        public int[]? TransformSkills { get; init; }
    }
}