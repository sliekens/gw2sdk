﻿namespace GuildWars2.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record Build
{
    public required int Id { get; init; }
}
