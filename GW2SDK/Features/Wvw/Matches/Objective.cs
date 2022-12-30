using System;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Objective
{
    public required string Id { get; init; }

    public required TeamColor Owner { get; init; }

    public required DateTimeOffset LastFlipped { get; init; }

    public required int PointsTick { get; init; }

    public required int PointsCapture { get; init; }
}
