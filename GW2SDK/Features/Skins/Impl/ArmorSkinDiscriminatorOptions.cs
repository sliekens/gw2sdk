using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Skins.Impl
{
    internal sealed class ArmorSkinDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(ArmorSkin);

        internal override string DiscriminatorFieldName => "armor_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Boots", typeof(BootsSkin));
            yield return ("Coat", typeof(CoatSkin));
            yield return ("Gloves", typeof(GlovesSkin));
            yield return ("Helm", typeof(HelmSkin));
            yield return ("HelmAquatic", typeof(HelmAquaticSkin));
            yield return ("Leggings", typeof(LeggingsSkin));
            yield return ("Shoulders", typeof(ShouldersSkin));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(BootsSkin)) return new BootsSkin();
            if (discriminatedType == typeof(CoatSkin)) return new CoatSkin();
            if (discriminatedType == typeof(GlovesSkin)) return new GlovesSkin();
            if (discriminatedType == typeof(HelmSkin)) return new HelmSkin();
            if (discriminatedType == typeof(HelmAquaticSkin)) return new HelmAquaticSkin();
            if (discriminatedType == typeof(LeggingsSkin)) return new LeggingsSkin();
            if (discriminatedType == typeof(ShouldersSkin)) return new ShouldersSkin();
            return new ArmorSkin();
        }
    }
}
