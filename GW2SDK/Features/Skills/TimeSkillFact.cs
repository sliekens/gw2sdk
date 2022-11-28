using System;
using JetBrains.Annotations;

namespace GuildWars2.Skills;

[PublicAPI]
public sealed record TimeSkillFact : SkillFact
{
    public required TimeSpan Duration { get; init; }
}
