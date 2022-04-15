﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Skills.Models;

[PublicAPI]
public sealed record ProfessionSkill : Skill
{
    public int? Specialization { get; init; }

    public Attunement? Attunement { get; init; }

    public int? Cost { get; init; }

    public IReadOnlyCollection<int>? TransformSkills { get; init; }
}
