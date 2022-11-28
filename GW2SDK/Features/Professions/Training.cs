using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record Training
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required TrainingCategory Category { get; init; }

    public required IReadOnlyCollection<TrainingObjective> Track { get; init; }
}
