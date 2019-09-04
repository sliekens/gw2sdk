using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Continents.Impl
{
    public sealed class PointOfInterestDiscriminatorOptions : DiscriminatorOptions
    {
        public PointOfInterestDiscriminatorOptions()
        {
            Activator = Create;
        }

        public override Type BaseType => typeof(PointOfInterest);

        public override string DiscriminatorFieldName => "type";

        public override bool SerializeDiscriminator => false;

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("landmark", typeof(Landmark));
            yield return ("waypoint", typeof(Waypoint));
            yield return ("vista", typeof(Vista));
            yield return ("unlock", typeof(UnlockerPointOfInterest));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(Landmark)) return new Landmark();
            if (objectType == typeof(Waypoint)) return new Waypoint();
            if (objectType == typeof(Vista)) return new Vista();
            if (objectType == typeof(UnlockerPointOfInterest)) return new UnlockerPointOfInterest();
            return new PointOfInterest();
        }
    }
}
