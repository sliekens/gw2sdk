using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Continents.Impl
{
    internal sealed class PointOfInterestDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(PointOfInterest);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("landmark", typeof(Landmark));
            yield return ("waypoint", typeof(Waypoint));
            yield return ("vista", typeof(Vista));
            yield return ("unlock", typeof(UnlockerPointOfInterest));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Landmark)) return new Landmark();
            if (discriminatedType == typeof(Waypoint)) return new Waypoint();
            if (discriminatedType == typeof(Vista)) return new Vista();
            if (discriminatedType == typeof(UnlockerPointOfInterest)) return new UnlockerPointOfInterest();
            return new PointOfInterest();
        }
    }
}
