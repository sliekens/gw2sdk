using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class GatheringToolDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(GatheringTool);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Foraging", typeof(ForagingTool));
            yield return ("Logging", typeof(LoggingTool));
            yield return ("Mining", typeof(MiningTool));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(ForagingTool)) return new ForagingTool();
            if (discriminatedType == typeof(LoggingTool)) return new LoggingTool();
            if (discriminatedType == typeof(MiningTool)) return new MiningTool();
            return new GatheringTool();
        }
    }
}
