using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a build template expansion, which adds an extra build template tab to the character when
/// consumed.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record BuildTemplateExpansion : Unlocker;
