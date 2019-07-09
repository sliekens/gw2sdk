using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class WeaponDiscriminatorOptions : DiscriminatorOptions
    {
        public WeaponDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Weapon);

        public override string DiscriminatorFieldName => "weapon_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
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

        public object Create(Type objectType)
        {
            if (objectType == typeof(Axe)) return new Axe();
            if (objectType == typeof(Dagger)) return new Dagger();
            if (objectType == typeof(Focus)) return new Focus();
            if (objectType == typeof(Greatsword)) return new Greatsword();
            if (objectType == typeof(Hammer)) return new Hammer();
            if (objectType == typeof(Harpoon)) return new Harpoon();
            if (objectType == typeof(LargeBundle)) return new LargeBundle();
            if (objectType == typeof(LongBow)) return new LongBow();
            if (objectType == typeof(Mace)) return new Mace();
            if (objectType == typeof(Pistol)) return new Pistol();
            if (objectType == typeof(Rifle)) return new Rifle();
            if (objectType == typeof(Scepter)) return new Scepter();
            if (objectType == typeof(Shield)) return new Shield();
            if (objectType == typeof(ShortBow)) return new ShortBow();
            if (objectType == typeof(SmallBundle)) return new SmallBundle();
            if (objectType == typeof(Speargun)) return new Speargun();
            if (objectType == typeof(Staff)) return new Staff();
            if (objectType == typeof(Sword)) return new Sword();
            if (objectType == typeof(Torch)) return new Torch();
            if (objectType == typeof(Toy)) return new Toy();
            if (objectType == typeof(ToyTwoHanded)) return new ToyTwoHanded();
            if (objectType == typeof(Trident)) return new Trident();
            if (objectType == typeof(Warhorn)) return new Warhorn();
            return new Weapon();
        }
    }
}
