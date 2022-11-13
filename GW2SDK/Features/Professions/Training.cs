using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record Training
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required TrainingCategory Category { get; init; }

    public required IReadOnlyCollection<TrainingObjective> Track { get; init; }
}
