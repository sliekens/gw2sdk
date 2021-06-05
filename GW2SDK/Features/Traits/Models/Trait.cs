using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Trait
    {
        public int Id { get; init; }

        public int Tier { get; init; }

        public int Order { get; init; }

        public string Name { get; init; } = "";

        public string Description { get; init; } = "";

        public TraitSlot Slot { get; init; }

        public TraitFact[]? Facts { get; init; }

        public CompoundTraitFact[]? TraitedFacts { get; init; }

        public TraitSkill[]? Skills { get; init; }

        public int SpezializationId { get; init; }

        public string Icon { get; init; } = "";
    }
}
