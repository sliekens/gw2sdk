using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
[DataTransferObject]
public sealed record AccountSummary
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required TimeSpan Age { get; init; }

    public required DateTimeOffset LastModified { get; init; }

    public required int World { get; init; }

    public required IReadOnlyCollection<string> Guilds { get; init; }

    [Scope(Permission.Guilds)]
    public required IReadOnlyCollection<string>? GuildLeader { get; init; }

    public required DateTimeOffset Created { get; init; }

    public required IReadOnlyCollection<ProductName> Access { get; init; }

    public required bool Commander { get; init; }

    [Scope(Permission.Progression)]
    public required int? FractalLevel { get; init; }

    [Scope(Permission.Progression)]
    public required int? DailyAp { get; init; }

    [Scope(Permission.Progression)]
    public required int? MonthlyAp { get; init; }

    [Scope(Permission.Progression)]
    public required int? WvwRank { get; init; }

    [Scope(Permission.Builds)]
    public required int? BuildStorageSlots { get; init; }
}
