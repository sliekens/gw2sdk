using GW2SDK.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record MasteryPoint
    {
        public int Id { get; init; }

        public MasteryRegionName Region { get; init; }

        public double[] Coordinates { get; init; } = new double[0];
    }
}
