using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
[DataTransferObject]
public sealed record AccountSummary
{
    public string Id { get; init; } = "";

    public string Name { get; init; } = "";

    public TimeSpan Age { get; init; }

    public DateTimeOffset LastModified { get; init; }

    public int World { get; init; }

    public IReadOnlyCollection<string> Guilds { get; init; } = Array.Empty<string>();

    [Scope(Permission.Guilds)]
    public IReadOnlyCollection<string>? GuildLeader { get; init; }

    public DateTimeOffset Created { get; init; }

    public IReadOnlyCollection<ProductName> Access { get; init; } = Array.Empty<ProductName>();

    public bool Commander { get; init; }

    [Scope(Permission.Progression)]
    public int? FractalLevel { get; init; }

    [Scope(Permission.Progression)]
    public int? DailyAp { get; init; }

    [Scope(Permission.Progression)]
    public int? MonthlyAp { get; init; }

    [Scope(Permission.Progression)]
    public int? WvwRank { get; init; }

    [Scope(Permission.Builds)]
    public int? BuildStorageSlots { get; init; }
}