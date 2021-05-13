using System;
using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record Utility : Consumable
    {
        public TimeSpan? Duration { get; init; }

        public int? ApplyCount { get; init; }

        public string EffectName { get; init; } = "";

        public string EffectDescription { get; init; } = "";

        public string? EffectIcon { get; init; }
    }
}
