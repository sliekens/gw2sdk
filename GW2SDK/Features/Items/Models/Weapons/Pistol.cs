using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a pistol.</summary>
[PublicAPI]
[JsonConverter(typeof(PistolJsonConverter))]
public sealed record Pistol : Weapon;
