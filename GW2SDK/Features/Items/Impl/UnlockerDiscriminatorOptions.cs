using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class UnlockerDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Unlocker);

        internal override string DiscriminatorFieldName => "unlock_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("BagSlot", typeof(BagSlotUnlocker));
            yield return ("BankTab", typeof(BankTabUnlocker));
            yield return ("BuildLibrarySlot", typeof(BuildLibrarySlot));
            yield return ("BuildLoadoutTab", typeof(BuildLoadoutTab));
            yield return ("Champion", typeof(ChampionUnlocker));
            yield return ("CollectibleCapacity", typeof(CollectibleCapacityUnlocker));
            yield return ("Content", typeof(ContentUnlocker));
            yield return ("CraftingRecipe", typeof(CraftingRecipeUnlocker));
            yield return ("Dye", typeof(DyeUnlocker));
            yield return ("GearLoadoutTab", typeof(GearLoadoutTab));
            yield return ("GliderSkin", typeof(GliderSkinUnlocker));
            yield return ("Minipet", typeof(MinipetUnlocker));
            yield return ("Ms", typeof(MsUnlocker));
            yield return ("Outfit", typeof(OutfitUnlocker));
            yield return ("SharedSlot", typeof(SharedSlotUnlocker));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(BagSlotUnlocker)) return new BagSlotUnlocker();
            if (discriminatedType == typeof(BankTabUnlocker)) return new BankTabUnlocker();
            if (discriminatedType == typeof(BuildLibrarySlot)) return new BuildLibrarySlot();
            if (discriminatedType == typeof(BuildLoadoutTab)) return new BuildLoadoutTab();
            if (discriminatedType == typeof(ChampionUnlocker)) return new ChampionUnlocker();
            if (discriminatedType == typeof(CollectibleCapacityUnlocker)) return new CollectibleCapacityUnlocker();
            if (discriminatedType == typeof(ContentUnlocker)) return new ContentUnlocker();
            if (discriminatedType == typeof(CraftingRecipeUnlocker)) return new CraftingRecipeUnlocker();
            if (discriminatedType == typeof(DyeUnlocker)) return new DyeUnlocker();
            if (discriminatedType == typeof(GearLoadoutTab)) return new GearLoadoutTab();
            if (discriminatedType == typeof(GliderSkinUnlocker)) return new GliderSkinUnlocker();
            if (discriminatedType == typeof(MinipetUnlocker)) return new MinipetUnlocker();
            if (discriminatedType == typeof(MsUnlocker)) return new MsUnlocker();
            if (discriminatedType == typeof(OutfitUnlocker)) return new OutfitUnlocker();
            if (discriminatedType == typeof(SharedSlotUnlocker)) return new SharedSlotUnlocker();
            if (discriminatedType == typeof(Unlocker)) return new Unlocker();
            return new Unlocker();
        }
    }
}
