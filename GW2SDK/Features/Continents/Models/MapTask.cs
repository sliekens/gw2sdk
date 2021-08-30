using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record MapTask
    {
        public int Id { get; init; }

        public string Objective { get; init; } = "";

        public int Level { get; init; }

        public double[] Coordinates { get; init; } = Array.Empty<double>();

        public double[][] Boundaries { get; init; } = Array.Empty<double[]>();

        public string ChatLink { get; init; } = "";
    }
}
