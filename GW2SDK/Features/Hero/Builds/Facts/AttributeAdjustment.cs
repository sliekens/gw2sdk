namespace GuildWars2.Hero.Builds.Facts;

/// <summary>An attribute that is modified by the skill/trait.</summary>
[PublicAPI]
public sealed record AttributeAdjustment : Fact
{
    /// <summary>The amount by which the attribute is modified, based on a level 80 character.</summary>
    public required int? Value { get; init; }

    /// <summary>The attribute that is modified. If the target is  <see cref="AttributeName.HealingPower" /> then the
    /// <see cref="Value" /> is the amount of health that is recovered.</summary>
    public required Extensible<AttributeName>? Target { get; init; }

    /// <summary>If present, the modifier is active for the indicated number of hits. For example, if <see cref="Fact.Text" />
    /// is First-Hit Healing and the hit count is 3, then the next 3 hits will apply healing.</summary>
    public required int? HitCount { get; init; }
}
