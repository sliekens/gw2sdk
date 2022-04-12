using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Maps;

[PublicAPI]
[DataTransferObject]
public sealed record World
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public WorldPopulation Population { get; init; }
}