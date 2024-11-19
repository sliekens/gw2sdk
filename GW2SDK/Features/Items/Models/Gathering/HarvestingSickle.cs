using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a harvesting sickle, which is used to harvest plant resource nodes to obtain ingredients.</summary>
[PublicAPI]
[JsonConverter(typeof(HarvestingSickleJsonConverter))]
public sealed record HarvestingSickle : GatheringTool;
