using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a sigil, which is used to upgrade weapons.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Sigil : UpgradeComponent;
