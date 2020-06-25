using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class WeaponDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Weapon);

        internal override string DiscriminatorFieldName => "weapon_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Axe", typeof(Axe));
            yield return ("Dagger", typeof(Dagger));
            yield return ("Focus", typeof(Focus));
            yield return ("Greatsword", typeof(Greatsword));
            yield return ("Hammer", typeof(Hammer));
            yield return ("Harpoon", typeof(Harpoon));
            yield return ("LargeBundle", typeof(LargeBundle));
            yield return ("LongBow", typeof(LongBow));
            yield return ("Mace", typeof(Mace));
            yield return ("Pistol", typeof(Pistol));
            yield return ("Rifle", typeof(Rifle));
            yield return ("Scepter", typeof(Scepter));
            yield return ("Shield", typeof(Shield));
            yield return ("ShortBow", typeof(ShortBow));
            yield return ("SmallBundle", typeof(SmallBundle));
            yield return ("Speargun", typeof(Speargun));
            yield return ("Staff", typeof(Staff));
            yield return ("Sword", typeof(Sword));
            yield return ("Torch", typeof(Torch));
            yield return ("Toy", typeof(Toy));
            yield return ("ToyTwoHanded", typeof(ToyTwoHanded));
            yield return ("Trident", typeof(Trident));
            yield return ("Warhorn", typeof(Warhorn));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Axe)) return new Axe();
            if (discriminatedType == typeof(Dagger)) return new Dagger();
            if (discriminatedType == typeof(Focus)) return new Focus();
            if (discriminatedType == typeof(Greatsword)) return new Greatsword();
            if (discriminatedType == typeof(Hammer)) return new Hammer();
            if (discriminatedType == typeof(Harpoon)) return new Harpoon();
            if (discriminatedType == typeof(LargeBundle)) return new LargeBundle();
            if (discriminatedType == typeof(LongBow)) return new LongBow();
            if (discriminatedType == typeof(Mace)) return new Mace();
            if (discriminatedType == typeof(Pistol)) return new Pistol();
            if (discriminatedType == typeof(Rifle)) return new Rifle();
            if (discriminatedType == typeof(Scepter)) return new Scepter();
            if (discriminatedType == typeof(Shield)) return new Shield();
            if (discriminatedType == typeof(ShortBow)) return new ShortBow();
            if (discriminatedType == typeof(SmallBundle)) return new SmallBundle();
            if (discriminatedType == typeof(Speargun)) return new Speargun();
            if (discriminatedType == typeof(Staff)) return new Staff();
            if (discriminatedType == typeof(Sword)) return new Sword();
            if (discriminatedType == typeof(Torch)) return new Torch();
            if (discriminatedType == typeof(Toy)) return new Toy();
            if (discriminatedType == typeof(ToyTwoHanded)) return new ToyTwoHanded();
            if (discriminatedType == typeof(Trident)) return new Trident();
            if (discriminatedType == typeof(Warhorn)) return new Warhorn();
            return new Weapon();
        }
    }
}
