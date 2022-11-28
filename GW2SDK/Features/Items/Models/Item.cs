using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Item
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required int Level { get; init; }

    public required Rarity Rarity { get; init; }

    public required Coin VendorValue { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<GameType> GameTypes { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<ItemFlag> Flags { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<ItemRestriction> Restrictions { get; init; }

    public required string ChatLink { get; init; }

    public required string? Icon { get; init; }
}
