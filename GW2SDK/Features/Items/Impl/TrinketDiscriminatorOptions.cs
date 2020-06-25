using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class TrinketDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Trinket);

        internal override string DiscriminatorFieldName => "trinket_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Accessory", typeof(Accessory));
            yield return ("Amulet", typeof(Amulet));
            yield return ("Ring", typeof(Ring));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Accessory)) return new Accessory();
            if (discriminatedType == typeof(Amulet)) return new Amulet();
            if (discriminatedType == typeof(Ring)) return new Ring();
            return new Trinket();
        }
    }
}
