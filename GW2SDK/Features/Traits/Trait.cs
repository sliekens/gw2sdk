using System.Diagnostics;
using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Trait
    {
        public int Id { get; set; }

        public int Tier { get; set; }

        public int Order { get; set; }

        public string Name { get; set; } = "";

        public string? Description { get; set; }

        public TraitSlot Slot { get; set; }

        public TraitFact[]? Facts { get; set; }

        public TraitFact[]? TraitedFacts { get; set; }

        public TraitSkill[]? Skills { get; set; }

        public int SpezializationId { get; set; }

        public string Icon { get; set; } = "";
    }
}
