using System.Text.Json.Serialization;

namespace GuildWars2.Wvw.Objectives;

/// <summary>Information about a mercenary camp in World vs. World.</summary>
[DataTransferObject]
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Mercenary : Objective;
