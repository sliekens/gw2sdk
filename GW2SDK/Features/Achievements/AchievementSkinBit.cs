﻿using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record AchievementSkinBit : AchievementBit
{
    public required int Id { get; init; }
}
