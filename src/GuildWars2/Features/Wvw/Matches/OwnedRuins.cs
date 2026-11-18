using System.Text.Json.Serialization;

namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the ownership of a ruin.</summary>
[DataTransferObject]
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record OwnedRuins : OwnedObjective;
