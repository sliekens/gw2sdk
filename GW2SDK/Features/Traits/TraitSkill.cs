using System.Diagnostics;
using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class TraitSkill
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public TraitFact[] Facts { get; set; } = new TraitFact[0];

        public TraitFact[]? TraitedFacts { get; set; }

        public string Description { get; set; } = "";

        public string Icon { get; set; } = "";

        public TraitSkillFlag[] Flags { get; set; } = new TraitSkillFlag[0];

        public string ChatLink { get; set; } = "";

        public SkillCategoryName[]? Categories { get; set; }
    }
}
