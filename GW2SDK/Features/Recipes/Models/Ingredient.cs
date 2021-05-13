using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Ingredient
    {
        public int ItemId { get; init; }

        public int Count { get; init; }
    }
}
