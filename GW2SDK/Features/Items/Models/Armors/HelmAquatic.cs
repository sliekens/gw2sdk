using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about aquatic head armor.</summary>
[PublicAPI]
[JsonConverter(typeof(HelmAquaticJsonConverter))]
public sealed record HelmAquatic : Armor;
