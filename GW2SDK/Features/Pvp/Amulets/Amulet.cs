﻿using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Amulets;

[PublicAPI]
[DataTransferObject]
public sealed record Amulet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Icon { get; init; }

    public required IDictionary<AttributeAdjustTarget, int> Attributes { get; init; }
}