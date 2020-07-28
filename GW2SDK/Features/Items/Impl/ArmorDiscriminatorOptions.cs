using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class ArmorDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Armor);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Boots", typeof(Boots));
            yield return ("Coat", typeof(Coat));
            yield return ("Gloves", typeof(Gloves));
            yield return ("Helm", typeof(Helm));
            yield return ("HelmAquatic", typeof(HelmAquatic));
            yield return ("Leggings", typeof(Leggings));
            yield return ("Shoulders", typeof(Shoulders));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Boots)) return new Boots();
            if (discriminatedType == typeof(Coat)) return new Coat();
            if (discriminatedType == typeof(Gloves)) return new Gloves();
            if (discriminatedType == typeof(Helm)) return new Helm();
            if (discriminatedType == typeof(HelmAquatic)) return new HelmAquatic();
            if (discriminatedType == typeof(Leggings)) return new Leggings();
            if (discriminatedType == typeof(Shoulders)) return new Shoulders();
            return new Armor();
        }
    }
}
