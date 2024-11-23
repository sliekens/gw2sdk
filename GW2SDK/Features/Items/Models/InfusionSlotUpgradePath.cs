using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an upgraded item that gains an extra infusion slot.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(InfusionSlotUpgradePathJsonConverter))]
public sealed record InfusionSlotUpgradePath
{
    /// <summary>Indicates whether the item is infused or attuned in this upgrade step.</summary>
    public required Extensible<InfusionSlotUpgradeKind> Upgrade { get; init; }

    /// <summary>The ID of the upgraded item.</summary>
    public required int ItemId { get; init; }
}
