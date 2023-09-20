﻿namespace GuildWars2.Items;

[PublicAPI]
[DataTransferObject]
public sealed record InfixUpgrade
{
    public required int ItemstatsId { get; init; }

    public required IReadOnlyCollection<UpgradeAttribute> Attributes { get; init; }

    public required Buff? Buff { get; init; }
}
