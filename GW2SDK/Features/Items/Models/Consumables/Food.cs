using System;
using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record Food : Consumable
{
    public required TimeSpan? Duration { get; init; }

    public required int? ApplyCount { get; init; }

    public required string EffectName { get; init; }

    public required string EffectDescription { get; init; }

    public required string? EffectIcon { get; init; }
}
