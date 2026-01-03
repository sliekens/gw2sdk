namespace GuildWars2.Hero.Banking;

/// <summary>Information about a material category in material storage.</summary>
public sealed record MaterialCategory
{
    /// <summary>The category ID.</summary>
    public required int Id { get; init; }

    /// <summary>The category name.</summary>
    public required string Name { get; init; }

    /// <summary>The IDs of the items in this category.</summary>
    public required IReadOnlyList<int> Items { get; init; }

    /// <summary>The display order of this category in the material storage.</summary>
    public required int Order { get; init; }
}
