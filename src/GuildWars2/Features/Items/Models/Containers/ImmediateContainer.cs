using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a container that is opened immediately upon receipt.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record ImmediateContainer : Container;
