using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record PetSkillBar
{
    public required IReadOnlyCollection<int?> Terrestrial { get; init; }

    public required IReadOnlyCollection<int?> Aquatic { get; init; }
}
