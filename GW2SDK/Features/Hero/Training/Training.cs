﻿namespace GuildWars2.Hero.Training;

[PublicAPI]
[DataTransferObject]
public sealed record Training
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required TrainingCategory Category { get; init; }

    public required IReadOnlyCollection<TrainingObjective> Track { get; init; }
}