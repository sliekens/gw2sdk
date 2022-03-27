using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record TraitSkill
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public IReadOnlyCollection<TraitFact> Facts { get; init; } = Array.Empty<TraitFact>();

        public IReadOnlyCollection<CompoundTraitFact>? TraitedFacts { get; init; }

        public string Description { get; init; } = "";

        public string Icon { get; init; } = "";

        public string ChatLink { get; init; } = "";

        public IReadOnlyCollection<SkillCategoryName>? Categories { get; init; }
    }
}
