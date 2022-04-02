using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record UnlockedRecipesView
    {
        /// <summary>The IDs of the recipes that the current character has unlocked.</summary>
        /// <summary>This includes unlocked recipes that are unavailable to the character's active crafting disciplines.</summary>
        public IEnumerable<int> Recipes { get; init; } = new List<int>(0);
    }
}
