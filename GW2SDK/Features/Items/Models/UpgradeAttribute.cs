﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record UpgradeAttribute
    {
        public UpgradeAttributeName Attribute { get; init; }

        public int Modifier { get; init; }
    }
}
