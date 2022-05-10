using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
public sealed record LegendaryItem
{
    /// <summary>The item id.</summary>
    public int Id { get; init; }

    /// <summary>The number of items that can be account bound.</summary>
    public int MaxCount { get; init; }
}
