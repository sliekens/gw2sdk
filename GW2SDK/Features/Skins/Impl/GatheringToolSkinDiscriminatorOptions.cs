using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Skins.Impl
{
    public sealed class GatheringToolSkinDiscriminatorOptions : DiscriminatorOptions
    {
        public GatheringToolSkinDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(GatheringToolSkin);

        public override string DiscriminatorFieldName => "gathering_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Foraging", typeof(ForagingToolSkin));
            yield return ("Logging", typeof(LoggingToolSkin));
            yield return ("Mining", typeof(MiningToolSkin));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(ForagingToolSkin)) return new ForagingToolSkin();
            if (objectType == typeof(LoggingToolSkin)) return new LoggingToolSkin();
            if (objectType == typeof(MiningToolSkin)) return new MiningToolSkin();
            return new GatheringToolSkin();
        }
    }
}