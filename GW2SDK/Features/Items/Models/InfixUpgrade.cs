﻿using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record InfixUpgrade
    {
        public int ItemstatsId { get; init; }

        public UpgradeAttribute[] Attributes { get; init; } = Array.Empty<UpgradeAttribute>();

        public Buff? Buff { get; init; }
    }
}
