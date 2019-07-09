using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class ArmorDiscriminatorOptions : DiscriminatorOptions
    {
        public ArmorDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Armor);

        public override string DiscriminatorFieldName => "armor_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Boots", typeof(Boots));
            yield return ("Coat", typeof(Coat));
            yield return ("Gloves", typeof(Gloves));
            yield return ("Helm", typeof(Helm));
            yield return ("HelmAquatic", typeof(HelmAquatic));
            yield return ("Leggings", typeof(Leggings));
            yield return ("Shoulders", typeof(Shoulders));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(Boots)) return new Boots();
            if (objectType == typeof(Coat)) return new Coat();
            if (objectType == typeof(Gloves)) return new Gloves();
            if (objectType == typeof(Helm)) return new Helm();
            if (objectType == typeof(HelmAquatic)) return new HelmAquatic();
            if (objectType == typeof(Leggings)) return new Leggings();
            if (objectType == typeof(Shoulders)) return new Shoulders();
            return new Armor();
        }
    }
}
