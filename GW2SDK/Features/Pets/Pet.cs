﻿using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pets;

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