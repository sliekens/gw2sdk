
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a shortbow.</summary>
[PublicAPI]
[JsonConverter(typeof(ShortbowJsonConverter))]
public sealed record Shortbow : Weapon;
