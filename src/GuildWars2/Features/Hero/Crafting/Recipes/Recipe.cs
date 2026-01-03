using System.Text.Json.Serialization;

using GuildWars2.Chat;
using GuildWars2.Hero.Crafting.Disciplines;

namespace GuildWars2.Hero.Crafting.Recipes;

/// <summary>Information about a crafting recipe. This is the base type. Cast objects of this type to a more specific type
/// to access more properties.</summary>
[Inheritable]
[DataTransferObject]
[JsonConverter(typeof(RecipeJsonConverter))]
public record Recipe
{
    /// <summary>The recipe ID.</summary>
    public required int Id { get; init; }

    /// <summary>The ID of the item that is crafted by this recipe.</summary>
    public required int OutputItemId { get; init; }

    /// <summary>How many items are crafted by this recipe.</summary>
    public required int OutputItemCount { get; init; }

    /// <summary>The required level to craft this recipe.</summary>
    public required int MinRating { get; init; }

    /// <summary>The duration of the crafting process.</summary>
    public required TimeSpan TimeToCraft { get; init; }

    /// <summary>The crafting disciplines that can use this recipe.</summary>
    public required IReadOnlyCollection<Extensible<CraftingDisciplineName>> Disciplines
    {
        get;
        init;
    }

    /// <summary>Contains various modifiers.</summary>
    public required RecipeFlags Flags { get; init; }

    /// <summary>The crafting ingredients required to craft the recipe.</summary>
    public required IReadOnlyList<Ingredient> Ingredients { get; init; }

#pragma warning disable CA1721 // Property names should not match get methods
    /// <summary>The chat code of the recipe. This can be used to link the recipe in the chat, but also in guild or squad
    /// messages.</summary>
    public required string ChatLink { get; init; }
#pragma warning restore CA1721 // Property names should not match get methods

#pragma warning disable CA1024 // Use properties where appropriate
    /// <summary>Gets a chat link object for this recipe.</summary>
    /// <returns>The chat link as an object.</returns>
    public RecipeLink GetChatLink()
    {
        return new() { RecipeId = Id };
    }
#pragma warning restore CA1024 // Use properties where appropriate
}
