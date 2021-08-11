using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    public interface ICharacterReader : IJsonReader<Character>
    {
        IJsonReader<string> Name { get; }

        IJsonReader<UnlockedRecipesView> Recipes { get; }
    }
}
