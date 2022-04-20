﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
[DataTransferObject]
public sealed record DailyAchievement
{
    public int Id { get; init; }

    public LevelRequirement Level { get; init; } = new();

    public ProductRequirement? RequiredAccess { get; init; }
}