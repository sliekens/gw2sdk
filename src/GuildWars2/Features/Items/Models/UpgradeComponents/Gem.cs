using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a universal upgrade component, such as a gem.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Gem : UpgradeComponent;
