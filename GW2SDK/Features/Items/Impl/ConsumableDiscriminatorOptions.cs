using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class ConsumableDiscriminatorOptions : DiscriminatorOptions
    {
        public ConsumableDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Consumable);

        public override string DiscriminatorFieldName => "consumable_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
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

        public object Create(Type objectType)
        {
            if (objectType == typeof(Consumable)) return new Consumable();
            if (objectType == typeof(AppearanceChanger)) return new AppearanceChanger();
            if (objectType == typeof(Booze)) return new Booze();
            if (objectType == typeof(ContractNpc)) return new ContractNpc();
            if (objectType == typeof(Currency)) return new Currency();
            if (objectType == typeof(Food)) return new Food();
            if (objectType == typeof(HalloweenConsumable)) return new HalloweenConsumable();
            if (objectType == typeof(ImmediateConsumable)) return new ImmediateConsumable();
            if (objectType == typeof(MountRandomUnlocker)) return new MountRandomUnlocker();
            if (objectType == typeof(RandomUnlocker)) return new RandomUnlocker();
            if (objectType == typeof(TeleportToFriend)) return new TeleportToFriend();
            if (objectType == typeof(Transmutation)) return new Transmutation();
            if (objectType == typeof(Unlocker)) return new Unlocker();
            if (objectType == typeof(UpgradeRemover)) return new UpgradeRemover();
            if (objectType == typeof(Utility)) return new Utility();
            return new Consumable();
        }
    }
}
