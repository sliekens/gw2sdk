using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record Pet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Icon { get; init; }

    public required IReadOnlyCollection<PetSkill> Skills { get; init; }
}
