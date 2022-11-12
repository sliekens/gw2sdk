﻿using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Minipets;

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
