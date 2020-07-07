using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class UpgradeComponentDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(UpgradeComponent);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Default", typeof(UpgradeComponent));
            yield return ("Gem", typeof(Gem));
            yield return ("Rune", typeof(Rune));
            yield return ("Sigil", typeof(Sigil));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(UpgradeComponent)) return new UpgradeComponent();
            if (discriminatedType == typeof(Gem)) return new Gem();
            if (discriminatedType == typeof(Rune)) return new Rune();
            if (discriminatedType == typeof(Sigil)) return new Sigil();
            return new UpgradeComponent();
        }
    }
}
