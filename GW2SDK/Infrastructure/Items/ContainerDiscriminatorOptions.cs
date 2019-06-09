using System;
using System.Collections.Generic;
using GW2SDK.Features.Items;

namespace GW2SDK.Infrastructure.Items
{
    public sealed class ContainerDiscriminatorOptions : DiscriminatorOptions
    {
        public ContainerDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Container);

        public override string DiscriminatorFieldName => "container_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Default", typeof(Container));
            yield return ("GiftBox", typeof(GiftBoxContainer));
            yield return ("Immediate", typeof(ImmediateContainer));
            yield return ("OpenUI", typeof(OpenUiContainer));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(Container)) return new Container();
            if (objectType == typeof(GiftBoxContainer)) return new GiftBoxContainer();
            if (objectType == typeof(ImmediateContainer)) return new ImmediateContainer();
            if (objectType == typeof(OpenUiContainer)) return new OpenUiContainer();
            return new Container();
        }
    }
}
