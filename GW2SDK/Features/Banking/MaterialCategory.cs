namespace GuildWars2.Banking;

/// <summary>Information about a material category in material storage.</summary>
[PublicAPI]
public sealed record MaterialCategory
{
    /// <summary>The category ID.</summary>
    public required int Id { get; init; }

    /// <summary>The category name.</summary>
    public required string Name { get; init; }

    /// <summary>The IDs of the items in this category.</summary>
    public required IReadOnlyList<int> Items { get; init; }

    /// <summary>The order of this category in the material storage. Categories are sorted by this value in ascending order.</summary>
    public required int Order { get; init; }
}
