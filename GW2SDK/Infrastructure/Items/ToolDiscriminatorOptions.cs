using System;
using System.Collections.Generic;
using GW2SDK.Features.Items;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class ToolDiscriminatorOptions : DiscriminatorOptions
    {
        public ToolDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Tool);

        public override string DiscriminatorFieldName => "tool_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Salvage", typeof(SalvageTool));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(SalvageTool)) return new SalvageTool();
            return new Tool();
        }
    }
}