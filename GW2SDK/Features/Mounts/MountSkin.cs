﻿using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record MountSkin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Icon { get; init; }

    public required IReadOnlyCollection<DyeSlot> DyeSlots { get; init; }

    public required MountName Mount { get; init; }
}
