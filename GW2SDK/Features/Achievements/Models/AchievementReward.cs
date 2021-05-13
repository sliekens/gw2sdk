﻿using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public record AchievementReward;
}