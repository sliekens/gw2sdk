using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record Score
{
    public required string Id { get; init; }

    public required int Value { get; init; }
}
