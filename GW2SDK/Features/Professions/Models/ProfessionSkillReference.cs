using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    public sealed record ProfessionSkillReference : SkillReference
    {
        /// <summary>In case of stolen skills (Thief only), the name of the profession from which it can be stolen.</summary>
        public ProfessionName? Source { get; init; }

        /// <summary>In case of elemental skills (Elementalist only), the name of the attunement that needs to be active.</summary>
        public Attunement? Attunement { get; init; }
    }
}
