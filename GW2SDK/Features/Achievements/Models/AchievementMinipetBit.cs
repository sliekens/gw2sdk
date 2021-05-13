﻿using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record AchievementMinipetBit : AchievementBit
    {
        public int Id { get; init; }
    }
}