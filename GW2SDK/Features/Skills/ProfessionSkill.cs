﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record ProfessionSkill : Skill
{
    public required Attunement? Attunement { get; init; }

    public required int? Cost { get; init; }

    public required IReadOnlyCollection<int>? TransformSkills { get; init; }
}
