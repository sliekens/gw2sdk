using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a universal upgrade component, such as a gem.</summary>
[PublicAPI]
[JsonConverter(typeof(GemJsonConverter))]
public sealed record Gem : UpgradeComponent;
