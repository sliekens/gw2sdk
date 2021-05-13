using JetBrains.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts.Recipes
{
    [PublicAPI]
    public interface IAccountRecipeReader
    {
        IJsonReader<int> Id { get; }
    }
}