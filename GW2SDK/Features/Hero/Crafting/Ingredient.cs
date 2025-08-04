namespace GuildWars2.Hero.Crafting;

/// <summary>Information about an ingredient required to craft an item.</summary>
[DataTransferObject]
public sealed record Ingredient
{
    /// <summary>The type of ingredient.</summary>
    public required Extensible<IngredientKind> Kind { get; init; }

    /// <summary>The ingredient ID. This can be an item ID, currency ID or guild upgrade ID, depending on the ingredient kind.</summary>
    public required int Id { get; init; }

    /// <summary>The amount of the ingredient required to craft the item.</summary>
    public required int Count { get; init; }
}
