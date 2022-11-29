using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Ranks;

[PublicAPI]
[DataTransferObject]
public sealed record Level
{
    public required int MinRank { get; init; }

    public required int MaxRank { get; init; }

    public required int Points { get; init; }
}
