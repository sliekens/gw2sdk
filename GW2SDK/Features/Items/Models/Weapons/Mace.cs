﻿using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a mace.</summary>
[PublicAPI]
[JsonConverter(typeof(MaceJsonConverter))]
public sealed record Mace : Weapon;
