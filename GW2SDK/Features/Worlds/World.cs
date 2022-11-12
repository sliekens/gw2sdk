using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Worlds;

[PublicAPI]
[DataTransferObject]
public sealed record World
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required WorldPopulation Population { get; init; }
}
