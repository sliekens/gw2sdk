using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about aquatic head armor.</summary>
[JsonConverter(typeof(HelmAquaticJsonConverter))]
public sealed record HelmAquatic : Armor;
