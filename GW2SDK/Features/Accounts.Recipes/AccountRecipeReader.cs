using GW2SDK.Annotations;
using GW2SDK.Json;

namespace GW2SDK.Accounts.Recipes
{
    [PublicAPI]
    public sealed class AccountRecipeReader : IAccountRecipeReader
    {
        public IJsonReader<int> Id { get; } = new Int32JsonReader();
    }
}