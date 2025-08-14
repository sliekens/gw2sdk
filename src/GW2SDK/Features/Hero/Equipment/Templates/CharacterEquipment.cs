using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>Information about items equipped on the character.</summary>
[DataTransferObject]
[JsonConverter(typeof(CharacterEquipmentJsonConverter))]
public sealed record CharacterEquipment
{
    /// <summary>All the items equipped by the current character. This includes items from all equipment tabs, not just the
    /// current tab.</summary>
    public required IReadOnlyList<EquipmentItem> Items { get; init; }
}
