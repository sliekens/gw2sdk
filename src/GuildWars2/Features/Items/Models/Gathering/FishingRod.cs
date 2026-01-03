using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a fishing rod, which is used for fishing activities.</summary>
[JsonConverter(typeof(FishingRodJsonConverter))]
public sealed record FishingRod : GatheringTool;
