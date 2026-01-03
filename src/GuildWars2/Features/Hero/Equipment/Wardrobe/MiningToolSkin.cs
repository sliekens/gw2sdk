using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a mining tool skin.</summary>
[JsonConverter(typeof(MiningToolSkinJsonConverter))]
public sealed record MiningToolSkin : GatheringToolSkin;
