using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class ContainerDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Container);

        internal override string DiscriminatorFieldName => "container_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Default", typeof(Container));
            yield return ("GiftBox", typeof(GiftBoxContainer));
            yield return ("Immediate", typeof(ImmediateContainer));
            yield return ("OpenUI", typeof(OpenUiContainer));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Container)) return new Container();
            if (discriminatedType == typeof(GiftBoxContainer)) return new GiftBoxContainer();
            if (discriminatedType == typeof(ImmediateContainer)) return new ImmediateContainer();
            if (discriminatedType == typeof(OpenUiContainer)) return new OpenUiContainer();
            return new Container();
        }
    }
}
