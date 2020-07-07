using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Skins.Impl
{
    internal sealed class WeaponSkinDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(WeaponSkin);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
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

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(AxeSkin)) return new AxeSkin();
            if (discriminatedType == typeof(DaggerSkin)) return new DaggerSkin();
            if (discriminatedType == typeof(FocusSkin)) return new FocusSkin();
            if (discriminatedType == typeof(GreatswordSkin)) return new GreatswordSkin();
            if (discriminatedType == typeof(HammerSkin)) return new HammerSkin();
            if (discriminatedType == typeof(LargeBundleSkin)) return new LargeBundleSkin();
            if (discriminatedType == typeof(LongbowSkin)) return new LongbowSkin();
            if (discriminatedType == typeof(MaceSkin)) return new MaceSkin();
            if (discriminatedType == typeof(PistolSkin)) return new PistolSkin();
            if (discriminatedType == typeof(RifleSkin)) return new RifleSkin();
            if (discriminatedType == typeof(ScepterSkin)) return new ScepterSkin();
            if (discriminatedType == typeof(ShieldSkin)) return new ShieldSkin();
            if (discriminatedType == typeof(ShortbowSkin)) return new ShortbowSkin();
            if (discriminatedType == typeof(SmallBundleSkin)) return new SmallBundleSkin();
            if (discriminatedType == typeof(SpearSkin)) return new SpearSkin();
            if (discriminatedType == typeof(SpeargunSkin)) return new SpeargunSkin();
            if (discriminatedType == typeof(StaffSkin)) return new StaffSkin();
            if (discriminatedType == typeof(SwordSkin)) return new SwordSkin();
            if (discriminatedType == typeof(TorchSkin)) return new TorchSkin();
            if (discriminatedType == typeof(ToySkin)) return new ToySkin();
            if (discriminatedType == typeof(ToyTwoHandedSkin)) return new ToyTwoHandedSkin();
            if (discriminatedType == typeof(TridentSkin)) return new TridentSkin();
            if (discriminatedType == typeof(WarhornSkin)) return new WarhornSkin();
            return new WeaponSkin();
        }
    }
}
