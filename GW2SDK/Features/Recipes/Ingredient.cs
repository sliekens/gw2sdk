using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class Ingredient
    {
        public int ItemId { get; set; }

        public int Count { get; set; }
    }
}
