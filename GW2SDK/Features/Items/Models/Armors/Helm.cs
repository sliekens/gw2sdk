using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about head armor.</summary>
[PublicAPI]
[JsonConverter(typeof(HelmJsonConverter))]
public sealed record Helm : Armor;
