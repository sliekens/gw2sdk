﻿using System;
using System.Diagnostics;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements.Groups
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class AchievementGroup
    {
        [NotNull]
        public string Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Description { get; set; }

        public int Order { get; set; }

        [NotNull]
        public int[] Categories { get; set; }
    }
}
