﻿using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an accessory.</summary>
[PublicAPI]
[JsonConverter(typeof(AccessoryJsonConverter))]
public sealed record Accessory : Trinket;
