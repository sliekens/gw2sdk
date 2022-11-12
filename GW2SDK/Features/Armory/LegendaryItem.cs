using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed record LegendaryItem
{
    /// <summary>The item id.</summary>
    public required int Id { get; init; }

    /// <summary>The number of items that can be account bound.</summary>
    public required int MaxCount { get; init; }
}
