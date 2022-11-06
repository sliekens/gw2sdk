using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record Pet
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public string Icon { get; init; } = "";

    public IReadOnlyCollection<PetSkill> Skills { get; init; } = Array.Empty<PetSkill>();
}
