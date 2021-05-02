using GW2SDK.Annotations;

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

        public double[] Coordinates { get; init; } = new double[0];

        public string ChatLink { get; init; } = "";
    }
}
