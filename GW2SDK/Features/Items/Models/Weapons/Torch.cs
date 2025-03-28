﻿using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a torch.</summary>
[PublicAPI]
[JsonConverter(typeof(TorchJsonConverter))]
public sealed record Torch : Weapon;
