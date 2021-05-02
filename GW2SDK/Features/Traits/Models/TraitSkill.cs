using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record TraitSkill
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public TraitFact[] Facts { get; init; } = new TraitFact[0];

        public TraitCombinationFact[]? TraitedFacts { get; init; }

        public string Description { get; init; } = "";

        public string Icon { get; init; } = "";

        public string ChatLink { get; init; } = "";

        public SkillCategoryName[]? Categories { get; init; }
    }
}
