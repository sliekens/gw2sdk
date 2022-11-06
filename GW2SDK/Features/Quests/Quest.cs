using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Quests;

[PublicAPI]
[DataTransferObject]
public sealed record Quest
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public int Level { get; init; }

    public int Story { get; init; }

    public IReadOnlyCollection<Goal> Goals { get; init; } = Array.Empty<Goal>();
}
