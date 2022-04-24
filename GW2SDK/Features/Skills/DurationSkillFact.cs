using System;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record DurationSkillFact : SkillFact
{
    public TimeSpan Duration { get; init; }
}
