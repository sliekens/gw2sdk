using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class ConsumableDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Consumable);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("AppearanceChange", typeof(AppearanceChanger));
            yield return ("Booze", typeof(Booze));
            yield return ("ContractNpc", typeof(ContractNpc));
            yield return ("Currency", typeof(Currency));
            yield return ("Food", typeof(Food));
            yield return ("Generic", typeof(Consumable));
            yield return ("Halloween", typeof(HalloweenConsumable));
            yield return ("Immediate", typeof(ImmediateConsumable));
            yield return ("MountRandomUnlock", typeof(MountRandomUnlocker));
            yield return ("RandomUnlock", typeof(RandomUnlocker));
            yield return ("TeleportToFriend", typeof(TeleportToFriend));
            yield return ("Transmutation", typeof(Transmutation));
            yield return ("Unlock", typeof(Unlocker));
            yield return ("UpgradeRemoval", typeof(UpgradeRemover));
            yield return ("Utility", typeof(Utility));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Consumable)) return new Consumable();
            if (discriminatedType == typeof(AppearanceChanger)) return new AppearanceChanger();
            if (discriminatedType == typeof(Booze)) return new Booze();
            if (discriminatedType == typeof(ContractNpc)) return new ContractNpc();
            if (discriminatedType == typeof(Currency)) return new Currency();
            if (discriminatedType == typeof(Food)) return new Food();
            if (discriminatedType == typeof(HalloweenConsumable)) return new HalloweenConsumable();
            if (discriminatedType == typeof(ImmediateConsumable)) return new ImmediateConsumable();
            if (discriminatedType == typeof(MountRandomUnlocker)) return new MountRandomUnlocker();
            if (discriminatedType == typeof(RandomUnlocker)) return new RandomUnlocker();
            if (discriminatedType == typeof(TeleportToFriend)) return new TeleportToFriend();
            if (discriminatedType == typeof(Transmutation)) return new Transmutation();
            if (discriminatedType == typeof(Unlocker)) return new Unlocker();
            if (discriminatedType == typeof(UpgradeRemover)) return new UpgradeRemover();
            if (discriminatedType == typeof(Utility)) return new Utility();
            return new Consumable();
        }
    }
}
