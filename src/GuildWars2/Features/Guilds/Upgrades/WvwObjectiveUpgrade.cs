using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about Tactics or Improvements for WvW objectives, for example Emergency Waypoints.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record WvwObjectiveUpgrade : GuildUpgrade;
