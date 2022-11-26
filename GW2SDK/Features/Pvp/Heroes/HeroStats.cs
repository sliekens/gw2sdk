using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pvp.Heroes;

[PublicAPI]
[DataTransferObject]
public sealed record HeroStats
{
    public required int Offense { get; init; }

    public required int Defense { get; init; }

    public required int Speed { get; init; }
}