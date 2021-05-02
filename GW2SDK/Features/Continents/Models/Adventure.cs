using GW2SDK.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Adventure
    {
        public string Id { get; init; } = "";

        public string Name { get; init; } = "";

        public string Description { get; init; } = "";

        public double[] Coordinates { get; init; } = new double[0];
    }
}
