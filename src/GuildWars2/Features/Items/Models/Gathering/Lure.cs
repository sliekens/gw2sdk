using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a fishing lure, which increases your fishing power.</summary>
[JsonConverter(typeof(LureJsonConverter))]
public sealed record Lure : GatheringTool;
