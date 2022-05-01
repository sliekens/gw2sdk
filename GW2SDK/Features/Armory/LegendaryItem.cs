using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed record LegendaryItem
{
    public int Id { get; init; }

    public int MaxCount { get; init; }
}
