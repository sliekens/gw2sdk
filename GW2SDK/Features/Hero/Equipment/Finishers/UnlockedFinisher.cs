﻿namespace GuildWars2.Hero.Equipment.Finishers;

[PublicAPI]
[DataTransferObject]
public sealed record UnlockedFinisher
{
    public required int Id { get; init; }

    public required bool Permanent { get; init; }

    public required int? Quantity { get; init; }
}