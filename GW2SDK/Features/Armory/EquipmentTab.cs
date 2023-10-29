namespace GuildWars2.Armory;

/// <summary>Information about an equipment tab on the character.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record EquipmentTab
{
    /// <summary>The tab number of the current tab.</summary>
    public required int Tab { get; init; }

    /// <summary>The player-chosen name for this equipment tab.</summary>
    public required string Name { get; init; }

    /// <summary>The selected equipment on this tab.</summary>
    public required IReadOnlyList<EquipmentItem> Items { get; init; }

    /// <summary>The selected PvP equipment on this tab.</summary>
    public required PvpEquipment PvpEquipment { get; init; }
}
