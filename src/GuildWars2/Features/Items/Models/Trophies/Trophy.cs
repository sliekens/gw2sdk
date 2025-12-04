using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a trophy, which generally drops from monsters. It can have one of several uses.</summary>
[JsonConverter(typeof(TrophyJsonConverter))]
public sealed record Trophy : Item;
