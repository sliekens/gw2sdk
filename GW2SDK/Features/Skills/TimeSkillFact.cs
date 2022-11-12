using System;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record TimeSkillFact : SkillFact
{
    public required TimeSpan Duration { get; init; }
}
