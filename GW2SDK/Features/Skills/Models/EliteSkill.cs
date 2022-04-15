using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record EliteSkill : Skill
{
    public int? Specialization { get; init; }

    public int? ToolbeltSkill { get; init; }

    public Attunement? Attunement { get; init; }

    public int? Cost { get; init; }

    public IReadOnlyCollection<int>? BundleSkills { get; init; }

    public IReadOnlyCollection<int>? TransformSkills { get; init; }

    public IReadOnlyCollection<SkillReference>? Subskills { get; init; }
}
