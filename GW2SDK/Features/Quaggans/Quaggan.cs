﻿namespace GuildWars2.Quaggans;

[PublicAPI]
[DataTransferObject]
public sealed record Quaggan
{
    public required string Id { get; init; }

    public required string ImageHref { get; init; }
}
