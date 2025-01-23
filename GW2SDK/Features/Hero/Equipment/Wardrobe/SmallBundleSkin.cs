﻿using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a small bundle skin.</summary>
[PublicAPI]
[JsonConverter(typeof(SmallBundleSkinJsonConverter))]
public sealed record SmallBundleSkin : WeaponSkin;
