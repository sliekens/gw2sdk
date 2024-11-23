using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an axe (weapon).</summary>
/// <remarks>For the logging tool, see <see cref="LoggingAxe" />.</remarks>
[PublicAPI]
[JsonConverter(typeof(AxeJsonConverter))]
public sealed record Axe : Weapon;
