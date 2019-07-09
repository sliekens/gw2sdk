using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class UpgradeComponentDiscriminatorOptions : DiscriminatorOptions
    {
        public UpgradeComponentDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(UpgradeComponent);

        public override string DiscriminatorFieldName => "upgrade_component_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Default", typeof(UpgradeComponent));
            yield return ("Gem", typeof(Gem));
            yield return ("Rune", typeof(Rune));
            yield return ("Sigil", typeof(Sigil));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(UpgradeComponent)) return new UpgradeComponent();
            if (objectType == typeof(Gem)) return new Gem();
            if (objectType == typeof(Rune)) return new Rune();
            if (objectType == typeof(Sigil)) return new Sigil();
            return new UpgradeComponent();
        }
    }
}
