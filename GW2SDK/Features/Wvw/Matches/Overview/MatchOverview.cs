using System;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches.Overview;

[PublicAPI]
[DataTransferObject]
public sealed record MatchOverview
{
    public required string Id { get; init; }

    public required Worlds Worlds { get; init; }

    public required AllWorlds AllWorlds { get; init; }

    public required DateTimeOffset StartTime { get; init; }

    public required DateTimeOffset EndTime { get; init; }
}
