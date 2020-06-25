using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Skins.Impl
{
    internal sealed class GatheringToolSkinDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(GatheringToolSkin);

        internal override string DiscriminatorFieldName => "gathering_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Foraging", typeof(ForagingToolSkin));
            yield return ("Logging", typeof(LoggingToolSkin));
            yield return ("Mining", typeof(MiningToolSkin));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(ForagingToolSkin)) return new ForagingToolSkin();
            if (discriminatedType == typeof(LoggingToolSkin)) return new LoggingToolSkin();
            if (discriminatedType == typeof(MiningToolSkin)) return new MiningToolSkin();
            return new GatheringToolSkin();
        }
    }
}
