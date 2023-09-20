﻿namespace GuildWars2.Items;

[PublicAPI]
public sealed record Rune : UpgradeComponent
{
    public required IReadOnlyCollection<string>? Bonuses { get; init; }
}
