using System.Text.Json.Serialization;

namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the ownership of a spawn camp where players enter the map in World vs. World.</summary>
[DataTransferObject]
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record OwnedSpawn : OwnedObjective;
