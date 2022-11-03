﻿using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.SkillChallenge;

[PublicAPI]
[DataTransferObject]
public sealed record SkillChallenge
{
    public string Id { get; init; } = "";

    public IReadOnlyCollection<double> Coordinates { get; init; } = Array.Empty<double>();
}