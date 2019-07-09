using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class GatheringToolDiscriminatorOptions : DiscriminatorOptions
    {
        public GatheringToolDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(GatheringTool);

        public override string DiscriminatorFieldName => "gathering_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Foraging", typeof(ForagingTool));
            yield return ("Logging", typeof(LoggingTool));
            yield return ("Mining", typeof(MiningTool));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(ForagingTool)) return new ForagingTool();
            if (objectType == typeof(LoggingTool)) return new LoggingTool();
            if (objectType == typeof(MiningTool)) return new MiningTool();
            return new GatheringTool();
        }
    }
}
