﻿using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Finishers;

[PublicAPI]
[DataTransferObject]
public sealed record Finisher
{
    public required int Id { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required string UnlockDetails { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<int> UnlockItems { get; init; }

    public required int Order { get; init; }

    public required string Icon { get; init; }

    public required string Name { get; init; }
}
