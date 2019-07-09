using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class GuildIngredient
    {
        public int UpgradeId { get; set; }

        public int Count { get; set; }
    }
}
