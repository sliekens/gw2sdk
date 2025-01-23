using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a foraging tool skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ForagingToolSkinJsonConverter))]
public sealed record ForagingToolSkin : GatheringToolSkin;
