﻿using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a build template expansion, which adds an extra build template tab to the character when
/// consumed.</summary>
[PublicAPI]
[JsonConverter(typeof(BuildTemplateExpansionJsonConverter))]
public sealed record BuildTemplateExpansion : Unlocker;
