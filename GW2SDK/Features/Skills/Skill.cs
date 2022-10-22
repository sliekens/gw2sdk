using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skill
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public IReadOnlyCollection<SkillFact>? Facts { get; init; }

    public IReadOnlyCollection<TraitedSkillFact>? TraitedFacts { get; init; }

    public string Description { get; init; } = "";

    public string? Icon { get; init; } = "";

    public WeaponType? WeaponType { get; init; }

    public IReadOnlyCollection<ProfessionName>? Professions { get; init; }

    public SkillSlot? Slot { get; init; }

    public string ChatLink { get; init; } = "";

    public IReadOnlyCollection<SkillFlag> SkillFlag { get; init; } = Array.Empty<SkillFlag>();

    public IReadOnlyCollection<SkillCategoryName>? Categories { get; init; }

    public int? FlipSkill { get; init; }

    public int? NextChain { get; init; }

    public int? PreviousChain { get; init; }

    public int? Specialization { get; init; }
}
