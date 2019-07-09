using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class UnlockerDiscriminatorOptions : DiscriminatorOptions
    {
        public UnlockerDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Unlocker);

        public override string DiscriminatorFieldName => "unlock_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("BagSlot", typeof(BagSlotUnlocker));
            yield return ("BankTab", typeof(BankTabUnlocker));
            yield return ("Champion", typeof(ChampionUnlocker));
            yield return ("CollectibleCapacity", typeof(CollectibleCapacityUnlocker));
            yield return ("Content", typeof(ContentUnlocker));
            yield return ("CraftingRecipe", typeof(CraftingRecipeUnlocker));
            yield return ("Dye", typeof(DyeUnlocker));
            yield return ("GliderSkin", typeof(GliderSkinUnlocker));
            yield return ("Minipet", typeof(MinipetUnlocker));
            yield return ("Ms", typeof(MsUnlocker));
            yield return ("Outfit", typeof(OutfitUnlocker));
            yield return ("SharedSlot", typeof(SharedSlotUnlocker));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(Unlocker)) return new Unlocker();
            if (objectType == typeof(BagSlotUnlocker)) return new BagSlotUnlocker();
            if (objectType == typeof(BankTabUnlocker)) return new BankTabUnlocker();
            if (objectType == typeof(ChampionUnlocker)) return new ChampionUnlocker();
            if (objectType == typeof(CollectibleCapacityUnlocker)) return new CollectibleCapacityUnlocker();
            if (objectType == typeof(ContentUnlocker)) return new ContentUnlocker();
            if (objectType == typeof(CraftingRecipeUnlocker)) return new CraftingRecipeUnlocker();
            if (objectType == typeof(DyeUnlocker)) return new DyeUnlocker();
            if (objectType == typeof(GliderSkinUnlocker)) return new GliderSkinUnlocker();
            if (objectType == typeof(MinipetUnlocker)) return new MinipetUnlocker();
            if (objectType == typeof(MsUnlocker)) return new MsUnlocker();
            if (objectType == typeof(OutfitUnlocker)) return new OutfitUnlocker();
            if (objectType == typeof(SharedSlotUnlocker)) return new SharedSlotUnlocker();
            return new Unlocker();
        }
    }
}