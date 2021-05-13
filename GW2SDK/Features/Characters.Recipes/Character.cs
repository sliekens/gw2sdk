using System.Collections.Generic;
using GW2SDK.Annotations;

namespace GW2SDK.Characters.Recipes
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Character
    {
        public IEnumerable<int> Recipes { get; init; } = new List<int>(0);
    }
}
