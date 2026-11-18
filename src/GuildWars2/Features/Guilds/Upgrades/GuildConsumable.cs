using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild consumable upgrade, for example a banner.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record GuildConsumable : GuildUpgrade;
