using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a sword.</summary>
[PublicAPI]
[JsonConverter(typeof(SwordJsonConverter))]
public sealed record Sword : Weapon;
