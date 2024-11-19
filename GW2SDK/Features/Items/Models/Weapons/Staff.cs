using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a staff.</summary>
[PublicAPI]
[JsonConverter(typeof(StaffJsonConverter))]
public sealed record Staff : Weapon;
