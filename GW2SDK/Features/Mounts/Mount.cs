using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record Mount
{
    public MountName Id { get; init; }

    public string Name { get; init; } = "";

    public int DefaultSkin { get; init; }

    public IReadOnlyCollection<int> Skins { get; init; } = Array.Empty<int>();

    public IReadOnlyCollection<SkillReference> Skills { get; init; } = Array.Empty<SkillReference>();
}