using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record RankTier
{
    public required int Rating { get; init; }
}
