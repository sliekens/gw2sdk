using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a pickaxe, which is used to mine metal resource nodes to obtain ore.</summary>
[JsonConverter(typeof(MiningPickJsonConverter))]
public sealed record MiningPick : GatheringTool;
