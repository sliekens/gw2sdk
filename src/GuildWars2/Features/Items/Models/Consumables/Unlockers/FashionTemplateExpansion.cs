using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a fashion template expansion, which adds an extra fashion template tab to the character
/// when consumed.</summary>
[JsonConverter(typeof(FashionTemplateExpansionJsonConverter))]
public sealed record FashionTemplateExpansion : Unlocker;
