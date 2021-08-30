using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record SkillChallenge
    {
        public string Id { get; init; } = "";

        public double[] Coordinates { get; init; } = Array.Empty<double>();
    }
}
