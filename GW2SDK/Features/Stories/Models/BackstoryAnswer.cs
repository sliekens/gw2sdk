using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Stories.Models;

[PublicAPI]
[DataTransferObject]
public sealed record BackstoryAnswer
{
    public string Id { get; init; } = "";

    public string Title { get; init; } = "";

    public string Description { get; init; } = "";

    public string Journal { get; init; } = "";

    public int Question { get; init; }

    public IReadOnlyCollection<Race>? Races { get; init; }

    public IReadOnlyCollection<ProfessionName>? Professions { get; init; }
}
