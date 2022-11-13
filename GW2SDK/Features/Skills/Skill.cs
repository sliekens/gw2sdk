using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skill
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<SkillFact>? Facts { get; init; }

    public required IReadOnlyCollection<TraitedSkillFact>? TraitedFacts { get; init; }

    public required string Description { get; init; }

    public required string? Icon { get; init; }

    public required WeaponType? WeaponType { get; init; }

    public required IReadOnlyCollection<ProfessionName>? Professions { get; init; }

    public required SkillSlot? Slot { get; init; }

    public required string ChatLink { get; init; }

    public required IReadOnlyCollection<SkillFlag> SkillFlag { get; init; }

    public required IReadOnlyCollection<SkillCategoryName>? Categories { get; init; }

    public required int? FlipSkill { get; init; }

    public required int? NextChain { get; init; }

    public required int? PreviousChain { get; init; }

    public required int? Specialization { get; init; }
}
