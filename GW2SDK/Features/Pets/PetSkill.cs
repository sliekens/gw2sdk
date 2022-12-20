﻿using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record PetSkill
{
    public required int Id { get; init; }
}