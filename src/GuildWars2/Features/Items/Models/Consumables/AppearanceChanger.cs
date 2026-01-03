using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that changes a character's appearance, for example Total Makeover Kit.</summary>
[JsonConverter(typeof(AppearanceChangerJsonConverter))]
public sealed record AppearanceChanger : Consumable;
