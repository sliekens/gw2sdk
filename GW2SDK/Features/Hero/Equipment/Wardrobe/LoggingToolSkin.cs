﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a logging tool skin.</summary>
[PublicAPI]
[JsonConverter(typeof(LoggingToolSkinJsonConverter))]
public sealed record LoggingToolSkin : GatheringToolSkin;
