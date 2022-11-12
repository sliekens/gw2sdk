using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record UtilitySkill : Skill
{
    public required int? ToolbeltSkill { get; init; }

    public required Attunement? Attunement { get; init; }

    public required int? Cost { get; init; }

    public required IReadOnlyCollection<int>? BundleSkills { get; init; }

    public required IReadOnlyCollection<SkillReference>? Subskills { get; init; }
}
