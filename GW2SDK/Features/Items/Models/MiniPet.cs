﻿using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record Minipet : Item
    {
        public int MinipetId { get; init; }
    }
}
