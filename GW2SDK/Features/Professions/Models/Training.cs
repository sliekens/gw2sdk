using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Professions.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Training
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public TrainingCategory Category { get; init; }

    public IReadOnlyCollection<TrainingObjective> Track { get; set; } = Array.Empty<TrainingObjective>();
}