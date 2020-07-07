using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class ToolDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Tool);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Salvage", typeof(SalvageTool));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(SalvageTool)) return new SalvageTool();
            return new Tool();
        }
    }
}
