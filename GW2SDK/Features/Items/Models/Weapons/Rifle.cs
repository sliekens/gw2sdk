using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a rifle.</summary>
[PublicAPI]
[JsonConverter(typeof(RifleJsonConverter))]
public sealed record Rifle : Weapon;
