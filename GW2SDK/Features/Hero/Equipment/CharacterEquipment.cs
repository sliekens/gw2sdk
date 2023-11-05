namespace GuildWars2.Hero.Equipment;

/// <summary>Information about items equipped on the character.</summary>
[PublicAPI]
public sealed record CharacterEquipment
{
    /// <summary>All the items equipped by the current character. This includes items from all equipment tabs, not just the
    /// current tab.</summary>
    public required IReadOnlyList<EquipmentItem> Items { get; init; }
}
