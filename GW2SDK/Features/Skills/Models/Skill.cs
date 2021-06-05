using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    public record Skill
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public SkillFact[]? Facts { get; init; }

        public TraitedSkillFact[]? TraitedFacts { get; init; }

        public string Description { get; init; } = "";

        public string? Icon { get; init; } = "";

        public WeaponType? WeaponType { get; init; }

        public ProfessionName[]? Professions { get; init; }

        public SkillSlot? Slot { get; init; }

        public string ChatLink { get; init; } = "";

        public SkillFlag[] SkillFlag { get; init; } = Array.Empty<SkillFlag>();

        public SkillCategoryName[]? Categories { get; init; }

        public int? FlipSkill { get; init; }

        public int? NextChain { get; init; }

        public int? PreviousChain { get; init; }
    }
}
