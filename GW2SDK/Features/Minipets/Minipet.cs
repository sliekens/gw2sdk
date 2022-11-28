﻿using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Minipets;

[PublicAPI]
[DataTransferObject]
public sealed record Minipet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Unlock { get; init; }

    public required string Icon { get; init; }

    public required int Order { get; init; }

    public required int ItemId { get; init; }
}
