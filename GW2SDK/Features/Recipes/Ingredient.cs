using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class Ingredient
    {
        public int ItemId { get; set; }

        public int Count { get; set; }
    }
}
