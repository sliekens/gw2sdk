using GuildWars2.Hero;

namespace GuildWars2.Items.Stats;

/// <summary>Information about an attribute like Power, to calculate the effective stats of an item.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Attribute
{
    /// <summary>The name of the attribute.</summary>
    public required Extensible<AttributeName> Name { get; init; }

    /// <summary>The multiplier for the attribute. To calculate the final item stats, multiply an item's Attribute Adjustment value
    /// with this multiplier, then add the result to the base <see cref="Value" />.</summary>
    /// <remarks>The formula is: attribute_adjustment * multiplier + value.</remarks>
    public required double Multiplier { get; init; }

    /// <summary>The base value for the attribute. To calculate the final item stats, multiple an item's Attribute Adjustment value
    /// with the <see cref="Multiplier" />, then add the result to this base value.</summary>
    /// <remarks>The formula is: attribute_adjustment * multiplier + value.</remarks>
    public required int Value { get; init; }
}
