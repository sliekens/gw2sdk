using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Items.Impl
{
    internal sealed class GizmoDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Gizmo);

        internal override string DiscriminatorFieldName => "gizmo_type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("ContainerKey", typeof(ContainerKey));
            yield return ("Default", typeof(Gizmo));
            yield return ("RentableContractNpc", typeof(RentableContractNpc));
            yield return ("UnlimitedConsumable", typeof(UnlimitedConsumable));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Gizmo)) return new Gizmo();
            if (discriminatedType == typeof(ContainerKey)) return new ContainerKey();
            if (discriminatedType == typeof(RentableContractNpc)) return new RentableContractNpc();
            if (discriminatedType == typeof(UnlimitedConsumable)) return new UnlimitedConsumable();
            return new Gizmo();
        }
    }
}
