using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Skins.Impl
{
    public sealed class ArmorSkinDiscriminatorOptions : DiscriminatorOptions
    {
        public ArmorSkinDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(ArmorSkin);

        public override string DiscriminatorFieldName => "armor_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Boots", typeof(BootsSkin));
            yield return ("Coat", typeof(CoatSkin));
            yield return ("Gloves", typeof(GlovesSkin));
            yield return ("Helm", typeof(HelmSkin));
            yield return ("HelmAquatic", typeof(HelmAquaticSkin));
            yield return ("Leggings", typeof(LeggingsSkin));
            yield return ("Shoulders", typeof(ShouldersSkin));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(BootsSkin)) return new BootsSkin();
            if (objectType == typeof(CoatSkin)) return new CoatSkin();
            if (objectType == typeof(GlovesSkin)) return new GlovesSkin();
            if (objectType == typeof(HelmSkin)) return new HelmSkin();
            if (objectType == typeof(HelmAquaticSkin)) return new HelmAquaticSkin();
            if (objectType == typeof(LeggingsSkin)) return new LeggingsSkin();
            if (objectType == typeof(ShouldersSkin)) return new ShouldersSkin();
            return new ArmorSkin();
        }
    }
}
