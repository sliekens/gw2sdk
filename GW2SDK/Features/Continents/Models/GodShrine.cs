using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record GodShrine
    {
        public int Id { get; init; }

        public int PointOfInterestId { get; init; }

        public string Name { get; init; } = "";

        public string NameContested { get; init; } = "";

        public string Icon { get; init; } = "";

        public string IconContested { get; init; } = "";

        public IReadOnlyCollection<double> Coordinates { get; init; } = Array.Empty<double>();
    }
}
