using System;
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

        public CraftingDisciplineName[] Disciplines { get; init; } = new CraftingDisciplineName[0];

        public RecipeFlag[] Flags { get; init; } = new RecipeFlag[0];

        public Ingredient[] Ingredients { get; init; } = new Ingredient[0];

        public string ChatLink { get; init; } = "";
    }
}
