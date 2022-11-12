using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record Mount
{
    public required MountName Id { get; init; }

    public required string Name { get; init; }

    public required int DefaultSkin { get; init; }

    public required IReadOnlyCollection<int> Skins { get; init; }

    public required IReadOnlyCollection<SkillReference> Skills { get; init; }
}
