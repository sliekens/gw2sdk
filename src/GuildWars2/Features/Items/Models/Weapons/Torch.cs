using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a torch.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Torch : Weapon;
