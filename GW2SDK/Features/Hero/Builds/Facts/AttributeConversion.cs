namespace GuildWars2.Hero.Builds.Facts;

/// <summary>An attribute which is converted into another attribute by a trait.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AttributeConversion : Fact
{
    /// <summary>How much of the <see cref="Source" /> attribute is converted to the <see cref="Target" /> attribute by the
    /// trait. Expressed as a percentage, where 100 is 100% chance.</summary>
    public required int Percent { get; init; }

    /// <summary>The attribute that is used to calculate by how much to increase the <see cref="Target" /> attribute. (The
    /// source attribute is not decreased.)</summary>
    public required AttributeAdjustmentTarget Source { get; init; }

    /// <summary>The attribute that is increased by the <see cref="Percent" /> of the <see cref="Source" /> attribute.</summary>
    public required AttributeAdjustmentTarget Target { get; init; }
}
