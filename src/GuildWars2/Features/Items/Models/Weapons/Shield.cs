using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a shield.</summary>
[JsonConverter(typeof(ShieldJsonConverter))]
public sealed record Shield : Weapon;
