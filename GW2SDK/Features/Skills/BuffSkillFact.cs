using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
[Inheritable]
public record BuffSkillFact : SkillFact
{
    /// <summary>The duration of the effect applied by the skill, or null when the effect is removed by the skill.</summary>
    public TimeSpan? Duration { get; init; }

    public string Status { get; init; } = "";

    public string Description { get; init; } = "";

    /// <summary>The number of stacks applied by the skill, or null when the effect is removed by the skill.</summary>
    public int? ApplyCount { get; init; }
}
