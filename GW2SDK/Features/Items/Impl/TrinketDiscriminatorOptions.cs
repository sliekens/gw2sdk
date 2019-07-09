using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class TrinketDiscriminatorOptions : DiscriminatorOptions
    {
        public TrinketDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Trinket);

        public override string DiscriminatorFieldName => "trinket_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Accessory", typeof(Accessory));
            yield return ("Amulet", typeof(Amulet));
            yield return ("Ring", typeof(Ring));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(Accessory)) return new Accessory();
            if (objectType == typeof(Amulet)) return new Amulet();
            if (objectType == typeof(Ring)) return new Ring();
            return new Trinket();
        }
    }
}
