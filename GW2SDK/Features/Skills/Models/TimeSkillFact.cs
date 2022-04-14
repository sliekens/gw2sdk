﻿using System;

using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record TimeSkillFact : SkillFact
{
    public TimeSpan Duration { get; init; }
}