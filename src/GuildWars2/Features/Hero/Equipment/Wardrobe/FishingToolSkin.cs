using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a fishing tool skin.</summary>
[JsonConverter(typeof(FishingToolSkinJsonConverter))]
public sealed record FishingToolSkin : GatheringToolSkin;
