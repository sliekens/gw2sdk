using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    public record Recipe
    {
        public int Id { get; init; }

        public int OutputItemId { get; init; }

        public int OutputItemCount { get; init; }

        public int MinRating { get; init; }

        public TimeSpan TimeToCraft { get; init; }

        public IReadOnlyCollection<CraftingDisciplineName> Disciplines { get; init; } =
            Array.Empty<CraftingDisciplineName>();

        public IReadOnlyCollection<RecipeFlag> Flags { get; init; } = Array.Empty<RecipeFlag>();

        public IReadOnlyCollection<Ingredient> Ingredients { get; init; } = Array.Empty<Ingredient>();

        public string ChatLink { get; init; } = "";
    }
}
