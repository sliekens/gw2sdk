﻿using System.Collections.Generic;
using System.Drawing;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Objectives;

[PublicAPI]
[DataTransferObject]
public sealed record Objective
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required int SectorId { get; init; }

    public required ObjectiveKind Kind { get; init; }

    public required MapKind MapKind { get; init; }

    public required int MapId { get; init; }

    public required int? UpgradeId { get; init; }

    public required IReadOnlyCollection<double>? Coordinates { get; init; }

    public required PointF? LabelCoordinates { get; init; }

    public required string MarkerHref { get; init; }

    public required string ChatLink { get; init; }
}
