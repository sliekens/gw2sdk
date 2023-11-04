﻿namespace GuildWars2.Builds.Facts;

/// <summary>An attribute that is modified by the skill/trait.</summary>
[PublicAPI]
public sealed record AttributeAdjustment : Fact
{
    /// <summary>The amount by which the attribute is modified, based on a level 80 character.</summary>
    public required int? Value { get; init; }

    /// <summary>The attribute that is modified. If the target is <see cref="AttributeAdjustmentTarget.Healing" /> then the
    /// <see cref="Value" /> is the amount of health that is recovered.</summary>
    public required AttributeAdjustmentTarget Target { get; init; }
}