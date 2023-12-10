﻿namespace GuildWars2.Hero.Equipment.Miniatures;

[PublicAPI]
[DataTransferObject]
public sealed record Minipet
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Unlock { get; init; }

    public required string IconHref { get; init; }

    public required int Order { get; init; }

    public required int ItemId { get; init; }
}