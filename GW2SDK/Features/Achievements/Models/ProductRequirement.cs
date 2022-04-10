﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record ProductRequirement
    {
        public ProductName Product { get; init; }

        public AccessCondition Condition { get; init; }
    }
}