using System.Text.Json.Serialization;

using GuildWars2.Hero.Accounts;

namespace GuildWars2.Tests.Common;

[JsonSerializable(typeof(Coin))]
[JsonSerializable(typeof(Items.Item))]
[JsonSerializable(typeof(Hero.Crafting.Disciplines.LearnedCraftingDisciplines))]
[JsonSerializable(typeof(Hero.Crafting.Recipes.Recipe))]
[JsonSerializable(typeof(Hero.Equipment.MailCarriers.MailCarrier))]
[JsonSerializable(typeof(Hero.Equipment.JadeBots.JadeBotSkin))]
[JsonSerializable(typeof(Hero.Equipment.Gliders.GliderSkin))]
[JsonSerializable(typeof(Hero.Equipment.Outfits.Outfit))]
[JsonSerializable(typeof(Hero.Equipment.Skiffs.SkiffSkin))]
[JsonSerializable(typeof(Hero.Equipment.Finishers.Finisher))]
[JsonSerializable(typeof(Hero.Equipment.Finishers.UnlockedFinisher))]
[JsonSerializable(typeof(Hero.Equipment.Mounts.Mount))]
[JsonSerializable(typeof(Hero.Equipment.Templates.BoundLegendaryItem))]
[JsonSerializable(typeof(Hero.Equipment.Mounts.MountSkin))]
[JsonSerializable(typeof(Hero.Equipment.Wardrobe.EquipmentSkin))]
[JsonSerializable(typeof(Hero.Equipment.Dyes.DyeColor))]
[JsonSerializable(typeof(Hero.Equipment.Templates.CharacterEquipment))]
[JsonSerializable(typeof(Hero.Equipment.Templates.EquipmentTemplate))]
[JsonSerializable(typeof(Hero.Equipment.Templates.LegendaryItem))]
[JsonSerializable(typeof(Hero.Equipment.Miniatures.Miniature))]
[JsonSerializable(typeof(Hero.Equipment.Novelties.Novelty))]
[JsonSerializable(typeof(Hero.Achievements.Groups.AchievementGroup))]
[JsonSerializable(typeof(Hero.Achievements.Titles.Title))]
[JsonSerializable(typeof(Hero.Achievements.Categories.AchievementCategory))]
[JsonSerializable(typeof(Hero.Achievements.Achievement))]
[JsonSerializable(typeof(Hero.Achievements.AccountAchievement))]
[JsonSerializable(typeof(Extensible<ProductName>))]
[JsonSerializable(typeof(ProductName))]
public partial class TestJsonContext : JsonSerializerContext
{
}
