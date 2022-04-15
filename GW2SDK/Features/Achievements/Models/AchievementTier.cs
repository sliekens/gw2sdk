﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
[DataTransferObject]
public sealed record AchievementTier
{
    public int Count { get; init; }

    public int Points { get; init; }
}
