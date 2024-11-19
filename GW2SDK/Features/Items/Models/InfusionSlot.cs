using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an infusion slot of an ascended or legendary item.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(InfusionSlotJsonConverter))]
public sealed record InfusionSlot
{
    /// <summary>Flags that indicate which types of infusions can be used in this slot.</summary>
    public required InfusionSlotFlags Flags { get; init; }

    /// <summary>The ID of the default infusion in this slot. If the item has no default infusion, this value is <c>null</c>.</summary>
    public required int? ItemId { get; init; }
}
