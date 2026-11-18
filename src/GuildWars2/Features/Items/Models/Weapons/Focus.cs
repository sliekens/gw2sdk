using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a focus.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Focus : Weapon;
