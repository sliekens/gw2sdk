﻿using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a logging axe, which is used to log tree resource nodes to obtain logs.</summary>
/// <remarks>For the weapon type, see <see cref="Axe" />.</remarks>
[PublicAPI]
[JsonConverter(typeof(LoggingAxeJsonConverter))]
public sealed record LoggingAxe : GatheringTool;
