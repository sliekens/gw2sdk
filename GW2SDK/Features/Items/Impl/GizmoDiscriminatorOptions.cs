using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    public sealed class GizmoDiscriminatorOptions : DiscriminatorOptions
    {
        public GizmoDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(Gizmo);

        public override string DiscriminatorFieldName => "gizmo_type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("ContainerKey", typeof(ContainerKey));
            yield return ("Default", typeof(Gizmo));
            yield return ("RentableContractNpc", typeof(RentableContractNpc));
            yield return ("UnlimitedConsumable", typeof(UnlimitedConsumable));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(Gizmo)) return new Gizmo();
            if (objectType == typeof(ContainerKey)) return new ContainerKey();
            if (objectType == typeof(RentableContractNpc)) return new RentableContractNpc();
            if (objectType == typeof(UnlimitedConsumable)) return new UnlimitedConsumable();
            return new Gizmo();
        }
    }
}
