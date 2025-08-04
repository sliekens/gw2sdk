using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an equipment template expansion, which adds an extra equipment template tab to the character
/// when consumed.</summary>
[JsonConverter(typeof(EquipmentTemplateExpansionJsonConverter))]
public sealed record EquipmentTemplateExpansion : Unlocker;
