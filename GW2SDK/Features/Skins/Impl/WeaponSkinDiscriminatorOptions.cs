using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Skins.Impl
{
    public sealed class WeaponSkinDiscriminatorOptions : DiscriminatorOptions
    {
        public WeaponSkinDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(WeaponSkin);

        public override string DiscriminatorFieldName => "weapon_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Axe", typeof(AxeSkin));
            yield return ("Dagger", typeof(DaggerSkin));
            yield return ("Focus", typeof(FocusSkin));
            yield return ("Greatsword", typeof(GreatswordSkin));
            yield return ("Hammer", typeof(HammerSkin));
            yield return ("LargeBundle", typeof(LargeBundleSkin));
            yield return ("Longbow", typeof(LongbowSkin));
            yield return ("Mace", typeof(MaceSkin));
            yield return ("Pistol", typeof(PistolSkin));
            yield return ("Rifle", typeof(RifleSkin));
            yield return ("Scepter", typeof(ScepterSkin));
            yield return ("Shield", typeof(ShieldSkin));
            yield return ("Shortbow", typeof(ShortbowSkin));
            yield return ("SmallBundle", typeof(SmallBundleSkin));
            yield return ("Spear", typeof(SpearSkin));
            yield return ("Speargun", typeof(SpeargunSkin));
            yield return ("Staff", typeof(StaffSkin));
            yield return ("Sword", typeof(SwordSkin));
            yield return ("Torch", typeof(TorchSkin));
            yield return ("Toy", typeof(ToySkin));
            yield return ("ToyTwoHanded", typeof(ToyTwoHandedSkin));
            yield return ("Trident", typeof(TridentSkin));
            yield return ("Warhorn", typeof(WarhornSkin));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(AxeSkin)) return new AxeSkin();
            if (objectType == typeof(DaggerSkin)) return new DaggerSkin();
            if (objectType == typeof(FocusSkin)) return new FocusSkin();
            if (objectType == typeof(GreatswordSkin)) return new GreatswordSkin();
            if (objectType == typeof(HammerSkin)) return new HammerSkin();
            if (objectType == typeof(LargeBundleSkin)) return new LargeBundleSkin();
            if (objectType == typeof(LongbowSkin)) return new LongbowSkin();
            if (objectType == typeof(MaceSkin)) return new MaceSkin();
            if (objectType == typeof(PistolSkin)) return new PistolSkin();
            if (objectType == typeof(RifleSkin)) return new RifleSkin();
            if (objectType == typeof(ScepterSkin)) return new ScepterSkin();
            if (objectType == typeof(ShieldSkin)) return new ShieldSkin();
            if (objectType == typeof(ShortbowSkin)) return new ShortbowSkin();
            if (objectType == typeof(SmallBundleSkin)) return new SmallBundleSkin();
            if (objectType == typeof(SpearSkin)) return new SpearSkin();
            if (objectType == typeof(SpeargunSkin)) return new SpeargunSkin();
            if (objectType == typeof(StaffSkin)) return new StaffSkin();
            if (objectType == typeof(SwordSkin)) return new SwordSkin();
            if (objectType == typeof(TorchSkin)) return new TorchSkin();
            if (objectType == typeof(ToySkin)) return new ToySkin();
            if (objectType == typeof(ToyTwoHandedSkin)) return new ToyTwoHandedSkin();
            if (objectType == typeof(TridentSkin)) return new TridentSkin();
            if (objectType == typeof(WarhornSkin)) return new WarhornSkin();
            return new WeaponSkin();
        }
    }
}