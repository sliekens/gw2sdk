using System.Collections.Generic;
using GW2SDK.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Floor
    {
        public int Id { get; init; }

        public double[] TextureDimensions { get; init; } = new double[0];

        public double[][]? ClampedView { get; init; }

        public Dictionary<int, Region> Regions { get; init; } = new(0);
    }
}
