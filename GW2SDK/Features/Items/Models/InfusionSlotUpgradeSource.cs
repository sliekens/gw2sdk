namespace GuildWars2.Items;

/// <summary>Information about a source item that is used in a recipe for an infused or attuned item.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record InfusionSlotUpgradeSource
{
    /// <summary>Indicates whether the item is infused or attuned in this upgrade step.</summary>
    public required InfusionSlotUpgradeKind Upgrade { get; init; }

    /// <summary>The ID of the source item.</summary>
    public required int ItemId { get; init; }
}
