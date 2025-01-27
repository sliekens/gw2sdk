using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>Information about an equipment tab on the character.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(EquipmentTemplateJsonConverter))]
public sealed record EquipmentTemplate
{
    /// <summary>The tab number of the current template.</summary>
    public required int TabNumber { get; init; }

    /// <summary>The player-chosen name for the current template.</summary>
    public required string Name { get; init; }

    /// <summary>The selected equipment for the current template.</summary>
    public required IReadOnlyList<EquipmentItem> Items { get; init; }

    /// <summary>The selected PvP equipment for the current template.</summary>
    public required PvpEquipment PvpEquipment { get; init; }
}
