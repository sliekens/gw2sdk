using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record UnlockedRecipesView
    {
        public IEnumerable<int> Recipes { get; init; } = new List<int>(0);

    }
}