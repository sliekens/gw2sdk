﻿using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Skins;

[PublicAPI]
[Inheritable]
public record WeaponSkin : Skin
{
    public required DamageType DamageType { get; init; }
}
