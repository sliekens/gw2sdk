using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements;

/// <summary>Information about a collection achievement and which items, skins or miniatures need to be obtained to
/// complete it.</summary>
[JsonConverter(typeof(CollectionAchievementJsonConverter))]
public sealed record CollectionAchievement : Achievement;
