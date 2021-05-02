using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    public interface IRecipeReader : IJsonReader<Recipe>
    {
        IJsonReader<int> Id { get; }
    }
}
