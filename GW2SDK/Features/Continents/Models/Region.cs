using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Region
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public double[] LabelCoordinates { get; init; } = Array.Empty<double>();

        public double[][] ContinentRectangle { get; init; } = Array.Empty<double[]>();

        public Dictionary<int, Map> Maps { get; init; } = new(0);
    }
}
