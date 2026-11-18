using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Builds.Facts;

/// <summary>An informational fact that does not have any data, only a description.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record NoData : Fact;
