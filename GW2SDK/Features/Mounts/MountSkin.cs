using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record MountSkin
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Icon { get; init; } = "";

    public IReadOnlyCollection<DyeSlot> DyeSlots { get; init; } = Array.Empty<DyeSlot>();

    public MountName Mount { get; init; }
}
