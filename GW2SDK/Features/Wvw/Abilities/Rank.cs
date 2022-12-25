using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Abilities;

[PublicAPI]
[DataTransferObject]
public sealed record Rank
{
    public required int Cost { get; init; }

    public required string Effect { get; init; }
}
