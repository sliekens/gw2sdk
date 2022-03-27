using System.Drawing;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public record PointOfInterest
    {
        /// <summary>The name as displayed on the map, or an empty string if the PoI doesn't have a name.</summary>
        public string Name { get; init; } = "";

        public int Id { get; init; }

        public int Floor { get; init; }

        public PointF Coordinates { get; init; }

        public string ChatLink { get; init; } = "";
    }
}
