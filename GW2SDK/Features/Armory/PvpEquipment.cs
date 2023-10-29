namespace GuildWars2.Armory;

/// <summary>Information about the equipped PvP amulet, rune, and sigils on the character.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record PvpEquipment
{
    /// <summary>The PvP amulet ID of the selected amulet.</summary>
    public required int? AmuletId { get; init; }

    /// <summary>The PvP rune ID of the selected rune.</summary>
    public required int? RuneId { get; init; }

    /// <summary>The PvP sigil IDs of all equipped sigils.</summary>
    public required IReadOnlyList<int?> SigilIds { get; init; }
}
