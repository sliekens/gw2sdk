using GW2SDK.Annotations;

namespace GW2SDK.Recipes
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record GuildIngredient
    {
        public int UpgradeId { get; init; }
        
        public int Count { get; init; }
    }
}
