using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Characters.Recipes
{
    [PublicAPI]
    public interface ICharacterReader : IJsonReader<Character>
    {
    }
}
