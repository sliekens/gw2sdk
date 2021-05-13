using GW2SDK.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record MapSector
    {
        public int Id { get; init; }

        public string? Name { get; init; }

        public int Level { get; init; }

        public double[] Coordinates { get; init; } = new double[0];

        public double[][] Boundaries { get; init; } = new double[0][];

        public string ChatLink { get; init; } = "";
    }
}
